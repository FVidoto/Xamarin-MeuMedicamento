using MeuMedicamento.data;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace MeuMedicamento
{
	public partial class App : Application
	{
        public static bool IsUserLoggedIn { get; set; }

        private static UsuarioItemDatabase dataBase;

        public App ()
		{
			InitializeComponent();

            //MainPage = new MainPage();
            //MainPage = new NavigationPage(new MainPage());
            //MainPage = new NavigationPage(new CadastroPage());
            //MainPage = new NavigationPage(new BuscaMedicamento());
            //MainPage = new NavigationPage(new TabPage());
            
            if (!IsUserLoggedIn)
            {
                MainPage = new NavigationPage(new MainPage());
            }
            else
            {
                MainPage = new NavigationPage(new TabPage());
            }
        }

        internal static UsuarioItemDatabase DataBase
        {
            get
            {
                if (dataBase == null)
                {
                    dataBase = new UsuarioItemDatabase(DependencyService.Get<IFileHelper>().GetLocalFilePath("UsuarioSQLite.db3"));
                }
                return dataBase;
            }
        }

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
