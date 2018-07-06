''' <summary>
''' Определение свойства для поддержки загрузки файлов изображений.
''' </summary>
Public Interface IPostedImageFileWrapper
	''' <summary>
	''' Загруженный на сервер файл изображения.
	''' </summary>
	''' <returns>HTTP-объект, предоставляющий доступ к файлу изображения, загруженного на сервер клиентом.</returns>
	Property ImageFile As HttpPostedFileWrapper
End Interface

''' <summary>
''' Размер изображения.
''' </summary>
Public Enum ImageSize
	Original
	Large
	Medium
	Small
	Thumbnail
End Enum

''' <summary>
''' Структура, содержащая данные изображения.
''' </summary>
Public Class ImageContent
	''' <summary>
	''' Устанавливает или возвращает бинарное содержание изображения.
	''' </summary>
	''' <returns>Массив байтов, содержащий бинарное содержание изображения.</returns>
	Property Content As Byte()

	''' <summary>
	''' Устанавливает или вовзращает MIME-тип изображения.
	''' </summary>
	''' <returns>Строка, содержащая условное обозначение MIME-типа.</returns>
	Property ContentType As String
End Class

''' <summary>
''' Предоставляет модель данных изображения.
''' </summary>
Public Class Image
	''' <summary>
	''' Устанавливает или возвращает уникальный идентификатор изображения.
	''' </summary>
	''' <returns>Структура <see cref="Guid"/>, содержащая уникальный идентификатор изображения.</returns>
	<Key>
	Public Property Id As Guid

	''' <summary>
	''' Устанавливает или вовзращает MIME-тип изображения.
	''' </summary>
	''' <returns>Строка, содержащая условное обозначение MIME-типа.</returns>
	Public Property ContentType As String

	''' <summary>
	''' Устанавливает или возвращает бинарное содержание оригинального изображения.
	''' </summary>
	''' <returns>Массив байтов, содержащий бинарное содержание изображения.</returns>
	<Column(TypeName:="Image")>
	Public Property Original As Byte()

	''' <summary>
	''' Устанавливает или возвращает бинарное содержание большого изображения.
	''' </summary>
	''' <returns>Массив байтов, содержащий бинарное содержание изображения.</returns>
	<Column(TypeName:="Image")>
	Public Property Large As Byte()

	''' <summary>
	''' Устанавливает или возвращает бинарное содержание среднего изображения.
	''' </summary>
	''' <returns>Массив байтов, содержащий бинарное содержание изображения.</returns>
	<Column(TypeName:="Image")>
	Public Property Medium As Byte()

	''' <summary>
	''' Устанавливает или возвращает бинарное содержание малого изображения.
	''' </summary>
	''' <returns>Массив байтов, содержащий бинарное содержание изображения.</returns>
	<Column(TypeName:="Image")>
	Public Property Small As Byte()

	''' <summary>
	''' Устанавливает или возвращает бинарное содержание миниатюры изображения.
	''' </summary>
	''' <returns>Массив байтов, содержащий бинарное содержание изображения.</returns>
	<Column(TypeName:="Image")>
	Public Property Thumbnail As Byte()
End Class

Partial Public Class ApplicationDbContext
	Public Property Images As DbSet(Of Image)
End Class