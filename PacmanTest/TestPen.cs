using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Business_Classes;

namespace PacmanTest
{
    /// <summary>
    /// Test if ghost(s) in pen are released and set to a "public static releasedPosition" position.
    /// </summary>
    [TestClass]
    public class TestPen
    {
        GameState myGameState = GameState.Parse("TestLevel.csv");
        
        [TestMethod]
        public void TestReleaseGhost()
        {
            foreach(Ghost aGhost in myGameState.GhostSquad)
            {
                aGhost.ChangeState(GhostState.Released);
                Assert.AreEqual(Ghost.releasedPosition, aGhost.Position);
            }
        }
    }
}
