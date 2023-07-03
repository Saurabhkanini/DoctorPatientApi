using DoctorPatientsManagementApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace DoctorPatientsManagementApi.Repository
{
    public interface IPatient
    {
         IEnumerable<Patient> GetAllPatients();
        Patient AddPatient(Patient t);
        Patient UpdatePatient(Patient t, int id);
        Patient DeletePatient(int id);
       Patient GetPatient(int id);
    }
}
