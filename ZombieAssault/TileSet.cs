#region Using Statements
using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;

#endregion

namespace ZombieAssault
{
	public class TileSet
	{
		public Texture2D TileSetTexture;
		public BlendState BlendMode = BlendState.AlphaBlend;
		public int TileWidth = 32;
		public int TileHeight = 32;

		public Rectangle GetSourceRectangle(int tileIndex)
		{
			int tileX = tileIndex % (TileSetTexture.Width / TileWidth);
			int tileY = tileIndex / (TileSetTexture.Width / TileWidth);

			return new Rectangle(tileX * (TileWidth+2) + 1, tileY * (TileHeight+2) + 1, TileWidth, TileHeight);
		}

		public TileSet (Texture2D texture, BlendState blendState = null)
		{
			TileSetTexture = texture;
			if (blendState != null)
				BlendMode = blendState;
		}
		
	}
}

