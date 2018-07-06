Imports System.ComponentModel.DataAnnotations
Imports System.Web.Mvc

''' <summary>
''' Предоставляет модель данных сущности.
''' </summary>
Public Class Entity
	Inherits Entity(Of Guid)
	Implements IEntity, IEntity(Of Guid)

	Public Sub New()
		Id = Guid.NewGuid
	End Sub
End Class

''' <summary>
''' Предоставялет модель данных сущности.
''' </summary>
''' <typeparam name="TKey">Тип ключевого поля сущности.</typeparam>
Public Class Entity(Of TKey)
	Implements IEntity(Of TKey)

	<Key>
	<HiddenInput(DisplayValue:=False)>
	Public Overridable Property Id As TKey Implements IEntity(Of TKey).Id

	<Required(ErrorMessage:="Укажите название.")>
	<Display(Name:="Название", Order:=10)>
	Public Overridable Property Title As String Implements IEntity(Of TKey).Title
End Class
