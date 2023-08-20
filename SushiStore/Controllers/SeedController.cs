using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SushiStore.Models;

namespace SushiStore.Controllers
{
    public class SeedController : Controller
    {
        private readonly ApplicationContext _context;

        public SeedController(ApplicationContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            ViewBag.Count = _context.Products.Count();
            return View(_context.Products.Include(e => e.Category).OrderBy(e => e.Id).Take(20));
        }
        [HttpPost]
        public IActionResult CreateSeedData(int count)
        {
            ClearData();
            if (count > 0)
            {
                _context.Database.SetCommandTimeout(TimeSpan.FromMinutes(10));
                _context.Database.ExecuteSqlRaw("DROP PROCEDURE IF EXISTS CreateSeedData");
                _context.Database.ExecuteSqlRaw($@"
                CREATE PROCEDURE CreateSeedData 
                @RowCount decimal
                AS
                BEGIN
                SET NOCOUNT ON
                DECLARE @i INT = 0;
                DECLARE @catId INT;
                DECLARE @CatCount INT = @RowCount / 10;
                
                DECLARE @rprice DECIMAL(5,2);
                BEGIN TRANSACTION
                WHILE @i < @CatCount
                BEGIN 
                INSERT INTO Categories (Name,Description)
                VALUES (CONCAT('Category-',@i),
                'Test Data Category');
                SET @catId = SCOPE_IDENTITY();
                DECLARE @j INT = 1;
                WHILE @j <= 10
                BEGIN
                SET @rprice = RAND() * (500-5+1);
                
                INSERT INTO Products (Name,CategoryId,Detail,RetailPrice,Sostav)
                VALUES (CONCAT('Product',@i,'-',@j),@catId,@detail,@rprice,@sostav)
                SET @j = @j + 1
                END
                SET @i = @i + 1
                END
                COMMIT
                END");
                _context.Database.BeginTransaction();
                _context.Database.ExecuteSqlRaw($"EXEC CreateSeedData @RowCount = {count}");
                _context.Database.CommitTransaction();
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult ClearData()
        {
            _context.Database.SetCommandTimeout(TimeSpan.FromMinutes(10));
            _context.Database.BeginTransaction();
            _context.Database.ExecuteSqlRaw("DELETE FROM Orders");
            _context.Database.ExecuteSqlRaw("DELETE FROM Categories");
            _context.Database.CommitTransaction();
            return RedirectToAction(nameof(Index));
        }
            [HttpPost]
            public IActionResult CreateProductionData()
            {
                ClearData();
                _context.Categories.AddRange
                    (
                        new Category
                        {
                            Name = "Осидзуси",
                            Description = " суши в виде брусочков, прессованных в деревянной форме.",
                            Products = new Product[]
                        {
                        new Product { Name = "Калифорния", Sostav = "обваляна в икре летучей рыбы",
                        Detail = "Просто объедение", RetailPrice = 705},
                         new Product { Name = "Филадельфия", Sostav = "сверху покрыта куском лосося",
                        Detail = "Пальчики оближешь", RetailPrice = 905},
                          new Product { Name = "Аляска", Sostav = " роллы готовят с крабовым мясом и посыпают кунжутом.",
                        Detail = "Блюдо от шеф-повара", RetailPrice = 1205},
                               new Product { Name = "Канада", Sostav = "роллы с обычной начинкой: лосось, огурцы, сливочный сыр + водоросли нори.",
                            Detail = "Акция!!!2 по цене 1", RetailPrice = 550},
                        }
                        },
                         new Category
                         {
                             Name = "Маки",
                             Description = "Рис и начинку оборачивают водорослями",
                             Products = new Product[]
                        {
                        new Product { Name = "Нигири", Sostav = "суши с кусочком сырой рыбы",
                        Detail = "Блюдо от шеф-повара", RetailPrice = 770},
                         new Product { Name = "Саке-маки", Sostav = "роллы с сырой рыбой - лососем.",
                        Detail = "Акция!!!2 по цене 1", RetailPrice = 790},
                          new Product { Name = "Каппа маки", Sostav = "не содержат мясных ингредиентов и морепродуктов: только рис и огурцы. ",
                        Detail = "Просто объедение", RetailPrice = 1270},
                           new Product { Name = "Текке-маки", Sostav = "это острая закуска, представляющая собой каппа-маки, но с острой приправой васаби",
                        Detail = "Пальчики оближешь", RetailPrice = 750},
                        }
                         },
                          new Category
                          {
                              Name = "Макидзуси",
                              Description = "суши в виде цилиндра, завернутого в водоросли с рисом и начинкой",
                              Products = new Product[]
                        {
                        new Product { Name = "Темпура", Sostav = "Рыба,Рис,Водоросли",
                        Detail = "Пальчики оближешь", RetailPrice = 700},
                         new Product { Name = "Текке-маки", Sostav = "Рис и морепродукты",
                        Detail = "Блюдо от шеф-повара", RetailPrice = 900},
                          new Product { Name = "Тем-Теке", Sostav = "Рыба,Рис,",
                        Detail = "Просто объедение", RetailPrice = 1200},
                           new Product { Name = "Теке-Тема", Sostav = "Кусочек Рыбы с листиком салата",
                        Detail = "Акция!!!2 по цене 1", RetailPrice = 500},
                        }
                          }

                    );
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
        }
    }
