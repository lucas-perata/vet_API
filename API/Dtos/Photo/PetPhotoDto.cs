using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos.Photo
{
    public class PetPhotoDto
    {
        public int  Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public int PetId { get; set; }
    }
}