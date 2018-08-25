using MeuMedicamento.data;
using System;
using System.Collections.Generic;
using System.Diagnostics;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeuMedicamento
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BuscaMedicamento : ContentPage
	{
        public BuscaMedicamento ()
		{
			InitializeComponent ();
		}

        async void OnButtonClicked(object sender, EventArgs args)
        {
            try
            {
                if (!String.IsNullOrEmpty(entryMedicamento.Text))
                {
                    listView.ItemsSource = null;
                    MedicamentoItemManager medicamentoManager = new MedicamentoItemManager(new RestService());

                    string nomeMedicamento = entryMedicamento.Text;
                    List<Medicamento> listaMedicamentos = new List<Medicamento>();
                    listaMedicamentos = await medicamentoManager.GetMedicamentosNome(nomeMedicamento);

                    if (!(listaMedicamentos.Count <= 0))
                    {
                        GenerateCardModel(listaMedicamentos);

                        listView.ItemsSource = listaMedicamentos;
                        entryMedicamento.Text = "";
                    }
                    else
                    {
                        await DisplayAlert("Erro ", "Nenhum medicamento encontrado com este nome", "OK");
                    }
                }
                else {
                    await DisplayAlert("Erro ", "Informe o nome do medicamento para realizar a consulta", "OK");
                }
                    
            } catch (Exception ex)
            {
                Debug.WriteLine(@"ERROR {0}", ex.Message);
                await DisplayAlert("Erro ", ex.Message, "OK");
            }
            
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
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