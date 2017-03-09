using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business_Classes;

namespace Business_Classes
{
    /// <summary>
    /// Represents a pellet in pacman. A pellet is worth 10 points.
    /// </summary>
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

        public event ICollidableEventHandler Collision;

        /// <summary>
        /// When pacman lands on a pellet
        /// </summary>
        protected virtual void OnCollision()
        {
            if (Collision != null)
            {
                Collision(this);
            }
        }

        public void Collide()
        {
            OnCollision();
        }
    }

   
}
