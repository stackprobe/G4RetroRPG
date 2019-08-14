﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using DxLibDLL;

namespace Charlotte.Common
{
	//
	//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
	//
	public static class GameDraw
	{
		// Extra >

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private class ExtraInfo
		{
			public GameTaskList TL = null;
			public bool BlendInv = false;
			public bool Mosaic = false;
			public bool IntPos = false;
			public bool IgnoreError = false;
			public int A = -1; // -1 == 無効
			public int BlendAdd = -1; // -1 == 無効
			public I3Color Bright = null;
		};

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private static ExtraInfo Extra = new ExtraInfo();

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void Reset()
		{
			Extra = new ExtraInfo();
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void SetTaskList(GameTaskList tl)
		{
			Extra.TL = tl;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void SetBlendInv()
		{
			Extra.BlendInv = true;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void SetMosaic()
		{
			Extra.Mosaic = true;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void SetIntPos()
		{
			Extra.IntPos = true;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void SetIgnoreError()
		{
			Extra.IgnoreError = true;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void SetAlpha(double a)
		{
			int pal = DoubleTools.ToInt(a * 255.0);

			pal = IntTools.Range(pal, 0, 255);

			Extra.A = pal;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void SetBlendAdd(double a)
		{
			int pal = DoubleTools.ToInt(a * 255.0);

			pal = IntTools.Range(pal, 0, 255);

			Extra.BlendAdd = pal;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void SetBright(double r, double g, double b)
		{
			int pR = DoubleTools.ToInt(r * 255.0);
			int pG = DoubleTools.ToInt(g * 255.0);
			int pB = DoubleTools.ToInt(b * 255.0);

			pR = IntTools.Range(pR, 0, 255);
			pG = IntTools.Range(pG, 0, 255);
			pB = IntTools.Range(pB, 0, 255);

			Extra.Bright = new I3Color(pR, pG, pB);
		}

		// < Extra

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private interface ILayoutInfo
		{ }

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private class FreeInfo : ILayoutInfo
		{
			public double LTX;
			public double LTY;
			public double RTX;
			public double RTY;
			public double RBX;
			public double RBY;
			public double LBX;
			public double LBY;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private class RectInfo : ILayoutInfo
		{
			public double L;
			public double T;
			public double R;
			public double B;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private class SimpleInfo : ILayoutInfo
		{
			public double X;
			public double Y;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private class DrawInfo
		{
			public GamePicture Picture;
			public ILayoutInfo Layout;
			public ExtraInfo Extra;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private static void SetBlend(int mode, int pal)
		{
			if (DX.SetDrawBlendMode(mode, pal) != 0) // ? 失敗
				throw new GameError();
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private static void ResetBlend()
		{
			if (DX.SetDrawBlendMode(DX.DX_BLENDMODE_NOBLEND, 0) != 0) // ? 失敗
				throw new GameError();
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private static void SetBright(int r, int g, int b)
		{
			if (DX.SetDrawBright(r, g, b) != 0) // ? 失敗
				throw new GameError();
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private static void ResetBright()
		{
			if (DX.SetDrawBright(255, 255, 255) != 0) // ? 失敗
				throw new GameError();
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private static void DrawPicMain(DrawInfo info)
		{
			// app > @ enter DrawPicMain

			// < app

			if (info.Extra.A != -1)
			{
				SetBlend(DX.DX_BLENDMODE_ALPHA, info.Extra.A);
			}
			else if (info.Extra.BlendAdd != -1)
			{
				SetBlend(DX.DX_BLENDMODE_ADD, info.Extra.BlendAdd);
			}
			else if (info.Extra.BlendInv)
			{
				SetBlend(DX.DX_BLENDMODE_INVSRC, 255);
			}

			if (info.Extra.Mosaic)
			{
				DX.SetDrawMode(DX.DX_DRAWMODE_NEAREST);
			}
			if (info.Extra.Bright != null)
			{
				SetBright(info.Extra.Bright.R, info.Extra.Bright.G, info.Extra.Bright.B);
			}

			{
				FreeInfo u = info.Layout as FreeInfo;

				if (u != null)
				{
					if (
						info.Extra.IntPos ?
						DX.DrawModiGraph(
							DoubleTools.ToInt(u.LTX),
							DoubleTools.ToInt(u.LTY),
							DoubleTools.ToInt(u.RTX),
							DoubleTools.ToInt(u.RTY),
							DoubleTools.ToInt(u.RBX),
							DoubleTools.ToInt(u.RBY),
							DoubleTools.ToInt(u.LBX),
							DoubleTools.ToInt(u.LBY),
							info.Picture.GetHandle(),
							1
							)
							!= 0
						:
						DX.DrawModiGraphF(
							(float)u.LTX,
							(float)u.LTY,
							(float)u.RTX,
							(float)u.RTY,
							(float)u.RBX,
							(float)u.RBY,
							(float)u.LBX,
							(float)u.LBY,
							info.Picture.GetHandle(),
							1
							)
							!= 0
						)
					// ? 失敗
					{
						if (info.Extra.IgnoreError == false)
							throw new GameError();
					}
					goto endDraw;
				}
			}

			{
				RectInfo u = info.Layout as RectInfo;

				if (u != null)
				{
					if (
						info.Extra.IntPos ?
						DX.DrawExtendGraph(
							DoubleTools.ToInt(u.L),
							DoubleTools.ToInt(u.T),
							DoubleTools.ToInt(u.R),
							DoubleTools.ToInt(u.B),
							info.Picture.GetHandle(),
							1
							)
							!= 0
						:
						DX.DrawExtendGraphF(
							(float)u.L,
							(float)u.T,
							(float)u.R,
							(float)u.B,
							info.Picture.GetHandle(),
							1
							)
							!= 0
						)
					// ? 失敗
					{
						if (info.Extra.IgnoreError == false)
							throw new GameError();
					}
					goto endDraw;
				}
			}

			{
				SimpleInfo u = info.Layout as SimpleInfo;

				if (u != null)
				{
					if (
						info.Extra.IntPos ?
						DX.DrawGraph(
							DoubleTools.ToInt(u.X),
							DoubleTools.ToInt(u.Y),
							info.Picture.GetHandle(),
							1
							)
							!= 0
						:
						DX.DrawGraphF(
							(float)u.X,
							(float)u.Y,
							info.Picture.GetHandle(),
							1
							)
							!= 0
						)
					// ? 失敗
					{
						if (info.Extra.IgnoreError == false)
							throw new GameError();
					}
					goto endDraw;
				}
			}

			throw new GameError(); // ? 不明なレイアウト
		endDraw:

			if (info.Extra.A != -1 || info.Extra.BlendAdd != -1 || info.Extra.BlendInv)
			{
				ResetBlend();
			}
			if (info.Extra.Mosaic)
			{
				DX.SetDrawMode(DX.DX_DRAWMODE_BILINEAR);
			}
			if (info.Extra.Bright != null)
			{
				ResetBright();
			}

			// app > @ leave DrawPicMain

			{
				FreeInfo u = info.Layout as FreeInfo;

				if (u != null)
				{
					double l = u.LTX;
					double t = u.LTY;
					double r = u.LTX;
					double b = u.LTY;

					l = Math.Min(l, u.RTX);
					l = Math.Min(l, u.RBX);
					l = Math.Min(l, u.LBX);

					t = Math.Min(t, u.RTY);
					t = Math.Min(t, u.RBY);
					t = Math.Min(t, u.LBY);

					r = Math.Max(r, u.RTX);
					r = Math.Max(r, u.RBX);
					r = Math.Max(r, u.LBX);

					b = Math.Max(b, u.RTY);
					b = Math.Max(b, u.RBY);
					b = Math.Max(b, u.LBY);

					Charlotte.Game.NamedRect.LastDrawedRect = new Charlotte.Tools.D4Rect()
					{
						L = l,
						T = t,
						W = r - l,
						H = b - t,
					};

					goto endPostDraw;
				}
			}

			{
				RectInfo u = info.Layout as RectInfo;

				if (u != null)
				{
					double l = u.L;
					double t = u.T;
					double r = u.R;
					double b = u.B;

					Charlotte.Game.NamedRect.LastDrawedRect = new Charlotte.Tools.D4Rect()
					{
						L = l,
						T = t,
						W = r - l,
						H = b - t,
					};

					goto endPostDraw;
				}
			}

			{
				SimpleInfo u = info.Layout as SimpleInfo;

				if (u != null)
				{
					Charlotte.Game.NamedRect.LastDrawedRect = new Charlotte.Tools.D4Rect()
					{
						L = u.X,
						T = u.Y,
						W = info.Picture.Get_W(),
						H = info.Picture.Get_H(),
					};

					goto endPostDraw;
				}
			}

			throw new GameError(); // ? 不明なレイアウト
		endPostDraw:
			;

			// < app
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private class DrawPicTask : IGameTask
		{
			public DrawInfo Info;

			public bool Routine()
			{
				DrawPicMain(this.Info);
				return false;
			}

			public void Dispose()
			{
				// noop
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private static void DrawPic(GamePicture picture, ILayoutInfo layout_binding)
		{
			DrawInfo info = new DrawInfo()
			{
				Picture = picture,
				Layout = layout_binding,
				Extra = Extra,
			};

			if (Extra.TL == null)
			{
				DrawPicMain(info);
			}
			else
			{
				Extra.TL.Add(new DrawPicTask()
				{
					Info = info,
				});
			}
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void DrawFree(GamePicture picture, double ltx, double lty, double rtx, double rty, double rbx, double rby, double lbx, double lby)
		{
			FreeInfo u = new FreeInfo()
			{
				LTX = ltx,
				LTY = lty,
				RTX = rtx,
				RTY = rty,
				RBX = rbx,
				RBY = rby,
				LBX = lbx,
				LBY = lby,
			};

			DrawPic(picture, u);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void DrawRect_LTRB(GamePicture picture, double l, double t, double r, double b)
		{
			if (
				l < -(double)IntTools.IMAX || (double)IntTools.IMAX - 1.0 < l ||
				t < -(double)IntTools.IMAX || (double)IntTools.IMAX - 1.0 < t ||
				r < l + 1.0 || (double)IntTools.IMAX < r ||
				b < t + 1.0 || (double)IntTools.IMAX < b
				)
				throw new GameError();


			RectInfo u = new RectInfo()
			{
				L = l,
				T = t,
				R = r,
				B = b,
			};

			DrawPic(picture, u);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void DrawRect(GamePicture picture, double l, double t, double w, double h)
		{
			DrawRect_LTRB(picture, l, t, l + w, t + h);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void DrawSimple(GamePicture picture, double x, double y)
		{
			if (
				x < -(double)IntTools.IMAX || (double)IntTools.IMAX < x ||
				y < -(double)IntTools.IMAX || (double)IntTools.IMAX < y
				)
				throw new GameError();

			SimpleInfo u = new SimpleInfo()
			{
				X = x,
				Y = y,
			};

			DrawPic(picture, u);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void DrawCenter(GamePicture picture, double x, double y)
		{
			if (
				x < -(double)IntTools.IMAX || (double)IntTools.IMAX < x ||
				y < -(double)IntTools.IMAX || (double)IntTools.IMAX < y
				)
				throw new GameError();

			DrawBegin(picture, x, y);
			DrawEnd();
		}

		// DrawBegin ～ DrawEnd >

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private class DBInfo
		{
			public GamePicture Picture;
			public double X;
			public double Y;
			public FreeInfo Layout;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		private static DBInfo DB = null;

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void DrawBeginRect_LTRB(GamePicture picture, double l, double t, double r, double b)
		{
			DrawBeginRect(picture, l, t, r - l, b - t);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void DrawBeginRect(GamePicture picture, double l, double t, double w, double h)
		{
			DrawBegin(picture, l + w / 2.0, t + h / 2.0);
			DrawSetSize(w, h);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void DrawBegin(GamePicture picture, double x, double y)
		{
			if (DB != null)
				throw new GameError();

			double w = picture.Get_W();
			double h = picture.Get_H();

			w /= 2.0;
			h /= 2.0;

			DB = new DBInfo()
			{
				Picture = picture,
				X = x,
				Y = y,
				Layout = new FreeInfo()
				{
					LTX = -w,
					LTY = -h,
					RTX = w,
					RTY = -h,
					RBX = w,
					RBY = h,
					LBX = -w,
					LBY = h,
				},
			};
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void DrawSlide(double x, double y)
		{
			if (DB == null)
				throw new GameError();

			DB.Layout.LTX += x;
			DB.Layout.LTY += y;
			DB.Layout.RTX += x;
			DB.Layout.RTY += y;
			DB.Layout.RBX += x;
			DB.Layout.RBY += y;
			DB.Layout.LBX += x;
			DB.Layout.LBY += y;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void DrawRotate(double rot)
		{
			if (DB == null)
				throw new GameError();

			GameUtils.Rotate(ref DB.Layout.LTX, ref DB.Layout.LTY, rot);
			GameUtils.Rotate(ref DB.Layout.RTX, ref DB.Layout.RTY, rot);
			GameUtils.Rotate(ref DB.Layout.RBX, ref DB.Layout.RBY, rot);
			GameUtils.Rotate(ref DB.Layout.LBX, ref DB.Layout.LBY, rot);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void DrawZoom_X(double z)
		{
			if (DB == null)
				throw new GameError();

			DB.Layout.LTX *= z;
			DB.Layout.RTX *= z;
			DB.Layout.RBX *= z;
			DB.Layout.LBX *= z;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void DrawZoom_Y(double z)
		{
			if (DB == null)
				throw new GameError();

			DB.Layout.LTY *= z;
			DB.Layout.RTY *= z;
			DB.Layout.RBY *= z;
			DB.Layout.LBY *= z;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void DrawZoom(double z)
		{
			DrawZoom_X(z);
			DrawZoom_Y(z);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void DrawSetSize_W(double w)
		{
			if (DB == null)
				throw new GameError();

			w /= 2.0;

			DB.Layout.LTX = -w;
			DB.Layout.RTX = w;
			DB.Layout.RBX = w;
			DB.Layout.LBX = -w;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void DrawSetSize_H(double h)
		{
			if (DB == null)
				throw new GameError();

			h /= 2.0;

			DB.Layout.LTY = -h;
			DB.Layout.RTY = -h;
			DB.Layout.RBY = h;
			DB.Layout.LBY = h;
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void DrawSetSize(double w, double h)
		{
			DrawSetSize_W(w);
			DrawSetSize_H(h);
		}

		//
		//	copied the source file by https://github.com/stackprobe/Factory/blob/master/SubTools/CopyLib.c
		//
		public static void DrawEnd()
		{
			if (DB == null)
				throw new GameError();

			DB.Layout.LTX += DB.X;
			DB.Layout.LTY += DB.Y;
			DB.Layout.RTX += DB.X;
			DB.Layout.RTY += DB.Y;
			DB.Layout.RBX += DB.X;
			DB.Layout.RBY += DB.Y;
			DB.Layout.LBX += DB.X;
			DB.Layout.LBY += DB.Y;

			DrawPic(DB.Picture, DB.Layout);
			DB = null;
		}

		// < DrawBegin ～ DrawEnd
	}
}