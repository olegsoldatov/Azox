Imports Owin
Imports Microsoft.Owin

<Assembly: OwinStartup(GetType(Startup))>

Partial Public Class Startup
    Public Sub Configuration(app As IAppBuilder)
        ConfigureAuth(app)
    End Sub
End Class
