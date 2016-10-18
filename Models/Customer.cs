using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bangazon.Models
{
    // ACTUALLY CONSTRUCTS DATABASE TABLE THROUGH CLASSES
    // NAME OF CLASS IS THE NAME OF THE TABLE
  public class Customer
  {
    [Key]
    public int CustomerId {get;set;}

    // HAS TO HAVE A VALUE
    [Required]
    // HAS TO BE A DATE
    [DataType(DataType.Date)]
    // WANT DATABASE TO GENERATE(IN THIS CASE THE CURRENT DATE)
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime DateCreated {get;set;}

    // EACH METHOD IS A COLUMN AND THE Required PROVIDES FORM VALIDATION AND WILL REJECT IF FORM FIELD IS EMPTY
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    // EVERY CUSTOMER CAN HAVE MULTPILE PAYMENT TYPES. THIS IS ESTABLISHING A 
    // RELATIONSHIP BETWEEN PAYMENT AND CUSTOMER. ONE TO MANY. THIS IS THE MANY SIDE
    public ICollection<PaymentType> PaymentTypes;
  }
}