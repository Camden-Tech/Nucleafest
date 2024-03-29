﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Microsoft.Xna.Framework.Graphics;

namespace StreamGame
{
	public class Projectile{
		public float x;
		public float y;
		public float xVelocity;
		public float yVelocity;
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
		public int damage;
		public int time;
		public List<float> aiVariables;
		public int UUID;
		public enum deathReasons
		{
			hitWall,
			hitPlayer,
			hitEntity,
			other
		}

		public Boolean Die(deathReasons reason)
		{
			return true;
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
		
		public Boolean CheckHitboxWithEntity(Entity e){
			Vector2 tMidPoint = new Vector2(width / 2 + x, height / 2 + y);
			Vector2 eMidPoint = new Vector2(e.width / 2 + e.x, e.height / 2 + e.y);
			float tWHRatio = width / height;
			float eWHRatio = e.width / e.height;
			Vector2 distanceSquared = new Vector2(Math.Abs(tMidPoint.X - eMidPoint.X), Math.Abs(tMidPoint.Y - eMidPoint.Y));
			distanceSquared.X -= width * tWHRatio / 2 + e.width * eWHRatio / 2;
			distanceSquared.Y -= height / 2 + e.height / 2;
	
			if(distanceSquared.X <= 0 && distanceSquared.Y <= 0){
				return true;
			} else {
				return false;
			}
		
		}

	}

}
