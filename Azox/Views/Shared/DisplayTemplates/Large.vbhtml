@ModelType Guid?
@If IsNothing(Model) Then
	@<img src="http://placehold.it/1200x450" alt="@ViewData("htmlAttributes").Alt" class="@ViewData("htmlAttributes").Class" itemprop="image" />
Else
	@<img src="@Url.Action("Large", "Images", New With {.id = Model})" alt="@ViewData("htmlAttributes").Alt" class="@ViewData("htmlAttributes").Class" itemprop="image" />
End If
