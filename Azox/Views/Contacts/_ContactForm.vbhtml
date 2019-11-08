@ModelType ContactFormViewModel

@Using Ajax.BeginForm("ContactForm", "contacts", routeValues:=Nothing, ajaxOptions:=New AjaxOptions With {.OnBegin = "$('#contact-form-button').button('loading');", .OnSuccess = "$('#contact-form-success-modal').modal('show');", .OnFailure = "$('#contact-form-failure-modal').modal('show');", .OnComplete = "$('#contact-form-button').button('reset'); $('form').trigger('reset');"}, htmlAttributes:=New With {.class = "contact-form"})
	@Html.AntiForgeryToken
	@<div class="row">
		<div class="col-md-6">
			<div class="form-group">
				@Html.LabelFor(Function(model) model.Name, htmlAttributes:=New With {.class = "control-label required"})
				@Html.ValidationMessageFor(Function(model) model.Name, "", New With {.class = "text-danger"})
				@Html.EditorFor(Function(model) model.Name, New With {.htmlAttributes = New With {.class = "form-control"}})
			</div>
		</div>
		<div class="col-md-6">
			<div class="form-group">
				@Html.LabelFor(Function(model) model.Email, htmlAttributes:=New With {.class = "control-label required"})
				@Html.ValidationMessageFor(Function(model) model.Email, "", New With {.class = "text-danger"})
				@Html.EditorFor(Function(model) model.Email, New With {.htmlAttributes = New With {.class = "form-control"}})
			</div>
		</div>
	</div>
	@<div class="form-group">
		@Html.LabelFor(Function(model) model.Subject, htmlAttributes:=New With {.class = "control-label required"})
		@Html.ValidationMessageFor(Function(model) model.Subject, "", New With {.class = "text-danger"})
		@Html.EditorFor(Function(model) model.Subject, New With {.htmlAttributes = New With {.class = "form-control"}})
	</div>
	@<div class="form-group">
		@Html.LabelFor(Function(model) model.Message, htmlAttributes:=New With {.class = "control-label required"})
		@Html.ValidationMessageFor(Function(model) model.Message, "", New With {.class = "text-danger"})
		@Html.EditorFor(Function(model) model.Message, New With {.htmlAttributes = New With {.class = "form-control"}})
	</div>
	@<div class="form-group">
		<button class="btn btn-primary" id="contact-form-button" data-loading-text="Подождите...">Отправить</button>
	</div>
End Using

@*Модальное окно после успешной отправки.*@
<div class="modal fade" id="contact-form-success-modal" tabindex="-1" role="dialog">
	<div class="modal-dialog" role="document">
		<div class="modal-content">
			<div class="modal-header">
				<button type="button" class="close" data-dismiss="modal" aria-label="Закрыть"><span aria-hidden="true">&times;</span></button>
				<h4 class="modal-title">Форма связи</h4>
			</div>
			<div class="modal-body">
				<div class="alert alert-success" role="alert">Ваше сообщение было успешно отправлено. Спасибо!</div>
			</div>
			<div class="modal-footer">
				<button type="button" class="btn btn-secondary" data-dismiss="modal">Закрыть</button>
			</div>
		</div>
	</div>
</div>

@*Модальное окно после ошибки.*@
<div class="modal fade" id="contact-form-failure-modal" tabindex="-1" role="dialog">
	<div class="modal-dialog" role="document">
		<div class="modal-content">
			<div class="modal-header">
				<button type="button" class="close" data-dismiss="modal" aria-label="Закрыть"><span aria-hidden="true">&times;</span></button>
				<h4 class="modal-title">Форма связи</h4>
			</div>
			<div class="modal-body">
				<div class="alert alert-danger" role="alert">Произошла ошибка при отправке формы.</div>
			</div>
			<div class="modal-footer">
				<button type="button" class="btn btn-secondary" data-dismiss="modal">Закрыть</button>
			</div>
		</div>
	</div>
</div>