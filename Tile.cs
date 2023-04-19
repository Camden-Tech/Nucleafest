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
        private float _width;
	private float _height;
	private float _heightRatio;
	private float _widthRatio;
	public float width
	{
		get => _width;  
	}
	public float height
	{
		get => _height;  
	}
	public float widthRatio
	{
		get => _widthRatio;  
	}
	public float heightRatio
	{
		get => _heightRatio;
	}
        public String sprite;
        public Vector2 topRightHitbox;
        public Vector2 bottomRightHitbox;
        public Vector2 topLeftHitbox;
        public Vector2 bottomLeftHitbox;
        public int UUID;

        public Tile(float X, float Y, float W, float H, String Sprite)
        {
            x = X;
            y = Y;
            _width = W;
            _height = H;
            sprite = Sprite;
            _widthRatio = width / sprite.Width;
            _heightRatio = height / sprite.Height;
            topRightHitbox = new Vector2(width, 0);
            bottomRightHitbox = new Vector2(width, -height);
            topLeftHitbox = new Vector2(0,0);
            bottomLeftHitbox = new Vector2(0, -height);
        }
        public void changeSizes(float w, float h)
        {
            _width = w;
            _height = h;
            _heightRatio = height / sprite.Height;
            _widthRatio = width / sprite.Width;
        }
	    
        public void update(){
            //Event to be overrided   
        }
        public Boolean groundCheck()
        {
            if (Player.bottomLeftHitbox.Y + Player.y <= topLeftHitbox.Y + y && Player.bottomLeftHitbox.Y + Player.y >= bottomLeftHitbox.Y + y && ((Player.bottomLeftHitbox.X + Player.x > bottomLeftHitbox.X + x && Player.bottomLeftHitbox.X + Player.x < bottomRightHitbox.X + x) || (Player.bottomRightHitbox.X + Player.x > bottomLeftHitbox.X + x && Player.bottomRightHitbox.X + Player.x < bottomRightHitbox.X + x)))
            {
                Player.changePosition(Player.x, topLeftHitbox.Y + y - Player.bottomLeftHitbox.Y);
                Player.onGround = true;
                return true;
            }
            return false;
        }

        
    }
}
