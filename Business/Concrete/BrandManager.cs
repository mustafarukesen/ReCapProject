using Business.Abstract;
using Business.Constants;
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

        public IResult Add(Brand brand)
        {
            if (brand.BrandName.Length >=2)
            {
                _brandDal.Add(brand);
                return new SuccessResult(Messages.BrandAdded);
            }

            return new ErrorResult(Messages.Invalid);
        }
        public IResult Update(Brand brand)
        {
            if (brand.BrandName.Length>=2)
            {
                _brandDal.Update(brand);
                return new SuccessResult(Messages.BrandUpdated);
            }

            return new ErrorResult(Messages.Invalid);
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
