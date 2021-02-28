using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.FileHelper;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        ICarImageDal _carImageDal;

        public CarImageManager(ICarImageDal carImageDal)
        {
            _carImageDal = carImageDal;
        }

        /*******************************************************************/

        [SecuredOperation("carImage.add, admin")]
        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Add(IFormFile file, CarImage carImage)
        {
            IResult result = BusinessRules.Run( CheckImageLimit(carImage.CarId) );

            if (result != null)
            {
                return result;
            }

            carImage.ImagePath = FileHelper.AddAsync(file);
            carImage.Date = DateTime.Now;
            _carImageDal.Add(carImage);
            return new SuccessResult(Messages.CarImageAdded);
        }

        [SecuredOperation("carImage.update, admin")]
        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Update(IFormFile file, CarImage carImage)
        {
            carImage.ImagePath = FileHelper.UpdateAsync(_carImageDal.Get(ci => ci.CarImageId == carImage.CarImageId).ImagePath, file);
            carImage.Date = DateTime.Now;
            _carImageDal.Update(carImage);
            return new SuccessResult(Messages.CarImageUpdated);
        }

        public IResult Delete(CarImage carImage)
        {
            IResult result = BusinessRules.Run( CarImageDelete(carImage) );
            if (result != null)
            {
                return new ErrorResult();
            }

            _carImageDal.Delete(carImage);

            return new SuccessResult(Messages.CarImageDeleted);
        }

        /*******************************************************************/

        public IDataResult<CarImage> Get(int carImageId)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(ci => ci.CarImageId == carImageId));
        }

        public IDataResult<CarImage> GetByCarId(int carId)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(c => c.CarId == carId));
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(), Messages.CarImageList);
        }

        /*******************************************************************/

        private IResult CheckImageLimit(int carId)
        {
            if (_carImageDal.GetAll(ci => ci.CarId == carId).Count >= 5)
            {
                return new ErrorResult(Messages.CarImageLimitedError);
            }
            return new SuccessResult();
        }

        private IResult CarImageDelete(CarImage carImage)
        {
            try
            {
                File.Delete(carImage.ImagePath);
            }
            catch (Exception exception)
            {

                return new ErrorResult(exception.Message);
            }
            return new SuccessResult();
        }

    }
}
