''' <summary>
''' Определяет минимальный набор полей базовой сущности.
''' </summary>
Public Interface IEntity
	''' <summary>
	''' Уникальный идентификатор. При реализации должен быть ключевым полем.
	''' </summary>
	''' <returns>Структура <see cref="Guid"/>.</returns>
	Property Id As Guid
End Interface
