using System.ComponentModel.DataAnnotations;

namespace DoctorPatientsManagementApi.Models
{
    public class Doctor
    {
        [Key]
        public int doctorId { get; set; }
        public string doctorName { get; set; }
        public string specialization { get; set; }
        public bool doctorStatus { get; set; }
        public int age { get; set; }
        public string imageData { get; set; }
        public string location { get; set; }
    }
}
