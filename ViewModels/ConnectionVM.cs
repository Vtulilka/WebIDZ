using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SubscriberBase.ViewModels
{
    public class ConnectionVM
    {
        public System.Guid ConnectionID { get; set; }
        [Required]
        [DisplayName("Дата подключения")]
        public System.DateTime ConnectionDate { get; set; }
        [Required]
        [DisplayName("Номер телефона")]
        //[StringLength(11)]
        public string PhoneNumb { get; set; }
        public System.Guid OperatorID { get; set; }
        public System.Guid TariffID { get; set; }
        public System.Guid SubscriberID { get; set; }
    }
}