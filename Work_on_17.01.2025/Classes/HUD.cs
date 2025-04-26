using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;

namespace Work_on_17.Classes
{
    public class HUD
    {
        private HealthBar _healthBar;
        private Label _labelScore;
        public HUD()
        {
            _healthBar = new HealthBar(new Vector2(20, 20), 10, 150, 25);
            Vector2 position = new Vector2(20, 20);
            _labelScore = new Label(new Vector2(position.X, position.Y + _healthBar.DestinationRectangle.Height + 20), "Score: 0", Color.White);

        }
        public void LoadContent(GraphicsDevice graphics, ContentManager content)
        {
            _healthBar.LoadContent(graphics);
            _labelScore.LoadContent(content);
        }
        public void Draw(SpriteBatch _spriteBatch)
        {
            _healthBar.Draw(_spriteBatch);
            _labelScore.Draw(_spriteBatch);
        }
        public void OnPlayerTakeDamage(int health)
        {
            _healthBar.NumParts = health;
        }
        public void OnScoreUpdate(int score)
        {
            _labelScore.Text = $"Score {score}";
        } 
        public void Reset()
        {
            _healthBar.NumParts = 10;
            _labelScore.Text = "Score: 0";
        }
    }
}
