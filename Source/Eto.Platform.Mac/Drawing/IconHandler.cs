using System;
using System.IO;
using Eto.Drawing;
using MonoMac.AppKit;
using MonoMac.Foundation;

namespace Eto.Platform.Mac.Drawing
{
	public class IconHandler : WidgetHandler<NSImage, Icon>, IIcon, IImageSource
	{
		public IconHandler()
		{
		}
		
		public IconHandler(NSImage image)
		{
			Control = image;
		}
		
		#region IIcon Members

		public void Create (Stream stream)
		{
			var data = NSData.FromStream(stream);
			Control = new NSImage (data);
		}

		public void Create (string fileName)
		{
			if (!File.Exists (fileName))
				throw new FileNotFoundException ("Icon not found", fileName);
			Control = new NSImage (fileName);
		}

		#endregion
		
		public Size Size {
			get { return Control.Size.ToEtoSize (); }
		}

		public NSImage GetImage ()
		{
			return Control;
		}

        #region IImage Members


        public int Width
        {
            get { return 0;/* TODO */ }
        }

        public int Height
        {
            get { return 0;/* TODO */ }
        }

        #endregion
    }
}
