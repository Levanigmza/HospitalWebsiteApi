using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HospitalWebsiteApi.Models
{
    [Table("HOSP_USERS", Schema = "dbo")]

    public class UserParameters
    {
        [Column("ID")]

        public int Id { get; set; }
        [Column("USER_ROLE")]
        public int UserRole { get; set; }

        [Column("NAME")]
        public string? Name { get; set; }
        [Column("EMAIL")]
        public string? Email { get; set; }
        [Column("SURNAME")]
        public string? Surname { get; set; }
        [Column("PASSWORD")]
        public string? Password { get; set; }
        [NotMapped]
        public string? PasswordHash { get; set; }
        [Column("PERSONALID")]
        public int? PersonalID { get; set; }
        [Column("ADDRESS")] 
        public string? Address { get; set; }
        [Column("PHONE")] 
        public int? Phone { get; set; }
        [Column("BIRTHDATE")]
        public DateTime? Birthdate { get; set; }

    }




}
