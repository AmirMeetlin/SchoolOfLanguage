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
    
    public partial class PhotoService
    {
        public int IDService { get; set; }
        public string Photo { get; set; }
        public int ID { get; set; }
    
        public virtual Service Service { get; set; }
    }
}
