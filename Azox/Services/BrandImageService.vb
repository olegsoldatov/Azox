Imports System.Drawing
Imports System.IO
Imports System.Threading.Tasks
Imports Soldata.Azox
Imports Soldata.Imaging

Public Class BrandImageService
    Implements IImageService, IDisposable

    Private disposedValue As Boolean

    Private ReadOnly ImageManager As ImageManager
    Private ReadOnly Property OriginalConfiguration As ImageConfiguration
    Private ReadOnly Property ThumbnailConfiguration As ImageConfiguration
    Private ReadOnly Property SmallConfiguration As ImageConfiguration

    Public Sub New(imageManager As ImageManager)
        Me.ImageManager = imageManager
        OriginalConfiguration = New ImageConfiguration
        ThumbnailConfiguration = New ImageConfiguration With {.Width = 200, .Height = 200, .Mode = StretchMode.Uniform, .Background = Color.White}
        SmallConfiguration = New ImageConfiguration With {.Width = 320, .Height = 320, .Mode = StretchMode.Uniform, .Background = Color.White}
    End Sub

    Public Async Function UploadAsync(entity As IPictorial, imageFile As (InputStream As Stream, ContentType As String)) As Task Implements IImageService.UploadAsync
        If IsNothing(entity) Then
            Throw New ArgumentNullException(NameOf(entity))
        End If

        If Not (IsNothing(imageFile.InputStream) And IsNothing(imageFile.ContentType)) Then
            Dim image = Await ImageManager.FindByIdAsync(entity.ImageId)
            If IsNothing(image) Then
                image = New Image
                Await ImageManager.CreateAsync(image)
                entity.ImageId = image.Id
            End If
            With image
                .ContentType = imageFile.ContentType
                .Original = ImageUtility.Generate(imageFile.InputStream, imageFile.ContentType, OriginalConfiguration.Width, OriginalConfiguration.Height, OriginalConfiguration.Mode, OriginalConfiguration.Background)
                .Thumbnail = ImageUtility.Generate(imageFile.InputStream, imageFile.ContentType, ThumbnailConfiguration.Width, ThumbnailConfiguration.Height, ThumbnailConfiguration.Mode, ThumbnailConfiguration.Background)
                .Small = If(IsNothing(SmallConfiguration), Nothing, ImageUtility.Generate(imageFile.InputStream, imageFile.ContentType, SmallConfiguration.Width, SmallConfiguration.Height, SmallConfiguration.Mode, SmallConfiguration.Background))
            End With
            Await ImageManager.UpdateAsync(image)
        End If
    End Function

    Public Async Function DeleteAsync(entity As IPictorial) As Task Implements IImageService.DeleteAsync
        Dim image = Await ImageManager.FindByIdAsync(entity.ImageId)
        If image IsNot Nothing Then Await ImageManager.DeleteAsync(image)
        entity.ImageId = Nothing
    End Function

    Public Async Function DeleteRangeAsync(entities As IEnumerable(Of IPictorial)) As Task Implements IImageService.DeleteRangeAsync
        If IsNothing(entities) Then
            Throw New ArgumentNullException(NameOf(entities))
        End If
        Await ImageManager.DeleteRangeAsync(entities.Join(ImageManager.Images, Function(x) x.ImageId, Function(y) y.Id, Function(x, y) y))
        For Each item In entities
            item.ImageId = Nothing
        Next
    End Function

#Region "IDisposable"
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ImageManager.Dispose()
            End If
            disposedValue = True
        End If
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class
