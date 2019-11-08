@ModelType Guid?
<div class="thumbnail">
	@Html.Hidden("", Model)
	@If Not IsNothing(Model) Then
		@<img alt="@ViewBag.Alt" src="@Url.Action("Thumbnail", "Images", New With {.id = Model})" class="@ViewBag.Class" itemprop="image" />
	End If
</div>
