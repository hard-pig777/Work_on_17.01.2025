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
using System.Diagnostics;
namespace Work_on_17.Classes
{
    public class Explosion
    {
        private Texture2D _texture;
        private Vector2 _position;
        private Rectangle _sorceRectangle;
        private double _time = 0.0d;
        private double _duration = 50.0d;
        private int _frameNumber = 0;


        private int _widthFrame = 117;
        private int _heightFrame = 117;

        public bool isAlive { get; set; } = true;
        public int Width { get { return _widthFrame; } }
        public int Height { get { return _heightFrame; } }
        public Vector2 Position { set { _position = value; } }  
        public Explosion(Vector2 position)
        {
            _position = position;
            _texture = null;
            
        }
        public void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>("explosion3");
        }
        public void Update(GameTime _gameTime)
        {
            _time += _gameTime.ElapsedGameTime.TotalMilliseconds;
            
            if(_time >= _duration)
            {
                _frameNumber++;
                _time = 0; 
            }
            _sorceRectangle = new Rectangle(_frameNumber * _widthFrame, 0, _widthFrame, _heightFrame);
            if(_frameNumber == 17)
            {
                isAlive = false;
            }
            Debug.WriteLine("Time: " + _gameTime.ElapsedGameTime.TotalMilliseconds);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, _sorceRectangle, Color.White);
        }
    }
}
