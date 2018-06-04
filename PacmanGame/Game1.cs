/// <summary>
/// Authors of whole project:
/// @author: Marc-Daniel Dialogo
/// @author: Rhai Hinds
/// @author: Trevor Eames
/// @version: 3/29/2017
/// </summary>


using Business_Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using System.IO;
using System.Drawing;
namespace PacmanGame
{
    /// <summary>
    /// Invokes the GameState’s static parse method to get all the appropriate business classes instantiated
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteFont spriteFont;
        private GameState game;
        MazeSprite mazeSprite;
        GhostsSprite ghostsSprite;
        PacmanSprite pmanSprite;
        ScoreSprite scoreSprite;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            string level = File.ReadAllText("levels.csv");
            game = GameState.Parse(level);

            mazeSprite = new MazeSprite(this,game);
            mazeSprite.Initialize();
            Components.Add(mazeSprite);

            ghostsSprite = new GhostsSprite(this, game);
            ghostsSprite.Initialize();
            Components.Add(ghostsSprite);

            pmanSprite = new PacmanSprite(this, game);
            pmanSprite.Initialize();
            Components.Add(pmanSprite);

            scoreSprite = new ScoreSprite(this, game);
            scoreSprite.Initialize();
            Components.Add(scoreSprite);

            graphics.PreferredBackBufferHeight = 850;
            graphics.PreferredBackBufferWidth = 750;
            graphics.ApplyChanges();
            this.Window.Position = new Point(graphics.GraphicsDevice.Viewport.Width / 2, 0);
            
            base.Initialize();
            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            spriteFont = this.Content.Load<SpriteFont>("ScoreLives");
            
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                Components.Clear();
                Initialize();
                
                //base.Update(gameTime);
            }
            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(Color.Black);
            mazeSprite.Draw(gameTime);
            pmanSprite.Draw(gameTime);
            ghostsSprite.Draw(gameTime);
            scoreSprite.Draw(gameTime);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
