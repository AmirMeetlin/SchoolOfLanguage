//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SchoolOfLanguageMetlin.EF
{
    using System;
    using System.Collections.Generic;
    
    public partial class ClientService
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClientService()
        {
            this.ClientServiceDocument = new HashSet<ClientServiceDocument>();
        }
    
        public int ID { get; set; }
        public int IDClient { get; set; }
        public int IDService { get; set; }
        public System.DateTime DateStart { get; set; }
        public string Comment { get; set; }
        public Nullable<decimal> FinalCost { get; set; }
        public Nullable<bool> IsConfirmed { get; set; }
        public Nullable<System.DateTime> DateEnd { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual Service Service { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientServiceDocument> ClientServiceDocument { get; set; }
    }
}
