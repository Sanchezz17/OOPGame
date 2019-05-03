using System.Drawing;
using System.Windows.Forms;

namespace OOP_Game
{
    public class FormUtils
    {
        public static TableLayoutPanel InitializeTableLayoutPanel(int rowsCount, int columnsCount)
        {
            var panel = new TableLayoutPanel();
            
            panel.ColumnCount = columnsCount;
            var columnSizeInPercent = 100 / panel.ColumnCount;
            for (var i = 0; i < panel.ColumnCount; i++)
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, columnSizeInPercent));           
            
            panel.RowCount = rowsCount;
            var rowSizeInPercent = 100 / panel.RowCount;
            for (var i = 0; i < panel.RowCount; i++)
                panel.RowStyles.Add(new RowStyle(SizeType.Percent, rowSizeInPercent));

            panel.Anchor = (AnchorStyles.Left | AnchorStyles.Right |
                            AnchorStyles.Top | AnchorStyles.Bottom);
            return panel;
        }

        public static Button GetButtonWithTextAndFontColor(string text, Color fontColor, int fontSize)
        {
            var button = new Button
            {
                Anchor = (AnchorStyles.Left | AnchorStyles.Right | 
                          AnchorStyles.Top | AnchorStyles.Bottom),
                ForeColor = fontColor,
                Text = text,
                Font = new Font("Arial", fontSize),
                UseVisualStyleBackColor = true
            };
            return button;
        }
    }
}