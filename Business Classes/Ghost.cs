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
    public class Ghost : IMovable, ICollidable
    {
        private Pacman pacman;
        private Vector2 target;
        private Pen pen;
        private Maze maze;
        private Direction direction;
        private String colour; // Change type to Color
        private IGhostState currentState;
        private static Timer scaredTime;
        private Vector2 startPosition;

        public Ghost(GameState g, int x, int y, Vector2 target, GhostState start, String color)
        {
            this.pacman = g.Pacman;
            this.target = target;
            startPosition = new Vector2(x, y);
            this.pen = g.Pen;
            this.maze = g.Maze;
            this.colour = color;
            this.Points = 200;
            scaredTime = new Timer(10000);
            scaredTime.Elapsed += BackToChase;
            ChangeState(start);
        }


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

        public void Reset()
        {
            ChangeState(GhostState.Released);
            //pen.AddToPen(this);
            this.Position = startPosition;
        }

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

        public void BackToChase(Object sender, ElapsedEventArgs e)
        {
            Timer time = (Timer)sender;
            time.Enabled = false;
            ChangeState(GhostState.Chase);
        }

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
