Imports System
Imports System.Data.Entity.Migrations
Imports Microsoft.VisualBasic

Namespace Migrations
    Public Partial Class fghfgjfj
        Inherits DbMigration
    
        Public Overrides Sub Up()
            AlterColumn("dbo.Pages", "Slug", Function(c) c.String(nullable := False, maxLength := 128))
            DropColumn("dbo.Pages", "Seo_Title")
            DropColumn("dbo.Pages", "Seo_Description")
            DropColumn("dbo.Pages", "Seo_Keywords")
        End Sub
        
        Public Overrides Sub Down()
            AddColumn("dbo.Pages", "Seo_Keywords", Function(c) c.String(maxLength := 250))
            AddColumn("dbo.Pages", "Seo_Description", Function(c) c.String(maxLength := 250))
            AddColumn("dbo.Pages", "Seo_Title", Function(c) c.String(maxLength := 250))
            AlterColumn("dbo.Pages", "Slug", Function(c) c.String(maxLength := 128))
        End Sub
    End Class
End Namespace
