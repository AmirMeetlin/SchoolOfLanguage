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

        EF.Client globalClient;
        List<EF.Tag> listTags = new List<EF.Tag>();
        List<EF.Tag> listTagClient = new List<EF.Tag>();
        public ChooseTags(EF.Client client)
        {
            globalClient = client;
            listTagClient = globalClient.Tags as List<EF.Tag>;
            listTags = Classhelper.AppData.Context.Tag.ToList();
            foreach (EF.Tag tags in listTags.ToArray())
            {
                foreach (EF.Tag tagOfClient in listTagClient.ToArray())
                {
                    if (tags.ID == tagOfClient.ID)
                    {
                        listTags.Remove(tags);
                    }
                }
            }
            InitializeComponent();
            Filter();
        }
        private void Filter()
        {
            lvSelectedTags.ItemsSource = listTagClient;
            lvSelectTags.ItemsSource = listTags;
        }

        private void LvSelectedTags_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(lvSelectedTags.SelectedItem is EF.Tag)
            {
                var tag = lvSelectedTags.SelectedItem as EF.Tag;
                listTagClient.Remove(tag);
                listTags.Add(tag);
                Filter();
            }          
        }

        private void LvSelectTags_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lvSelectedTags.SelectedItem is EF.Tag)
            {
                var tag = lvSelectedTags.SelectedItem as EF.Tag;
                listTags.Remove(tag);
                listTagClient.Add(tag);
                Filter();
            }
        }
    }
}
