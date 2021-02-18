using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class CarValidator : AbstractValidator<Car>
    {
        public CarValidator()
        {
            RuleFor(c => c.BrandId).NotEmpty().WithMessage("Bu alanı boş bırakmayınız!");

            RuleFor(c => c.ColorId).NotEmpty().WithMessage("Bu alanı boş bırakmayınız!");

            RuleFor(c => c.CarName).MinimumLength(3).WithMessage("3 karakterden azını giremezsiniz!!");

            RuleFor(c => c.DailyPrice).NotEmpty().WithMessage("Bu alanı boş bırakmayınız!");
            RuleFor(c => c.DailyPrice).GreaterThan(100).WithMessage("Seçtiğiniz arabanın değeri '100TL'den fazla olmalı!!");
            RuleFor(c => c.DailyPrice).GreaterThanOrEqualTo(150).When(c => c.BrandId == 5).WithMessage("Seçtiğiniz arabanın değeri '150TL'den fazla olmalı!!");

            RuleFor(c => c.Description).MinimumLength(30).WithMessage("30 karakterden fazlasını girmelisiniz!!");
        }
    }
}
