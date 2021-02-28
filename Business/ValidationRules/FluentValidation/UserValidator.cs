using Core.Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.FirstName).NotEmpty().WithMessage("Bu alanı boş bırakmayınız!");
            RuleFor(u => u.FirstName).MaximumLength(30).WithMessage("30 karakterden fazlasını giremezsiniz!!");

            RuleFor(u => u.LastName).NotEmpty().WithMessage("Bu alanı boş bırakmayınız!");
            RuleFor(u => u.LastName).MaximumLength(30).WithMessage("30 karakterden fazlasını giremezsiniz!!");

            RuleFor(u => u.Email).NotEmpty().WithMessage("Bu alanı boş bırakmayınız!");
            RuleFor(u => u.Email).MaximumLength(40).WithMessage("40 karakterden fazlasını giremezsiniz!!");
        }
    }
}
