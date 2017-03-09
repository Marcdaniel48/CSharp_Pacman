using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Business_Classes;

namespace PacmanTest
{
    [TestClass]
    public class TestPellet
    {
        Pellet myPellet = new Pellet();
        
        [TestMethod]
        public void TestPropertyPoints()
        {
            Assert.AreEqual(10, myPellet.Points);
            myPellet.Points = 5;
            Assert.AreEqual(5, myPellet.Points);
        }

        /*[TestMethod]
        public void TestCollide()
        {
            myPellet.Collide();
        }*/
    }
}
