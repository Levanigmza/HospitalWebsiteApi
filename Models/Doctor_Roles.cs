using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalWebsiteApi.Models
{
  
        [Table("DOCTOR_ROLES", Schema = "dbo")]
        public class Doctor_Roles
        {
            [Column("ID")]
            public int Id { get; set; }
            [Column("DOCTOR_ID")]
            public string DoctorID { get; set; }
            [Column("POSSITION")]
            public string Position { get; set; }
        }
    
}
