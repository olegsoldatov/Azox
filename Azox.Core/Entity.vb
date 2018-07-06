Imports System.ComponentModel.DataAnnotations
Imports System.Web.Mvc

''' <summary>
''' Предоставляет базовую модель данных сущности.
''' </summary>
Public MustInherit Class Entity
	''' <summary>
	''' Устанавливает или возвращает уникальный идентификатор. По умолчанию возвращает новое сгенерированное значение.
	''' </summary>
	''' <returns>Структура <see cref="Guid"/>, содержащая уникальный идентификатор.</returns>
	<Key>
	<HiddenInput(DisplayValue:=False)>
	<Display(Order:=0)>
	Public Overridable Property Id As Guid = Guid.NewGuid

	''' <summary>
	''' Устанавливает или возвращает название.
	''' </summary>
	''' <returns>Строка, содержащая название.</returns>
	<Required(ErrorMessage:="Укажите название.")>
	<Display(Name:="Название", Order:=10)>
	Public Overridable Property Title As String
End Class
