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
        private float _width;
		private float _height;
		private float _heightRatio;
		private float _widthRatio;
		public abstract float width
		{
			get => _width;  
		};
		public abstract float height
		{
			get => _height;  
		};
		public abstract float widthRatio
		{
			get => _widthRatio;  
		};
		public abstract float heightRatio
		{
			get => _heightRatio;
		};
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
            _width = w;
            _height = h;
            heightRatio = height / sprite.Height;
            widthRatio = width / sprite.Width;
        }
    }
}
