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

        public void CreateAdoptionWithPetAsync(Adoption adoption)
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
                .Adoptions.Include(p => p.Pet)
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
                .Adoptions.ProjectTo<AdoptionDto>(_mapper.ConfigurationProvider)
                .AsNoTracking();

            return await PagedList<AdoptionDto>.CreateAsync(
                query,
                userParams.PageNumber,
                userParams.PageSize
            );
        }

        // TODO: Make search not sensible no CAPS and optional
        public async Task<PagedList<AdoptionDto>> SearchAdoptions(
            UserParams userParams,
            string gender,
            string area,
            string province
        )
        {
            var query = _context
                .Adoptions.Include(p => p.Pet)
                .Where(a => a.Pet.Gender == gender && a.StatusList == 0)
                .Where(a => a.Province == province || a.Area == area)
                .ProjectTo<AdoptionDto>(_mapper.ConfigurationProvider)
                .AsNoTracking();

            return await PagedList<AdoptionDto>.CreateAsync(
                query,
                userParams.PageNumber,
                userParams.PageSize
            );
        }
    }
}
