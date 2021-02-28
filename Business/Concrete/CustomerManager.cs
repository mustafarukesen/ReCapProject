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
    public class CustomerManager : ICustomerService
    {
        ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        /*******************************************************************/

        [SecuredOperation("customer.add, admin")]
        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Add(Customer customer)
        {
            _customerDal.Add(customer);
            return new SuccessResult(Messages.UserAdded);
        }


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

        public IDataResult<List<Customer>> GetAll()
        {
            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll(), Messages.CustomerList);
        }

        public IDataResult<Customer> GetById(int customerId)
        {
            return new SuccessDataResult<Customer>(_customerDal.Get(c => c.CustomerId == customerId));
        }
    }
}
