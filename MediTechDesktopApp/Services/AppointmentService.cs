// File: Services\AppointmentService.cs
using System;
using System.Collections.Generic;
using System.Data;
using MediTechDesktopApp.DataAccess;
using MediTechDesktopApp.Models;

namespace MediTechDesktopApp.Services
{
    /// <summary>
    /// Service class for managing appointment CRUD operations.
    /// </summary>
    public class AppointmentService
    {
        private readonly MySqlDbHelper _db;

        public AppointmentService()
        {
            _db = new MySqlDbHelper();
        }

        public List<Appointment> GetAllAppointments()
        {
            var table = _db.ExecuteStoredProcedure("sp_GetAppointments");
            var list = new List<Appointment>();

            foreach (DataRow row in table.Rows)
            {
                list.Add(new Appointment
                {
                    AppointmentId = row["appointment_id"] != DBNull.Value ? Convert.ToInt32(row["appointment_id"]) : 0,
                    PatientId = row["patient_id"] != DBNull.Value ? Convert.ToInt32(row["patient_id"]) : 0,
                    DoctorId = row["doctor_id"] != DBNull.Value ? Convert.ToInt32(row["doctor_id"]) : 0,
                    AppointmentDate = row["appointment_date"] != DBNull.Value ? Convert.ToDateTime(row["appointment_date"]) : DateTime.MinValue,
                    Status = row["status"]?.ToString(),
                    Notes = row["notes"]?.ToString()
                });
            }

            return list;
        }

        public void AddAppointment(Appointment appt)
        {
            var parameters = new Dictionary<string, object>
            {
                { "p_patient_id", appt.PatientId },
                { "p_doctor_id",  appt.DoctorId  },
                { "p_date",       appt.AppointmentDate },
                { "p_status",     appt.Status    },
                { "p_notes",      appt.Notes     }
            };

            _db.ExecuteNonQuery("sp_AddAppointment", parameters);
        }

        public void UpdateAppointmentStatus(int appointmentId, string newStatus)
        {
            var parameters = new Dictionary<string, object>
            {
                { "p_appointment_id", appointmentId },
                { "new_status",       newStatus     }
            };

            _db.ExecuteNonQuery("sp_UpdateAppointmentStatus", parameters);
        }

        public void DeleteAppointment(int appointmentId)
        {
            var parameters = new Dictionary<string, object>
            {
                { "p_appointment_id", appointmentId }
            };

            _db.ExecuteNonQuery("sp_DeleteAppointment", parameters);
        }
    }
}
