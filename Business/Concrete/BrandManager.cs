using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        /*******************************************************************/
        [SecuredOperation("brand.add, admin")]
        [ValidationAspect(typeof(BrandValidator))]
        public IResult Add(Brand brand)
        {
                _brandDal.Add(brand);
                return new SuccessResult(Messages.BrandAdded);
        }
        [SecuredOperation("brand.update, admin")]
        [ValidationAspect(typeof(BrandValidator))]
        public IResult Update(Brand brand)
        {
                _brandDal.Update(brand);
                return new SuccessResult(Messages.BrandUpdated);
        }

        public IResult Delete(Brand brand)
        {
            foreach (var _brand in _brandDal.GetAll())
            {
                if (_brand.BrandId == brand.BrandId)
                {
                    _brandDal.Delete(_brand);
                    return new SuccessResult(Messages.BrandDeleted);
                }
            }
            return new ErrorResult(Messages.Invalid);
        }

        /*******************************************************************/

        public IDataResult<Brand> GetById(int brandId)
        {
            return new SuccessDataResult<Brand>(_brandDal.Get(b => b.BrandId == brandId));
        }

        public IDataResult<List<Brand>> GetAll()
        {
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll(), Messages.BrandList);
        }

    }
}
