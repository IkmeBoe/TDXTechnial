using System;

namespace TechnicalTest.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ColumnAttribute : Attribute
    {
        private int ColumnNumber { get; }

        public ColumnAttribute(int columnNumber)
        {
            ColumnNumber = columnNumber;
        }
    }
}
