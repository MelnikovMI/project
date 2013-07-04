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
        string funk, variable;
        string flag = "textbox";
        string a1, b1, X0, Y0;
        string Path = @"D:\\";
        string path = "picture";
        double a, b, x0, y0;
        ArrayList myX = new ArrayList();
        ArrayList myY = new ArrayList();
        int num_points = 10, num_starting_points = 4;
        System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        StreamWriter sw = new StreamWriter(File.Open("log.file", FileMode.Append));

        public MainWindow()
        {
            InitializeComponent();
            Charts.Series.Clear();
            if (File.Exists("log.file"))
            {
            }
            else
            {
                StreamWriter sw = new StreamWriter("log.file", true);
                sw.Close();
            }
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            sw.WriteLine(DateTime.UtcNow.ToString() + "\tПользователь перезапустил приложение");
            Charts.Series.Clear();
            dispatcherTimer.Stop();
            t_variable.Visibility = Visibility.Collapsed;
            t_function.Visibility = Visibility.Collapsed;
            start.Visibility = Visibility.Collapsed;
            radioButton_file.Visibility = Visibility.Visible;
            radioButton_TextBox.Visibility = Visibility.Visible;
            next.Visibility = Visibility.Visible;
            Charts.Visibility = Visibility.Collapsed;
            progressBar1.Visibility = Visibility.Collapsed;
            s_word.Visibility = Visibility.Collapsed;
            s_picture.Visibility = Visibility.Collapsed;
            s_txt.Visibility = Visibility.Collapsed;
            flag = "textbox";
            t_function.Text = "";
            t_a.Text = "";
            t_b.Text = "";
            t_variable.Text = "";
            t_x0.Text = "";
            t_y0.Text = "";
            funk = "";
            variable = "";
            a1 = "";
            b1 = "";
            X0 = "";
            Y0 = "";
            path = "picture";
            s_picture.IsChecked = false;
            s_txt.IsChecked = false;
            s_word.IsChecked = false;
        }

    }
}
