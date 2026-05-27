using System;
using System.Drawing;
using System.Windows.Forms;

namespace DoAnGiaSu_WinForms.GUI
{
    public static class AdminCardFactory
    {
        public static FlowLayoutPanel CreateCardPanel()
        {
            return new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                WrapContents = true,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(12),
                BackColor = Color.WhiteSmoke
            };
        }
    }
}