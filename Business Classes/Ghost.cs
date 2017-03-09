using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Business_Classes;
using System.Timers;
using System.Collections;

namespace Business_Classes
{
    /// <summary>
    /// Represents a ghost object in a pacman game
    /// </summary>
    public class Ghost : IMovable, ICollidable
    {
        private Pacman pacman;
        private Vector2 target;
        private Pen pen;
        private Maze maze;
        private Direction direction;
        private String colour; // Change type to Color
        private IGhostState currentState;
        private Timer scaredTime;
        private Vector2 startPosition;
        private Vector2 position;

        /// <summary>
        /// Creates a ghost
        /// </summary>
        /// <param name="g">The gamestate</param>
        /// <param name="x">The x position of the ghost</param>
        /// <param name="y">The y position of the ghost</param>
        /// <param name="target">Where the ghost wants to go</param>
        /// <param name="start">The starting state of the ghost</param>
        /// <param name="color">Color of the ghost</param>
        public Ghost(GameState g, int x, int y, Vector2 target, GhostState start, String color)
        {
            this.pacman = g.Pacman;
            this.target = target;
            position = new Vector2(x, y);
            startPosition = position;
            this.pen = g.Pen;
            this.maze = g.Maze;
            this.colour = color;
            this.Points = 200;
            scaredTime = new Timer(10000);
            scaredTime.Elapsed += BackToChase;
            ChangeState(start);
        }

        // Events and delegates
        public event ICollidableEventHandler Collision;
        public delegate void PacmanDeathEventHandler();
        public event PacmanDeathEventHandler PacmanDied;

        protected virtual void OnPacmanDied()
        {
            if(PacmanDied != null)
            {
                PacmanDied();
            }
        }


        protected virtual void OnCollision()
        {
            if (Collision != null)
            {
                Collision(this);
            }
        }

        /// <summary>
        /// When a collision has been detected, then depending on the state of the ghost,
        /// pacman will either die or consume the ghost
        /// </summary>
        public void Collide()
        {
            if(this.Position == pacman.Position)
            {
                switch (CurrentState)
                {
                    case GhostState.Chase:
                        OnPacmanDied();
                        break;
                    case GhostState.Scared:
                        OnCollision();
                        break;
                }
            }
            target = pacman.Position;



        }


        public GhostState CurrentState
        {
            get;
            private set;
        }

        public String Colour //change return type to Color
        {
            get { return colour; }
        }

        /// <summary>
        /// Resets ghost to starting state and starting position
        /// </summary>
        public void Reset()
        {
            ChangeState(GhostState.Released);
            this.Position = startPosition;
        }

        /// <summary>
        /// Changes the state of the ghost to scared/released/chase, depending on the parameter.
        /// </summary>
        /// <param name="stateParam">The state we want to set the ghost into</param>
        public void ChangeState(GhostState stateParam)
        {
            if(stateParam == GhostState.Scared)
            {
                currentState = new Scared(this,maze);
                scaredTime.Enabled = true;
            }
            else if(stateParam == GhostState.Chase)
            {
                currentState = new Chase(this, maze, pacman, Position);
            }
            else if(stateParam == GhostState.Released)
            {
                currentState = new Chase(this, maze, pacman, Position);
            }

            CurrentState = stateParam;
        }


        /// <summary>
        /// Returns ghost back into chase state, after the effect of the energizer wears off
        /// </summary>
        /// <param name="sender">The timer object</param>
        /// <param name="e">The time ellapsed event</param>
        public void BackToChase(Object sender, ElapsedEventArgs e)
        {
            Timer time = (Timer)sender;
            time.Enabled = false;
            ChangeState(GhostState.Chase);
        }

        /// <summary>
        /// Moves ghost based on the IGhostState it is currently set to. When a move has been made, checks to see if a collision has been made
        /// </summary>
        public void Move()
        {
            currentState.Move();
            Collide();
        }

        public Direction Direction
        {
            get
            {
                return direction;
            }

            set
            {
                direction = value;
            }
        }

        public Vector2 Position
        {
            get
            {
                return this.target;
            }

            set
            {
                this.target = value;
            }
        }

        public int Points
        {
            get
            {
                return Points;
            }
            set { Points = value; }

        }
    }
}
