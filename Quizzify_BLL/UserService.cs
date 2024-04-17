﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Quizzify_DAL;
using System.Security.Cryptography;
using System.Text;

namespace Quizzify_BLL
{
    public class UserService
    {
        private readonly IMapper mapper;
        public UserService()
        {
            var mapConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<UserDTO, User>();
                cfg.CreateMap<Organisation, OrganisationDTO>();
                cfg.CreateMap<OrganisationDTO, Organisation>();
                cfg.CreateMap<UserProfile, UserProfileDTO>();
                cfg.CreateMap<UserProfileDTO, UserProfile>();
            });
            mapper = mapConfig.CreateMapper();
        }
        Quizzify_DAL.QuizzifyDbContext db = new QuizzifyDbContext();

        public OrganisationDTO GetOrganisationByName(string organisationName)
        {
            Organisation organisation = db.Organisations.FirstOrDefault(o => o.Name == organisationName);
            OrganisationDTO organisation1 = mapper.Map<OrganisationDTO>(organisation);
            return organisation1;
        }
        public bool RegisterNewUser(UserDTO userDTO)
        {
            UserDAL userDAL = new UserDAL(db);
            User user = mapper.Map<User>(userDTO);
            bool result = userDAL.RegisterNewUser(user);
            return result;
        }
        public List<OrganisationDTO> GetOrganisations()
        {
            UserDAL userDAL = new UserDAL(db);
            List<Organisation> organisations = userDAL.GetOrganisation();
            List<OrganisationDTO> organisationDTOs = mapper.Map<List<OrganisationDTO>>(organisations);
            return organisationDTOs;
        }
        public int AddOrganisationName(string organisationName)
        {
            UserDAL userDAL = new UserDAL(db);
            int id = userDAL.AddOrganisationName(organisationName);
            return id;
        }
        public bool UpdatePassword(string email, string newPassword)
        {
            UserDAL userDAL = new UserDAL(db);
            return userDAL.UpdatePassword(email, newPassword);
        }
        public bool DoesUserExist(string email)
        {
            UserDAL userDAL = new UserDAL(db);
            return userDAL.DoesUserExist(email);
        }
        public List<string> GetAdminEmailsByOrganisation(string organisationName)
        {
            
            OrganisationDTO organisation = GetOrganisationByName(organisationName);
            Organisation organisation1 = mapper.Map<Organisation>(organisation);
            UserDAL userDAL = new UserDAL(db);
            return userDAL.GetAdminEmailsByOrganisation(organisation1.Id);
        }
        public UserDTO Login(string email, string password)
        {
            DbSet<User> userDb = db.Users;
            string hashedPassword = HashPassword(password);
            string normalPassword = password;
            User loginUser = userDb.FirstOrDefault(u => u.EmailId == email && (u.Password == hashedPassword || u.Password == normalPassword));

            if (loginUser != null)
            {
                UserDTO userDTO = new UserDTO
                {
                    Id = loginUser.Id,
                    EmailId = loginUser.EmailId,
                    Name = loginUser.Name,
                    RoleId = loginUser.RoleId,
                    IsApproved = loginUser.IsApproved,
                };

                return userDTO;
            }
            else
            {
                throw new InvalidOperationException("Invalid EmailId or Password.");
            }
        }
        public UserProfileDTO GetUserProfile(int userId)
        {
            UserDAL userDAL = new UserDAL(db);
            UserProfile userProfile = userDAL.GetUserProfile(userId);
            UserProfileDTO userProfileDTO = mapper.Map<UserProfileDTO>(userProfile);
            return userProfileDTO;
        }
        public string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                byte[] truncatedBytes = new byte[8];
                Array.Copy(bytes, truncatedBytes, 8);

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < truncatedBytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}