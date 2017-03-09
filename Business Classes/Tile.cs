using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Business_Classes
{
    /// <summary>
    /// Represents the base class for the tile objects (wall,path) in the maze
    /// </summary>
    public abstract class Tile
    {
       /// <summary>
       /// Creates a tile object with the given co ordinates
       /// </summary>
       /// <param name="x">X position</param>
       /// <param name="y">Y position</param>
        public Tile(int x, int y)
        {
            this.Position = new Vector2(x, y);
        }


        public Vector2 Position
        {
            get;
        }

        /// <summary>
        /// The current ICollidable object (pellet or energizer) placed on the tile
        /// </summary>
        public abstract ICollidable Member
        {
            get;
            set;
        }


        public abstract Boolean CanEnter();

        public abstract void Collide();

        public abstract Boolean IsEmpty();

        /// <summary>
        /// Distance between an object (ghost) and the tile
        /// </summary>
        /// <param name="goal"></param>
        /// <returns></returns>
        public float GetDistance(Vector2 goal)
        {
            return Vector2.Distance(this.Position, goal);
        }

    }
}

