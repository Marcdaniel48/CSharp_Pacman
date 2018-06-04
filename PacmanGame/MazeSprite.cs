using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business_Classes;
using Microsoft.Xna.Framework;

namespace PacmanGame
{
    /// <summary>
    /// Responsible for rendering the maze
    /// </summary>
    public class MazeSprite : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Game1 g2;
        private GameState game;
        private Maze level;
        private Texture2D imgWall;
        private Texture2D imgPath;

        private Texture2D imgPellet;
        private Texture2D imgEnergizer;

        public MazeSprite(Game1 g1,GameState game):base(g1)
        {
            g2 = g1;
            this.game = game;
            this.level = game.Maze;
        }

        public override void Draw(GameTime gameTime)
        {

            spriteBatch.Begin();
            for(int i = 0; i < level.Size; i++)
            {
              for(int j = 0; j < level.Size; j++)
                {
                    if (level[i, j] is Wall)
                    {
                        spriteBatch.Draw(imgWall, new Rectangle(j * 32, i * 32, 32, 32), Color.White);
                    }
                    else if(level[i,j] is Path)
                    {
                        if(level[i,j].Member is Energizer)
                        {
                            spriteBatch.Draw(imgEnergizer, new Rectangle(j * 32, i * 32, 32, 32), Color.White);
                        }
                        else if(level[i, j].Member is Pellet)
                        {
                            spriteBatch.Draw(imgPellet, new Rectangle(j * 32, i * 32, 32, 32), Color.White);
                        }
                        else
                        {
                            spriteBatch.Draw(imgPath, new Rectangle(j * 32, i * 32, 32, 32), Color.White);
                        }
                    }
                }
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            imgWall = g2.Content.Load<Texture2D>("wall");
            imgPath = g2.Content.Load<Texture2D>("empty");
            imgPellet = g2.Content.Load<Texture2D>("pellet");
            imgEnergizer = g2.Content.Load<Texture2D>("energizer");
        }



    }
}
