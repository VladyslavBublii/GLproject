using Core.Models;
using DAL;
using System;

namespace BL
{
    public class SaveInput
    {
        private UnitOfWork unitOfWork;

        public SaveInput()
        {
            this.unitOfWork = new UnitOfWork();
        }

        public void SaveUser(object user)
        {

        }

        public void SaveCustomer()
        {

        }
    }
}
