using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SparkLogViewer.Core.Enums;
using SparkLogViewer.Core.Models;
using SparkLogViewer.Core.Services.Interfaces;
using System.Collections.ObjectModel;

namespace SparkLogViewer.ViewModels;

public partial class SparkLogViewerViewModel(ILogReaderService logReader, ILogParser logParser) : ObservableObject
{
    private readonly List<LogEntry> _allLogs = [];

    [ObservableProperty]
    private ObservableCollection<LogEntry> _logs = [];

    // 日志级别计数器
    [ObservableProperty]
    private int _fatalCount;

    [ObservableProperty]
    private int _errorCount;

    [ObservableProperty]
    private int _warnCount;

    [ObservableProperty]
    private int _infoCount;

    [ObservableProperty]
    private int _debugCount;

    [ObservableProperty]
    private bool _isAutoScrollEnabled;

    [ObservableProperty]
    private bool _isLoading;

    private LogLevel? _currentFilter;

    [RelayCommand]
    private async Task LoadLogAsync(string filePath)
    {
        if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath)) return;
        IsLoading = true;
        _allLogs.Clear();
        Logs.Clear();
        ResetCounts();

        await Task.Run(async () =>
        {
            var firstBatch = new List<LogEntry>();
            var isFirstBatchDispatched = false;
            ulong lineNumber = 0;
            LogEntry? lastEntry = null;

            await foreach (var line in logReader.ReadLinesAsync(filePath))
            {
                lineNumber++;
                if (logParser.TryParse(line, lineNumber, out var entry))
                {
                    if (lastEntry != null)
                    {
                        if (entry.Level == LogLevel.Unknown && lastEntry.Level is LogLevel.Fatal or LogLevel.Error)
                        {
                            entry.Level = lastEntry.Level;
                        }
                    }

                    _allLogs.Add(entry);
                    UpdateCount(entry.Level);
                    lastEntry = entry;

                    if (!isFirstBatchDispatched)
                    {
                        firstBatch.Add(entry);
                        if (firstBatch.Count >= 200)
                        {
                            UpdateUiWithBatch(firstBatch);
                            isFirstBatchDispatched = true;
                            firstBatch.Clear();
                        }
                    }
                }
            }

            if (!isFirstBatchDispatched && firstBatch.Count == 0)
            {
                UpdateUiWithBatch(firstBatch);
            }

            MainThread.BeginInvokeOnMainThread(() =>
            {
                Logs.Clear();
                foreach (var e in _allLogs)
                {
                    Logs.Add(e);
                }

                IsLoading = false;
            });
        });
    }

    private void UpdateUiWithBatch(List<LogEntry> batch)
    {
        MainThread.BeginInvokeOnMainThread(() =>
        {
            foreach (var e in batch)
            {
                Logs.Add(e);
            }
        });
    }

    [RelayCommand]
    private void FilterByLevel(LogLevel? level)
    {
        // 如果点击的是当前已激活的过滤器，则取消过滤
        _currentFilter = (_currentFilter == level) ? null : level;
        Logs.Clear();

        // 使用LINQ从内存中的完整列表进行过滤，速度非常快
        var filteredLogs = _allLogs.Where(log => _currentFilter == null || log.Level == _currentFilter);

        foreach (var log in filteredLogs)
        {
            Logs.Add(log);
        }
    }

    private void ResetCounts()
    {
        FatalCount = ErrorCount = WarnCount = InfoCount = DebugCount = 0;
    }

    private void UpdateCount(LogLevel level)
    {
        // 在UI线程更新计数器属性
        MainThread.BeginInvokeOnMainThread(() =>
        {
            switch (level)
            {
                case LogLevel.Fatal: FatalCount++; break;
                case LogLevel.Error: ErrorCount++; break;
                case LogLevel.Warning: WarnCount++; break;
                case LogLevel.Info: InfoCount++; break;
                case LogLevel.Debug: DebugCount++; break;
            }
        });
    }
}