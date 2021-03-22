using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, ReCapProjectDbContext>, ICarDal
    {       
        public List<CarDetailDto> GetCarDetails(Expression<Func<Car, bool>> filter = null)
        {
            using (ReCapProjectDbContext context = new ReCapProjectDbContext())
            {
                 var result = from c in filter is null ? context.Cars : context.Cars.Where(filter)
                              join b in context.Brands on c.BrandId equals b.BrandId
                              join cl in context.Colors on c.ColorId equals cl.ColorId
                              select new CarDetailDto
                              {
                                  CarId = c.CarId,
                                  BrandId = b.BrandId,
                                  ColorId = cl.ColorId,
                                  CarName = c.CarName,
                                  Description = c.Description,
                                  DailyPrice = c.DailyPrice,
                                  BrandName = b.BrandName,
                                  ColorName = cl.ColorName,
                                  ModelYear = c.ModelYear,
                                  ImagePath = context.CarImages.Where(ci => ci.CarId == c.CarId).FirstOrDefault().ImagePath
                              };
                return result.ToList();
            }
        }

    }
}
