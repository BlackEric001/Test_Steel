using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Steel_Test.Models
{
    public class Position
    {
        public int PositionID { get; set; }

        [Required]
        [StringLength(256)]
        public string PositionName { get; set; }

        public virtual ICollection<User> Users { get; set; }

    }
}