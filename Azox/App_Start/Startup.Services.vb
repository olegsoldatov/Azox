﻿Imports Autofac
Imports Autofac.Integration.Mvc
Imports Owin
Imports Soldata.Azox

Partial Public Class Startup
    Public Sub ConfigureServices(app As IAppBuilder)
        Dim builder = New ContainerBuilder()
        builder.RegisterControllers(GetType(MvcApplication).Assembly)

        ' TODO: Клиенты.

        ' Контексты данных.
        builder.RegisterType(Of ApplicationDbContext).InstancePerRequest()

        ' Хранилища данных.
        builder.RegisterType(Of EntityStore(Of Page)).As(Of IEntityStore(Of Page)).InstancePerRequest()
        builder.RegisterType(Of EntityStore(Of Image)).As(Of IEntityStore(Of Image)).InstancePerRequest()
        builder.RegisterType(Of EntityStore(Of Brand)).As(Of IEntityStore(Of Brand)).InstancePerRequest()
        builder.RegisterType(Of EntityStore(Of Category)).As(Of IEntityStore(Of Category)).InstancePerRequest()
        builder.RegisterType(Of EntityStore(Of Product)).As(Of IEntityStore(Of Product)).InstancePerRequest()
        builder.RegisterType(Of EntityStore(Of Warehouse)).As(Of IEntityStore(Of Warehouse)).InstancePerRequest()
        builder.RegisterType(Of EntityStore(Of Customer)).As(Of IEntityStore(Of Customer)).InstancePerRequest()

        ' Сервисы.
        builder.RegisterType(Of BrandImageService).InstancePerRequest()
        builder.RegisterType(Of CategoryImageService).InstancePerRequest()
        builder.RegisterType(Of ProductImageService).InstancePerRequest()

        ' Менеджеры.
        builder.RegisterType(Of PageManager).InstancePerRequest()
        builder.RegisterType(Of ImageManager).InstancePerRequest()
        builder.RegisterType(Of BrandManager).InstancePerRequest()
        builder.RegisterType(Of CategoryManager).InstancePerRequest()
        builder.RegisterType(Of ProductManager(Of Product)).InstancePerRequest()
        builder.RegisterType(Of WarehouseManager).InstancePerRequest()
        builder.RegisterType(Of CustomerManager).InstancePerRequest()

        Dim container = builder.Build()
        DependencyResolver.SetResolver(New AutofacDependencyResolver(container))
        app.UseAutofacMiddleware(container)
        app.UseAutofacMvc()
    End Sub
End Class
