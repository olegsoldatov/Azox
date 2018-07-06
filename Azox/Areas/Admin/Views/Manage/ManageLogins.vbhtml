@ModelType ManageLoginsViewModel
@Imports Microsoft.Owin.Security
@Imports Microsoft.AspNet.Identity
@Code
    ViewBag.Title = "Управление внешними именами входа"
End Code

<h2>@ViewBag.Title.</h2>

<p class="text-success">@ViewBag.StatusMessage</p>
@If Model.CurrentLogins.Count > 0  Then
    @<h4>Зарегистрированные имена входа</h4>
    @<table class="table">
        <tbody>
            @For Each account As UserLoginInfo In Model.CurrentLogins
                @<tr>
                    <td>@account.LoginProvider</td>
                    <td>
                        @If ViewBag.ShowRemoveButton
                            @Using Html.BeginForm("RemoveLogin", "Manage")
                                @Html.AntiForgeryToken()
                                @<div>
                                    @Html.Hidden("loginProvider", account.LoginProvider)
                                    @Html.Hidden("providerKey", account.ProviderKey)
                                    <input type="submit" class="btn btn-default" value="Удалить" title="Удалить это имя входа @account.LoginProvider из вашей учетной записи" />
                                </div>
                            End Using
                        Else
                            @: &nbsp;
                        End If
                    </td>
                </tr>
            Next
        </tbody>
    </table>
End If
@If Model.OtherLogins.Count > 0
    @<text>
    <h4>Добавьте другую службу для входа.</h4>
    <hr />
    </text>
    @Using Html.BeginForm("LinkLogin", "Manage")
        @Html.AntiForgeryToken()
        @<div id="socialLoginList">
        <p>
            @For Each p As AuthenticationDescription In Model.OtherLogins
                @<button type="submit" class="btn btn-default" id="@p.AuthenticationType" name="provider" value="@p.AuthenticationType" title="Войти с помощью учетной записи @p.Caption">@p.AuthenticationType</button>
            Next
        </p>
        </div>
    End Using
End If

