''' <summary>
''' Определяет минимальный набор полей базовой вещи.
''' </summary>
Public Interface IThing
	Inherits IEntity

	''' <summary>
	''' Название. При реализации должен быть обязательным полем.
	''' </summary>
	''' <returns>Строка, содержащая название.</returns>
	Property Title As String
	''' <summary>
	''' Описание.
	''' </summary>
	''' <returns>Строка, содержащая описание.</returns>
	Property Description As String
	''' <summary>
	''' Уникальный идентификатор связанного с сущностью изображения.
	''' </summary>
	''' <returns>Структура <see cref="Guid"/>, содержащая уникальный идентификатор связанного изображения.</returns>
	Property ImageId As Guid?
End Interface
