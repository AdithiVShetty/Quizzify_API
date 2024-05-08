﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Quizzify_DAL.ModelClass
{
    public class Quiz
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string QuizCategory { get; set; }
        [DefaultValue(true)]
        public bool AutoValidation { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public TimeSpan Duration { get; set; }
        [Required]
        public string Level { get; set; }
        [Required]
        public decimal TotalMarks { get; set; }
        [Required]
        public int TotalQuestion { get; set; }
        [DefaultValue(true)]
        public bool IsEnable { get; set; }
        public User User { get; set; }
    }
}