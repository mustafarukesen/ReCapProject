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
    public class ColorManager : IColorService
    {
        IColorDal _colorDal;

        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }

        /*******************************************************************/

        [CacheRemoveAspect("IColorService.Get")]
        [SecuredOperation("color.add, admin")]
        [ValidationAspect(typeof(ColorValidator))]
        public IResult Add(Color color)
        {
            _colorDal.Add(color);
            return new SuccessResult(Messages.ColorAdded);
        }

        [CacheRemoveAspect("IColorService.Get")]
        [SecuredOperation("color.update, admin")]
        [ValidationAspect(typeof(ColorValidator))]
        public IResult Update(Color color)
        {
            _colorDal.Update(color);
            return new SuccessResult(Messages.ColorUpdated);
        }

        public IResult Delete(Color color)
        {
            foreach (var _color in _colorDal.GetAll())
            {
                if (_color.ColorId == color.ColorId)
                {
                    _colorDal.Delete(_color);
                    return new SuccessResult(Messages.ColorDeleted);
                }
            }
            return new ErrorResult(Messages.Invalid);
        }

        /*******************************************************************/
        
        [CacheAspect]
        public IDataResult<Color> GetById(int colorId)
        {
            return new SuccessDataResult<Color>(_colorDal.Get(c=> c.ColorId == colorId));
        }

        [CacheAspect]
        [PerformanceAspect(3)]
        public IDataResult<List<Color>> GetAll()
        {
            Thread.Sleep(3500);
            return new SuccessDataResult<List<Color>>(_colorDal.GetAll(), Messages.ColorList);
        }

        /*******************************************************************/

        [TransactionScopeAspect]
        public IResult TransactionalOperation(Color color)
        {
            _colorDal.Update(color);
            _colorDal.Add(color);
            return new SuccessResult(Messages.ColorUpdated);
        }

    }
}
