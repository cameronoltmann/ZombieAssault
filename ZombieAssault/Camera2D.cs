#region Using Statements
using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Input;

#endregion

namespace ZombieAssault
{

	public class Camera2D
	{
		protected float _zoom;
		protected Vector2 _location;
		protected Matrix _transform;
		public float Rotation { get; set; }
		public float MinZoom { get; set; }
		public float MaxZoom { get; set; }
		public TileMap _map = null;

		protected Rectangle bounds;

		public Camera2D (TileMap tm = null){
			MinZoom = .1f;
			MaxZoom = 8f;
			Zoom = 1f;
			Rotation = 0f;
			Location = Vector2.Zero;
			Map = tm;
		}

		public float Zoom {
			get { return _zoom; }
			set {
				_zoom = MathHelper.Clamp(value, MinZoom, MaxZoom);
			}
		}

		public TileMap Map {
			get { return _map; }
			set {
				_map = value;
				if (_map != null) {
					bounds = new Rectangle(0,0,_map.Width, _map.Height);
				}
			}
		}

		protected Vector2 ClampToBounds (Vector2 loc) {
			return new Vector2(
				MathHelper.Clamp (loc.X, bounds.X, bounds.X+bounds.Width-1),
				MathHelper.Clamp (loc.Y, bounds.Y, bounds.Y+bounds.Height-1));
		}

		public Vector2 Location {
			get { return _location; }
			set {
				_location = ClampToBounds(value);
			}
		}

		public void Move (Vector2 amount){
			Location += amount;
		}

		public Matrix getTransformation (Viewport viewport){
			_transform = 
				Matrix.CreateTranslation (new Vector3 (-Location.X, -Location.Y, 1)) *
				Matrix.CreateRotationZ (Rotation) *
				Matrix.CreateScale (new Vector3 (Zoom, Zoom, 1)) *
				Matrix.CreateTranslation (new Vector3 (viewport.Width * 0.5f, viewport.Height * 0.5f, 0));
			return _transform;
		}

		public void Render (Viewport viewport, TileSet tileSet, SpriteBatch spriteBatch) {
			if (Map != null) {
				Map.Draw (viewport, tileSet, spriteBatch);
			}
		}

	}
}

