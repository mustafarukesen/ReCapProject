using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
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
using System.Linq;
using System.Text;
using System.Threading;

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

        [CacheRemoveAspect("ICarImageService.Get")]
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

        [CacheRemoveAspect("ICarImageService.Get")]
        [SecuredOperation("carImage.update, admin")]
        [ValidationAspect(typeof(CarImageValidator))]
        public IResult Update(IFormFile file, CarImage carImage)
        {
            IResult result = BusinessRules.Run(CheckImageLimit(carImage.CarId));

            if (result != null)
            {
                return result;
            }

            carImage.ImagePath = FileHelper.UpdateAsync(_carImageDal.Get(ci => ci.CarImageId == carImage.CarImageId).ImagePath, file);
            carImage.Date = DateTime.Now;
            _carImageDal.Update(carImage);
            return new SuccessResult(Messages.CarImageUpdated);
        }

        public IResult Delete(CarImage carImage)
        {
            FileHelper.DeleteAsync(carImage.ImagePath);
            _carImageDal.Delete(carImage);
            return new SuccessResult(Messages.CarImageDeleted);
        }

        /*******************************************************************/

        public IDataResult<CarImage> Get(int carImageId)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(ci => ci.CarImageId == carImageId));
        }

        [CacheAspect]
        public IDataResult<List<CarImage>> GetByCarId(int carId)
        {
            IResult result = BusinessRules.Run(CheckIfCarImageNull(carId));

            if (result != null)
            {
                return new ErrorDataResult<List<CarImage>>(result.Message);
            }

            return new SuccessDataResult<List<CarImage>>(CheckIfCarImageNull(carId).Data);
        }

        [CacheAspect]
        [PerformanceAspect(3)]
        public IDataResult<List<CarImage>> GetAll()
        {
            Thread.Sleep(3500);
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(), Messages.CarImageList);
        }

        /*******************************************************************/

        [TransactionScopeAspect]
        public IResult TransactionalOperation(CarImage carImage)
        {
            _carImageDal.Update(carImage);
            _carImageDal.Add(carImage);
            return new SuccessResult(Messages.CarImageUpdated);
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

        private IDataResult<List<CarImage>> CheckIfCarImageNull(int id)
        {
            try
            {
                string path = @"\Images\default.png";

                var result = _carImageDal.GetAll(c => c.CarId == id).Any();

                if (!result)
                {
                    List<CarImage> carImage = new List<CarImage>();

                    carImage.Add(new CarImage { CarId = id, Date = DateTime.Now, ImagePath = path});

                    return new SuccessDataResult<List<CarImage>>(carImage);
                }
            }
            catch (Exception e)
            {

                return new ErrorDataResult<List<CarImage>>(e.Message);
            }

            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll(c => c.CarId == id));
        }

    }
}
