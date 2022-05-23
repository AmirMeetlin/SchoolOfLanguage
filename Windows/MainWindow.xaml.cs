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
using SchoolOfLanguageMetlin.EF;
using SchoolOfLanguageMetlin.Windows;

namespace SchoolOfLanguageMetlin
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        List<EF.Client> listClient = new List<EF.Client>();
        int pageCounter = 0;
        int recordPerPage;

        List<string> listFilter = new List<string>()
            {
                "Все",
                "Мужчины",
                "Женщины"
            };
        List<string> listSort = new List<string>()
            {
                "По Умолчанию",
                "По фамилии",
                "По дате последнего посещения",
                "По количеству посещений"
            };
        public MainWindow()
        {
            InitializeComponent();
            listClient = Classhelper.AppData.Context.Client.ToList();
            lvClients.ItemsSource = listClient;
            cbFilter.ItemsSource = listFilter;
            cbFilter.SelectedIndex = 0;
            cbSort.ItemsSource = listSort;
            cbSort.SelectedIndex = 0;
            tbQty.Text = listClient.Count().ToString();
        }

        private void Filter()
        {
            listClient = Classhelper.AppData.Context.Client.ToList();
            lvClients.ItemsSource = listClient;
            listClient = listClient.
                Where(i => i.Email.ToLower().Contains(tbSearch.Text.ToLower())
                || i.Phone.ToLower().Contains(tbSearch.Text.ToLower())
                || i.FIO.ToLower().Contains(tbSearch.Text.ToLower())).ToList();
            switch (cbFilter.SelectedIndex)
            {
                case 0:
                    listClient = listClient.OrderBy(i => i.ID).ToList();
                    break;
                case 1:
                    listClient = listClient.Where(i => i.IDGender.Contains("м")).ToList();
                    break;
                case 2:
                    listClient = listClient.Where(i => i.IDGender.Contains("ж")).ToList();
                    break;
                default:
                    listClient = listClient.OrderBy(i => i.ID).ToList();
                    break;
            }
            switch (cbSort.SelectedIndex)
            {
                case 0:
                    listClient = listClient.OrderBy(i => i.ID).ToList();
                    break;
                case 1:
                    listClient = listClient.OrderBy(i => i.LastName).ToList();
                    break;
                case 2:
                    listClient = listClient.OrderBy(i => i.LastVizit).ToList();
                    break;
                case 3:
                    listClient = listClient.OrderBy(i => i.qtyOfVizits).ToList();
                    break;
                default:
                    listClient = listClient.OrderBy(i => i.ID).ToList();
                    break;
            }
            tbQty.Text = listClient.Count().ToString();
            tbRows.Text = tbQty.Text;
            lvClients.ItemsSource = listClient;
        }

        private void CbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void CbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filter();
        }

        private void TbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            Filter();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            listClient = listClient.Where(i => i.DateOfBirth.Month == DateTime.Now.Month).ToList();
            tbQty.Text = listClient.Count().ToString();
            tbRows.Text = tbQty.Text;
            lvClients.ItemsSource = listClient;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            listClient = Classhelper.AppData.Context.Client.ToList();
            lvClients.ItemsSource = listClient;
            tbSearch.Text = "";
            cbFilter.SelectedIndex = 0;
            cbSort.SelectedIndex = 0;
            checkboxDateOfBirth.IsChecked = false;
        }

        private void BtnTo10Rows_Click(object sender, RoutedEventArgs e)
        {
            pageCounter = 0;
            recordPerPage = 10;
            lvClients.ItemsSource = listClient.Skip(pageCounter * recordPerPage).Take(recordPerPage).ToList();
            tbRows.Text = listClient.Skip(pageCounter * recordPerPage).Take(recordPerPage).ToList().Count().ToString();
            tbQty.Text = listClient.Count().ToString();
        }

        private void BtnTo50Rows_Click(object sender, RoutedEventArgs e)
        {
            pageCounter = 0;
            recordPerPage = 50;
            lvClients.ItemsSource = listClient.Skip(pageCounter * recordPerPage).Take(recordPerPage).ToList();
            tbRows.Text = listClient.Skip(pageCounter * recordPerPage).Take(recordPerPage).ToList().Count().ToString();
            tbQty.Text = listClient.Count().ToString();
        }

        private void BtnTo200Rows_Click(object sender, RoutedEventArgs e)
        {
            pageCounter = 0;
            recordPerPage = 200;
            lvClients.ItemsSource = listClient.Skip(pageCounter * recordPerPage).Take(recordPerPage).ToList();
            tbRows.Text = listClient.Skip(pageCounter * recordPerPage).Take(recordPerPage).ToList().Count().ToString();
            tbQty.Text = listClient.Count().ToString();
        }

        private void BtnToAllRows_Click(object sender, RoutedEventArgs e)
        {
            lvClients.ItemsSource = Classhelper.AppData.Context.Client.ToList();
            tbRows.Text = listClient.Count().ToString();
        }

        private void BtnLeftPage_Click(object sender, RoutedEventArgs e)
        {
            if(pageCounter !=0)
            {
                pageCounter--;
                lvClients.ItemsSource = listClient.Skip(pageCounter * recordPerPage).Take(recordPerPage).ToList();
            }
        }

        private void BtnRightPage_Click(object sender, RoutedEventArgs e)
        {
            if(listClient.Count()/recordPerPage>pageCounter&&recordPerPage!=0)
            {
                pageCounter++;
                lvClients.ItemsSource = listClient.Skip(pageCounter * recordPerPage).Take(recordPerPage).ToList();
            }
            
        }

        private void CheckboxDateOfBirth_Unchecked(object sender, RoutedEventArgs e)
        {
            lvClients.ItemsSource = Classhelper.AppData.Context.Client.ToList();
            
        }

        private void LvClients_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete || e.Key == Key.Back)
            {
                var resClick = MessageBox.Show("Удалить пользователя?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (resClick == MessageBoxResult.No)
                {
                    return;
                }
                try
                {
                    if (lvClients.SelectedItem is EF.Client)
                    {
                        var empl = lvClients.SelectedItem as EF.Client;

                        Classhelper.AppData.Context.Client.Remove(empl);

                        Classhelper.AppData.Context.SaveChanges();

                        MessageBox.Show("Пользователь успешно удален", "Готово", MessageBoxButton.OK, MessageBoxImage.Information);
                        Filter();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void BtnAddClient_Click(object sender, RoutedEventArgs e)
        {
            AddClient addClient = new AddClient();
            addClient.ShowDialog();
            Filter();
        }

        private void LvClients_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var client = lvClients.SelectedItem as EF.Client;
            AddClient addClient = new AddClient(client);
            addClient.ShowDialog();
            Filter();


            //var equipment = lvEquipment.SelectedItem as EF.Product;
            //AddEquipment addEquipment = new AddEquipment(equipment);
            //addEquipment.ShowDialog();
            //Filter();
        }

        private void LvClients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnShowAllVizits.Visibility = Visibility.Visible;
        }

        private void BtnShowAllVizits_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
