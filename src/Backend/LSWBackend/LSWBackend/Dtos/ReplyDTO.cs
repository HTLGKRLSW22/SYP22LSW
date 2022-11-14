using System;
namespace LSWBackend.Dtos
{
    public class ReplyDTO
    {
        public ReplyDTO()
        {
        }

        public bool isOK { get; set; }
        public string? ErrorMessage { get; set; }
    }
}

