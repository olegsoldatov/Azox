Imports System.Threading.Tasks

Namespace Controllers
	Public Class CartController
		Inherits Controller

		Private ReadOnly db As New ApplicationDbContext

		' GET:/cart
		<OutputCache(Duration:=0, NoStore:=False)>
		Public Function Index() As ActionResult
			If IsNothing(Session("Order")) OrElse Not CType(Session("Order"), Order).Goods.Any Then
				Return View("Empty")
			End If
			Return View(Session("Order"))
		End Function

		' Добавляет товар в корзину.
		' POST:/cart/add
		<HttpPost, ValidateAntiForgeryToken>
		Public Async Function Add(<Bind(Include:="ProductId,Quantity")> model As CartAddViewModel, returnUrl As String) As Task(Of ActionResult)
			Dim product = Await db.Products.FindAsync(model.ProductId)

			' Если товар электронный или доступное количество не имеет значения, то из условия нужно убрать использование поля AvailableQuantity.
			'If Not IsNothing(product) AndAlso (product.Price > Decimal.Zero And product.AvailableQuantity > 0) And model.Quantity > 0 Then
			'	Dim good As New Good With {
			'		.Id = Guid.NewGuid,
			'		.Name = product.Title,
			'		.Sku = product.Sku,
			'		.ProductId = product.Id,
			'		.ProductUrl = Url.Action("details", "products", New With {product.Id}, Request.Url.Scheme),
			'		.ImageUrl = product.ImageUrl,
			'		.Price = product.Price,
			'		.Quantity = model.Quantity
			'	}

			'	If IsNothing(Session("Order")) Then
			'		Session.Add("Order", New Order With {.Goods = New List(Of Good) From {good}})
			'	ElseIf CType(Session("Order"), Order).Goods.Any(Function(x) x.ProductId = good.ProductId) Then
			'		CType(Session("Order"), Order).Goods.Single(Function(x) x.ProductId = good.ProductId).Quantity += good.Quantity
			'	Else
			'		CType(Session("Order"), Order).Goods.Add(good)
			'	End If

			'	TempData("Message") = "Товар добавлен в корзину."
			'End If
			Return RedirectToLocal(returnUrl)
		End Function

		' Обновляет товар в корзине.
		' POST:/cart/update
		<HttpPost, ValidateAntiForgeryToken>
		Public Async Function Update(<Bind(Include:="ProductId,Quantity")> model As CartUpdateViewModel) As Task(Of ActionResult)
			Dim i As Integer = 0
			'For Each item In model.ProductId
			'	Dim product = Await db.Products.FindAsync(item)
			'	' Если товар электронный или доступное количество не имеет значения, то из условия нужно убрать использование поля AvailableQuantity.
			'	If Not IsNothing(product) AndAlso (product.Price > Decimal.Zero And product.AvailableQuantity > 0) And model.Quantity(i) > 0 Then
			'		CType(Session("Order"), Order).Goods.Single(Function(x) x.ProductId = item).Quantity = If(product.AvailableQuantity < model.Quantity(i), product.AvailableQuantity, model.Quantity(i))
			'	Else
			'		CType(Session("Order"), Order).Goods.Remove(CType(Session("Order"), Order).Goods.Single(Function(x) x.ProductId = item))
			'	End If
			'	i += 1
			'Next
			Return RedirectToAction("index")
		End Function

		' Удаляет товар из корзины.
		' GET:/cart/remove/5
		Public Function Remove(id As Guid) As ActionResult
			If Not IsNothing(Session("Order")) AndAlso CType(Session("Order"), Order).Goods.Any(Function(x) x.Id = id) Then
				CType(Session("Order"), Order).Goods.Remove(CType(Session("Order"), Order).Goods.Single(Function(x) x.Id = id))
			End If
			Return RedirectToAction("index")
		End Function

		' Очищает корзину.
		' GET:/cart/clear
		Public Function Clear() As ActionResult
			Session.Remove("Order")
			Return RedirectToAction("index")
		End Function

		' Выбор типа доставки и способа оплаты.
		' GET:/cart/order
		<OutputCache(Duration:=0, NoStore:=False)>
		Public Function Order() As ActionResult
			If IsNothing(Session("Order")) OrElse Not CType(Session("Order"), Order).Goods.Any Then
				Return RedirectToAction("index")
			End If
			Return View(Session("Order"))
		End Function

		' POST:/cart/order
		<HttpPost, ValidateAntiForgeryToken>
		Public Async Function Order(<Bind(Include:="Name,Phone,Email,Zip,Region,District,City,Street,Building,Flat,DeliveryType,PaymentType")> model As Order) As Task(Of ActionResult)
			If ModelState.IsValid Then
				With CType(Session("Order"), Order)
					.Name = model.Name
					.Phone = model.Phone
					.Email = model.Email
					.Zip = model.Zip
					.Region = model.Region
					.District = model.District
					.City = model.City
					.Street = model.Street
					.Building = model.Building
					.Flat = model.Flat
					.DeliveryType = model.DeliveryType
					.PaymentType = model.PaymentType
					'TODO: Здесь нужно реализовать обращение к калькулятору доставки.
					.DeliveryCost = Decimal.Zero
				End With
				Return RedirectToAction("checkout")
			End If
			Return View(model)
		End Function

		' Подтверждение заказа.
		' GET:/cart/checkout
		<OutputCache(Duration:=0, NoStore:=False)>
		Public Function Checkout() As ActionResult
			If IsNothing(Session("Order")) OrElse Not CType(Session("Order"), Order).Goods.Any Then
				Return RedirectToAction("index")
			End If
			Return View(Session("Order"))
		End Function

		' POST:/cart/checkout
		<HttpPost, ValidateAntiForgeryToken>
		Public Async Function Checkout(<Bind(Include:="PaymentType")> model As Order) As Task(Of ActionResult)
			With CType(Session("Order"), Order)
				.Id = Guid.NewGuid
				.CreateDate = Date.Now.Date
			End With
			db.Orders.Add(CType(Session("Order"), Order))
			'For Each item In CType(Session("Order"), Order).Goods
			'	item.OrderId = CType(Session("Order"), Order).Id

			'	' Если учитывается поле AvailableQuantity, то нужно вычесть заказанное количество товара.
			'	Dim product = Await db.Products.FindAsync(item.ProductId)
			'	If Not IsNothing(product) Then
			'		product.AvailableQuantity -= item.Quantity
			'	End If
			'Next
			db.Goods.AddRange(CType(Session("Order"), Order).Goods)

			Await db.SaveChangesAsync

			'Await (New EmailService).SendOrderToManager(Session("Order"))
			'Await (New EmailService).SendOrderToCustomer(Session("Order"))
			'Await (New SmsService).SendOrderToCustomer(Session("Order"))

			Session.Remove("Order")

			Select Case model.PaymentType
				Case PaymentType.Yandex
					Return RedirectToAction("payment")
				Case Else
					Return RedirectToAction("success")
			End Select
		End Function

		' Страница перехода к оплате.
		' GET:/cart/payment
		Public Function Payment() As ActionResult
			Return View()
		End Function

		' Сообщение об успешном завершении операции.
		' GET:/cart/success
		Public Function Success() As ActionResult
			Return View()
		End Function

		Private Function RedirectToLocal(returnUrl As String) As ActionResult
			If Url.IsLocalUrl(returnUrl) Then
				Return Redirect(returnUrl)
			End If
			Return RedirectToAction("index")
		End Function

		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing Then
				db.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub
	End Class
End Namespace