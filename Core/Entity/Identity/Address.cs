using System.ComponentModel.DataAnnotations;

namespace Core.Entity.Identity
{
    public class Address
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        
        // Relation ship 1 - 1 với Class AppUser.cs ======================================
        [Required] // không cho khóa ngoại AppUserId có giá trị null 
        public string AppUserId { get; set; }
        public AppUser AppUser {get ; set;}
        // ==========================================================
    }
}