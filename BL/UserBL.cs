using BL.DTO;
using Core.Models;
using DAL;
using System;

namespace BL
{
    public class UserBL
    {
        public  UnitOfWork _unitOfWork;

        public UserBL()
        {   
            _unitOfWork = new UnitOfWork();
        }
         
        public void SaveUser(UserDTO userDTO, CustomerDTO customerDTO)
        {

        } 
    }
}
