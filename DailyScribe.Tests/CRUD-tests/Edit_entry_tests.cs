using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyScribe.Tests.CRUD_tests
{
    public class Edit_entry_tests
    {
        private readonly string _filePath = @"C:\Users\PC\OneDrive - Office 365\Pulpit\Inne\";

        [Fact]
        public void Edit_Entry_Should_Return_True()
        {
            var entryToEdit = new EditEntry();

            Uri url = new Uri(_filePath);

            var filePath = url.LocalPath;

            string entryName = "komendyv2.txt";

            string newText = "Lorem ipsum";

            var getPath = Path.Combine(filePath, entryName);

            //

            string[] lines = File.ReadAllLines(getPath);

            newText = lines[1]; // w menu dodajemy 1 numer w górę aby użytkownik nie podał '0'

            entryToEdit.Edit(getPath, entryName, newText, 2);

            //

            Assert.Equal(newText, lines[1]);
        }

        [Fact]
        public void Edit_Entry_Should_Return_New_Text_Is_Empty_Argument_Exception()
        {
            var entryToEdit = new EditEntry();

            Uri url = new Uri(_filePath);

            var filePath = url.LocalPath;

            string entryName = "komendyv2.txt";

            string newText = "";

            var getPath = Path.Combine(filePath, entryName);

            //

            Assert.Throws<ArgumentException>(() => entryToEdit.Edit(getPath, entryName, newText, 2));
        }

        [Fact]
        public void Edit_Entry_Should_Return_Index_Out_Of_Range_Exception_Line_Number()
        {
            var entryToEdit = new EditEntry();

            Uri url = new Uri(_filePath);

            var filePath = url.LocalPath;

            string entryName = "komendyv2.txt";

            string newText = "Lorem ipsum";

            var getPath = Path.Combine(filePath, entryName);

            //

            Assert.Throws<IndexOutOfRangeException>(() => entryToEdit.Edit(getPath, entryName, newText, 0));
        }

        [Fact]
        public void Edit_Entry_Should_Return_Uri_Format_Exception()
        {
            string filePath = @"dawdadawdawd";

            //

            Assert.Throws<UriFormatException>(() =>
            {
                Uri url = new Uri(filePath);
            });
        }

        [Fact]
        public void Edit_Entry_Should_Return_Directory_Not_Found_Exception()
        {
            string filePath = @"C:\Us\PC\OneDrake - Office 22\Pulpot\Inne";

            string entryName = "notexist.txt";

            var fullPath = Path.Combine(filePath, entryName);
            //

            var edit = new EditEntry();

            Assert.Throws<DirectoryNotFoundException>(() =>
            {
                edit.Edit(fullPath, entryName, null, 0);

            });
        }
    }
}
