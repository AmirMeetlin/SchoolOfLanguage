using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolOfLanguageMetlin.EF
{
    public partial class VW_CientInfo
    {
        public string FIO { get => $"{LastName}{FirstName}{Patronymic}"; }
    }

    public partial class Client
    {

        public string FIO { get => $"{LastName}{FirstName}{Patronymic}"; }

        public DateTime? LastVizit
        {
            get
            {
                return ClientService.LastOrDefault()?.DateStart;
            }
        }
        public int qtyOfVizits
        {
            get
            {
                return ClientService.Count;
            }
        }
        public List<Tag> Tags
        {
            get
            {
                return Tag.ToList();
            }
        }
    }
}
