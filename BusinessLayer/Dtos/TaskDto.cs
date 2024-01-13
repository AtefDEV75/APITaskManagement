using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Dtos
{
    public class TaskDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        [RegularExpression(@"^[a-zA-Z\s]*$", ErrorMessage = "Title should only contain letters and spaces")]
        public string Title { get; set; } = null!;

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Assignee is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? Assignee { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "Invalid date format")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-ddTHH:mm:ss}")]
        public DateTime? DueDate { get; set; }

        [RegularExpression(@"^(NotStarted|InProgress|Completed)$", ErrorMessage = "Invalid status")]
        public string? Status { get; set; }
    }
}
