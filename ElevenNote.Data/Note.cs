using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevenNote.Data
{
    public class Note
    {
        [Key]
        [Display(Name ="Your Note")]
        public int NoteId { get; set; }

        [Required]
        public Guid OwnerId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public TypeOfNote NoteType { get; set; } // enum example

        [DefaultValue(false)]
        public bool IsStarred { get; set; }

        [Required]
        [Display(Name = "Created")]
        public DateTimeOffset CreatedUtc { get; set; } //can't be null

        [Display(Name = "Modified")]
        public DateTimeOffset? ModifiedUtc { get; set; } //can be null bc of "?"

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

    }

    public enum TypeOfNote // enum example
    {
        [Display(Name = "School")]
        S,
        [Display(Name = "To Do")]
        TD,
        [Display(Name = "Important")]
        I,

    }
}
