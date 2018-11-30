@ModelType Guid?
@Html.Hidden("", Model)
<div>
	<a href="@Url.Action("changeimage", New With {.id = ViewBag.Id})" title="Изменить изображение">
		@If IsNothing(Model) Then
			@<img alt="@Html.DisplayNameForModel()" src="http://placehold.it/200x200" />
		Else
			@<img alt="@Html.DisplayNameForModel()" src="~/images/thumbnail/@Model" />
		End If
	</a>
</div>


