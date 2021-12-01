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
    /// Логика взаимодействия для SearchTrainer.xaml
    /// </summary>
    public partial class SearchTrainer : Window
    {
        public SearchTrainer()
        {
            InitializeComponent();
            this.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Gym.СпортзалDataSet спортзалDataSet = ((Gym.СпортзалDataSet)(this.FindResource("спортзалDataSet")));
                Gym.СпортзалDataSetTableAdapters.ТренерTableAdapter спортзалDataSetТренерTableAdapter = new Gym.СпортзалDataSetTableAdapters.ТренерTableAdapter();
                спортзалDataSetТренерTableAdapter.Fill(спортзалDataSet.Тренер);
                System.Windows.Data.CollectionViewSource тренерViewSource = ((System.Windows.Data.CollectionViewSource)(this.FindResource("тренерViewSource")));
                тренерViewSource.View.MoveCurrentToFirst();
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