﻿@ModelType IndexViewModel
@Code
    ViewBag.Title = "Управление"
End Code

<h2>@ViewBag.Title.</h2>

<p class="text-success">@ViewBag.StatusMessage</p>
<div>
    <h4>Изменение параметров учетной записи</h4>
    <hr />
    <dl class="dl-horizontal">
        <dt>Пароль:</dt>
        <dd>
            [
            @If Model.HasPassword Then
                @Html.ActionLink("Смена пароля", "ChangePassword")
            Else
                @Html.ActionLink("Создать", "SetPassword")
            End If
            ]
        </dd>
        <dt>Внешние имена входа:</dt>
        <dd>
            @Model.Logins.Count [
            @Html.ActionLink("Управление", "ManageLogins") ]
        </dd>
        @*
            Номера телефонов можно использовать в качестве второго проверочного фактора для системы двухфакторной проверки подлинности.
             
             В <a href="https://go.microsoft.com/fwlink/?LinkId=403804">этой статье</a>
                можно узнать, как настроить для этого приложения ASP.NET двухфакторную проверку подлинности с использованием SMS.
             
             Настроив двухфакторную проверку подлинности, раскомментируйте следующий блок
        *@
        @* 
            <dt>Номер телефона:</dt>
            <dd>
                @(If(Model.PhoneNumber, "None"))
                @If (Model.PhoneNumber <> Nothing) Then
                    @<br />
                    @<text>[&nbsp;&nbsp;@Html.ActionLink("Change", "AddPhoneNumber")&nbsp;&nbsp;]</text>
                    @Using Html.BeginForm("RemovePhoneNumber", "Manage", FormMethod.Post, New With {.class = "form-horizontal", .role = "form"})
                        @Html.AntiForgeryToken
                        @<text>[<input type="submit" value="Удалить" class="btn-link" />]</text>
                    End Using
                Else
                    @<text>[&nbsp;&nbsp;@Html.ActionLink("Add", "AddPhoneNumber") &nbsp;&nbsp;]</text>
                End If
            </dd>
        *@
        <dt>Двухфакторная проверка подлинности:</dt>
        <dd>
            <p>
                Поставщики двухфакторной проверки подлинности не настроены. В <a href="https://go.microsoft.com/fwlink/?LinkId=403804">этой статье</a>
                можно узнать, как настроить двухфакторную проверку подлинности для этого приложения ASP.NET.
            </p>
            @*
                @If Model.TwoFactor Then
                    @Using Html.BeginForm("DisableTwoFactorAuthentication", "Manage", FormMethod.Post, New With { .class = "form-horizontal", .role = "form" })
                      @Html.AntiForgeryToken()
                      @<text>
                      Включено
                      <input type="submit" value="Отключить" class="btn btn-link" />
                      </text>
                    End Using
                Else
                    @Using Html.BeginForm("EnableTwoFactorAuthentication", "Manage", FormMethod.Post, New With { .class = "form-horizontal", .role = "form" })
                      @Html.AntiForgeryToken()
                      @<text>
                      Отключено
                      <input type="submit" value="Включить" class="btn btn-link" />
                      </text>
                    End Using
                End If
	     *@
        </dd>
    </dl>
</div>
