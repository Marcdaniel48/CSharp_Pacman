using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Business_Classes
{

    public enum Direction
    {
        Up, Down, Left, Right
    }

    public enum GhostState
    {
        Scared, Chase, Released
    }

    public delegate void ICollidableEventHandler(ICollidable Collidable);

    /// <summary>
    /// Outline for a collidable object
    /// </summary>
    public interface ICollidable
    {
        event ICollidableEventHandler Collision;

        int Points
        {
            get;
            set;
        }
        
        void Collide();

    }
    
    /// <summary>
    /// Outline for a moveable object
    /// </summary>
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
