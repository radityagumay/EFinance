using EFI.DataParse;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EFI.Services
{
    public class Economy : IEconomy
    {
        #region members
        private string url = "http://radityalabs.net/sunnah/list_sunnah.php";
        #endregion

        public async void GetDataKeuangan(Action<EconomyDataParse, Exception> callback)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    EconomyDataParse content = JsonConvert.DeserializeObject<EconomyDataParse>(responseBody);
                    callback.Invoke(content, new Exception());
                }
            }
        }
    }
}
