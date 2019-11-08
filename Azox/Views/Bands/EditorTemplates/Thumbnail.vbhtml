@ModelType Guid?
<div class="thumbnail">
	@Html.Hidden("", Model)
	@If IsNothing(Model) Then
		@<img alt="@ViewBag.Alt" src="http://placehold.it/200x200" class="@ViewBag.Class" itemprop="image" />
	Else
		@<img alt="@ViewBag.Alt" src="@Url.Action("Thumbnail", "Images", New With {.id = Model})" class="@ViewBag.Class" itemprop="image" />
	End If
</div>


