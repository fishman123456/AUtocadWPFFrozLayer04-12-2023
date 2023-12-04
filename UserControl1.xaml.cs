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

namespace AUtocadWPFFrozLayer04_12_2023
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : Window
    {
       
        public UserControl1()
        {
            InitializeComponent();
            WF1.ShowDialog();
        }
        // список куда складывать будем имена слоёв
        public List<string> list_lay_name = new List<string>();
        private void B0_Click(object sender, RoutedEventArgs e)
        {
            string[] separator = { "\n", "\r" };
            // добавляем данные в список из текстбокса TextBox_Lay_name 
            string[] massTextBoxLayName = lay.Text.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            try
            {

                foreach (var item in massTextBoxLayName)
                {
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void lay_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
