using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Business_Classes;

namespace PacmanTest
{
    [TestClass]
    public class TestPellet
    {
        GameState myGameState = GameState.Parse("levels.csv");

        Pellet myPellet = new Pellet();
        
        [TestMethod]
        public void TestPropertyPoints()
        {
            Assert.AreEqual(10, myPellet.Points);
            myPellet.Points = 5;
            Assert.AreEqual(5, myPellet.Points);
        }

        [TestMethod]
        public void TestCollide()
        {
            myPellet.Collision += myGameState.Score.incrementScore;
            myPellet.Collide();
            Assert.AreEqual(10, myGameState.Score.Score);
        }
    }
}
