using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LunaViewer01
{

    using System.Windows.Controls.Primitives;

    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        ToggleButton currentButton;

        string DummyRootDirectory = "C:/Users/h/Desktop/FIles/picture/城ケ崎姫子";
        //string DummyRootDirectory = "C:/Users/h/Downloads/temp";

        public MainWindow()
        {
            InitializeComponent();

            // 最大化表示
            this.WindowState = WindowState.Maximized;

            // UIスレッドで行う例
            /*var source = new BitmapImage();
            source.BeginInit();
            //source.UriSource = new Uri("file://C:/Users/h/Desktop/FIles/picture/城ケ崎姫子/134e3b8c15e8258e27261d155efa535a_600.jpg");
            source.UriSource = new Uri("file://C:/Users/h/Desktop/FIles/picture/城ケ崎姫子/51332128.jpeg");
            source.EndInit();

            MainImage.Source = source;*/



            //ボタン作る

            /*string[] files = System.IO.Directory.GetFiles(
            @"C:/Users/h/Desktop/FIles/picture/城ケ崎姫子", "*", System.IO.SearchOption.AllDirectories);*/

            string[] files = System.IO.Directory.GetFiles(
            string.Format(@"{0}", DummyRootDirectory), "*", System.IO.SearchOption.AllDirectories);
            int cnt = 0;
            foreach (string fles in files)
            {

                //if (cnt++ > 10) { break; }
                if (fles.IndexOf(".db") != -1) { continue; }

                var b = new ToggleButton
                {
                    //Content = fles,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Top,
                    //Margin = new Thickness(10, 10, 0, 0),
                    Width = 32,
                    Height = 32
                };

                {
                    /*StackPanel stackPnl = new StackPanel();
                    stackPnl.Orientation = Orientation.Horizontal;
                    stackPnl.Margin = new Thickness(10);*/

                    try
                    {
                        var inSource = new BitmapImage();
                        inSource.BeginInit();
                        inSource.UriSource = new Uri("file:///" + fles);
                        inSource.EndInit();
                        MainImage.Source = inSource;

                        ImageBrush myImageBrush = new ImageBrush(inSource);

                        /*Canvas myCanvas = new Canvas();
                        myCanvas.Width = 64;
                        myCanvas.Height = 64;
                        myCanvas.Background = myImageBrush;

                        stackPnl.Children.Add(myCanvas);*/

                        b.Background = myImageBrush;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error:" + fles);
                    }
                }

                b.Click += (s, e) =>
                {
                    // UIスレッドで行う例
                    var inSource = new BitmapImage();
                    inSource.BeginInit();
                    inSource.UriSource = new Uri("file:///" + fles);
                    inSource.EndInit();
                    MainImage.Source = inSource;

                    ToggleButton cButton = s as ToggleButton;

                    if (null != currentButton)
                    {
                        currentButton.IsChecked = false;

                    }

                    cButton.IsChecked = true;
                    currentButton = cButton;

                    //MessageBox.Show(fles);

                    //dummyScroll.ScrollToVerticalOffset(32*10);
                };

                dummyStack.Children.Add(b);
            }


        }
        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            //if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
            {
                if (e.Delta > 0)
                    dummyScroll.PageLeft();
                else
                    dummyScroll.PageRight();
                e.Handled = true;
            }
        }

    }


}
