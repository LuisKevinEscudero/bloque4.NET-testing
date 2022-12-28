using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1.FirstTest.Test
{
    public static class WordlDumbesFunctionTest
    {
        //naming convention: ClassName_MethodName_ExpectResult
        public  static void WorldDumbestFunction_ReturnsPikachuIfZero_ReturnString()
        {
            try
            {
                //Arrange - Get your variables, objects, etc. ready
                int num = 0;
                var worldDumbest = new WorldDumbestFunction();

                //Act - execute this function
                string result = worldDumbest.ReturnsPikachuIfZero(num);

                //Assert - check the result
                if (result == "Pikachu")
                {
                    Console.WriteLine("Test Passed");
                }
                else
                {
                    Console.WriteLine("Test Failed");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
