using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using XF_FirebaseCrud.Services;
using XF_FirebaseCrud.Views;

namespace XF_FirebaseCrud.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _Username;
        public string Username
        {
            set
            {
                this._Username = value;
                OnPropertyChenge();
            }
            get
            {
                return this._Username;
            }
        }

        private string _Password;
        public string Password
        {
            set
            {
                this._Password = value;
                OnPropertyChenge();
            }
            get
            {
                return this._Password;
            }
        }

        private bool _Result;
        public bool Result
        {
            set
            {
                this._IsBusy = value;
                OnPropertyChenge();
            }
            get
            {
                return this._IsBusy;
            }
        }

        //Verificar se o login está sento executado
        private bool _IsBusy;
        public bool IsBusy
        {
            set
            {
                this._Result = value;
                OnPropertyChenge();
            }
            get
            {
                return this._Result;
            }
        }

        //Implementar Funcionalidades dos Botôes
        public Command LoginCommand { get; set; }
        public Command RegisterCommant { get; set; }

        public LoginViewModel()
        {
            LoginCommand = new Command(async () => await LoginCommandAsync());
            RegisterCommant = new Command(async () => await RegisterCommantAsync());
        }

        private async Task RegisterCommantAsync()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                var userService = new UserService();
                Result = await userService.RegisterUser(Username, Password);
                if (Result)
                {
                    await Application.Current.MainPage.DisplayAlert("Sucesso", "Usuário registrado!", "OK");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", "Falha ao registrar usuário", "OK");
                }
            }
            catch (Exception ex)
            {
                await  Application.Current.MainPage.DisplayAlert("Erro", "Falta da cominicação com o banco de dados" + ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task LoginCommandAsync()
        {
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                var userService = new UserService();
                Result = await userService.LoginUser(Username, Password);
                if (Result)
                {
                    Preferences.Set("Username", Username);
                    await Application.Current.MainPage.Navigation.PushAsync(new ContatosView());
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Erro", "Usuário/Senha inválido(s)", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Erro", "Falta da cominicação com o banco de dados" + ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
