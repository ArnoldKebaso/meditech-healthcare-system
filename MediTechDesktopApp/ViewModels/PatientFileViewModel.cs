// File: ViewModels\PatientFileViewModel.cs
using MediTechDesktopApp.Models;
using MediTechDesktopApp.Services;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;

namespace MediTechDesktopApp.ViewModels
{
    /// <summary>
    /// VM to manage PatientFile list and upload/delete actions.
    /// </summary>
    public class PatientFileViewModel
    {
        private readonly PatientFileService _service;

        public ObservableCollection<PatientFile> Files { get; private set; }
            = new ObservableCollection<PatientFile>();

        public PatientFile SelectedFile { get; set; } = new PatientFile();

        public PatientFileViewModel()
        {
            _service = new PatientFileService();
            LoadFiles();
        }

        public void LoadFiles()
        {
            Files.Clear();
            var all = _service.GetAllFiles();
            foreach (var pf in all)
                Files.Add(pf);
        }

        public void AddFile(int patientId, string filepath)
        {
            if (patientId <= 0)
                throw new ArgumentException("Patient ID must be > 0");

            _service.AddPatientFile(patientId, filepath);
            LoadFiles();
        }

        public void DeleteFile(int fileId)
        {
            _service.DeletePatientFile(fileId);
            LoadFiles();
        }
    }
}
