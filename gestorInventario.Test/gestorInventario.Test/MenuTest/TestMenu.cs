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
        private readonly Menu _menu;
        private readonly DBConnection _conn;

        public TestMenu()
        {
            _menu = A.Fake<Menu>();
            _conn = A.Fake<DBConnection>();
        }

        [Fact]
        public void Menu_InsertExampleData_ReturnsVoid() 
        {
            //Arrange

            //Act
            _conn.DropTable();
            _conn.CreateTable();
            _menu.InsertExampleData();

            //Assert
            var insertedItems = _conn.ReadAll();
            var expectedItemCount = 10;
            foreach (var item in insertedItems)
            {
                Console.WriteLine(item.Name);
            }
            Assert.Equal(expectedItemCount, insertedItems.Count());
            Assert.Equal("Item 1", insertedItems[0].Name);
            Assert.Equal("Item 2", insertedItems[1].Name);
        }
    }
}
