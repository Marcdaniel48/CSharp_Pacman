﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Business_Classes
{
    public abstract class Tile
    {
       
        public Tile(int x, int y)
        {
            this.Position = new Vector2(x, y);
        }
        public Vector2 Position
        {
            get;
        }

        public abstract ICollidable Member
        {
            get;
            set;
        }

        public abstract Boolean CanEnter();

        public abstract void Collide();

        public abstract Boolean IsEmpty();


        public float GetDistance(Vector2 goal)
        {
            return Vector2.Distance(this.Position, goal);
        }

    }
    public class Wall : Tile
    {
        public Wall(int x, int y) :base(x,y)
        {
        }

        public override ICollidable Member
        {
            set
            {
                throw new NotImplementedException();
            }
            get
            {
                throw new NotImplementedException();

            }
        }

        

        public override bool CanEnter()
        {
            return false;
        }

        public override void Collide()
        {
            throw new NotImplementedException();
        }
        

        public override bool IsEmpty()
        {
            return true;
        }

    }

    public class Path : Tile
    {
        private ICollidable member;

        public Path(int x, int y, ICollidable member): base(x,y)
        {
            this.member = member;
        }

        public override bool CanEnter()
        {
            return true;
        }

        public override void Collide()
        {
            this.Member.Collide();
        }

        public override bool IsEmpty()
        {
            if(member == null)
            {
                return true;
            }

            return false;
        }
        

        public override ICollidable Member
        {
            get
            {
                return this.Member;
            }
            set
            {
                this.Member = value;
            }
        }
    }

    public class Maze
    {
        private Tile[,] maze;

        public delegate void winHandler();

        public Maze()
        {
        }

        public void SetTiles(Tile[,] maze)
        {
            this.maze = maze; 
        }

        //event
        public event winHandler PacmanWon;

        protected virtual void OnPacmanWon()
        {
            if (PacmanWon != null){
                PacmanWon();
            }
        }
        public Tile this[int x, int y]
        {
            get { return maze[x, y]; }
            set { maze[x, y] = value; }
        }

        public int Size
        {
            get { return maze.GetLength(0); }
        }

        public List<Tile> GetAvailableNeighbours(Vector2 position, Direction dir)
        {
            List<Tile> pathTiles = new List<Tile>();

            switch (dir)
            {
                case Direction.Up:
                    if(maze[(int)position.X,(int)position.Y+1].CanEnter())
                    {
                        pathTiles.Add(maze[(int)position.X, (int)position.Y + 1]);
                    }
                    if (maze[(int)position.X-1, (int)position.Y].CanEnter())
                    {
                        pathTiles.Add(maze[(int)position.X-1, (int)position.Y]);
                    }
                    if (maze[(int)position.X+1, (int)position.Y].CanEnter())
                    {
                        pathTiles.Add(maze[(int)position.X+1, (int)position.Y]);
                    }
                    break;
                case Direction.Down:
                    if (maze[(int)position.X, (int)position.Y-1].CanEnter())
                    {
                        pathTiles.Add(maze[(int)position.X, (int)position.Y-1]);
                    }
                    if (maze[(int)position.X+1, (int)position.Y].CanEnter())
                    {
                        pathTiles.Add(maze[(int)position.X+1, (int)position.Y]);
                    }
                    if (maze[(int)position.X-1, (int)position.Y].CanEnter())
                    {
                        pathTiles.Add(maze[(int)position.X-1, (int)position.Y]);
                    }
                    break;
                case Direction.Left:
                    if (maze[(int)position.X-1, (int)position.Y].CanEnter())
                    {
                        pathTiles.Add(maze[(int)position.X-1, (int)position.Y]);
                    }
                    if (maze[(int)position.X, (int)position.Y+1].CanEnter())
                    {
                        pathTiles.Add(maze[(int)position.X, (int)position.Y+1]);
                    }
                    if (maze[(int)position.X, (int)position.Y-1].CanEnter())
                    {
                        pathTiles.Add(maze[(int)position.X, (int)position.Y-1]);
                    }
                    break;
                case Direction.Right:
                    if (maze[(int)position.X+1, (int)position.Y].CanEnter())
                    {
                        pathTiles.Add(maze[(int)position.X+1, (int)position.Y]);
                    }
                    if (maze[(int)position.X, (int)position.Y-1].CanEnter())
                    {
                        pathTiles.Add(maze[(int)position.X, (int)position.Y-1]);
                    }
                    if (maze[(int)position.X, (int)position.Y+1].CanEnter())
                    {
                        pathTiles.Add(maze[(int)position.X, (int)position.Y+1]);
                    }
                    break;
            }

            return pathTiles;
        }

        public void CheckMembersLeft()
        {

            bool allEmpty = true;
            for(int i = 0; i < maze.GetLength(0); i++)
            {
                for(int j = 0; j < maze.GetLength(1); j++)
                {
                    if(maze[i,j].Member != null)
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
