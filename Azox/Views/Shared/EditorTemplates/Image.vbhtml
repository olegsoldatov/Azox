@ModelType Guid?
@If IsNothing(Model) Then
	@Html.ActionLink("Добавить", "ChangeImage", New With {.Id = ViewBag.Id}, New With {.class = "btn btn-default btn-block"})
Else
	@Html.Hidden("", Model)
	@<div class="row">
		<div class="col-md-12">
			<div class="thumbnail pull-left">
				<a href="@Url.Action("ChangeImage", New With {.Id = ViewBag.Id})">
					<img src="@Url.Action("Thumbnail", "Images", New With {.area = "", .id = Model})" alt="Изображение" />
				</a>
				<div class="text-right">
					<a href="@Url.Action("DeleteImage", New With {.id = ViewBag.Id})" class="btn btn-link btn-sm" title="Удалить">
						<span class="fa fa-remove"></span>
					</a>
				</div>
			</div>
		</div>
	</div>
End If

