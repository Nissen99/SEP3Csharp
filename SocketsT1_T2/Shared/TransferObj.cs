using System;
/*
 * Den klasse bruges til at pakke forskellige objekter ned som overføres over Socket forbindelsen.
 */
namespace SocketsT1_T2.Shared
{
    [Serializable]
    public class TransferObj
    {
        public string Action { get; set; }
        public string Arg { get; set; }
    }
}