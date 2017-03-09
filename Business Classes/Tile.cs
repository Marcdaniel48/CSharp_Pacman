using System;
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
}

