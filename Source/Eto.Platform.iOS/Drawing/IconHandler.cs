using System;
using System.IO;
using Eto.Drawing;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using SD = System.Drawing;
using MonoTouch.CoreGraphics;

namespace Eto.Platform.iOS.Drawing
{
	public class IconHandler : ImageHandler<UIImage, Icon>, IIcon
	{
		public IconHandler()
		{
		}
		
		public IconHandler(UIImage image)
		{
			Control = image;
		}
		
		public void Create (Stream stream)
		{
			var data = NSData.FromStream(stream);
			Control = new UIImage (data);
		}

		public void Create (string fileName)
		{
			Control = new UIImage (fileName);
		}

		public override Size Size {
			get {
				return Control.Size.ToEtoSize ();
			}
		}

		public int Width
		{
			get { return 0;/* TODO */ }
		}
		
		public int Height
		{
			get { return 0;/* TODO */ }
		}

		public override UIImage GetUIImage ()
		{
			return this.Control;
		}

		public override void DrawImage (GraphicsHandler graphics, Rectangle source, Rectangle destination)
		{
			//var sourceRect = graphics.Translate(Generator.ConvertF(source), nsimage.Size.Height);
			SD.RectangleF destRect = graphics.TranslateView (destination.ToSDRectangleF (), false);
			if (source.TopLeft != Point.Empty || sourceRect.Size != nsimage.Size) {
				graphics.Control.SaveState ();
				//graphics.Context.ClipToRect(destRect);
				if (graphics.Flipped) {
					graphics.Control.TranslateCTM (0, nsimage.Size.Height);
					graphics.Control.ScaleCTM (nsimage.Size.Width / destRect.Width, -(nsimage.Size.Height / destRect.Height));
				} else {
					graphics.Control.ScaleCTM (nsimage.Size.Width / destRect.Width, nsimage.Size.Height / destRect.Height);
				}
				graphics.Control.DrawImage (new SD.RectangleF (SD.PointF.Empty, destRect.Size), nsimage.CGImage);
				//nsimage.CGImage(destRect, CGBlendMode.Normal, 1);
				
				graphics.Control.RestoreState ();
				
				//var imgportion = nsimage..CGImage.WithImageInRect(sourceRect);
				/*graphics.Context.SaveState();
				if (graphics.Flipped) {
					graphics.Context.TranslateCTM(0, destRect.Bottom);
					graphics.Context.ScaleCTM(1.0F, -1.0F);
				}*/
				//var context = graphics.ControlObject as CGContext;
				//Console.WriteLine("drawing source:{0} dest:{1}", source, destRect);
				//graphics.Context.DrawImage(destRect, imgportion);
				
				//nsimage = UIImage.FromImage(imgportion);
				//nsimage.Draw(destRect, CGBlendMode.Normal, 1);
				
				//imgportion.Dispose();
				//nsimage.Dispose();
				//graphics.Context.RestoreState();
			} else {
				//graphics.Context.DrawImage(destRect, nsimage.CGImage);
				//Console.WriteLine("drawing full image");	
				nsimage.Draw (destRect, CGBlendMode.Normal, 1);
			}
		}
	}
}
