using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamGame
{
	public class Chunk{
		public int x;
		public int y;
		public List<Tile> tiles;
		public List<Entity> entities;
		public const int chunkLength = 64;
		public List<Projectile> projectiles;

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

		public static Vector2 inWhatChunk(int X, int Y)
		{
			return new Vector2(X / chunkLength, Y / chunkLength);
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
