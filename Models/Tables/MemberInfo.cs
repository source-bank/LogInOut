using System.ComponentModel.DataAnnotations;

namespace LogInOut.Models.Tables
{
    public class MemberInfo
    {
        [Key]
        public int UserNo { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserPwd { get; set; }        
    }
}
