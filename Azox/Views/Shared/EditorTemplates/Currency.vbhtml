@Imports System.Globalization
@ModelType Decimal?
@Code
	Dim value = String.Empty
	If Not IsNothing(Model) Then
		value = Model.Value.ToString("N", New CultureInfo(CultureInfo.CurrentCulture.Name) With {.NumberFormat = New NumberFormatInfo With {.NumberDecimalSeparator = ",", .NumberGroupSeparator = " "}})
	End If
End Code
<div class="input-group">
	@Html.TextBox("", value, ViewData("htmlAttributes"))
	<div class="input-group-append">
		<span class="input-group-text">@RegionInfo.CurrentRegion.CurrencySymbol</span>
	</div>
</div>
