using Microsoft.AspNetCore.Mvc;
using Shared.Extensions.Pagination;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Services.Repositorys.Abstractions;
using WebMVC.ViewModels.Pagination;
using WebMVC.ViewModels.Products;
using WebMVC.ViewModels.Products.Abstraction;
using WebMVC.ViewModels.Products.ProductSideBar.Abstraction;
using WebMVC.ViewModels.Products.ProductSideBar.Filterize;
using WebMVC.ViewModels.Products.ProductSideBar.Filterize.Abstraction;
using WebMVC.ViewModels.ProductSideBar;
using WebMVC.ViewModels.Reviews;
using WebMVC.ViewModels.SysRequirement;

namespace WebMVC.Controllers
{
    [Route("/product")]
    public class ProductController : Controller
    {
        private readonly ICatalogRepository _catalogRepository;
        private SideBar _sideBar;

        public ProductController(ICatalogRepository catalogRepository)
        {
            _catalogRepository = catalogRepository;
        }

        [HttpGet]
        [Route("/products")]
        public async Task<IActionResult> Index(int pageIndex = 0, int pageSize = 30, IEnumerable<string> tags = null, IEnumerable<string> categories = null, int shortType = 0)
        {
            var skip = pageIndex * pageSize;
            var take = pageSize;

            var model = new ProductsPageGridModel()
            {
                Products = new List<ProductItem>()
                {
                    new ProductItem() { Category = "FPS", DiscountedPrice = 0, ID = "csgo", ImageUrl="https://via.placeholder.com/470x580", Name="Counter Strike: Global Offensive", Price = 3000},
                    new ProductItem() { Category = "FPS", DiscountedPrice = 0, ID = "bfvde", ImageUrl="https://via.placeholder.com/470x580", Name="BattleField V Deluxe Edition", Price = 1000},
                    new ProductItem() { Category = "Open World", DiscountedPrice = 0, ID = "gtav", ImageUrl="https://via.placeholder.com/470x580", Name="Grand Theft Auto V", Price = 6000},
                    new ProductItem() { Category = "FPS", DiscountedPrice = 0, ID = "codmw", ImageUrl="https://via.placeholder.com/470x580", Name="Call Of Duty: Modern Warfare", Price = 13000},
                },
                SideBar = _sideBar ?? CreateSideBar(),
                Pagination = new PaginationInfo()
                {
                    ActualPage = 0,
                    MaxPageNumber = 5,
                    ItemsPerPage = 20,
                    TotalItemsCount = 4
                }
            };

            //var paginatedData = default(PaginationViewModel<ProductItem>);

            //if (tags == default && categories == default)
            //{
            //    paginatedData = await _catalogRepository.GetItems(skip,take);
            //}
            //else
            //{
            //    paginatedData = await _catalogRepository.FilterItems(skip,take,tags,categories);
            //}

            //if (paginatedData != default)
            //{
            //    var maxPageSize = (int)Math.Ceiling(paginatedData.TotalItemsCount / (double)pageSize);

            //    model = new ProductsPageGridModel()
            //    {
            //        Products = paginatedData.Data.ToList(),
            //        Pagination = new PaginationInfo()
            //        {
            //            ActualPage = pageIndex,
            //            ItemsPerPage = pageSize,
            //            MaxPageNumber = maxPageSize,
            //            TotalItemsCount = paginatedData.TotalItemsCount,
            //            Next = pageIndex < maxPageSize ? "" : " inactive",
            //            Previous = pageIndex > 0 ? "" : " inactive"
            //        }
            //    }; 
            //}

            return View(model);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Product(string id)
        {
            var product = new DetailProduct()
            {
                Product = new Product()
                {
                    Categorys = new List<string>()
                                {
                                    "FPS",
                                    "Akció",
                                },
                    DiscountedPrice = 0,
                    Price = 3000,
                    HelpActivateKey = "<div class='row'>Ezt a terméket a <a href='steamcommunity.com'>Steamen</a> tudod aktíválni a saját fiókodban</div>",
                    Description = "<div class='row'>A Counter-Strike: Global Offensive online csapatalapú first-person shooter, amelyet a Valve Corporation és a Hidden Path Entertainment fejleszt, akik korábban a Counter-Strike: Source frissítéseiért is feleltek.</div>",
                    ShortDescription = "A CS széria eddigi legsikeresebb tagja",
                    ID = "csgo",
                    ImageUrls = new List<string>()
                                {
                                    
                                },
                    Tags = new List<string>()
                                {
                                    "Akció",
                                    "FPS",
                                    "CS",
                                    "CSGO",
                                    "Steam"
                                },
                    Name = "Counter-Strike: Global Offensive",
                    Reviews = new List<Review>()
                            {
                                new Review()
                                {
                                    
                                }
                            },
                    SystemRequirement = new SystemRequirement()
                    {
                        
                    }
                },
                RecomendedProducts = new List<ProductItem>()
                {
                    new ProductItem() { Category = "FPS", DiscountedPrice = 0, ID = "bfvde", ImageUrl="https://via.placeholder.com/80x101", Name="BattleField V Deluxe Edition", Price = 1000},
                    new ProductItem() { Category = "Open World", DiscountedPrice = 0, ID = "gtav", ImageUrl="https://via.placeholder.com/80x101", Name="Grand Theft Auto V", Price = 6000},
                    new ProductItem() { Category = "FPS", DiscountedPrice = 0, ID = "codmw", ImageUrl="https://via.placeholder.com/80x101", Name="Call Of Duty: Modern Warfare", Price = 13000},
                }
            };
            return View(product);
        }

        private SideBar CreateSideBar()
        {
            _sideBar = new SideBar()
            {
                CategoryTitle = "Kategóriák",
                CategoryData = new List<ISideBarCategoryContent>()
                {
                    new SideBarCategoryContent() { Title = "FPS", Data = "1" },
                    new SideBarCategoryContent() { Title = "Akctió", Data = "2" },
                    new SideBarCategoryContent() { Title = "TPS",Data = "3" },
                },
                RefineByTitle = "Szűrés",
                RefineByData = new List<ISideBarRefineByContent>()
                {

                },
                MinPrice = 0,
                MaxPrice = 20000
            };

            return _sideBar;
        }
    }
}