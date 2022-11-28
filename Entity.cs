using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace StreamGame
{
    public class Entity
    {
        public float x;
        public float y;
        public float xVelocity;
        public float yVelocity;
        public float width;
        public float height;
        public int damage;
        public int maxHealth;
        public int health;
        public int UUID;
        public float heightRatio;
        public float widthRatio;
        public Texture2D sprite;
        public int time;
        public List<float> aiVariables;

        public void changeSizes(float w, float h)
        {
            width = w;
            height = h;
            heightRatio = height / sprite.Height;
            widthRatio = width / sprite.Width;
        }
    }
}
