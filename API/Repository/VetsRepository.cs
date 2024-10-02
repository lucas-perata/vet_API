using API.Data;
using API.Dtos;
using API.Entities.Identity;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class VetsRepository : IVetsInterface
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public VetsRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedList<VetDto>> GetVetList(UserParams userParams, AppUser user)
        {
            var nearVetQuery = _context.Vets
              .Include(v => v.User)
              .Where(v => v.User.Area == user.Area && v.User.Province == user.Province)
              .ProjectTo<VetDto>(_mapper.ConfigurationProvider)
              .AsNoTracking();

            if (!await nearVetQuery.AnyAsync())
            {
                var generalVetsQuery = _context.Vets
              .Include(v => v.User)
              .Where(v => v.User.Area == user.Area)
              .ProjectTo<VetDto>(_mapper.ConfigurationProvider)
              .AsNoTracking();

                return await PagedList<VetDto>.CreateAsync(generalVetsQuery,
                    userParams.PageNumber, userParams.PageSize);

            }

            return await PagedList<VetDto>.CreateAsync(nearVetQuery,
                userParams.PageNumber,
                userParams.PageSize);
        }
    }
}
