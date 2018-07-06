@If Not IsNothing(TempData("Message")) Then
	@<div class="alert alert-success alert-dismissible" role="alert">
		<button type="button" class="close" data-dismiss="alert" aria-label="Закрыть"><span aria-hidden="true">&times;</span></button>
		@TempData("Message")
	</div>
ElseIf Not IsNothing(TempData("Error")) Then
	@<div class="alert alert-danger alert-dismissible" role="alert">
		<button type="button" class="close" data-dismiss="alert" aria-label="Закрыть"><span aria-hidden="true">&times;</span></button>
		@TempData("Error")
	</div>
End If