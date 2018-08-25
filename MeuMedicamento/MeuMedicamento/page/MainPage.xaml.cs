using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MeuMedicamento
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        async void OnButtonClicked_Cadastro(object sender, EventArgs args)
        {
            var cadastroPage = new CadastroPage();
            await Navigation.PushAsync(cadastroPage);
        }

        async void OnButtonClicked_Login(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(emailEntry.Text) && !String.IsNullOrEmpty(senhaEntry.Text))
            {
                if (IsEmailValido(emailEntry.Text))
                {
                    if (ValidarSenha(senhaEntry.Text))
                    {
                        Usuario usuario = await App.DataBase.buscaUsuarioEmail(emailEntry.Text);

                        if (usuario == null) {
                            await DisplayAlert("Erro ", "Usuário não cadastrado.", "OK");
                        }
                        else {
                            var user = new Usuario
                            {
                                Email = emailEntry.Text,
                                Senha = senhaEntry.Text
                            };



                            var isValid = VerificaCredenciais(user, usuario);
                            if (isValid)
                            {
                                App.IsUserLoggedIn = true;
                                Navigation.InsertPageBefore(new TabPage(), this);
                                await Navigation.PopAsync();
                            }
                            else
                            {
                                await DisplayAlert("Login falhou ", "Usuario/senha não conferem", "OK");
                            }
                        }
                    }
                    else
                    {
                        await DisplayAlert("Login falhou ", "Usuario/senha não conferem", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Erro ", "Email inválido", "OK");
                }
            }
            else
            {
                await DisplayAlert("Erro ", "Preencha os campos de email e senha.", "OK");
            }
        }

        bool VerificaCredenciais(Usuario user, Usuario usuario)
        {
            //return user.Email == "paulo@gmail.com" && user.Senha == "123qwe";
            return user.Email == usuario.Email && user.Senha == usuario.Senha;
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
