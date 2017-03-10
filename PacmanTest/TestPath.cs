using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Business_Classes;
using Microsoft.Xna.Framework;

namespace PacmanTest
{
    [TestClass]
    public class TestPath
    {
        
        GameState g = GameState.Parse("levels.csv");
        Path myPath = new Path(4, 5, new Pellet());

        [TestMethod]
        public void TestCanEnter()
        {
            Assert.AreEqual(true, myPath.CanEnter());
        }

        [TestMethod]
        public void TestCollide()
        {
            myPath.Member.Collision += g.Score.incrementScore;
            myPath.Collide();
            Assert.AreEqual(10, g.Score.Score);
        }

        [TestMethod]
        public void TestIsEmpty()
        {
            Assert.AreEqual(false, myPath.IsEmpty());
            myPath.Member = null;
            Assert.AreEqual(true, myPath.IsEmpty());
        }

        [TestMethod]
        public void TestGetDistance()
        {
            Vector2 myVect = new Vector2(4, 5);
            Assert.AreEqual(0, myPath.GetDistance(myVect));
        }

        [TestMethod]
        public void TestPropertyMember()
        {
            Assert.AreEqual(myPath.Member, myPath.Member);
        }

        [TestMethod]
        public void TestPropertyPosition()
        {
            Vector2 vect = new Vector2(4, 5);
            Assert.AreEqual(vect, myPath.Position);
            Vector2 vect2 = new Vector2(3, 5);
            Assert.AreNotEqual(myPath.Position, vect2);
        }
    }
}
