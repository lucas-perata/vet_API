using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dtos
{
    public class UpdateMedicalHistoryDto
    {
        public DateTime Date { get; set; }   
        public string Description { get; set; }
        public string Treatment { get; set; }
        public string VetName { get; set; }
    }
}