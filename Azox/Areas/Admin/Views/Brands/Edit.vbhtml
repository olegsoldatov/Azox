@ModelType Brand
@Code
	ViewBag.Title = "Изменение бренда"
End Code

@Section Toolbar
	<button class="btn" form="model-form">
		<span class="fa fa-save"></span>
		<span>Сохранить</span>
	</button>
	<a class="btn" href="@Url.Action("brands", "catalog", New With {.id = Model.Name.ToLower, .area = ""})" target="_blank">
		<span class="fa fa-external-link"></span>
		<span>Посмотреть</span>
	</a>
	<a class="btn" href="@Url.Action("delete", New With {.id = Model.Id, .returnUrl = Url.Action("index")})">
		<span class="fa fa-remove"></span>
		<span>Удалить</span>
	</a>
End Section

<header>
	<h1 class="heading">@ViewBag.Title</h1>
</header>

<article>
	@Using Html.BeginForm(Nothing, Nothing, New With {.returnUrl = If(Request.QueryString("ReturnUrl"), Request.UrlReferrer.PathAndQuery)}, FormMethod.Post, New With {.id = "model-form"})
		@Html.AntiForgeryToken
		@<ul class="nav nav-tabs">
			<li class="nav-item">
				<a class="nav-link active" id="home-tab" data-toggle="tab" href="#home" role="tab" aria-controls="home" aria-selected="true">Бренд</a>
			</li>
			<li class="nav-item">
				<a class="nav-link" id="image-tab" data-toggle="tab" href="#image" role="tab" aria-controls="image" aria-selected="false">Изображение</a>
			</li>
		</ul>
		@<div class="tab-content">
			<div class="tab-pane fade show active" id="home" role="tabpanel" aria-labelledby="home-tab">
				@Html.HiddenFor(Function(model) model.Id)
				@Html.EditorForModel
			</div>
			<div class="tab-pane fade" id="image" role="tabpanel" aria-labelledby="image-tab">
				@If Model.ImageId Is Nothing Then
					@<p class="lead text-center">
						@Html.ActionLink("Добавьте", "createImage", New With {Model.Id, .returnUrl = Url.Action("edit", New With {.id = Model.Id, .returnUrl = If(Request.QueryString("ReturnUrl"), Request.UrlReferrer.PathAndQuery)})}, Nothing)
						изображение.
					</p>
				Else
					@<div class="d-flex">
						<div>
							@Html.EditorFor(Function(model) model.ImageId, "Thumbnail")
							<div class="text-center">
								@Html.ActionLink("Удалить", "deleteImage", New With {Model.Id, .returnUrl = Url.Action("edit", New With {.id = Model.Id, .returnUrl = If(Request.QueryString("ReturnUrl"), Request.UrlReferrer.PathAndQuery)})}, New With {.class = "btn btn-link"})
							</div>
						</div>
					</div>
				End If
			</div>
		</div>
	End Using
</article>

@Section Scripts
	@Scripts.Render("~/bundles/jqueryval")
End Section
