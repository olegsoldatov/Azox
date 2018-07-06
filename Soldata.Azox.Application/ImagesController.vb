Imports System.Data.Entity
Imports System.Web.Mvc
Imports Soldata.Azox.EntityFramework

''' <summary>
''' Предоставляет базовый контроллер для вывода в представление изображений.
''' </summary>
Public MustInherit Class ImagesController(Of TContext As {DbContext, IImageDbContext(Of Image)})
	Inherits Controller

	Private _Db As TContext

	''' <summary>
	''' Создает новый экземпляр контроллера для вывода изображений.
	''' </summary>
	''' <param name="context">Контекст данных.</param>
	Protected Sub New(context As TContext)
		_Db = context
	End Sub

	''' <summary>
	''' Возвращает экземпляр контекста данных.
	''' </summary>
	''' <returns>Экземпляр контекста данных.</returns>
	Public ReadOnly Property Db As TContext
		Get
			Return _Db
		End Get
	End Property

	Function Original(id As Guid) As ActionResult
		Return New ImageContentResilt(GetImageContent(id, ImageSize.Original))
	End Function

	<OutputCache(Duration:=30, VaryByParam:="none")>
	Function Large(id As Guid) As ActionResult
		Return New ImageContentResilt(GetImageContent(id, ImageSize.Large))
	End Function

	<OutputCache(Duration:=30, VaryByParam:="none")>
	Function Medium(id As Guid) As ActionResult
		Return New ImageContentResilt(GetImageContent(id, ImageSize.Medium))
	End Function

	<OutputCache(Duration:=30, VaryByParam:="none")>
	Function Small(id As Guid) As ActionResult
		Return New ImageContentResilt(GetImageContent(id, ImageSize.Small))
	End Function

	Function Thumbnail(id As Guid) As ActionResult
		Return New ImageContentResilt(GetImageContent(id, ImageSize.Thumbnail))
	End Function

	''' <summary>
	''' Получает экземпляр класса <see cref="ImageContent"/>, содержащий данные изображения для вывода в поток HTTP.
	''' </summary>
	''' <param name="id">Уникальный идентификатор изображения.</param>
	''' <param name="size">Размер изображения.</param>
	''' <returns>Экземпляр класса <see cref="ImageContent"/>, содержащий данные изображения для вывода в поток HTTP.</returns>
	Private Function GetImageContent(id As Guid, size As ImageSize) As ImageContent
		Dim entity = _Db.Images.Find(id)

		If IsNothing(entity) Then Return Nothing

		Select Case size
			Case ImageSize.Thumbnail
				Return New ImageContent With {.Content = entity.Thumbnail, .ContentType = entity.ContentType}
			Case ImageSize.Large
				Return New ImageContent With {.Content = entity.Large, .ContentType = entity.ContentType}
			Case ImageSize.Medium
				Return New ImageContent With {.Content = entity.Medium, .ContentType = entity.ContentType}
			Case ImageSize.Small
				Return New ImageContent With {.Content = entity.Small, .ContentType = entity.ContentType}
			Case Else
				Return New ImageContent With {.Content = entity.Original, .ContentType = entity.ContentType}
		End Select
	End Function

	Protected Overrides Sub Dispose(ByVal disposing As Boolean)
		If (disposing) Then
			If _Db IsNot Nothing Then
				_Db.Dispose()
				_Db = Nothing
			End If
		End If
		MyBase.Dispose(disposing)
	End Sub
End Class
