<div class="input-group">
	@Html.TextBox("", CType(Model, Decimal).ToString(), ViewData("htmlAttributes"))
	<div class="input-group-append">
		<span class="input-group-text">@System.Globalization.RegionInfo.CurrentRegion.CurrencySymbol</span>
	</div>
</div>
