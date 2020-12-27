@ModelType Guid?
@Code
	If Model IsNot Nothing Then
		Dim img As New TagBuilder("img")
		img.Attributes.Add("src", Url.Action("small", "images", New With {.area = "", .id = Model}))
		img.MergeAttributes(New RouteValueDictionary(ViewData("htmlAttributes")), True)
		@Html.Raw(img.ToString(TagRenderMode.SelfClosing))
	End If
End Code


