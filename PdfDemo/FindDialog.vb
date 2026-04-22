Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms

Namespace PdfDemo
    ''' <summary>
    ''' Summary description for FindDialog.
    ''' </summary>
    Public Class FindDialog : Inherits System.Windows.Forms.Form
        Private txtFind As System.Windows.Forms.TextBox
        Private WithEvents btnNext As System.Windows.Forms.Button
        Private cbMatchCase As System.Windows.Forms.CheckBox
        Private cbWholeWord As System.Windows.Forms.CheckBox
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.Container = Nothing

        Public Sub New()
            '
            ' Required for Windows Form Designer support
            '
            InitializeComponent()

            '
            ' TODO: Add any constructor code after InitializeComponent call
            '
        End Sub

        ''' <summary>
        ''' Clean up any resources being used.
        ''' </summary>
        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If Not components Is Nothing Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub

#Region "Windows Form Designer generated code"
        ''' <summary>
        ''' Required method for Designer support - do not modify
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Me.txtFind = New System.Windows.Forms.TextBox
            Me.btnNext = New System.Windows.Forms.Button
            Me.cbMatchCase = New System.Windows.Forms.CheckBox
            Me.cbWholeWord = New System.Windows.Forms.CheckBox
            Me.SuspendLayout()
            ' 
            ' txtFind
            ' 
            Me.txtFind.Location = New System.Drawing.Point(8, 9)
            Me.txtFind.Name = "txtFind"
            Me.txtFind.Size = New System.Drawing.Size(168, 20)
            Me.txtFind.TabIndex = 0
            Me.txtFind.Text = ""
            ' 
            ' btnNext
            ' 
            Me.btnNext.Location = New System.Drawing.Point(188, 7)
            Me.btnNext.Name = "btnNext"
            Me.btnNext.Size = New System.Drawing.Size(64, 24)
            Me.btnNext.TabIndex = 1
            Me.btnNext.Text = "Next"
            '			Me.btnNext.Click += New System.EventHandler(Me.btnNext_Click);
            ' 
            ' cbMatchCase
            ' 
            Me.cbMatchCase.Location = New System.Drawing.Point(8, 32)
            Me.cbMatchCase.Name = "cbMatchCase"
            Me.cbMatchCase.Size = New System.Drawing.Size(96, 24)
            Me.cbMatchCase.TabIndex = 2
            Me.cbMatchCase.Text = "Match &Case"
            ' 
            ' cbWholeWord
            ' 
            Me.cbWholeWord.Location = New System.Drawing.Point(104, 32)
            Me.cbWholeWord.Name = "cbWholeWord"
            Me.cbWholeWord.TabIndex = 3
            Me.cbWholeWord.Text = "&Whole Word"
            ' 
            ' FindDialog
            ' 
            Me.AcceptButton = Me.btnNext
            Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
            Me.ClientSize = New System.Drawing.Size(264, 62)
            Me.Controls.Add(Me.cbWholeWord)
            Me.Controls.Add(Me.cbMatchCase)
            Me.Controls.Add(Me.btnNext)
            Me.Controls.Add(Me.txtFind)
            Me.MaximizeBox = False
            Me.MinimizeBox = False
            Me.MinimumSize = New System.Drawing.Size(272, 72)
            Me.Name = "FindDialog"
            Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Find Text"
            Me.TopMost = True
            Me.ResumeLayout(False)

        End Sub
#End Region


        Public Delegate Sub FindNextHandler(ByVal text As String, ByVal matchCase As Boolean, ByVal wholeWord As Boolean)
        Public Event OnFindNext As FindNextHandler

        Private Sub btnNext_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNext.Click
            RaiseEvent OnFindNext(txtFind.Text, cbMatchCase.Checked, cbWholeWord.Checked)
        End Sub
    End Class
End Namespace
