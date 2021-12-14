using System.Threading.Tasks;
using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;

/*
 * popup Brugt til Confirmation, returnere true hvis Confrim og false ved cancel. 
 */
namespace Blazor.Pages
{
    public partial class ConfirmChoice:ComponentBase   
    {
        [CascadingParameter] public BlazoredModalInstance BlazoredModal { get; set; }

        private async Task Confirm()
        {
            await BlazoredModal.CloseAsync(ModalResult.Ok(true));
        }
        
        private async Task Cancel()
        {
            await BlazoredModal.CancelAsync();
        }
    }
}