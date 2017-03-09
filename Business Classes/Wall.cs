using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Classes
{
    /// <summary>
    /// Represents a wall object in the maze
    /// </summary>
    public class Wall : Tile
    {
        /// <summary>
        /// Creates a wall tile by making a call to the base constructor.
        /// </summary>
        /// <param name="x">X position</param>
        /// <param name="y">Y position</param>
        public Wall(int x, int y) : base(x, y)
        {
        }

        /// <summary>
        /// Current IColliable object (pellet or energizer) that a tile contains.
        /// However, since it is wall there is no set memeber.
        /// </summary>
        public override ICollidable Member
        {
            set
            {
                throw new NotImplementedException();
            }
            get
            {
                throw new NotImplementedException();

            }
        }

        public override bool CanEnter()
        {
            return false;
        }

        public override void Collide()
        {
            throw new NotImplementedException();
        }


        public override bool IsEmpty()
        {
            return true;
        }

    }
}
