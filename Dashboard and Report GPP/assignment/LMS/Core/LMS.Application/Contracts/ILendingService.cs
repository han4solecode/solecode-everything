using LMS.Domain.Entities;

namespace LMS.Application.Contracts
{
    public interface ILendingService
    {
        Task<IEnumerable<Lending>> GetAllLendings(int a, int b);

        Task<Lending?> GetLendingById(int id);

        Task<bool> AddNewLending(Lending lending);

        Task<bool> UpdatedExistingLending(int id, Lending inputLending);

        Task<bool> DeleteExistingLending(int id);

        Task<IEnumerable<Lending>> GetAllLendingsNoPaging();

        Task<IEnumerable<object>> GetUsersOverdueBooksAndPenalty();

        Task<byte[]> GenerateUserOverdueBooksAndPenaltyReport();

        Task<IEnumerable<Lending>> GetOverdueBooks();
    }
}