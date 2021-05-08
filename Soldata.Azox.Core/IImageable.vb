''' <summary>
''' Связывает сущность с изображением.
''' </summary>
Public Interface IImageable
	''' <summary>
	''' Устанавливает или возвращает идентификатор изображения.
	''' </summary>
	''' <returns>Структура <see cref="Guid"/>, содержащая идентификатор.</returns>
	Property ImageId As Guid?
End Interface
