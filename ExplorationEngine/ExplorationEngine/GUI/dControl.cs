#region "Using Statements"
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
#endregion

namespace ExplorationEngine.GUI
{
	public abstract class dControl
	{
		public string name;

		public Rectangle positionSize;
		public Rectangle interactPositionSize;

		public Vector2 position;

		public Vector2 offset;
		public Vector2 _offset;

		public Vector2 size;
		public Vector2 interactSize;
		public Vector2 _size;

		public Vector2 scale = Vector2.One;
		public Vector2 _scale = Vector2.One;

		public Texture2D OriginalTexture;
		public Texture2D HoverTexture;
		public Texture2D PressTexture;
		public Texture2D ReleaseTexture;
		public Texture2D DisabledTexture;

		public SoundEffectInstance EnterSound = Engine.Sound_Click1.CreateInstance();
		public SoundEffectInstance LeaveSound;
		public SoundEffectInstance ReleaseSound = Engine.Sound_Click3.CreateInstance();
		public SoundEffectInstance PressSound = Engine.Sound_Click2.CreateInstance();
		public SoundEffectInstance DisabledRelease = Engine.Sound_Click5.CreateInstance();

		private Texture2D _texture;
		public Texture2D Texture
		{
			get { return _texture; }
			set
			{
				if (value != null)
				{
					_texture = value;
				}
			}
		}

		public Rectangle source;

		public Color OriginalColor = Color.White;
		public Color HoverColor = Color.White;
		public Color PressColor = Color.White;
		public Color ReleaseColor = Color.White;
		public Color DisabledColor = Color.DimGray;

		private Color _color;
		public Color Color
		{
			get { return _color; }
			set 
			{
				//if (value != null)
				//{
					_color = value;
				//}
			}
		}

		public Color debugColor = Color.Red;

		public Vector2 textureOffset = Vector2.Zero;
		public Vector2 _textureOffset = Vector2.Zero;

		public dControl parent = null;

		public float alpha = 0;

		public float layer = 0;

		public bool PlaySound = false;
		public bool Active = false;
		public bool UserInteract = true;

		public bool DrawOnDebug = true;
		public bool BlocksParentInteraction = true;

		//Resolution stuff
		public Vector2 WindowScale;
		public bool CenteredX;
		public bool CenteredY;

		//Children controls
		public List<dControl> children = new List<dControl>();
		public List<dControl> childrenToDelete = new List<dControl>();
		public dControl AChildThatContainsMouse = null;




		//Events
		//public delegate void Handler(dControl sender);
		public Engine.Handler OnMousePress;
		public Engine.Handler OnMouseRelease;
		//public Handler onMouseMove;
		public Engine.Handler OnMouseEnter;
		public Engine.Handler OnMouseLeave;

		//Event Variables
		public bool ContainedMouse = false;
		public bool ContainsMouse = false;
		public bool MouseWasReleasedWithin = false;
		public bool MouseWasPressedWithin = false;
		public bool MouseIsPressedWithin = false;
		public bool IsClosing = false;


		//Other
		public string DebugString = string.Empty;



		public dControl(string name, Vector2 position, Vector2 size, Texture2D texture, dControl parent, bool centeredX, bool centeredY)
		{
			_color = Color.White;

			this.name = name;
			this.CenteredX = centeredX;
			this.CenteredY = centeredY;
			this.offset = (parent != null ? parent.position + position : position);
			this._offset = offset;
			this.size = new Vector2((size.X > 0 ? (int)size.X : (texture != null ? texture.Width : 0)), (size.Y > 0 ? (int)size.Y : (texture != null ? texture.Height : 0)));
			this._size = this.size;
			this.OriginalTexture = texture;
			this.Texture = texture;
			this.source = new Rectangle(0, 0, (texture != null ? texture.Width : (int)size.X), (texture != null ? texture.Height : (int)size.Y));
			this.parent = parent;
			//this.Active = true;
		}


		public virtual void AddControl(dControl child)
		{
			children.Add(child);
		}
		public virtual void DeleteComponent(dControl child)
		{
			childrenToDelete.Add(child);
		}


