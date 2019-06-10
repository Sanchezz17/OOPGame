using System.Drawing;
using System.Windows.Forms;

namespace App.Graphics
{
    /// <summary>
    /// Double Buffered layout panel - removes flicker during resize operations.
    /// </summary>
    public sealed class DBLayoutPanel : TableLayoutPanel
    {
        public DBLayoutPanel()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.UserPaint, true);
            ForeColor = Color.Transparent;
            BackColor = Color.Transparent;
        }
    }
}