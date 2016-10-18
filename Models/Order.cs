using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Bangazon.Models
{
  public class Order
  {
    [Key]
    public int OrderId {get;set;}

    [Required]
    [DataType(DataType.Date)]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime DateCreated {get;set;}

    [DataType(DataType.Date)]
    public DateTime? DateCompleted {get;set;}
    // CustomerId type id is the foreign key
    public int CustomerId {get;set;}
    // Customer is the table the foreign key is going to be on
    public Customer Customer {get;set;}
    // payment type id is the foreign key
    public int? PaymentTypeId {get;set;}
    // PaymentType is the table the foreign key is going to be on
    public PaymentType PaymentType {get;set;} 

    public ICollection<LineItem> LineItems;
  }
}