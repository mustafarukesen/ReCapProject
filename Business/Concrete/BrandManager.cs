using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        /////////////////////

        public void Add(Brand brand)
        {
            if (brand.BrandName.Length >=2)
            {
                _brandDal.Add(brand);
            }
            else
            {
                Console.WriteLine("Lütfen karakter uzunluğu '2' den büyük bir değer giriniz.");
            }
        }
        public void Update(Brand brand)
        {
            if (brand.BrandName.Length>=2)
            {
                _brandDal.Update(brand);
            }
            else
            {
                Console.WriteLine("Lütfen karakter uzunluğu '2' den büyük bir değer giriniz.");
            }
        }

        public void Delete(Brand brand)
        {
            _brandDal.Delete(brand);
        }

        /////////////////////
        
        public Brand GetById(int id)
        {
            return _brandDal.Get(b => b.BrandId == id);
        }

        public List<Brand> GetAll()
        {
            return _brandDal.GetAll();
        }

    }
}
