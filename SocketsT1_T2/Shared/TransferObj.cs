using System;

namespace Entities
{
    [Serializable]
    public class TransferObj<T>
    {
        public string Action { get; set; }
        public T Arg { get; set; }
    }
}