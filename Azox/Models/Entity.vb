Imports System.ComponentModel.DataAnnotations
Imports Soldata.Azox

''' <summary>
''' Базовая сущность.
''' </summary>
Public MustInherit Class Entity
	Implements IEntity

	''' <summary>
	''' Устанавливает или возвращает идентификатор.
	''' </summary>
	<Key>
	Public Property Id As Guid Implements IEntity.Id

	''' <summary>
	''' Устанавливает или возвращает дату последнего изменения.
	''' </summary>
	<ScaffoldColumn(False)>
	<Display(Name:="Дата изменения")>
	Public Property LastUpdateDate As Date Implements IEntity.LastUpdateDate
End Class
