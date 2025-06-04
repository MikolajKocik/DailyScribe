using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyScribe.Tests.CRUD_tests
{
    public class Read_entry_tests
    {
        [Fact]
        public void Read_Entry_Should_Return_Expected_Strings()
        {
            var read = new ReadEntry();

            string logPath = "some/fake/path";

            string[] entries = { "note1.txt", "note2.txt", "note3.txt" };

            int getNumber = 3;

            //

            var result = read.Entries(logPath, entries, getNumber);

            var fill = string.Join(", ", result);

            //

            Assert.NotEmpty(result);
            Assert.False(string.IsNullOrWhiteSpace(fill));
        }

        [Fact]
        public void Read_Entry_Should_Throw_IndexOutOfRangeException_When_Number_Is_Out_Of_Bounds()
        {
            var read = new ReadEntry();

            string logPath = "some/fake/path";

            string[] entries = { "note1.txt", "note2.txt", "note3.txt" };

            int getNumber = 5;

            //

            Assert.Throws<IndexOutOfRangeException>(() => read.Entries(logPath, entries, getNumber));
        }

        [Fact]
        public void Read_Entry_Should_Throw_Directory__Not_Found_Exception()
        {
            var read = new ReadEntry();

            string logPath = "some/fake/path";

            string[] entries = { " " };

            int getNumber = entries.Length; // purposely set to entries.Length to trigger error scenario without specifying a hardcoded number

            //

            Assert.Throws<DirectoryNotFoundException>(() => read.Entries(logPath, entries, getNumber));
        }
    }
}
