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
    }
}
