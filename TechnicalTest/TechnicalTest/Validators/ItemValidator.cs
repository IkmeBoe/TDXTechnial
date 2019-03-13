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
            
            RuleFor(item => item.PartId)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .NotNull()
                .NotEqual(Guid.Empty);
            RuleFor(item => item.PartName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .NotNull();
            RuleFor(item => item.PartType)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .IsInEnum();
            RuleFor(item => item.Quantity)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .NotNull()
                .NotEqual(0);
            RuleFor(item => item.DateAdded)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .NotNull()
                .NotEqual(DateTime.MinValue);
            RuleFor(item => item.PartLength)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NotEmpty()
                .NotNull();

        }
        
    }
}
