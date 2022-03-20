Imports Microsoft.Owin
Imports Owin

<Assembly: OwinStartup(GetType(Startup))>

Partial Public Class Startup
	Public Sub Configuration(app As IAppBuilder)
		ConfigureAuth(app)
		ConfigureServices(app)
	End Sub
End Class
