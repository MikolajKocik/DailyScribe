using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyScribe.Tests
{
    public class CRUD
    {

        private readonly static string filePath = @"C:\Users\PC\OneDrive - Office 365\Pulpit\Inne\komendyv2.txt";
        private readonly static string fileName = "test.txt";

        [Fact]
        public void Add_Entry_Should_Return_True()
        {

        }
        
        [Fact]
        public void Read_Entry_Should_Return_True()
        {

        }
      
        [Fact]
        public void Edit_Entry_Should_Return_True()
        {

        }
        
        [Fact]
        public void Delete_Entry_Should_Return_True()
        {
            var delete = new DeleteEntry();

            //

           /*

            Assert.NotNull(filePath);
            Assert.NotNull(filePath);

            if (!Directory.Exists(filePath)!)
            {
                Assert.Throws<DirectoryNotFoundException>(exception!);
            }   
           */
        }
    }
}
