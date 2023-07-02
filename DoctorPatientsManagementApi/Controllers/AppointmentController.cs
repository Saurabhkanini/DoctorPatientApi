using DoctorPatientsManagementApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoctorPatientsManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly DoctorsDbContext mdbc;
        public AppointmentController(DoctorsDbContext tdbc)
        {
            this.mdbc = tdbc;
        }
        [HttpGet]
        [Authorize(Roles = "admin")]
        [AllowAnonymous]
        public async Task<ActionResult> GetAllAppointment()
        {
            if (!User.Identity.IsAuthenticated || !User.IsInRole("admin"))
            {
                return Unauthorized("You are Not authorized");
            }
            var appointment = await mdbc.appointments.ToListAsync();
            return Ok(appointment);
        }
        [HttpPost]
        public async Task<ActionResult> AddAppointment(Appointment t)
        {
            await mdbc.appointments.AddAsync(t);
            await mdbc.SaveChangesAsync();
            return Ok(t);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAppointment(Appointment t, int id)
        {
            var appointment = await mdbc.appointments.FindAsync(id);
            if (appointment == null)
            {
                return BadRequest($"Appointment Not find With id = {id}");
            }
            appointment.title = t.title;
            appointment.patientName = t.patientName;
            appointment.date = t.date;
            appointment.isuue = t.isuue;


            mdbc.appointments.Update(appointment);
            mdbc.SaveChanges();
            return Ok(appointment);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAppointment(int id)
        {
            var appointment = await mdbc.appointments.FindAsync(id);
            if (appointment == null)
            {
                return NotFound($"Appointment Not Found with id = {id}");
            }
            mdbc.appointments.Remove(appointment);
            await mdbc.SaveChangesAsync();
            return Ok(appointment);
        }
      
    }
}
