Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports System.Collections

Namespace WindowsApplication1
	Public Class MyDataSourceWrapper
		Implements ITypedList, IList

		Public ReadOnly NestedList As IList
		Public ReadOnly Property NestedTypedList() As ITypedList
			Get
				Return CType(NestedList, ITypedList)
			End Get
		End Property
		Private descriptor As MyPropertyDescriptor


		Public Sub New(ByVal dataSource As ITypedList, ByVal unboundColumnFieldName As String, ByVal getValueHandler As GetValueEventHanlder, ByVal setValueHanlder As SetValueEventHanlder)
			Me.NestedList = CType(dataSource, IList)
			descriptor = New MyPropertyDescriptor(unboundColumnFieldName)
			AddHandler descriptor.GetUnboundValue, getValueHandler
			AddHandler descriptor.SetUnboundValue, setValueHanlder
		End Sub

		#Region "ITypedList Members"

		Private Function GetItemProperties(ByVal listAccessors() As PropertyDescriptor) As PropertyDescriptorCollection Implements ITypedList.GetItemProperties
			Dim propertiesList As PropertyDescriptorCollection = NestedTypedList.GetItemProperties(listAccessors)
			propertiesList.Add(descriptor)
			Return propertiesList
		End Function

		Private Function GetListName(ByVal listAccessors() As PropertyDescriptor) As String Implements ITypedList.GetListName
			Return NestedTypedList.GetListName(listAccessors)
		End Function

		#End Region

		#Region "IList Members"

		Private Function Add(ByVal value As Object) As Integer Implements IList.Add
		   Return NestedList.Add(value)
		End Function

		Private Sub Clear() Implements IList.Clear
			NestedList.Clear()
		End Sub

		Private Function Contains(ByVal value As Object) As Boolean Implements IList.Contains
			Return NestedList.Contains(value)
		End Function

		Private Function IndexOf(ByVal value As Object) As Integer Implements IList.IndexOf
			Return NestedList.IndexOf(value)
		End Function

		Private Sub Insert(ByVal index As Integer, ByVal value As Object) Implements IList.Insert
			NestedList.Insert(index, value)
		End Sub

		Private ReadOnly Property IsFixedSize() As Boolean Implements IList.IsFixedSize
			Get
				Return NestedList.IsFixedSize
			End Get
		End Property

		Private ReadOnly Property IsReadOnly() As Boolean Implements IList.IsReadOnly
			Get
				Return NestedList.IsReadOnly
			End Get
		End Property

		Private Sub Remove(ByVal value As Object) Implements IList.Remove
			NestedList.Remove(value)
		End Sub

		Private Sub RemoveAt(ByVal index As Integer) Implements IList.RemoveAt
			NestedList.RemoveAt(index)
		End Sub

		Public Property IList_Item(ByVal index As Integer) As Object Implements IList.Item
			Get
				Return NestedList(index)
			End Get
			Set(ByVal value As Object)
				NestedList(index)= value
			End Set
		End Property

		#End Region

		#Region "ICollection Members"

		Private Sub CopyTo(ByVal array As Array, ByVal index As Integer) Implements ICollection.CopyTo
			NestedList.CopyTo(array, index)
		End Sub

		Private ReadOnly Property Count() As Integer Implements ICollection.Count
			Get
				Return NestedList.Count
			End Get
		End Property

		Private ReadOnly Property IsSynchronized() As Boolean Implements ICollection.IsSynchronized
			Get
				Return NestedList.IsSynchronized
			End Get
		End Property

		Private ReadOnly Property SyncRoot() As Object Implements ICollection.SyncRoot
			Get
				Return NestedList.SyncRoot
			End Get
		End Property

		#End Region

		#Region "IEnumerable Members"

		Private Function GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
			Return NestedList.GetEnumerator()
		End Function

		#End Region
	End Class

	Public Class MyPropertyDescriptor
		Inherits PropertyDescriptor
		Protected Sub New(ByVal name As String, ByVal attrs() As Attribute)
			MyBase.New(name, attrs)

		End Sub
		Protected Sub New(ByVal descr As MemberDescriptor)
			MyBase.New(descr)

		End Sub
		Protected Sub New(ByVal descr As MemberDescriptor, ByVal attrs() As Attribute)
			MyBase.New(descr, attrs)

		End Sub

		Public Sub New(ByVal name As String)
			MyBase.New(name, Nothing)

		End Sub



		Private Event onGetValue As GetValueEventHanlder

		Public Custom Event GetUnboundValue As GetValueEventHanlder
			AddHandler(ByVal value As GetValueEventHanlder)
				AddHandler onGetValue, value
			End AddHandler
			RemoveHandler(ByVal value As GetValueEventHanlder)
				RemoveHandler onGetValue, value
			End RemoveHandler
			RaiseEvent(ByVal component As Object, <System.Runtime.InteropServices.Out()> ByRef result As Object)
			End RaiseEvent
		End Event





		Private Event onSetValue As SetValueEventHanlder

		Public Custom Event SetUnboundValue As SetValueEventHanlder
			AddHandler(ByVal value As SetValueEventHanlder)
				AddHandler onSetValue, value
			End AddHandler
			RemoveHandler(ByVal value As SetValueEventHanlder)
				RemoveHandler onSetValue, value
			End RemoveHandler
			RaiseEvent(ByVal component As Object, ByVal value As Object)
			End RaiseEvent
		End Event


		Public Overrides Function CanResetValue(ByVal component As Object) As Boolean
			Return False
		End Function

		Public Overrides ReadOnly Property ComponentType() As Type
			Get
				Return GetType(Object)
			End Get
		End Property

		Public Overrides Function GetValue(ByVal component As Object) As Object
			Dim result As Object = Nothing
			RaiseEvent onGetValue(component, result)
			Return result
		End Function

		Public Overrides ReadOnly Property IsReadOnly() As Boolean
			Get
				Return False
			End Get
		End Property

		Public Overrides ReadOnly Property PropertyType() As Type
			Get
				Return GetType(Object)
			End Get
		End Property

		Public Overrides Sub ResetValue(ByVal component As Object)

		End Sub

		Public Overrides Sub SetValue(ByVal component As Object, ByVal value As Object)
			RaiseEvent onSetValue(component, value)
		End Sub

		Public Overrides Function ShouldSerializeValue(ByVal component As Object) As Boolean
			Return False
		End Function
	End Class
			Public Delegate Sub GetValueEventHanlder(ByVal component As Object, <System.Runtime.InteropServices.Out()> ByRef result As Object)
		 Public Delegate Sub SetValueEventHanlder(ByVal component As Object, ByVal value As Object)
End Namespace
