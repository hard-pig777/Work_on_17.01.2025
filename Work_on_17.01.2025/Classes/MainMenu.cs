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
using System;
namespace Work_on_17.Classes
{
    internal class MainMenu
    {
        private List<Label> _buttonList = new List<Label>();
        private int _selected;

        private int _widthScreen;
        private int _heightScreen;
        private KeyboardState _keyboardState;
        private KeyboardState _prevkeyboardState;
        public event Action OnPlayingStarted;
        public MainMenu(int widthScreen, int heightScreen)
        {
            _widthScreen = widthScreen;
            _heightScreen = heightScreen;
            _selected = 0;
            _buttonList.Add(new Label(new Vector2(0, 0), "Play", Color.White));
            _buttonList.Add(new Label(new Vector2(0, 40), "Exit", Color.White));
            _buttonList.Add(new Label(new Vector2(0, 40), "Hi I am developer", Color.White));

        }
        public void LoadContent(ContentManager content)
        {
            int width = -10000000;
            int height = 0;

            foreach(Label button in _buttonList)
            {
                button.LoadContent(content);
                if(button.SizeText.X > width)
                {
                    width = (int)button.SizeText.X;
                }
                height += (int)button.SizeText.Y;

            }
            height = height + 20 * (_buttonList.Count - 1);
            int x = _widthScreen / 2 - width / 2;
            int y = _heightScreen / 2 - height / 2;
            int offset = 0;
            for(int i = 0; i < _buttonList.Count; i++)
            {
                _buttonList[i].Position = new Vector2(x + (width - _buttonList[i].SizeText.X) / 2, y + offset);
                offset += (int)_buttonList[i].SizeText.Y + 20;
            }
        }
        public void Update()
        {
            _keyboardState = Keyboard.GetState();

            if (_prevkeyboardState.IsKeyUp(Keys.S) && _keyboardState.IsKeyDown(Keys.S)){
                _selected++;
                if(_selected >= _buttonList.Count())
                {
                    _selected = 0;
                }
            }
            if (_prevkeyboardState.IsKeyUp(Keys.W) && _keyboardState.IsKeyDown(Keys.W))
            {
                _selected --;
                if (_selected < 0)
                {
                    _selected = _buttonList.Count() - 1;
                }
            }
            if (_keyboardState.IsKeyDown(Keys.Enter) && _prevkeyboardState.IsKeyUp(Keys.Enter))
            {
                if(_selected == 0)
                {
                    if(OnPlayingStarted != null)
                    {
                        OnPlayingStarted();
                    }
                }else if(_selected == 1)
                {
                    Game1.gameMode = GameMode.Exit;
                }
            }

            _prevkeyboardState = _keyboardState;
        }
        public void Draw(SpriteBatch _spriteBatch)
        {
            for(int i = 0; i < _buttonList.Count; i++)
            {
                Color colorbutton;
                if(i == _selected)
                {
                    colorbutton = Color.Yellow;
                }
                else
                {
                    colorbutton = Color.White;
                }
                _buttonList[i].Color = colorbutton;
                _buttonList[i].Draw(_spriteBatch );
            }
        }
    }
}
