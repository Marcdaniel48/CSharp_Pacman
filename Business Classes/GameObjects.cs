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
   
    public class GameState
    {
        Pacman pman;
        GhostPack ghosts;
        Maze maze;
        Pen pen;
        ScoreAndLives score;

        public GameState()
        {

        }
        public static GameState Parse(string filecontent)
        {
            return null;
        }

        public Pacman Pacman
        {
            get { return pman; }
            private set { pman = value; }
        }

        public GhostPack GhostPack
        {
            get { return ghosts; }
            private set { ghosts = value; }
        }

        public Maze Maze
        {
            get { return maze; }
            private set { maze = value; }
        }

        public Pen Pen
        {
            get { return pen; }
            private set { Pen = value; }
        }

        public ScoreAndLives Score
        {
            get { return score; }
        }
    }
    public class Pacman
    {
        private GameState controller;
        private Maze maze;
        private Vector2 pos;

        public Pacman(GameState state)
        {
            controller = state;
            this.maze = state.Maze;
        }

        public void Move(Direction dir)
        {
            switch (dir)
            {
                case Direction.Up:
                    break;
                case Direction.Down:
                    break;
                case Direction.Left:
                    break;
                case Direction.Right:
                    break;
            }
        }
        public Vector2 Position
        {
            get { return pos; }
            set { pos = value; }
        }

        public void CheckCollisions()
        {

        }
    }

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

        public event collideHandler Collision;

        protected virtual void OnCollision(ICollidable scoreUpdate)
        {
            if(Collision != null)
            {
				Collision(scoreUpdate);
            }
        }

        public void Collide()
        {
            OnCollision(this);
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

        public event collideHandler Collision;

        public Energizer(GhostPack ghosts)
        {
            this.ghosts = ghosts;
        }

        protected virtual void OnCollision(ICollidable param)
        {
            collideHandler handler = Collision;
            if (Collision != null)
            {
                Collision(param);
            }
        }

        public void Collide()
        {
            OnCollision(this);
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
        private IGhostState currentState;
        private static Timer scared;

        public event collideHandler Collision;

        //public Ghost() { }
        
        public Ghost(GameState G, int X, int y, Vector2 target,IGhostState start, String color)
        {
            scared.Start();
        }

        //public event PacmanDied;
        //public event Collision;

        public IGhostState CurrentState
        {
            get { return currentState; }
        }

        public String Colour //change return type to Color
        {
            get { return colour;}
            set { }
        }

        public void Reset()
        {
            currentState = new Chase(this, this.maze, this.pacman, this.target);
        }

        public void ChangeState(IGhostState stateParam)
        {
            if(currentState is Chase)
            {
                currentState = new Scared(this, this.maze);
            }
            else if(currentState is Scared)
            {
                Reset();
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

        public void Collide()
        {
            throw new NotImplementedException();
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

        public Boolean CheckCollideGhosts(Vector2 bearing)
        {
            return true;
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

        public void Add(Ghost g)
        {
            ghosts.Add(g);
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
    public class Pen
    {
        private Queue<Ghost> ghosts;
        private List<Timer> timers;
        private List<Tile> pen;

        public Pen()
        {

        }

        public void AddTime(Tile tile)
        {

        }

        public void AddToPen(Ghost ghost)
        {

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

        }
    }
}
