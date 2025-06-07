using Microsoft.VisualStudio.TestTools.UnitTesting;
using MediTechDesktopApp.DataAccess;
using System.Data;
using System.Linq;

namespace MediTechDesktopApp.Tests.IntegrationTests
{
    [TestClass]
    public class DatabaseIntegrationTests
    {
        private MySqlDbHelper _db;

        [TestInitialize]
        public void Init()
        {
            _db = new MySqlDbHelper();
        }

        [TestMethod]
        public void ExecuteRawQuery_ReturnsDataTable_ForKnownTable()
        {
            // Using small lookup table, e.g. Departments
            DataTable dt = _db.ExecuteRawQuery("SELECT * FROM departments LIMIT 1;");
            Assert.IsFalse(dt.Rows.Count == 0, "Expected at least one department row");
            Assert.IsTrue(dt.Columns.Contains("department_id"));
        }
    }
}
