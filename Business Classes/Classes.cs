using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Business_Classes
{
    public abstract class Tile
    {
        private int x;
        private int y;

        public Tile(int x, int y)
        {
            this.x = x;
            this.y = y;
        }


        public Vector2 Position()
        {
        }

        public abstract ICollidable Member
        {
            get;
            set;
        }

        public abstract Boolean CanEnter();

        public abstract void Collide();

        public abstract Boolean IsEmpty();


        public float GetDistance(Vector2 goal);

    }
    public class Wall : Tile
    {
        public Wall(int x, int y) :base(x,y)
        {
            
        }

        public override ICollidable Member
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public override bool CanEnter()
        {
            throw new NotImplementedException();
        }

        public override void Collide()
        {
            throw new NotImplementedException();
        }

        public override float GetDistance(Vector2 goal)
        {
            throw new NotImplementedException();
        }

        public override bool IsEmpty()
        {
            throw new NotImplementedException();
        }

       
        public override Vector2 Position()
        {
            throw new NotImplementedException();
        }
    }
    public class Path : Tile
    {
        private ICollidable memeber;

        public Path(int x, int y, ICollidable member): base(x,y)
        {

        }

        public override bool CanEnter()
        {
            throw new NotImplementedException();
        }

        public override void Collide()
        {
            throw new NotImplementedException();
        }

        public override float GetDistance(Vector2 goal)
        {
            throw new NotImplementedException();
        }

        public override bool IsEmpty()
        {
            throw new NotImplementedException();
        }

        public override ICollidable Member()
        {
            throw new NotImplementedException();
        }

        public override Vector2 Position()
        {
            throw new NotImplementedException();
        }
    }

    public class Maze
    {
        private Tile[,] maze;

        public Maze()
        {

        }

        public void SetTiles(Tile[,] maze)
        {

        }

        //events
        //public event PacmanWon
        public Tile this[int x, int y]
        {
            get { return maze[x, y]; }
            set { maze[x, y] = value; }
        }

        public int Size
        {
            get { return maze.GetLength(0); }
        }

        public List<Tile> GetAvailableNeighbours(Vector2 position, Direction dir)
        {
            return null;
        }

        public void CheckMembersLeft()
        {

        }

    }

}
