using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos.Message
{
    public class CreateMessageDto
    {
        public string RecipientId { get; set; }
        public string Content { get; set; }
    }
}