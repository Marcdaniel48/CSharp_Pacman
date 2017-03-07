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
        private GhostState currentState;
        private static Timer scared;
        private Vector2 startPosition;


        public Ghost(GameState g, int x, int y, Vector2 target, GhostState start, String color)
        {
            this.pacman = g.Pacman;
            this.target = target;
            startPosition = new Vector2(x, y);
            this.pen = g.Pen;
            this.maze = g.Maze;
            this.colour = color;
            //need to use enum
            this.currentState = start;
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
            OnCollision();
        }


        public GhostState CurrentState
        {
            get { return currentState; }
        }

        public String Colour //change return type to Color
        {
            get { return colour; }
        }

        public void Reset()
        {
            currentState = GhostState.Chase;
        }

        public void ChangeState(GhostState stateParam)
        {
            if(currentState == GhostState.Released)
            {
                currentState = GhostState.Chase;
                this.Position = startPosition;
            }
            else if (currentState == GhostState.Chase)
            {
                currentState = GhostState.Scared;
            }
            else if (currentState == GhostState.Scared)
            {
                currentState = GhostState.Chase;
            }
        }

        public void Move()
        {
            switch (direction)
            {
                case Direction.Up:
                    target.Y += 1;
                    break;
                case Direction.Down:
                    target.Y -= 1;
                    break;
                case Direction.Left:
                    target.X -= 1;
                    break;
                case Direction.Right:
                    target.X += 1;
                    break;
            }
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
                if(CurrentState == GhostState.Scared)
                {
                    return 200;
                }
                return 0;
            }
            set
            {
                this.Points = value;
            }

        }
    }

    public class GhostPack : IEnumerable<Ghost>
    {
        private List<Ghost> ghosts;

        public GhostPack()
        {
        }

        public void CheckCollideGhosts(Vector2 bearing)
        {
            foreach(var ghost in ghosts)
            {
                //ghost
            }
        }

        public void ResetGhosts()
        {
            foreach(var ghost in ghosts)
            {
                ghost.ChangeState(GhostState.Chase);
            }
        }

        public void ScaredGhosts()
        {
            foreach(var ghost in ghosts)
            {
                ghost.ChangeState(GhostState.Scared);
            }
        }

        public void Move()
        {
            
        }

        public void Add(Ghost aGhost)
        {
            ghosts.Add(aGhost);
        }

        public IEnumerator<Ghost> GetEnumerator()
        {
            return ghosts.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ghosts.GetEnumerator();
        }
    }
}
