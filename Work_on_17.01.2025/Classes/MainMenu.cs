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
    internal class MainMenu
    {
        private List<Label> _buttonList = new List<Label>();
        private int _selected;
        public MainMenu()
        {
            _selected = 0;
            _buttonList.Add(new Label(new Vector2(0, 0), "Play", Color.Yellow));
            _buttonList.Add(new Label(new Vector2(0, 40), "Exit", Color.White));

        }
        public void LoadContent(ContentManager content)
        {
            foreach(Label button in _buttonList)
            {
                button.LoadContent(content);
            }
        }
        public void Update()
        {

        }
        public void Draw(SpriteBatch _spriteBatch)
        {
            foreach (Label button in _buttonList)
            {
                button.Draw(_spriteBatch);
            }
        }
    }
}
