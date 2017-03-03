using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Business_Classes
{
    //need to determine what the delegate does
    public delegate void collideHandler(ICollidable collidableParam);

    public enum Direction
    {
        Up, Down, Left, Right
    }

    public interface ICollidable
    {
        event collideHandler Collision;
        int Points
        {
            get;
            set;
        }


        void Collide();

    }
    
    public interface IMovable
    {
        Direction Direction
        {
            get;
            set;
        }
        Vector2 Position
        {
            get;
            set;
        }

        void Move();                 
    }

    public interface IGhostState
    {
        void Move();
    }
}
