Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema
Imports System.Data.Entity

Public Class Order
	<Key>
	Public Property Id As Guid

	<DatabaseGenerated(DatabaseGeneratedOption.Identity), Display(Name:="№ заказа")>
	Public Property Number As Integer

	<DataType(DataType.Date), Display(Name:="Дата")>
	Public Property CreateDate As Date

	<Required(ErrorMessage:="Укажите имя покупателя."), Display(Name:="Покупатель")>
	Public Property Name As String

	<Required(ErrorMessage:="Укажите телефон."), Phone, Display(Name:="Телефон")>
	Public Property Phone As String

	<EmailAddress, Display(Name:="Электронная почта")>
	Public Property Email As String

	<DataType(DataType.MultilineText), MaxLength(1000), Display(Name:="Комментарий")>
	Public Property Comment As String

	<Required(ErrorMessage:="Укажите почтовый индекс."), StringLength(6, ErrorMessage:="Не более {1} знаков."), Display(Name:="Индекс")>
	Public Property Zip As String

	<Required(ErrorMessage:="Укажите субъект федерации."), Display(Name:="Регион")>
	Public Property Region As String

	<Display(Name:="Район")>
	Public Property District As String

	<Required(ErrorMessage:="Укажите населенный пункт."), Display(Name:="Город")>
	Public Property City As String

	<Required(ErrorMessage:="Укажите улицу."), Display(Name:="Улица")>
	Public Property Street As String

	<Required(ErrorMessage:="Укажите дом."), Display(Name:="Дом")>
	Public Property Building As String

	<Display(Name:="Квартира")>
	Public Property Flat As String

	<Required(ErrorMessage:="Укажите тип доставки."), Display(Name:="Тип доставки")>
	Public Property DeliveryType As DeliveryType

	<DataType(DataType.Currency), Display(Name:="Стоимость доставки")>
	Public Property DeliveryCost As Decimal

	<Display(Name:="Способ оплаты")>
	Public Property PaymentType As PaymentType

	<DataType(DataType.Currency), Display(Name:="Оплачено")>
	Public Property Paid As Decimal

	<Display(Name:="Статус")>
	Public Property Status As OrderStatus

	<Display(Name:="Товары")>
	Public Overridable Property Goods As ICollection(Of Good)
End Class

Public Class Good
	<Key>
	Public Property Id As Guid

	<Required(ErrorMessage:="Укажите название."), Display(Name:="Название")>
	Public Property Name As String

	<Required(ErrorMessage:="Укажите артикул."), Display(Name:="Артикул")>
	Public Property Sku As String

	<Required, HiddenInput(DisplayValue:=False)>
	Public Property ProductId As Guid

	<DataType(DataType.Url), Display(Name:="Ссылка на продукт")>
	Public Property ProductUrl As String

	<DataType(DataType.ImageUrl), Display(Name:="Изображение")>
	Public Property ImageUrl As String

	<Required(ErrorMessage:="Укажите цену."), DataType(DataType.Currency), Display(Name:="Цена")>
	Public Property Price As Decimal

	<Obsolete("Не должно быть этого поля в модели.")>
	<Display(Name:="Доступно")>
	Public Property AvailableQuantity As Integer

	<Required(ErrorMessage:="Укажите количество."), Display(Name:="Кол-во")>
	Public Property Quantity As Integer

	<Display(Name:="Заказ")>
	Public Overridable Property Order As Order

	<Display(Name:="Заказ")>
	Public Overridable Property OrderId As Guid
End Class

Partial Public Class ApplicationDbContext
	Public Property Orders As DbSet(Of Order)
	Public Property Goods As DbSet(Of Good)
End Class

Public Class CartAddViewModel
	Public Property ProductId As Guid
	Public Property Quantity As Integer
End Class

Public Class CartUpdateViewModel
	Public Property ProductId As Guid()
	Public Property Quantity As Integer()
End Class

Public Class CheckoutViewModel
	<Display(Name:="Способ оплаты")>
	Public Property PaymentType As PaymentType
End Class

Public Enum OrderStatus
	<Display(Name:="Новый")>
	[New] = 0

	<Display(Name:="В обработке")>
	Processing = 1

	<Display(Name:="Доставляется")>
	Delivering = 2

	<Display(Name:="Закрыт")>
	Closed = 3

	<Display(Name:="Отменен")>
	Canceled = 4
End Enum

Public Enum DeliveryType
	<Display(Name:="До пункта выдачи заказов")>
	ToShop = 0
	<Display(Name:="Курьером до двери")>
	ToAddress = 1
End Enum

Public Enum PaymentType
	<Display(Name:="Курьеру при доставке")>
	[Default] = 0

	<Display(Name:="Электронным платежом через Яндекс.Кассу")>
	Yandex = 1
End Enum

Public Class OrderFilterViewModel
	<Display(Name:="Поиск")>
	Public Property SearchString As String
End Class
