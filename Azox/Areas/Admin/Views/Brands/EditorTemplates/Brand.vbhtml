@ModelType Brand
<ul class="nav nav-tabs">
	<li class="nav-item">
		<a class="nav-link active" id="home-tab" data-toggle="tab" href="#home" role="tab" aria-controls="home" aria-selected="true">Бренд</a>
	</li>
	<li class="nav-item">
		<a class="nav-link" id="seo-tab" data-toggle="tab" href="#seo" role="tab" aria-controls="seo" aria-selected="false">SEO</a>
	</li>
</ul>

<div class="tab-content">
	<div class="tab-pane fade show active" id="home" role="tabpanel" aria-labelledby="home-tab">
		<div class="row">
			<div class="col-lg-9">
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
					@Html.LabelFor(Function(model) model.Name, htmlAttributes:=New With {.class = "control-label"})
					@Html.ValidationMessageFor(Function(model) model.Name, "", New With {.class = "text-danger"})
					@Html.EditorFor(Function(model) model.Name, New With {.htmlAttributes = New With {.class = "form-control"}})
				</div>
			</div>
			<div class="col-lg-3">
				<div class="form-group">
					@Html.LabelFor(Function(model) model.ImageId, htmlAttributes:=New With {.class = "control-label"})
					@Html.Editor("ImageId", "ImageFile")
					@Html.ValidationMessageFor(Function(model) model.ImageId, "", New With {.class = "text-danger"})
				</div>

				<div class="form-group">
					@Html.LabelFor(Function(model) model.Order, htmlAttributes:=New With {.class = "control-label"})
					@Html.ValidationMessageFor(Function(model) model.Order, "", New With {.class = "text-danger"})
					@Html.Editor("Order", New With {.htmlAttributes = New With {.class = "form-control text-right"}})
				</div>

				<div class="form-group form-check">
					@Html.CheckBoxFor(Function(model) model.Draft, New With {.class = "form-check-input"})
					@Html.LabelFor(Function(model) model.Draft, New With {.class = "form-check-label"})
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
