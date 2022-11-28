using System;
namespace LSWBackend.Dtos;

public class ReplyDTO
{
    public ReplyDTO() {
    }

    public bool IsOK { get; set; }
    public string? ErrorMessage { get; set; }
}

