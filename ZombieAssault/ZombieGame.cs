#region Using Statements
using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;

#endregion

namespace ZombieAssault
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	public class ZombieGame : Game
	{
		GraphicsDeviceManager graphics;
		SpriteBatch spriteBatch;

		TileMap level = new TileMap();
		TileSet tileSet;
		TileSet mobTileSet;
		Viewport mainView;
		Camera camera = new Camera();

		public ZombieGame ()
		{
			graphics = new GraphicsDeviceManager (this);
			Content.RootDirectory = "Content";	            
			//graphics.PreferredBackBufferWidth = 1024;
			//graphics.PreferredBackBufferHeight = 768;

			graphics.IsFullScreen = true;		
		}

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize ()
		{
			// TODO: Add your initialization logic here
			if (graphics.IsFullScreen) {
				graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
				graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
			}
			base.Initialize ();

			mainView = graphics.GraphicsDevice.Viewport;
			
			Console.WriteLine("{0}, {1}, {2}, {3}", mainView.X, mainView.Y, mainView.Width, mainView.Height);
			
			camera.Location = new Vector2(level.Width/2, level.Height/2);

		}

		/// <summary>
		/// LoadContent will be called once per game and is the place to load
		/// all of your content.
		/// </summary>
		protected override void LoadContent ()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch (GraphicsDevice);

			//TODO: use this.Content to load your game content here 
			tileSet = new TileSet(Content.Load<Texture2D>("Tiles"));
			mobTileSet = new TileSet(Content.Load<Texture2D>("Tiles"), BlendState.NonPremultiplied);
			Console.WriteLine("{0}, {1}, {2}, {3}", GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height, 0, 0);

		}

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update (GameTime gameTime)
		{
			// For Mobile devices, this logic will close the Game when the Back button is pressed
			if (GamePad.GetState (PlayerIndex.One).Buttons.Back == ButtonState.Pressed) {
				Exit ();
			}

			KeyboardState ks = Keyboard.GetState ();
			if (ks.IsKeyDown (Keys.Left)) {
				camera.Location.X = MathHelper.Clamp (camera.Location.X - 2, 0, level.MapWidth * tileSet.TileWidth);
			}
			if (ks.IsKeyDown (Keys.Right)) {
				camera.Location.X = MathHelper.Clamp (camera.Location.X + 2, 0, level.MapWidth * tileSet.TileWidth);
			}
			if (ks.IsKeyDown (Keys.Up)) {
				camera.Location.Y = MathHelper.Clamp (camera.Location.Y - 2, 0, level.MapHeight * tileSet.TileHeight);
			}
			if (ks.IsKeyDown (Keys.Down)) {
				camera.Location.Y = MathHelper.Clamp (camera.Location.Y + 2, 0, level.MapHeight * tileSet.TileHeight);
			}

			// TODO: Add your update logic here

			base.Update (gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw (GameTime gameTime)
		{
			graphics.GraphicsDevice.Clear (Color.CornflowerBlue);

			graphics.GraphicsDevice.Viewport = mainView;
		
			//TODO: Add your drawing code here
			//spriteBatch.Begin (SpriteSortMode.FrontToBack, BlendState.NonPremultiplied); -- use this for adding mobs to map
			spriteBatch.Begin (SpriteSortMode.FrontToBack, tileSet.BlendMode);
			//spriteBatch.Begin ();

			level.Draw(mainView, camera, tileSet, spriteBatch);


			spriteBatch.End();

			base.Draw (gameTime);
		}
	}
}

