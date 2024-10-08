using API.Data;
using API.Dtos.Adoption;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class AdoptionRepository : IAdoptionInterface
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public AdoptionRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void CreateAdoptionAsync(Adoption adoption)
        {
            _context.Adoptions.Add(adoption);
        }

        public bool DeleteAdoption(Adoption adoption)
        {
            _context.Remove(adoption);
            return _context.SaveChanges() > 0;
        }

        public async Task<Adoption> GetAdoption(int id)
        {
            return await _context
                .Adoptions.Include(adoption => adoption.AdoptionPhotos)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public void UpdateAdoption(Adoption adoption)
        {
            _context.Entry(adoption).State = EntityState.Modified;
        }

        // TODO: GET ADOPTIONS STATUS === 0

        public async Task<PagedList<AdoptionDto>> GetAdoptions(UserParams userParams)
        {
            var query = _context
                .Adoptions.Include(adoption => adoption.AdoptionPhotos)
                .ProjectTo<AdoptionDto>(_mapper.ConfigurationProvider)
                .AsNoTracking();

            return await PagedList<AdoptionDto>.CreateAsync(
                query,
                userParams.PageNumber,
                userParams.PageSize
            );
        }

        public async Task<PagedList<AdoptionDto>> SearchAdoptions(
            UserParams userParams,
            string gender,
            string area,
            string province
        )
        {
            IQueryable<AdoptionDto> query;

            if (gender == "default")
            {
                query = _context
                    .Adoptions.Where(a => a.StatusList == 0)
                    .Where(a => a.Province == province && a.Area == area)
                    .ProjectTo<AdoptionDto>(_mapper.ConfigurationProvider)
                    .AsNoTracking();
            }
            else
            {
                query = _context
                    .Adoptions.Where(a => a.StatusList == 0 && a.Gender == gender)
                    .Where(a => a.Province == province && a.Area == area)
                    .ProjectTo<AdoptionDto>(_mapper.ConfigurationProvider)
                    .AsNoTracking();
            }

            return await PagedList<AdoptionDto>.CreateAsync(
                query,
                userParams.PageNumber,
                userParams.PageSize
            );
        }
    }
}
