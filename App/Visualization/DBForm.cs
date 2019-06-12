using System;
using System.Windows.Forms;

namespace App.Visualization
{
    public class DBForm : Form
    {
        public DBForm()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.UserPaint, true);
            VisibleChanged += OnVisibleChanged;
            Closed += OnExit;
        }
        
        private void OnExit(object sender, EventArgs e) => Application.Exit();
        
        private void OnVisibleChanged(object sender, EventArgs e)
        {
            if (Visible && Owner != null)
            {
                Location = Owner.Location;
                Size = Owner.Size;
                Owner = null;
            }
        }
    }
}