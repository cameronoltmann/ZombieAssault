using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace ZombieAssault
{
	public class MapRow
	{
		public List<MapCell> Columns = new List<MapCell>();
	}
	
	public class TileMap
	{
		public List<MapRow> Rows = new List<MapRow>();
		public int MapWidth = 50;
		public int MapHeight = 50;
		public int TileWidth = 32;
		public int TileHeight = 32;
		public int Width;
		public int Height;
		float Scale = 1f;
		float ScaleMod = .01f;

		public TileMap ()
		{
			Width = MapWidth * TileWidth;
			Height = MapHeight * TileHeight; 

			for (int y=0; y< MapHeight; y++)
			{
				MapRow thisRow = new MapRow ();
				for (int x = 0; x < MapWidth; x++)
				{
					thisRow.Columns.Add (new MapCell (0));
				}
				Rows.Add (thisRow);
			}
			
			// Create Sample Map Data
			Rows[0].Columns[3].TileID = 23;
			Rows[0].Columns[4].TileID = 23;
			Rows[0].Columns[5].TileID = 1;
			Rows[0].Columns[6].TileID = 1;
			Rows[0].Columns[7].TileID = 1;
			
			Rows[1].Columns[3].TileID = 23;
			Rows[1].Columns[4].TileID = 1;
			Rows[1].Columns[5].TileID = 1;
			Rows[1].Columns[6].TileID = 1;
			Rows[1].Columns[7].TileID = 1;
			
			Rows[2].Columns[2].TileID = 23;
			Rows[2].Columns[3].TileID = 1;
			Rows[2].Columns[4].TileID = 1;
			Rows[2].Columns[5].TileID = 1;
			Rows[2].Columns[6].TileID = 1;
			Rows[2].Columns[7].TileID = 1;
			
			Rows[3].Columns[2].TileID = 23;
			Rows[3].Columns[3].TileID = 1;
			Rows[3].Columns[4].TileID = 1;
			Rows[3].Columns[5].TileID = 2;
			Rows[3].Columns[6].TileID = 2;
			Rows[3].Columns[7].TileID = 2;
			
			Rows[4].Columns[2].TileID = 23;
			Rows[4].Columns[3].TileID = 1;
			Rows[4].Columns[4].TileID = 1;
			Rows[4].Columns[5].TileID = 2;
			Rows[4].Columns[6].TileID = 2;
			Rows[4].Columns[7].TileID = 2;
			
			Rows[5].Columns[2].TileID = 23;
			Rows[5].Columns[3].TileID = 1;
			Rows[5].Columns[4].TileID = 1;
			Rows[5].Columns[5].TileID = 2;
			Rows[5].Columns[6].TileID = 2;
			Rows[5].Columns[7].TileID = 2;
		}

		public void Draw (Viewport viewport, TileSet tileSet, SpriteBatch spriteBatch)
		{

			for (int y = 0; y < MapHeight; y++) {
				for (int x = 0; x < MapWidth; x++) {
					spriteBatch.Draw (
						tileSet.TileSetTexture,
						new Rectangle(x * tileSet.TileWidth, y * tileSet.TileHeight, tileSet.TileWidth, tileSet.TileHeight),
						tileSet.GetSourceRectangle (Rows[y].Columns[x].TileID),
						Color.Gray);
				}
			}


		}
	}
}

