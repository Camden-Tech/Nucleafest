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
				for (int i = 0; i < tiles.Count; i++)
				{
					Tile t = tiles[i];
					//Tile code stuff
					if (!isStillInChunk((int)t.x, (int)t.y))
					{
						//Add to other chunk
						//Remove from current chunk
					}
				}
				for (int i = 0; i < entities.Count; i++)
				{
					Entity e = entities[i];
					//Entity code stuff
					if (!isStillInChunk((int)e.x, (int)e.y))
					{
						//Add to other chunk
						//Remove from current chunk
					}
				}
				for (int i = 0; i < projectiles.Count; i++)
				{
					Projectile p = projectiles[i];
					//Projectile code stuff
					if (!isStillInChunk((int)p.x, (int)p.y))
					{
						//Add to other chunk
						//Remove from current chunk
					}
				}
			}
		}
		
		public static Boolean chunkIsLoaded(int x, int y){
			for(int i = 0; i < loadedChunks.Count; i++)
			{
				Chunk c = loadedChunks[i];
                if (c.x == x && c.y == y)
                {
					return true;
                }
			}
			return false;
		}
		
		
		public static Vector2 inWhatChunk(int X, int Y)
		{
			return new Vector2(X / chunkLength, Y / chunkLength);
		}
		
		public static void attemptLoadChunk(int x, int y)
		{

            if (chunkIsLoaded(x,y))
            {
				return;
            }
			Chunk c = new Chunk(x,y);
			loadedChunks.Add(c);


		}
		
		public Boolean isStillInChunk(int X, int Y)
		{
			if (inWhatChunk(X, Y).Equals(new Vector2(x, y)))
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
