''' <summary>
''' Минимальный интерфейс базовой сущности с ключевым полем типа <see cref="Guid"/>.
''' </summary>
Public Interface IEntity
	Inherits IEntity(Of Guid)
End Interface

Public Interface IEntity(Of Out TKey)
	ReadOnly Property Id As TKey
	Property Title As String
End Interface
