using HospitalWebsiteApi.Models;
using System.Security.Cryptography;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HospitalWebsiteApi.Services
{
        public class SqlService
        {
        private readonly ApplicationDbContext _context;

        public SqlService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool IsEmailRegistered(string email)
        {
            return _context.UserParameters.Any(u => u.Email == email);
        }


        ///  registracia  user

        public async Task<int> RegisterUserAsync(UserParameters user)
        {
    
            if (IsEmailRegistered(user.Email))
            {
                return -1;
            }

            var newUser = new UserParameters
            {
                UserRole = 0,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Password = HashPassword(user.Password)

            };

            _context.UserParameters.Add(newUser);
            await _context.SaveChangesAsync();
            return newUser.Id;
        }

        /// registracia doctor :
        public async Task<int> RegisterDoctorAsync(UserParameters doctor, string position)
        {
            try
            {
                if (IsEmailRegistered(doctor.Email))
                {
                    return -1;
                }

                var newUser = new UserParameters
                {
                    UserRole = 1,
                    Name = doctor.Name,
                    Surname = doctor.Surname,
                    Email = doctor.Email,
                    Password = HashPassword(doctor.Password)
                };

                _context.UserParameters.Add(newUser);
                await _context.SaveChangesAsync();

                var doctorRole = new Doctor_Roles
                {
                    DoctorID = newUser.Id.ToString(),
                    Position = position
                };

                _context.Doctor_Roles.Add(doctorRole);
                await _context.SaveChangesAsync();

                return newUser.Id;
            }
            catch (Exception ex)
            {
                throw new Exception("Doctor registration failed: " + ex.Message);
            }
        }




        public string HashPassword(string password)
        {
        using (var deriveBytes = new Rfc2898DeriveBytes(password, saltSize: 0, iterations: 10, HashAlgorithmName.SHA256))
        {
                byte[] salt = null;
                byte[] hashedPassword = deriveBytes.GetBytes(32); 
            return  Convert.ToBase64String(hashedPassword);
        }
    }
        public async Task<UserParameters> GetUserByIdAsync(int userId)
        {
   
            return await _context.UserParameters.FindAsync(userId);

        }

//    piradi informaciis redaqtireba
        public async Task UpdateUserNameAsync(int userId, string newName)
        {
            var user = await GetUserByIdAsync(userId);

           /// var currentUser = _context.Users.where

            if (user != null)
            {
                user.Name = newName;
                await _context.SaveChangesAsync();
            }
        }
        public async Task UpdateUserPersonalIdAsync(int userId, int newPersonalId)
        {
            var user = await GetUserByIdAsync(userId);

            user.PersonalID = user != null ? newPersonalId : user.PersonalID;
            await _context.SaveChangesAsync();

            //if (user != null)
            //{
            //    user.PersonalID = newPersonalId;
            //    await _context.SaveChangesAsync();
            //}
        }
        public async Task UpdateSurnameAsync(int userId, string NewSurname)
        {
            var user = await GetUserByIdAsync(userId);
            if (user != null)
            {
                user.Surname = NewSurname;
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateUserAddressAsync(int userId, string NewAddress)
        {
            var user = await GetUserByIdAsync(userId);
            if (user != null)
            {
                user.Address = NewAddress;
                await _context.SaveChangesAsync();
            }

        }
        public async Task UpdateUserPhoneAsync(int userId, int newPhone)
        {
            var user = await GetUserByIdAsync(userId);
            if (user != null)
            {
                user.Phone = null;
                user.Phone = newPhone;
                await _context.SaveChangesAsync();
            }
        }
        public async Task UpdateUserBirthdateAsync(int userId, DateTime? newBirthdate)
        {
            var user = await GetUserByIdAsync(userId);
            if (user != null)
            {
                user.Birthdate = newBirthdate;
                await _context.SaveChangesAsync();
            }
        }


        // avtorizaciistvisss :

        public async Task<UserParameters> FindByEmailAsync(string email)
        {
            return await _context.UserParameters.FirstOrDefaultAsync(u => u.Email == email);
        }

        public bool VerifyPassword( string providedPassword , string storedPasswordHash)
        {

            if (storedPasswordHash != null)
            {
                string hashedPassword = HashPassword(providedPassword);

                if (storedPasswordHash.Equals(hashedPassword))
                {
                    return true;
                }
            }

            return false;
        }


        // javshani 

        public async Task<int> ScheduleAppointmentAsync(Appointment appointment)
        {

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return appointment.Id;
        }
        public async Task<bool> IsDoctorAppointmentSlotAvailableAsync(string doctorId, string date, string time)
        {
            bool hasAppointments = await _context.Appointments
                   .AnyAsync(a => a.DoctorId == doctorId && a.Date == date && a.Time == time);


            return !hasAppointments;
        }
        public async Task<bool> IsUserAppointmentSlotAvailableAsync(string userId, string date, string time)
        {
            bool userHasAnotherAppointments = await _context.Appointments
            .AnyAsync(a => a.UserId == userId && a.Date == date && a.Time == time);


            return !userHasAnotherAppointments;
        }

    }



}