'=============================
'Последняя редакция 09.08.2014
'=============================

Imports System.Data.Entity
Imports System.Data.Entity.ModelConfiguration.Conventions

<ObsoleteAttribute>
Public Class AzoxDbContext
    Inherits AzoxDbContext(Of Content, Category, ContentCategory)

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(nameOrConnectionString As String)
        MyBase.New(nameOrConnectionString)
    End Sub
End Class

<ObsoleteAttribute>
Public Class AzoxDbContext(Of TContent As Content)
    Inherits AzoxDbContext(Of TContent, Category, ContentCategory)

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(nameOrConnectionString As String)
        MyBase.New(nameOrConnectionString)
    End Sub
End Class

<ObsoleteAttribute>
Public Class AzoxDbContext(Of TContent As Content(Of TContentCategory), TCategory As Category(Of TContentCategory), TContentCategory As ContentCategory)
    Inherits DbContext

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(nameOrConnectionString As String)
        MyBase.New(nameOrConnectionString)
    End Sub

    Public Overridable Property Contents As IDbSet(Of TContent)
    Public Overridable Property Categories As IDbSet(Of TCategory)

    Protected Overrides Sub OnModelCreating(modelBuilder As DbModelBuilder)
        modelBuilder.Entity(Of ContentCategory) _
            .HasKey(Function(m) New With {m.ContentId, m.CategoryId})

        modelBuilder.Entity(Of Content) _
            .HasMany(Function(m) m.Categories).WithRequired()

        modelBuilder.Entity(Of Category) _
            .HasMany(Function(m) m.Contents).WithRequired()

        MyBase.OnModelCreating(modelBuilder)
    End Sub
End Class
