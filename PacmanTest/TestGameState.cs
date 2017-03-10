using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Business_Classes;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace PacmanTest
{
    [TestClass]
    public class TestGameState
    {

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }




        GameState myGameState = GameState.Parse("TestLevel.csv");

        [TestMethod]
        public void TestMaze()
        {
            // Using Maze's indexer to check the type of tile at [x, y]
            Assert.AreEqual(new Wall(0, 0).GetType(), myGameState.Maze[0, 0].GetType()); // At [0,0] is w
            Assert.AreEqual(new Path(0, 0, null).GetType(), myGameState.Maze[1, 2].GetType()); // At [1,2] is p
            Assert.AreNotEqual(new Wall(0, 0).GetType(), myGameState.Maze[5, 1].GetType()); // At [5,1] is e

            // Checking size of Maze
            Assert.AreEqual(12, myGameState.Maze.Size);

            // Checking amount of available tiles at position [x,y]
            Assert.AreEqual(0, myGameState.Maze.GetAvailableNeighbours(new Vector2(1, 5), Direction.Left).Count);

            // Testing CheckMembersLeft() method

        }

        [TestMethod]
        public void TestPacman()
        {
            // Testing the Move method of Pacman, by checking Pacman's coordinates after moving
            Assert.AreEqual(1, myGameState.Pacman.Position.X);
            Assert.AreEqual(1, myGameState.Pacman.Position.Y);
            myGameState.Pacman.Move(Direction.Right);
            Assert.AreEqual(2, myGameState.Pacman.Position.X);
            Assert.AreEqual(1, myGameState.Pacman.Position.Y);
            myGameState.Pacman.Move(Direction.Down);
            Assert.AreEqual(2, myGameState.Pacman.Position.X);
            Assert.AreEqual(1, myGameState.Pacman.Position.Y);
            myGameState.Pacman.Move(Direction.Right);
            myGameState.Pacman.Move(Direction.Right);
            myGameState.Pacman.Move(Direction.Right);
            myGameState.Pacman.Move(Direction.Down);
            Assert.AreEqual(5, myGameState.Pacman.Position.X);
            Assert.AreEqual(2, myGameState.Pacman.Position.Y);

            // Testing CheckCollision method of Pacman
            myGameState.Pacman.Position = new Vector2(4, 4);
            Assert.AreEqual(typeof(Pellet), myGameState.Maze[5, 4].Member.GetType());
            myGameState.Pacman.Move(Direction.Down);
            Assert.AreEqual(null, myGameState.Maze[5, 4].Member);
        }
    }
}
