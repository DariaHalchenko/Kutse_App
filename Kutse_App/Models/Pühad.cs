using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Kutse_App.Models
{
    //Добавьте класс Праздники(обязательными атрибутами должны быть Назавание праздника, Дата проведения).
    public class Pühad
    {
        [Key] 
        public int Id { get; set; }

        [Required] 
        [StringLength(100)] 
        public string Puhkuse_nimi { get; set; } // Название праздника

        [Required] 
        [DataType(DataType.Date)] 
        public DateTime Kuupaev { get; set; } // Дата проведения праздника
    }
}