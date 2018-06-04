using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Business_Classes;

namespace PacmanGame
{
    /// <summary>
    /// Responsible for the GhostPack/Ghosts
    /// </summary>
    class GhostsSprite : DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        GameState superGameState;
        Game1 game;
        Texture2D imgGhost;
        GhostPack gSquad;

        int threshold = 0; // Helps regulate the speed of ghosts

        public GhostsSprite(Game1 game, GameState gs) : base(game)
        {
            this.game = game;
            this.superGameState = gs;
            gSquad = gs.GhostSquad;
          
        }

        

        public override void Draw(GameTime gameTime)
        {

            if (!superGameState.Score.gameDone)
            {
                threshold++;
                spriteBatch.Begin();
                foreach (Ghost ghost in gSquad)
                {
                    spriteBatch.Draw(imgGhost, new Rectangle((int)ghost.Position.X * 32, (int)ghost.Position.Y * 32, 32, 32), ghost.Colour);
                }
                if (threshold == 15)
                {
                    gSquad.Move();
                    threshold = 0;
                }
                spriteBatch.End();

                base.Draw(gameTime);
            }


        }
        public override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            imgGhost = game.Content.Load<Texture2D>("ghost");
        }
    }
}
