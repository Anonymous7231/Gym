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
using System.Windows.Shapes;

namespace Gym
{
    /// <summary>
    /// Логика взаимодействия для SearchClient.xaml
    /// </summary>
    public partial class SearchClient : Window
    {
        public SearchClient()
        {
            InitializeComponent();
            this.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Gym.СпортзалDataSet спортзалDataSet = ((Gym.СпортзалDataSet)(this.FindResource("спортзалDataSet")));
                Gym.СпортзалDataSetTableAdapters.КлиентTableAdapter спортзалDataSetКлиентTableAdapter = new Gym.СпортзалDataSetTableAdapters.КлиентTableAdapter();
                спортзалDataSetКлиентTableAdapter.Fill(спортзалDataSet.Клиент);
                System.Windows.Data.CollectionViewSource клиентViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("клиентViewSource")));
                клиентViewSource.View.MoveCurrentToFirst();
            }
            catch (Exception)
            {
            }
        }
        private void Exit(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.Hide();
                MainWindow A = new MainWindow();
                A.Show();
            }
        }
    }
}