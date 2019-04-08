namespace IEProject_Final.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("School")]
    public partial class School
    {
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Suburb { get; set; }

        public int PrimaryNo { get; set; }

        public int SecondaryNo { get; set; }

        public int SpecialNo { get; set; }
    }

    public partial class Updated_Result
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Suburb { get; set; }

        public int SchoolNo { get; set; }
    }
}