		public virtual void SetActive(bool active, bool setchildren, bool quick)
		{
			if (setchildren)
			{
				for (int ii = 0; ii < children.Count; ii++)
				{
					children[ii].SetActive(active, setchildren, quick);
				}
			}

			if (quick)
				alpha = (active ? 1 : 0);

			Active = active;

		}

		public virtual void SetUserInteract(bool interact, bool setchildren)
		{
			if (setchildren)
			{
				for (int ii = 0; ii < children.Count; ii++)
				{
					//Set interactivity
					children[ii].SetUserInteract(interact, setchildren);
				}
			}

			UserInteract = interact;
		}


		public virtual void Close()
		{
			//Active = false;
			IsClosing = true;

			//Close its children
			foreach (dControl child in children)
			{
				child.Close();
			}

			//fade the window first
			if (alpha <= 0)
			{
				//close the children who are to be deleted
				foreach (dControl child in children)
				{
					childrenToDelete.Add(child);
				}

			}
		}


		//Simulate a click
		public virtual void Click()
		{
			if (UserInteract)
			{
				if (OnMouseRelease != null)
				{
					OnMouseRelease(this);
				}

				if (PlaySound && ReleaseSound != null)
					ReleaseSound.Play();
			}
			else
			{
				if (PlaySound && DisabledRelease != null)
					DisabledRelease.Play();
			}
		}


