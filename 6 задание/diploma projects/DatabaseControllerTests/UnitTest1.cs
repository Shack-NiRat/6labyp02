using Microsoft.VisualStudio.TestTools.UnitTesting;
using diploma_projects;
using System;

namespace DatabaseControllerTests
{
    [TestClass]
    public class TestDatabase
    {
        private string sqlstr = "Server=.\\SQLEXPRESS;Initial Catalog =НоябрьУпКозлов ; Trusted_Connection=True;Encrypt = false ";
        private Database database;

        public TestDatabase()
        {
            database = Database.GetInstance(sqlstr);
        }


        [TestMethod]
        public void TestExecuteSqlScript()
        {
            object result = database.GetScalar("SELECT 322");
            Assert.AreEqual(result.ToString(), "322");
        }

        [TestMethod]
        public void ExecuteCommandWithParameters()
        {
            string query = "INSERT INTO Autorisation (login, password, role) VALUES (@login, @password, @role)";
            Parameter[] par = 
             {
                new Parameter("@login", "user1"),
                new Parameter("@password", "12345"),
                new Parameter("@role", "Student")
             };
            int rows = database.Execute(query, par);
            Assert.AreEqual(rows, 1);
        }

        [TestMethod]
        public void UpdateAutorisation()
        {
            string query = "update Autorisation set role = \'test\' where login = @login and password = @password";
            Parameter[] par =
             {
                new Parameter("@login", "user1"),
                new Parameter("@password", "12345"),
             };
            int rows = database.Execute(query, par);
            Assert.AreEqual(rows, 0);
        }

        [TestMethod]
        public void GetScalarFromAutorisation()
        {
            string query = "SELECT COUNT(*) FROM Autorisation";
            object result = database.GetScalar(query);
            Assert.IsNotNull(
            result,
            "Не удалось получить число записей."
            );
        }

        [TestMethod]
        public void RemoveUser()
        {
            string query = "Delete from Autorisation where login = @login and password = @pas ";
            Parameter[] par =
            {
                new Parameter("@login", "user1"),
                new Parameter("@pas","12345")
            };
            int rows = database.Execute(query, par);
            Assert.AreEqual(rows, 1);
        }

        [TestMethod]
        public void GetScalarFromMarks()
        {
            string query = "SELECT COUNT(*) FROM Отметки";
            object result = database.GetScalar(query);
            Assert.IsNotNull(
            result,
            "Не удалось получить число записей."
            );
        }

        [TestMethod]
        public void GetScalarFromHistory()
        {
            string query = "SELECT COUNT(*) FROM History";
            object result = database.GetScalar(query);
            Assert.IsNotNull(
            result,
            "Не удалось получить число записей."
            );
        }

        [TestMethod]
        public void GetNoFromHistoryWhithParameters()
        {
            string query = "Select Count(*) from History where login = \'Makoka\'";
            object result = database.GetScalar(query);
            Assert.IsNotNull(
            result,
            "Не удалось получить число записей."
            );
        }

        [TestMethod]
        public void SelectRowsFromAutorisation()
        {
            string query = "SELECT login, password, role FROM Autorisation";
            object[][] result = database.GetRowsData(query);
            CollectionAssert.AllItemsAreNotNull(
            result,
            "Не удалось получить записи из таблицы."
            );
        }

        [TestMethod]
        [ExpectedException (typeof(AssertFailedException))]
        public void SelectNoRowsFromAutorisation()
        {
            string query = "SELECT loginn, password, role FROM Autorisation";
            object[][] result = database.GetRowsData(query);
            CollectionAssert.AllItemsAreNotNull(
            result,
            "Не удалось получить записи из таблицы."
            );
        }






    }
}
