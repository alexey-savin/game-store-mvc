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
    }
}
