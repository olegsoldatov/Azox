'Imports System.ComponentModel.DataAnnotations
'Imports System.Data.Entity
'Imports System.Threading.Tasks

'''' <summary>
'''' Узел.
'''' </summary>
'Public Class Node
'	Inherits Entity

'	<Display(Name:="Заголовок")>
'	Public Property Heading As String
'	<Display(Name:="Описание")>
'	Public Property Description As String
'	<Display(Name:="Ключевые слова")>
'	Public Property Keywords As String
'	<Display(Name:="URL")>
'	Public Property Url As String
'	<Display(Name:="Порядок")>
'	Public Property Order As Integer = 0
'	<Display(Name:="Родительский узел")>
'	Public Property ParentId As Guid = Guid.Empty
'End Class

'Partial Public Class ApplicationDbContext
'	Public Property Nodes As DbSet(Of Node)
'End Class

'Public Class NodeManager
'	Inherits EntityManager(Of Node)

'	Public Sub New()
'		MyBase.New(New ApplicationDbContext)
'	End Sub

'	Public Sub New(context As ApplicationDbContext)
'		MyBase.New(context)
'	End Sub

'	''' <summary>
'	''' Возвращает узлы верхнего уровня относительно текущего узла.
'	''' </summary>
'	''' <param name="currentNode">Экземпляр класса текущего узла.</param>
'	Public Function GetUpLevelNodes(currentNode As Node) As IEnumerable(Of Node)
'		Dim childNodes = GetChildNodes(currentNode)
'		Return Entities.ToList.Where(Function(m) m.Id <> currentNode.Id And Not childNodes.Contains(m))
'	End Function

'	Public Overrides Function CreateAsync(entity As Node) As Task(Of ManagerResult)
'		If Entities.Any(Function(m) m.ParentId = Guid.Empty) And entity.ParentId = Guid.Empty Then
'			Return Task.FromResult(ManagerResult.Failed("URL '{0}' уже существует."))
'		End If

'		If Not String.IsNullOrEmpty(entity.Url) AndAlso Entities.Any(Function(m) m.Url = entity.Url) Then
'			Return Task.FromResult(ManagerResult.Failed(String.Format("URL '{0}' уже существует.", entity.Url)))
'		End If

'		Return MyBase.CreateAsync(entity)
'	End Function

'	Public Overrides Function UpdateAsync(entity As Node) As Task(Of ManagerResult)
'		If Not String.IsNullOrEmpty(entity.Url) AndAlso Entities.Any(Function(m) m.Url = entity.Url And Not m.Id = entity.Id) Then
'			Return Task.FromResult(ManagerResult.Failed(String.Format("URL '{0}' уже существует.", entity.Url)))
'		End If

'		Return MyBase.UpdateAsync(entity)
'	End Function

'	Public Overrides Function DeleteAsync(entity As Node) As Task(Of ManagerResult)
'		'Проверка на удаление корневого узла.
'		If entity.ParentId = Guid.Empty Then
'			Return Task.FromResult(ManagerResult.Failed("Нельзя удалять корневой узел."))
'		End If

'		'Присвоение потомкам удаляемого узла нового родительского узла, которым является родительский узел удаляемого узла.
'		For Each item In Entities.Where(Function(m) m.ParentId = entity.Id)
'			item.ParentId = entity.ParentId
'			Context.Entry(item).State = EntityState.Modified
'		Next

'		Return MyBase.DeleteAsync(entity)
'	End Function

'	Public Function GetRooNodeAsync() As Task(Of Node)
'		Return Entities.SingleAsync(Function(m) m.ParentId = Guid.Empty)
'	End Function

'	''' <summary>
'	''' Возвращает дочерние узлы текущего узла.
'	''' </summary>
'	''' <param name="currentNode">Экземпляр класса текущего узла.</param>
'	Public Function GetChildNodes(currentNode As Node) As IEnumerable(Of Node)
'		Dim result As New List(Of Node)
'		GetChild(currentNode, Entities.ToList, result)
'		Return result
'	End Function

'	Private Sub GetChild(node As Node, nodes As IEnumerable(Of Node), list As List(Of Node))
'		Dim items = nodes.Where(Function(m) m.ParentId = node.Id)
'		For Each item In items
'			list.Add(item)
'			GetChild(item, nodes, list)
'		Next
'	End Sub

