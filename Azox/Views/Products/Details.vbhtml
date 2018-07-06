@ModelType Product
@Code
	ViewBag.Title = Model.Title
	ViewBag.Description = Model.Description
	ViewBag.EditUrl = Url.Action("edit", New With {.area = "admin", .id = Model.Id})
End Code

<h1>@Html.Raw(If(Model.Heading, Model.Title))</h1>

@Html.Raw(Model.Content)
