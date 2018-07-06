'=============================
'Последняя редакция 09.08.2014
'=============================

Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

<Table("Contents")>
<ObsoleteAttribute>
Public Class Content
    Inherits Content(Of ContentCategory)
    Implements IContent
End Class

<ObsoleteAttribute>
Public Class Content(Of TCategory As ContentCategory)
    Inherits Entity
    Implements IContent

    Private _Categories As ICollection(Of TCategory)

    Public Sub New()
        _Categories = New HashSet(Of TCategory)()
    End Sub

    ''' <summary>
    ''' Устанавливает или возвращает название.
    ''' </summary>
    ''' <value>Строка, содержащая текст.</value>
    ''' <returns>Строка, содержащая название.</returns>
    <Display(Name:="Название", Order:=0)>
    Public Property Title As String Implements IContent.Title

    ''' <summary>
    ''' Устанавливает или возвращает описание.
    ''' </summary>
    ''' <value>Строка, содержащая текст.</value>
    ''' <returns>Строка, содержащая название.</returns>
    <Display(Name:="Описание", Order:=5)>
    Public Property Description As String Implements IContent.Description

    ''' <summary>
    ''' Устанавливает или возвращает текст содержания.
    ''' </summary>
    ''' <value>Строка, содержащая текст.</value>
    ''' <returns>Строка, содержащая текст.</returns>
    <UIHint("Text")>
    <DataType(DataType.MultilineText)>
    <Display(Name:="Содержание", Order:=10)>
    Public Property Text As String Implements IContent.Text

    ''' <summary>
    ''' Устанавливает или возвращает порядок элемента содержания в перечислении.
    ''' </summary>
    ''' <value>Целое число.</value>
    ''' <returns>Целое число.</returns>
    <Display(Name:="Порядок", Order:=50)>
    Public Property Order As Integer Implements IContent.Order

    ''' <summary>
    ''' Устанавливает или возвращает значение, указывающее, что содержание является черновиком.
    ''' </summary>
    ''' <value>Логическое значение.</value>
    ''' <returns>Логическое значение.</returns>
    <UIHint("Draft")>
    <Display(Name:="Черновик", Order:=60)>
    Public Property Draft As Boolean Implements IContent.Draft

    ''' <summary>
    ''' Устанавливает или возвращает уникальный идентификатор миниатюры, связанной с содержанием.
    ''' </summary>
    ''' <value>Структура <see cref="Guid" />, содержащая уникальный идентификатор.</value>
    ''' <returns>Структура <see cref="Guid" />, содержащая уникальный идентификатор.</returns>
    <Display(Name:="Миниатюра", Order:=30)>
    Public Property ThumbnailId As Guid Implements IContent.ThumbnailId

    ''' <summary>
    ''' Устанавливает или возвращает дату создания содержания.
    ''' </summary>
    ''' <value>Структура <see cref="Date" />, выраженная как дата и время суток.</value>
    ''' <returns>Структура <see cref="Date" />, выраженная как дата и время суток.</returns>
    <DataType(DataType.DateTime)>
    <Display(Name:="Дата публикации", Order:=40)>
    Public Property CreationDate As Date Implements IContent.CreationDate

    ''' <summary>
    ''' Устанавливает или возвращает дату обновления содержания.
    ''' </summary>
    ''' <value>Структура <see cref="Date" />, выраженная как дата и время суток.</value>
    ''' <returns>Структура <see cref="Date" />, выраженная как дата и время суток.</returns>
    <DataType(DataType.DateTime)>
    <Display(Name:="Дата изменения", Order:=45)>
    Public Property LastUpdatedDate As Date Implements IContent.LastUpdatedDate

    Public Overridable ReadOnly Property Categories As ICollection(Of TCategory)
        Get
            Return _Categories
        End Get
    End Property
End Class
