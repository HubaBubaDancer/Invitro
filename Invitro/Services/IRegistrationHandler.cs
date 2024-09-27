using Invitro.Areas.Identity.Data;
using Invitro.Dto;
using Invitro.Models;
using Microsoft.AspNetCore.Identity;

namespace Invitro.Services;

    public interface IRegistrationHandler
    {
        public Task RegisterDoctor(DoctorDto doctor);
        public Task RegisterUser(RegisterModel model);
    
    }

    public class RegistrationHandler : IRegistrationHandler
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public RegistrationHandler(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task RegisterUser(RegisterModel model)
        {
            var user = new ApplicationUser
            {
                UserName = model.firstName,
                Email = model.email,
                PhoneNumber = model.phone,
                FirstName = model.firstName,
                LastName = model.lastName
            };

            var result = await _userManager.CreateAsync(user, model.password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
            }
        }

        public async Task RegisterDoctor(DoctorDto doctorDto)
        {
            var user = await _userManager.FindByIdAsync(doctorDto.UserId.ToString());

            if (user == null)
            {
                return;
            }

            await _userManager.AddToRolesAsync(user, new[] { "Doctor" });

            var doctor = new Doctor
            {
                firstName = doctorDto.firstName,
                lastName = doctorDto.lastName,
                UserId = doctorDto.UserId
            };

            await _context.Doctors.AddAsync(doctor);
            await _context.SaveChangesAsync();
        }
    }