using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;
using System.Web.Mvc;
using System.Linq;

namespace GameStore.WebUI.Controllers
{
    public class GameController : Controller
    {
        public int PageSize = 4;

        private IGameRepository _repository;

        public GameController(IGameRepository repository)
        {
            _repository = repository;
        }

        public ViewResult List(int page = 1)
        {
            return View(_repository.Games
                .OrderBy(game => game.GameId)
                .Skip((page - 1) * PageSize)
                .Take(PageSize));
        }
    }
}