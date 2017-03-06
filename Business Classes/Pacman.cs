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
}
