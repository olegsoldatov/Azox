@ModelType Service
@Code
	ViewBag.Title = Model.Title
End Code

<h1>@Html.Raw(If(Model.Heading, Model.Title))</h1>

@If Not IsNothing(Model.Image) Then
	@<picture>
		<img src="@Url.Action("Original", "Images", New With {.id = Model.Image.Id})" alt="@Model.Title" />
	</picture>
End If

@Html.Raw(Model.Content)
