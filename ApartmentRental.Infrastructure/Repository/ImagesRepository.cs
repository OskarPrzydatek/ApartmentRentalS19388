using ApartmentRental.Core.Entities;
using ApartmentRental.Infrastructure.Context;
using ApartmentRental.Infrastructure.Exeptions;
using Microsoft.EntityFrameworkCore;

namespace ApartmentRental.Infrastructure.Repository
{
    public class ImagesRepository : IImagesRepository
    {
        private readonly MainContext _mainContext;

        public ImagesRepository(MainContext mainContext)
        {
            _mainContext = mainContext;
        }

        public async Task AddAsync(Image entity)
        {
            entity.DateOfCreation = DateTime.UtcNow;
            await _mainContext.AddAsync(entity);
            await _mainContext.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(int id)
        {
            if (_mainContext.Image != null)
            {
                var imageToDelete = await _mainContext.Image.SingleOrDefaultAsync(x => x.Id == id);
                if (imageToDelete == null) throw new EntityNotFoundException();
                _mainContext.Image.Remove(imageToDelete);
            }

            await _mainContext.SaveChangesAsync();

            throw new EntityNotFoundException();
        }

        public async Task<IEnumerable<Image>> GetAllAsync()
        {
            var image = await (_mainContext.Image ?? throw new InvalidOperationException()).ToListAsync();

            return image;
        }

        public async Task<Image> GetByIdAsync(int id)
        {
            var image = await (_mainContext.Image ?? throw new InvalidOperationException()).SingleOrDefaultAsync(x => x.Id == id);
            if (image == null)
            {
                throw new EntityNotFoundException();
            }

            return image;
        }

        public async Task UpdateAsync(Image entity)
        {
            if (_mainContext.Image != null)
            {
                var imageToUpdate = await _mainContext.Image.SingleOrDefaultAsync(x => x.Id == entity.Id);
                if (imageToUpdate == null)
                {
                    throw new EntityNotFoundException();
                }

                imageToUpdate.Data = entity.Data;
                imageToUpdate.ApartmentId = entity.ApartmentId;
            }

            await _mainContext.SaveChangesAsync();
        }
    }
}