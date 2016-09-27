using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;
using System.Collections.Generic;
using GameStore.WebUI.Controllers;
using System.Linq;
using System.Web.Mvc;
using GameStore.WebUI.Models;
using GameStore.WebUI.HtmlHelpers;

namespace GameStore.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Can_Paginate()
        {
            // Организация (arrange)
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<Game>
            {
                new Game { GameId = 1, Name = "Игра1"},
                new Game { GameId = 2, Name = "Игра2"},
                new Game { GameId = 3, Name = "Игра3"},
                new Game { GameId = 4, Name = "Игра4"},
                new Game { GameId = 5, Name = "Игра5"}
            });

            GameController controller = new GameController(mock.Object);
            controller.PageSize = 3;

            // Действие (act)
            GamesListViewModel result = (GamesListViewModel)controller.List(null, 2).Model;

            // Утверждение (assert)
            List<Game> games = result.Games.ToList();
            Assert.IsTrue(games.Count == 2);
            Assert.AreEqual(games[0].Name, "Игра4");
            Assert.AreEqual(games[1].Name, "Игра5");
        }

        [TestMethod]
        public void Can_Generate_Page_Links()
        {
            // arrange
            HtmlHelper myHelper = null;

            PagingInfo pagingInfo = new PagingInfo
            {
                CurrentPage = 2,
                TotalItems = 28,
                ItemPerPage = 10
            };

            Func<int, string> pageUrlDelegate = i => "Page" + i;

            // act
            MvcHtmlString result = myHelper.PageLinks(pagingInfo, pageUrlDelegate);

            // assert
            Assert.AreEqual(@"<a class=""btn btn-default"" href=""Page1"">1</a>"
                + @"<a class=""btn btn-default btn-primary selected"" href=""Page2"">2</a>"
                + @"<a class=""btn btn-default"" href=""Page3"">3</a>",
                result.ToString());
        }

        [TestMethod]
        public void Can_Send_Pagination_View_Model()
        {
            // Arrange
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<Game>
            {
                new Game { GameId = 1, Name = "Игра1"},
                new Game { GameId = 2, Name = "Игра2"},
                new Game { GameId = 3, Name = "Игра3"},
                new Game { GameId = 4, Name = "Игра4"},
                new Game { GameId = 5, Name = "Игра5"}
            });

            GameController controller = new GameController(mock.Object);
            controller.PageSize = 3;

            // Act
            GamesListViewModel result = (GamesListViewModel)controller.List(null, 2).Model;

            // Assert
            PagingInfo pageInfo = result.PagingInfo;

            Assert.AreEqual(2, pageInfo.CurrentPage);
            Assert.AreEqual(3, pageInfo.ItemPerPage);
            Assert.AreEqual(5, pageInfo.TotalItems);
            Assert.AreEqual(2, pageInfo.TotalPages);
        }

        [TestMethod]
        public void Can_Filter_Games()
        {
            // Arrange
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<Game>
            {
                new Game { GameId = 1, Name = "Игра1", Category="Cat1"},
                new Game { GameId = 2, Name = "Игра2", Category="Cat2"},
                new Game { GameId = 3, Name = "Игра3", Category="Cat1"},
                new Game { GameId = 4, Name = "Игра4", Category="Cat2"},
                new Game { GameId = 5, Name = "Игра5", Category="Cat3"}
            });

            GameController controller = new GameController(mock.Object);
            controller.PageSize = 3;

            // Act
            List<Game> result = ((GamesListViewModel)controller.List("Cat2", 1).Model).Games.ToList();

            // Assert
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result[0].Name == "Игра2" && result[0].Category == "Cat2");
            Assert.IsTrue(result[1].Name == "Игра4" && result[1].Category == "Cat2");
        }

        [TestMethod]
        public void Can_Create_Categories()
        {
            // Arrange
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<Game> {
                new Game { GameId = 1, Name = "Игра1", Category="Симулятор"},
                new Game { GameId = 2, Name = "Игра2", Category="Симулятор"},
                new Game { GameId = 3, Name = "Игра3", Category="Шутер"},
                new Game { GameId = 4, Name = "Игра4", Category="RPG"},
            });

            NavController nav = new NavController(mock.Object);

            // Act
            List<string> result = ((IEnumerable<string>)nav.Menu().Model).ToList();

            // Assert
            Assert.AreEqual(3, result.Count, 3);
            Assert.AreEqual("RPG", result[0]);
            Assert.AreEqual("Симулятор", result[1]);
            Assert.AreEqual("Шутер", result[2]);
        }

        [TestMethod]
        public void Indicates_Selected_Category()
        {
            // Arrange
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new Game[] {
                new Game { GameId = 1, Name ="Game-1", Category = "Simulator" },
                new Game {GameId=2, Name="Game-2", Category="Shooter" }
            });

            NavController nav = new NavController(mock.Object);
            string categoryToSelect = "Shooter";

            // Act
            string result = nav.Menu(categoryToSelect).ViewBag.SelectedCategory;

            // Assert
            Assert.AreEqual(categoryToSelect, result);
        }

        [TestMethod]
        public void Generate_Category_Specific_Game_Count()
        {
            // Arrange
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new Game[] 
            {
                new Game { GameId = 1, Name = "Game1", Category="Cat1"},
                new Game { GameId = 2, Name = "Game2", Category="Cat2"},
                new Game { GameId = 3, Name = "Game3", Category="Cat1"},
                new Game { GameId = 4, Name = "Game4", Category="Cat2"},
                new Game { GameId = 5, Name = "Game5", Category="Cat3"}
            });

            GameController controller = new GameController(mock.Object);
            controller.PageSize = 3;

            // Act
            int res1 = ((GamesListViewModel)controller.List("Cat1").Model).PagingInfo.TotalItems;
            int res2 = ((GamesListViewModel)controller.List("Cat2").Model).PagingInfo.TotalItems;
            int res3 = ((GamesListViewModel)controller.List("Cat3").Model).PagingInfo.TotalItems;
            int resAll = ((GamesListViewModel)controller.List(null).Model).PagingInfo.TotalItems;

            // Assert
            Assert.AreEqual(2, res1);
            Assert.AreEqual(2, res2);
            Assert.AreEqual(1, res3);
            Assert.AreEqual(5, resAll);
        }
    }
}
