using System;

namespace SocketsT1_T2.Shared
{
    [Serializable]
    public class TransferObj
    {
        public string Action { get; set; }
        public string Arg { get; set; }
    }
}