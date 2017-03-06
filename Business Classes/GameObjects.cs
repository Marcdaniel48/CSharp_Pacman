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

    public class ScoreAndLives
    {
        private int points;
        private int lives;

        //need to create a delegate
        public ScoreAndLives(GameState state)
        {
            points = state.Score.Score;
            lives = state.Score.Lives;
        }
        //public event GameOver()
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
                Console.WriteLine();
            }
        }

        private void incrementScore(ICollidable collide)
        {
            Score += collide.Points;
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
        

        public Ghost(GameState g, int x, int y, Vector2 target, GhostState start, String color)
        {
            this.pacman = g.Pacman;
            this.target = target;
            this.pen = g.Pen;
            this.maze = g.Maze;
            this.colour = color;
            this.currentState = start;
        }


        public event ICollidableEventHandler Collision;
        public delegate void PacmanDeathEventHandler();
        public event PacmanDeathEventHandler pacmanDies;


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
            if(currentState == GhostState.Chase)
            {
                currentState = GhostState.Scared;
            }
            else if(currentState == GhostState.Scared)
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
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int Points
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }
    }

    public class GhostPack: IEnumerable<Ghost>
    {
        private List<Ghost> ghosts;

        public GhostPack()
        {

        }

        public void CheckCollideGhosts(Vector2 bearing)
        {

        }

        public void ResetGhosts()
        {

        }

        public void ScaredGhosts()
        {
            
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

    /// <summary>
    /// The Scared class encapsulates the required behaviour when a Ghost is in scared state. The Ghost will
    /// change direction immediately upon instantiating the Scared state. Each move is subsequently randomly
    /// chosen from the available tiles.
    /// will
    /// </summary>
    public class Scared : IGhostState
    {
        private Ghost ghost;
        private Maze maze;

        /// <summary>
        /// Two-parameter constructor to initialize the Scared state. It requires a handle to the Ghost who is scared
        /// as well as the Maze to know which tiles are available.
        /// </summary>
        /// <param name="ghost"></param>
        /// <param name="maze"></param>
        public Scared(Ghost ghost, Maze maze)
        {
            //change direction - make a 180 degree turn
            switch (ghost.Direction)
            {
                case Direction.Up:
                    ghost.Direction = Direction.Down;
                    break;
                case Direction.Down:
                    ghost.Direction = Direction.Up;
                    break;
                case Direction.Right:
                    ghost.Direction = Direction.Left;
                    break;
                case Direction.Left:
                    ghost.Direction = Direction.Right;
                    break;
            }
            this.ghost = ghost;
            this.maze = maze;
        }

        /// <summary>
        /// This method is invoked to move the scared Ghost to the random available tile.
        /// Everytime a Ghost moves, we have to do two things: update the Ghost's Position
        /// and update the Ghosts's Direction. This indicates the direction in which it is moving, 
        /// and it is required to make sure that the Ghosts doesn't turn back to it's previous
        /// position (i.e., to avoid 180 degree turns) (used by the Maze class's GetAvailableNeighbours
        /// method)
        /// </summary>
        public void Move()
        {
            Tile current = maze[(int)ghost.Position.X, (int)ghost.Position.Y];
            List<Tile> places = maze.GetAvailableNeighbours(ghost.Position, ghost.Direction);
            int num = places.Count;
            if (num == 0)
                throw new Exception("Nowhere to go");

            Random rand = new Random();
            int choice = rand.Next(num);
            //determine direction
            if (places[choice].Position.X == ghost.Position.X + 1)
                ghost.Direction = Direction.Right;
            else if (places[choice].Position.X == ghost.Position.X - 1)
                ghost.Direction = Direction.Left;
            else if (places[choice].Position.Y == ghost.Position.Y - 1)
                ghost.Direction = Direction.Up;
            else
                ghost.Direction = Direction.Down;
            ghost.Position = places[choice].Position;
        }
    }

    public class Chase : IGhostState
    {
        private Ghost ghost;
        private Maze maze;
        private Vector2 target;
        private Pacman pacman;

        public Chase(Ghost ghost, Maze maze, Pacman pacman, Vector2 target)
        {
            this.ghost = ghost;
            this.maze = maze;
            this.pacman = pacman;
            this.target = target;
        }

        public void Move()
        {
            throw new NotImplementedException();
        }
    }
}
