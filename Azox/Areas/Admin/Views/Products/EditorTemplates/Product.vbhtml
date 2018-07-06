@ModelType Product

<div class="form-group">
	@Html.LabelFor(Function(model) model.Title, htmlAttributes:=New With {.class = "control-label required"})
	@Html.ValidationMessageFor(Function(model) model.Title, "", New With {.class = "text-danger"})
	@Html.EditorFor(Function(model) model.Title, New With {.htmlAttributes = New With {.class = "form-control"}})
</div>

<div class="form-group">
	@Html.LabelFor(Function(model) model.Description, htmlAttributes:=New With {.class = "control-label"})
	@Html.ValidationMessageFor(Function(model) model.Description, "", New With {.class = "text-danger"})
	@Html.EditorFor(Function(model) model.Description, New With {.htmlAttributes = New With {.class = "form-control"}})
</div>

<div class="form-group">
	@Html.LabelFor(Function(model) model.Content, htmlAttributes:=New With {.class = "control-label"})
	@Html.ValidationMessageFor(Function(model) model.Content, "", New With {.class = "text-danger"})
	@Html.EditorFor(Function(model) model.Content, New With {.htmlAttributes = New With {.class = "form-control"}})
</div>

@*<div class="col-md-3">
		<div class="form-group">
			@Html.LabelFor(Function(model) model.Sku, htmlAttributes:=New With {.class = "control-label required"})
			@Html.ValidationMessageFor(Function(model) model.Sku, "", New With {.class = "text-danger"})
			@Html.EditorFor(Function(model) model.Sku, New With {.htmlAttributes = New With {.class = "form-control"}})
		</div>

		<div class="form-group">
			<div class="checkbox">
				<label>
					@Html.EditorFor(Function(model) model.Draft)
					@Html.DisplayNameFor(Function(model) model.Draft)
				</label>
			</div>
		</div>

		@If Not IsNothing(Model) Then
			@<div class="form-group">
				@Html.LabelFor(Function(model) model.ImageId, htmlAttributes:=New With {.class = "control-label"})
				@Html.ValidationMessageFor(Function(model) model.ImageId, "", New With {.class = "text-danger"})
				@If IsNothing(Model.ImageId) Then
					@Html.ActionLink("Добавить", "changeimage", New With {.Id = Model.Id}, New With {.class = "btn btn-default btn-block"})
				Else
					@Html.HiddenFor(Function(model) model.ImageId)
					@<div class="row">
						<div class="col-md-12">
							<div class="thumbnail pull-left">
								<a href="@Url.Action("changeimage", New With {.Id = Model.Id})">
									<img src="@Url.Action("thumbnail", "images", New With {.area = "", .id = Model.ImageId})" alt="Изображение" />
								</a>
								<div class="text-right">
									<a href="@Url.Action("deleteimage", New With {.id = Model.Id})" class="btn btn-link btn-sm" data-toggle="tooltip" title="Удалить"><span class="fa fa-remove"></span></a>
								</div>
							</div>
						</div>
					</div>
				End If
			</div>
		End If
	</div>*@
