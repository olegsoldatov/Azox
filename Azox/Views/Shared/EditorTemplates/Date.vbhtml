@ModelType Date?
@Code
	Dim dt = Date.Now
	If Not IsNothing(Model) Then
		dt = Model
	End If
End Code
<div class="input-group date">
	@Html.TextBox("", dt.ToString("dd.MM.yyyy", Globalization.CultureInfo.CreateSpecificCulture("ru-RU")), New With {.class = "form-control", .autocomplete = "off"})
	<span class="input-group-addon"><span class="fa fa-calendar"></span></span>
</div>
