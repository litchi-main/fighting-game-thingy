using System;

public class NoControllerError : Exception
{
    public override string Message => "No controller found, defaulting to CPU VERSUS";
}
