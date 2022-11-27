using System;
using System.Collections.Generic;

namespace Contracts.Responses;

public abstract class ErrorOrResponse
{
    public bool IsValid { get; set; }
    public Lazy<List<string>> Errors { get; set; } = new Lazy<List<string>>();
}