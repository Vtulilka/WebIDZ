//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SubscriberBase.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class Tariffs
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tariffs()
        {
            this.Connection = new HashSet<Connection>();
            this.Operators = new HashSet<Operators>();
        }
    
        public System.Guid TariffID { get; set; }
        public string TariffName { get; set; }
        public int MonthlyFee { get; set; }
        public string Time_Calculation_method { get; set; }
        public string Cost_on_network_Calls { get; set; }
        public string Cost_long_distance_Calls { get; set; }
        public string Cost_local_Calls { get; set; }
        public string Cost_on_network_SMS { get; set; }
        public string Cost_local_SMS { get; set; }
        public string Cost_long_distance_SMS { get; set; }
        public int InternetGB { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Connection> Connection { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Operators> Operators { get; set; }
    }
}
