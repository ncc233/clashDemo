using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashDemo.Interfaces
{
    public interface INavigationViewModel
    {
        Task<bool> NavigaedTo();
    }
}
