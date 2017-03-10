using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Business_Classes
{
    /// <summary>
    /// Represents the maze/map of the pacman game.
    /// </summary>
    public class Maze
    {
        private Tile[,] maze; // The maze is made into a array of tiles

        public delegate void winHandler();

        public Maze()
        {
        }

        /// <summary>
        /// Sets the tiles of the maze
        /// </summary>
        /// <param name="maze">An array of tiles representing the map</param>
        public void SetTiles(Tile[,] maze)
        {
            this.maze = maze;
        }

        //event
        public event winHandler PacmanWon;

        protected virtual void OnPacmanWon()
        {
            if (PacmanWon != null)
            {
                PacmanWon();
            }
        }

        /// <summary>
        /// Indexer. X and Y of a tile on the map
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Tile this[int x, int y]
        {
            get { return maze[x, y]; }
            set { maze[x, y] = value; }
        }


        public int Size
        {
            get { return maze.GetLength(0); }
        }

        /// <summary>
        /// The amount of available tiles an object(pacman & ghost) can move to
        /// </summary>
        /// <param name="position"></param>
        /// <param name="dir"></param>
        /// <returns></returns>
        public List<Tile> GetAvailableNeighbours(Vector2 position, Direction dir)
        {
            List<Tile> pathTiles = new List<Tile>();

            switch (dir)
            {
                case Direction.Up:
                    if (maze[(int)position.X, (int)position.Y - 1].CanEnter())
                    {
                        pathTiles.Add(maze[(int)position.Y + 1,(int)position.X]);
                    }
                    if (maze[(int)position.Y,(int)position.X - 1].CanEnter())
                    {
                        pathTiles.Add(maze[(int)position.Y,(int)position.X - 1]);
                    }
                    if (maze[(int)position.Y,(int)position.X + 1].CanEnter())
                    {
                        pathTiles.Add(maze[(int)position.Y,(int)position.X + 1]);
                    }
                    break;
                case Direction.Down:
                    if (maze[(int)position.Y + 1,(int)position.X].CanEnter())
                    {
                        pathTiles.Add(maze[(int)position.Y - 1,(int)position.X]);
                    }
                    if (maze[(int)position.Y,(int)position.X + 1].CanEnter())
                    {
                        pathTiles.Add(maze[(int)position.Y,(int)position.X + 1]);
                    }
                    if (maze[(int)position.Y,(int)position.X - 1].CanEnter())
                    {
                        pathTiles.Add(maze[(int)position.Y,(int)position.X - 1]);
                    }
                    break;
                case Direction.Left:
                    if (maze[(int)position.Y,(int)position.X - 1].CanEnter())
                    {
                        pathTiles.Add(maze[(int)position.Y,(int)position.X - 1]);
                    }
                    if (maze[(int)position.Y + 1,(int)position.X].CanEnter())
                    {
                        pathTiles.Add(maze[(int)position.Y + 1,(int)position.X]);
                    }
                    if (maze[(int)position.Y - 1,(int)position.X].CanEnter())
                    {
                        pathTiles.Add(maze[(int)position.Y - 1,(int)position.X]);
                    }
                    break;
                case Direction.Right:
                    if (maze[(int)position.Y, (int)position.X + 1].CanEnter())
                    {
                        pathTiles.Add(maze[ (int)position.Y, (int)position.X + 1]);
                    }
                    if (maze[(int)position.Y - 1,(int)position.X ].CanEnter())
                    {
                        pathTiles.Add(maze[(int)position.Y - 1,(int)position.X ]);
                    }
                    if (maze[(int)position.Y + 1,(int)position.X ].CanEnter())
                    {
                        pathTiles.Add(maze[(int)position.Y + 1,(int)position.X ]);
                    }
                    break;
            }

            return pathTiles;
        }

        /// <summary>
        /// Checks to see if there are no more points nor energizers on the maze. If there are none, trigger the pacman won event
        /// </summary>
        public void CheckMembersLeft()
        {

            bool allEmpty = true;
            for (int i = 0; i < maze.GetLength(0); i++)
            {
                for (int j = 0; j < maze.GetLength(1); j++)
                {
                    if (maze[i, j].Member != null)
                    {
                        allEmpty = false;
                    }
                }
            }

            if (allEmpty)
            {
                OnPacmanWon();
            }
        }

    }

}
