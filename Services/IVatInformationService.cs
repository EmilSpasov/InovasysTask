using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Inovasys.Models;

namespace Inovasys.Services
{
    public interface IVatInformationService
    {
        VatResultViewModel GetInformation(string countryCodeAndVatNumber);
    }
}
