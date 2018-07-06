Imports System.Configuration

''' <summary>
''' Предоставляет визуализацию файлового менеджера CKFinder.
''' </summary>
Public NotInheritable Class CKFinder
    ''' <summary>
    ''' Устанавливает или возвращает ссылку на файл скрипта файлового менеджера.
    ''' </summary>
    ''' <value>Строка, содержащая виртуальный путь.</value>
    ''' <returns>Строка, содержащая виртуальный путь.</returns>
    ''' <remarks>По умолчанию значение берется из секции appSettings файла конфигурации приложения.</remarks>
    Public Shared Property ScriptFile As String = ConfigurationManager.AppSettings("CKFinder:ScriptFile")

    ''' <summary>
    ''' Отрисовывает скрипт, подключающий файловый менеджер.
    ''' </summary>
    Public Shared Function Render() As IHtmlString
        If String.IsNullOrEmpty(ScriptFile) Then Return New HtmlString(String.Empty)

        Dim result As New StringBuilder

        Dim scriptFileTag As New TagBuilder("script")
        scriptFileTag.Attributes.Add("src", VirtualPathUtility.ToAbsolute(ScriptFile))
        result.AppendLine(scriptFileTag.ToString(TagRenderMode.Normal))

        Dim scriptCodeTag As New TagBuilder("script")
        scriptCodeTag.InnerHtml = "window.onload = function() {var finder = new CKFinder(); finder.basePath = '" & VirtualPathUtility.ToAbsolute("~/CKFinder/") & "'; finder.create();};"
        result.AppendLine(scriptCodeTag.ToString(TagRenderMode.Normal))

        Return New HtmlString(result.ToString)
    End Function
End Class
