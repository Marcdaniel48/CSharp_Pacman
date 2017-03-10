using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Business_Classes;
using Microsoft.Xna.Framework;
namespace PacmanTest
{
    /// <summary>
    /// Tests the Wall class.
    /// </summary>
    [TestClass]
    public class TestWall
    {
        Wall myWall = new Wall(5, 8);

        [TestMethod]
        public void TestCanEnter()
        {
            Assert.AreEqual(false, myWall.CanEnter());
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException),"Not implemented.")]
        public void TestCollide()
        {
            myWall.Collide();
        }

        [TestMethod]
        public void TestIsEmpty()
        {
            Assert.AreEqual(true, myWall.IsEmpty());
        }

        [TestMethod]
        public void TestGetDistance()
        {
            Vector2 myVect = new Vector2(3,11);
            Assert.AreEqual(Vector2.Distance(myWall.Position,myVect), myWall.GetDistance(myVect));
        }
        
        /*[TestMethod]
        public void TestPropertyMember()
        {
            
        }*/

        [TestMethod]
        public void TestPropertyPosition()
        {
            Vector2 vect = new Vector2(5, 8);
            Assert.AreEqual(vect, myWall.Position);
            Vector2 vect2 = new Vector2(5, 5);
            Assert.AreNotEqual(myWall.Position, vect2);
        }
    }
}
