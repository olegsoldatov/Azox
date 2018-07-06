@ModelType ExternalLoginListViewModel
@Imports Microsoft.Owin.Security
@Code
    Dim loginProviders = Context.GetOwinContext().Authentication.GetExternalAuthenticationTypes()
End Code
<h4>Используйте для входа другую службу.</h4>
<hr />
@If loginProviders.Count() = 0 Then
    @<div>
          <p>
              Внешние службы проверки подлинности не настроены. Подробности о настройке входа через внешние службы для этого приложения ASP.NET
              см. в <a href="https://go.microsoft.com/fwlink/?LinkId=403804">этой статье</a>.
          </p>
    </div>
Else
    @Using Html.BeginForm("ExternalLogin", "Account", New With {.ReturnUrl = Model.ReturnUrl}, FormMethod.Post, New With {.class = "form-horizontal", .role = "form"})
        @Html.AntiForgeryToken()
        @<div id="socialLoginList">
           <p>
               @For Each p As AuthenticationDescription In loginProviders
                   @<button type="submit" class="btn btn-default" id="@p.AuthenticationType" name="provider" value="@p.AuthenticationType" title="Войти с помощью учетной записи @p.Caption">@p.AuthenticationType</button>
               Next
           </p>
        </div>
    End Using
End If
