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

        private double zoomValue = 1.0;
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
                    //Width = 32,
                    //Height = 32
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

                        //float n = inSource.PixelHeight > inSource.PixelWidth ? inSource.PixelHeight : inSource.PixelWidth;

                        //b.Width = inSource.PixelWidth / n * 32;
                        //b.Height = inSource.PixelHeight / n + 32;
                        b.Width = 32;
                        b.Height = 32;
                        
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
                    MainImage.Width = inSource.PixelWidth;// * 2;
                    MainImage.Height = inSource.PixelHeight;// * 2;*

                    //label_WidthNum.Content = inSource.PixelWidth;
                    label_WidthNum.Content = rate.Value;
                    label_HeightNum.Content = inSource.PixelHeight;

                    // canvasの拡大縮小
                    //rate.Value = 0;

                    rate.Value = 100;
                    //  マウスホイールのイベントを受け取り、スライダーをずらす
                    var index = rate.Ticks.IndexOf(rate.Value);
                    Double scale = rate.Ticks[index] / rate.Value;
                    rate.Value = rate.Ticks[index];

                    // canvasサイズの変更
                    canvas.Height *= scale;
                    canvas.Width *= scale;

                    //MainImage.Width = inSource.PixelWidth * scale;
                    //MainImage.Height = inSource.PixelHeight * scale;

                    // canvasの拡大縮小
                    Matrix m0 = new Matrix();
                    m0.Scale(rate.Value * 0.01, rate.Value * 0.01);//元のサイズとの比
                                                                   //m0.Transform(new Point(500, 500));
                                                                   //m0.Translate(200, 300);
                    matrixTransform.Matrix = m0;


                    scrollViewer.CaptureMouse();



                    ToggleButton cButton = s as ToggleButton;

                    if (null != currentButton)
                    {
                        currentButton.IsChecked = false;

                    }

                    cButton.IsChecked = true;
                    currentButton = cButton;

                   // zoomValue = 1.0f;

                    //ScaleTransform scale2= new ScaleTransform(zoomValue, zoomValue);
                    //MainImage.LayoutTransform = scale2;

                    //MessageBox.Show(fles);

                    //dummyScroll.ScrollToVerticalOffset(32*10);
                };

                dummyStack.Children.Add(b);
            }


        }

        /// <summary>
        /// 画像変更時のCanvasサイズの変更
        /// Scrollviewerのスクロールを出す用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void image_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            canvas.Height = e.NewSize.Height;
            canvas.Width = e.NewSize.Width;
        }

        private void canvas_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {

            if ((Keyboard.Modifiers & ModifierKeys.Control) <= 0)
            {
                return;
            }

            // 後続のイベントを実行しないための処理
            e.Handled = true;

            //  マウスホイールのイベントを受け取り、スライダーをずらす
            var index = rate.Ticks.IndexOf(rate.Value);
            if (0 < e.Delta)
            {
                index += 1;
            }
            else
            {
                index -= 1;
            }

            if (index < 0)
            {
                return;
            }
            else if (rate.Ticks.Count <= index)
            {
                return;
            }

            Double scale = rate.Ticks[index] / rate.Value;
            rate.Value = rate.Ticks[index];

            // canvasサイズの変更
            canvas.Height *= scale;
            canvas.Width *= scale;

            // canvasの拡大縮小
            Matrix m0 = new Matrix();
            m0.Scale(rate.Value * 0.01, rate.Value * 0.01);//元のサイズとの比
            //m0.Transform(new Point(500, 500));
            //m0.Translate(200, 300);
            matrixTransform.Matrix = m0;

            // scrollViewerのスクロールバーの位置をマウス位置を中心とする。
            Point mousePoint = e.GetPosition(scrollViewer);
            Double x_barOffset = (scrollViewer.HorizontalOffset + mousePoint.X) * scale - mousePoint.X;
            scrollViewer.ScrollToHorizontalOffset(x_barOffset);

            Double y_barOffset = (scrollViewer.VerticalOffset + mousePoint.Y) * scale - mousePoint.Y;
            scrollViewer.ScrollToVerticalOffset(y_barOffset);
        }

        Point scrollMousePoint = new Point();
        double hOff = 1;
        double vOff = 1;
        private void scrollViewer_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            scrollMousePoint = e.GetPosition(scrollViewer);
            hOff = scrollViewer.HorizontalOffset;
            vOff = scrollViewer.VerticalOffset;
            scrollViewer.CaptureMouse();
        }

        private void scrollViewer_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (scrollViewer.IsMouseCaptured)
            {
                scrollViewer.ScrollToHorizontalOffset(hOff + (scrollMousePoint.X - e.GetPosition(scrollViewer).X));
                scrollViewer.ScrollToVerticalOffset(vOff + (scrollMousePoint.Y - e.GetPosition(scrollViewer).Y));
            }
        }

        private void scrollViewer_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            scrollViewer.ReleaseMouseCapture();
        }

    }


}
