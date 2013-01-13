using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace ZombieAssault
{
	public class MapCell
	{
		public int TileID { get; set; }
		
		public MapCell (int tileID)
		{
			TileID = tileID;
		}
	}
}

