@Modeltype CatalogDocument
@Code
	ViewBag.Title = "Бренды"
	ViewBag.Description = ""
End Code

<header>
	<div class="container">
		<h1 class="h1">@ViewBag.Title</h1>
	</div>
</header>

<article>
	<div class="container">
		<div class="card-columns">
			@For Each item In Model.BrandList.Where(Function(x) Not String.IsNullOrEmpty(x.Title)).GroupBy(Function(m) m.Title.Substring(0, 1)).OrderBy(Function(x) x.Key)
				@<div class="card">
					<div class="card-body">
						<h5 class="card-title">@item.Key</h5>
						<ul class="list-unstyled">
							@For Each brand In item
								@<li>@Html.ActionLink(brand.Title, "brands", New With {.id = brand.Name.ToLower}, New With {.class = "card-link"})</li>
							Next
						</ul>
					</div>
				</div>
			Next
		</div>
	</div>
</article>
