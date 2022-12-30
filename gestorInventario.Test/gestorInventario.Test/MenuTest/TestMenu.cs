using FakeItEasy;
using gestor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gestorInventario.Test.MenuTest
{
    public class TestMenu
    {
        private readonly DBConnection _conn;
        private readonly String _dbName;
        private readonly Console consol;
        public TestMenu()
        {
            _conn = A.Fake<DBConnection>();
            _dbName = "database.db";
        }

        [Fact]
        public void Menu_Start_ReturnsVoid()
        {
            Console console = new A().Fake<Console>();
        }
    }
}
