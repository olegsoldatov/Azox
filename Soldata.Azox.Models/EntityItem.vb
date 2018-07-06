''' <summary>
''' Базовый элемент коллекции сущностей.
''' </summary>
''' <remarks></remarks>
Public MustInherit Class EntityItem
    Inherits Entity
    Implements IEntityItem

    Sub New()
        PublishDate = Date.Now
    End Sub

    ''' <summary>
    ''' Устанавливает или возвращает дату публикации.
    ''' </summary>
    <DataType(DataType.Date)>
    <Display(Name:="Дата публикации", Order:=500)>
    Public Property PublishDate As Date Implements IEntityItem.PublishDate

    ''' <summary>
    ''' Устанавливает или возвращает порядок.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <UIHint("Number")>
    <Display(Name:="Порядок", Order:=501)>
    Public Property Order As Integer Implements IEntityItem.Order

    ''' <summary>
    ''' Устанавливает или возвращает отметку о публикации.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <UIHint("IsPublished")>
    <Display(Name:="Опубликовано", Order:=502)>
    Public Property IsPublished As Boolean Implements IEntityItem.IsPublished
End Class
