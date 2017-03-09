using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Business_Classes;
using Microsoft.Xna.Framework;

namespace PacmanTest
{
    [TestClass]
    public class TestGhost
    {
        Ghost myGhost = new Ghost(new GameState(), 6, 4, new Vector2(3,3), GhostState.Chase, "Blue");

        [TestMethod]
        public void TestMethod1()
        {
        }
    }

    [TestClass]
    public class Chase
    {

    }
}
