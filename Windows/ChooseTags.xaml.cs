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
            lvSelectedTags.ItemsSource = listTagClient.ToList();
            lvSelectTags.ItemsSource = listTags.ToList();
        }

        private void LvSelectedTags_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lvSelectedTags.SelectedItem is EF.Tag)
            {             
                var tag = lvSelectedTags.SelectedItem as EF.Tag;
                //delByValue(ref listTagClientArray, tag);
                listTags.Add(tag);
                listTagClient.Remove(tag);
                //EF.Tag[] listTagClientArray = listTagClient.ToArray();
                //EF.Tag[] listTagsArray = listTags.ToArray();
                //listTagClient = listTagClientArray.ToList();
                //listTags = listTagsArray.ToList();
                Filter();
            }
        }

        private void LvSelectTags_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lvSelectTags.SelectedItem is EF.Tag)
            {               
                var tag = lvSelectTags.SelectedItem as EF.Tag;
                //delByValue(ref listTagsArray, tag);
                listTagClient.Add(tag);
                listTags.Remove(tag);
                //EF.Tag[] listTagClientArray = listTagClient.ToArray();
                //EF.Tag[] listTagsArray = listTags.ToArray();
                //listTags = listTagsArray.ToList();
                //listTagClient = listTagClientArray.ToList();
                Filter();
            }
        }
        public static void delByIndex(ref EF.Tag[] data, int delIndex)
        {
            EF.Tag[] newData = new EF.Tag[data.Length - 1];
            for (int i = 0; i < delIndex; i++)
            {
                newData[i] = data[i];
            }
            for (int i = delIndex; i < newData.Length; i++)
            {
                newData[i] = data[i + 1];
            }
            data = newData;
        }

        public static void delByValue(ref EF.Tag[] data, EF.Tag delValue)
        {
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] == delValue)
                {
                    delByIndex(ref data, i);
                    i--;
                }
            }
        }

        private void BtnSend_Click(object sender, RoutedEventArgs e)
        {
            globalClient.Tag = listTagClient;
            this.Close();
        }
    }
}
