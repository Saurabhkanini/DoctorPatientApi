using DoctorPatientsManagementApi.Models;
using DoctorPatientsManagementApi.Repository;
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
        private readonly IPatient mdbc;
        public PatientController(IPatient tdbc)
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
            var patients = mdbc.GetAllPatients();
            return Ok(patients);
        }
        [HttpPost]
        public async Task<ActionResult> AddPatient(Patient t)
        {
            mdbc.AddPatient(t);
            return Ok(t);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePatient(Patient t, int id)
        {
            var patient =  mdbc.UpdatePatient(t, id);
            if (patient == null)
            {
                return BadRequest($"Patient Not find With id = {id}");
            }
            
            return Ok(patient);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePatient(int id)
        {
            var patient =  mdbc.DeletePatient(id);
            if (patient == null)
            {
                return NotFound($"Patient Not Found with id = {id}");
            }
           
            return Ok(patient);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetPatient(int id)
        {
            var patient =  mdbc.GetPatient(id);
            if (patient == null)
            {
                return NotFound($"Patient Not Found with id = {id}");
            }
            return Ok(patient);

        }
    }
}
