using API.Data;
using API.Dtos;
using API.Dtos.Appointment;
using API.Dtos.Message;
using API.Dtos.Review;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class DashboardRepository : IDashboardInterface
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DashboardRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Task<OwnerDashDto> GetDataForOwner(string vetId)
        {
            // TODO:
            throw new NotImplementedException();
        }

        public async Task<VetDashDto> GetDataForVet(string vetId)
        {
            var appointments = _context
                .Appointments.Where(a => a.VetId == vetId)
                .ProjectTo<AppointmentDto>(_mapper.ConfigurationProvider)
                .AsNoTracking();
            // var expenses =
            var reviews = _context
                .Reviews.Where(r => r.VetId == vetId)
                .ProjectTo<ReviewDto>(_mapper.ConfigurationProvider)
                .AsNoTracking();
            var messages = _context
                .Messages.Where(m => m.RecipientId == vetId)
                .ProjectTo<MessageDto>(_mapper.ConfigurationProvider)
                .AsNoTracking();
            // var followers =

            VetDashDto VetDashDto = new VetDashDto
            {
                Appointments = appointments,
                Reviews = reviews,
                Messages = messages,
            };

            return VetDashDto;
        }
    }
}
