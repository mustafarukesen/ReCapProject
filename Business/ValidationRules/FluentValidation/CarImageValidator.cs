using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class CarImageValidator : AbstractValidator<CarImage>
    {
        public CarImageValidator()
        {
            RuleFor(ci => ci.CarId).NotEmpty().WithMessage("Bu alanı boş bırakmayınız!");
            //RuleFor(ci => ci.ImagePath).NotEmpty().WithMessage("Bu alanı boş bırakmayınız!");
            //RuleFor(ci => ci.Date).NotEmpty().WithMessage("Bu alanı boş bırakmayınız!");
        }
    }
}
