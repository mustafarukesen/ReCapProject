using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            CarManager carManager = new CarManager(new EfCarDal());
            BrandManager brandManager = new BrandManager(new EfBrandDal());
            ColorManager colorManager = new ColorManager(new EfColorDal());


            bool finfis = true;
            while (finfis)
            {
                Console.WriteLine("-------------Mustafa RENT a CAR-------------\n");
                Console.WriteLine("Lütfen yapmak istediğiniz işlemi seçiniz:\n");
                Console.WriteLine("1-Tüm Araç bilgilerini gör \n2-Araç Ekle \n3-Araç Güncelle \n4-Araç Sil \n5-Tüm Marka Bilgilerini Gör \n6-Marka Ekle \n7-Marka Güncelle \n8-Marka Sil \n9-Tüm Renk Bilgilerini Gör \n10-Renk Ekle \n11-Renk Güncelle \n12-Renk Sil \n13-Çıkış");
                int select = Convert.ToInt32(Console.ReadLine());
                Console.Clear();

                switch (select)
                {
                    case 1:  CarsList(carManager);              break;
                    case 2:  AddCar(carManager);                break;
                    case 3:  CarUpdate(carManager);             break;
                    case 4:  DeleteCar(carManager);             break;
                    case 5:  BrandsList(brandManager);          break;
                    case 6:  AddBrand(brandManager);            break;
                    case 7:  BrandUpdate(brandManager);         break;
                    case 8:  DeleteBrand(brandManager);         break;
                    case 9:  ColorsList(colorManager);          break;
                    case 10: AddColor(colorManager);            break;
                    case 11: UpdateColor(colorManager);         break;
                    case 12: DeleteColor(colorManager);         break;
                    case 13: finfis = false;                    break;
                    default: Console.WriteLine("Lütfen 1-13 arasında bir değer giriniz."); break;
                }
            }

        }

        private static void DeleteColor(ColorManager colorManager)
        {
            ColorsList(colorManager);
            Console.WriteLine("Lütfen silmek istediğiniz rengin 'id' sini giriniz: ");
            int deleteColor = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            colorManager.Delete(new Color { ColorId = deleteColor });
        }

        private static void UpdateColor(ColorManager colorManager)
        {
            ColorsList(colorManager);
            Console.WriteLine("Lütfen güncellemek istediğiniz rengin 'id' sini giriniz: ");
            int updateColor = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            Console.WriteLine("Lütfen güncellemek istediğiniz rengin ismini giriniz: ");
            string updateColorName = Console.ReadLine();
            Console.Clear();
            colorManager.Update(new Color { ColorId = updateColor, ColorName = updateColorName });
        }

        private static void AddColor(ColorManager colorManager)
        {
            ColorsList(colorManager);
            Console.WriteLine("Lütfen eklemek istediğiniz rengin ismini giriniz: ");
            string addColorName = Console.ReadLine();
            Console.Clear();
            colorManager.Add(new Color { ColorName = addColorName });
        }

        private static void DeleteBrand(BrandManager brandManager)
        {
            BrandsList(brandManager);
            Console.WriteLine("Lütfen silmek istediğiniz marka 'id' sini giriniz: ");
            int deleteBrand = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            brandManager.Delete(new Brand { BrandId = deleteBrand });
        }

        private static void BrandUpdate(BrandManager brandManager)
        {
            BrandsList(brandManager);
            Console.WriteLine("Lütfen güncellemek istediğiniz marka 'id' sini giriniz: ");
            int updateBrand = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            Console.WriteLine("Lütfen eklemek istediğiniz marka ismini giriniz: ");
            string updateBrandName = Console.ReadLine();
            Console.Clear();
            brandManager.Update(new Brand { BrandId = updateBrand, BrandName = updateBrandName });
        }

        private static void AddBrand(BrandManager brandManager)
        {
            BrandsList(brandManager);
            Console.WriteLine("Lütfen eklemek istediğiniz marka ismini giriniz: ");
            string addBrandName = Console.ReadLine();
            Console.Clear();
            brandManager.Add(new Brand { BrandName = addBrandName });
        }

        private static void DeleteCar(CarManager carManager)
        {
            CarsList(carManager);
            Console.WriteLine("Lütfen silmek istediğiniz aracın 'id' sini giriniz: ");
            int deleteCar = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            carManager.Delete(new Car { CarId = deleteCar });
        }

        private static void CarUpdate(CarManager carManager)
        {
            CarsList(carManager);
            Console.WriteLine("Lütfen güncellemek istediğiniz aracın 'id' sini giriniz: ");
            int updateCar = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            Console.WriteLine("Aracın 'BrandId''sini giriniz: ");
            int updateBrandId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Aracın 'ColorId''sini giriniz: ");
            int updateColorId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Aracın 'Model Year''ını giriniz:");
            int updateModelYear = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Aracın 'Daily Price''ını giriniz:");
            decimal updateDailyPrice = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Aracın 'Description''ını giriniz:");
            string updateDescription = Console.ReadLine();
            Console.Clear();
            carManager.Update(new Car { CarId = updateCar, BrandId = updateBrandId, ColorId = updateColorId, ModelYear = updateModelYear, DailyPrice = updateDailyPrice, Description = updateDescription });
        }

        private static void AddCar(CarManager carManager)
        {
            Console.WriteLine("Aracın 'BrandId''sini giriniz: ");
            int addBrandId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Aracın 'ColorId''sini giriniz: ");
            int addColorId = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Aracın 'Model Year''ını giriniz:");
            int addModelYear = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Aracın 'Daily Price''ını giriniz:");
            decimal addDailyPrice = Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("Aracın 'Description''ını giriniz:");
            string addDescription = Console.ReadLine();
            Console.Clear();
            carManager.Add(new Car { BrandId = addBrandId, ColorId = addColorId, ModelYear = addModelYear, DailyPrice = addDailyPrice, Description = addDescription });
        }

        private static void ColorsList(ColorManager colorManager)
        {
            foreach (var colors in colorManager.GetAll())
            {
                Console.WriteLine(colors.ColorId + " ----- " + colors.ColorName);
            }
        }

        private static void BrandsList(BrandManager brandManager)
        {
            foreach (var brands in brandManager.GetAll())
            {
                Console.WriteLine(brands.BrandId + " ----- " + brands.BrandName);
            }
        }

        private static void CarsList(CarManager carManager)
        {
            foreach (var cars in carManager.GetCarDetails())
            {
                Console.WriteLine("CarId: " + cars.CarId + ",  Car Daily Price: " + cars.DailyPrice + ",  Car Color: " + cars.ColorName);
                Console.WriteLine("Car Name: " + cars.BrandName + cars.Description);
            }
        }
    }
}