		public virtual void Update()
		{
			//Update scale resolution change
			WindowScale = offset / new Vector2(1280, 720);
			_offset = (parent != null ? (parent.parent != null ? offset : parent.size * WindowScale) : Engine.CurrentScreenResolution * WindowScale);

			//Fade windows in or out
			alpha += (alpha < 1f && !IsClosing && Active ? 0.01f : (IsClosing || !Active ? -0.01f : 0f));
			alpha = Math.Max(Math.Min(1, alpha), 0);
			if (alpha <= 0 && IsClosing) Close();

			//Layer
			//layer = (parent == null ? (Engine.ActiveForms.Count > 0 ? Engine.ActiveForms.IndexOf(this) / Engine.ActiveForms.Count : 0) : parent.layer + 0.001f);

			//Active statement
			if (Active || alpha > 0 && !IsClosing)
			{
				//Event variables
				ContainsMouse = interactPositionSize.Contains(new Rectangle(Input.LocalMouseState.X, Input.LocalMouseState.Y, 1, 1));
					if (ContainsMouse && parent != null) { parent.AChildThatContainsMouse = this; }
				MouseWasPressedWithin = ContainsMouse && Input.MouseLeftPressed && (parent != null ? parent.AChildThatContainsMouse == this : true);
				MouseWasReleasedWithin = ContainsMouse && Input.MouseLeftReleased && (parent != null ? parent.AChildThatContainsMouse == this : true);
				MouseIsPressedWithin = ContainsMouse && Input.MouseState(Input.LocalMouseState.LeftButton, ButtonState.Pressed) && (parent != null ? parent.AChildThatContainsMouse == this : true);

				//Update position, scale, and size
				if (parent != null)
				{
					_scale = parent.scale * scale;
					position = parent.position + _offset - new Vector2((CenteredX ? (size.X * _scale.X) / 2 : 0), CenteredY ? (size.Y * _scale.Y) / 2 : 0);
					_textureOffset = parent.textureOffset + textureOffset;
				}
				else
				{
					_textureOffset = textureOffset;
				}
				_size = size * scale;


				//Update positionsize rectangle
				positionSize = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y);
				interactPositionSize = (interactSize != Vector2.Zero && interactSize != null ? new Rectangle((int)position.X, (int)position.Y, (int)interactSize.X, (int)interactSize.Y) : positionSize);

				//Debug
				DebugString = "Name:" + name + "\n" +
					//"Layer:" + layer.ToString() + "\n" +
					"Alpha:" + alpha.ToString() + "\n" +
					"Size:" + size.X.ToString() + "," + size.Y.ToString() + "\n" +
					"Scale:" + scale.X.ToString() + "," + scale.Y.ToString() + "\n" +
					"Source:" + source.X.ToString() + "," + source.Y.ToString() + "," + source.Width.ToString() + "," + source.Height.ToString() + "\n" +
					//"_offset:" + _offset.X.ToString() + "," + _offset.Y.ToString() + "\n" +
					"Offset:" + offset.X.ToString() + "," + offset.Y.ToString() + "\n" +
					"Position:" + position.X.ToString() + "," + position.Y.ToString() + "\n" +
					"Children:" + children.Count.ToString() + "\n" +
					"CenteredX/Y:" + CenteredX.ToString() + "/" + CenteredY.ToString() + "\n" +
					"MouseWithin:" + ContainsMouse.ToString() + "\n" +
					"MousePress:" + MouseWasPressedWithin.ToString() + "\n" +
					"MouseRelease:" + MouseWasReleasedWithin.ToString() + "\n" +
					"MouseDown:" + MouseIsPressedWithin.ToString();

				debugColor = (ContainsMouse ? Color.Green : Color.Red);
				

				//Events
				if (UserInteract)
				{
					//OnRelease
					if (MouseWasReleasedWithin)
					{
						if (OnMouseRelease != null)
							OnMouseRelease(this);
						if (PlaySound && ReleaseSound != null)
							ReleaseSound.Play();
					}
					//OnPress
					if (MouseWasPressedWithin)
					{
						if (OnMousePress != null)
							OnMousePress(this);
						if (PlaySound && PressSound != null)
							PressSound.Play();
					}
				}

				//OnMouseEnter
				if (ContainsMouse && !ContainedMouse)
				{
					if (UserInteract)
					{
						if (OnMouseEnter != null)
						{
							ContainedMouse = true;
							OnMouseEnter(this);

							if (PlaySound && EnterSound != null)
								EnterSound.Play();
						}
					}

				}

				//OnMouseLeave
				if (!ContainsMouse && ContainedMouse)
				{
					if (OnMouseLeave != null)
					{
						ContainedMouse = false;
						OnMouseLeave(this);
					}
					if (PlaySound && LeaveSound != null)
						LeaveSound.Play();
				}
				//}


				//Delete children that need to be deleted
				for (int ii = 0; ii < childrenToDelete.Count; ii++)
				{
					children.Remove(childrenToDelete[ii]);
					childrenToDelete[ii] = null;
				}
				childrenToDelete.Clear();

				//Update children
				for (int ii = 0; ii < children.Count; ii++)
				{
					children[ii].Update();
				}
			}
			else
			{
				alpha = 0f;
			}

		}

		public virtual void Draw(SpriteBatch spritebatch)
		{
			if (Active || alpha > 0 && !IsClosing)
			{
				//Draw GUI object
				if (OriginalTexture != null)
				{
					spritebatch.Draw(Texture, new Rectangle((int)(position.X + _textureOffset.X), (int)(position.Y + _textureOffset.Y), (int)(_size.X), (int)(_size.Y)), source, _color * alpha, 0f, Vector2.Zero, SpriteEffects.None, layer);
				}

				//Debug drawing
				if (Engine.DebugGUI && DrawOnDebug)
				{
					spritebatch.Draw(Engine.Square, new Rectangle((int)(position.X), (int)(position.Y), (int)_size.X, (int)_size.Y), source, debugColor * 0.2f, 0f, Vector2.Zero, SpriteEffects.None, layer);
					spritebatch.Draw(Engine.Square, interactPositionSize, source, Color.Yellow * 0.2f, 0f, Vector2.Zero, SpriteEffects.None, layer);

					spritebatch.DrawString(Engine.Font_Small, DebugString, new Vector2(position.X + _size.X, position.Y), debugColor, 0f, Vector2.Zero, 1f, SpriteEffects.None, layer);
				}

				//Draw children
				for (int ii = 0; ii < children.Count; ii++)
				{
					children[ii].Draw(spritebatch);
				}
			}
		}
	}
}
