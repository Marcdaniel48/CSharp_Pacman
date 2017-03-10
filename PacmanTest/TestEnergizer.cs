using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Business_Classes;
using System.Collections.Generic;
using System.Collections;

namespace PacmanTest
{
    [TestClass]
    public class TestEnergizer
    {
        GameState myGameState = GameState.Parse("levels.csv");

        Energizer myEnergizer; 

        [TestMethod]
        public void TestCollide()
        {
            
            myEnergizer = new Energizer(myGameState.GhostSquad);
            myEnergizer.Collision += myGameState.Score.incrementScore;
            myEnergizer.Collide();
            Assert.AreEqual(100, myGameState.Score.Score);
            myEnergizer.Collide();
            Assert.AreEqual(200, myGameState.Score.Score);

            foreach(Ghost ghost in myGameState.GhostSquad)
            {
                Assert.AreEqual(GhostState.Scared, ghost.CurrentState);
            }
            
        }
    }
}
