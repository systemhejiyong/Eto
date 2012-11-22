using System;
using System.IO;
using Eto.Drawing;
using MonoTouch.UIKit;

namespace Eto.Platform.iOS.Drawing
{
	interface IImageHandler
	{
		void DrawImage (GraphicsHandler graphics, int x, int y);

		void DrawImage (GraphicsHandler graphics, int x, int y, int width, int height);

		void DrawImage (GraphicsHandler graphics, Rectangle source, Rectangle destination);

		UIImage GetUIImage ();
	}

	public static class ImageExtensions
	{
		public static UIImage ToUIImage (this Image image)
		{
			if (image == null)
				return null;
			var handler = image.Handler as IImageHandler;
			if (handler != null)
				return handler.GetUIImage ();
			else
				return null;
		}
	}

	public abstract class ImageHandler<T, W> : WidgetHandler<T, W>, IImage, IImageHandler
		where T: class
		where W: Image
	{

		public abstract Size Size { get; }

		public virtual void DrawImage (GraphicsHandler graphics, int x, int y)
		{
			DrawImage (graphics, x, y, Size.Width, Size.Height);
		}

		public virtual void DrawImage (GraphicsHandler graphics, int x, int y, int width, int height)
		{
			DrawImage (graphics, new Rectangle (new Point (0, 0), Size), new Rectangle (x, y, width, height));
		}

		public abstract void DrawImage (GraphicsHandler graphics, Rectangle source, Rectangle destination);

		public abstract UIImage GetUIImage ();
		public int Width
		{
			get { return 0;/* TODO */ }
		}
		
		public int Height
		{
			get { return 0;/* TODO */ }
		}
	}
}
