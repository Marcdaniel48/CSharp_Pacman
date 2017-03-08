using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Business_Classes
{
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
                    if(maze[(int)Position.X,(int)Position.Y+1] is Path)
                    {
                        pos = new Vector2(Position.X, Position.Y+1);
                        this.CheckCollisions();
                    }
                    break;
                case Direction.Down:
                    if (maze[(int)Position.X, (int)Position.Y-1] is Path)
                    {
                        pos = new Vector2(Position.X, Position.Y - 1);
                        this.CheckCollisions();
                    }
                    break;
                case Direction.Left:
                    if (maze[(int)Position.X-1, (int)Position.Y] is Path)
                    {
                        pos = new Vector2(Position.X-1, Position.Y);
                        this.CheckCollisions();
                    }
                    break;
                case Direction.Right:
                    if (maze[(int)Position.X+1, (int)Position.Y] is Path)
                    {
                        pos = new Vector2(Position.X+1, Position.Y);
                        this.CheckCollisions();
                    }
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
            controller.GhostPack.CheckCollideGhosts(Position);

        }
    }
}
