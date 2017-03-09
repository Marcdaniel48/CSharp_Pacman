using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Classes
{
    public class Wall : Tile
    {
        public Wall(int x, int y) : base(x, y)
        {
        }

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
