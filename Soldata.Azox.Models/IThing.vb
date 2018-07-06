''' <summary>
''' Предоставляет определение элемента сущности.
''' </summary>
Public Interface IThing
	''' <summary>
	''' Уникальный идентификатор.
	''' </summary>
	Property Id As Guid

	''' <summary>
	''' Название.
	''' </summary>
	Property Title As String

	''' <summary>
	''' Заголовок.
	''' </summary>
	Property Heading As String

	''' <summary>
	''' Содержание.
	''' </summary>
	Property Content As String

	''' <summary>
	''' Порядок в перечислении.
	''' </summary>
	Property Order As Integer

	''' <summary>
	''' Отметка о публикации.
	''' </summary>
	Property IsPublished As Boolean

	''' <summary>
	''' Описание.
	''' </summary>
	Property Description As String

	''' <summary>
	''' Ключевые слова.
	''' </summary>
	Property Keywords As String

	''' <summary>
	''' Ярлык.
	''' </summary>
	Property ActionName As String

	''' <summary>
	''' Уникальный идентификатор изображения, связанного с сущностью.
	''' </summary>
	Property ImageId As Guid

	''' <summary>
	''' Загруженный на сервер файл изображения.
	''' </summary>
	''' <returns>HTTP-объект, предоставляющий доступ к файлу изображения, загруженного на сервер клиентом.</returns>
	Property ImageFile As HttpPostedFileWrapper
End Interface
