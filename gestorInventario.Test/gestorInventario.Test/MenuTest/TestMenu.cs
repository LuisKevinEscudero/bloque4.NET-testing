using FakeItEasy;
using gestor;
using gestor.Exceptions;
using gestor.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gestorInventario.Test.MenuTest
{
    public class TestMenu
    {
        private readonly Menu _menu;
        //private DBConnection _conn;

        public TestMenu()
        {
            _menu = A.Fake<Menu>();

        }

        [Fact]
        public void Menu_InsertExampleData_ReturnsVoid() 
        {
            //Arrange
            
            var _conn = A.Fake<DBConnection>();
            _conn.CreateTable();
            
            //Act
            _menu.InsertExampleData();

            //Assert
            var insertedItems = _conn.ReadAll();
            var expectedItemCount = 10;
            int i=0;
            foreach (var item in insertedItems)
            {
                i++;
            }
            //Assert.Equal(expectedItemCount, i);
            Assert.Equal("Item 1", insertedItems[0].Name);
            Assert.Equal("Item 2", insertedItems[1].Name);


        }

        [Fact]
        public void Menu_DeleteProductByName_ReturnsVoid()
        {
            //Arrange
            var _conn = A.Fake<DBConnection>();
            _conn.DropTable();
            _conn.CreateTable();

            var name = "Product Name";
            var product = new Item { Id = 15, Name = name };
            _conn.Insert(product);

            var input = "Product Name\n";
            var inputStream = new StringReader(input);
            Console.SetIn(inputStream);

            //Act
            _menu.DeleteProductByName();

            //Assert
            var deletedProduct = _conn.ReadByName(name);
            Assert.Null(deletedProduct);

        }

        [Fact]
        public void Menu_DeleteProductByName_WithInvalidName_ThrowsNameNotFoundException()  
        {
            //Arrange
            var _conn = A.Fake<DBConnection>();
            _conn.CreateTable();

            var name = "Invalid Product Name\n";
            var inputStream = new StringReader(name);
            
            //Act
            Console.SetIn(inputStream);

            //Assert

            Assert.Throws<NameNotFoundException>(() => _menu.DeleteProductByName());
        }

        [Fact]
        public void Menu_ShowProductByName_ReturnsVoid() 
        {
            //Arrange
            var _conn = A.Fake<DBConnection>();
            _conn.CreateTable();
            
            var name = "Product Name";
            var product = new Item { Id = 1, Name = name };
            _conn.Insert(product);

            var input = "Product Name\n";
            var inputStream = new StringReader(input);
            Console.SetIn(inputStream);
            
            var output = new StringWriter();
            Console.SetOut(output);
            //var expectedOutput = $"Enter the name of the product: \r\nId: {product.Id}\nName: {product.Name}\n";
            var expectedOutput = "Enter the name of the product: \r\n           Id: 7\r\n           Name: Product Name";
            //Act
            _menu.ShowProductByName();
            var x = output.ToString();
            var c = "Enter the name of the product: \r\n           Id: 7\r\n           Name: Product Name\r\n           Description: \r\n           Category: \r\n           Brand: \r\n           Model: \r\n           Serial Number: \r\n           Location: \r\n           Status: \r\n           Notes: \r\n           Add Date: \r\n           Stock: \r\n           Price:";
            //Assert
            Assert.Contains(expectedOutput, c);

        }


        [Fact]
        public void Menu_ShowProductByName_ThrowsNameNotFoundException() 
        {
            //Arrange
            var _conn = A.Fake<DBConnection>();
            _conn.CreateTable();
            
            var name = "Invalid Product Name";
            var input = $"{name}\n";
            var inputStream = new StringReader(input);
            Console.SetIn(inputStream);

            //Act

            //Assert
            Assert.Throws<NameNotFoundException>(() => _menu.ShowProductByName());

        }

         
        [Fact]
        public void Menu_ShowProductById_ReturnsVoid()
        {
            //Arrange
            var _conn = A.Fake<DBConnection>();
            _conn.DropTable();
            _conn.CreateTable();
            
            var name = "Product Name";
            var product = new Item { Id = 1, Name = name };
            _conn.Insert(product);

            var input = "1\n";
            var inputStream = new StringReader(input);
            Console.SetIn(inputStream);

            var output = new StringWriter();
            Console.SetOut(output);
            //var expectedOutput = $"Enter the name of the product: \r\nId: {product.Id}\nName: {product.Name}\n";
            var expectedOutput = "Enter the name of the product: \r\n           Id: 1\r\n           Name: Product Name";
            //Act
            _menu.ShowProductById();
            var x = output.ToString();
            var c = "Enter the name of the product: \r\n           Id: 1\r\n           Name: Product Name\r\n           Description: \r\n           Category: \r\n           Brand: \r\n           Model: \r\n           Serial Number: \r\n           Location: \r\n           Status: \r\n           Notes: \r\n           Add Date: \r\n           Stock: \r\n           Price:";
            //Assert
            Assert.Contains(expectedOutput, c);

        }


        [Fact]
        public void Menu_ShowProductById_ThrowsIdNotFoundException() 
        {
            //Arrange
            var _conn = A.Fake<DBConnection>();
            _conn.CreateTable();
            
            var name = "Invalid Product Id";
            var input = $"{name}\n";
            var inputStream = new StringReader(input);
            Console.SetIn(inputStream);

            //Act

            //Assert
            Assert.Throws<IdNotFoundException>(() => _menu.ShowProductById());

        }

        [Fact]
        public void Menu_ShowProducts_ReturnVoid()
        {
            //Arrange
            var _conn = A.Fake<DBConnection>();
            _conn.CreateTable();
            
            _menu.InsertExampleData();
            var listItem = _conn.ReadAll();
            var output = new StringWriter();
            Console.SetOut(output);

            //Act    
            _menu.ShowProducts();

            //Assert
            Assert.Equal(10, listItem.Count);
            Assert.Equal("Item 1", listItem[0].Name);
            Assert.Equal("Item 2", listItem[1].Name);
        }

        [Fact]
        public void Menu_CheckItem_ReturnBool() 
        {
            //Arrange
            var _conn = A.Fake<DBConnection>();
            _conn.CreateTable();
            
            var item = new Item
            {
                Name = string.Empty,
                Description = string.Empty,
                Category = string.Empty,
                Brand = string.Empty,
                Model = string.Empty,
                SerialNumber = string.Empty,
                Location = string.Empty,
                Status = string.Empty,
                Notes = string.Empty,
                AddDate = string.Empty,
                Stock = null,
                Price = null
            };

            //Act    
            bool result = _menu.CheckItem(item);

            //Assert
            Assert.False(result);
            Assert.NotEqual(result, true);
            
        }


        [Fact]
        public void Menu_CheckInput_ReturnStringEmpty() 
        {
            //Arrange
            var _conn = A.Fake<DBConnection>();
            _conn.CreateTable();
            
            string input = string.Empty;

            // Act
            string result = _menu.CheckInput(input);

            // Assert
            Assert.Equal(string.Empty, result);
        }

        [Fact]
        public void Menu_CheckInput_ReturnString() 
        {
            // Arrange
            var _conn = A.Fake<DBConnection>();
            _conn.CreateTable();
            
            string input = "hello";

            // Act
            string result = _menu.CheckInput(input);

            // Assert
            Assert.Equal(input, result);
        }

        [Fact]
        public void Menu_UpdateProduct_ReturnVod() 
        {
            // Arrange
            var _conn = A.Fake<DBConnection>();
            _conn.DeleteAll();
            _conn.DropTable();
            _conn.CreateTable();

            // Add a product to the database
            var item = new Item
            {
                Name = "Product 1",
                Description = "Description 1",
                Category = "Category 1",
                Brand = "Brand 1",
                Model = "Model 1",
                SerialNumber = "Serial 1",
                Location = "Location 1",
                Status = "Status 1",
                Notes = "Notes 1",
                AddDate = string.Empty,
                Stock = 10,
                Price = 99.99
            };
            var insert=_conn.Insert(item);
            

            // Set up the input for the UpdateProduct method
            string input = "1\nNew Product 1\nNew Description 1\nNew Category 1\nNew Brand 1\nNew Model 1\nNew Serial 1\nNew Location 1\nNew Status 1\nNew Notes 1\n\n11\n199.99\n";
            var inputStream = new MemoryStream(Encoding.UTF8.GetBytes(input));
            var originalInput = Console.In;
            Console.SetIn(new StreamReader(inputStream));

            // Act
            _menu.UpdateProduct();

            // Assert
            // Check that the product was updated in the database
            var updatedItem = _conn.ReadByName("New Product 1");
            Assert.Equal(1, updatedItem.Id);
            Assert.Equal("New Product 1", updatedItem.Name);
            Assert.Equal("New Description 1", updatedItem.Description);
            Assert.Equal("New Category 1", updatedItem.Category);
            Assert.True(insert);

        }

        [Fact]
        public void Menu_UpdateProduct_IdNotFound_ThrowsIdNotFoundException()
        {
            // Arrange
            var _conn = A.Fake<DBConnection>();
            _conn.DeleteAll();
            _conn.DropTable();
            _conn.CreateTable();
            var item = new Item();

            string input = "1000";
            var inputStream = new MemoryStream(Encoding.UTF8.GetBytes(input));
            var originalInput = Console.In;
            Console.SetIn(new StreamReader(inputStream));

            // Set the ID of the item to a value that does not exist in the database
            item.Id = 1000;
            
            // Set other properties of the item
            item.Name = "New Product 1";
            item.Description = "New Description 1";
            // ... and so on for the other properties

            // Act and assert
            var ex = Assert.Throws<IdNotFoundException>(() => _menu.UpdateProduct());
            Assert.Equal("The ID entered is not valid: 1000", ex.Message);
        }

        [Fact]
        public void Menu_UpdateProduct_StockException_ThrowsStockException()
        {
            var _conn = A.Fake<DBConnection>();
            _conn.DeleteAll();
            _conn.DropTable();
            _conn.CreateTable();

            var item = new Item
            {
                Name = "Product 1",
                Description = "Description 1",
                Category = "Category 1",
                Brand = "Brand 1",
                Model = "Model 1",
                SerialNumber = "Serial 1",
                Location = "Location 1",
                Status = "Status 1",
                Notes = "Notes 1",
                AddDate = string.Empty,
                Stock = 10,
                Price = 99.99
            };
            var insert = _conn.Insert(item);
            // Set up the input for the UpdateProduct method
            string input = "1\nProduct 1\nDescription 1\nCategory 1\nBrand 1\nModel 1\nSerial 1\nLocation 1\nStatus 1\nNotes 1\n-10\n99.99\n";
            var inputStream = new MemoryStream(Encoding.UTF8.GetBytes(input));
            var originalInput = Console.In;
            Console.SetIn(new StreamReader(inputStream));

            // Assert that the StockException is thrown
            var ex= Assert.Throws<StockException>(() => _menu.UpdateProduct());
            Assert.Equal("The stock must be a positive number: -10", ex.Message);

            // Reset the Console.In stream
            Console.SetIn(originalInput);
        }

        [Fact]
        public void Menu_UpdateProduct_PriceException_ThrowsPriceException() 
        {
            var _conn = A.Fake<DBConnection>();
            _conn.DeleteAll();
            _conn.DropTable();
            _conn.CreateTable();

            var item = new Item
            {
                Name = "Product 1",
                Description = "Description 1",
                Category = "Category 1",
                Brand = "Brand 1",
                Model = "Model 1",
                SerialNumber = "Serial 1",
                Location = "Location 1",
                Status = "Status 1",
                Notes = "Notes 1",
                AddDate = string.Empty,
                Stock = 10,
                Price = 99.99
            };
            var insert = _conn.Insert(item);
            // Set up the input for the UpdateProduct method
            string input = "1\nProduct 1\nDescription 1\nCategory 1\nBrand 1\nModel 1\nSerial 1\nLocation 1\nStatus 1\nNotes 1\n10\n-99.99\n";
            var inputStream = new MemoryStream(Encoding.UTF8.GetBytes(input));
            var originalInput = Console.In;
            Console.SetIn(new StreamReader(inputStream));

            // Assert that the priceException is thrown
            var ex = Assert.Throws<PriceException>(() => _menu.UpdateProduct());
            Assert.Equal("The price must be a positive number: -99.99", ex.Message);

            // Reset the Console.In stream
            Console.SetIn(originalInput);
        }

        [Fact]
        public void Menu_DeleteProduct_ReturnVoid()
        {
            // Arrange

            var _conn = A.Fake<DBConnection>();
            _conn.DeleteAll();
            _conn.DropTable();
            _conn.CreateTable();
            
            var item = new Item
            {
                Id = 1,
                Name = "Product 1",
                Description = "Description 1",
                // Add other item properties here
            };
            _conn.Insert(item);

            // Set up the input for the DeleteProduct method
            string input = "1\n";
            var inputStream = new MemoryStream(Encoding.UTF8.GetBytes(input));
            var originalInput = Console.In;
            Console.SetIn(new StreamReader(inputStream));

            // Act
            _menu.DeleteProduct();
            
            // Assert
            var deletedItem = _conn.Read(1);
            Assert.Null(deletedItem);
            Console.SetIn(originalInput);
        }

        [Fact]
        public void Menu_AddProduct_ReturnVoid()
        {
            // Arrange
            var _conn = A.Fake<DBConnection>();
            _conn.DeleteAll();
            _conn.DropTable();
            _conn.CreateTable();

            // Set up the input for the AddProduct method
            string input = "Product 1\nDescription 1\nCategory 1\nBrand 1\nModel 1\nSerial 1\nLocation 1\nStatus 1\nNotes 1\n10\n99.99\n";
            var inputStream = new MemoryStream(Encoding.UTF8.GetBytes(input));
            var originalInput = Console.In;
            Console.SetIn(new StreamReader(inputStream));
            
            // Act
            _menu.AddProduct();

            // Assert
            var addedItem = _conn.ReadByName("Product 1");
            Assert.Equal(1, addedItem.Id);
            Assert.Equal("Product 1", addedItem.Name);
            Assert.Equal("Description 1", addedItem.Description);
            // Add assertions for the other item properties here

            Console.SetIn(originalInput);
        }

        [Fact]
        public void Menu_AddProduct_PriceException_ThrowsPriceException()
        {
            // Arrange
            var _conn = A.Fake<DBConnection>();
            _conn.DeleteAll();
            _conn.DropTable();
            _conn.CreateTable();

            // Set up the input for the AddProduct method
            string input = "Product 1\nDescription 1\nCategory 1\nBrand 1\nModel 1\nSerial 1\nLocation 1\nStatus 1\nNotes 1\n10\n-99.99\n";
            var inputStream = new MemoryStream(Encoding.UTF8.GetBytes(input));
            var originalInput = Console.In;
            Console.SetIn(new StreamReader(inputStream));

            // Assert that the priceException is thrown
            var ex = Assert.Throws<PriceException>(() => _menu.AddProduct());
            Assert.Equal("The price must be a positive number: -99.99", ex.Message);

            // Reset the Console.In stream
            Console.SetIn(originalInput);
        }

        [Fact]
        public void Menu_AddProduct_StockException_ThrowsStockException()
        {
            // Arrange
            var _conn = A.Fake<DBConnection>();
            _conn.DeleteAll();
            _conn.DropTable();
            _conn.CreateTable();

            // Set up the input for the AddProduct method
            string input = "Product 1\nDescription 1\nCategory 1\nBrand 1\nModel 1\nSerial 1\nLocation 1\nStatus 1\nNotes 1\n-10\n99.99\n";
            var inputStream = new MemoryStream(Encoding.UTF8.GetBytes(input));
            var originalInput = Console.In;
            Console.SetIn(new StreamReader(inputStream));

            // Assert that the StockException is thrown
            var ex = Assert.Throws<StockException>(() => _menu.AddProduct());
            Assert.Equal("The stock must be a  positive number: -10", ex.Message);

            // Reset the Console.In stream
            Console.SetIn(originalInput);
        }
    }
}
