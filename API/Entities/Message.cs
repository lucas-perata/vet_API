using System.ComponentModel.DataAnnotations.Schema;
using API.Entities.Identity;

namespace API.Entities
{
    public class Message
    {
        public int Id { get; set; }

        [ForeignKey("Sender")]
        public string SenderId { get; set; }
        public string SenderUsername { get; set; }
        public AppUser Sender { get; set; }

        [ForeignKey("Recipient")]
        public string RecipientId { get; set; }
        public string RecipientUsername { get; set; }
        public AppUser Recipient { get; set; }
        public string Content { get; set; }
        public DateTime? DateRead { get; set; }
        public DateTime MessageSent { get; set; } = DateTime.UtcNow;
        public bool SenderDeleted { get; set; }
        public bool RecipientDeleted { get; set; }
    }
}