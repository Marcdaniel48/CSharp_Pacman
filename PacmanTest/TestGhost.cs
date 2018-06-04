using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Business_Classes;
using Microsoft.Xna.Framework;
using System.IO;

namespace PacmanTest
{
    
    [TestClass]
    public class TestGhost
    {
        GameState myGameState = GameState.Parse(File.ReadAllText("Testlevel.csv"));

        [TestMethod]
        public void TestCollisionOnChase()
        {

            // Makes Pacman move toward a ghost, and then checks collision on it.
            // Pacman should lose a life. Also, pacman is moving onto two tiles with pellets on it,
            // so his points should be 20.

            List<Ghost> g = new List<Ghost>();
            int count = 0;
            foreach (Ghost aGhost in myGameState.GhostSquad)
            {

                if (count == 0)
                {
                    g.Add(aGhost);
                    count++;
                }
            }

            myGameState.Pacman.Move(Direction.Right);
            myGameState.Pacman.Move(Direction.Right);
            myGameState.Pacman.Move(Direction.Right);

            Assert.AreEqual(4, g[0].Position.X);
            Assert.AreEqual(1, g[0].Position.Y);


            Assert.AreEqual(1, myGameState.Score.Lives);
            Assert.AreEqual(20, myGameState.Score.Score);

        }

        [TestMethod]
        public void TestCollisionOnScared()
        {

            // Makes Pacman move toward a ghost in scared state, and then checks collision on it.
            // Score should be incremented by 200. Also, pacman is moving onto two tiles with pellets on it,
            // so his points should be 220.

            List<Ghost> g = new List<Ghost>();
            int count = 0;
            foreach (Ghost aGhost in myGameState.GhostSquad)
            {

                if (count == 0)
                {
                    g.Add(aGhost);
                    count++;
                }
            }

            g[0].ChangeState(GhostState.Scared);

            myGameState.Pacman.Move(Direction.Right);
            myGameState.Pacman.Move(Direction.Right);
            myGameState.Pacman.Move(Direction.Right);

            Assert.AreEqual(4, g[0].Position.X);
            Assert.AreEqual(1, g[0].Position.Y);


            Assert.AreEqual(2, myGameState.Score.Lives);
            Assert.AreEqual(220, myGameState.Score.Score);

        }

        [TestMethod]
        public void TestReset()
        {
            List<Ghost> g = new List<Ghost>();
            int count = 0;
            // We only needed 1 ghost to test collision with pacman, so we fetched the first ghost that is outside of pen
            foreach (Ghost aGhost in myGameState.GhostSquad)
            {

                if (count == 0)
                {
                    g.Add(aGhost);
                    count++;
                }
            }

            Assert.AreEqual(new Vector2(4, 1), g[0].Position);
            g[0].Move();
            Assert.AreEqual(new Vector2(3, 1), g[0].Position);
            g[0].Move();
            Assert.AreEqual(new Vector2(2, 1), g[0].Position);
            g[0].Move();
            Assert.AreEqual(new Vector2(4, 1), g[0].Position); // Pacman dies, so position back at start
        }

        [TestMethod]
        public void TestChangeState()
        {
            myGameState.GhostSquad.ScaredGhosts(); // State of all ghosts set to scared --> ScaredGhosts() call ChangeState()

            foreach(Ghost myGhost in myGameState.GhostSquad)
            {
                Assert.AreEqual(GhostState.Scared, myGhost.CurrentState);
            }
        }
    }
}
