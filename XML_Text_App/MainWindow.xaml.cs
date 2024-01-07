using System.Printing;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace XML_Text_App
{

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void GumbOboji_Click(object sender, RoutedEventArgs e)
        {
            ClearOutputBox();
            TransformText();
        }

        private void TransformText()
        {
            TextRange inputRange = new TextRange(inputText.Document.ContentStart, this.inputText.Document.ContentEnd);

            string[] splitTags = Regex.Split(inputRange.Text, @"(<[^>]+>)"); //split by tags (<tag, else>)
            foreach (string tag in splitTags)
            {
                if (tag != null)
                {
                    if ((tag.StartsWith("<") || tag.StartsWith("<\\")) && tag.EndsWith(">"))
                    {
                        string[] splitAtt = Regex.Split(tag, @"([\b\w\b]+=)|(\??>|>)"); //split everything

                        foreach (string part in splitAtt)
                        {
                            AppendAndColorRTB_Tags(part);
                        }
                    }
                    else
                    {
                        AppendAndColorRTB_Text(tag);
                    }
                }
            }

        }

        private void AppendAndColorRTB_Tags(string text)
        {
            if (text != "")
            {
                TextRange range = new TextRange(outputText.Document.ContentEnd,
                    outputText.Document.ContentEnd);
                range.Text = text;

                if (text.StartsWith("<") || text.StartsWith(">") || text.StartsWith("?>"))
                {
                    range.ApplyPropertyValue(TextElement.ForegroundProperty, "Blue");
                }
                else if (text.StartsWith("/"))
                {
                    range.ApplyPropertyValue(TextElement.ForegroundProperty, "Blue");
                }
                else if (text.EndsWith("="))
                {
                    range.ApplyPropertyValue(TextElement.ForegroundProperty, "Red");
                }
                else if (text.StartsWith("\"") && (text.EndsWith("\"") || text.EndsWith(" ")))
                {
                    range.ApplyPropertyValue(TextElement.ForegroundProperty, "Brown");
                }
            }
        }

        private void AppendAndColorRTB_Text(string text)
        {
            if (text != "")
            {
                TextRange range = new TextRange(outputText.Document.ContentEnd,
                    outputText.Document.ContentEnd);
                range.Text = text;

                range.ApplyPropertyValue(TextElement.ForegroundProperty, "Green");
            }
        }

        private void ClearOutputBox()
        {
            TextRange range = new TextRange(outputText.Document.ContentStart,
                outputText.Document.ContentEnd);
            range.Text = "";
        }
    }
}