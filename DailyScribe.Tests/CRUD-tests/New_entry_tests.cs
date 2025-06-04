using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyScribe.Tests.CRUD_tests
{
    public class New_entry_tests
    {
        [Fact]
        public void New_Entry_Should_Return_True()
        {
            var add = new NewEntry();

            var header = "test header";

            var body = "Lorem Ipsum is simply dummy text of the printing and typesetting industry.";

            var url = @"C:\\Users\\PC\\OneDrive - Office 365\\Pulpit";

            var fileName = $"{DateTime.Now:HH-mm}-{header}.txt";
            var filePath = Path.Combine(url, fileName);

            //

            add.Entry(header, body, url);

            //

            Assert.True(File.Exists(filePath), $"File {filePath} was not created");

            var content = File.ReadAllText(filePath);

            Assert.Equal(expected: body, actual: content);

            // clean after test

            File.Delete(filePath);
        }

        [Fact]
        public void New_Entry_Should_Throw_Null_Exception()
        {
            var add = new NewEntry();

            var header = "test header";

            var body = "Lorem Ipsum is simply dummy text of the printing and typesetting industry.";

            var url = @"";

            Assert.Throws<ArgumentNullException>(() => add.Entry(header, body, url));
        }
    }
}
