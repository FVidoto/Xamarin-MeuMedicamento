using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MeuMedicamento
{
    class Medicamento
    {
        public string CodBarraEan { get; set; }
        public string PrincipioAtivo { get; set; }
        //Fabricante
        public string Laboratorio { get; set; }
        public string Cnpj { get; set; }
        //Descricao
        public string Apresentacao { get; set; }
        public string ClasseTerapeutica { get; set; }
        //PrecoFabrica 
        public string Pf0 { get; set; }
        //PrecoMaxConsumidor 
        public string Pmc0 { get; set; }
        //UltimaAtualizacao
        public string UltimaAlteracao { get; set; }
        public string Produto { get; set; }
        public string PrecoLiberado { get; set; }
        public string Restricao { get; set; }
        //teste
        public Color AlertColor { get; set; }
    }
}
