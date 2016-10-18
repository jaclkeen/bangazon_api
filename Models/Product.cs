using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bangazon.Models
{
  public class Product
  {
    [Key]
    public int ProductId {get;set;}

    [Required]
    [DataType(DataType.Date)]
    [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Computed)]
    public DateTime DateCreated {get;set;}

    [Required]
    // SETS MAX LENGTH ON STRING Description
    [StringLength(255)]
    public string Description { get; set; }

    [Required]
    public double Price { get; set; }
    public ICollection<LineItem> LineItems;
  }
}