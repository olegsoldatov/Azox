Imports System.ComponentModel.DataAnnotations

Public Class ChangeImageViewModel
	<HiddenInput(DisplayValue:=False)>
	Public Property Id As Guid

	<Display(Name:="Файл изображения", Order:=35)>
	Public Property ImageFile As HttpPostedFileWrapper
End Class