Imports System.ComponentModel.DataAnnotations

Public Class CategoryAdminItem
	Public Property Id As Guid

	<Display(Name:="Название")>
	Public Property Title As String

	<Display(Name:="Имя")>
	Public Property Name As String

	<Display(Name:="Товары")>
	Public Property Products As Integer

	<Display(Name:="Порядок")>
	Public Property Order As Integer?

	<Display(Name:="Черновик")>
	<UIHint("Draft")>
	Public Property Draft As Boolean
End Class
