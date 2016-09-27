using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;
using System.Web.Mvc;
using System.Linq;
using GameStore.WebUI.Models;

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
            //return View(_repository.Games
            //    .OrderBy(game => game.GameId)
            //    .Skip((page - 1) * PageSize)
            //    .Take(PageSize));

            GamesListViewModel viewModel = new GamesListViewModel
            {
                Games = _repository.Games
                    .OrderBy(g => g.GameId)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemPerPage = PageSize,
                    TotalItems = _repository.Games.Count()
                }
            };

            return View(viewModel);
        }
    }
}