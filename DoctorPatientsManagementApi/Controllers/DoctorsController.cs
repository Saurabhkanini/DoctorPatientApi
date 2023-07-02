using DoctorPatientsManagementApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoctorPatientsManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly DoctorsDbContext mdbc;
        public DoctorsController(DoctorsDbContext tdbc)
        {
            this.mdbc = tdbc;
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        [AllowAnonymous]
        public async Task<ActionResult> GetAllDoctors()
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("admin"))
            {
                return Unauthorized("You are Not authorized");
            }
            var doctors = await mdbc.Doctors.ToListAsync();
            return Ok(doctors);
        }
        [HttpPost]
        public async Task<ActionResult> AddDoctor(Doctor t)
        {
            await mdbc.Doctors.AddAsync(t);
            await mdbc.SaveChangesAsync();
            return Ok(t);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDoctor(Doctor t, int id)
        {
            var doctor = await mdbc.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return BadRequest($"Doctor Not find With id = {id}");
            }
            doctor.doctorName = t.doctorName;
            doctor.specialization = t.specialization;
            doctor.age = t.age;
            doctor.location = t.location;
            doctor.imageData = t.imageData;
            mdbc.Doctors.Update(doctor);
            mdbc.SaveChanges();
            return Ok(doctor);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDoctor(int id)
        {
            var doctor = await mdbc.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound($"Doctor Not Found with id = {id}");
            }
            mdbc.Doctors.Remove(doctor);
            await mdbc.SaveChangesAsync();
            return Ok(doctor);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetDoctor(int id)
        {
            var doctor = await mdbc.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound($"Doctor Not Found with id = {id}");
            }
            return Ok(doctor);

        }
        [Route("api/doctor/change-status/{id}")]
        [HttpPut]
        public async Task<ActionResult> ChangeStatus(int id)
        {
            var doctor = await mdbc.Doctors.FindAsync(id);
            if (doctor == null)
            {
                return NotFound($"Doctor Not Found with id = {id}");

            }
            doctor.doctorStatus = !doctor.doctorStatus;
            mdbc.SaveChanges();
            return Ok(doctor);
        }
        [HttpGet("ActiveDoctors")]
        public async Task<ActionResult> GetActiveDoctors()
        {
            var doctors = mdbc.Doctors.Where(t => t.doctorStatus).ToList();
            if (doctors.Count == 0)
            {
                return NotFound("No Doctor Found With Active Status");
            }
            return Ok(doctors);
        }
    }
}
