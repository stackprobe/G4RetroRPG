using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace Charlotte.Tools
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public static class CanvasTools
	{
		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static bool IsFairImageSize(int w, int h)
		{
			return 1 <= w && w <= 10000 && 1 <= h && h <= 10000 && w * h <= 9000000; // max 10000 x 900, 3000 x 3000, 900 x 10000, etc.
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static Image GetImage(byte[] raw)
		{
			using (MemoryStream mem = new MemoryStream(raw))
			{
				return Bitmap.FromStream(mem);
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static byte[] GetBytes(Image image)
		{
			return GetBytes(image, ImageFormat.Png);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static byte[] GetBytes(Image image, ImageFormat format, int quality = -1)
		{
			// quarity: 0 ～ 100 == 低画質 ～ 高画質, -1 == 無効

			using (MemoryStream mem = new MemoryStream())
			{
				if (format == ImageFormat.Jpeg && quality != -1)
				{
					if (quality < 0 || 100 < quality)
						throw new ArgumentException("Bad quality: " + quality);

					using (EncoderParameters eps = new EncoderParameters(1))
					using (EncoderParameter ep = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, (long)quality))
					{
						eps.Param[0] = ep;
						ImageCodecInfo ici = GetICI(format);
						image.Save(mem, ici, eps);
					}
				}
				else
				{
					image.Save(mem, format);
				}
				return mem.ToArray();
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private static ImageCodecInfo GetICI(ImageFormat format)
		{
			return ImageCodecInfo.GetImageEncoders().First(ici => ici.FormatID == format.Guid);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static Color Cover(Color back, Color fore)
		{
			int fa = fore.A;
			int ba = back.A;

			ba = (int)((ba * (255 - fa)) / 255.0 + 0.5);

			return Color.FromArgb(
				ba + fa,
				(int)((ba * back.R + fa * fore.R) / (double)(ba + fa) + 0.5),
				(int)((ba * back.G + fa * fore.G) / (double)(ba + fa) + 0.5),
				(int)((ba * back.B + fa * fore.B) / (double)(ba + fa) + 0.5)
				);
		}
	}
}
