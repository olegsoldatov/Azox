Imports System.ComponentModel.DataAnnotations
Imports Soldata.Azox

''' <summary>
''' Базовая сущность.
''' </summary>
Public MustInherit Class Entity
	Implements IEntity

	''' <summary>
	''' Идентификатор.
	''' </summary>
	<Key>
	Public Property Id As Guid Implements IEntity.Id
End Class
