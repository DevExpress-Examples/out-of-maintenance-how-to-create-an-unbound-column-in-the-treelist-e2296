Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports System.Collections
Imports DevExpress.XtraTreeList

Namespace WindowsApplication1
	Public Class MyTreeList
		Inherits TreeList

		Public Sub New()

		End Sub

		Protected Overrides Overloads Function GetColumnError(ByVal column As DevExpress.XtraTreeList.Columns.TreeListColumn, ByVal node As DevExpress.XtraTreeList.Nodes.TreeListNode) As String
		  If column Is Nothing OrElse column.FieldName = "Unbound" Then ' your condition
			  Return String.Empty
		  End If
			Return MyBase.GetColumnError(column, node)
		End Function

	End Class
End Namespace
