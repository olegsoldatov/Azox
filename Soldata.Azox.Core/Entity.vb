''' <summary>
''' Абстрактный класс базовой сущности.
''' </summary>
Public MustInherit Class Entity
	Implements IEntity

	''' <summary>
	''' Устанавливает или возвращает уникальный идентификатор сущности.
	''' </summary>
	''' <returns>Структура <see cref="Guid"/>, содержащая уникальный идентификатор сущности.</returns>
	<Key>
	Public Property Id As Guid Implements IEntity.Id
End Class
