using LMS.Application.Contracts;
using LMS.Application.Persistance;
using LMS.Domain.Entities;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using TheArtOfDev.HtmlRenderer.Core;
using TheArtOfDev.HtmlRenderer.PdfSharp;

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

        public async Task<IEnumerable<Lending>> GetAllLendingsNoPaging()
        {
            var lendings = await _lendingRepository.GetAllNoPaging();

            return lendings;
        }

        public Task<Lending?> GetLendingById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdatedExistingLending(int id, Lending inputLending)
        {
            throw new NotImplementedException();
        }

        public async Task<byte[]> GenerateUserOverdueBooksAndPenaltyReport()
        {
            var userOverdueLendings = await _lendingRepository.GetUsersOverdueLendings();

            var no = 1;

            var htmlContent = string.Empty;

            htmlContent += "<h1> Overdue Borrow Report </h1>";
            htmlContent += "<table>";
            htmlContent += "<tr><th>No</th><th>Name</th><th>Book Title</th><th>Borrow Date</th><th>Return Due Date</th><th>Overdue Days</th><th>Penalty</th></tr>";

            userOverdueLendings.ToList().ForEach(item =>
            {
                htmlContent += "<tr>";
                htmlContent += "<td>" + no++ + "</td>";
                htmlContent += "<td>" + $"{item.AppUser!.FirstName} {item.AppUser!.LastName}" + "</td>";
                htmlContent += "<td>" + item.Book!.Title + "</td>";
                htmlContent += "<td>" + string.Format("{0:dddd, d MMMM yyyy}", item.BorrowDate) + "</td>";
                htmlContent += "<td>" + string.Format("{0:dddd, d MMMM yyyy}", item.DueReturnDate) + "</td>";
                htmlContent += "<td>" + (DateOnly.FromDateTime(DateTime.Now).DayNumber - item.DueReturnDate.DayNumber) + "</td>";
                htmlContent += "<td>" + $"Rp {item.Penalty}" + "</td>";
                htmlContent += "</tr>";
            });
            htmlContent += "<table>";

            var document = new PdfDocument();
            var pdfConfig = new PdfGenerateConfig
            {
                PageOrientation = PageOrientation.Landscape,
                PageSize = PageSize.A4
            };

            var cssFile = File.ReadAllText(@"./ReportTemplates/style.css");
            CssData css = PdfGenerator.ParseStyleSheet(cssFile);

            PdfGenerator.AddPdfPages(document, htmlContent, pdfConfig, css);

            var ms = new MemoryStream();
            document.Save(ms, false);
            var bytes = ms.ToArray();

            return bytes;
        }

        public async Task<IEnumerable<object>> GetUsersOverdueBooksAndPenalty()
        {
            var userOverdueLendings = await _lendingRepository.GetUsersOverdueLendings();

            var res = userOverdueLendings.Select(x => new {
                LendingId = x.Id,
                UserId = x.AppUserId,
                Name = $"{x.AppUser!.FirstName} {x.AppUser!.LastName}",
                BookId = x.BookId,
                BookTitle = $"{x.Book!.Title}",
                BorrowDate = x.BorrowDate,
                DueReturnDate = x.DueReturnDate,
                OverdueDays = DateOnly.FromDateTime(DateTime.Now).DayNumber - x.DueReturnDate.DayNumber,
                Penalty = x.Penalty
            });

            return res;
        }

        public async Task<IEnumerable<Lending>> GetOverdueBooks()
        {
            var userOverdueLendings = await _lendingRepository.GetUsersOverdueLendings();

            return userOverdueLendings;
        }
    }
}