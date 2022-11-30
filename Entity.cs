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
        private float _x;
        private float _y;
        public float x
        {
            get => _x;
        }
        public float y
        {
            get => _y;
        }
        public float xVelocity;
        public float yVelocity;
        public string Name;
        public bool Respawnable;
        public bool exists;
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
        public int damage;
        public int maxHealth;
        public int health;
        public int UUID;
        public Texture2D sprite;
        public int time;
        public List<float> aiVariables;
        
        public void changePositions(float xP, float yP}){
            _x = xP;
            _y = yP;
            Vector2 coords = Chunk.inWhatChunk(x,y);
            if(Respawnable){
               if(!Chunk.chunkIsLoaded(coords.x,coords.y)){
                    exists = false;
               }
            } else {
                Chunk.attemptLoadChunk(coords.x,coords.y);
            }
        }
        
        public void changeSizes(float w, float h)
        {
            _width = w;
            _height = h;
            _heightRatio = height / sprite.Height;
            _widthRatio = width / sprite.Width;
        }
    }
}
