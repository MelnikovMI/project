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



































        private void Help_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Программа предназначена для решения обыкновенных дифференциальных уравнений с заданной точностью.\n Поддерживается распознавание функций: abs, acos, asin, atan, cos, cosh, floor, ln, log, sign, sin, sinh, sqrt, tan, tanh. \n Поддерживается ввод данных из файла. Размещение данных в файле:\n 1-я строка - левая граница\n 2-я строка - правая граница\n 3-я сторка - x0\n 4-я сторка - y0\n 5-я сторка - имя переменной\n 6-я строка - выражение", "Справка");
        }

        public class ChartPoint
        {
            public double Value1 { get; set; }
            public double Value2 { get; set; }
        }
    }
}
