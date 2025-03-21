using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private Label _label;
        MainMenu _mainMenu;
        private GameMode _gameMode = GameMode.Menu;
        private GameOver _gameOver;
        //private Asteroid _asteroid;
        private List<Asteroid> _asteroids;
        private List<Explosion> _explosions;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _gameOver = new GameOver(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            _player = new Player();
            _space = new Space();
            _mainMenu = new MainMenu();
            Vector2 _labelPosition = new Vector2(0, 0);
            Color labelColor = Color.White;
            _label = new Label(_labelPosition,"You are loser", labelColor);
            //_asteroid = new Asteroid();
            _asteroids = new List<Asteroid>();
            _explosions = new List<Explosion>();
            //_explosion = new Explosion(new Vector2(100, 100));
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _player.LoadContent(Content);
            _space.LoadContent(Content);
            _label.LoadContent(Content);
            _gameOver.LoadContent(Content);
            _mainMenu.LoadContent(Content);
            for(int i = 0; i < COUNT_ASTEROIDS; i++)
            {
                LoadAsteroid();
            }
            //_asteroid.LoadContent(Content);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            switch (_gameMode)
            {
                case GameMode.Menu:
                    _mainMenu.Update();
                    break;
                case GameMode.GameOver:
                    _space.Update();
                    _gameOver.Update();
                    break;
                case GameMode.Playing:
                     _space.Update();
                    _player.Update(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight, Content);
                    UpdateExplosions(gameTime);
                    //_asteroid.Update();
                    UpdateAsteroids();
                    CheckCollision();
                    base.Update(gameTime);
                    break;
                default:
                    break;
            }
            // TODO: Add your update logic here
            
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();
            {
                switch (_gameMode)
                {
                    case GameMode.Menu:
                        _mainMenu.Draw(_spriteBatch);
                        break;
                    case GameMode.GameOver:
                        _space.Draw(_spriteBatch);
                        _gameOver.Draw(_spriteBatch);
                        break;
                    case GameMode.Playing:
                        _space.Draw(_spriteBatch);
                        _player.Draw(_spriteBatch);
                        foreach (Asteroid asteroid in _asteroids)
                        {
                            asteroid.Draw(_spriteBatch);
                        }
                        foreach (var explosion in _explosions)
                        {
                            explosion.Draw(_spriteBatch);
                        }
                        //_explosion.Draw(_spriteBatch);
                        _label.Draw(_spriteBatch);
                        break;
                    default:
                        break;
                }
            }
            _spriteBatch.End();
            base.Draw(gameTime);
        }
        private void UpdateAsteroids()
        {
            for (int i = 0; i < _asteroids.Count; i++)
            {
                Asteroid asteroid = _asteroids[i];

                asteroid.Update();

                // teleport
                if (asteroid.Poisition.Y > _graphics.PreferredBackBufferHeight)
                {
                    Random random = new Random();

                    int x = random.Next(0, _graphics.PreferredBackBufferWidth - asteroid.Width);
                    int y = random.Next(0, _graphics.PreferredBackBufferHeight);

                    asteroid.Poisition = new Vector2(x, -y);
                }

                // check collision
                if (!asteroid.IsAlive)
                {
                    _asteroids.Remove(asteroid);
                    i--;
                }
            }

            // Загружаем доп. астероиды в игру
            if (_asteroids.Count < 10)
            {
                LoadAsteroid();
            }
        }

        private void LoadAsteroid()
        {
            Asteroid asteroid = new Asteroid();
            asteroid.LoadContent(Content);

            Random random = new Random();

            int x = random.Next(0, _graphics.PreferredBackBufferWidth - asteroid.Width);
            int y = random.Next(0, _graphics.PreferredBackBufferHeight);

            asteroid.Poisition = new Vector2(x, -y);

            _asteroids.Add(asteroid);
        }
        private void CheckCollision()
        {
            foreach(Asteroid asteroid in _asteroids)
            {
                if (asteroid.Collision.Intersects(_player.Collision))
                {
                    asteroid.IsAlive = false;
                    Explosion explosion = new Explosion(asteroid.Poisition);
                    Vector2 position = asteroid.Poisition;
                    position = new Vector2(position.X - explosion.Width / 2, position.Y - explosion.Height / 2);
                    explosion.LoadContent(Content);
                    position = new Vector2(position.X + asteroid.Width/ 2, position.Y + asteroid.Height / 2);
                    explosion.Position = position; 
                    _explosions.Add(explosion);
                }

                foreach (Bullet bullet in _player.Bullets)
                {
                    if (asteroid.Collision.Intersects(bullet.Collision))
                    {
                        asteroid.IsAlive = false;
                        bullet.IsAlive = false;
                        Explosion explosion = new Explosion(asteroid.Poisition);
                        Vector2 position = asteroid.Poisition;
                        position = new Vector2(position.X - explosion.Width / 2, position.Y - explosion.Height / 2);
                        explosion.LoadContent(Content);
                        position = new Vector2(position.X + asteroid.Width / 2, position.Y + asteroid.Height/ 2);
                        explosion.Position = position;

                        _explosions.Add(explosion);
                    }
                }
            }
        }
        private void UpdateExplosions(GameTime gameTime)
        {
            for(int i = 0;  i < _explosions.Count; i++)
            {
                _explosions[i].Update(gameTime);
                if (!_explosions[i].isAlive)
                {
                    _explosions.RemoveAt(i);
                    i--;
                }
            }
        }
    }
}
