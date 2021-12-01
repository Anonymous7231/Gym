using System;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using System.IO;
using System.Windows.Media.Imaging;

namespace Gym
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    class Trainer
    {
        static int id;
        static string fio;
        static int age;
        static char gender;
        public static int ID
        {
            get { return id; }
            set { }
        }
        public static string FIO
        {
            get { return fio; }
            set { }
        }
        public static int AGE
        {
            get { return age; }
            set { }
        }
        public static char GENDER
        {
            get { return gender; }
            set { }
        }
    }
    class Client
    {
        static int id;
        static string fio;
        static int age;
        static char gender;
        static int trainer_id;
        public static int ID
        {
            get { return id; }
            set { }
        }
        public static string FIO
        {
            get { return fio; }
            set { }
        }
        public static int AGE
        {
            get { return age; }
            set { }
        }
        public static char GENDER
        {
            get { return gender; }
            set { }
        }
        public static int TRAINER_ID
        {
            get { return trainer_id; }
            set { }
        }
    }
    public partial class MainWindow : Window
    {
        string connectionString, connectionString2, connectionString3, connectionString4;
        SqlDataAdapter adapter, adapter2;
        System.Data.DataTable trainertable, clienttable;
        SqlConnection connection2 = null;
        public MainWindow()
        {
            InitializeComponent();
            this.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            connectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Спортзал;Integrated Security=True";
            SqlConnection connection = null;
            connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
                connectionStringsSection.ConnectionStrings["DefaultConnection"].ConnectionString = "Data Source=.\\SQLEXPRESS;Initial Catalog=Спортзал;Integrated Security=True";
                config.Save();
                ConfigurationManager.RefreshSection("connectionStrings");
            }
            catch
            {
                try
                {
                    connectionString2 = "Data Source=.;Initial Catalog=Спортзал;Integrated Security=True";
                    connection2 = new SqlConnection(connectionString2);
                    connection2.Open();
                    var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    var connectionStringsSection = (ConnectionStringsSection)config.GetSection("connectionStrings");
                    connectionStringsSection.ConnectionStrings["DefaultConnection"].ConnectionString = "Data Source=.;Initial Catalog=Спортзал;Integrated Security=True";
                    config.Save();
                    ConfigurationManager.RefreshSection("connectionStrings");
                }
                catch
                {
                    MessageBox.Show("Не удалось установить соединение с базой данных.", "ВНИМАНИЕ!");
                }
                finally
                {
                    connection2.Close();
                }
            }
            finally
            {
                connection.Close();
            }
            try
            {
                connectionString3 = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                TrainerGrid.RowEditEnding += TrainerGrid_RowEditEnding;
                ClientGrid.RowEditEnding += ClientGrid_RowEditEnding;
            }
            catch (Exception)
            {
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string sql = "SELECT * FROM Тренер";
            trainertable = new System.Data.DataTable();
            SqlConnection connection = null;
            string sql2 = "SELECT * FROM Клиент";
            clienttable = new System.Data.DataTable();
            SqlConnection connection2 = null;
            try
            {
                connection = new SqlConnection(connectionString3);
                SqlCommand command = new SqlCommand(sql, connection);
                adapter = new SqlDataAdapter(command);
                adapter.InsertCommand = new SqlCommand("Ввод_Тренеров", connection);
                adapter.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Тренер_ФИО", SqlDbType.NVarChar, 50, "Тренер_ФИО"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Тренер_Возраст", SqlDbType.Int, 100, "Тренер_Возраст"));
                adapter.InsertCommand.Parameters.Add(new SqlParameter("@Тренер_Пол", SqlDbType.NVarChar, 100, "Тренер_Пол"));
                SqlParameter parameter = adapter.InsertCommand.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
                parameter.Direction = ParameterDirection.Output;
                connection.Open();
                adapter.Fill(trainertable);
                TrainerGrid.ItemsSource = trainertable.DefaultView;
                connection2 = new SqlConnection(connectionString3);
                SqlCommand command2 = new SqlCommand(sql2, connection2);
                adapter2 = new SqlDataAdapter(command2);
                adapter2.InsertCommand = new SqlCommand("Ввод_Клиентов", connection2);
                adapter2.InsertCommand.CommandType = CommandType.StoredProcedure;
                adapter2.InsertCommand.Parameters.Add(new SqlParameter("@Клиент_ФИО", SqlDbType.NVarChar, 50, "Клиент_ФИО"));
                adapter2.InsertCommand.Parameters.Add(new SqlParameter("@Клиент_Возраст", SqlDbType.Int, 100, "Клиент_Возраст"));
                adapter2.InsertCommand.Parameters.Add(new SqlParameter("@Клиент_Пол", SqlDbType.NVarChar, 100, "Клиент_Пол"));
                adapter2.InsertCommand.Parameters.Add(new SqlParameter("@ID_Тренера", SqlDbType.Int, 100, "ID_Тренера"));
                SqlParameter parameter2 = adapter2.InsertCommand.Parameters.Add("@ID", SqlDbType.Int, 0, "ID");
                parameter2.Direction = ParameterDirection.Output;
                connection2.Open();
                adapter2.Fill(clienttable);
                ClientGrid.ItemsSource = clienttable.DefaultView;
            }
            catch (Exception)
            {
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                }
                if (connection2 != null)
                {
                    connection2.Close();
                }
            }
        }
        private void UpdateDB()
        {
            try
            {
                SqlCommandBuilder comandbuilder = new SqlCommandBuilder(adapter);
                adapter.Update(trainertable);
            }
            catch (Exception)
            {
            }
        }
        private void UpdateTrainerButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UpdateDB();
            }
            catch (Exception)
            {
            }
        }
        private void UpdateDB2()
        {
            try
            {
                SqlCommandBuilder comandbuilder = new SqlCommandBuilder(adapter2);
                adapter2.Update(clienttable);
            }
            catch (Exception)
            {
            }
        }
        private void UpdateClientButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UpdateDB2();
            }
            catch (Exception)
            {
            }
        }
        private void DeleteTrainerButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TrainerGrid.SelectedItems != null)
                {
                    for (int i = 0; i < TrainerGrid.SelectedItems.Count; i++)
                    {
                        DataRowView datarowView = TrainerGrid.SelectedItems[i] as DataRowView;
                        DataRowView drv = (DataRowView)TrainerGrid.SelectedItem;
                        if (datarowView != null)
                        {
                            DataRow dataRow = (DataRow)datarowView.Row;
                            connectionString4 = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                            string sql = "DELETE FROM Тренер WHERE Тренер_ФИО = '" + drv[1].ToString() + "' AND Тренер_Возраст = '" + drv[2].ToString() + "' AND Тренер_Пол = '" + drv[3].ToString() + "'";
                            SqlConnection connection = null;
                            connection = new SqlConnection(connectionString4);
                            SqlCommand command = new SqlCommand(sql, connection);
                            connection.Open();
                            command.ExecuteNonQuery();
                            this.Hide();
                            MainWindow A = new MainWindow();
                            A.Show();
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        private void DeleteClientButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ClientGrid.SelectedItems != null)
                {
                    for (int i = 0; i < ClientGrid.SelectedItems.Count; i++)
                    {
                        DataRowView datarowView = ClientGrid.SelectedItems[i] as DataRowView;
                        DataRowView drv = (DataRowView)ClientGrid.SelectedItem;
                        if (datarowView != null)
                        {
                            DataRow dataRow = (DataRow)datarowView.Row;
                            connectionString4 = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                            string sql = "DELETE FROM Клиент WHERE Клиент_ФИО = '" + drv[1].ToString() + "' AND Клиент_Возраст = '" + drv[2].ToString() + "' AND Клиент_Пол = '" + drv[3].ToString() + "'";
                            SqlConnection connection = null;
                            connection = new SqlConnection(connectionString4);
                            SqlCommand command = new SqlCommand(sql, connection);
                            connection.Open();
                            command.ExecuteNonQuery();
                            this.Hide();
                            MainWindow A = new MainWindow();
                            A.Show();
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        private void TrainerGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            try
            {
                UpdateDB();
            }
            catch (Exception)
            {
            }
        }
        private void ClientGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            try
            {
                UpdateDB2();
            }
            catch (Exception)
            {
            }
        }
        private void Exit(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите выйти из приложения?", "Выход", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        {
                            foreach (Window window in App.Current.Windows)
                            {
                                window.Close();
                            }
                            break;
                        }
                    case MessageBoxResult.No:
                        {
                            ForFocus.Focus();
                            break;
                        }
                }
            }
        }
    }
}