@ModelType Service

<div class="row">
	<div class="col-md-9">
		<div class="form-group">
			@Html.LabelFor(Function(model) model.Title, htmlAttributes:=New With {.class = "control-label required"})
			@Html.EditorFor(Function(model) model.Title, New With {.htmlAttributes = New With {.class = "form-control"}})
			@Html.ValidationMessageFor(Function(model) model.Title, "", New With {.class = "text-danger"})
		</div>

		<div class="form-group">
			@Html.LabelFor(Function(model) model.Heading, htmlAttributes:=New With {.class = "control-label"})
			@Html.EditorFor(Function(model) model.Heading, New With {.htmlAttributes = New With {.class = "form-control"}})
			@Html.ValidationMessageFor(Function(model) model.Heading, "", New With {.class = "text-danger"})
		</div>

		<div class="form-group">
			@Html.LabelFor(Function(model) model.Content, htmlAttributes:=New With {.class = "control-label"})
			@Html.EditorFor(Function(model) model.Content, New With {.htmlAttributes = New With {.class = "form-control"}})
			@Html.ValidationMessageFor(Function(model) model.Content, "", New With {.class = "text-danger"})
		</div>
	</div>
	<div class="col-md-3">
		<div class="form-group">
			@Html.LabelFor(Function(model) model.Image, htmlAttributes:=New With {.class = "control-label"})
			@Html.ValidationMessageFor(Function(m) m.Image, "", New With {.class = "text-danger"})
			<div class="fileupload fileupload-new" data-provides="fileupload">
				<div class="fileupload-new thumbnail">
					@If Not IsNothing(Model) AndAlso Not IsNothing(Model.Image) Then
						@<img src="@Url.Action("Thumbnail", "Images", New With {.id = Model.ImageId, .area = ""})" alt="Изображение" class="img-responsive" />
						@Html.HiddenFor(Function(m) m.ImageId)
					Else
						@<img src="http://placehold.it/200x200" alt="Изображение" class="img-responsive" />
					End If
				</div>
				<div class="fileupload-preview fileupload-exists thumbnail"></div>
				<span class="btn btn-default btn-file"><span class="fileupload-new">Выбрать</span><span class="fileupload-exists">Изменить</span>@Html.TextBox("ImageFile", "", New With {.type = "file", .accept = "image/*"})</span>
				<a href="#" class="btn btn-default fileupload-exists" data-dismiss="fileupload">Убрать</a>
			</div>
		</div>


		<div class="form-group">
			@Html.LabelFor(Function(model) model.Slug, htmlAttributes:=New With {.class = "control-label"})
			@Html.EditorFor(Function(model) model.Slug, New With {.htmlAttributes = New With {.class = "form-control"}})
			@Html.ValidationMessageFor(Function(model) model.Slug, "", New With {.class = "text-danger"})
		</div>

		<div class="form-group">
			@Html.LabelFor(Function(model) model.Order, htmlAttributes:=New With {.class = "control-label"})
			@Html.EditorFor(Function(model) model.Order, New With {.htmlAttributes = New With {.class = "form-control"}})
			@Html.ValidationMessageFor(Function(model) model.Order, "", New With {.class = "text-danger"})
		</div>

		<div class="checkbox">
			<label>
				@Html.EditorFor(Function(m) m.Draft)
				@Html.DisplayNameFor(Function(m) m.Draft)
			</label>
		</div>
	</div>
</div>
