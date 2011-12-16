﻿using System;
using System.Windows.Forms;
using ProcessManagerUI.Forms;

namespace ProcessManagerUI
{
	static class Program
	{
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new ConfigurationForm());
		}
	}
}
