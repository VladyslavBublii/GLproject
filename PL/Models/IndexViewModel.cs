using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PL.Models
{
	public class IndexViewModel
    {
        public IEnumerable<ProductViewModel> ProductViewModels { get; set; }
        public PageViewModel PageViewModel { get; set; }
        public string CurrentCategory { get; set; }
    }
}
