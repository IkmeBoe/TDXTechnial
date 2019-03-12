using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FluentValidation;
using FluentValidation.Validators;
using TechnicalTest.Attributes;
using TechnicalTest.Enums;
using TechnicalTest.Inventory;

namespace TechnicalTest.Validators
{
    class ItemValidator : AbstractValidator<Item>
    {
        public ItemValidator()
        {
            
            RuleFor(item => item.PartId).NotNull().NotEqual(Guid.Empty);
            RuleFor(item => item.PartName).NotNull();
            RuleFor(item => item.PartType).NotNull().IsInEnum();
            RuleFor(item => item.Quantity).NotNull().NotEqual(0);
            RuleFor(item => item.DateAdded).NotNull().NotEqual(DateTime.MinValue);
            RuleFor(item => item.PartLength).NotNull();

        }
        
    }
}
