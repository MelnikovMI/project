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
    }
}
