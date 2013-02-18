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
			for (int y=0; y<MapHeight; y++) {
				for (int x=0; x<MapHeight; x++) {
					Rows[y].Columns[0].TileID = 1;
					Rows[y].Columns[MapWidth-1].TileID = 1;
					Rows[0].Columns[x].TileID = 1;
					Rows[MapWidth-1].Columns[x].TileID = 1;
				}
			}
					
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

