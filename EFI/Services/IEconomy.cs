using EFI.DataParse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFI.Services
{
    public interface IEconomy
    {
        void GetDataKeuangan(Action<EconomyDataParse, Exception> callback);
    }
}
