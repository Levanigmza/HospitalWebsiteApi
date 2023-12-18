namespace HospitalWebsiteApi.Models
{
    public class AppointmentResponse
    {
        public int AppointmentId { get; set; }
        public string? UserId { get; set; }
        public string? DoctorId { get; set; }
        public string? Date { get; set; }
        public string? Time { get; set; }
        public string? Comment { get; set; }
        public DoctorInfo? DoctorInfo { get; set; }
    }
}
