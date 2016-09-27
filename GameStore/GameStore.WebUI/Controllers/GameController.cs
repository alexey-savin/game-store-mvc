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

        public ViewResult List(string category, int page = 1)
        {
            GamesListViewModel viewModel = new GamesListViewModel
            {
                Games = _repository.Games
                    .Where(p => category == null || p.Category == category)
                    .OrderBy(g => g.GameId)
                    .Skip((page - 1) * PageSize)
                    .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemPerPage = PageSize,
                    TotalItems = category == null ? 
                        _repository.Games.Count() :
                        _repository.Games.Where(g => g.Category == category).Count()
                },
                CurrentCategory = category
            };

            return View(viewModel);
        }
    }
}