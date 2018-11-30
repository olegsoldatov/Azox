@ModelType Guid?
@If IsNothing(Model) Then
	@<img alt="@ViewBag.Alt" src="http://placehold.it/640x480" class="@ViewBag.Class" itemprop="image" />
Else
	@<img alt="@ViewBag.Alt" src="@Url.Action("Large", "Images", New With {.id = Model})" class="@ViewBag.Class" itemprop="image" />
End If


