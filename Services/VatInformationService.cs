using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Xml;
using Inovasys.Models;

namespace Inovasys.Services
{
    public class VatInformationService : IVatInformationService
    {
        public VatResultViewModel GetInformation(string countryCodeAndVatNumber)
        {
            if (countryCodeAndVatNumber == null)
            {
                return null;
            }

            var countryCode = countryCodeAndVatNumber.Substring(0, 2);
            var vatNumber = countryCodeAndVatNumber.Substring(2);

            countryCode = countryCode.Trim();
            vatNumber = vatNumber.Trim().Replace(" ", string.Empty);

            const string url = "http://ec.europa.eu/taxation_customs/vies/services/checkVatService";
            const string xml = @"<s:Envelope xmlns:s='http://schemas.xmlsoap.org/soap/envelope/'><s:Body><checkVat xmlns='urn:ec.europa.eu:taxud:vies:services:checkVat:types'><countryCode>{0}</countryCode><vatNumber>{1}</vatNumber></checkVat></s:Body></s:Envelope>";

            try
            {
                using (var client = new WebClient())
                {
                    var doc = new XmlDocument();
                    doc.LoadXml(client.UploadString(url, string.Format(xml, countryCode, vatNumber)));
                    var response = doc.SelectSingleNode("//*[local-name()='checkVatResponse']") as XmlElement;
                    if (response == null || response["valid"]?.InnerText != "true")
                        return null;

                    var info = new VatResultViewModel();
                    info.CountryCode = response["countryCode"].InnerText;
                    info.VatNumber = response["vatNumber"].InnerText;
                    info.Name = response["name"]?.InnerText;
                    info.Address = response["address"]?.InnerText;
                    return info;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
