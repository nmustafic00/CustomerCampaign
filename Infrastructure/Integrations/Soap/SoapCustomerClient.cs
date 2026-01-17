using CustomerCampaign.DTOs;
using System.Text;
using System.Xml.Linq;

namespace CustomerCampaign.Infrastructure.Integrations.Soap
{
    public class SoapCustomerClient : ISoapCustomerClient
    {
        private readonly HttpClient _httpClient;

        public SoapCustomerClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CustomerPreviewDto> FindPersonAsync(string id)
        {

            var url = "https://www.crcind.com:443/csp/samples/SOAP.Demo.cls";
            var soapAction = "http://tempuri.org/SOAP.Demo.FindPerson";

            var soapBody = $@"<?xml version=""1.0"" encoding=""utf-8""?>
            <soap:Envelope xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:tem=""http://tempuri.org"">
              <soap:Header/>
              <soap:Body>
                <tem:FindPerson>
                  <tem:id>{id}</tem:id>
                </tem:FindPerson>
              </soap:Body>
            </soap:Envelope>";


            using var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Headers.Add("SOAPAction", soapAction);
            request.Content = new StringContent(soapBody, Encoding.UTF8, "text/xml");

            var response = await client.SendAsync(request);
            var xml = await response.Content.ReadAsStringAsync();

            return ParseCustomerPreview(xml, id);

        }

        private CustomerPreviewDto? ParseCustomerPreview(string xml, string customerId)
        {
            var doc = XDocument.Parse(xml);

            var result = doc.Descendants().FirstOrDefault(x => x.Name.LocalName == "FindPersonResult");
            if (result == null) return null;

            DateTime? dob = null;
            var dobStr = result.Elements().FirstOrDefault(x => x.Name.LocalName == "DOB")?.Value;
            if (!string.IsNullOrWhiteSpace(dobStr) && DateTime.TryParse(dobStr, out var parsedDob))
            {
                dob = parsedDob;
            }

            int age = 0;
            var ageStr = result.Elements().FirstOrDefault(x => x.Name.LocalName == "Age")?.Value;
            if (!string.IsNullOrWhiteSpace(ageStr))
                int.TryParse(ageStr, out age);
            var home = result.Elements().FirstOrDefault(x => x.Name.LocalName == "Home");
            var homeAddress = new AddressDto
            {
                Street = home?.Elements().FirstOrDefault(x => x.Name.LocalName == "Street")?.Value ?? "",
                City = home?.Elements().FirstOrDefault(x => x.Name.LocalName == "City")?.Value ?? "",
                State = home?.Elements().FirstOrDefault(x => x.Name.LocalName == "State")?.Value ?? "",
                Zip = home?.Elements().FirstOrDefault(x => x.Name.LocalName == "Zip")?.Value ?? ""
            };

            return new CustomerPreviewDto
            {
                CustomerId = customerId,
                FullName = result.Elements().FirstOrDefault(x => x.Name.LocalName == "Name")?.Value ?? "",
                DateOfBirth = dob,
                Age = age,
                HomeAddress = homeAddress
            };
        }

    }

}
