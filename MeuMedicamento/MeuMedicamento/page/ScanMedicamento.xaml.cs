using MeuMedicamento.data;
using System;
using System.Collections.Generic;
using System.Diagnostics;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeuMedicamento
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ScanMedicamento : ContentPage
	{

        public ScanMedicamento ()
		{
			InitializeComponent ();
		}

        async void OnButtonClicked(object sender, EventArgs args)
        {
            try
            {
                if (!String.IsNullOrEmpty(entryCodigoMedicamento.Text))
                {
                    listView.ItemsSource = null;
                    MedicamentoItemManager medicamentoManager = new MedicamentoItemManager(new RestService());

                    string codigoMedicamento = entryCodigoMedicamento.Text;
                    List<Medicamento> listaMedicamentos = new List<Medicamento>();
                    listaMedicamentos = await medicamentoManager.GetMedicamentosCodigo(codigoMedicamento);

                    if (!(listaMedicamentos.Count <= 0)) {
                        GenerateCardModel(listaMedicamentos);

                        listView.ItemsSource = listaMedicamentos;
                        entryCodigoMedicamento.Text = "";
                    }
                    else
                    {
                        await DisplayAlert("Erro ", "Nenhum medicamento encontrado com este codigo de barras", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Erro ", "Informe o codigo de barras do medicamento para realizar a consulta", "OK");
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
                await DisplayAlert("Erro ", ex.Message, "OK");
            }

        }

         public void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var medicamentoItem = e.SelectedItem as Medicamento;
            var detalheMedicamentoPage = new DetalheMedicamentoPage();
            detalheMedicamentoPage.BindingContext = medicamentoItem;
            Navigation.PushAsync(detalheMedicamentoPage);
        }

        private void GenerateCardModel(List<Medicamento> listaMedicamentos)
        {
            int i = 0;
            foreach (Medicamento m in listaMedicamentos)
            {
                m.AlertColor = i % 2 == 0 ? Color.Green : Color.Blue;
                i++;
            }
        }
    }
}