using System.Text;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfUIApplication
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            MainViewModel mainViewModel = new MainViewModel();
            DataContext = mainViewModel;
        }

        /// <summary>
        /// 显示更多选项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoreButton_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)sender;
            string? tag = btn.Tag.ToString();

            Image image = (Image)btn.FindName("MoreImg");
            TextBlock text = (TextBlock)btn.FindName("MoreText");

            if (tag != null && tag.Equals("More"))
            {
                if (image != null)
                {
                    image.Source = (ImageSource)FindResource("LessOptions");
                }

                if (text != null)
                {
                    text.Text = "收起";
                }

                MyStar.Visibility = Visibility.Visible;
                DownloadManage.Visibility = Visibility.Visible;
                LocalMusic.Visibility = Visibility.Visible;
                CloudDisk.Visibility = Visibility.Visible;
                btn.Tag = "Collapsed";
            }
            else
            {
                if (image != null)
                {
                    image.Source = (ImageSource)FindResource("MoreOptionsLight");
                }

                if (text != null)
                {
                    text.Text = "更多";
                }

                MyStar.Visibility = Visibility.Collapsed;
                DownloadManage.Visibility = Visibility.Collapsed;
                LocalMusic.Visibility = Visibility.Collapsed;
                CloudDisk.Visibility = Visibility.Collapsed;
                btn.Tag = "More";
            }
        }
    }
}