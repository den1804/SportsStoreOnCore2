using Microsoft.AspNetCore.Mvc;
using System.Linq;
using SportsStoreOnCore2.Models;

namespace SportsStoreOnCore2.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private IProductRepository repository;

        public NavigationMenuViewComponent (IProductRepository repo)
        {
            repository = repo;
        }

        public IViewComponentResult Invoke()
        {
            return View(repository.Products
                .Select(x => x.Category)
                .Distinct()
                .OrderBy(x => x));
        }
    }
}
