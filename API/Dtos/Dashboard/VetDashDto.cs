using API.Dtos.Appointment;
using API.Dtos.Message;
using API.Dtos.Review;

namespace API.Dtos
{
    public class VetDashDto
    {
        public IEnumerable<AppointmentDto> Appointments { get; set; }
        public IEnumerable<ReviewDto> Reviews { get; set; }
        public IEnumerable<MessageDto> Messages { get; set; }
        public IEnumerable<ExpensesVetDto> ExpensesVet { get; set; }
    }
}
