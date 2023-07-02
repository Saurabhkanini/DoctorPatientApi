using System.ComponentModel.DataAnnotations;

namespace DoctorPatientsManagementApi.Models
{
    public class Appointment
    {
        [Key]
        public int id { get; set; }
        public string? title { get; set; }   
        public string? patientName { get; set; }
        public  DateTime? date { get; set; }
        public string? isuue { get; set; }   

    }
}
