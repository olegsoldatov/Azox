﻿''' <summary>
''' Модель содержания сущности.
''' </summary>
Public MustInherit Class EntityContent
    Inherits Entity
    Implements IEntityContent

    ''' <summary>
    ''' Заголовок.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <Display(Name:="Заголовок", Order:=1)>
    Public Property Heading As String Implements IEntityContent.Heading

    ''' <summary>
    ''' Содержание.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    <AllowHtml>
    <DataType(DataType.MultilineText)>
    <UIHint("WYSIWYG")>
    <Display(Name:="Содержание", Order:=2)>
    Public Property Content As String Implements IEntityContent.Content
End Class
