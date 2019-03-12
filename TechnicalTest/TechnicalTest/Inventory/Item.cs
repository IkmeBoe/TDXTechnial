using System;
using TechnicalTest.Attributes;
using TechnicalTest.Enums;

namespace TechnicalTest.Inventory
{
    internal class Item
    {
        [Column(1)]
        public Guid PartId { get; set; }
        [Column(2)]
        public string PartName { get; set; }
        [Column(3)]
        public PartType PartType { get; set; }
        [Column(4)]
        public int Quantity { get; set; }
        [Column(5)]
        public DateTime DateAdded { get; set; }
        [Column(6)]
        public double? PartLength { get; set; }

    }
}