'	''' <summary>
'	''' Возвращает список упорядоченных по подчиненности узлов для визуализации.
'	''' </summary>
'	''' <param name="nodes">Перечисление узлов, которые необходимо упорядочить.</param>
'	''' <returns>Упорядоченное пречисление узлов.</returns>
'	Public Shared Function GetViewList(nodes As IEnumerable(Of Node)) As IEnumerable(Of Node)
'		Dim result As New List(Of Node)

'		If nodes.Any Then
'			'Выделим корневой узел и переименуем его для наглядности.
'			Dim rootNode = nodes.Single(Function(m) m.ParentId = Guid.Empty) : rootNode.Title = "(Нет)"

'			result.Add(rootNode)
'			AddSpace(rootNode, "", nodes, result)
'		End If

'		Return result
'	End Function

'	Private Shared Sub AddSpace(parentNode As Node, space As String, nodes As IEnumerable(Of Node), list As List(Of Node))
'		Dim items = nodes.Where(Function(m) m.ParentId = parentNode.Id).OrderBy(Function(m) m.Order).ThenBy(Function(m) m.Title)
'		For Each item In items
'			item.Title = String.Format("{0}{1}", space, item.Title)
'			list.Add(item)
'			AddSpace(item, space & "   ", nodes, list)
'		Next
'	End Sub
'End Class

'''' <summary>
'''' Предоставляет провайдер карты сайта.
'''' </summary>
'''' <remarks>
'''' Для подключения провайдера необходимо добавить в файл Web.config секцию.
'''' <code>
'''' <system.web>
''''		<siteMap defaultProvider="ApplicationSiteMapProvider">
''''			<providers>
''''				<add name = "ApplicationSiteMapProvider" type="Azox.ApplicationSiteMapProvider" providerName="System.Data.SqlClient" />
''''			</providers>
''''		</siteMap>
''''	</system.web>
'''' </code>
'''' </remarks>
'Public Class ApplicationSiteMapProvider
'	Inherits StaticSiteMapProvider

'	Public Overrides Function BuildSiteMap() As SiteMapNode
'		Dim rootNode As SiteMapNode
'		Using manager As New NodeManager
'			Clear()
'			Dim root = manager.Entities.SingleOrDefault(Function(m) m.ParentId = Guid.Empty)
'			If IsNothing(root) Then
'				root = New Node With {.Id = Guid.NewGuid, .Url = "~/", .Title = "Главная", .ParentId = Guid.Empty}
'				manager.CreateAsync(root)
'			End If
'			Dim attrs As New NameValueCollection : attrs.Set("Keywords", root.Keywords) : attrs.Set("Heading", root.Heading)
'			rootNode = New SiteMapNode(Me, root.Id.ToString, If(Not String.IsNullOrEmpty(root.Url) AndAlso VirtualPathUtility.IsAppRelative(root.Url), VirtualPathUtility.ToAbsolute(root.Url), root.Url), root.Title, root.Description, Nothing, attrs, Nothing, String.Empty)
'			AddNode(rootNode)
'			AddChildren(rootNode, root.Id, manager.Entities.OrderBy(Function(m) m.Order).ToList)
'		End Using
'		Return rootNode
'	End Function

'	Protected Overrides Function GetRootNodeCore() As SiteMapNode
'		Return BuildSiteMap()
'	End Function

'	Public Overrides ReadOnly Property RootNode As SiteMapNode
'		Get
'			Return BuildSiteMap()
'		End Get
'	End Property

'	Private Sub AddChildren(rootNode As SiteMapNode, rootId As Guid, nodes As IEnumerable(Of Node))
'		For Each item In nodes.Where(Function(m) m.ParentId = rootId)
'			Dim attrs As New NameValueCollection : attrs.Set("Keywords", item.Keywords) : attrs.Set("Heading", item.Heading)
'			Dim childNode = New SiteMapNode(Me, item.Id.ToString, If(Not String.IsNullOrEmpty(item.Url) AndAlso VirtualPathUtility.IsAppRelative(item.Url), VirtualPathUtility.ToAbsolute(item.Url), item.Url), item.Title, item.Description, Nothing, attrs, Nothing, String.Empty)
'			AddNode(childNode, rootNode)
'			AddChildren(childNode, item.Id, nodes)
'		Next
'	End Sub
'End Class
