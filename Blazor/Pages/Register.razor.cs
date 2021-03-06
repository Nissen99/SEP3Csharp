using System.Threading.Tasks;
using Blazored.Modal;
using Blazored.Modal.Services;
using Entities;
using Microsoft.AspNetCore.Components;
/*
 * Popup brugt til at oprette user
 *
 * Hvis brugeren vælger confirm bliver den nyporettet user returneret
 * 
 */
namespace Blazor.Pages
{
    public partial class Register : ComponentBase

    {
        

        private string username;
        private string password;
        private string errorMessage;

        [CascadingParameter] public BlazoredModalInstance BlazoredModal { get; set; }
        private User User = new User(){Role = "StandardUser"};

        public async Task PerformRegister()
        {
            await BlazoredModal.CloseAsync(ModalResult.Ok(User));
        }
        
        private async Task Cancel()
        {
            await BlazoredModal.CancelAsync();
        }
    }
}