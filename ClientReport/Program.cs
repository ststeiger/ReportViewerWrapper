using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ClientReport
{
	static class Program
	{
		/// <summary>
		/// Der Haupteinstiegspunkt der Anwendung.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}
	}
}