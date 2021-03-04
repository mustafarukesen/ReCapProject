using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

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

        [CacheRemoveAspect("IBrandService.Get")]
        [SecuredOperation("brand.add, admin")]
        [ValidationAspect(typeof(BrandValidator))]
        public IResult Add(Brand brand)
        {
                _brandDal.Add(brand);
                return new SuccessResult(Messages.BrandAdded);
        }

        [CacheRemoveAspect("IBrandService.Get")]
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

        [TransactionScopeAspect]
        public IResult TransactionalOperation(Brand brand)
        {
            _brandDal.Update(brand);
            _brandDal.Add(brand);
            return new SuccessResult(Messages.BrandUpdated);
        }

        /*******************************************************************/

        [CacheAspect]
        public IDataResult<Brand> GetById(int brandId)
        {
            return new SuccessDataResult<Brand>(_brandDal.Get(b => b.BrandId == brandId));
        }

        [CacheAspect]
        [PerformanceAspect(3)]
        public IDataResult<List<Brand>> GetAll()
        {
            Thread.Sleep(3500);
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll(), Messages.BrandList);
        }
    }
}
