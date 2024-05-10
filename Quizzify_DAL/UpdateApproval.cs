using System.ComponentModel;

namespace Quizzify_DAL
{
    public class UpdateApproval
    {

        [DefaultValue(false)]
        public bool IsApproved { get; set; }
    }
}
