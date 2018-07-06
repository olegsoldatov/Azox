Imports System.Threading.Tasks
Imports Soldata.Azox

''' <summary>
''' Предоставляет API управления продуктами в источнике данных.
''' </summary>
Public Class ProductManager(Of TProduct As Product)
	Inherits EntityManager(Of TProduct)

	Public Sub New()
		MyBase.New(New ApplicationDbContext)
	End Sub

	Public Overrides Async Function CreateAsync(product As TProduct) As Task(Of ManagerResult)
		If product Is Nothing Then
			Throw New ArgumentNullException(NameOf(product))
		End If

		' Приведем отображаемый артикул в системное представление.
		product.SkuRaw = SkuToRaw(product.Sku)

		Return Await MyBase.CreateAsync(product)
	End Function

	Public Overrides Async Function UpdateAsync(product As TProduct) As Task(Of ManagerResult)
		If product Is Nothing Then
			Throw New ArgumentNullException(NameOf(product))
		End If

		' Приведем отображаемый артикул в системное представление.
		product.SkuRaw = SkuToRaw(product.Sku)

		Return Await MyBase.UpdateAsync(product)
	End Function

#Region "Вспомогательные методы"
	Public Shared Function SkuToRaw(text As String) As String
		Dim result = text.Replace("А", "A").Replace("а", "a") _
				.Replace("В", "B").Replace("в", "b") _
				.Replace("Е", "E").Replace("е", "e") _
				.Replace("И", "U").Replace("и", "u") _
				.Replace("К", "K").Replace("к", "k") _
				.Replace("М", "M").Replace("м", "m") _
				.Replace("Н", "H").Replace("н", "h") _
				.Replace("О", "O").Replace("о", "o") _
				.Replace("Р", "P").Replace("р", "p") _
				.Replace("С", "C").Replace("с", "c") _
				.Replace("Т", "T").Replace("т", "t") _
				.Replace("У", "Y").Replace("у", "y") _
				.Replace("Х", "X").Replace("х", "x")

		For i = 1 To Len(result)
			If Not Char.IsLetterOrDigit(Mid(result, i, 1)) Then
				text = Replace(text, Mid(result, i, 1), String.Empty)
			End If
		Next
		Return text.ToUpper
	End Function
#End Region
End Class
