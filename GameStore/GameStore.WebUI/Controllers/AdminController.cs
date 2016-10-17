using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;
using System.Linq;
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

        public ViewResult Edit(int gameId)
        {
            Game game = _repository.Games
                .FirstOrDefault(g => g.GameId == gameId);

            return View(game);
        }
    }
}