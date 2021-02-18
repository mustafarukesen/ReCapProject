using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class RentalValidator : AbstractValidator<Rental>
    {
        public RentalValidator()
        {
            RuleFor(r => r.CarId).NotEmpty().WithMessage("Bu alanı boş bırakmayınız!");

            RuleFor(r => r.CustomerId).NotEmpty().WithMessage("Bu alanı boş bırakmayınız!");

            RuleFor(r => r.RentDate).NotEmpty().WithMessage("Bu alanı boş bırakmayınız!");
        }
    }
}
