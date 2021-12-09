using System;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Entities;
using SocketsT1_T2.Shared;

namespace SocketsT1_T2.Tier2.Util
{
    public class ServerResponse
    {
        public static async Task<TransferObj> PrepareTransferObjectWithValueAsync<T>(T TObject)
        {
            string objectAsJson = JsonSerializer.Serialize(TObject);
            string action = "RETURN";
            
            if (TObject is Error)
            {
                 action = "Exception";
            }
            TransferObj transferObj = new TransferObj
            {
                Action = action, Arg = objectAsJson
            };
            return transferObj;

        }

        public static async Task<TransferObj> PrepareTransferObjectNoValueAsync()
        {
            string action = "RETURN";
            
            TransferObj transferObj = new TransferObj
            {
                Action = action
            };
            return transferObj;
        }

        public static async Task<TransferObj> PrepareTransferObjectWithException(Exception exception)
        {
            Error error = new Error(exception);
            return await PrepareTransferObjectWithValueAsync(error);
        }
    }
}