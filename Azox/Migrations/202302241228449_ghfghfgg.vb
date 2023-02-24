Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class ghfghfgg
        Inherits DbMigration
    
        Public Overrides Sub Up()
            DropColumn("dbo.Brands", "Content")
            DropColumn("dbo.Brands", "Order")
            DropColumn("dbo.Brands", "Description")
            DropColumn("dbo.Brands", "Keywords")
            DropColumn("dbo.Brands", "LastUpdateDate")
        End Sub
        
        Public Overrides Sub Down()
            AddColumn("dbo.Brands", "LastUpdateDate", Function(c) c.DateTime(nullable := False))
            AddColumn("dbo.Brands", "Keywords", Function(c) c.String())
            AddColumn("dbo.Brands", "Description", Function(c) c.String())
            AddColumn("dbo.Brands", "Order", Function(c) c.Int())
            AddColumn("dbo.Brands", "Content", Function(c) c.String())
        End Sub
    End Class
End Namespace
