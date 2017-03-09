using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Classes
{
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

        public override bool IsEmpty()
        {
            if (member == null)
            {
                return true;
            }

            return false;
        }


        public override ICollidable Member
        {
            get
            {
                return this.Member;
            }
            set
            {
                this.Member = value;
            }
        }
    }

}
