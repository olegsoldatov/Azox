@Imports System.Globalization
@ModelType Decimal
@If Model.Equals(Decimal.Zero) Then
	@<span itemprop="price" content="0">&mdash;</span>
	@<meta itemprop="priceCurrency" content="RUR" />
Else
	@<text>
		<span itemprop="price" content="@Model.ToString("N")">@Model.ToString("N", New CultureInfo(CultureInfo.CurrentCulture.Name) With {.NumberFormat = New NumberFormatInfo With {.NumberDecimalSeparator = ",", .NumberGroupSeparator = " ", .NumberDecimalDigits = 2}})</span>&nbsp;<span itemprop="priceCurrency" content="RUR">@RegionInfo.CurrentRegion.CurrencySymbol</span>
	</text>
End If
