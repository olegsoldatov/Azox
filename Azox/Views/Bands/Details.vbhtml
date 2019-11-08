@ModelType Brand
@Code
	ViewBag.Title = Model.Name
	ViewBag.Description = ""
End Code

<ol class="breadcrumb">
	<li class="breadcrumb-item">
		<a href="~/">Главная</a>
	</li>
	<li class="breadcrumb-item">
		<a href="~/products">Каталог</a>
	</li>
	<li class="breadcrumb-item">
		<a href="~/brands">Бренды</a>
	</li>
</ol>

<h1>@ViewBag.Title</h1>

<p class="lead">
	Продукция, представленная этим брендом.
</p>
