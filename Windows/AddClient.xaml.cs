using Microsoft.Win32;
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
using System.Runtime;
using System.IO;

namespace SchoolOfLanguageMetlin.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddClient.xaml
    /// </summary>
    public partial class AddClient : Window
    {
        bool isEdit = false;
        EF.Client editClient = new EF.Client();
        string pathPhoto = null;
        public AddClient()
        {
            InitializeComponent();
            cbGender.ItemsSource = Classhelper.AppData.Context.Gender.ToList();
            cbGender.DisplayMemberPath = "ID";
            cbGender.SelectedIndex = 0;
        }
        public AddClient(EF.Client client)
        {
            InitializeComponent();
            cbGender.ItemsSource = Classhelper.AppData.Context.Gender.ToList();
            cbGender.DisplayMemberPath = "ID";
            cbGender.SelectedIndex = 0;

            tbFirstName.Text = client.FirstName;
            tbSecondName.Text = client.LastName;
            tbPatronymic.Text = client.Patronymic;
            tbPhone.Text = client.Phone;
            dpBirthday.SelectedDate = client.DateOfBirth;
            tbMail.Text = client.Email;
            if(client.IDGender.ToString()=="м")
            {
                cbGender.SelectedIndex = 1;
            }
            tbID.Text = client.ID.ToString();
            try
            {
                photoUser.Source = new BitmapImage(new Uri(client.Photo));
            }
            catch
            {

            }
         
            tbTitle.Text = "Изменение клиента";

            isEdit = true;
            editClient = client;
        }



        //public AddEmployee(EF.Employee employee)
        //{
        //    InitializeComponent();
        //    cbGender.ItemsSource = ClassHelper.AppData.Conrext.Gender.ToList();
        //    cbGender.DisplayMemberPath = "Gender1";
        //    cbRole.ItemsSource = ClassHelper.AppData.Conrext.Role.ToList();
        //    cbRole.DisplayMemberPath = "Role1";
                                                                                                                                           
        //    tbFirstName.Text = employee.FirstName;
        //    tbSecondName.Text = employee.SecondName;
        //    tbPatronymic.Text = employee.Patronymic;
        //    tbPhone.Text = employee.Phone;
        //    cbGender.SelectedIndex = employee.IDGender - 1;
        //    cbRole.SelectedIndex = employee.IDRole - 1;
        //    tbLogin.Text = employee.Login;
        //    Login = employee.Login;
        //    tbPassword.Password = employee.Password;

        //    if (employee.Photo != null)
        //    {
        //        using (MemoryStream stream = new MemoryStream(employee.Photo))
        //        {
        //            BitmapImage bitmapImage = new BitmapImage();
        //            bitmapImage.BeginInit();
        //            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
        //            bitmapImage.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
        //            bitmapImage.StreamSource = stream;
        //            bitmapImage.EndInit();

        //            photoUser.Source = bitmapImage;
        //        }

        //    }

        //    tbTitle.Text = "Изменение работников";

        //    isEdit = true;
        //    editEmployee = employee;
        //}
        private void textBoxes_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            if (int.TryParse(e.Text, out int i ))
            {
                e.Handled = true;
            }
            
        }
        
        private void btnChoosePhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            if (openFile.ShowDialog() == true)
            {
                photoUser.Source = new BitmapImage(new Uri(openFile.FileName));
                pathPhoto = openFile.FileName;
            }
        }
        private void btnSend_Click(object sender, RoutedEventArgs e)
        {
            string specSymb = "!\"#$%&'()*+,./::<=>?@[\\]:_{|}";
            if (tbFirstName.Text.IndexOfAny(specSymb.ToCharArray()) >= 1|| tbSecondName.Text.IndexOfAny(specSymb.ToCharArray()) >= 1|| tbPatronymic.Text.IndexOfAny(specSymb.ToCharArray()) >= 1)
            {
                MessageBox.Show("Поля ФИО могут содержать в себе только буквы и следующие символы: пробел и дефис!","Ошибка",MessageBoxButton.OK,MessageBoxImage.Error);
                return;
            }
            if (String.IsNullOrWhiteSpace(tbFirstName.Text)|| String.IsNullOrWhiteSpace(tbSecondName.Text)|| String.IsNullOrWhiteSpace(tbPatronymic.Text))
            {
                MessageBox.Show("Поля ФИО должны быть заполнены!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if(String.IsNullOrWhiteSpace(tbPhone.Text))
            {
                MessageBox.Show("Поле ТЕЛЕФОН должно быть заполнено!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (dpBirthday.SelectedDate==null)
            {
                MessageBox.Show("Поле ДЕНЬ РОЖДЕНИЯ должно быть заполнено!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (String.IsNullOrWhiteSpace(tbMail.Text))
            {
                MessageBox.Show("Поле ПОЧТА должно быть заполнено!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (tbFirstName.Text.Length>50|| tbSecondName.Text.Length > 50 || tbPatronymic.Text.Length > 50)
            {
                MessageBox.Show("Поля ФИО могут быть длиннее 50 символов!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if(tbMail.Text.Contains('@')&&(tbMail.Text.Contains(".com")|| tbMail.Text.Contains(".ru")) == false)
            {
                MessageBox.Show("Поле ПОЧТА заполнено неверно", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            specSymb = "!\"#$%&'*,./::<=>?@[\\]:_{|}";
            if (tbPhone.Text.IndexOfAny(specSymb.ToCharArray()) >= 1)
            {
                MessageBox.Show("Поле НОМЕР ТЕЛЕФОНА содержит недопустимые символы!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if(isEdit)
            {
                var resClick = MessageBox.Show("Изменить пользователя", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (resClick == MessageBoxResult.No)
                {
                    return;
                }


                editClient.FirstName = tbFirstName.Text;
                editClient.LastName = tbSecondName.Text;
                editClient.Patronymic = tbPatronymic.Text;
                editClient.Phone = tbPhone.Text;
                editClient.IDGender = cbGender.SelectedItem.ToString();
                editClient.Email = tbMail.Text;
                editClient.DateOfBirth = Convert.ToDateTime(dpBirthday.SelectedDate);

                if (pathPhoto != null)
                {
                    editClient.Photo = pathPhoto;
                }

                Classhelper.AppData.Context.SaveChanges();
                MessageBox.Show("Пользователь изменен");

                this.Close();
            }
            else
            {
                var resClick = MessageBox.Show("Добавить пользователя", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (resClick == MessageBoxResult.No)
                {
                    return;
                }

                EF.Client newClient = new EF.Client();
                newClient.FirstName = tbFirstName.Text;
                newClient.LastName = tbSecondName.Text;
                newClient.Patronymic = tbPatronymic.Text;
                newClient.Phone = tbPhone.Text;
                newClient.IDGender = cbGender.SelectedItem.ToString();
                newClient.Email = tbMail.Text;
                newClient.DateOfBirth = Convert.ToDateTime(dpBirthday.SelectedDate);

                if (pathPhoto != null)
                {
                    newClient.Photo = pathPhoto;
                }

                Classhelper.AppData.Context.SaveChanges();

                this.Close();
            }
            
        }
    }
}
