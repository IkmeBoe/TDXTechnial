using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TechnicalTest.Enums;
using TechnicalTest.FileLoad;
using TechnicalTest.Helpers;
using TechnicalTest.Interface;
using TechnicalTest.Inventory;

namespace TechnicalTestUnitTests
{
    [TestFixture]
    public class UnitTest1
    {
        private ILog _log;
        private FileHelper _fileHelper;
        private DbHelper _dbHelper;

        [SetUp]
        public void SetUp()
        {
            _log = new FakeLog();
            _fileHelper = new FileHelper();
            _dbHelper = new DbHelper(null);
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
