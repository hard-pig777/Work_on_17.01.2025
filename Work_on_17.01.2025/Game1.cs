using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Work_on_17.Classes;
namespace Work_on_17
{
    public class Game1 : Game
    {
        private const int COUNT_ASTEROIDS = 10;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        //Поля  
        private Player _player;
        private Space _space;
        //private Asteroid _asteroid;
        private List<Asteroid> _asteroids;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _player = new Player();
            _space = new Space();
            //_asteroid = new Asteroid();
            _asteroids = new List<Asteroid>();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _player.LoadContent(Content);
            _space.LoadContent(Content);
            for(int i = 0; i < COUNT_ASTEROIDS; i++)
            {
                Asteroid asteroid = new Asteroid(new Vector2(i * 40, 0));
                asteroid.LoadContent(Content);
                _asteroids.Add(asteroid);
            }
            //_asteroid.LoadContent(Content);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            _space.Update();
            _player.Update(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight, Content);
            //_asteroid.Update();
            foreach(Asteroid asteroid in _asteroids)
            {
                asteroid.Update();
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            {
                _space.Draw(_spriteBatch);
                _player.Draw(_spriteBatch);
                foreach (Asteroid asteroid in _asteroids)
                {
                    asteroid.Draw(_spriteBatch);
                }
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
