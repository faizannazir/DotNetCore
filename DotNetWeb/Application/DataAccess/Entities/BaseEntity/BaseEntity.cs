using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entities.BaseEntity
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get;set; }

        public string? CreatedBy { get; set; }   
        public string? UpdatedBy { get; set; }
        public string? DeletedBy { get; set; }   
        public bool? IsDeleted { get; set; } 
        public DateTime? CreatedDate { get; set; }   
        public DateTime? UpdateDate { get; set; }   
        public DateTime? DeletedDate { get; set; }   

    }
}
