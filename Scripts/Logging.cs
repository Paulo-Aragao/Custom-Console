using UnityEngine;
using System.Text;

public static class Logging
{
    public enum LogLevel
    {
        Debug,
        Gameplay,
        Services,
        System,
        Warning,
        Error,
        Lua,
    }
    
    
    public delegate void LogEventHandler(string message, LogLevel level, string stackTrace);
    public static event LogEventHandler OnLogEmitted;
    
    private static void InternalPrint(string message,LogLevel level)
    {
        switch (level)
        {
            case LogLevel.Debug: message = "[DEBUG] " + message; break;
            case LogLevel.Gameplay: message = "[INFO] " + message; break;
            case LogLevel.Warning: message = "[WARNING] " + message; break;
            case LogLevel.Error: message = "[ERROR] " + message; break;
            case LogLevel.Lua: message = "[LUA] " + message; break;
        }
        
        StringBuilder sb = new StringBuilder();
        sb.Append(message);
        
        string trace = level == LogLevel.Error
            ? System.Environment.StackTrace
            : string.Empty;
        
        //Debug.Log($"<color=#{ColorUtility.ToHtmlStringRGB(GetColor(level))}>{sb}</color>");
        OnLogEmitted?.Invoke(sb.ToString(), level, trace);
    }
    
    public static void Print(string message, LogLevel logLevel = default)
    {
        InternalPrint(message, logLevel);
    }
    
}
