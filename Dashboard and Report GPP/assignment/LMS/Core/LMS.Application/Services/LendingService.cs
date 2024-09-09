using LMS.Application.Contracts;
using LMS.Application.Persistance;
using LMS.Domain.Entities;

namespace LMS.Application.Services
{
    public class LendingService : ILendingService
    {
        private readonly ILendingRepository _lendingRepository;

        public LendingService(ILendingRepository lendingRepository)
        {
            _lendingRepository = lendingRepository;
        }

        public Task<bool> AddNewLending(Lending lending)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteExistingLending(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Lending>> GetAllLendings(int a, int b)
        {
            throw new NotImplementedException();
        }

        public Task<Lending?> GetLendingById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdatedExistingLending(int id, Lending inputLending)
        {
            throw new NotImplementedException();
        }
    }
}