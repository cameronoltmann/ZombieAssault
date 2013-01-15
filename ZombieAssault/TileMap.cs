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

		public void Draw (Viewport viewport, Camera camera, TileSet tileSet, SpriteBatch spriteBatch)
		{

			int squaresAcross = (viewport.Width+tileSet.TileWidth-1)/tileSet.TileWidth + 1;
			int squaresDown = (viewport.Height+tileSet.TileHeight-1)/tileSet.TileHeight + 1;

			int left = (int)camera.Location.X - viewport.Width/2;
			int top = (int)camera.Location.Y - viewport.Height/2;

			int leftSquare = left/tileSet.TileWidth;
			int topSquare = top/tileSet.TileHeight;

			int xOffset = left % tileSet.TileWidth;
			int yOffset = top % tileSet.TileHeight;

			for (int y = Math.Max(0, topSquare); y < Math.Min(topSquare+squaresDown, MapHeight); y++) {
				for (int x = Math.Max(0, leftSquare); x < Math.Min(leftSquare+squaresAcross, MapWidth); x++) {
					spriteBatch.Draw (
						tileSet.TileSetTexture,
						new Rectangle ((x - leftSquare) * tileSet.TileWidth - xOffset, (y - topSquare) * tileSet.TileHeight - yOffset, tileSet.TileHeight, tileSet.TileWidth),
						tileSet.GetSourceRectangle (Rows[y].Columns[x].TileID),
						Color.Gray);
				}
			}


		}
	}
}

