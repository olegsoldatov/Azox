Imports System.ComponentModel.DataAnnotations

''' <summary>
''' Абстрактный класс базовой сущности.
''' </summary>
Public MustInherit Class Entity
	Implements IEntity

	''' <summary>
	''' Инициализирует новый экземпляр класса <see cref="Entity"/>.
	''' </summary>
	Public Sub New()
		_Id = Guid.NewGuid
	End Sub

	''' <summary>
	''' Устанавливает или возвращает уникальный идентификатор сущности.
	''' </summary>
	''' <returns>Структура <see cref="Guid"/>, содержащая уникальный идентификатор сущности.</returns>
	<Key>
	Public Property Id As Guid Implements IEntity.Id
End Class
