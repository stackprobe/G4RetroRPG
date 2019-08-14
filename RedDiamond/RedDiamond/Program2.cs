using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DxLibDLL;
using Charlotte.Common;
using Charlotte.Tools;
using Charlotte.Tests;
using Charlotte.Game;

namespace Charlotte
{
	public class Program2
	{
		public void Main2()
		{
			try
			{
				Main3();
			}
			catch (Exception e)
			{
				ProcMain.WriteLog(e);
			}
		}

		private void Main3()
		{
			GameAdditionalEvents.Ground_INIT = () =>
			{
				GameGround.RO_MouseDispMode = true;
			};

			GameAdditionalEvents.Ground_FNLZ = () =>
			{
				// none
			};

			GameAdditionalEvents.PostGameStart = () =>
			{
				// Font >

				GameFontRegister.Add(@"riitf\RiiT_F.otf");

				// < Font

				Ground.I = new Ground();
			};

			GameAdditionalEvents.Save = lines =>
			{
				lines.Add(DateTime.Now.ToString()); // Dummy
				lines.Add(DateTime.Now.ToString()); // Dummy
				lines.Add(DateTime.Now.ToString()); // Dummy

				lines.Add("" + (SaveData.HasSaveData ? 1 : 0));

				// 新しい項目をここへ追加...
			};

			GameAdditionalEvents.Load = lines =>
			{
				int c = 0;

				GameUtils.Noop(lines[c++]); // Dummy
				GameUtils.Noop(lines[c++]); // Dummy
				GameUtils.Noop(lines[c++]); // Dummy

				SaveData.HasSaveData = int.Parse(lines[c++]) != 0;

				// 新しい項目をここへ追加...
			};

			GameMain2.Perform(Main4);
		}

		private void Main4()
		{
			//new Test0001().Test01();
			Title.TitleMain();
		}
	}
}
