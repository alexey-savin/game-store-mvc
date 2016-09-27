using GameStore.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IGameRepository _repository;

        public NavController(IGameRepository repo)
        {
            _repository = repo;
        }

        public PartialViewResult Menu()
        {
            IEnumerable<string> categories = _repository.Games
                .Select(g => g.Category)
                .Distinct()
                .OrderBy(x => x);

            return PartialView(categories);
        }
    }
}