@ModelType IEnumerable(Of Product)

@If Model.Any Then
	@<text>...</text>
Else
	@<p class="lead">Список пуст.</p>
End If
