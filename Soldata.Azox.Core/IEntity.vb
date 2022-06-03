' © Софт Бизнес, ООО. Все права защищены.

''' <summary>
''' Минимальный интерфейс сущности.
''' </summary>
Public Interface IEntity
	''' <summary>
	''' Устанавливает или возвращает идентификатор.
	''' </summary>
	''' <remarks>
	''' При реализации в производном классе должно быть ключевым полем.
	''' </remarks>
	Property Id As Guid

	''' <summary>
	''' Устанавливает или возвращает дату последнего изменения.
	''' </summary>
	Property LastUpdateDate As Date
End Interface
