using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(c => c.UserId).NotEmpty().WithMessage("Bu alanı boş bırakmayınız!");

            RuleFor(c => c.CompanyName).MinimumLength(7).WithMessage("7 karakterden fazlasını giremezsiniz!!");
        }
    }
}
