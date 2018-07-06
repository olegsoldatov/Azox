@ModelType Integer?
@Code
	Dim number = 0
	If Not IsNothing(Model) Then
		number = Model
	End If
	Dim list As New List(Of Integer)
	For i = -10 To 10
		list.Add(i)
	Next
End Code

@Html.DropDownList("", New SelectList(list, number), New With {.class = "form-control"})
