using FakeItEasy;
using FluentAssertions;
using gestor;
using gestor.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static SQLite.SQLite3;

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

        [Fact]
        public void DBConnection_Insert_ReturnsBool()
        {
            //Arrange
            var item = new Item();
            //Act
            _conn.CreateTable();
            var result = _conn.Insert(item);

            //Assert
            result.Should().BeTrue();
            result.Should().NotBe(false);
            result.Should().Be(true);
        }

        [Fact]
        public void DBConnection_Update_ReturnsBool()
        {
            //Arrange
            var item = new Item();
            //Act
            _conn.CreateTable();
            var result = _conn.Update(item);

            //Assert
            result.Should().BeTrue();
            result.Should().NotBe(false);
            result.Should().Be(true);
        }

        [Fact]
        public void DBConnection_Delete_ReturnsBool()
        {
            //Arrange
            var item = new Item();
            //Act
            _conn.CreateTable();
            var result = _conn.Delete(1);

            //Assert
            result.Should().BeTrue();
            result.Should().NotBe(false);
            result.Should().Be(true);
        }

        [Fact]
        public void DBConnection_GetMaxId_ReturnsInt()
        {
            //Arrange
            var item = new Item();
            
            //Act
            _conn.CreateTable();
            _conn.Insert(item);
            var result = _conn.GetMaxId();
            
            //Assert
            result.Should().BeInRange(1, 5);
            result.Should().NotBe(0);
            //result.Should().Be(2);
        }

        [Fact]
        public void DBConnection_ShowItem_ReturnsVoid()
        {
            //Arrange
            var item = new Item()
            {
                Id = 1,
                Name = "Test",
                Description = "Test",
                Category = "Test",
                Brand = "Test",
                Model = "Test",
                SerialNumber = "Test",
                Location = "Test",
                Status = "Test",
                AddDate = "Test",
                Stock = 1,
                Price = 1
            };
            var output = new StringWriter();
            Console.SetOut(output);

            //Act
            _conn.ShowItem(item);


            //Assert
            output.ToString().Should().Contain("Id: 1");
            output.ToString().Should().Contain("Name: Test");
            output.ToString().Should().Contain("Description: Test");
            output.ToString().Should().Contain("Category: Test");
            output.ToString().Should().Contain("Brand: Test");
            output.ToString().Should().Contain("Model: Test");
            output.ToString().Should().Contain("Serial Number: Test");
            output.ToString().Should().Contain("Location: Test");
            output.ToString().Should().Contain("Status: Test");
            output.ToString().Should().Contain("Add Date: Test");
            output.ToString().Should().Contain("Stock: 1");
            output.ToString().Should().Contain("Price: 1");
        }

        [Fact]
        public void DBConnection_ShowItems_ReturnsVoid()
        {
            //Arrange
            var item = new Item()
            {
                Id = 1,
                Name = "Test",
                Description = "Test",
                Category = "Test",
                Brand = "Test",
                Model = "Test",
                SerialNumber = "Test",
                Location = "Test",
                Status = "Test",
                AddDate = "Test",
                Stock = 1,
                Price = 1
            };
            var item2 = new Item()
            {
                Id = 2,
                Name = "Test2",
                Description = "Test2",
                Category = "Test2",
                Brand = "Test2",
                Model = "Test2",
                SerialNumber = "Test2",
                Location = "Test2",
                Status = "Test2",
                AddDate = "Test2",
                Stock = 2,
                Price = 2
            };
            var itemList = new List<Item>() { item, item2 };
            
            var output = new StringWriter();
            Console.SetOut(output);

            //Act
            _conn.ShowItems(itemList);


            //Assert
            output.ToString().Should().Contain("Id: 1");
            output.ToString().Should().Contain("Name: Test");
            output.ToString().Should().Contain("Description: Test");
            output.ToString().Should().Contain("Category: Test");
            output.ToString().Should().Contain("Brand: Test");
            output.ToString().Should().Contain("Model: Test");
            output.ToString().Should().Contain("Serial Number: Test");
            output.ToString().Should().Contain("Location: Test");
            output.ToString().Should().Contain("Status: Test");
            output.ToString().Should().Contain("Add Date: Test");
            output.ToString().Should().Contain("Stock: 1");
            output.ToString().Should().Contain("Price: 1");

            output.ToString().Should().Contain("Id: 2");
            output.ToString().Should().Contain("Name: Test2");
            output.ToString().Should().Contain("Description: Test2");
            output.ToString().Should().Contain("Category: Test2");
            output.ToString().Should().Contain("Brand: Test2");
            output.ToString().Should().Contain("Model: Test2");
            output.ToString().Should().Contain("Serial Number: Test2");
            output.ToString().Should().Contain("Location: Test2");
            output.ToString().Should().Contain("Status: Test2");
            output.ToString().Should().Contain("Add Date: Test2");
            output.ToString().Should().Contain("Stock: 2");
            output.ToString().Should().Contain("Price: 2");
        }

        [Fact]
        public void DBConnection_DeleteAll_ReturnsVoid()
        {
            //Arrange
            _conn.CreateTable();
            var item = new Item { Name = "Test Item" };
            _conn.Insert(item);

            //Act
            _conn.DeleteAll();

            //Assert
            using (var conn = new SQLiteConnection("database.db"))
            {
                int rowCount = conn.ExecuteScalar<int>("SELECT COUNT(*) FROM Item");
                Assert.Equal(0, rowCount);
            }

        }

        [Fact]
        public void DBConnection_generateItems_ReturnsListItem()
        {
            //Arrange


            //Act
            var items = _conn.generateItems();

            //Assert
            items.Should().NotBeEmpty();
            Assert.Equal(10, items.Count);
            Assert.Equal("Item 1", items[0].Name);
            Assert.Equal("Item 2", items[1].Name);
            Assert.Equal("Item 3", items[2].Name);
            Assert.Equal("Item 4", items[3].Name);
            Assert.Equal("Item 5", items[4].Name);
            Assert.Equal("Item 6", items[5].Name);
            Assert.Equal("Item 7", items[6].Name);
            Assert.Equal("Item 8", items[7].Name);
            Assert.Equal("Item 9", items[8].Name);
            Assert.Equal("Item 10", items[9].Name);

            Assert.Equal("Description 1", items[0].Description);
            Assert.Equal("Description 2", items[1].Description);
            Assert.Equal("Description 3", items[2].Description);
            Assert.Equal("Description 4", items[3].Description);
            Assert.Equal("Description 5", items[4].Description);
            Assert.Equal("Description 6", items[5].Description);
            Assert.Equal("Description 7", items[6].Description);
            Assert.Equal("Description 8", items[7].Description);
            Assert.Equal("Description 9", items[8].Description);
            Assert.Equal("Description 10", items[9].Description);

            Assert.Equal("Category 1", items[0].Category);
            Assert.Equal("Category 2", items[1].Category);
            Assert.Equal("Category 3", items[2].Category);
            Assert.Equal("Category 4", items[3].Category);
            Assert.Equal("Category 5", items[4].Category);
            Assert.Equal("Category 6", items[5].Category);
            Assert.Equal("Category 7", items[6].Category);
            Assert.Equal("Category 8", items[7].Category);
            Assert.Equal("Category 9", items[8].Category);
            Assert.Equal("Category 10", items[9].Category);

            Assert.Equal("Brand 1", items[0].Brand);
            Assert.Equal("Brand 2", items[1].Brand);
            Assert.Equal("Brand 3", items[2].Brand);
            Assert.Equal("Brand 4", items[3].Brand);
            Assert.Equal("Brand 5", items[4].Brand);
            Assert.Equal("Brand 6", items[5].Brand);
            Assert.Equal("Brand 7", items[6].Brand);
            Assert.Equal("Brand 8", items[7].Brand);
            Assert.Equal("Brand 9", items[8].Brand);

            Assert.Equal("Model 1", items[0].Model);
            Assert.Equal("Model 2", items[1].Model);
            Assert.Equal("Model 3", items[2].Model);
            Assert.Equal("Model 4", items[3].Model);
            Assert.Equal("Model 5", items[4].Model);
            Assert.Equal("Model 6", items[5].Model);
            Assert.Equal("Model 7", items[6].Model);
            Assert.Equal("Model 8", items[7].Model);
            Assert.Equal("Model 9", items[8].Model);

            Assert.Equal("Serial Number 1", items[0].SerialNumber);
            Assert.Equal("Serial Number 2", items[1].SerialNumber);
            Assert.Equal("Serial Number 3", items[2].SerialNumber);
            Assert.Equal("Serial Number 4", items[3].SerialNumber);
            Assert.Equal("Serial Number 5", items[4].SerialNumber);
            Assert.Equal("Serial Number 6", items[5].SerialNumber);
            Assert.Equal("Serial Number 7", items[6].SerialNumber);
            Assert.Equal("Serial Number 8", items[7].SerialNumber);
            Assert.Equal("Serial Number 9", items[8].SerialNumber);

            Assert.Equal("Location 1", items[0].Location);
            Assert.Equal("Location 2", items[1].Location);
            Assert.Equal("Location 3", items[2].Location);
            Assert.Equal("Location 4", items[3].Location);
            Assert.Equal("Location 5", items[4].Location);
            Assert.Equal("Location 6", items[5].Location);
            Assert.Equal("Location 7", items[6].Location);
            Assert.Equal("Location 8", items[7].Location);
            Assert.Equal("Location 9", items[8].Location);

            Assert.Equal("Status 1", items[0].Status);
            Assert.Equal("Status 2", items[1].Status);
            Assert.Equal("Status 3", items[2].Status);
            Assert.Equal("Status 4", items[3].Status);
            Assert.Equal("Status 5", items[4].Status);
            Assert.Equal("Status 6", items[5].Status);
            Assert.Equal("Status 7", items[6].Status);
            Assert.Equal("Status 8", items[7].Status);
            Assert.Equal("Status 9", items[8].Status);

            Assert.Equal(1, items[0].Price);
            Assert.Equal(2, items[1].Price);
            Assert.Equal(3, items[2].Price);
            Assert.Equal(4, items[3].Price);
            Assert.Equal(5, items[4].Price);
            Assert.Equal(6, items[5].Price);
            Assert.Equal(7, items[6].Price);
            Assert.Equal(8, items[7].Price);
            Assert.Equal(9, items[8].Price);

            Assert.Equal(1, items[0].Stock);
            Assert.Equal(2, items[1].Stock);
            Assert.Equal(3, items[2].Stock);
            Assert.Equal(4, items[3].Stock);
            Assert.Equal(5, items[4].Stock);
            Assert.Equal(6, items[5].Stock);
            Assert.Equal(7, items[6].Stock);
            Assert.Equal(8, items[7].Stock);
            Assert.Equal(9, items[8].Stock);
        }


        [Fact]
        public void DBConnection_Read_ReturnsItem() 
        {
            //Arrange
            _conn.DropTable();
            _conn.CreateTable();
            var item = new Item { Name = "Test Item" }; 
            _conn.Insert(item);

            //Act
            var result = _conn.Read(1);

            //Assert
            Assert.Equal("Test Item", result.Name);

        }

        [Fact]
        public void DBConnection_ReadByName_ReturnsItem()
        {
            //Arrange
            _conn.DropTable();
            _conn.CreateTable();
            var item = new Item { Name = "Test Item" };
            _conn.Insert(item);

            //Act
            var result = _conn.ReadByName("Test Item");

            //Assert
            Assert.Equal("Test Item", result.Name);

        }

        [Fact]
        public void DBConnection_ReadAll_ReturnsListItem() 
        {
            //Arrange
            _conn.DropTable();
            _conn.CreateTable();
            var item1 = new Item { Name = "Item 1" };
            var item2 = new Item { Name = "Item 2" };
            var item3 = new Item { Name = "Item 3" };
            _conn.Insert(item1);
            _conn.Insert(item2);
            _conn.Insert(item3);

            //Act
            var results = _conn.ReadAll();

            //Assert
            Assert.Equal(3, results.Count);
            Assert.Equal("Item 1", results[0].Name);
            Assert.Equal("Item 2", results[1].Name);
            Assert.Equal("Item 3", results[2].Name);

        }

    }
}
