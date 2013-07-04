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

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            sw.WriteLine(DateTime.UtcNow.ToString() + "\tПользователь закрыл приложение");
            if (Directory.Exists(path))
            {
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                foreach (FileInfo file in dirInfo.GetFiles())
                {
                    file.Delete();
                }
            }
            sw.Close();
            this.Close();
        }




















        private void start_Click(object sender, RoutedEventArgs e)
        {
            string input;
            Parser p = new Parser();
            Parser pp = new Parser();
            a1 = t_a.Text;
            b1 = t_b.Text;
            X0 = t_x0.Text;
            Y0 = t_y0.Text;
            variable = t_variable.Text;
            funk = t_function.Text;
            input = funk;
            if ((funk != "") || (a1 != "") || (b1 != "") || (X0 != "") || (Y0 != ""))
            {
                string sPattern = @"(^(\+|\-){0,1}\d+$)|(^(\+|\-){0,1}\d+(\.|\,){1}\d+(\*10\^{0,1}\({0,1}(\+|\-){0,1}\d*\){0,1}){0,1}$)|(^\({0,1}(\+|\-){0,1}\d+\/{1}\d+\){0,1}(\*10\^{0,1}\({0,1}(\+|\-){0,1}\d*\){0,1}){0,1}$)|(^(\+|\-){0,1}\d+(\*10\^{0,1}\({0,1}(\+|\-){0,1}\d*\){0,1}){0,1}$)|(^(10\^{0,1}\({0,1}(\+|\-){0,1}\d*\){0,1}){1}$)";
                if (Regex.IsMatch(a1, sPattern) && Regex.IsMatch(b1, sPattern) && Regex.IsMatch(X0, sPattern) && Regex.IsMatch(Y0, sPattern))
                {
                    string p1 = @"\.";
                    if (Regex.IsMatch(a1, p1))
                    {
                        a1 = Regex.Replace(a1, p1, ",");
                    }
                    if (Regex.IsMatch(b1, p1))
                    {
                        b1 = Regex.Replace(b1, p1, ",");
                    }
                    if (Regex.IsMatch(X0, p1))
                    {
                        X0 = Regex.Replace(X0, p1, ",");
                    }
                    if (Regex.IsMatch(Y0, p1))
                    {
                        Y0 = Regex.Replace(Y0, p1, ",");
                    }
                    string p2 = @"abs(.*)|acos(.*)|asin(.*)|atan(.*)|cos(.*)|cosh(.*)|floor(.*)|ln(.*)|log(.*)|sign(.*)|sin(.*)|sinh(.*)|qrt(.*)|tan(.*)|tanh(.*)";
                    if (Regex.IsMatch(variable, p2))
                    {
                        System.Windows.MessageBox.Show("Недопустимое имя переменной", "Ошибка!");
                    }
                    else
                    {
                        if (p.Evaluate(a1))
                        {
                            a = p.Result;
                        }
                        if (p.Evaluate(b1))
                        {
                            b = p.Result;
                        }
                        if (p.Evaluate(X0))
                        {
                            x0 = p.Result;
                        }
                        if (p.Evaluate(Y0))
                        {
                            y0 = p.Result;
                        }
                        sw.WriteLine(DateTime.UtcNow.ToString() + "\tПользователь ввел данные:\ta=" + a1 + "\tb=" + b1 + "\tx0=" + X0 + "\ty0=" + Y0 + "\tf(" + variable + ")=" + funk);
                        if (p.Evaluate(Regex.Replace(funk, variable, "(" + X0 + "," + Y0 + ")")))
                        {

                            start.Visibility = Visibility.Collapsed;
                            Charts.Visibility = Visibility.Visible;
                            progressBar1.Visibility = Visibility.Visible;
                            if ((s_picture.IsChecked == true) || (s_word.IsChecked == true) || (s_txt.IsChecked == true))
                            {
                                save_path();
                            }
                            if (s_picture.IsChecked == true)
                            {
                                if (!(Directory.Exists(Path + path)))
                                {
                                    Directory.CreateDirectory(Path + path);
                                }
                                DirectoryInfo dirInfo = new DirectoryInfo(Path + path);
                                foreach (FileInfo file in dirInfo.GetFiles())
                                {
                                    file.Delete();
                                }
                            }
                            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
                            dispatcherTimer.Tick += new EventHandler(MetodAdamsa);
                            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
                            dispatcherTimer.Start();

                        }
                        else
                        {
                            sw.WriteLine(DateTime.UtcNow.ToString() + "\tВведенная функция не может быть распознана");
                            System.Windows.MessageBox.Show("Введенная функция не может быть распознана. Проверьте правильность ввода.", "Ошибка!");
                        }
                    }
                }
                else
                {
                    sw.WriteLine(DateTime.UtcNow.ToString() + "\tДанные введены некорректно (неизвестный формат)");
                    System.Windows.MessageBox.Show("Данные введены некорректно (неизвестный формат)", "Ошибка!");
                }
            }
            else
            {
                sw.WriteLine(DateTime.UtcNow.ToString() + "\tДанные не были введены");
                System.Windows.MessageBox.Show("Введите данные", "Ошибка!");
            }
        }
    }
}
