using System;
using System.Windows.Forms;
using DoAnGiaSu_WinForms.GUI;

namespace DoAnGiaSu_WinForms
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new FormDangNhap());
        }
    }
}