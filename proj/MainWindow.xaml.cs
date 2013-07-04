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


        public void save_word(double x)
        {
            Word.Application wordApplication = new Word.Application(); //объявили переменную типа Word
            Object template = Type.Missing;
            Object newTemplate = Type.Missing;
            Object documentType = Type.Missing;
            Object visible = Type.Missing;
            wordApplication.Documents.Add(ref template, ref newTemplate, ref documentType, ref visible);//добавили в проложение документ
            Word.Document doc = wordApplication.ActiveDocument;
            //wordApplication.Visible = true; //делаем что бы word не работал в фоновом режиме
            Object r = Type.Missing;
            Word.Paragraph par = doc.Content.Paragraphs.Add(ref r);//дабавляем в документ параграф
            Object missing = Type.Missing;
            Word.Range rng = doc.Range(ref missing, ref missing); //получаем текстовую область параграфа
            rng.Tables.Add(doc.Paragraphs[doc.Paragraphs.Count].Range, 9, 1, ref missing, ref missing);//вставляем в текстовую область таблицу
            Word.Table tbl = doc.Tables[doc.Tables.Count];//для удобства работы присваиваем таблицу переменной
            tbl.Cell(1, 1).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
            tbl.Cell(1, 1).Range.Text = "МЕТОД АДАМСА ДЛЯ РЕШЕНИЯ ОБЫНОВЕННЫХ ДИФФЕРЕНЦИАЛЬНЫХ УРАВНЕНИЙ";
            tbl.Cell(2, 1).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            tbl.Cell(2, 1).Range.Text = "Вы ввели уравнение f(" + variable + ")=" + funk;
            tbl.Cell(3, 1).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            tbl.Cell(3, 1).Range.Text = "Левая граница = " + a;
            tbl.Cell(4, 1).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            tbl.Cell(4, 1).Range.Text = "Правая граница = " + b;
            tbl.Cell(5, 1).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            tbl.Cell(5, 1).Range.Text = "x0 = " + X0;
            tbl.Cell(6, 1).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            tbl.Cell(6, 1).Range.Text = "y0 = " + Y0;
            tbl.Cell(7, 1).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            tbl.Cell(7, 1).Range.Text = "Ответ: " + variable + " : ";
            tbl.Cell(8, 1).Range.Paragraphs.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
            for (int i = 0; i < num_points; i++)
            {
                tbl.Cell(8, i).Range.Text = "( " + Convert.ToString(myX[i]) + " , " + Convert.ToString(myY[i]) + " )";
            }
            object fileName = Path + @"Решение.doc";
            doc.SaveAs(ref fileName,
            ref missing, ref missing, ref missing, ref missing, ref missing,
            ref missing, ref missing, ref missing, ref missing, ref missing,
            ref missing, ref missing, ref missing, ref missing, ref missing);
            //doc.Close(ref missing, ref missing, ref missing);
            //wordApplication.Quit();
        }



        public void save_txt(double x)
        {
            StreamWriter sw1 = new StreamWriter(Path + "Решение.txt", false);
            sw1.WriteLine("МЕТОД АДАМСА ДЛЯ РЕШЕНИЯ ОБЫНОВЕННЫХ ДИФФЕРЕНЦИАЛЬНЫХ УРАВНЕНИЙ\r\nВы ввели уравнение f(" + variable + ")=" + funk + "\r\nЛевая граница = " + a + "\r\nПравая граница = " + b + "\r\nx0=" + X0 + "\r\ny0=" + Y0 + "\r\nОтвет: " + variable + " :");
            for (int i = 0; i < num_points; i++)
            {
                sw1.WriteLine("( " + Convert.ToString(myX[i]) + " , " + Convert.ToString(myY[i]) + " )\r\n");
            }
            sw1.Close();
        }



        public void save_path()
        {

            System.Windows.Forms.FolderBrowserDialog OpenFolder = new System.Windows.Forms.FolderBrowserDialog();
            // Показываем надпись в наверху диалога. 
            OpenFolder.Description = "Выбор каталога";
            // Выбираем первоначальную папку. 
            OpenFolder.SelectedPath = @"D:\";
            if (OpenFolder.ShowDialog() != 0)
            {
                Path = OpenFolder.SelectedPath;
            }
        }
    }
}
