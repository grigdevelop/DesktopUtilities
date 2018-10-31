using System;
using System.Text;
using System.Windows;
using System.Windows.Documents;

namespace RawHTMLEmptySpaceFiller
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string myText = new TextRange(InputTextBox.Document.ContentStart, InputTextBox.Document.ContentEnd).Text;
            string[] lines = myText.Split(new[] { "\n" }, StringSplitOptions.None);
            StringBuilder sb = new StringBuilder();
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    sb.AppendLine(line);
                    continue;
                }
                var nIndex = getIndexOfNonEmpty(line);
                if (nIndex == -1)
                {
                    sb.AppendLine(line);
                    continue;
                }

                string emptyPart = line.Substring(0, nIndex);
                string newString = "<span style=\"color: #569cd6;\">" + emptyPart + @"</span>";
                sb.AppendLine(newString + line.Substring(nIndex + 1));
            }

            string finalString = sb.ToString();

            InputTextBox.Document.Blocks.Clear();
            InputTextBox.Document.Blocks.Add(new Paragraph(new Run(finalString)));
        }

        private int getIndexOfNonEmpty(string input)
        {
            int index = -1;
            int i = 0;
            while (input[i] == ' ' && i < input.Length - 1)
            {
                index = i;
                i++;                
            }

            return index;
        }

    }
}
