﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business_Classes;

namespace Business_Classes
{
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

    public class Energizer : ICollidable
    {
        private int points = 100;
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
            ghosts.ScaredGhosts();
        }
    }
}
