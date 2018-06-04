using Business_Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame
{
    /// <summary>
    /// Responsible for writing score-related information, such as the score and lives
    /// </summary>
    class ScoreSprite : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private SpriteFont spriteFont;
        private Game1 g2;
        private GameState myGameState;

        public ScoreSprite(Game1 game, GameState gs) : base(game)
        {
            this.g2 = game;
            this.myGameState = gs;
        }

        /// <summary>
        /// Draws the score and lives onto the game interface
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(spriteFont, "Score: " + myGameState.Score.Score, new Vector2(0, 750), Color.White);
            spriteBatch.DrawString(spriteFont, "Lives: " + myGameState.Score.Lives, new Vector2(0, 800), Color.White);

            // Once pacman's lives reach -1, the game is considered to be loss, so an appropriate string is printed onto the screen
            if (myGameState.Score.Lives < 0 || myGameState.Score.isLose == false)
            {
                spriteBatch.DrawString(spriteFont, myGameState.Score.gameEndString(), new Vector2(500, 500), Color.White);
            }
            spriteBatch.End();
        }
        public override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteFont = g2.Content.Load<SpriteFont>("ScoreLives");
        }
        
    }
}
