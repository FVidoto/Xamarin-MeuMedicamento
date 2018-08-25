using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MeuMedicamento.data
{
    class RestService : IRestService
    {
        HttpClient client;

        public List<Medicamento> listaMedicamentos { get; private set; }

        public RestService()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 256000;
        }

        public async Task<List<Medicamento>> GetMedicamentosNome(string nomeMedicamento)
        {
            listaMedicamentos = new List<Medicamento>();

            var uri = new Uri(string.Format(Constants.RestUrlNome, nomeMedicamento));
            Debug.WriteLine(@"ERROR {0}", uri);
            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    listaMedicamentos = JsonConvert.DeserializeObject<List<Medicamento>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return listaMedicamentos;
        }

        public async Task<List<Medicamento>> GetMedicamentosCodigo(string codigoMedicamento)
        {
            listaMedicamentos = new List<Medicamento>();

            var uri = new Uri(string.Format(Constants.RestUrlCodigoEan, codigoMedicamento));
            Debug.WriteLine(@"ERROR {0}", uri);
            try
            {
                var response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    listaMedicamentos = JsonConvert.DeserializeObject<List<Medicamento>>(content);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
            }

            return listaMedicamentos;
        }
    }
}
