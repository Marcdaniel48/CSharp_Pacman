﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Business_Classes
{
    /// <summary>
    /// Represents the playable character of the pacman game
    /// </summary>
    public class Pacman
    {
        private GameState controller;
        private Maze maze;
        private Vector2 pos;

        /// <summary>
        /// Creates pacman depending on the gamestate and gives pacman a layout of the maze
        /// </summary>
        /// <param name="state"></param>
        public Pacman(GameState state)
        {
            controller = state;
            this.maze = state.Maze;
        }

        /// <summary>
        /// Controls movement of pacman
        /// </summary>
        /// <param name="dir">Direction you want to move in</param>
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
            CheckCollisions();
        }

        public Vector2 Position
        {
            get { return pos; }
            set { pos = value; }
        }

        /// <summary>
        /// Uses the ghostpack's checkcollideghosts method to check for a collision whenever pacman moves
        /// </summary>
        public void CheckCollisions()
        {
            controller.GhostSquad.CheckCollideGhosts(Position);

        }
    }
}
