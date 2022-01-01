using System;
using System.Threading.Tasks;
using SocketsT1_T2.Shared;
using SocketsT1_T2.Tier2.Util;

/*
 * Commando klasse. Den klasse bruges til tilfældet at et kommando kald ikke findes. Så opstår der
 * ikke NullPointer problemer.
 */
namespace SocketsT1_T2.Tier2.Commands
{
    [MyCommand]
    public class NullCommand : ICommand
    
    {
        public string Action { get; }
        public TransferObj RequestObj { get; set; }
        public Task<TransferObj> Execute()
        {
            
            Exception exception = new NotImplementedException("Den sendte request er ikke registreret commando");
            return ServerResponse.PrepareTransferObjectWithExceptionAsync(exception);
        }
    }
}
