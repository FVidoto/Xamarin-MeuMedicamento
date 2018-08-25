using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeuMedicamento.data
{
    class MedicamentoItemManager
    {
        IRestService restService;

        public MedicamentoItemManager(IRestService service)
        {
            restService = service;
        }

        public Task<List<Medicamento>> GetMedicamentosNome(string nomeMedicamento)
        {
            return restService.GetMedicamentosNome(nomeMedicamento);
        }

        public Task<List<Medicamento>> GetMedicamentosCodigo(string codigoMedicamento)
        {
            return restService.GetMedicamentosCodigo(codigoMedicamento);
        }
    }
}
