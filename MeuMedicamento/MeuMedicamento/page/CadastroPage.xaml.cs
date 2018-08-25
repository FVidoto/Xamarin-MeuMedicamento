using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeuMedicamento
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CadastroPage : ContentPage
	{
		public CadastroPage ()
		{
			InitializeComponent ();
		}

        async void OnButtonClicked_Cadastrar(object sender, EventArgs args)
        {
            if (!String.IsNullOrEmpty(nomeEntry.Text)
                && !String.IsNullOrEmpty(emailEntry.Text) 
                && !String.IsNullOrEmpty(senhaEntry.Text)
                && !String.IsNullOrEmpty(senhaConfirmeEntry.Text)) {

                if (IsEmailValido(emailEntry.Text))
                {
                    if (ValidarSenha(senhaEntry.Text))
                    {
                        if (senhaEntry.Text.Equals(senhaConfirmeEntry.Text))
                        {
                            //verificar se o email nao pertence a outro usuario
                            Usuario usuario = await App.DataBase.buscaUsuarioEmail(emailEntry.Text);

                            if (usuario == null) {
                                Usuario user = new Usuario();
                                user.Nome = nomeEntry.Text;
                                user.Email = emailEntry.Text;
                                user.Senha = senhaEntry.Text;

                                //grava no banco de dados local
                                await App.DataBase.SaveItemAsync(user);

                                //realiza o cadastro e redireciona para a pagina principal logado
                                var rootPage = Navigation.NavigationStack.FirstOrDefault();
                                if (rootPage != null)
                                {
                                    App.IsUserLoggedIn = true;
                                    Navigation.InsertPageBefore(new TabPage(), Navigation.NavigationStack.First());
                                    await Navigation.PopToRootAsync();
                                }
                            }
                            else
                            {
                                await DisplayAlert("Erro ", "E-mail já cadastrado para outro usuário.", "OK");
                            }
                        }
                        else
                        {
                            await DisplayAlert("Erro ", "Senhas não conferem", "OK");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Erro ", "A senha deve conter no minimo 6 caracteres com letras e numeros.", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Erro ", "Email inválido", "OK");
                }
            }
            else
            {
                await DisplayAlert("Erro ", "Preencha todos os campos para se cadastrar", "OK");
            }
        }

        public bool IsEmailValido(string enderecoEmail)
        {
            try
            {
                MailAddress m = new MailAddress(enderecoEmail);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        bool ValidarSenha(string senha)
        {
            if (string.IsNullOrEmpty(senha) || senha.Length < 6)
            {
                return false;
            }

            if (!Regex.IsMatch(senha, "(?!^[0-9]*$)(?!^[a-zA-Z]*$)^([a-zA-Z0-9]{6,10})$"))
            {
                return false;
            }

            return true;
        }
    }
}