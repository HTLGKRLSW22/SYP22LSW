using System;
namespace UntisLib
{
#pragma warning disable IDE1006

    public class LoginInfo
    {
        public string jsonrpc { get; set; } = null!;
        public string id { get; set; } = null!;
        public LoginResult result { get; set; } = null!;
    }

    public class LoginResult
    {
        public string sessionId { get; set; } = null!;
        public int personType { get; set; }
        public int personId { get; set; }
        public int klasseId { get; set; }
    }
#pragma warning restore IDE1006

}
