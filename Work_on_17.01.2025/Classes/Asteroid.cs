using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;

namespace Work_on_17.Classes { 

    internal class Asteroid
    {
        private Vector2 _position;
        private Texture2D _texture;
        public Asteroid() :this(Vector2.Zero)
        {

        }
        public Asteroid(Vector2 position)
        {
            _position = position;
            _texture = null;
        }

        public void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>("asteroid");
        }
        public void Update()
        {
            _position.Y +=2;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_texture, _position, Color.White);
        }
    }
}
