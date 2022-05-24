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

namespace SchoolOfLanguageMetlin.Windows
{
    /// <summary>
    /// Логика взаимодействия для ChooseTags.xaml
    /// </summary>
    public partial class ChooseTags : Window
    {

        public ChooseTags(EF.Client client)
        {
            List<EF.Tag> listTags = new List<EF.Tag>();
            listTags = Classhelper.AppData.Context.Tag.ToList();
            List<EF.Tag> listTagClient = client.Tags as List<EF.Tag>;
            EF.Tag AllTags = new EF.Tag();

            //EF.Tag NotClientTag;
            //foreach (EF.Tag tag in client.Tags)
            //{
            //    if (AllTags.ID == tag.ID)
            //    {
            //        MessageBox.Show(" ");
            //        //listTags.Remove(tag);
            //    }
            //}
            //EF.Tag tag = client.Tags as EF.Tag;
            //listTags = listTags.Where(i => i.ID != tag.ID).ToList();
            InitializeComponent();
            lvSelectedTags.ItemsSource = listTagClient;          
            lvSelectTags.ItemsSource = listTags;
        }
    }
}
