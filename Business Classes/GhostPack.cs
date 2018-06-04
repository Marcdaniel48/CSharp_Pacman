using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Business_Classes
{
    /// <summary>
    /// An object representing a list of all the ghosts in pacman
    /// </summary>
    public class GhostPack : IEnumerable<Ghost>
    {
        private List<Ghost> ghosts;

        public GhostPack()
        {
            this.ghosts = new List<Ghost>();
        }

        /// <summary>
        /// Loops through every single ghost and checks for collision
        /// </summary>
        /// <param name="pacPosition"></param>
        public void CheckCollideGhosts(Vector2 pacPosition)
        {
            foreach (var ghost in ghosts)
            {
                if (ghost.Position == pacPosition)
                {
                    switch (ghost.CurrentState)
                    {
                        case GhostState.Chase:
                            ghost.Collide();
                            ResetGhosts();
                            break;
                        case GhostState.Scared:
                            ghost.Collide();
                            break;
                    }

                }
            }

        }

        /// <summary>
        /// Loops through every single ghost and resets them, using their own reset method.
        /// </summary>
        public void ResetGhosts()
        {
            foreach (var ghost in ghosts)
            {
                ghost.Reset();
            }
        }


        /// <summary>
        /// Changes the state of all ghosts into scared
        /// </summary>
        public void ScaredGhosts()
        {
            foreach (var ghost in ghosts)
            {
                ghost.ChangeState(GhostState.Scared);
            }
        }

        /// <summary>
        /// Makes every single ghost move, using their own move methods
        /// </summary>
        public void Move()
        {
            foreach (var ghost in ghosts)
            {
                ghost.Move();
            }
        }

        /// <summary>
        /// Adds a ghost into the ghost pack
        /// </summary>
        /// <param name="aGhost"></param>
        public void Add(Ghost aGhost)
        {
            ghosts.Add(aGhost);
        }


        /// <summary>
        /// Allows us to iterate through the ghost pack
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Ghost> GetEnumerator()
        {
            return ghosts.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ghosts.GetEnumerator();
        }

        public Ghost Ghost1()
        {
            return ghosts[0];
        }
        public Ghost Ghost2()
        {
            return ghosts[1];
        }
        public Ghost Ghost3()
        {
            return ghosts[2];
        }
        public Ghost Ghost4()
        {
            return ghosts[3];
        }
    }
}
