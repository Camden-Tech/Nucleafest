using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamGame
{
	public class Chunk{
		private int _x;
		private int _y;
		public int x
		{
			get { return _x; }

		}
		public int y
		{
			get { return _y; }
		}
		private List<Tile> tiles;
		private List<Projectile> projectiles;
		private List<Entity> entities;
		public const int chunkLength = 1500; //I have to hardcode this. The amount of effort I'll have to put it in order to NOT would honestly be too much and not enough reward
		public const int chunkLoadDistance = 4500; //Change this later to be configured in options menu. Also prevent yml changes to the file in order to change how far is can load below the minimum limit
		public static List<Chunk> loadedChunks = new List<Chunk>();
		
		public Chunk(int xPos, int yPos){
			//load tiles, projectiles, and entities from file if a file exists
			_x = xPos;
			_y = yPos;
		}

		public List<Tile> getTiles()
		{
			return tiles;
		}
		public List<Entity> getEntities()
		{
			return entities;
		}
		public List<Projectile> getProjectiles()
		{
			return projectiles;
		}


		public void interactContents()
		{
			if (checkIfLoaded())
			{
				Vector2 coords;
				for (int i = 0; i < tiles.Count; i++)
				{
					Tile t = tiles[i];
					//Tile code stuff
					coords = Chunk.inWhatChunk(t.x,t.y);
					
					Chunk c = Chunk.chunkIsLoaded(coords.x,coords.y);
					if (c != this)
					{
						if(c == null){
							t.exists = false;
							this.tiles.remove(t);
					        } else {
							
							c.tiles.add(t);
							this.tiles.remove(t);
						}
					}
				}
				for (int i = 0; i < entities.Count; i++)
				{
					Entity e = entities[i];
					coords = Chunk.inWhatChunk(e.x,e.y);
					
					Chunk c = Chunk.chunkIsLoaded(coords.x,coords.y);
					if (c != this)
					{
					    if(e.Respawnable){
					       if(c == null){
							e.exists = false;
						       this.entities.remove(e);
					       } else {

							c.entities.add(e);
							this.entities.remove(e);
					       }
					    } else {
						Chunk.attemptLoadChunk(coords.x,coords.y);
					    }
					}
				}
				for (int i = 0; i < projectiles.Count; i++)
				{
					Projectile p = projectiles[i];
					coords = Chunk.inWhatChunk(p.x,p.y);
					
					Chunk c = Chunk.chunkIsLoaded(coords.x,coords.y);
					if (c != this)
					{
						if(c == null){
							p.exists = false;
							this.projectiles.remove(p);
					        } else {
							c.projectiles.add(p);
							this.projectiles.remove(p);
						}
					} 
				}
			}
		}
		
		public static Chunk chunkIsLoaded(int xP, int yP){
			for(int i = 0; i < loadedChunks.Count; i++)
			{
				Chunk c = loadedChunks[i];
             			if (c.x == xP && c.y == yP)
             		 	{
					return c;
              		  	}
			}
			return null;
		}
		
		
		public static Vector2 inWhatChunk(int Xp, int Yp)
		{
			return new Vector2(Xp / chunkLength, Yp / chunkLength);
		}
		
		public static void attemptLoadChunk(int xP, int yP)
		{

         		if (chunkIsLoaded(xP,yP))
          		{
				return;
           		}
			Chunk c = new Chunk(xP,yP);
			loadedChunks.Add(c);


		}
		
		public Boolean isStillInChunk(int xP, int uP)
		{
			if (inWhatChunk(xP, yP).Equals(new Vector2(x, y)))
			{
				return true;
			}
			return false;
		}

		public Boolean checkIfLoaded()
		{
			int distanceSquared = Math.Abs((int)(Player.x / chunkLength + Player.y / chunkLength) - x - y);
			if (distanceSquared <= chunkLoadDistance)
			{
				return true;
			}
			return false;
		}

	}

}
