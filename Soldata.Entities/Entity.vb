Imports System.ComponentModel.DataAnnotations
Imports System.Web.Mvc

Public MustInherit Class Entity
	Implements IEntity

	<Key>
	<HiddenInput(DisplayValue:=False)>
	Public Overridable Property Id As Guid = Guid.NewGuid Implements IEntity.Id

	<Required(ErrorMessage:="Укажите название.")>
	<Display(Name:="Название")>
	Public Overridable Property Title As String Implements IEntity.Title
End Class
