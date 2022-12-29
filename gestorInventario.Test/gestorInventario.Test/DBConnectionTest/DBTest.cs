using FakeItEasy;
using FluentAssertions;
using gestor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gestorInventario.Test.DBConnectionTest
{
    public class DBTest
    {
        private readonly DBConnection _conn;
        private readonly String _dbName;

        public DBTest()
        {
            _conn = A.Fake<DBConnection>();
            _dbName = "database.db";
        }
        
        [Fact]
        public void DBConnection_CreateTable_ReturnsVoid()
        {
            //Arrange
            

            //Act
            _conn.CreateTable();

            //Assert
            Assert.True(File.Exists(_dbName));
        }

        [Fact]
        public void DBConnection_DropTable_ReturnsBool()
        {
            //Arrange

            //Act
            var x= _conn.DropTable();

            //Assert
            x.Should().BeTrue();
            x.Should().NotBe(false);
            x.Should().Be(true);
        }
    }
}
