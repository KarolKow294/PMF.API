using PMF.API.Entities;

namespace PMF.API.Services
{
    public interface IStorageService
    {
        Task<List<Storage>> GetAllAsync();
    }
}
