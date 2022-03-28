using Asp_Mvc.Data;
using Asp_Mvc.Models;
using Asp_Mvc.Views.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace Asp_Mvc.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _host;

        public ProfileController(ApplicationDbContext context, IWebHostEnvironment host)
        {
            _context = context;
            _host = host;
        }
        [Route("profile")]
        public async Task<IActionResult> Index(string id)
        {
            var viewModel = new ProfileViewModel();
            viewModel.Address = await _context.UserAddresses.Include(x => x.Address).FirstOrDefaultAsync(x => x.UserId == User.FindFirst("UserId").Value);
            var profileImageEntity = await _context.Images.FirstOrDefaultAsync(x => x.UserId == User.FindFirst("UserId").Value);
            viewModel.profileImage = new UserProfileImage
            {
                FileName = profileImageEntity.FileName
            };

            return View(viewModel);
        }



        public IActionResult FileUpload()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> FileUpload(ImageModel model)
        {
            var userId = User.FindFirst("UserId").Value;


            if (ModelState.IsValid)
            {

                var imageEntity = new ImageEntity

                {
                    FileName = $"{userId}_{ model.File.FileName}",
                    UserId = userId
                };

                //    string fileName = $"{Path.GetFileNameWithoutExtension(imageEntity.File.FileName)}_{Guid.NewGuid()}{Path.GetExtension(imageEntity.File.FileName)}";

                string filePath = Path.Combine($"{_host.WebRootPath}/profileImages", imageEntity.FileName);

                // uploading file to server
                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    await model.File.CopyToAsync(fs);
                }

                // saving file information to database
                //   imageEntity.FileName = fileName;
                //  _context.Add(imageEntity);
                _context.Images.Add(imageEntity);
                await _context.SaveChangesAsync();


                return RedirectToAction(nameof(Index));
            }

            return View(model);

        }

















        [Route("edit")]
        public async Task<IActionResult> Edit(string id)
        {
            var userAddress = await _context.UserAddresses.Include(x => x.Address).Include(x => x.User).FirstOrDefaultAsync(x => x.UserId == User.FindFirst("UserId").Value);
            var profileForm = new EditProfileForm
            {

                FirstName = userAddress.User.FirstName,
                LastName = userAddress.User.LastName,
               
                Email = userAddress.User.Email,
                StreetName = userAddress.Address.AddressLine,
                City = userAddress.Address.City,
                PostalCode = userAddress.Address.PostalCode,
                Country = userAddress.Address.Country,

            };

            return View(profileForm);

        }




        [HttpPost("Edit")]

        public async Task<IActionResult> Edit(string id, EditProfileForm profileForm)
        {
            var profile = new EditProfileForm();

            var addresENtity = await _context.UserAddresses.Include(x => x.Address).Include(x => x.User).FirstOrDefaultAsync(x => x.UserId == User.FindFirst("UserId").Value);

            var userEntity = await _context.Users.FindAsync(addresENtity.UserId);

        

            if (userEntity != null)
            {  
                userEntity.FirstName = profileForm.FirstName;
                userEntity.LastName = profileForm.LastName;
              
                userEntity.Email = profileForm.Email;
                addresENtity.Address.City = profileForm.City;

                addresENtity.Address.AddressLine = profileForm.StreetName;
                addresENtity.Address.PostalCode = profileForm.PostalCode;
                _context.Entry(userEntity).State = EntityState.Modified;
                _context.Update(userEntity).State = EntityState.Modified;
                _context.Database.AutoSavepointsEnabled = true;
               
                await _context.SaveChangesAsync();
            }

            else
            {
                userEntity = await _context.Users.FindAsync(addresENtity.UserId);
                _context.Entry(userEntity).State = EntityState.Modified;

                _context.Users.Update(userEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Profile");
            }

             
            
                {

                    if (addresENtity != null)
                    {

                        userEntity.FirstName = profileForm.FirstName;
                        userEntity.LastName = profileForm.LastName;
                        userEntity.Email = profileForm.Email;
                        addresENtity.Address.City = profileForm.City;

                        addresENtity.Address.AddressLine = profileForm.StreetName;
                        addresENtity.Address.PostalCode = profileForm.PostalCode;
                    _context.Entry(userEntity).State = EntityState.Modified;
                    _context.Users.Update(userEntity);

                    await _context.SaveChangesAsync();

                    _context.Entry(addresENtity).State = EntityState.Modified;

                    await _context.SaveChangesAsync();

                }
                }
                
                _context.Entry(userEntity).State = EntityState.Modified;
                _context.Users.Update(userEntity);
                
                await _context.SaveChangesAsync();

                _context.Entry(addresENtity).State = EntityState.Modified;

                await _context.SaveChangesAsync();


                return RedirectToAction("Index", "Profile");
            }
            
            



        }
    }




