using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
namespace Entities.Models
{
    [Table("Account")]
    public class Account : IEntity
    {
        [Key]
        public Guid AccountId { get; set; }
 
        [Required(ErrorMessage = "Date created is required")]
        public DateTime DateCreated { get; set; }
 
        [Required(ErrorMessage = "Account type is required")]
        public string AccountType { get; set; }
 
        [Required(ErrorMessage = "Owner Id is required")]
        public Guid OwnerId { get; set; }
        public Guid Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}