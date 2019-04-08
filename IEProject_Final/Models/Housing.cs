namespace IEProject_Final.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Housing")]
    public partial class Housing
    {
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Suburb { get; set; }

        public int SchoolNo { get; set; }

        public int CrimeNo { get; set; }

        public int Buy_Price { get; set; }

        public int HospitalNo { get; set; }
    }

    public partial class House_Rating
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Suburb { get; set; }

        public double Rating { get; set; }


    }
}
