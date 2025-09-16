using Godot;
using Godot.Collections;
using System;
using System.Text;

public partial class GameLogger : Logger
{
    private static readonly object _logLock = new();

	[Signal]
    public delegate void ErrorCaughtEventHandler(string msg);
	[Signal]
    public delegate void MessageCaughtEventHandler(string msg, bool error);

    public override void _LogError(
        string function,
        string file,
        int line,
        string code,
        string rationale,
        bool editorNotify,
        int errorType,
        Array<ScriptBacktrace> scriptBacktraces)
    {
        lock (_logLock)
        {
            var sb = new StringBuilder();
            // 这里不能使用AppendLine，因为在Godot环境下，\r\n会被视为两次换行（AppendLine会在字符串末尾添加\r\n)
            sb.Append($"Caught an error:\n");
            sb.Append($"function: {function}\n");
            sb.Append($"file: {file}\n");
            sb.Append($"line: {line}\n");
            sb.Append($"code: {code}\n");
            sb.Append($"rationale: {rationale}\n");
            sb.Append($"editor notify: {editorNotify}\n");
            sb.Append($"error type: {(ErrorType)errorType}\n");
            if (scriptBacktraces != null && scriptBacktraces.Count > 0)
            {
                sb.Append("script backtraces: \n");
                foreach (var backtrace in scriptBacktraces)
                {
                    sb.Append($"{backtrace.Format()}\n");
                }
            }
            using var filestream = FileAccess.Open("user://error_log.txt", FileAccess.ModeFlags.Write);
            filestream.StoreString(sb.ToString());
            filestream.Close();

            EmitSignalErrorCaught(sb.ToString());
        }
    }

    public override void _LogMessage(string message, bool error)
    {
        lock (_logLock)
        {
            EmitSignalMessageCaught(message, error);
        }
    }
}
