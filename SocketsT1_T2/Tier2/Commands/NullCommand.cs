using System;
using System.Threading.Tasks;
using SocketsT1_T2.Shared;
using SocketsT1_T2.Tier2.Util;

namespace SocketsT1_T2.Tier2.Commands
{
    public class NullCommand : ICommand
    
    {
        public Task<TransferObj> Execute()
        {
            
            Exception exception = new NotImplementedException("Den sendte request er ikke registreret commando");
            return ServerResponse.PrepareTransferObjectWithExceptionAsync(exception);
        }
    }
}