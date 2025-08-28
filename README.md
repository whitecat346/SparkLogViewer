# SparkLogViewer
> 此项目为学习用途，请勿用于任何非法行为

## 简介
该项目为作者大半夜闲的没事干光速开发的一个Minecraft日志查看器，包含以下特性：
- 日志高亮

（没错，就这些……）

## 项目结构
> AI写项目结构真方便~

```
SparkLogViewer.sln (解决方案)
│
├── 📁 SparkLogViewer.Core (类库项目 - 定义核心逻辑和接口)
│   ├── 📁 Enums
│   │   └── LogLevel.cs             // 定义 Info, Warn, Error 等枚举
│   ├── 📁 Models
│   │   └── LogEntry.cs             // 定义单条日志的数据结构
│   ├── 📁 Parsers
│   │   └── MinecraftLogParser.cs   // Minecraft日志格式的具体解析器实现
│   └── 📁 Services
│       └── 📁 Interfaces
│           ├── ILogParser.cs       // 定义解析器的通用接口
│           └── ILogReaderService.cs  // 定义日志读取服务的接口
│
├── 📁 SparkLogViewer.Infrastructure (类库项目 - 提供具体的技术实现)
│   └── 📁 Services
│       └── 📁 Implementations
│           └── LogFileReaderService.cs // ILogReaderService接口的具体实现，负责文件IO
│
└── 📁 SparkLogViewer (MAUI项目 - UI和应用入口)
    ├── 📁 Platforms                 // MAUI平台特定代码 (自动生成)
    ├── 📁 Converters                
    │    └── LogLevelToTextColorConverter.cs // 用于将日志等级转换为文字颜色
    ├── 📁 Resources                 // 应用资源，如图片、字体、样式
    │   └── 📁 Styles
    │       └── Colors.xaml         // 可以在这里定义INFO/WARN/ERROR的颜色
    │       └── Styles.xaml
    ├── 📁 Selectors
    │   └── LogLevelTemplateSelector.cs // 用于根据日志级别切换CollectionView项模板
    ├── 📁 ViewModels
    │   └── SparkLogViewerViewModel.cs     // 主界面的ViewModel
    ├── 📁 Views
    │   └── SparkLogViewerView.xaml        // 主界面的XAML定义
    │   └── SparkLogViewerView.xaml.cs     // 主界面的后台代码
    ├── App.xaml                      // 应用级的资源定义
    ├── App.xaml.cs                   // 应用的生命周期代码
    ├── AppShell.xaml                 // 应用的导航结构
    ├── AppShell.xaml.cs
    └── MauiProgram.cs                // ⭐ 程序的入口，DI容器在这里配置
```

## 开发
使用Visual Studio 2022打开该项目（确保你按照了MAUI的工作负载）就行了……
用Visual Studio Code或者Rider也行，习惯哪个用哪个

> 这个项目还留有很多没有使用的接口，便于后续功能的接入什么的……
> 但是我很懒，实在不想写了 orz...