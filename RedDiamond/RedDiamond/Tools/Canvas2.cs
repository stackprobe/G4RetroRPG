using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace Charlotte.Tools
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public class Canvas2
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private Image Image;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public Canvas2(int w, int h)
		{
			if (CanvasTools.IsFairImageSize(w, h) == false)
			{
				throw new ArgumentException();
			}
			this.Image = new Bitmap(w, h);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public Canvas2(String file)
			: this(File.ReadAllBytes(file))
		{ }

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public Canvas2(byte[] raw)
			: this(CanvasTools.GetImage(raw))
		{ }

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public Canvas2(Image image)
		{
			this.Image = image;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public int GetWidth()
		{
			return this.Image.Width;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public int GetHeight()
		{
			return this.Image.Height;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public Image GetImage()
		{
			return this.Image;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public byte[] GetBytes()
		{
			return CanvasTools.GetBytes(this.Image);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public byte[] GetBytes(ImageFormat format, int quality = -1)
		{
			return CanvasTools.GetBytes(this.Image, format, quality);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void Save(string file)
		{
			File.WriteAllBytes(file, this.GetBytes());
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void Save(string file, ImageFormat format)
		{
			File.WriteAllBytes(file, this.GetBytes(format));
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public Canvas ToCanvas()
		{
			return new Canvas(this.Image);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public bool AntiAliasing = true;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public Graphics GetGraphics()
		{
			Graphics g = Graphics.FromImage(this.Image);

			if (this.AntiAliasing)
			{
				g.TextRenderingHint = TextRenderingHint.AntiAlias;
				g.SmoothingMode = SmoothingMode.AntiAlias;
			}
			return g;
		}

		// memo: g.DrawString() の x, y は、描画した文字列の左上の座標っぽい。余白が入るので文字本体は座標より少し離れる。

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static double DRAW_STRING_DEFAULT_Y_RATE = -0.5;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void DrawString(String str, Font font, Color color, int x, int y, double xRate = -0.5)
		{
			this.DrawString(str, font, color, x, y, xRate, DRAW_STRING_DEFAULT_Y_RATE);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public void DrawString(String str, Font font, Color color, int x, int y, double xRate, double yRate)
		{
			using (Graphics g = this.GetGraphics())
			{
				SizeF size = g.MeasureString(str, font);

				g.DrawString(str, font, new SolidBrush(color), (float)(x + size.Width * xRate), (float)(y + size.Height * yRate));
			}
		}
	}
}
