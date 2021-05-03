Imports System.Data.Entity
Imports System.IO
Imports System.Threading.Tasks
Imports System.Drawing
Imports Soldata.Imaging

Public Class ImageService
	Implements IDisposable

	Private ReadOnly db As New ApplicationDbContext

	Public ReadOnly Property OriginalConfiguration As New ImageConfiguration
	Public ReadOnly Property ThumbnailConfiguration As New ImageConfiguration With {.Width = 200, .Height = 200}
	Public Property LargeConfiguration As ImageConfiguration
	Public Property MediumConfiguration As ImageConfiguration
	Public Property SmallConfiguration As ImageConfiguration

	Public Async Function UploadAsync(entity As IImageable, stream As Stream, contentType As String) As Task
		If IsNothing(entity) Then
			Throw New ArgumentNullException(NameOf(entity))
		End If

		If IsNothing(stream) Then
			Throw New ArgumentNullException(NameOf(stream))
		End If

		Dim image = Await db.Images.FindAsync(entity.ImageId.GetValueOrDefault)
		If IsNothing(image) Then
			image = db.Images.Add(New Image With {.Id = Guid.NewGuid})
			entity.ImageId = image.Id
		End If
		With image
			.ContentType = contentType
			.Original = ImageUtility.Generate(stream, contentType, OriginalConfiguration.Width, OriginalConfiguration.Height, OriginalConfiguration.Mode, OriginalConfiguration.Background)
			.Thumbnail = ImageUtility.Generate(stream, contentType, ThumbnailConfiguration.Width, ThumbnailConfiguration.Height, ThumbnailConfiguration.Mode, ThumbnailConfiguration.Background)
			.Large = If(IsNothing(LargeConfiguration), Nothing, ImageUtility.Generate(stream, contentType, LargeConfiguration.Width, LargeConfiguration.Height, LargeConfiguration.Mode, LargeConfiguration.Background))
			.Medium = If(IsNothing(MediumConfiguration), Nothing, ImageUtility.Generate(stream, contentType, MediumConfiguration.Width, MediumConfiguration.Height, MediumConfiguration.Mode, MediumConfiguration.Background))
			.Small = If(IsNothing(SmallConfiguration), Nothing, ImageUtility.Generate(stream, contentType, SmallConfiguration.Width, SmallConfiguration.Height, SmallConfiguration.Mode, SmallConfiguration.Background))
		End With
		Await db.SaveChangesAsync
	End Function

	Public Async Function DeleteAsync(entity As IImageable) As Task
		If IsNothing(entity) Then
			Throw New ArgumentNullException(NameOf(entity))
		End If
		Dim image = Await db.Images.FindAsync(entity.ImageId)
		If Not IsNothing(image) Then
			db.Images.Remove(image)
		End If
		Await db.SaveChangesAsync
	End Function

	Public Async Function DeleteAsync(entities As IEnumerable(Of IImageable)) As Task
		If IsNothing(entities) Then
			Throw New ArgumentNullException(NameOf(entities))
		End If
		db.Images.RemoveRange(Await db.Images.Join(entities.Select(Function(x) x.ImageId), Function(x) x.Id, Function(y) y, Function(x, y) x).ToListAsync)
		Await db.SaveChangesAsync
	End Function

#Region "IDisposable"

	Private disposedValue As Boolean

	Protected Overridable Sub Dispose(disposing As Boolean)
		If Not disposedValue Then
			If disposing Then
				db.Dispose()
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

Public Class ImageConfiguration
	Public Property Width As Integer
	Public Property Height As Integer
	Public Property Mode As StretchMode
	Public Property Background As Color = Color.Transparent
End Class
