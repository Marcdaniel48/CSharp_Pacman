using Business_Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacmanGame
{
    /// <summary>
    /// Responsible for drawing Pacman and taking user input
    /// </summary>
    class PacmanSprite : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Game1 g2;
        private Texture2D imgPman;
        private GameState myGameState;
        private KeyboardState oldState;
        private Pacman pman;
        int threshold = 0; // Regulates the speed of pacman

        public PacmanSprite(Game1 game, GameState gs) : base(game)
        {
            this.g2 = game;
            this.myGameState = gs;
            pman = myGameState.Pacman;
        }

        public override void Draw(GameTime gameTime)
        {
            if (!myGameState.Score.gameDone)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(imgPman, new Rectangle((int)myGameState.Pacman.Position.X * 32, (int)myGameState.Pacman.Position.Y * 32, 32, 32), Color.White);
                spriteBatch.End();
                base.Draw(gameTime);
            }
        }
        public override void Initialize()
        {
            oldState = Keyboard.GetState();

            base.Initialize();
        }
        
        public override void Update(GameTime gameTime)
        {
            if (!myGameState.Score.gameDone)
            {
                checkInput();
                base.Update(gameTime);
            }
        }

        /// <summary>
        /// Checks arrow key presses to determine the direction in which pacman should move towards
        /// </summary>
        private void checkInput()
        {
            

            KeyboardState newState = Keyboard.GetState();
            if (newState.IsKeyDown(Keys.Right))
            {
                threshold++;
                if (threshold == 4)
                {
                    pman.Move(Direction.Right);
                    threshold = 0;
                }
            }
            else if(newState.IsKeyDown(Keys.Left))
            {
                threshold++;
                if (threshold == 4)
                {
                    pman.Move(Direction.Left);
                    threshold = 0;
                }
               
            }
            else if(newState.IsKeyDown(Keys.Up))
            {
                threshold++;
                if (threshold == 4)
                {
                    pman.Move(Direction.Up);
                    threshold = 0;
                }
            }
            else if(newState.IsKeyDown(Keys.Down))
            {
                threshold++;
                if (threshold == 4)
                {
                    pman.Move(Direction.Down);
                    threshold = 0;
                }
            }

        }
        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            imgPman = g2.Content.Load<Texture2D>("pacman");
        }
    }
}
