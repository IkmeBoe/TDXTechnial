using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using TechnicalTest.FileLoad;
using TechnicalTest.Helpers;
using TechnicalTest.Interface;

namespace TechnicalTestUnitTests
{
    [TestFixture]
    public class UnitTest1
    {
        private ILog _log;
        private FileHelper _fileHelper;


        [SetUp]
        public void SetUp()
        {
            _log = new FakeLog();
            _fileHelper = new FileHelper();

        }
        [TestCase()]
        public void TestMethod1()
        {
            FileValidation fileValidation = new FileValidation(_log, "");

            var validRow = new List<string>
            {
                "1291e565-9543-e911-9e01-58fb84cf12b8,legobrick,plate,1,10/03/2019,22.00"
            };

            LoadedFile file = new LoadedFile
            {
                Filename = "DummyFile.csv",
                FileExtension = ".csv",
                FileBytes = validRow.SelectMany(s => System.Text.Encoding.ASCII.GetBytes(s)).ToArray()


        };
            fileValidation.ValidateFile(file, _fileHelper).Should().BeTrue();
        }

        [TestCase]
        public void FileShouldBeValid()
        {
           
            var file = _fileHelper.GetFile($@"{ AppDomain.CurrentDomain.BaseDirectory}\TestFiles\ValidFile1Row.csv");
            file.FileExtension.Should().Be("csv");
            file.Filename.Should().EndWith("ValidFile1Row.csv");
            file.FileBytes.Should().NotBeNullOrEmpty();

        }
    }
}
