Imports System.Web.Mvc

''' <summary>
''' Статичная страница.
''' </summary>
Public Class StaticPage
    Inherits EntityPage

    <HiddenInput(DisplayValue:=False)>
    Public Property ControllerName As String
End Class
