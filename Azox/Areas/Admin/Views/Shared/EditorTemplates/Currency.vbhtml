<div class="input-group">
	@Html.TextBox("", CType(Model, Decimal).ToString("0"), New With {.class = "form-control form-control-currency"})
	<div class="input-group-addon">@System.Globalization.RegionInfo.CurrentRegion.CurrencySymbol</div>
</div>

