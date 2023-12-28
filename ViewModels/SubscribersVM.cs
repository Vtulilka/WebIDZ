using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SubscriberBase.ViewModels
{
    public class SubscribersVM
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
    }
}