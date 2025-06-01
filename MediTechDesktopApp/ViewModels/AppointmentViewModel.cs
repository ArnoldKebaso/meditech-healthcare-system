// File: ViewModels\AppointmentViewModel.cs
using System;
using System.Collections.ObjectModel;
using System.Linq;
using MediTechDesktopApp.Models;
using MediTechDesktopApp.Services;

namespace MediTechDesktopApp.ViewModels
{
    /// <summary>
    /// ViewModel for Appointments: exposes an ObservableCollection<Appointment> for binding,
    /// and methods to Add/UpdateStatus/Delete appointments.
    /// </summary>
    public class AppointmentViewModel
    {
        private readonly AppointmentService _service;

        public ObservableCollection<Appointment> Appointments { get; private set; }
            = new ObservableCollection<Appointment>();

        public Appointment SelectedAppointment { get; set; } = new Appointment();

        public AppointmentViewModel()
        {
            _service = new AppointmentService();
            LoadAppointments();
        }

        public void LoadAppointments()
        {
            Appointments.Clear();
            var all = _service.GetAllAppointments();
            foreach (var a in all)
            {
                Appointments.Add(a);
            }
        }

        public void AddAppointment(Appointment newAppt)
        {
            if (newAppt == null) return;
            _service.AddAppointment(newAppt);
            LoadAppointments();
        }

        public void UpdateAppointmentStatus(int appointmentId, string newStatus)
        {
            if (appointmentId == 0 || string.IsNullOrWhiteSpace(newStatus)) return;
            _service.UpdateAppointmentStatus(appointmentId, newStatus);
            LoadAppointments();
        }

        public void DeleteAppointment(int appointmentId)
        {
            if (appointmentId == 0) return;
            _service.DeleteAppointment(appointmentId);
            LoadAppointments();
        }

        public string GetAppointmentSummaries()
        {
            return string.Join("\n", Appointments.Select(a => a.DisplayInfo));
        }
    }
}
