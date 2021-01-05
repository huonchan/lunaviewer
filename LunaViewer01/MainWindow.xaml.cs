﻿using System;
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
                    MainImage.Height = inSource.PixelHeight;// * 2;

                    label_WidthNum.Content = inSource.PixelWidth;
                    label_HeightNum.Content = inSource.PixelHeight;


                    ToggleButton cButton = s as ToggleButton;

                    if (null != currentButton)
                    {
                        currentButton.IsChecked = false;

                    }

                    cButton.IsChecked = true;
                    currentButton = cButton;

                    zoomValue = 1.0f;

                    ScaleTransform scale = new ScaleTransform(zoomValue, zoomValue);
                    MainImage.LayoutTransform = scale;

                    //MessageBox.Show(fles);

                    //dummyScroll.ScrollToVerticalOffset(32*10);
                };

                dummyStack.Children.Add(b);
            }


        }

        //https://www.c-sharpcorner.com/forums/scroll-a-view-horizontally-using-the-mouse-wheel-in-wpf
        //todo:https://stackoverflow.com/questions/3727439/how-to-enable-horizontal-scrolling-with-mouse が使えるか確かめる
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

        private void scrollviewer_image_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            //if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
            {
                if (e.Delta > 0)
                {

                    zoomValue += 0.1;
                }

                else
                {
                    zoomValue -= 0.1;
                }

            }

            ScaleTransform scale = new ScaleTransform(zoomValue, zoomValue);
            MainImage.LayoutTransform = scale;
        }

    }


}
