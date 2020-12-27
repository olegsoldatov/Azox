Imports System.ComponentModel.DataAnnotations

Public Class CategoryListItem
	Public Property Id As Guid
	<Display(Name:="Название")>
	Public Property Title As String
	Public Property Name As String
	Public Property Path As String
	Public Property ParentId As Guid?
	Public Property Popular As Popular
	<Display(Name:="Порядок")>
	Public Property Order As Integer?
	Public Property ImageId As Guid?
End Class
