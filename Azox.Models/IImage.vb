''' <summary>
''' Минимальный интерфейс модели данных изображения с ключевым полем типа <see cref="Guid"/>.
''' </summary>
Public Interface IImage
	Inherits IImage(Of Guid)
End Interface

''' <summary>
''' Минимальный интерфейс модели данных изображения.
''' </summary>
''' <typeparam name="TKey">Тип ключевого поля.</typeparam>
Public Interface IImage(Of Out TKey)
	ReadOnly Property Id As TKey
	Property ContentType As String
	Property Content As Byte()
	Property Thumbnail As Byte()
End Interface
