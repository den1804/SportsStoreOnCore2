using Microsoft.AspNetCore.Mvc;
using SportsStoreOnCore2.Models;
using SportsStoreOnCore2.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStoreOnCore2.Controllers
{
    public class ProductController : Controller 
    {
        private IProductRepository repository;
        public int PageSize = 7;

        public ProductController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult List(int productPage = 1)
        {
            return View(new ProductsListViewModel {
                Products = repository.Products
                    .OrderBy(p => p.ProductId)
                    .Skip((productPage - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = repository.Products.Count()
                }
            });
        }
    }
}

