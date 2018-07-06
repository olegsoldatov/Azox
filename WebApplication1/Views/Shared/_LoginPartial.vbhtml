@Imports Microsoft.AspNet.Identity

@If Request.IsAuthenticated
    @Using Html.BeginForm("LogOff", "Account", FormMethod.Post, New With { .id = "logoutForm", .class = "navbar-right" })
        @Html.AntiForgeryToken()
        @<ul class="nav navbar-nav navbar-right">
            <li>
                @Html.ActionLink("Здравствуйте, " + User.Identity.GetUserName() + "!", "Index", "Manage", routeValues := Nothing, htmlAttributes := New With { .title = "Manage" })
            </li>
            <li><a href="javascript:document.getElementById('logoutForm').submit()">Выйти</a></li>
        </ul>
    End Using
Else
    @<ul class="nav navbar-nav navbar-right">
        <li>@Html.ActionLink("Регистрация", "Register", "Account", routeValues := Nothing, htmlAttributes := New With { .id = "registerLink" })</li>
        <li>@Html.ActionLink("Выполнить вход", "Login", "Account", routeValues := Nothing, htmlAttributes := New With { .id = "loginLink" })</li>
    </ul>
End If

