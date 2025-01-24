using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace Work_on_17.Classes
{
    public class Player
    {
        private Vector2 _position;
        private Texture2D _texture;
        public Player()
        {
            _position = new Vector2(30,30);
            _texture = null;
        }
        public void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>("player");
        }
        public void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.S))
            {
                _position.Y += 5;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.W))
            {
                _position.Y -= 5;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                _position.Y -= 5;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                _position.Y += 5;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
        }
    }
}
