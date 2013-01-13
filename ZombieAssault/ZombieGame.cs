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

		TileMap myMap = new TileMap();
		int squaresAcross = 10;
		int squaresDown = 10;
		TileSet tileSet;
		TileSet mobTileSet;
		Viewport mainView;

		public ZombieGame ()
		{
			graphics = new GraphicsDeviceManager (this);
			Content.RootDirectory = "Content";	            
			//graphics.IsFullScreen = true;		
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
			mainView = graphics.GraphicsDevice.Viewport;

			mainView.X = 16;
			mainView.Y = 16;
			mainView.Width = 320;
			mainView.Height = 320;
			base.Initialize ();
				
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
			squaresAcross = mainView.Width / tileSet.TileWidth + 1;
			squaresDown = mainView.Height / tileSet.TileHeight + 1;

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
				Camera.Location.X = MathHelper.Clamp (Camera.Location.X - 2, 0, (myMap.MapWidth - squaresAcross) * tileSet.TileWidth);
			}
			if (ks.IsKeyDown (Keys.Right)) {
				Camera.Location.X = MathHelper.Clamp (Camera.Location.X + 2, 0, (myMap.MapWidth - squaresAcross) * tileSet.TileWidth);
			}
			if (ks.IsKeyDown (Keys.Up)) {
				Camera.Location.Y = MathHelper.Clamp (Camera.Location.Y - 2, 0, (myMap.MapHeight - squaresDown) * tileSet.TileHeight);
			}
			if (ks.IsKeyDown (Keys.Down)) {
				Camera.Location.Y = MathHelper.Clamp (Camera.Location.Y + 2, 0, (myMap.MapHeight - squaresDown) * tileSet.TileHeight);
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

			Vector2 firstSquare = new Vector2 (Camera.Location.X / tileSet.TileWidth, Camera.Location.Y / tileSet.TileHeight);
			int firstX = (int)firstSquare.X;
			int firstY = (int)firstSquare.Y;

			Vector2 squareOffset = new Vector2 (Camera.Location.X % tileSet.TileWidth, Camera.Location.Y % tileSet.TileHeight);
			int offsetX = (int)squareOffset.X;
			int offsetY = (int)squareOffset.Y;

			for (int y = 0; y < squaresDown; y++)
			{
				for (int x = 0; x < squaresAcross; x++) {
					spriteBatch.Draw (
						tileSet.TileSetTexture,
						new Rectangle ((x * 32) - offsetX, (y * 32) - offsetY, 32, 32),
						tileSet.GetSourceRectangle (myMap.Rows[y + firstY].Columns [x + firstX].TileID),
						Color.Gray);
				}
			}

			spriteBatch.End();

			base.Draw (gameTime);
		}
	}
}

