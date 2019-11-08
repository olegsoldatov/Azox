@ModelType IEnumerable(Of Brand)
@Code
	ViewBag.Title = "Бренды"
	ViewBag.Description = ""
End Code

<ol class="breadcrumb">
	<li class="breadcrumb-item">
		<a href="~/">Главная</a>
	</li>
	<li class="breadcrumb-item">
		<a href="~/products">Каталог</a>
	</li>
</ol>

<h1>@ViewBag.Title</h1>
<p class="lead">Продукция этих брендов в большом ассортименте представлена в нашем интернет-магазине.</p>

<ul class="list-unstyled list-alphabet">
	@For Each item In Model.GroupBy(Function(m) m.Name.Substring(0, 1))
		@<li>
			<div class="caption">@item.Key</div>
			<ul class="list-unstyled">
				@For Each brand In item
					@<li>@Html.ActionLink(brand.Name, "brand", "products", New With {.id = brand.Id}, Nothing)</li>
				Next
			</ul>
		</li>
	Next
</ul>