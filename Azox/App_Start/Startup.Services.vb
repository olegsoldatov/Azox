Imports Autofac
Imports Autofac.Integration.Mvc
Imports Owin
Imports Soldata.Azox

Partial Public Class Startup
    Public Sub ConfigureServices(app As IAppBuilder)
        Dim builder = New ContainerBuilder()
        builder.RegisterControllers(GetType(MvcApplication).Assembly)

        ' Контексты.
        builder.RegisterType(Of ApplicationDbContext).InstancePerRequest()

        ' Хранилища.
        builder.RegisterType(Of EntityStore(Of Image)).As(Of IEntityStore(Of Image)).InstancePerRequest()
        builder.RegisterType(Of EntityStore(Of Brand)).As(Of IEntityStore(Of Brand)).InstancePerRequest()
        builder.RegisterType(Of EntityStore(Of Category)).As(Of IEntityStore(Of Category)).InstancePerRequest()
        builder.RegisterType(Of EntityStore(Of Product)).As(Of IEntityStore(Of Product)).InstancePerRequest()

        ' Менеджеры.
        builder.RegisterType(Of SettingManager).InstancePerRequest()
        builder.RegisterType(Of PageManager).InstancePerRequest()
        builder.RegisterType(Of ImageManager).InstancePerRequest()
        builder.RegisterType(Of BrandManager).InstancePerRequest()
        builder.RegisterType(Of CategoryManager).InstancePerRequest()
        builder.RegisterType(Of ProductManager(Of Product)).InstancePerRequest()

        ' Сервисы изображений.
        builder.RegisterType(Of BrandImageService).InstancePerRequest()
        builder.RegisterType(Of CategoryImageService).InstancePerRequest()
        builder.RegisterType(Of ProductImageService).InstancePerRequest()

        Dim container = builder.Build()
        DependencyResolver.SetResolver(New AutofacDependencyResolver(container))
        app.UseAutofacMiddleware(container)
        app.UseAutofacMvc()
    End Sub
End Class
