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
    public delegate void gameOverHandler();
    public class ScoreAndLives
    {
        private int points;
        private int lives;

        //need to create a delegate
        public ScoreAndLives(GameState state)
        {
            points = state.Score.Score;
            lives = state.Score.Lives;

            
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

 
        //EVENT HANDLERS
      
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

        public void incrementScore(ICollidable collide)
        {   
                Score += collide.Points;
        }

        public void gameWon()
        {
            Score += 5000;
        }

        public void gameLost()
        {
            Score -= Score+1;
        }
        
    }

}
