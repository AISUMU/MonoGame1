using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGame1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        public const int WIDTH_screen = 800;
        public const int HEIGHT_screen = 600;

        public const int setUpScene = 0;
        public const int resetScene = 1;
        public const int menuScene = 2;
        public const int playScene = 3;
        public const int gameOverScene = 4;

        private int scene;
        private int sceneIndex;
        private int nextScene;
        private bool sceneChanged;

        public static Data data;

        public static Texture2D spriteTexture;

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
            graphics.PreferredBackBufferWidth = WIDTH_screen;
            graphics.PreferredBackBufferHeight = HEIGHT_screen;
            graphics.ApplyChanges();

            scene = setUpScene;



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

            // TODO: use this.Content to load your game content here
            spriteTexture = Content.Load<Texture2D>("Tranky");


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
        /// อนุญาตให้เกม run Logic เช่น อัปเดตโลก
        /// ตรวจสอบการชน รวบรวม Input และ เล่น Audio
        /// </summary>
        /// <param name="gameTime">ให้ภาพรวมของค่าเวลา</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            if (sceneChanged)
            {
                scene = nextScene;
                sceneIndex = 0;
                sceneChanged = false;
            }
            switch (scene)
            {
                case setUpScene:
                    break;
                case resetScene:
                    break;
                case menuScene:
                    break;
                case playScene:
                    break;
                case gameOverScene:
                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            switch (scene)
            {
                case setUpScene:
                    break;
                case resetScene:
                    break;
                case menuScene:
                    break;
                case playScene:
                    break;
                case gameOverScene:
                    break;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void _changScene(int nextScene)
        {
            this.nextScene = nextScene;
            sceneChanged = true;
        }
        
    }
}
