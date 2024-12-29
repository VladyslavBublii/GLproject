using AutoMapper;
using BL.DTO;
using BL.Services.Interfaces;
using Core.Enums;
using Core.Models;
using DAL.Interfaces;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordService _password;
        private readonly IEmailService _email;
        private readonly IMapper _mapper;

        public UserService()
        {
            _unitOfWork = new UnitOfWork();
            _password = new PasswordService();
            _email = new EmailService();

            _mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDTO>();
                cfg.CreateMap<Customer, CustomerDTO>();
            }).CreateMapper();
        }

        public async Task<UserDTO> GetUserAsync(Guid id)
        {
            var user = await _unitOfWork.Users.GetAsync(id);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<bool> IsPasswordSameAsync(string password)
        {
            var userDtos = await GetUsersAsync();
            foreach (var userDto in userDtos)
            {
                if (userDto.Password == password)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> IsEmailFreeAsync(string email)
        {
            var userDtos = await GetUsersAsync();
            foreach (var userDto in userDtos)
            {
                if (userDto.Email == email)
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<UserDTO> GetUserLogAsync(string email, string password, Role userRole)
        {
            var userDtos = await GetUsersAsync();
            foreach (var userDto in userDtos)
            {
                if (userDto.Email == email &&
                    userDto.Password == _password.GetHashString(password) &&
                    userDto.UserRole == userRole)
                {
                    return userDto;
                }
            }
            return null;
        }

        public async Task<IEnumerable<UserDTO>> GetUsersAsync()
        {
            var users = await _unitOfWork.Users.GetAllAsync();
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public async Task<CustomerDTO> GetCustomerAsync(Guid id)
        {
            var customer = await _unitOfWork.Customers.GetAsync(id);
            return _mapper.Map<CustomerDTO>(customer);
        }

        public async Task<CustomerDTO> GetCustomerByUserIdAsync(Guid userId)
        {
            var customer = await _unitOfWork.CustomersRepository.GetByUserIdAsync(userId);
            return _mapper.Map<CustomerDTO>(customer);
        }

        public async Task<IEnumerable<CustomerDTO>> GetCustomersAsync()
        {
            var customers = await _unitOfWork.Customers.GetAllAsync();
            return _mapper.Map<IEnumerable<CustomerDTO>>(customers);
        }

        public async Task SaveUserAsync(UserDTO userDTO, CustomerDTO customerDTO)
        {
            if (!_email.ValideEmail(userDTO.Email))
            {
                throw new Exception("Invalid Email");
            }
            if (_password.PasswordStrength(userDTO.Password) < PassStrength.Medium)
            {
                throw new Exception("Password not strong enough");
            }

            var user = new User
            {
                UserRole = userDTO.UserRole,
                Email = userDTO.Email,
                Password = _password.GetHashString(userDTO.Password),
            };

            var customer = new Customer
            {
                Name = customerDTO.Name,
                SurName = customerDTO.SurName,
                City = customerDTO.City,
                PostIndex = customerDTO.PostIndex,
                User = user
            };

            user.Customer = customer;

            await _unitOfWork.Users.CreateAsync(user);
            await _unitOfWork.Customers.CreateAsync(customer);

            await _unitOfWork.SaveAsync();
        }
    }
}
