//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Laboratory.DbModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class Patients_services
    {
        public int Service_code { get; set; }
        public int Patient_id { get; set; }
        public int Patients_services_id { get; set; }
    
        public virtual Patients Patients { get; set; }
        public virtual Services Services { get; set; }
    }
}
