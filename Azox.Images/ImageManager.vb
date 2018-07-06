Imports System.Data.Entity
Imports System.Web

Public Class ImageManager
	Implements IDisposable

	''' <summary>
	''' Возвращает экземпляр контекста данных.
	''' </summary>
	''' <returns>Экземпляр <see cref="DbContext"/>.</returns>
	Protected Friend ReadOnly Property Context As DbContext

	''' <summary>
	''' Возвращает опции диспетчера изображений.
	''' </summary>
	Public ReadOnly Property Options As New ImageManagerOptions

	''' <summary>
	''' Инициализирует экземпляр класса <see cref="ImageManager"/>.
	''' </summary>
	''' <param name="context">Экземпляр контекста данных.</param>
	Public Sub New(context As DbContext)
		_Context = context
	End Sub

	''' <summary>
	''' Создает или обновляет изображение, связанное с моделью.
	''' </summary>
	''' <param name="imageId">Уникальный идентификатор изображения в базе данных.</param>
	<Obsolete("Функция будет удалена в следующей сборке.")>
	Public Overridable Sub CreateOrUpdateImage(imageFile As HttpPostedFileBase, ByRef imageId As Guid)
		If Not IsNothing(imageFile) Then
			Dim image = Context.Set(Of Image).FindAsync(imageId).Result
			If IsNothing(image) Then
				image = New Image With {.Id = Guid.NewGuid}
				imageId = image.Id
				Context.Entry(image).State = EntityState.Added
			End If
			With image
				.ContentType = imageFile.ContentType
				.Thumbnail = ImageUtility.ToBytes(imageFile, Options.Thumbnail)
				.Small = ImageUtility.ToBytes(imageFile, Options.Small)
				.Medium = ImageUtility.ToBytes(imageFile, Options.Medium)
				.Large = ImageUtility.ToBytes(imageFile, Options.Large)
				.Original = ImageUtility.ToBytes(imageFile)
			End With
			Context.SaveChanges()
		End If
	End Sub

	''' <summary>
	''' Получает экземпляр класса <see cref="ImageContent"/>.
	''' </summary>
	''' <param name="id">Уникальный идентификатор изображения.</param>
	''' <param name="size">Размер изображения.</param>
	''' <returns>Экземпляр класса <see cref="ImageContent"/>, содержащий данные изображения для вывода в поток HTTP.</returns>
	Public Overridable Function GetImageContent(id As Guid, size As ImageSize) As ImageContent
		Dim entity = Context.Set(Of Image).Find(id)

		If IsNothing(entity) Then Return Nothing

		Select Case size
			Case ImageSize.Large
				Return New ImageContent With {.Content = entity.Large, .ContentType = entity.ContentType}
			Case ImageSize.Medium
				Return New ImageContent With {.Content = entity.Medium, .ContentType = entity.ContentType}
			Case ImageSize.Small
				Return New ImageContent With {.Content = entity.Small, .ContentType = entity.ContentType}
			Case ImageSize.Thumbnail
				Return New ImageContent With {.Content = entity.Thumbnail, .ContentType = entity.ContentType}
			Case Else
				Return New ImageContent With {.Content = entity.Original, .ContentType = entity.ContentType}
		End Select
	End Function

#Region "IDisposable Support"
	Private disposedValue As Boolean

	Protected Overridable Sub Dispose(disposing As Boolean)
		If Not disposedValue Then
			If disposing Then
				_Context.Dispose()
			End If
		End If
		disposedValue = True
	End Sub

	Public Sub Dispose() Implements IDisposable.Dispose
		Dispose(True)
	End Sub
#End Region
End Class
