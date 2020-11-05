using System.Collections.Generic;
using System.Windows.Controls;

namespace Learning_System_Algebra_logic
{
	internal delegate void changePage(Page page);

	internal delegate void startLoad();
	internal static class ManagerPage
	{
		private static readonly Dictionary<string, changePage> frames = new Dictionary<string, changePage>();
		private static startLoad _startLoad;
		private static startLoad _stopLoad;
		public static void ChangePage(string nameFrame, Page page)
		{
			frames[nameFrame](page);
		}
		public static void AddFrame(string nameFrame, changePage page)
		{
			if (frames.TryGetValue(nameFrame, out _)) DeleteFrame(nameFrame);
			frames.Add(nameFrame, page);
		}

		public static void SetSpinner(startLoad load, startLoad stopLoad)
		{
			_startLoad = load;
			_stopLoad = stopLoad;
		}
		public static void StartSpinner()
		{
			_startLoad();
		}
		public static void StopSpinner()
		{
			_stopLoad();
		}
		public static void DeleteFrame(string nameFrame)
		{
			if (frames.TryGetValue(nameFrame, out _)) frames.Remove(nameFrame);
		}
	}
}