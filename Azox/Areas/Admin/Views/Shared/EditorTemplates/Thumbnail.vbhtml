@ModelType Guid?
@Code
	Dim id = Guid.Empty
	If Not IsNothing(Model) Then
		id = Model
	End If
End Code
@If id.Equals(Guid.Empty) Then
	@<img alt="@Html.DisplayNameForModel()" src="http://placehold.it/200x200" class="img-responsive" />
Else
	@<img alt="@Html.DisplayNameForModel()" src="~/images/thumbnail/@id" class="img-responsive" />
End If
@Html.Hidden("", id)


