@Code
	ViewBag.Title = "Возможность"
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

<div class="region-default">
	<div class="container">
		<div class="row">
			<div class="col-md-12">
				<h1>@ViewBag.Title</h1>
				<p><em>Возможность</em> используются для описания одного или нескольких ключевых функционалов продукта или услуги. Они могут чередоваться для лучшей наглядности в потоке страницы. <em>Возможности</em> могут быть заметными частями страницы, но не предназначены для замены <em>героев</em>.</p>
			</div>
		</div>
	</div>
</div>

<div class="container">
	<div class="row">
		<div class="col-md-12">
			<h2>Пример</h2>
			<hr />
			<section class="feature align-left bg-light">
				<picture>
					<source srcset="http://placehold.it/576x324" media="(min-width: 1200px)" />
					<source srcset="http://placehold.it/480x270" media="(min-width: 992px)" />
					<source srcset="http://placehold.it/352x264" media="(min-width: 768px)" />
					<source srcset="http://placehold.it/768x432" media="(min-width: 576px)" />
					<source srcset="http://placehold.it/576x324" media="(min-width: 0)" />
					<img srcset="http://placehold.it/576x324" src="http://placehold.it/555x312" alt="..." />
				</picture>
				<div>
					<div>
						<h2 class="heading">Heading</h2>
						<p class="paragraph">Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.</p>
						<a href="#" class="btn btn-primary">Call to action</a>
					</div>
				</div>
			</section>
		</div>
	</div>
</div>


