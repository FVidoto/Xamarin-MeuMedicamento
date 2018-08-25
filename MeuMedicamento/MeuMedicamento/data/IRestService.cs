using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MeuMedicamento.data
{
    interface IRestService
    {
        Task<List<Medicamento>> GetMedicamentosNome(string nomeMedicamento);

        Task<List<Medicamento>> GetMedicamentosCodigo(string codigoMedicamento);
    }
}
