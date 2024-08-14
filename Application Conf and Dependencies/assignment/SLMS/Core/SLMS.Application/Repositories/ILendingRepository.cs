using SLMS.Domain.Entities;

namespace SLMS.Application.Repositories
{
    public interface ILendingRepository
    {
        // Task<IEnumerable<Lending>?> BorrowBook(int userId, int[] bookIds);

        // Task ReturnBook(int userId);

        Task<IEnumerable<Lending>> GetAllLendings();

        Task<Lending?> GetLendingById(int lendingId);

        Task AddLending(Lending lending);

        Task<Lending?> UpdateLending(int lendingId, Lending inputLending);

        Task<bool> DeleteLending(int lendingId);
    }
}