using PinewoodTask.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PinewoodTask.Models
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CustomerID { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string? MiddleName { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                return string.Join(" ", new[] { FirstName, MiddleName, LastName }.Where(s => !string.IsNullOrWhiteSpace(s)));
            }
        }

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [Phone]
        [MaxLength(15)]
        public string? PhoneNumber { get; set; }

        public bool OptInMarketing { get; set; }

        [Required]
        public AuditActionTypes AuditAction { get; set; }
        
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
