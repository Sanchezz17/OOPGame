using System.Collections;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace App.Graphics
{
    public static class FormUtils
    {
        public static DBLayoutPanel GetTableLayoutPanel(int rowsCount, int columnsCount)
        {
            var panel = new DBLayoutPanel();
            
            panel.ColumnCount = columnsCount;
            var columnSizeInPercent = 100 / panel.ColumnCount;
            for (var i = 0; i < panel.ColumnCount; i++)
                panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, columnSizeInPercent));           
            
            panel.RowCount = rowsCount;
            var rowSizeInPercent = 100 / panel.RowCount;
            for (var i = 0; i < panel.RowCount; i++)
                panel.RowStyles.Add(new RowStyle(SizeType.Percent, rowSizeInPercent));

            SetAnchorForAllSides(panel);
            panel.Margin = Padding.Empty;
            return panel;
        }

        public static Button GetButtonWithTextAndFontColor(
            string text="", Color fontColor=default(Color), int fontSize=15)
        {
            var button = new Button();
            SetAnchorForAllSides(button);
            SetTextAndFont(button, text, fontColor, fontSize);
            return button;
        }

        public static Label GetLabelWithTextAndFontColor(
            string text="", Color fontColor=default(Color), int fontSize=15)
        {
            var label = new Label();
            SetAnchorForAllSides(label);
            SetTextAndFont(label, text, fontColor, fontSize);
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.BorderStyle = BorderStyle.FixedSingle;
            label.Margin = Padding.Empty;
            return label;
        }

        public static Label GetTransparentLabel()
        {
            var transparentLabel = new Label();
            SetAnchorForAllSides(transparentLabel);
            transparentLabel.BackColor = Color.Transparent;
            transparentLabel.Margin = Padding.Empty;
            return transparentLabel;
        }

        public static void SetAnchorForAllSides(Control control)
        {
            control.Anchor = AnchorStyles.Left | AnchorStyles.Right |
                              AnchorStyles.Top | AnchorStyles.Bottom;
        }

        private static void SetTextAndFont(Control control, string text, Color fontColor, int fontSize)
        {
            control.ForeColor = fontColor;
            control.Text = text;
            control.Font = new Font("Arial", fontSize, FontStyle.Bold);
        }

        public static void SplitColumnsByPercentages(
            TableLayoutColumnStyleCollection columnStyles, float[] percentages)
        {
            for (var i = 0; i < columnStyles.Count; i++)
                columnStyles[i] = new ColumnStyle(SizeType.Percent, percentages[i]);
        }
        
        public static void SplitRowsByPercentages(
            TableLayoutRowStyleCollection rowStyles, float[] percentages)
        {
            for (var i = 0; i < rowStyles.Count; i++)
                rowStyles[i] = new RowStyle(SizeType.Percent, percentages[i]);
        }
    }
}