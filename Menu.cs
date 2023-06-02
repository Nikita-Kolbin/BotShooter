using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VillageDefence.Weapon;

namespace VillageDefence
{
    internal class Menu
    {
        public Texture2D start;
        public Texture2D info;
        public Texture2D happyEnd;
        public Texture2D badEnd;

        public void LoadTexture(ContentManager Content)
        {
            start = Content.Load<Texture2D>("start");
            info = Content.Load<Texture2D>("info");
            happyEnd = Content.Load<Texture2D>("happy_end");
            badEnd = Content.Load<Texture2D>("bad_end");
        }
    }
}
