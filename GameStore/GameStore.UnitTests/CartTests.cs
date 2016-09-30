using GameStore.Domain.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.UnitTests
{
    [TestClass]
    public class CartTests
    {
        [TestMethod]
        public void Can_Add_New_Lines()
        {
            // Arrange
            Game g1 = new Game { GameId = 1, Name = "Game-1" };
            Game g2 = new Game { GameId = 2, Name = "Game-2" };

            Cart cart = new Cart();

            // Act
            cart.AddItem(g1, 1);
            cart.AddItem(g2, 1);
            List<CartLine> results = cart.Lines.ToList();

            // Assert
            Assert.AreEqual(2, results.Count);
            Assert.AreEqual(g1, results[0].Game);
            Assert.AreEqual(g2, results[1].Game);
        }

        [TestMethod]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            // Arrange
            Game g1 = new Game { GameId = 1, Name = "Game-1" };
            Game g2 = new Game { GameId = 2, Name = "Game-2" };

            Cart cart = new Cart();

            // Act
            cart.AddItem(g1, 1);
            cart.AddItem(g2, 1);
            cart.AddItem(g1, 5);
            List<CartLine> results = cart.Lines.OrderBy(c => c.Game.GameId).ToList();


            // Assert
            Assert.AreEqual(2, results.Count);
            Assert.AreEqual(6, results[0].Quantity);
            Assert.AreEqual(1, results[1].Quantity);
        }

        [TestMethod]
        public void Can_Remove_Line()
        {
            // Arrange
            Game g1 = new Game { GameId = 1, Name = "Game-1" };
            Game g2 = new Game { GameId = 2, Name = "Game-2" };
            Game g3 = new Game { GameId = 3, Name = "Game-3" };

            Cart cart = new Cart();

            cart.AddItem(g1, 1);
            cart.AddItem(g2, 4);
            cart.AddItem(g3, 2);
            cart.AddItem(g2, 1);

            // Act
            cart.RemoveLine(g2);

            // Assert
            Assert.AreEqual(0, cart.Lines.Where(c => c.Game == g2).Count());
            Assert.AreEqual(2, cart.Lines.Count());
        }

        [TestMethod]
        public void Calculate_Cart_Total()
        {
            // Arrange
            Game g1 = new Game { GameId = 1, Name = "Game-1", Price=100 };
            Game g2 = new Game { GameId = 2, Name = "Game-2", Price = 55 };

            Cart cart = new Cart();

            // Act
            cart.AddItem(g1, 1);
            cart.AddItem(g2, 1);
            cart.AddItem(g1, 5);

            decimal result = cart.ComputeTotalValue();

            // Assert
            Assert.AreEqual(655, result);
        }

        [TestMethod]
        public void Can_Clear_Contents()
        {
            // Arrange
            Game g1 = new Game { GameId = 1, Name = "Game-1", Price = 100 };
            Game g2 = new Game { GameId = 2, Name = "Game-2", Price = 55 };

            Cart cart = new Cart();

            // Act
            cart.AddItem(g1, 1);
            cart.AddItem(g2, 1);
            cart.AddItem(g1, 5);

            cart.Clear();

            // Assert
            Assert.AreEqual(0, cart.Lines.Count());
        }
    }
}
