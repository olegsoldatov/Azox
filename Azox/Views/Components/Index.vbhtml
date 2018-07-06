@Code
	ViewBag.Title = "Компоненты"
End Code

@Section Navbar
	<nav class="navbar navbar-expand-lg theme-light">
		<div class="container">
			<a class="navbar-brand" href="~/" aria-label="Soldata">
				<img src="~/Images/logo.svg" alt="Логотип Soldata" />
			</a>
			<button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
				<span class="navbar-toggler-icon"></span>
			</button>

			<div class="collapse navbar-collapse" id="navbarNav">
				@Html.Partial("Navigation")
			</div>
		</div>
	</nav>
End Section

<div class="container">
	<div class="row">
		<div class="col-md-12">
			<h1>@ViewBag.Title</h1>

		</div>
	</div>
</div>

