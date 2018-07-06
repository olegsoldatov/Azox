Imports System.Configuration

''' <summary>
''' Предоставляет визуализацию текстового редактора.
''' </summary>
Public NotInheritable Class TextEditor
    ''' <summary>
    ''' Устанавливает или возвращает ссылку на файл скрипта текстового редактора.
    ''' </summary>
    ''' <value>Строка, содержащая виртуальный путь.</value>
    ''' <returns>Строка, содержащая виртуальный путь.</returns>
    ''' <remarks>По умолчанию значение берется из секции appSettings файла конфигурации приложения.</remarks>
    Public Shared Property TextEditorScriptFile As String = ConfigurationManager.AppSettings.Get("TextEditor:ScriptFile")

    ''' <summary>
    ''' Устанавливает или возвращает ссылку на файл скрипта файлового менеджера.
    ''' </summary>
    ''' <value>Строка, содержащая виртуальный путь.</value>
    ''' <returns>Строка, содержащая виртуальный путь.</returns>
    ''' <remarks>По умолчанию значение берется из секции appSettings файла конфигурации приложения.</remarks>
    Public Shared Property FileManagerScriptFile As String = ConfigurationManager.AppSettings.Get("FileManager:ScriptFile")

    ''' <summary>
    ''' Устанавливает или возвращает виртуальный базовый путь файлового менеджера.
    ''' </summary>
    ''' <value>Строка, содержащая виртуальный путь.</value>
    ''' <returns>Строка, содержащая виртуальный путь.</returns>
    ''' <remarks>По умолчанию значение берется из секции appSettings файла конфигурации приложения.</remarks>
    Public Shared Property FileManagerBasePath As String = ConfigurationManager.AppSettings.Get("FileManager:BasePath")

    ''' <summary>
    ''' Отрисовывает скрипт, подключающий текстовый редактор к указанному HTML-элементу.
    ''' </summary>
    ''' <param name="elementId">Идентификактор элемента.</param>
    ''' <exception cref="ArgumentException">Идентификактор элемента не должен быть пустой строкой.</exception>
    Public Shared Function Render(elementId As String) As IHtmlString
        If String.IsNullOrEmpty(elementId) Then Throw New ArgumentException("Значение не может быть равно NULL или быть пустым.", "elementId")
        If String.IsNullOrEmpty(TextEditorScriptFile) Then Return New HtmlString(String.Empty)

        Dim result As New StringBuilder

        Dim textEditorScriptFileTag As New TagBuilder("script")
        textEditorScriptFileTag.Attributes.Add("src", VirtualPathUtility.ToAbsolute(TextEditorScriptFile))
        result.AppendLine(textEditorScriptFileTag.ToString(TagRenderMode.Normal))

        Dim scriptCodeTag As New TagBuilder("script")
        If String.IsNullOrEmpty(FileManagerScriptFile) Or String.IsNullOrEmpty(FileManagerBasePath) Then
            scriptCodeTag.InnerHtml = BuildScript(elementId, String.Empty)
        Else
            Dim fileManagerScriptFileTag As New TagBuilder("script")
            fileManagerScriptFileTag.Attributes.Add("src", VirtualPathUtility.ToAbsolute(FileManagerScriptFile))
            result.AppendLine(fileManagerScriptFileTag.ToString(TagRenderMode.Normal))

            scriptCodeTag.InnerHtml = BuildScript(elementId, VirtualPathUtility.ToAbsolute(FileManagerBasePath))
        End If
        result.AppendLine(scriptCodeTag.ToString(TagRenderMode.Normal))

        Return New HtmlString(result.ToString)
    End Function

    Private Shared Function BuildScript(elementId As String, fileManagerBasePath As String) As String
        Dim script As New StringBuilder("window.onload = function() {CKEDITOR.replace('")
        script.Append(elementId).Append("'")

        If Not String.IsNullOrEmpty(fileManagerBasePath) Then
            With script
                .Append(",{")
                .AppendFormat("filebrowserBrowseUrl:'{0}ckfinder.html',", fileManagerBasePath)
                .AppendFormat("filebrowserImageBrowseUrl:'{0}ckfinder.html?type=Images',", fileManagerBasePath)
                .AppendFormat("filebrowserFlashBrowseUrl:'{0}ckfinder.html?type=Flash',", fileManagerBasePath)
                .AppendFormat("filebrowserUploadUrl:'{0}core/connector/aspx/connector.aspx?command=QuickUpload&type=Files',", fileManagerBasePath)
                .AppendFormat("filebrowserImageUploadUrl:'{0}core/connector/aspx/connector.aspx?command=QuickUpload&type=Images',", fileManagerBasePath)
                .AppendFormat("filebrowserFlashUploadUrl:'{0}connector/aspx/connector.aspx?command=QuickUpload&type=Flash'", fileManagerBasePath)
                .Append("}")
            End With
        End If

        script.AppendLine(");};")
        Return script.ToString
    End Function
End Class
