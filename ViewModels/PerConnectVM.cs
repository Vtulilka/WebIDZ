using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;

namespace SubscriberBase.ViewModels
{
    public class PerConnectVM
    {
        public System.Guid SubscriberID { get; set; }

        [Required]
        [DisplayName("Фамилия")]
        [StringLength(50)]
        public string Surname { get; set; }

        [Required]
        [DisplayName("Имя")]
        [StringLength(50)]
        public string Name { get; set; }

        [DisplayName("Отчество")]
        [StringLength(50)]
        public string Patronymic { get; set; }

        [Required]
        [DisplayName("Дата рождения")]
        public System.DateTime BirthDay { get; set; }
        public System.Guid ConnectionID { get; set; }
        [Required]
        [DisplayName("Дата подключения")]
        public System.DateTime ConnectionDate { get; set; }
        [Required]
        [DisplayName("Номер телефона")]
        public string PhoneNumb { get; set; }
        public System.Guid OperatorID { get; set; }
        public System.Guid TariffID { get; set; }
    }
}