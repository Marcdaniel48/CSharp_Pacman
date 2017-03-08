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

            foreach (var ghost in state.GhostPack)
            {
                ghost.Collision += incrementScore;
                ghost.PacmanDied += deadPacman;
            }

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
      
        private void deadPacman()
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

        public void gameLost()
        {

        }
        
    }

    public class Pellet : ICollidable
    {
        private int points = 10;

        public int Points
        {
            get
            {
                return points;
            }

            set
            {
                points = value;
            }
        }

        public event ICollidableEventHandler Collision;

        protected virtual void OnCollision()
        {
            if(Collision != null)
            {
				Collision(this);
            }
        }

        public void Collide()
        {
            OnCollision();
        }
    }

    public class Energizer : ICollidable
    {
        private int points = 100;
        private GhostPack ghosts;

        public int Points
        {
            get
            {
                return points;
            }

            set
            {
                points = value;
            }
        }
        

        public Energizer(GhostPack ghosts)
        {
            this.ghosts = ghosts;
        }

        public event ICollidableEventHandler Collision;


        protected virtual void OnCollision()
        {
            if (Collision != null)
            {
                Collision(this);
            }
        }

        public void Collide()
        {
            OnCollision();
            ghosts.ScaredGhosts();
        }
    }
    

    /// <summary>
    /// The Pen represents the area where Ghosts go when the game starts, when they are eaten or when
    /// Pacman dies and they need to restart. The Pen releases Ghosts in a First-In-First-Out manner, after
    /// a time period has elapsed.
    /// </summary>
    public class Pen
    {
        private Queue<Ghost> ghosts; //fifo structure to release the appropriate ghost
        private List<Timer> timers; //multiple times since more than 1 Ghost may be in teh Pen
        private List<Tile> pen; //list of the Tiles that make up the Pen, so two ghosts aren't placed on teh same Tile

        /// <summary>
        /// Constructor instantiates the internal data structures to empty
        /// </summary>
        public Pen()
        {
            this.ghosts = new Queue<Ghost>();
            this.timers = new List<Timer>();
            pen = new List<Tile>();
        }

        /// <summary>
        /// This method add Tiles to the Pen area. It is meant to be invoked when the game is being
        /// initialized by the GameState.
        /// </summary>
        /// <param name="tile">a Tiles that is part of the Pen</param>
        public void AddTile(Tile tile)
        {
            pen.Add(tile);
        }

        /// <summary>
        /// Event handler for a Timer Elapsed event. Each time a Timer elapses,
        /// the first Ghost in the queue is dequeued and released, and the Timer is removed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Release(object sender, ElapsedEventArgs e)
        {
            Timer t = (Timer)sender;
            t.Enabled = false;
            Ghost g = ghosts.Dequeue();
            timers.Remove(t);
            g.ChangeState(GhostState.Released);
        }

        /// <summary>
        /// Every time a Ghost is added to the Pen (either at the beginning of the
        /// game when the game is being initialized, or every time the Ghost needs to be reset),
        /// it is enqueued. It's position is determined by the next unoccupied Tile in the Pen.
        /// A timer is started: the timer duration is based on how many ghosts are enqueued, so that 
        /// they are not all released at the same time.
        /// </summary>
        /// <param name="ghost"></param>
        public void AddToPen(Ghost ghost)
        {
            ghosts.Enqueue(ghost);
            ghost.Position = pen[ghosts.Count - 1].Position;
            Timer t = new Timer((ghosts.Count * 1000));
            t.Enabled = true;
            t.Elapsed += Release;
            timers.Add(t);
        }
    }

}
