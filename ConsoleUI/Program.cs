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

            RentalManager rentalManager = new RentalManager(new EfRentalDal());
            CustomerManager customerManager = new CustomerManager(new EfCustomerDal());
            UserManager userManager = new UserManager(new EfUserDal());


            bool finfis = true;
            while (finfis)
            {
                Console.WriteLine("-------------Mustafa RENT a CAR-------------\n");
                Console.WriteLine("Lütfen yapmak istediğiniz işlemi seçiniz:\n");
                Console.WriteLine("1-Tüm Araç Bilgilerini Gör \n2-Araç Ekle \n3-Araç Güncelle \n4-Araç Sil \n");
                Console.WriteLine("5-Tüm Marka Bilgilerini Gör \n6-Marka Ekle \n7-Marka Güncelle \n8-Marka Sil \n");
                Console.WriteLine("9-Tüm Renk Bilgilerini Gör \n10-Renk Ekle \n11-Renk Güncelle \n12-Renk Sil \n");
                Console.WriteLine("13-Tüm Kullanıcı Bilgilerini Gör \n14-Kullanıcı Ekle \n15-Kullanıcı Güncelle \n16-Kullanıcı Sil \n\n17-Tüm Müşteri Bilgilerini Gör \n18-Müşteri Ekle \n");
                Console.WriteLine("\n19-Tüm Kiralama Bilgilerini Gör \n20-Kiralama Ekle \n21-Kiralama Güncelle \n22-Kiralama Sil \n23-Geri Getirme Tarihi Ekle  \n\n24-Çıkış \n");
                int select = Convert.ToInt32(Console.ReadLine());
                Console.Clear();

                switch (select)
                {
                    case 1:  CarsList(carManager);                                               break;
                    case 2:  AddCar(carManager, brandManager, colorManager);                     break;
                    case 3:  UpdateCar(carManager, brandManager, colorManager);                  break;
                    case 4:  DeleteCar(carManager);                                              break;
                    case 5:  BrandsList(brandManager);                                           break;
                    case 6:  AddBrand(brandManager);                                             break;
                    case 7:  UpdateBrand(brandManager);                                          break;
                    case 8:  DeleteBrand(brandManager);                                          break;
                    case 9:  ColorsList(colorManager);                                           break;
                    case 10: AddColor(colorManager);                                             break;
                    case 11: UpdateColor(colorManager);                                          break;
                    case 12: DeleteColor(colorManager);                                          break;
                    case 13: UsersList(userManager);                                             break;
                    case 14: AddUser(userManager);                                               break;
                    case 15: UpdateUser(userManager);                                            break;
                    case 16: DeleteUser(userManager);                                            break;
                    case 17: CustomersList(customerManager);                                     break;
                    case 18: AddCustomer(customerManager, userManager);                          break;
                    case 19: RentalsList(rentalManager);                                         break;
                    case 20: AddRental(carManager, rentalManager, customerManager, userManager); break;
                    //case 21: UpdateRental(carManager, rentalManager, customerManager);         break;
                    case 22: DeleteRental(rentalManager);                                        break;
                    case 24: finfis = false;                                                     break;
                    case 23: UpdateReturnDate(rentalManager);                                    break;
                    default: Console.WriteLine("Lütfen 1-13 arasında bir değer giriniz.");       break;
                }
            }

            
        }

        private static void UpdateReturnDate(RentalManager rentalManager)
        {
            RentalsList(rentalManager);
            Console.WriteLine("Lütfen hangi kiralamaya geri getirme tarihi eklemek istiyorsanız 'rentalId'sini giriniz: ");
            int updateRental = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            //Console.WriteLine("Lütfen 'son kiralanma tarihi' giriniz: ");
            //DateTime updateReturnDate = Convert.ToDateTime(Console.ReadLine());
            //Console.Clear();
            //story.modifiedDate = DateTime.Now;
            //var story = _db.ArticleSet.First(a => a.storyId == ArticleToEdit.storyId);
            rentalManager.Update(new Rental { RentalId = updateRental, ReturnDate = DateTime.Now });
        }

        private static void AddCustomer(CustomerManager customerManager, UserManager userManager)
        {
            Console.WriteLine("Şirket varsa '1' yoksa '2' giriniz: ");
            int company = Convert.ToInt32(Console.ReadLine());
            switch (company)
            {
                case 1:
                    Console.WriteLine("Lütfen şirket adınızı giriniz: ");                                                   string addCustomerCompany = Console.ReadLine();
                    UsersList(userManager); Console.WriteLine("Lütfen aracı kiralayan kullanıcının 'id'sini giriniz");      int selectUser = Convert.ToInt32(Console.ReadLine());
                    customerManager.Add(new Customer { UserId = selectUser, CompanyName = addCustomerCompany });
                    Console.Clear(); break;
                case 2:
                    UsersList(userManager); Console.WriteLine("Lütfen aracı kiralayan kullanıcının 'id'sini giriniz");      int selectUser1 = Convert.ToInt32(Console.ReadLine());
                    customerManager.Add(new Customer { UserId = selectUser1 });
                    Console.Clear(); break;
                default: Console.WriteLine("Yanlış girdiniz!"); break;
            }
        }

        private static void DeleteRental(RentalManager rentalManager)
        {
            RentalsList(rentalManager);
            Console.WriteLine("Lütfen silmek istediğiniz kiralamanın 'id' sini giriniz: ");
            int deleteRental = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            rentalManager.Delete(new Rental { RentalId = deleteRental });
        }

        //private static void UpdateRental(CarManager carManager, RentalManager rentalManager, CustomerManager customerManager)
        //{
        //    RentalsList(rentalManager);
        //    Console.WriteLine("Lütfen güncellemek istediğiniz kiralamanın 'id'sini giriniz: ");
        //    int updateRental = Convert.ToInt32(Console.ReadLine());
        //    Console.Clear();

        //    CarsList(carManager);
        //    Console.WriteLine("Lütfen kiralanan aracı güncellemek için 'id'sini giriniz: ");
        //    int updateCar = Convert.ToInt32(Console.ReadLine());
        //    Console.Clear();

        //    Console.WriteLine("Lütfen güncellenen 'kiralanma tarihi'ni giriniz: ");
        //    DateTime updateRent = Convert.ToDateTime(Console.ReadLine());
        //    Console.Clear();

        //    Console.WriteLine("Lütfen güncellenen 'geri getirme tarihini'ni giriniz: ");
        //    DateTime updateReturn = Convert.ToDateTime(Console.ReadLine());
        //    Console.Clear();

        //    CustomersList(customerManager);
        //    Console.WriteLine("Lütfen kiraladığınız müşterinin 'id'sini giriniz: ");
        //    int updateCustomer = Convert.ToInt32(Console.ReadLine());
        //    Console.Clear();

        //    rentalManager.Update(new Rental { RentalId = updateRental, CarId = updateCar, RentDate = updateRent, ReturnDate = updateReturn, CustomerId = updateCustomer});
        //}

        private static void AddRental(CarManager carManager, RentalManager rentalManager, CustomerManager customerManager, UserManager userManager)
        {

            CarsList(carManager);
            Console.WriteLine("Lütfen kiralamak istediğiniz aracın 'id'sini giriniz: ");
            int addCar = Convert.ToInt32(Console.ReadLine());
            //rentalManager.ChechReturnDate(addCar);
            Console.Clear();

            Console.WriteLine("Lütfen 'kiralanan tarihi' giriniz: ");
            DateTime addRent = Convert.ToDateTime(Console.ReadLine());
            Console.Clear();

            CustomersList(customerManager);
            Console.WriteLine("Lütfen kiraladığınız müşterinin 'id'sini giriniz: ");
            int addCustomer = Convert.ToInt32(Console.ReadLine());  
            rentalManager.Add(new Rental { CarId = addCar, RentDate = addRent, CustomerId = addCustomer , ReturnDate = null });
            
        }

        private static void DeleteUser(UserManager userManager)
        {
            UsersList(userManager);
            Console.WriteLine("Lütfen silmek istediğiniz kullanıcının 'id' sini giriniz: ");
            int deleteUser = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            userManager.Delete(new User { UserId = deleteUser });
        }

        private static void UpdateUser(UserManager userManager)
        {
            UsersList(userManager);
            Console.WriteLine("Lütfen güncellemek istediğiniz kullanıcının 'id'sini giriniz: ");
            int updateUser = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            Console.WriteLine("Kullanıcının 'adı'nı giriniz: ");
            string updateFirstName = Console.ReadLine();

            Console.WriteLine("Kullanıcının 'soyadı'nı giriniz: ");
            string updateLastName = Console.ReadLine();

            Console.WriteLine("Kullanıcının 'email'ini giriniz: ");
            string updateEmail = Console.ReadLine();

            Console.WriteLine("Kullanıcının 'şifre'sini giriniz: ");
            string updatePassword = Console.ReadLine();
            Console.Clear();

            userManager.Update(new User { UserId = updateUser, FirstName = updateFirstName, LastName = updateLastName, Email = updateEmail, Password = updatePassword });
        }

        private static void AddUser(UserManager userManager)
        {
            UsersList(userManager);
            Console.WriteLine("Lütfen eklemek istediğiniz kullanıcının 'adı'nı giriniz: ");
            string addFirstName = Console.ReadLine();

            Console.WriteLine("Lütfen eklemek istediğiniz kullanıcının 'soyadı'nı giriniz: ");
            string addLastName = Console.ReadLine();

            Console.WriteLine("Lütfen eklemek istediğiniz kullanıcının 'email'ini giriniz: ");
            string addEmail = Console.ReadLine();

            Console.WriteLine("Lütfen eklemek istediğiniz kullanıcının 'şifre'sini giriniz: ");
            string addPassword = Console.ReadLine();
            Console.Clear();

            userManager.Add(new User { FirstName = addFirstName, LastName = addLastName, Email = addEmail, Password = addPassword });
        }

        private static void CustomersList(CustomerManager customerManager)
        {
            var result = customerManager.GetAll();

            if (result.Success == true)
            {
                foreach (var customers in result.Data)
                {
                    Console.WriteLine("CustomerId: " + customers.CustomerId + "\n" + "UserId: " + customers.UserId + "\n" + "CompanyName: " + customers.CompanyName + "\n");
                }
            }

            else
            {
                Console.WriteLine(result.Message);
            }
        }

        private static void UsersList(UserManager userManager)
        {
            var result = userManager.GetAll();

            if (result.Success == true)
            {
                foreach (var users in result.Data)
                {
                    Console.WriteLine("UserId: " + users.UserId + "\n" + "UserName: " + users.FirstName + users.LastName + "\n" + "UserEmail: " + users.Email + "\n" + "UserPassword: " + users.Password + "\n");
                }
            }

            else
            {
                Console.WriteLine(result.Message);
            }
        }

        private static void RentalsList(RentalManager rentalManager)
        {
            var result = rentalManager.GetRentalDetails();

            if (result.Success == true)
            {
                foreach (var rentals in result.Data)
                {
                    Console.WriteLine("RentalId: " + rentals.RentalId + "\n" + "CarName: " + rentals.CarName + "\n" + "RentDate: " + rentals.RentDate);
                    Console.WriteLine("ReturnDate: " + rentals.ReturnDate + "\n" + "CustomerName: " + rentals.CustomerName + "\n" + "UserName: " + rentals.UserName + "\n");
                }
            }

            else
            {
                Console.WriteLine(result.Message);
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

            Console.WriteLine("Lütfen güncellemek istediğiniz 'rengin ismini' giriniz: ");
            string updateColorName = Console.ReadLine();
            Console.Clear();

            colorManager.Update(new Color { ColorId = updateColor, ColorName = updateColorName });
        }

        private static void AddColor(ColorManager colorManager)
        {
            ColorsList(colorManager);
            Console.WriteLine("Lütfen eklemek istediğiniz 'rengin ismini' giriniz: ");
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

        private static void UpdateBrand(BrandManager brandManager)
        {
            BrandsList(brandManager);
            Console.WriteLine("Lütfen güncellemek istediğiniz marka 'id' sini giriniz: ");
            int updateBrand = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            Console.WriteLine("Lütfen eklemek istediğiniz 'marka ismini' giriniz: ");
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

        private static void UpdateCar(CarManager carManager, BrandManager brandManager, ColorManager colorManager)
        {
            CarsList(carManager);
            Console.WriteLine("Lütfen güncellemek istediğiniz aracın 'id' sini giriniz: ");
            int updateCar = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            BrandsList(brandManager);
            Console.WriteLine("Aracın 'BrandId'sini giriniz: ");
            int updateBrandId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Aracın 'CarName'ini giriniz");
            string updateCarName = Console.ReadLine();

            ColorsList(colorManager);
            Console.WriteLine("Aracın 'ColorId'sini giriniz: ");
            int updateColorId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Aracın 'Model Year'ını giriniz:");
            int updateModelYear = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Aracın 'Daily Price'ını giriniz:");
            decimal updateDailyPrice = Convert.ToDecimal(Console.ReadLine());

            Console.WriteLine("Aracın 'Description'ını giriniz:");
            string updateDescription = Console.ReadLine();
            Console.Clear();

            carManager.Update(new Car { CarId = updateCar, BrandId = updateBrandId, CarName= updateCarName, ColorId = updateColorId, ModelYear = updateModelYear, DailyPrice = updateDailyPrice, Description = updateDescription });
        }

        private static void AddCar(CarManager carManager, BrandManager brandManager, ColorManager colorManager)
        {
            BrandsList(brandManager);
            Console.WriteLine("Aracın 'BrandId'sini giriniz: ");
            int addBrandId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Aracın 'CarName'ini giriniz: ");
            string addCarName = Console.ReadLine();

            ColorsList(colorManager);
            Console.WriteLine("Aracın 'ColorId'sini giriniz: ");
            int addColorId = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Aracın 'Model Year'ını giriniz: ");
            int addModelYear = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Aracın 'Daily Price'ını giriniz: ");
            decimal addDailyPrice = Convert.ToDecimal(Console.ReadLine());

            Console.WriteLine("Aracın 'Description'ını giriniz: ");
            string addDescription = Console.ReadLine();
            Console.Clear();

            carManager.Add(new Car { BrandId = addBrandId, CarName= addCarName, ColorId = addColorId, ModelYear = addModelYear, DailyPrice = addDailyPrice, Description = addDescription });
        }

        private static void ColorsList(ColorManager colorManager)
        {
            var result = colorManager.GetAll();

            if (result.Success == true)
            {
                foreach (var colors in result.Data)
                {
                    Console.WriteLine(colors.ColorId + " ----- " + colors.ColorName);
                }
            }

            else
            {
                Console.WriteLine(result.Message);
            }
            
        }

        private static void BrandsList(BrandManager brandManager)
        {
            var result = brandManager.GetAll();

            if (result.Success == true)
            {
                foreach (var brands in result.Data)
                {
                    Console.WriteLine(brands.BrandId + " ----- " + brands.BrandName);
                }
            }

            else
            {
                Console.WriteLine(result.Message);
            }
            
        }

        private static void CarsList(CarManager carManager)
        {
            var result = carManager.GetCarDetails();

            if (result.Success == true)
            {
                foreach (var cars in result.Data)
                {
                    Console.WriteLine("CarId: " + cars.CarId + "\nCar Daily Price: " + cars.DailyPrice + "\nCar Color: " + cars.ColorName);
                    Console.WriteLine("CarName: " + cars.CarName + "\nBrandName: " + cars.BrandName + "\n" + "CarDescription: " + cars.Description + "\n");
                }
            }

            else
            {
                Console.WriteLine(result.Message);
            }
            
        }
    }
}
