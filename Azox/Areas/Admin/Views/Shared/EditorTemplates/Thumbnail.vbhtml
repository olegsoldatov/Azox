@ModelType Guid?
@Code
	Dim img As New TagBuilder("img")
	img.Attributes.Add("src", If(IsNothing(Model), Url.Content("~/Images/empty.svg"), Url.Action("thumbnail", "images", New With {.area = "", .id = Model})))
	img.MergeAttributes(New RouteValueDictionary(ViewData("htmlAttributes")), True)
	@Html.Raw(img.ToString(TagRenderMode.SelfClosing))
End Code
@Html.Hidden("", Model)


