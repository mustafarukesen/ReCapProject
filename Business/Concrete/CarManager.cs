﻿using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

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

        public IResult Add(Car car)
        {
            if (car.DailyPrice>0)
            {
                _carDal.Add(car);
                return new SuccessResult(Messages.CarAdded);
            }
            return new ErrorResult(Messages.Invalid);

        }
        public IResult Update(Car car)
        {
            if (car.DailyPrice>0)
            {
                _carDal.Update(car);
                return new SuccessResult(Messages.CarUpdated);              
            }
            return new ErrorResult(Messages.Invalid);
            
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

        public IDataResult<Car> GetById(int carId)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.CarId == carId));
        }

        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.CarList);
        }

        /*******************************************************************/

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(), Messages.CarDetail);
        }

    }
}
