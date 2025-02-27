﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace HTPSSystem.Entities;

[Table("Registration")]
public partial class Registration
{
    [Key]
    public int RegistrationID { get; set; }

    public int ProductID { get; set; }

    public int CustomerID { get; set; }

    [Required(ErrorMessage = "Serial Number is required")]
    [StringLength(10, ErrorMessage = "Serial Number is limited to 10 characters.")]
    [Unicode(false)]
    public string SerialNumber { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DateOfPurchase { get; set; }

    [Required(ErrorMessage = "Purchased from retailer is required")]
    [StringLength(50, ErrorMessage = "Purchased from retailer is limited to 50 characters.")]
    [Unicode(false)]
    public string PurchasedFrom { get; set; }

    [Required(ErrorMessage = "Purchased province is required")]
    [StringLength(2, ErrorMessage = "Purchased province is limited to 2 characters.")]
    [Unicode(false)]
    public string PurchaseProvince { get; set; }

    [ForeignKey("CustomerID")]
    [InverseProperty("Registrations")]
    public virtual Customer Customer { get; set; }

    [ForeignKey("ProductID")]
    [InverseProperty("Registrations")]
    public virtual Product Product { get; set; }
}