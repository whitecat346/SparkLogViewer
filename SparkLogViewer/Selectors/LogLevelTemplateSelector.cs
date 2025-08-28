using SparkLogViewer.Core.Enums;
using SparkLogViewer.Core.Models;

#pragma warning disable CS8618 // 在退出构造函数时，不可为 null 的字段必须包含非 null 值。请考虑声明为可以为 null。

namespace SparkLogViewer.Selectors;

public class LogLevelTemplateSelector : DataTemplateSelector
{
    public DataTemplate InfoTemplate { get; set; }
    public DataTemplate WarnTemplate { get; set; }
    public DataTemplate ErrorTemplate { get; set; }
    public DataTemplate FatalTemplate { get; set; }
    public DataTemplate DebugTemplate { get; set; }
    public DataTemplate UnknownTemplate { get; set; }

    /// <inheritdoc />
    protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
    {
        if (item is not LogEntry logEntry)
        {
            return UnknownTemplate;
        }

        return logEntry.Level switch
        {
            LogLevel.Info => InfoTemplate,
            LogLevel.Warning => WarnTemplate,
            LogLevel.Error => ErrorTemplate,
            LogLevel.Fatal => FatalTemplate,
            LogLevel.Debug => DebugTemplate,
            LogLevel.Unknown => UnknownTemplate,
            _ => throw new ArgumentOutOfRangeException(nameof(item))
        };
    }
}
