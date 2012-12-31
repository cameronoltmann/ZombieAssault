#region Using Statements
using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace ZombieAssault
{
	static class Program
	{
		private static ZombieGame game;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main ()
		{
			game = new ZombieGame ();
			game.Run ();
		}
	}
}
