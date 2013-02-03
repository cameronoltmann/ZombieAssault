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

		InputState input = new InputState(); 

		TileMap level = new TileMap();
		TileSet tileSet;
		TileSet mobTileSet;
		Viewport mainView;
		Camera2D camera = new Camera2D();
		float targetZoom = 1f;
		float lastZoomFactor = 1f;

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
			
			camera.Map = level;
			camera.Location = new Vector2(level.Width/2, level.Height/2);
			camera.MinZoom = .2f;

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

			input.Update ();

			// For Windows etc
			if (input.IsKeyDown (Buttons.Escape)) {
				this.Exit ();
			}

			int xPan = 0;
			int yPan = 0;
			int PanSpeed = Math.Max (1, (int)(8 / camera.Zoom));

			if (input.IsKeyDown (Buttons.Left) || input.IsKeyDown (Buttons.A)) {
				xPan -= PanSpeed;
			}
			if (input.IsKeyDown (Buttons.Right) || input.IsKeyDown (Buttons.D)) {
				xPan += PanSpeed;
			}
			if (input.IsKeyDown (Buttons.Up) || input.IsKeyDown (Buttons.W)) {
				yPan -= PanSpeed;
			}
			if (input.IsKeyDown (Buttons.Down) || input.IsKeyDown (Buttons.S)) {
				yPan += PanSpeed;
			}
			Vector3 move = Vector3.Transform (new Vector3 (xPan, yPan, 0), Matrix.CreateRotationZ (-camera.Rotation));
			camera.Move (new Vector2 (move.X, move.Y));
			float rot = 0f;
			float rotSpeed = (float)Math.PI / 32;
			if (input.IsKeyDown (Buttons.Q)) {
				rot += rotSpeed;
			}
			if (input.IsKeyDown (Buttons.E)) {
				rot -= rotSpeed;
			}
			camera.Rotation += rot;

			float zoomBy = 1.2f;
			if (input.Pressed (Buttons.MWUp)) {
				targetZoom *= zoomBy;
			} else if (input.Pressed (Buttons.MWDown)) {
				targetZoom /= zoomBy;
			}
			if (input.Pressed (Buttons.MiddleButton)) {
				targetZoom = 1f;
			}
			targetZoom = MathHelper.Clamp (targetZoom, camera.MinZoom, camera.MaxZoom);
			float zoomFactor = 1f;
			if (targetZoom > camera.Zoom) {
				zoomFactor = Math.Max (Math.Min (targetZoom/camera.Zoom,1.01f), Math.Min ((float)Math.Pow (targetZoom/camera.Zoom, .2f), lastZoomFactor*1.01f));
				camera.Zoom *= zoomFactor;
			} else if (targetZoom < camera.Zoom) {
				zoomFactor = 1/Math.Max (Math.Min (camera.Zoom/targetZoom,1.01f), Math.Min ((float)Math.Pow(camera.Zoom/targetZoom, .2f), 1/lastZoomFactor*1.01f));
				camera.Zoom *= zoomFactor;
			}
			lastZoomFactor = zoomFactor;
			//camera.Zoom = targetZoom;

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
			//spriteBatch.Begin (SpriteSortMode.FrontToBack, tileSet.BlendMode);
			spriteBatch.Begin (SpriteSortMode.FrontToBack,
			                   tileSet.BlendMode,
			                   null,
			                   null,
			                   null,
			                   null,
			                   camera.getTransformation(mainView));
			//spriteBatch.Begin ();

			camera.Render(mainView, tileSet, spriteBatch);
			spriteBatch.End();

			base.Draw (gameTime);
		}
	}
}

