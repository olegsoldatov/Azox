@ModelType Guid
@If Not Model.Equals(Guid.Empty) Then
    @<img class="img-responsive" alt="@Html.DisplayNameForModel()" src="~/images/small/@Model" itemprop="image" />
End If

