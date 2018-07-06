Imports System.Configuration

''' <summary>
''' Предоставляет визуализацию файлового менеджера.
''' </summary>
Public NotInheritable Class FileManager
    ''' <summary>
    ''' Устанавливает или возвращает виртуальный базовый путь файлового менеджера.
    ''' </summary>
    ''' <value>Строка, содержащая виртуальный путь.</value>
    ''' <returns>Строка, содержащая виртуальный путь.</returns>
    ''' <remarks>По умолчанию значение берется из секции appSettings файла конфигурации приложения.</remarks>
    Public Shared Property FileManagerBasePath As String = ConfigurationManager.AppSettings.Get("FileManager:BasePath")

    ''' <summary>
    ''' Отрисовывает скрипт, подключающий файловый менеджер.
    ''' </summary>
    Public Shared Function Render() As IHtmlString
        If String.IsNullOrEmpty(FileManagerBasePath) Then Return New HtmlString(String.Empty)
        Dim ckfinder As New CKFinder.FileBrowser
        ckfinder.BasePath = VirtualPathUtility.ToAbsolute(FileManagerBasePath)
        Return New HtmlString(ckfinder.CreateHtml)
    End Function
End Class
