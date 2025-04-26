using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using System.Threading;
using Work_on_17.Classes ;
namespace Work_on_17.Classes
{
    public class Player : ISaveable 
    {
        private Vector2 _position;
        private Texture2D _texture;
        private float _speed;
        private Rectangle _collision;
        private List <Bullet> _bulletList = new List <Bullet>();
        private int _health = 10;
        private int _score = 0;
        public List<Bullet> Bullets
        {
            get { return _bulletList; }
        }
        public Rectangle Collision
        {
            get { return _collision; }
        }
        public int Health
        {
            get => _health;

        }
        public int Score
        {
            get => _score;
        }
        public event Action<int> TakeDamage;
        public event Action<int> UpdateScore;
        //weapon
        
        //timer
        private int _timer = 1;
        private int _maxTime = 10;
        public Player()
        {
            _position = new Vector2(30,30);
            _texture = null;
            _speed = 9;
            _collision = new Rectangle((int)_position.X, (int)_position.Y, 0, 0);
        }
        
        public void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>("player");
        }
        public void Update(int widthSc, int heightSc, ContentManager content)
            
        {
            KeyboardState keyboard = Keyboard.GetState();
            #region Moevment
            if (keyboard.IsKeyDown(Keys.S))
            {
                _position.Y += _speed;
            }
            if (keyboard.IsKeyDown(Keys.W))
            {
                _position.Y -= _speed;
            }
            if (keyboard.IsKeyDown(Keys.A))
            {
                _position.X -= _speed;
            }
            if (keyboard.IsKeyDown(Keys.D))
            {
                _position.X += _speed;
            }
            if (keyboard.IsKeyDown(Keys.Space) && _timer >= _maxTime)
            {
                Bullet bullet = new Bullet();
                bullet.Position =new Vector2(_position.X + _texture.Width /2 - bullet.Width / 2, _position.Y - bullet.Height/2);
                bullet .LoadContent(content);
                _bulletList.Add(bullet);
                _timer = 0;            
            }
            foreach (Bullet bullet in _bulletList)
            {
                bullet.Update(); 

            }
            for(int i = 0; i <_bulletList.Count(); i++)
            {
                if (_bulletList[i].IsAlive == false)
                {
                    _bulletList.RemoveAt(i);
                    i--;
                }
            }
            if(_timer <= _maxTime)
            {
                _timer++;
            }
            #endregion
            _collision = new Rectangle(
                (int)_position.X,
                (int)_position.Y,
                _texture.Width,
                _texture.Height
            );
            #region Bounds
            if (_position.X < 0)
            {
                _position.X = 0;
            }
            if (_position.X > widthSc - _texture.Width)
            {
                _position.X = widthSc - _texture.Width;
            }
            if (_position.Y < 0)
            {
                _position.Y = 0;
            }
            if (_position.Y > heightSc - _texture.Height)
            {
                _position.Y = heightSc - _texture.Height;
            }
            
            #endregion
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
            foreach (Bullet bullet in _bulletList)
            {
                bullet.Draw(spriteBatch);
            }
        }
        public void Damage()
        {
            _health--;
            if (TakeDamage != null)
            {
                TakeDamage(_health);
            }
        }
        public void AddScore()
        {
            _score++;
            if(UpdateScore != null)
            {
                UpdateScore(_score);
            }
        }
        public void Reset()
        {
            _position = new Vector2(350, 400);
            _score = 0;
            _health = 10;
            _bulletList.Clear();
        }

        public object SaveData()
        {
            List<BulletData> bullets = new List<BulletData>();
            foreach(var bullet in _bulletList)
            {
                bullets.Add((BulletData)bullet.SaveData());
            }
            PlayerData playerData = new PlayerData() { Position = _position, Score = _score, Timer = _timer, Health = _health, Bullets = bullets};
            return playerData;
        }

        public void LoadData(object data, ContentManager content)
        {
            if (!(data is PlayerData))
            {
                return;
            }
            PlayerData playerData = (PlayerData)data;
            _position = playerData.Position;
            _score = playerData.Score;
            _health = playerData.Health;
            
            foreach(var bullet in playerData.Bullets)
            {
                Bullet bull = new Bullet();
                bull.LoadData(bullet, content);
                bull.LoadContent(content);

                _bulletList.Add(bull);
            }
        }
    }
}
