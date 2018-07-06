Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

''' <summary>
''' Предоставляет модель данных содержания.
''' </summary>
<Table("Contents")>
Public Class Content
    Implements IContent

    ''' <summary>
    ''' Устанавливает или возвращает уникальный идентификатор.
    ''' </summary>
    ''' <value>Структура <see cref="Guid" />, содержащая уникальный идентификатор.</value>
    ''' <returns>Структура <see cref="Guid" />, содержащая уникальный идентификатор.</returns>
    <Key>
    Public Overridable Property Id As Guid Implements IContent.Id

    ''' <summary>
    ''' Устанавливает или возвращает название.
    ''' </summary>
    ''' <value>Строка, содержащая название.</value>
    ''' <returns>Строка, содержащая название.</returns>
    Public Overridable Property Title As String Implements IContent.Title

    ''' <summary>
    ''' Устанавливает или возвращает описание.
    ''' </summary>
    ''' <value>Строка, содержащая описание.</value>
    ''' <returns>Строка, содержащая описание.</returns>
    Public Overridable Property Description As String Implements IContent.Description

    ''' <summary>
    ''' Устанавливает или возвращает ключевые слова.
    ''' </summary>
    ''' <value>Строка, содержащая ключевые слова.</value>
    ''' <returns>Строка, содержащая ключевые слова.</returns>
    Public Overridable Property Keywords As String Implements IContent.Keywords

    ''' <summary>
    ''' Устанавливает или возвращает заголовок.
    ''' </summary>
    ''' <value>Строка, содержащая заголовок.</value>
    ''' <returns>Строка, содержащая заголовок.</returns>
    Public Overridable Property Heading As String Implements IContent.Heading

    ''' <summary>
    ''' Устанавливает или возвращает текст содержания.
    ''' </summary>
    ''' <value>Строка, содержащая текст содержания.</value>
    ''' <returns>Строка, содержащая текст содержания.</returns>
    Public Overridable Property Text As String Implements IContent.Text

    ''' <summary>
    ''' Устанавливает или возвращает имя действия.
    ''' </summary>
    ''' <value>Строка, содержащая имя действия.</value>
    ''' <returns>Строка, содержащая имя действия.</returns>
    Public Overridable Property ActionName As String Implements IContent.ActionName

    ''' <summary>
    ''' Устанавливает или возвращает имя контроллера.
    ''' </summary>
    ''' <value>Строка, содержащая имя контроллера.</value>
    ''' <returns>Строка, содержащая имя контроллера.</returns>
    Public Overridable Property ControllerName As String Implements IContent.ControllerName
End Class
