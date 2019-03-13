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
            
            RuleFor(item => item.PartId).NotEmpty().NotNull().NotEqual(Guid.Empty);
            RuleFor(item => item.PartName).NotEmpty().NotNull();
            RuleFor(item => item.PartType).NotEmpty().NotEmpty().IsInEnum();
            RuleFor(item => item.Quantity).NotEmpty().NotNull().NotEqual(0);
            RuleFor(item => item.DateAdded).NotEmpty().NotNull().NotEqual(DateTime.MinValue);
            RuleFor(item => item.PartLength).NotEmpty().NotNull();

        }
        
    }
}
