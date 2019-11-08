@ModelType Guid?
@If IsNothing(Model) Then
	@<img src="http://placehold.it/544x306" alt="@ViewData("htmlAttributes").Alt" class="@ViewData("htmlAttributes").Class" itemprop="image" />
Else
	@<img src="@Url.Action("Medium", "Images", New With {.id = Model})" alt="@ViewData("htmlAttributes").Alt" class="@ViewData("htmlAttributes").Class" itemprop="image" />
End If
