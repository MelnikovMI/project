using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;
using Microsoft.Win32;
using System.Windows.Controls.DataVisualization.Charting;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.IO;
using System.IO.Log;
using System.Reflection;
using Word = Microsoft.Office.Interop.Word;
using Gif.Components;
using MathParser;

namespace proj
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }













































































































        public void gif()
        {
            int index;
            path = "picture";
            String outputFilePath = Path + "Решение.gif";
            AnimatedGifEncoder e = new AnimatedGifEncoder();
            e.Start(outputFilePath);
            e.SetDelay(500);
            e.SetRepeat(0);
            if (Directory.Exists(Path + path))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(Path + path);
                int tt = dirInfo.GetFiles().Length;
                for (index = 1; index <= tt; index++)
                {
                    e.AddFrame(System.Drawing.Image.FromFile(Path + path + "\\Graf" + index.ToString() + ".png"));
                }
            }
            e.Finish();

        }
    }
}
