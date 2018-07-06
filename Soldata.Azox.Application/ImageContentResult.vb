Imports System.Web.Mvc

Public Class ImageContentResilt
    Inherits ActionResult

    Private _ImageContent As ImageContent

    Public Sub New(imageContent As ImageContent)
        _ImageContent = imageContent
    End Sub

    Public Overrides Sub ExecuteResult(context As ControllerContext)
        If IsNothing(_ImageContent) Then
            context.HttpContext.Response.StatusCode = 404
        Else
            context.HttpContext.Response.BinaryWrite(_ImageContent.Content)
            context.HttpContext.Response.ContentType = _ImageContent.ContentType
        End If
    End Sub
End Class
