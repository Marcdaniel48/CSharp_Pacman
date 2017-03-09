using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Business_Classes
{
    public class GhostPack : IEnumerable<Ghost>
    {
        private List<Ghost> ghosts;

        public GhostPack()
        {
        }

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
                            ghost.Reset();
                            break;
                    }

                }
            }

        }

        public void ResetGhosts()
        {
            foreach (var ghost in ghosts)
            {
                ghost.Reset();
            }
        }

        public void ScaredGhosts()
        {
            foreach (var ghost in ghosts)
            {
                ghost.ChangeState(GhostState.Scared);
            }
        }

        public void Move()
        {
            foreach (var ghost in ghosts)
            {
                ghost.Move();
            }
        }

        public void Add(Ghost aGhost)
        {
            ghosts.Add(aGhost);
        }

        public IEnumerator<Ghost> GetEnumerator()
        {
            return ghosts.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ghosts.GetEnumerator();
        }
    }
}
