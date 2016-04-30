using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;
using System.Web.Mvc;

namespace GameStore.WebUI.Controllers
{
    public class GameController : Controller
    {
        private IGameRepository _repository;

        public GameController(IGameRepository repository)
        {
            _repository = repository;
        }

        public ViewResult List()
            => View(_repository.Games);
    }
}