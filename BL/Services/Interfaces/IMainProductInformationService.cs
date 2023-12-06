using BL.DTO;
using System.Collections.Generic;

namespace BL.Services.Interfaces
{
    public interface IMainProductInformationService
    {
        IEnumerable<MainProductInformationDTO> GetProducts();
    }
}
