Imports System.ComponentModel.DataAnnotations

''' <summary>
''' Предоставляет базовую модель данных сущности.
''' </summary>
Public Class Entity
	Implements IEntity

	''' <summary>
	''' Устанавливает или возвращает уникальный идентификатор сущности.
	''' </summary>
	''' <returns>Структура <see cref="Guid"/>, содержащая уникальный идентификатор сущности.</returns>
	<Key>
	Public Overridable Property Id As Guid = Guid.NewGuid Implements IEntity.Id
End Class
