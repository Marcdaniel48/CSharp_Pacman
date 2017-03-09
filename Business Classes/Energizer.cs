using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Classes
{
    /// <summary>
    /// Represents the energizer in Pacman.
    /// </summary>
    public class Energizer : ICollidable
    {
        private int points = 100; // The amount of points the energizer is worth
        private GhostPack ghosts;

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


        public Energizer(GhostPack ghosts)
        {
            this.ghosts = ghosts;
        }

        public event ICollidableEventHandler Collision;


        /// <summary>
        /// Event trigger for when pacman collides with an energizer
        /// </summary>
        protected virtual void OnCollision()
        {
            if (Collision != null)
            {
                Collision(this);
            }
        }

        /// <summary>
        /// What happens when pacman collides with an energizer: calls the OnCollision method and sets the state of all ghosts to scared
        /// </summary>
        public void Collide()
        {
            OnCollision();
            ghosts.ScaredGhosts();
        }
    }
}
