﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Hi_Tech_Order_Management_System.GUI;

namespace Hi_Tech_Order_Management_System
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormLogin());
            //Application.Run(new FormUserEmployee());
            //Application.Run(new FormOrder());
            // Application.Run(new FormBook());
            //Application.Run(new FormCustomer());
        }
    }
}