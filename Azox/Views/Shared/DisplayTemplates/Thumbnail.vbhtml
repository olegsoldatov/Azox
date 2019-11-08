@ModelType Guid?
@If IsNothing(Model) Then
	@<img src="http://placehold.it/200x200" alt="@ViewData("htmlAttributes").Alt" class="@ViewData("htmlAttributes").Class" itemprop="image" />
Else
	@<img src="@Url.Action("Thumbnail", "Images", New With {.id = Model})" alt="@ViewData("htmlAttributes").Alt" class="@ViewData("htmlAttributes").Class" itemprop="image" />
End If
