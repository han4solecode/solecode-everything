// using LMS.Application.Contracts;
// using LMS.Application.Persistance;
// using LMS.Domain.Entities;

// namespace LMS.Application.Services
// {
//     public class UserService : IUserService
//     {
//         private readonly IUserRepository _userRepository;

//         public UserService(IUserRepository userRepository)
//         {
//             _userRepository = userRepository;
//         }

//         public async Task<bool> AddNewUser(User user)
//         {
//             try
//             {
//                 await _userRepository.Create(user);

//                 // if (user.Position == "Manager" || user.Position == "Librarian")
//                 // {

//                 // }

//                 return true;
//             }
//             catch (System.Exception)
//             {
//                 return false;
//             }
//         }

//         public async Task<bool> DeleteExistingUser(int id)
//         {
//             var user = await _userRepository.GetById(id);

//             if (user == null)
//             {
//                 return false;
//             }

//             try
//             {
//                 await _userRepository.Delete(user);
//                 return true;
//             }
//             catch (System.Exception)
//             {
//                 return false;
//             }
//         }

//         public async Task<IEnumerable<User>> GetAllUsers(int a, int b)
//         {
//             var users = await _userRepository.GetAll(a, b);

//             return users;
//         }

//         public async Task<User?> GetUserById(int id)
//         {
//             var user = await _userRepository.GetById(id);

//             return user;
//         }

//         public async Task<bool> UpdateExistingUser(int id, User inputUser)
//         {
//             var user = await _userRepository.GetById(id);

//             if (user == null)
//             {
//                 return false;
//             }

//             user.FirstName = inputUser.FirstName;
//             user.LastName = inputUser.LastName;
//             user.Email = inputUser.Email;
//             user.Address = inputUser.Address;
//             user.Position = inputUser.Position;

//             await _userRepository.Update(user);

//             return true;
//         }
//     }
// }

using LMS.Application.Contracts;
using LMS.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using TheArtOfDev.HtmlRenderer.Core;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace LMS.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<byte[]> GeneratePatronInfoByIdReport(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            
            var htmlContent = string.Empty;

            htmlContent += "<h1> Patron Info Report </h1>";
            htmlContent += "<h2> User Credentials </h2>";
            htmlContent += $"<p> First Name: {user!.FirstName} </p>";
            htmlContent += $"<p> Last Name: {user!.LastName} </p>";
            htmlContent += $"<p> Username: {user!.UserName} </p>";
            htmlContent += $"<p> Email: {user!.Email} </p>";
            htmlContent += $"<p> Libary Card Number: {user!.LibraryCard!.CardNumber} </p>";

            htmlContent += "<h2> Book Borrow History </h2>";
            htmlContent += "<table>";
            htmlContent += "<tr><th>No</th><th>Book Title</th><th>Borrow Date</th><th>Return Due Date</th><th>Returned Date</th></tr>";

            var userLendings = user.Lendings.ToList();

            var no = 1;
            
            userLendings.ToList().ForEach(item => {
                htmlContent += "<tr>";
                htmlContent += $"<td> {no++} </td>";
                htmlContent += $"<td> {item.Book!.Title} </td>";
                htmlContent += $"<td> {string.Format("{0:dddd, d MMMM yyyy}", item.BorrowDate)} </td>";
                htmlContent += $"<td> {string.Format("{0:dddd, d MMMM yyyy}", item.DueReturnDate)} </td>";
                htmlContent += $"<td> {string.Format("{0:dddd, d MMMM yyyy}", item.DateReturned)} </td>";
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

        public async Task<IEnumerable<AppUser>> Get10MostActiveMembers()
        {
            var members = await _userManager.Users.OrderByDescending(x => x.Lendings.Count()).Where(x => x.Lendings.Count != 0).Take(10).ToListAsync();

            return members;
        }

        public async Task<object> GetLibraryUserInfo(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user != null)
            {
                var info = new
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    LibraryCardNumber = user.LibraryCard!.CardNumber,
                    LibraryCardExpiryDate = user.LibraryCard!.ExpiryDate,
                    Penalty = user.Penalty,
                    BooksNotReturnedNum = user.Lendings.Count
                };

                return info;
            }

            return new
            {
                Message = "User not found"
            };
        }

        public async Task<object> GetPatronInfoById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return new
                {
                    Status = "Error",
                    Message = $"User with id {id} does not exist"
                };
            }

            var userLendings = user.Lendings.ToList();

            var info = new
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                Email = user.Email,
                LibraryCardNumber = user.LibraryCard!.CardNumber,
                Lendings = userLendings.Select(l => new {
                    Id = l.Id,
                    Title = l.Book!.Title,
                    BorrowDate = l.BorrowDate,
                    DueReturnDate = l.DueReturnDate,
                    DateReturned = l.DateReturned,
                })
            };

            return new {
                Status = "Success",
                Message = "User info retrieved successfully",
                Data = info
            };  
        }
    }
}