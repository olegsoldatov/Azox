Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class fghfghfghgg
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AddColumn("dbo.Pages", "Name", Function(c) c.String())
            AddColumn("dbo.Pages", "Title", Function(c) c.String())
            AddColumn("dbo.Pages", "Description", Function(c) c.String())
        End Sub
        
        Public Overrides Sub Down()
            DropColumn("dbo.Pages", "Description")
            DropColumn("dbo.Pages", "Title")
            DropColumn("dbo.Pages", "Name")
        End Sub
    End Class
End Namespace
