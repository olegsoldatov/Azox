@Imports System.Web.Configuration
@ModelType Guid?
@Html.Hidden("", Model)
@If Not IsNothing(Model) Then
	@<div class="p-1 mb-3 border text-center">
		<img src="@Url.Action("thumbnail", "images", New With {.area = "", .id = Model})" class="img-fluid" alt="Миниатюра" />
	</div>
End If
<input type="file" name="ImageFile" accept="image/*" />
<small class="form-text text-muted">@String.Format("Размер файла не более {0} МБ.", CType(WebConfigurationManager.GetSection("system.web/httpRuntime"), HttpRuntimeSection).MaxRequestLength / 1024)</small>

