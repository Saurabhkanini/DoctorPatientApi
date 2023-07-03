using DoctorPatientsManagementApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoctorPatientsManagementApi.Repository
{
    public class PatientInterFaceImplementation : IPatient
    {
        private DoctorsDbContext mdbc;
        public PatientInterFaceImplementation(DoctorsDbContext d)
        {
            mdbc = d;
        }

        public IEnumerable<Patient> GetAllPatients()
        {

            var patients = mdbc.Patients.ToList();
            return patients;
        }
        public Patient AddPatient(Patient t)
        {
            mdbc.Patients.Add(t);
            mdbc.SaveChanges();
            return (t);
        }
        public Patient UpdatePatient(Patient t, int id)
        {
            var patient = mdbc.Patients.Find(id);

            patient.pname = t.pname;
            patient.plocation = t.plocation;
            patient.pissue = t.pissue;

            mdbc.Patients.Update(patient);
            mdbc.SaveChanges();
            return (patient);
        }
        public Patient DeletePatient(int id)
        {
            var patient = mdbc.Patients.Find(id);

            mdbc.Patients.Remove(patient);
            mdbc.SaveChanges();
            return (patient);
        }
        public Patient GetPatient(int id)
        {
            var patient = mdbc.Patients.Find(id);

            return (patient);

        }
    }
}
