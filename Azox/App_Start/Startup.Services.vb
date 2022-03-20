Imports System.Data.Entity
Imports Autofac
Imports Autofac.Integration.Mvc
Imports Owin
Imports Soldata.Azox

Partial Public Class Startup
    Public Sub ConfigureServices(app As IAppBuilder)
        Dim builder = New ContainerBuilder()
        builder.RegisterControllers(GetType(MvcApplication).Assembly)

        ' Контексты.
        builder.RegisterType(Of ApplicationDbContext).As(Of DbContext).InstancePerRequest()

        ' Менеджеры.
        builder.RegisterType(Of PageManager).InstancePerRequest()

        ' Служба доставки.
        builder.RegisterType(Of ApplicationDeliveryService).As(Of IDeliveryService)()

        Dim container = builder.Build()
        DependencyResolver.SetResolver(New AutofacDependencyResolver(container))
        app.UseAutofacMiddleware(container)
        app.UseAutofacMvc()
    End Sub
End Class
