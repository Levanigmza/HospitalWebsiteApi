namespace HospitalWebsiteApi.Models
{
    public class DoctorInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Position { get; set; }
    }

    public class DoctorRegistrationRequest
    {
        public UserParameters Doctor { get; set; }
        public string Position { get; set; }
    }


}
