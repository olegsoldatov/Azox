@ModelType Azox.Page
<ul class="nav nav-tabs">
	<li class="nav-item">
		<a class="nav-link active" id="home-tab" data-toggle="tab" href="#home" role="tab" aria-controls="home" aria-selected="true">Страница</a>
	</li>
	<li class="nav-item">
		<a class="nav-link" id="seo-tab" data-toggle="tab" href="#seo" role="tab" aria-controls="seo" aria-selected="false">SEO</a>
	</li>
</ul>

<div class="tab-content">
	<div class="tab-pane fade show active" id="home" role="tabpanel" aria-labelledby="home-tab">
		<div class="row">
			<div class="col-lg-12">
				<div class="form-group">
					@Html.LabelFor(Function(model) model.Title, htmlAttributes:=New With {.class = "control-label required"})
					@Html.ValidationMessageFor(Function(model) model.Title, "", New With {.class = "text-danger"})
					@Html.EditorFor(Function(model) model.Title, New With {.htmlAttributes = New With {.class = "form-control"}})
				</div>
				<div class="form-group">
					@Html.LabelFor(Function(model) model.Content, htmlAttributes:=New With {.class = "control-label"})
					@Html.ValidationMessageFor(Function(model) model.Content, "", New With {.class = "text-danger"})
					@Html.EditorFor(Function(model) model.Content, New With {.htmlAttributes = New With {.class = "form-control"}})
				</div>
				<div class="form-group">
					@Html.LabelFor(Function(model) model.AbsolutePath, htmlAttributes:=New With {.class = "control-label"})
					@Html.ValidationMessageFor(Function(model) model.AbsolutePath, "", New With {.class = "text-danger"})
					@Html.EditorFor(Function(model) model.AbsolutePath, New With {.htmlAttributes = New With {.class = "form-control", .placeholder = "/example"}})
				</div>
			</div>
		</div>
	</div>
	<div class="tab-pane fade" id="seo" role="tabpanel" aria-labelledby="seo-tab">
		<div class="row">
			<div class="col-lg-12">
				<div class="form-group">
					@Html.LabelFor(Function(model) model.Description, htmlAttributes:=New With {.class = "control-label"})
					@Html.ValidationMessageFor(Function(model) model.Description, "", New With {.class = "text-danger"})
					@Html.EditorFor(Function(model) model.Description, New With {.htmlAttributes = New With {.class = "form-control"}})
				</div>
				<div class="form-group">
					@Html.LabelFor(Function(model) model.Keywords, htmlAttributes:=New With {.class = "control-label"})
					@Html.ValidationMessageFor(Function(model) model.Keywords, "", New With {.class = "text-danger"})
					@Html.EditorFor(Function(model) model.Keywords, New With {.htmlAttributes = New With {.class = "form-control"}})
				</div>
			</div>
		</div>
	</div>
</div>
