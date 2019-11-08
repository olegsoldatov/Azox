@ModelType Azox.Page
@Code
	ViewBag.Title = If(Model.Title, Model.Name)
	ViewBag.Description = If(Model.Description, "Информация о компании.")
	ViewBag.Heading = If(Model.Heading, If(Model.Title, Model.Name))
	ViewBag.EditUrl = Url.Action("edit", "pages", New With {.area = "admin", .id = Model.Id, .returnUrl = Request.Url.PathAndQuery})
End Code

<div class="container">
	<h1>@ViewBag.Title</h1>
	@Html.Raw(Model.Content)
</div>

