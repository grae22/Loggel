﻿using System;
using System.Windows.Forms;
using Loggella.UI;

namespace Loggella
{
  static class Program
  {
    //-------------------------------------------------------------------------

    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault( false );
      Application.Run( new MainForm() );
    }

    //-------------------------------------------------------------------------
  }
}
