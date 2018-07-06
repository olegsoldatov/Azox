Imports System.Web
Imports System.ComponentModel.DataAnnotations
Imports System.IO

Public Class FileTypeValidationAttribute
	Inherits ValidationAttribute

	Dim _types As List(Of String)

	Public Sub New(types As String)
		_types = types.Replace(" ", "").Split(",").ToList()
	End Sub

	Public Overrides Function IsValid(value As Object) As Boolean
		If IsNothing(value) Then Return True

		Dim fileExt = Path.GetExtension(CType(value, HttpPostedFileWrapper).FileName).Substring(1)

		Return _types.Contains(fileExt, StringComparer.OrdinalIgnoreCase)
	End Function

	Public Overrides Function FormatErrorMessage(name As String) As String
		Return String.Format("Неправильный тип файла. Поддерживаются только типы {0}.", String.Join(", ", _types))
	End Function
End Class
