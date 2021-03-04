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
    public class CustomerManager : ICustomerService
    {
        ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        /*******************************************************************/

        [CacheRemoveAspect("ICustomerService.Get")]
        [SecuredOperation("customer.add, admin")]
        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Add(Customer customer)
        {
            _customerDal.Add(customer);
            return new SuccessResult(Messages.UserAdded);
        }

        [CacheRemoveAspect("ICustomerService.Get")]
        public IResult Update(Customer customer)
        {
            _customerDal.Update(customer);
            return new SuccessResult(Messages.UserUpdated);
        }

        public IResult Delete(Customer customer)
        {
            foreach (var _customer in _customerDal.GetAll())
            {
                if (_customer.CustomerId == customer.CustomerId)
                {
                    _customerDal.Delete(_customer);
                    return new SuccessResult(Messages.UserDeleted);
                }
            }
            return new ErrorResult(Messages.Invalid);
        }

        /*******************************************************************/

        [CacheAspect]
        [PerformanceAspect(3)]
        public IDataResult<List<Customer>> GetAll()
        {
            Thread.Sleep(3500);
            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll(), Messages.CustomerList);
        }

        [CacheAspect]
        public IDataResult<Customer> GetById(int customerId)
        {
            return new SuccessDataResult<Customer>(_customerDal.Get(c => c.CustomerId == customerId));
        }

        /*******************************************************************/

        [TransactionScopeAspect]
        public IResult TransactionalOperation(Customer customer)
        {
            _customerDal.Update(customer);
            _customerDal.Add(customer);
            return new SuccessResult(Messages.CustomerUpdated);
        }
    }
}
