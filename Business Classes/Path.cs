using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Classes
{
    /// <summary>
    /// A tile you can move into
    /// </summary>
    public class Path : Tile
    {
        private ICollidable member;

        public Path(int x, int y, ICollidable member) : base(x, y)
        {
            this.member = member;
        }

        public override bool CanEnter()
        {
            return true;
        }

        public override void Collide()
        {
            this.Member.Collide();
        }


        /// <summary>
        /// Checks to see if the current path tile holds a pellet or energizer.
        /// </summary>
        /// <returns></returns>
        public override bool IsEmpty()
        {
            if (member == null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// What the path is holding (pellet, energizer, or none)
        /// </summary>
        public override ICollidable Member
        {
            get
            {
                return this.member;
            }
            set
            {
                this.member = value;
            }
        }
    }

}
