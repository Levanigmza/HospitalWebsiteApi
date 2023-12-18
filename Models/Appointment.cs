using System.ComponentModel.DataAnnotations.Schema;

namespace HospitalWebsiteApi.Models
{
    [Table("APPOINTMENTS", Schema = "dbo")]
    public class Appointment
    {
        [Column("ID")]
        public int Id { get; set; }
        [Column("USER_ID")]
        public string UserId { get; set; }
        [Column("DOCTOR_ID")]
        public string DoctorId { get; set; }
        [Column("DATE")]
        public string Date { get; set; }
        [Column("TIME")]
        public string Time { get; set; }
        [Column("COMMENT")]
        public string Comment { get; set; }
    }
}
