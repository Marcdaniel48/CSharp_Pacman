using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business_Classes;
using Microsoft.Xna.Framework;
using System.Timers;
using System.Collections;

namespace Business_Classes
{
    //Event hanlder for the GameOver event
    public delegate void gameOverHandler();

    /// <summary>
    /// Represents the score and lives object of the pacman game, that
    /// stores the players current amount of lives and score. 
    /// </summary>
    public class ScoreAndLives
    {
        private int points;
        private int lives;

        /// <summary>
        /// Constructor initializes the instances of score and lives. It also 
        /// registers the appropriate event handlers within the class.
        /// </summary>
        /// <param name="state">The current state of the game</param>
        public ScoreAndLives(GameState state)
        {
            lives = 2;
            state.Maze.PacmanWon += gameWon;
            GameOver += gameLost;
        }

        public event gameOverHandler GameOver;

        protected void OnGameOver()
        {
            if(GameOver != null)
            {
                //need to determine delegate input
                GameOver();
            }
        }

        public int Lives
        {
            get { return lives; }
            set { lives = value; }
        }

        public int Score
        {
            get { return points; }
            set { points = value; }
        }

 
      /// <summary>
      /// Event handler that determines what happens when pacman dies
      /// </summary>
        public void deadPacman()
        {
            if (lives >= 1)
            {
                lives -= 1;
            }
            else if (lives < 1)
            {
                OnGameOver();
            }
        }

        /// <summary>
        /// Event handler that increments the score when pacman collides with
        /// a pellet, energizer or frightened ghost.
        /// </summary>
        /// <param name="collide">Represents an IColliable object(ghost,pellet,energizer)</param>
        public void incrementScore(ICollidable collide)
        {
            Score += collide.Points;
        }

        //Event hanlder that determines what happens when pacman wins
        public void gameWon()
        {
            Score += 5000;
        }

        //Event hanlder that determines what happens when pacman loses
        public void gameLost()
        {
            Score -= Score+1;
        }
        
    }

}
