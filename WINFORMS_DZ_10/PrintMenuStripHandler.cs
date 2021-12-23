using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WINFORMS_DZ_10
{
    public partial class MainForm : Form
    {
        List<string> _linesToPrint;

        private void InitializePrintMenuStrip()
        {
            var customizeMenu = menuStrip1.Items.Add("Print");
            customizeMenu.Click += new EventHandler(printMenu_Click);
        }

        private void printMenu_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length < 1)
            {
                MessageBox.Show("Нечего печатать", "Ошибка");
                return;
            }

            _linesToPrint = new List<string>(textBox1.Text.Split('\n'));

            PrintDocument printer = new PrintDocument();
            printer.PrintPage += new PrintPageEventHandler(printer_PrintPage);
            printer.Print();
        }

        private void printer_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Событие вывода на печать страницы (PrintPage)
            // Чтобы получить пустой обработчик этого события можно дважды
            // щелкнуть на значке printDocument1 в дизайнере формы
            int leftMargin = e.MarginBounds.Left;
            int topMargin = e.MarginBounds.Top;
            Font font = CustomizationOptions.TextFont;
            float linesOnPage = e.MarginBounds.Height / font.GetHeight(e.Graphics);
            
            // Печатаем каждую строку файла
            for (int i = 0; i < linesOnPage; ++i)
            {
                if (_linesToPrint.Count < 1)
                    break;

                string toPrint = _linesToPrint[0];
                _linesToPrint.RemoveAt(0);

                // Для VB: If Строка Is Nothing Then Exit While
                float Y = topMargin + i * font.GetHeight(e.Graphics);
                // Печать строки
                e.Graphics.DrawString(toPrint, font, Brushes.Black,
                                  leftMargin, Y, new StringFormat());
            }
            // Печать следующей страницы, если есть еще строки файла
            e.HasMorePages = _linesToPrint.Count > 0;
        }
    }
}
