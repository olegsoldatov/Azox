Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class dgfdfghfh
        Inherits DbMigration
    
        Public Overrides Sub Up()
            DropColumn("dbo.Warehouses", "LastUpdateDate")
            DropColumn("dbo.Images", "LastUpdateDate")
            DropColumn("dbo.Settings", "LastUpdateDate")
        End Sub
        
        Public Overrides Sub Down()
            AddColumn("dbo.Settings", "LastUpdateDate", Function(c) c.DateTime(nullable := False))
            AddColumn("dbo.Images", "LastUpdateDate", Function(c) c.DateTime(nullable := False))
            AddColumn("dbo.Warehouses", "LastUpdateDate", Function(c) c.DateTime(nullable := False))
        End Sub
    End Class
End Namespace
