@ModelType Decimal

@If Model.Equals(Decimal.Zero) Then
	@<span itemprop="price" content="0">&mdash;</span>
	@<meta itemprop="priceCurrency" content="RUR" />
Else
	@<text>
		<span itemprop="price" content="@Model.ToString("N")">@Model.ToString("## ### ### ###")</span>&nbsp;<span itemprop="priceCurrency" content="RUR">руб.</span>
	</text>
End If
