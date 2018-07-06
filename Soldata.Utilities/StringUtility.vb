''' <summary>
''' Содержит методы преобразования строк.
''' </summary>
Public Class StringUtility
	''' <summary>
	''' Преобразует строку в совместимую с файловой системой Windows, заменяя запрещенные символы на дефис.
	''' </summary>
	''' <param name="text">Исходная строка.</param>
	''' <returns>Преобразованная строка.</returns>
	Public Shared Function FileSystemName(text As String) As String
		If String.IsNullOrEmpty(text) Then
			Throw New ArgumentNullException(NameOf(text))
		End If
		Dim regex = "\/:*?""<>|~!@#$%^&=`"
		For i = 1 To Len(regex)
			text = Replace(text, Mid(regex, i, 1), "-")
		Next
		Return text
	End Function
End Class
