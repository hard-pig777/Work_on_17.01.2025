using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Work_on_17.Classes;
namespace Work_on_17.Classes
{
    public class Bullet : ISaveable
    {
        private Texture2D _texture;
        private int _width = 20;
        private int _height= 20;
        private int _speed = 3;
        private bool _isAlive;
        private Rectangle _destinationRectangle;
        public Vector2 Position
        {
            set
            {
                _destinationRectangle.X = (int)value.X;
                _destinationRectangle.Y = (int)value.Y;
            }
            get
            {
                return new Vector2(_destinationRectangle.X, _destinationRectangle.Y);
            }
        }
        public int Width
        {
            get { return _width; }
        }
        public int Height
        {
            get { return _height; }
        }
        public bool IsAlive
        {
            set { _isAlive = value; }
            get { return _isAlive; }
        }
        public Rectangle Collision
        {
            get { return _destinationRectangle; }
        }
        public Bullet()
        {
            _isAlive = true;
            _texture = null;
            _destinationRectangle = new Rectangle(100, 300, _width, _height);
        }
        public void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>("bullet");
        }
        public void Update()
        {

            _destinationRectangle.Y -= _speed;
            if(_destinationRectangle.Y<= 0 - _height)
            {
                _isAlive = false;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _destinationRectangle, Color.White);
        }

        public object SaveData()
        {
            BulletData data = new BulletData() { Position = this.Position, IsAlive = IsAlive};

            return data;
        }

        public void LoadData(object data, ContentManager content)
        {
            if(!(data is BulletData))
            {
                return;
            }

            BulletData bulletData = (BulletData)data;   
            Position = bulletData.Position;
            _isAlive = bulletData.IsAlive;
        }
    }
}
