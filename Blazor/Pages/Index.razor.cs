using System;
using System.Threading.Tasks;
using Blazor.Authentication;
using Blazor.Model;
using Blazor.Model.UserModel;
using Blazored.Modal.Services;
using Entities;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Blazor.Pages
{
    public partial class Index : ComponentBase
    {
        [Inject] public IModalService ModalService { get; set; }
        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private IUserModel UserModel { get; set; }


        private string username;
        private string password;
        private string errorMessage;

        public async Task PerformLogin()
        {
            errorMessage = "";
            try
            {
                User user = new User() {Username = username, Password = password, Role = "StandardUser"};
                await ((CustomAuthenticationStateProvider) AuthenticationStateProvider).ValidateLogin(user);
                username = "";
                password = "";
            }
            catch (Exception e)
            {
                errorMessage = "Error logging in. Try again.";
            }
        }

        public async Task PerformLogout()
        {
            errorMessage = "";
            username = "";
            password = "";
            try
            {
                ((CustomAuthenticationStateProvider) AuthenticationStateProvider).Logout();
                NavigationManager.NavigateTo("/");
            }
            catch (Exception e)
            {
            }
        }
        
        private async void registerPopup()
        {
            var form = ModalService.Show<Register>();
            var result = await form.Result;
            if (!result.Cancelled)
            {
                User justCreated = (User) result.Data;
                await UserModel.RegisterUser(justCreated);
                
                StateHasChanged(); 
            }

        }
    }
}