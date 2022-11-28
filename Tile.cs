using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamGame
{
    public class Tile
    {
        public float x;
        public float y;
        public float width;
        public float height;
        public Texture2D sprite;
        public float widthRatio;
        public float heightRatio;
        public Vector2 topRightHitbox;
        public Vector2 bottomRightHitbox;
        public Vector2 topLeftHitbox;
        public Vector2 bottomLeftHitbox;
        public int UUID;

        public Tile(float X, float Y, float W, float H, Texture2D Sprite)
        {
            x = X;
            y = Y;
            width = W;
            height = H;
            sprite = Sprite;
            widthRatio = width / sprite.Width;
            heightRatio = height / sprite.Height;
            topRightHitbox = new Vector2(width, 0);
            bottomRightHitbox = new Vector2(width, -height);
            topLeftHitbox = new Vector2(0,0);
            bottomLeftHitbox = new Vector2(0, -height);
        }
        public void changeSizes(float w, float h)
        {
            width = w;
            height = h;
            heightRatio = height / sprite.Height;
            widthRatio = width / sprite.Width;
        }
        public Boolean groundCheck()
        {
            if (Player.bottomLeftHitbox.Y + Player.y <= topLeftHitbox.Y + y && Player.bottomLeftHitbox.Y + Player.y >= bottomLeftHitbox.Y + y && ((Player.bottomLeftHitbox.X + Player.x > bottomLeftHitbox.X + x && Player.bottomLeftHitbox.X + Player.x < bottomRightHitbox.X + x) || (Player.bottomRightHitbox.X + Player.x > bottomLeftHitbox.X + x && Player.bottomRightHitbox.X + Player.x < bottomRightHitbox.X + x)))
            {
                
                Player.y = topLeftHitbox.Y + y - Player.bottomLeftHitbox.Y;
                Player.onGround = true;
                return true;
            }
            return false;
        }

        
    }
}
