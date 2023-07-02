using System.ComponentModel.DataAnnotations;

namespace DoctorPatientsManagementApi.Models
{
    public class Patient
    {
        [Key]
        public int pid { get; set; }
        public string pname { get; set; }
        public string plocation { get; set; }
        public string pissue { get; set; }

    }
}
