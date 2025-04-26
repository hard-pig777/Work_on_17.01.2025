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
    public class GameOver
    {
        private Label _label;
        private int _widthScreen;
        private int _heightScreen;
        private Label _lblScore;
        private Label _lblInstructions;
        public GameOver(int widthScreen, int heightScreen) {
            _label = new Label(new Vector2(250, 250), "GAME OVER", Color.Red);
            _widthScreen = widthScreen;
            _heightScreen = heightScreen;
            _lblInstructions = new Label(new Vector2(250, 250), "Press Enter to continue", Color.Orange);
            _lblScore = new Label(new Vector2(250, 250), "", Color.White);
        }
        public void LoadContent(ContentManager content)
        {
            _label.LoadContent(content);
            _label.Position = new Vector2(_widthScreen / 2 - _label.SizeText.X / 2, _heightScreen / 2 - _label.SizeText.Y / 2 - 20);
            _lblScore.LoadContent(content);
            _lblScore.Position = new Vector2(_widthScreen / 2 - _lblScore.SizeText.X / 2, _heightScreen / 2 - _lblScore.SizeText.Y / 2);
            _lblInstructions.LoadContent(content);
            _lblInstructions.Position = new Vector2(_widthScreen / 2 - _lblInstructions.SizeText.X / 2, _heightScreen / 2 - _lblInstructions.SizeText.Y / 2 + 20);
        }
        public void Update()
        {
            KeyboardState keyboard = Keyboard.GetState();
            if(keyboard.IsKeyDown(Keys.Enter))
            {
                Game1.gameMode = GameMode.Menu;
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            _label.Draw(spriteBatch);
            _lblScore.Draw(spriteBatch);
            _lblInstructions.Draw(spriteBatch);
        }
        public void SetScore(int score)
        {
            _lblScore.Text = $"Final score: {score}";
            _lblScore.Position = new Vector2(_widthScreen / 2 - _lblScore.SizeText.X / 2, _heightScreen / 2 - _lblScore.SizeText.Y / 2);
        }
    }
}
