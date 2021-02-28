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
    public class EfRentalDal : EfEntityRepositoryBase<Rental, ReCapProjectDbContext>, IRentalDal
    {
        public List<RentalDetailDto> GetRentalDetails(Expression<Func<Rental, bool>> filter=null)
        {
            using (ReCapProjectDbContext context = new ReCapProjectDbContext())
            {
                var result = from r  in context.Rentals
                             join ca in context.Cars      on r.CarId      equals  ca.CarId
                             join cu in context.Customers on r.CustomerId equals  cu.CustomerId
                             join u  in context.Users     on cu.UserId    equals  u.Id
                             select new RentalDetailDto
                             {
                                 RentalId = r.RentalId,
                                 RentDate   = r.RentDate,
                                 ReturnDate = r.ReturnDate,
                                 CarId = ca.CarId,
                                 CarName = ca.CarName,
                                 CustomerName = cu.CompanyName,
                                 UserName = u.FirstName + " " + u.LastName                                 
                             };

                return result.ToList();
            }
        }
    }
}
