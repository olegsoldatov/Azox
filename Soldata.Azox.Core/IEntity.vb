' © Софт Бизнес. Все права защищены.

''' <summary>
''' Минимальный интерфейс для сущности с ключевым полем типа GUID.
''' </summary>
Public Interface IEntity
	Inherits IEntity(Of Guid)
End Interface

Public Interface IEntity(Of Out TKey)
	ReadOnly Property Id As TKey
End Interface
