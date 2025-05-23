using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyScribe.Tests
{
    public class Delete_entry_tests
    {
        private readonly string _filePath = @"C:\Users\PC\OneDrive - Office 365\Pulpit\Inne\";

        [Fact]
        public void Delete_Entry_Should_Return_True()
        {
            var delete = new DeleteEntry();

            Uri url = new Uri(_filePath);

            var filePath = url.LocalPath;

            string entryName = "komendyv2.txt";

            var getPath = Path.Combine(filePath, entryName);

            int lineToDelete = 2;

            var result = delete.Remove(getPath, entryName, lineToDelete);
            Assert.NotNull(result);
        }
        
        [Fact]
        public void Delete_Entry_Should_Return_Uri_Is_Empty()
        {
            var delete = new DeleteEntry();

            var emptyPath = "";

            Assert.Throws<UriFormatException>(() =>
            {
                Uri url = new Uri(emptyPath);
            });
        }

        [Fact]
        public void Delete_Entry_Should_Return_Entry_Name_Not_Found()
        {
            var delete = new DeleteEntry();

            Uri url = new Uri(_filePath);

            var filePath = url.LocalPath;

            string entryName = "";

            var getPath = Path.Combine(filePath, entryName);

            //

            Assert.Throws<DirectoryNotFoundException>(() => delete.Remove(getPath, entryName, 0));
        }

        [Fact]
        public void Delete_Entry_Should_Return_Invalid_Line_Number()
        {
            var delete = new DeleteEntry();

            Uri url = new Uri(_filePath);

            var filePath = url.LocalPath;

            string entryName = "komendyv2.txt";

            var getPath = Path.Combine(filePath, entryName);

            int lineToDelete = -1;

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                delete.Remove(getPath, entryName, lineToDelete);
            });
        }
    }
}
