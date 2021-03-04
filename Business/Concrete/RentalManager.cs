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
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        IRentalDal _rentalDal;

        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }

        /*******************************************************************/

        [CacheRemoveAspect("IRentalService.Get")]
        [SecuredOperation("rental.add, admin")]
        [ValidationAspect(typeof(RentalValidator))]
        public IResult Add(Rental rental)
        {
            var result = _rentalDal.GetAll(r => r.CarId == rental.CarId && r.ReturnDate == null);
            if (result.Count > 0)
            {
                return new ErrorResult(Messages.RentalAddedError);
            }
            _rentalDal.Add(rental);
            return new SuccessResult(Messages.RentalAdded);

        }

        [CacheRemoveAspect("IRentalService.Get")]
        public IResult Update(Rental rental)
        {

            _rentalDal.Update(rental);
            return new SuccessResult(Messages.RentalUpdated);
        }

        public IResult UpdateReturnDate(int rentalId)
        {
            var result = _rentalDal.GetAll(x => x.RentalId == rentalId);
            var updatedRental = result.LastOrDefault();
            if (updatedRental.ReturnDate != null)
            {
                return new ErrorResult(Messages.Invalid);
            }
            Console.WriteLine("Lütfen geri getirme tarihini giriniz: ");
            DateTime? updateReturnDate = Convert.ToDateTime(Console.ReadLine());
            Console.Clear();

            updatedRental.ReturnDate = updateReturnDate;
            _rentalDal.Update(updatedRental);
            return new SuccessResult(Messages.RentalUpdated);
        }

        public IResult Delete(Rental rental)
        {
            foreach (var _rental in _rentalDal.GetAll())
            {
                if (_rental.RentalId == rental.RentalId)
                {
                    _rentalDal.Delete(_rental);
                    return new SuccessResult(Messages.RentalDeleted);
                }
            }
            return new ErrorResult(Messages.Invalid);
        }

        /*******************************************************************/

        [CacheAspect]
        [PerformanceAspect(3)]
        public IDataResult<List<Rental>> GetAll()
        {
            Thread.Sleep(3500);
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(), Messages.RentalList);
        }

        public IDataResult<List<RentalDetailDto>> GetRentalDetails()
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetRentalDetails(), Messages.RentalDetail);
        }

        /*******************************************************************/

        [TransactionScopeAspect]
        public IResult TransactionalOperation(Rental rental)
        {
            _rentalDal.Update(rental);
            _rentalDal.Add(rental);
            return new SuccessResult(Messages.RentalUpdated);
        }

    }
}
