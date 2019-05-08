using System.Drawing;
using System.Windows.Forms;

namespace OOP_Game
{
    public class FormUtils
    {
        public static DBLayoutPanel InitializeTableLayoutPanel(int rowsCount, int columnsCount)
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

        public static Button GetButtonWithTextAndFontColor(string text, Color fontColor, int fontSize)
        {
            var button = new Button();
            SetAnchorForAllSides(button);
            SetTextAndFont(button, text, fontColor, fontSize);
            return button;
        }

        public static Label GetLabelWithTextAndFontColor(string text, Color fontColor, int fontSize)
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
            FormUtils.SetAnchorForAllSides(transparentLabel);
            transparentLabel.BackColor = Color.Transparent;
            return transparentLabel;
        }

        public static void SetAnchorForAllSides(Control control)
        {
            control.Anchor = (AnchorStyles.Left | AnchorStyles.Right |
                              AnchorStyles.Top | AnchorStyles.Bottom);
        }

        public static void SetTextAndFont(Control control, string text, Color fontColor, int fontSize)
        {
            control.ForeColor = fontColor;
            control.Text = text;
            control.Font = new Font("Arial", fontSize);
        }
    }
}