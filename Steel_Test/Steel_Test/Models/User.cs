using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;


namespace Steel_Test.Models
{
    public class User
    {
        public int ID { get; set; }

        [Required]
        [StringLength(256)]
        public string FIO { get; set; }

        public int TableNumber { get; set; }

        public int PositionID { get; set; }

        public virtual Position Position { get; set; }

        public byte[] Photo { get; set; }

    }
}