using GameStore.Domain.Abstract;
using GameStore.Domain.Entities;
using GameStore.WebUI.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.UnitTests
{
    [TestClass]
    public class AdminTests
    {
        [TestMethod]
        public void Index_Contains_All_Games()
        {
            // Arrange
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<Game>
            {
                new Game {GameId = 1, Name="G-1" },
                new Game {GameId =2, Name="G-2" },
                new Game {GameId = 3, Name="G-3" },
                new Game {GameId =4, Name="G-4" },
                new Game {GameId=5,Name="G-5" }
            });

            AdminController controller = new AdminController(mock.Object);

            // Act
            List<Game> result = ((IEnumerable<Game>)controller.Index().ViewData.Model).ToList();

            // Assert
            Assert.AreEqual(5, result.Count);
            Assert.AreEqual("G-1", result[0].Name);
            Assert.AreEqual("G-2", result[1].Name);
            Assert.AreEqual("G-3", result[2].Name);
        }

        [TestMethod]
        public void Can_Edit_Game()
        {
            // Arrange
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<Game>
            {
                new Game {GameId=1, Name="G-1" },
                new Game { GameId = 2, Name="G-2"},
                new Game {GameId=3, Name="G-3" },
                new Game { GameId = 4, Name="G-4"},
                new Game {GameId=5, Name="G-5" },
            });

            AdminController controller = new AdminController(mock.Object);

            // Act
            Game g1 = controller.Edit(1).ViewData.Model as Game;
            Game g2 = controller.Edit(2).ViewData.Model as Game;
            Game g3 = controller.Edit(3).ViewData.Model as Game;

            // Assert
            Assert.AreEqual(1, g1.GameId);
            Assert.AreEqual(2, g2.GameId);
            Assert.AreEqual(3, g3.GameId);
        }

        [TestMethod]
        public void Cannot_Edit_NonExistent_Game()
        {
            // Arrange
            Mock<IGameRepository> mock = new Mock<IGameRepository>();
            mock.Setup(m => m.Games).Returns(new List<Game> {
                 new Game {GameId=1, Name="G-1" },
                new Game { GameId = 2, Name="G-2"},
                new Game {GameId=3, Name="G-3" },
                new Game { GameId = 4, Name="G-4"},
                new Game {GameId=5, Name="G-5" },
            });

            AdminController controller = new AdminController(mock.Object);

            // Act
            Game result = controller.Edit(6).ViewData.Model as Game;

            // Assert
            Assert.IsNull(result);
        }
    }
}
