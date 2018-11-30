@ModelType Guid?
@If IsNothing(Model) Then
	@<img alt="@ViewBag.Alt" src="http://placehold.it/160x120" class="@ViewBag.Class" itemprop="image" />
Else
	@<img alt="@ViewBag.Alt" src="@Url.Action("Small", "Images", New With {.id = Model})" class="@ViewBag.Class" itemprop="image" />
End If


