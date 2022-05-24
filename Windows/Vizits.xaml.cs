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
    /// Логика взаимодействия для Vizits.xaml
    /// </summary>
    public partial class Vizits : Window
    {

        EF.Client clientSave;
        public Vizits(EF.Client client)
        {
            InitializeComponent();
            clientSave = client;
            Filter();
        }

        public void Filter()
        {
            List<EF.ClientService> clientService = new List<EF.ClientService>();
            clientService = Classhelper.AppData.Context.ClientService.Where(i => i.IDClient == clientSave.ID).ToList();
            foreach(EF.ClientService CS in clientService)
            {
                CS.numOfDocs = Classhelper.AppData.Context.ClientServiceDocument.ToList().Where(i => i.ClientService.ID == CS.ID).Count();
            }
            lvClient.ItemsSource = clientService;
        }
    }
}
