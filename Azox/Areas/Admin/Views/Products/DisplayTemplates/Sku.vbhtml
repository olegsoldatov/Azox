@ModelType String
@If Not String.IsNullOrWhiteSpace(Model) Then
	@<div class="text-muted" title="@Html.DisplayNameForModel">
		<small>@Model</small>
	</div>
End If
