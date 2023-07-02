using DoctorPatientsManagementApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoctorPatientsManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly DoctorsDbContext mdbc;
        public PatientController(DoctorsDbContext tdbc)
        {
            this.mdbc = tdbc;
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        [AllowAnonymous]
        public async Task<ActionResult> GetAllPatients()
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("admin"))
            {
                return Unauthorized("You are Not authorized");
            }
            var patients = await mdbc.Patients.ToListAsync();
            return Ok(patients);
        }
        [HttpPost]
        public async Task<ActionResult> AddPatient(Patient t)
        {
            await mdbc.Patients.AddAsync(t);
            await mdbc.SaveChangesAsync();
            return Ok(t);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePatient(Patient t, int id)
        {
            var patient = await mdbc.Patients.FindAsync(id);
            if (patient == null)
            {
                return BadRequest($"Patient Not find With id = {id}");
            }
            patient.pname = t.pname;
            patient.plocation = t.plocation;
            patient.pissue = t.pissue;
           
            mdbc.Patients.Update(patient);
            mdbc.SaveChanges();
            return Ok(patient);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePatient(int id)
        {
            var patient = await mdbc.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound($"Patient Not Found with id = {id}");
            }
            mdbc.Patients.Remove(patient);
            await mdbc.SaveChangesAsync();
            return Ok(patient);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetPatient(int id)
        {
            var patient = await mdbc.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound($"Patient Not Found with id = {id}");
            }
            return Ok(patient);

        }
    }
}
