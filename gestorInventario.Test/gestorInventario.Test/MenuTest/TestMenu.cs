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
        private readonly DBConnection _conn;

        public TestMenu()
        {
            _menu = A.Fake<Menu>();
            _conn = A.Fake<DBConnection>();

            _conn.CreateTable();
        }

        [Fact]
        public void Menu_InsertExampleData_ReturnsVoid() 
        {
            //Arrange

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
            var name = "Product Name";
            var product = new Item { Id = 1, Name = name };
            _conn.Insert(product);

            var input = "7\n";
            var inputStream = new StringReader(input);
            Console.SetIn(inputStream);

            var output = new StringWriter();
            Console.SetOut(output);
            //var expectedOutput = $"Enter the name of the product: \r\nId: {product.Id}\nName: {product.Name}\n";
            var expectedOutput = "Enter the name of the product: \r\n           Id: 7\r\n           Name: Product Name";
            //Act
            _menu.ShowProductById();
            var x = output.ToString();
            var c = "Enter the name of the product: \r\n           Id: 7\r\n           Name: Product Name\r\n           Description: \r\n           Category: \r\n           Brand: \r\n           Model: \r\n           Serial Number: \r\n           Location: \r\n           Status: \r\n           Notes: \r\n           Add Date: \r\n           Stock: \r\n           Price:";
            //Assert
            Assert.Contains(expectedOutput, c);

        }


        [Fact]
        public void Menu_ShowProductById_ThrowsIdNotFoundException() 
        {
            //Arrange
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
            var listItem = _conn.ReadAll();

            //Act
            _conn.ShowItems(listItem);

            //Assert


        }

    }
}
