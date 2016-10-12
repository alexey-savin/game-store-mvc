using GameStore.Domain.Abstract;
using System.Web.Mvc;

namespace GameStore.WebUI.Controllers
{
    public class AdminController : Controller
    {
        IGameRepository _repository;

        public AdminController(IGameRepository repo)
        {
            _repository = repo;
        }

        public ViewResult Index()
        {
            return View(_repository.Games);
        }
    }
}