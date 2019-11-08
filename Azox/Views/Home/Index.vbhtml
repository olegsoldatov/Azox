@Code
	ViewBag.Title = "Azox"
	ViewBag.Description = "Домашняя страница"
End code

@Section Hero
	<section class="hero theme-dark mask-50">
		<picture>
			<img src="~/Images/hero.jpg" alt="..." />
		</picture>
		<div>
			<div>
				<h1 class="h1">Bootstrap starter template</h1>
				<p class="lead">Use this document as a way to quickly start any new project.<br> All you get is this text and a mostly barebones HTML document.</p>
			</div>
		</div>
	</section>
End Section

<div class="region">
	<div class="container">
		<div class="row">
			<div class="col-md-4">
				<h2>Heading</h2>
				<p>Donec id elit non mi porta gravida at eget metus. Fusce dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh, ut fermentum massa justo sit amet risus. Etiam porta sem malesuada magna mollis euismod. Donec sed odio dui. </p>
				<p><a class="btn btn-secondary" role="button" href="#">View details »</a></p>
			</div>
			<div class="col-md-4">
				<h2>Heading</h2>
				<p>Donec id elit non mi porta gravida at eget metus. Fusce dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh, ut fermentum massa justo sit amet risus. Etiam porta sem malesuada magna mollis euismod. Donec sed odio dui. </p>
				<p><a class="btn btn-secondary" role="button" href="#">View details »</a></p>
			</div>
			<div class="col-md-4">
				<h2>Heading</h2>
				<p>Donec sed odio dui. Cras justo odio, dapibus ac facilisis in, egestas eget quam. Vestibulum id ligula porta felis euismod semper. Fusce dapibus, tellus ac cursus commodo, tortor mauris condimentum nibh, ut fermentum massa justo sit amet risus.</p>
				<p><a class="btn btn-secondary" role="button" href="#">View details »</a></p>
			</div>
		</div>
	</div>
</div>
