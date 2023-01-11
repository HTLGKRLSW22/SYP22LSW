using System;
namespace UntisLib
{
    public class ClazzResult
    {
#pragma warning disable IDE1006
        public string jsonrpc { get; set; } = null!;
        public string id { get; set; } = null!;
        public Clazz[] result { get; set; } = null!;
#pragma warning restore IDE1006


    }
}

