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

using static System.Net.Mime.MediaTypeNames;

namespace AUtocadWPFFrozLayer04_12_2023
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : Window
    {
        // открываем видимость для обьекта класса ClassFrozen
        private ClassFrozen classFrozen;

        // поле для хранения 
        public UserControl1(ClassFrozen classFrozen)
        {
            InitializeComponent();
            this.classFrozen = classFrozen;
        }
        // список куда складывать будем имена слоёв
        public List<string> list_lay_name = new List<string>();
        //private void Image_Loaded(object sender, RoutedEventArgs e)
        //{
        //    // добавляем картинку
        //    // ... Create a new BitmapImage.
        //    System.Windows.Controls.Image myImage = new System.Windows.Controls.Image();
        //    myImage.Width = 200;

        //    BitmapImage b = new BitmapImage();
        //        b.BeginInit();
        //        b.UriSource = new Uri("2.jpg");
        //        b.EndInit();
        //    b.DecodePixelWidth = 200;
        //    myImage.Source = b; }
    
            public void picture ()
        {
            // версия 2
            BitmapImage image = new BitmapImage(new Uri("2.jpg", UriKind.Relative));
            img.Source = image;
        }

    // по клику кнопки собираем из тексбокса строки в список по \n\r
    private void B0_Click(object sender, RoutedEventArgs e)
    {

        // разделитель по строкам для заполнения списка
        string[] separator = { "\n", "\r" };
        // добавляем данные в список из текстбокса TextBox_Lay_name 
        string[] massTextBoxLayName = lay.Text.Split(separator, StringSplitOptions.RemoveEmptyEntries);
        try
        {
            // цикл для заполнения списка
            foreach (var item in massTextBoxLayName)
            {
                list_lay_name.Add(item);
                classFrozen.ClTransports.Add(item);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.ToString());
        }
        finally
        {
            // передаём спсок в метод для замораживания слоёв
            // очищаем список
            //list_lay_name.Clear();
        }
        this.DialogResult = true;
        this.Close();
    }
}
}
