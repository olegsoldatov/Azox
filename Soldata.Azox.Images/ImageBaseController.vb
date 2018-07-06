Imports System.Data.Entity
Imports System.Web.Mvc

''' <summary>
''' Предоставляет базовый контроллер для вывода изображений.
''' </summary>
Public MustInherit Class ImageBaseController(Of TContext As {DbContext, IImageDbContext})
    Inherits Controller

    Private ImageManager As ImageManager(Of TContext)

    ''' <summary>
    ''' Создает новый экземпляр контроллера для вывода изображений.
    ''' </summary>
    ''' <param name="context">Контекст данных.</param>
    Protected Sub New(context As TContext)
        ImageManager = New ImageManager(Of TContext)(context)
    End Sub

    Function Original(id As Guid) As ActionResult
        Return New ImageContentResilt(ImageManager.GetImageContent(id))
    End Function

    <OutputCache(Duration:=30, VaryByParam:="none")>
    Function Large(id As Guid) As ActionResult
        Return New ImageContentResilt(ImageManager.GetImageContent(id, ImageSize.Large))
    End Function

    <OutputCache(Duration:=30, VaryByParam:="none")>
    Function Medium(id As Guid) As ActionResult
        Return New ImageContentResilt(ImageManager.GetImageContent(id, ImageSize.Medium))
    End Function

    <OutputCache(Duration:=30, VaryByParam:="none")>
    Function Small(id As Guid) As ActionResult
        Return New ImageContentResilt(ImageManager.GetImageContent(id, ImageSize.Small))
    End Function

    Function Thumbnail(id As Guid) As ActionResult
        Return New ImageContentResilt(ImageManager.GetImageContent(id, ImageSize.Thumbnail))
    End Function

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If (disposing) Then
            If ImageManager IsNot Nothing Then
                ImageManager.Dispose()
                ImageManager = Nothing
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub
End Class
