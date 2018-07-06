''' <summary>
''' Модель страницы.
''' </summary>
Public MustInherit Class EntityPage
    Inherits EntityContent
    Implements IEntityPage

    ''' <summary>
    ''' Описание.
    ''' </summary>
    <Display(Name:="Описание", Order:=201)>
    Public Property Description As String Implements IEntityPage.Description

    ''' <summary>
    ''' Ключевые слова.
    ''' </summary>
    <Display(Name:="Ключевые слова", Order:=202)>
    Public Property Keywords As String Implements IEntityPage.Keywords

    ''' <summary>
    ''' Ярлык.
    ''' </summary>
    <Display(Name:="Ярлык", Order:=203)>
    Public Property ActionName As String Implements IEntityPage.ActionName
End Class
