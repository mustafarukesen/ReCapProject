using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCarDal : ICarDal
    {
        List<Car> _cars;
        public InMemoryCarDal()
        {
            _cars = new List<Car>
            {
                new Car{CarId=1, BrandId=1, ColorId=1, ModelYear=2019, DailyPrice=8999, Description= "McLaren GT- 'Spor uzun yol otomobili' olarak kullanabileceğiniz bu aracı günlük 500₺'ye kiralayabilirsiniz." },
                new Car{CarId=2, BrandId=2, ColorId=1, ModelYear=2013, DailyPrice=129, Description= "Ford Tourneo Courier- Ailenizle vakit geçirmek ve günlük kullanım için gayet ideal araçtır." },
                new Car{CarId=3, BrandId=2, ColorId=2, ModelYear=2015, DailyPrice=7999, Description= "Ford Mustang Shelby GT350- Spor araba tutkunları için günlük kiralayıp keyifle gezebileceğiniz bir spor araba."},
                new Car{CarId=4, BrandId=3, ColorId=3, ModelYear=2015, DailyPrice=139, Description= "Volkswagen Polo- Her türlü günlük ihtiyacınızı kolaylıkla giderebileceğiniz konforlu bir araç."},
                new Car{CarId=5, BrandId=3, ColorId=1, ModelYear=2020, DailyPrice=599, Description= "Volkswagen Passat 1.5 TSI Business- Tam bir iş arabası olarak kullanabileceğiniz bu modeli yıllık olarak kiralayabilirsiniz."},
                new Car{CarId=6, BrandId=4, ColorId=4, ModelYear=2017, DailyPrice=699, Description= "Mercedes - Benz C 200 d BlueTEC Comfort- Orta sınıf bir araç için mükemmel bir fiyat ve kullanım avantajı. Size en uygun olabilecek araçlardan biri."}
        };
        }
        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public void Delete(Car car)
        {
            Car carToDelete = _cars.SingleOrDefault(c=> c.CarId==car.CarId);

            _cars.Remove(car);
        }

        public List<Car> GetAll()
        {
            return _cars;
        }

        public List<Car> GetAllByBrand(int brandId)
        {
            return _cars.Where(c=> c.BrandId==brandId).ToList();
        }

        public List<Car> GetAllByColor(int colorId)
        {
            return _cars.Where(c=> c.ColorId==colorId).ToList();
        }

        public void Update(Car car)
        {
            Car carToUpdate = _cars.SingleOrDefault(c => c.CarId == car.CarId);
            carToUpdate.CarId = car.CarId;
            carToUpdate.BrandId = car.BrandId;
            carToUpdate.ColorId = car.ColorId;
            carToUpdate.ModelYear = car.ModelYear;
            carToUpdate.DailyPrice = car.DailyPrice;
            carToUpdate.Description = car.Description;
        }
    }
}
