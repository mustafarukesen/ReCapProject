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
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        /*******************************************************************/

        [CacheRemoveAspect("ICarService.Get")]
        [SecuredOperation("car.add, admin")]
        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car car)
        {
                _carDal.Add(car);
                return new SuccessResult(Messages.CarAdded);

        }

        [CacheRemoveAspect("ICarService.Get")]
        [SecuredOperation("car.update, admin")]
        [ValidationAspect(typeof(CarValidator))]
        public IResult Update(Car car)
        {
                _carDal.Update(car);
                return new SuccessResult(Messages.CarUpdated);              
            
        }

        public IResult Delete(Car car)
        {
            foreach (var _car in _carDal.GetAll())
            {
                if (_car.CarId== car.CarId)
                {
                    _carDal.Delete(_car);
                    return new SuccessResult(Messages.CarDeleted);
                }                   
            }
            return new ErrorResult(Messages.Invalid);
        }

        /*******************************************************************/

        [CacheAspect]
        public IDataResult<Car> GetById(int carId)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.CarId == carId));
        }

        [CacheAspect]
        [PerformanceAspect(3)]
        public IDataResult<List<Car>> GetAll()
        {
            Thread.Sleep(3500);
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.CarList);
        }

        /*******************************************************************/

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(), Messages.CarDetail);
        }


        /*******************************************************************/

        [TransactionScopeAspect]
        public IResult TransactionalOperation(Car car)
        {
            _carDal.Update(car);
            _carDal.Add(car);
            return new SuccessResult(Messages.CarUpdated);
        }

    }
}
