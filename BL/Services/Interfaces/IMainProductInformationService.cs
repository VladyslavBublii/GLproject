using BL.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Services.Interfaces;

public interface IMainProductInformationService
{
    Task<IEnumerable<MainProductInformationDTO>> GetProductsAsync();
}