using System;
using System.Text.Json;

namespace Entities
{
    [Serializable]
    public class TransferObj
    {
        public string Action { get; set; }
        public string Arg { get; set; }
    }
}