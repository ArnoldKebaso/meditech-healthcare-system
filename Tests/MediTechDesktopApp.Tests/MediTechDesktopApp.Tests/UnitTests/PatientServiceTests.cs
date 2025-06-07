using MediTechDesktopApp.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using System.Reflection;

namespace MediTechDesktopApp.Tests.UnitTests
{
    [TestClass]
    public class PatientServiceTests
    {
        private PatientService _service;

        [TestInitialize]
        public void Setup()
        {
            // If your PatientService can accept a connection string override,
            // you could point it at a test database here.
            _service = new PatientService();
        }

        [TestMethod]
        public void GetAllPatients_ReturnsAtLeastOnePatient_WithExpectedFields()
        {
            var patients = _service.GetAllPatients();
            Assert.IsTrue(patients.Any(), "Expected at least one patient record");

            var first = patients.First();
            Assert.IsTrue(!string.IsNullOrEmpty(first.FullName));
            Assert.IsTrue(first.PatientId > 0);
        }

        [TestMethod]
        public void AddPatient_ThenGetAllPatients_IncludesNewPatient()
        {
            var newPatient = new Models.Patient
            {
                FirstName = "Unit",
                LastName = "Tester",
                DateOfBirth = new System.DateTime(2000, 1, 1),
                Gender = "Other",
                Phone = "000-0000",
                Email = "unit@test.com",
                Address = "Test Address"
            };

            // Add and then fetch again
            _service.AddPatient(newPatient);
            var all = _service.GetAllPatients();

            Assert.IsTrue(all.Any(p => p.Email == "unit@test.com"),
                "Newly added patient should appear in GetAllPatients()");
        }
    }
}
