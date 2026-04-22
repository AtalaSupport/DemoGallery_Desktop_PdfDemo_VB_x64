Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Printing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports System.IO
Imports Atalasoft.Imaging
Imports Atalasoft.Imaging.Codec
Imports Atalasoft.Imaging.Codec.Pdf
Imports Atalasoft.Imaging.WinControls
Imports Atalasoft.Annotate.UI
Imports Atalasoft.Annotate
Imports Atalasoft.PdfDoc

Namespace PdfDemo
    ''' <summary>
    ''' Summary description for Form1.
    ''' </summary>
    Public Class Form1 : Inherits System.Windows.Forms.Form
        Private WithEvents workspaceViewer1 As Atalasoft.Annotate.UI.AnnotateViewer
        Private mainMenu1 As System.Windows.Forms.MainMenu
        Private openFileDialog1 As System.Windows.Forms.OpenFileDialog
        Private saveFileDialog1 As System.Windows.Forms.SaveFileDialog
        Private statusBar1 As System.Windows.Forms.StatusBar
        Private menuFile As System.Windows.Forms.MenuItem
        Private WithEvents menuOpen As System.Windows.Forms.MenuItem
        Private WithEvents menuGetInfo As System.Windows.Forms.MenuItem
        Private WithEvents menuExtractImages As System.Windows.Forms.MenuItem
        Private WithEvents thumbnailView1 As Atalasoft.Imaging.WinControls.ThumbnailView
        Private statusBarPanel1 As System.Windows.Forms.StatusBarPanel
        Private progressBar1 As System.Windows.Forms.ProgressBar
        Private _pdf As PdfDecoder
        Private _extractedImages As Boolean = False
        Private WithEvents menuView As System.Windows.Forms.MenuItem
        Private WithEvents menuViewFullSize As System.Windows.Forms.MenuItem
        Private WithEvents menuViewFitWidth As System.Windows.Forms.MenuItem
        Private WithEvents menuViewBestFit As System.Windows.Forms.MenuItem
        Private menuItem2 As System.Windows.Forms.MenuItem
        Private menuItem3 As System.Windows.Forms.MenuItem
        Private menuItem4 As System.Windows.Forms.MenuItem
        Private menuItem5 As System.Windows.Forms.MenuItem
        Private WithEvents menuSave As System.Windows.Forms.MenuItem
        Private menuItem1 As System.Windows.Forms.MenuItem
        Private WithEvents menuItem6 As System.Windows.Forms.MenuItem
        Private WithEvents menuPrint As System.Windows.Forms.MenuItem
        Private menuItem7 As System.Windows.Forms.MenuItem
        Private WithEvents menuItemFind As System.Windows.Forms.MenuItem
        Private statusProgress As System.Windows.Forms.StatusBarPanel
        Private toolTip1 As System.Windows.Forms.ToolTip
        Private components As System.ComponentModel.IContainer

        Private _currentFile As String = Nothing
        Private _currentResolution As Integer = -1

        ' members to control text search
        Private _findDialog As FindDialog
        Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
        Friend WithEvents tabPages As System.Windows.Forms.TabPage
        Friend WithEvents tabBookmarks As System.Windows.Forms.TabPage
        Friend WithEvents treeBookmarks As System.Windows.Forms.TreeView
        Friend WithEvents Splitter1 As System.Windows.Forms.Splitter
        Private _pdfDocSearch As PdfDocumentSearch


        Public Sub New()

            '
            ' Required for Windows Form Designer support
            '
            InitializeComponent()

            Try
                _pdf = New PdfDecoder
                _pdf.Resolution = 150
                _pdf.RenderSettings = New RenderSettings() With {.AnnotationSettings = AnnotationRenderSettings.RenderNone}

                'add the PDF Rasterizer as a decoder
                RegisteredDecoders.Decoders.Insert(0, _pdf)
            Catch e1 As Atalasoft.Imaging.AtalasoftLicenseException
                Me.menuFile.Enabled = False
                ShowLicenseMessage("PdfRasterizer")
            End Try

            Me.workspaceViewer1.ImageBorderPen = New Atalasoft.Imaging.Drawing.AtalaPen(Color.Black, 1)
            workspaceViewer1.Annotations.Layers.Add(New LayerAnnotation)
        End Sub

        Private Sub ShowLicenseMessage(ByVal product As String)
            If MessageBox.Show("This demo requires a " & product & " license." & Constants.vbCrLf & "An evaluation license can be requested with our activation utility." & Constants.vbCrLf & Constants.vbCrLf & "Would you like to run this utility now?", product & " License Required", MessageBoxButtons.YesNo, MessageBoxIcon.Information) = DialogResult.Yes Then
                Dim activationUtil As String = System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles) & "\Atalasoft\DotImage "

                ' Use reflection to find out which version of DotImage we are using.
                Dim assemblies As System.Reflection.AssemblyName() = System.Reflection.Assembly.GetExecutingAssembly().GetReferencedAssemblies()
                For Each name As System.Reflection.AssemblyName In assemblies
                    If name.Name = "Atalasoft.dotImage" Then
                        activationUtil &= name.Version.ToString(2)
                        Exit For
                    End If
                Next name

                activationUtil &= "\AtalasoftToolkitActivation.exe"

                If System.IO.File.Exists(activationUtil) Then
                    System.Diagnostics.Process.Start(activationUtil)
                Else
                    MessageBox.Show("We were unable to location the activation utility on this system." & Constants.vbCrLf & "Please run it from the start menu.", "AtalasoftToolkitActivation.exe Not Found")
                End If
            End If
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
            Me.components = New System.ComponentModel.Container
            Me.workspaceViewer1 = New Atalasoft.Annotate.UI.AnnotateViewer
            Me.mainMenu1 = New System.Windows.Forms.MainMenu(Me.components)
            Me.menuFile = New System.Windows.Forms.MenuItem
            Me.menuOpen = New System.Windows.Forms.MenuItem
            Me.menuSave = New System.Windows.Forms.MenuItem
            Me.menuItemFind = New System.Windows.Forms.MenuItem
            Me.menuExtractImages = New System.Windows.Forms.MenuItem
            Me.menuGetInfo = New System.Windows.Forms.MenuItem
            Me.menuItem7 = New System.Windows.Forms.MenuItem
            Me.menuPrint = New System.Windows.Forms.MenuItem
            Me.menuView = New System.Windows.Forms.MenuItem
            Me.menuViewFullSize = New System.Windows.Forms.MenuItem
            Me.menuViewFitWidth = New System.Windows.Forms.MenuItem
            Me.menuViewBestFit = New System.Windows.Forms.MenuItem
            Me.menuItem1 = New System.Windows.Forms.MenuItem
            Me.menuItem6 = New System.Windows.Forms.MenuItem
            Me.openFileDialog1 = New System.Windows.Forms.OpenFileDialog
            Me.saveFileDialog1 = New System.Windows.Forms.SaveFileDialog
            Me.statusBar1 = New System.Windows.Forms.StatusBar
            Me.statusBarPanel1 = New System.Windows.Forms.StatusBarPanel
            Me.statusProgress = New System.Windows.Forms.StatusBarPanel
            Me.thumbnailView1 = New Atalasoft.Imaging.WinControls.ThumbnailView
            Me.progressBar1 = New System.Windows.Forms.ProgressBar
            Me.menuItem2 = New System.Windows.Forms.MenuItem
            Me.menuItem3 = New System.Windows.Forms.MenuItem
            Me.menuItem4 = New System.Windows.Forms.MenuItem
            Me.menuItem5 = New System.Windows.Forms.MenuItem
            Me.toolTip1 = New System.Windows.Forms.ToolTip(Me.components)
            Me.TabControl1 = New System.Windows.Forms.TabControl
            Me.tabPages = New System.Windows.Forms.TabPage
            Me.tabBookmarks = New System.Windows.Forms.TabPage
            Me.Splitter1 = New System.Windows.Forms.Splitter
            Me.treeBookmarks = New System.Windows.Forms.TreeView
            CType(Me.statusBarPanel1, System.ComponentModel.ISupportInitialize).BeginInit()
            CType(Me.statusProgress, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.TabControl1.SuspendLayout()
            Me.tabPages.SuspendLayout()
            Me.tabBookmarks.SuspendLayout()
            Me.SuspendLayout()
            '
            'workspaceViewer1
            '
            Me.workspaceViewer1.AntialiasDisplay = Atalasoft.Imaging.WinControls.AntialiasDisplayMode.ReductionOnly
            Me.workspaceViewer1.Asynchronous = True
            Me.workspaceViewer1.BackColor = System.Drawing.SystemColors.Control
            Me.workspaceViewer1.Centered = True
            Me.workspaceViewer1.DefaultSecurity = Nothing
            Me.workspaceViewer1.DisplayProfile = Nothing
            Me.workspaceViewer1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.workspaceViewer1.Location = New System.Drawing.Point(157, 0)
            Me.workspaceViewer1.Magnifier.BackColor = System.Drawing.Color.White
            Me.workspaceViewer1.Magnifier.BorderColor = System.Drawing.Color.Black
            Me.workspaceViewer1.Magnifier.Size = New System.Drawing.Size(100, 100)
            Me.workspaceViewer1.Name = "workspaceViewer1"
            Me.workspaceViewer1.OutputProfile = Nothing
            Me.workspaceViewer1.RotationSnapInterval = 0.0!
            Me.workspaceViewer1.RotationSnapThreshold = 0.0!
            Me.workspaceViewer1.Selection = Nothing
            Me.workspaceViewer1.Size = New System.Drawing.Size(803, 611)
            Me.workspaceViewer1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.[Default]
            Me.workspaceViewer1.TabIndex = 0
            Me.workspaceViewer1.Text = "workspaceViewer1"
            Me.workspaceViewer1.ToolTip = Nothing
            Me.workspaceViewer1.UndoManager.Levels = 0
            '
            'mainMenu1
            '
            Me.mainMenu1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuFile, Me.menuView, Me.menuItem1})
            '
            'menuFile
            '
            Me.menuFile.Index = 0
            Me.menuFile.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuOpen, Me.menuSave, Me.menuItemFind, Me.menuExtractImages, Me.menuGetInfo, Me.menuItem7, Me.menuPrint})
            Me.menuFile.Text = "File"
            '
            'menuOpen
            '
            Me.menuOpen.Index = 0
            Me.menuOpen.Shortcut = System.Windows.Forms.Shortcut.CtrlO
            Me.menuOpen.Text = "Open"
            '
            'menuSave
            '
            Me.menuSave.Index = 1
            Me.menuSave.Text = "Save As"
            '
            'menuItemFind
            '
            Me.menuItemFind.Enabled = False
            Me.menuItemFind.Index = 2
            Me.menuItemFind.Shortcut = System.Windows.Forms.Shortcut.CtrlF
            Me.menuItemFind.Text = "Find ..."
            '
            'menuExtractImages
            '
            Me.menuExtractImages.Index = 3
            Me.menuExtractImages.Text = "Extract Images"
            '
            'menuGetInfo
            '
            Me.menuGetInfo.Index = 4
            Me.menuGetInfo.Text = "Get Information"
            '
            'menuItem7
            '
            Me.menuItem7.Index = 5
            Me.menuItem7.Text = "-"
            '
            'menuPrint
            '
            Me.menuPrint.Index = 6
            Me.menuPrint.Text = "Print ... "
            '
            'menuView
            '
            Me.menuView.Index = 1
            Me.menuView.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuViewFullSize, Me.menuViewFitWidth, Me.menuViewBestFit})
            Me.menuView.Text = "View"
            '
            'menuViewFullSize
            '
            Me.menuViewFullSize.Index = 0
            Me.menuViewFullSize.Text = "Full Size"
            '
            'menuViewFitWidth
            '
            Me.menuViewFitWidth.Index = 1
            Me.menuViewFitWidth.Text = "Fit To Width"
            '
            'menuViewBestFit
            '
            Me.menuViewBestFit.Index = 2
            Me.menuViewBestFit.Text = "Best Fit"
            '
            'menuItem1
            '
            Me.menuItem1.Index = 2
            Me.menuItem1.MenuItems.AddRange(New System.Windows.Forms.MenuItem() {Me.menuItem6})
            Me.menuItem1.Text = "Help"
            '
            'menuItem6
            '
            Me.menuItem6.Index = 0
            Me.menuItem6.Text = "About ..."
            '
            'openFileDialog1
            '
            Me.openFileDialog1.DefaultExt = "pdf"
            Me.openFileDialog1.Filter = "PDF Files|*.pdf|All Files|*.*"
            '
            'saveFileDialog1
            '
            Me.saveFileDialog1.DefaultExt = "pdf"
            Me.saveFileDialog1.Filter = "PDF File(*.pdf)|*.pdf|TIFF File(*tif)|*.tif|JPEG File(*.jpg)|*.jpg"
            '
            'statusBar1
            '
            Me.statusBar1.Location = New System.Drawing.Point(0, 611)
            Me.statusBar1.Name = "statusBar1"
            Me.statusBar1.Panels.AddRange(New System.Windows.Forms.StatusBarPanel() {Me.statusBarPanel1, Me.statusProgress})
            Me.statusBar1.ShowPanels = True
            Me.statusBar1.Size = New System.Drawing.Size(960, 22)
            Me.statusBar1.TabIndex = 1
            '
            'statusBarPanel1
            '
            Me.statusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring
            Me.statusBarPanel1.Name = "statusBarPanel1"
            Me.statusBarPanel1.Width = 643
            '
            'statusProgress
            '
            Me.statusProgress.Name = "statusProgress"
            Me.statusProgress.Width = 300
            '
            'thumbnailView1
            '
            Me.thumbnailView1.BackColor = System.Drawing.Color.Gray
            Me.thumbnailView1.CaptionLines = 0
            Me.thumbnailView1.DisplayText = Atalasoft.Imaging.WinControls.ThumbViewAttribute.None
            Me.thumbnailView1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.thumbnailView1.DragSelectionColor = System.Drawing.Color.Red
            Me.thumbnailView1.ForeColor = System.Drawing.SystemColors.WindowText
            Me.thumbnailView1.HighlightBackgroundColor = System.Drawing.SystemColors.Highlight
            Me.thumbnailView1.HighlightTextColor = System.Drawing.SystemColors.HighlightText
            Me.thumbnailView1.LoadErrorMessage = ""
            Me.thumbnailView1.LoadMethod = Atalasoft.Imaging.WinControls.ThumbLoadMethod.EntireFolder
            Me.thumbnailView1.Location = New System.Drawing.Point(3, 3)
            Me.thumbnailView1.Margins = New Atalasoft.Imaging.WinControls.Margin(4, 4, 4, 4)
            Me.thumbnailView1.SelectionMode = ThumbnailSelectionMode.SingleSelect
            Me.thumbnailView1.Name = "thumbnailView1"
            Me.thumbnailView1.SelectionRectangleBackColor = System.Drawing.Color.Transparent
            Me.thumbnailView1.SelectionRectangleDashStyle = System.Drawing.Drawing2D.DashStyle.Solid
            Me.thumbnailView1.SelectionRectangleLineColor = System.Drawing.Color.Black
            Me.thumbnailView1.Size = New System.Drawing.Size(137, 579)
            Me.thumbnailView1.TabIndex = 2
            Me.thumbnailView1.Text = "thumbnailView1"
            Me.thumbnailView1.ThumbnailBackground = Nothing
            Me.thumbnailView1.ThumbnailOffset = New System.Drawing.Point(0, 0)
            Me.thumbnailView1.ThumbnailSize = New System.Drawing.Size(100, 100)
            '
            'progressBar1
            '
            Me.progressBar1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.progressBar1.Location = New System.Drawing.Point(646, 613)
            Me.progressBar1.Name = "progressBar1"
            Me.progressBar1.Size = New System.Drawing.Size(300, 20)
            Me.progressBar1.TabIndex = 3
            '
            'menuItem2
            '
            Me.menuItem2.Index = -1
            Me.menuItem2.Text = "10"
            '
            'menuItem3
            '
            Me.menuItem3.Index = -1
            Me.menuItem3.Text = "75"
            '
            'menuItem4
            '
            Me.menuItem4.Index = -1
            Me.menuItem4.Text = "200"
            '
            'menuItem5
            '
            Me.menuItem5.Index = -1
            Me.menuItem5.Text = "300"
            '
            'TabControl1
            '
            Me.TabControl1.Controls.Add(Me.tabPages)
            Me.TabControl1.Controls.Add(Me.tabBookmarks)
            Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Left
            Me.TabControl1.Location = New System.Drawing.Point(0, 0)
            Me.TabControl1.Name = "TabControl1"
            Me.TabControl1.SelectedIndex = 0
            Me.TabControl1.Size = New System.Drawing.Size(151, 611)
            Me.TabControl1.TabIndex = 4
            '
            'tabPages
            '
            Me.tabPages.Controls.Add(Me.thumbnailView1)
            Me.tabPages.Location = New System.Drawing.Point(4, 22)
            Me.tabPages.Name = "tabPages"
            Me.tabPages.Padding = New System.Windows.Forms.Padding(3)
            Me.tabPages.Size = New System.Drawing.Size(143, 585)
            Me.tabPages.TabIndex = 0
            Me.tabPages.Text = "Pages"
            Me.tabPages.UseVisualStyleBackColor = True
            '
            'tabBookmarks
            '
            Me.tabBookmarks.Controls.Add(Me.treeBookmarks)
            Me.tabBookmarks.Location = New System.Drawing.Point(4, 22)
            Me.tabBookmarks.Name = "tabBookmarks"
            Me.tabBookmarks.Padding = New System.Windows.Forms.Padding(3)
            Me.tabBookmarks.Size = New System.Drawing.Size(143, 585)
            Me.tabBookmarks.TabIndex = 1
            Me.tabBookmarks.Text = "Bookmarks"
            Me.tabBookmarks.UseVisualStyleBackColor = True
            '
            'Splitter1
            '
            Me.Splitter1.Location = New System.Drawing.Point(151, 0)
            Me.Splitter1.Name = "Splitter1"
            Me.Splitter1.Size = New System.Drawing.Size(6, 611)
            Me.Splitter1.TabIndex = 5
            Me.Splitter1.TabStop = False
            '
            'treeBookmarks
            '
            Me.treeBookmarks.BackColor = System.Drawing.Color.Gray
            Me.treeBookmarks.Dock = System.Windows.Forms.DockStyle.Fill
            Me.treeBookmarks.ForeColor = System.Drawing.Color.White
            Me.treeBookmarks.Location = New System.Drawing.Point(3, 3)
            Me.treeBookmarks.Name = "treeBookmarks"
            Me.treeBookmarks.Size = New System.Drawing.Size(137, 579)
            Me.treeBookmarks.TabIndex = 0
            '
            'Form1
            '
            Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
            Me.ClientSize = New System.Drawing.Size(960, 633)
            Me.Controls.Add(Me.workspaceViewer1)
            Me.Controls.Add(Me.Splitter1)
            Me.Controls.Add(Me.TabControl1)
            Me.Controls.Add(Me.progressBar1)
            Me.Controls.Add(Me.statusBar1)
            Me.Menu = Me.mainMenu1
            Me.Name = "Form1"
            Me.Text = "PDF Demo"
            CType(Me.statusBarPanel1, System.ComponentModel.ISupportInitialize).EndInit()
            CType(Me.statusProgress, System.ComponentModel.ISupportInitialize).EndInit()
            Me.TabControl1.ResumeLayout(False)
            Me.tabPages.ResumeLayout(False)
            Me.tabBookmarks.ResumeLayout(False)
            Me.ResumeLayout(False)

        End Sub
#End Region

        ''' <summary>
        ''' The main entry point for the application.
        ''' </summary>
        <STAThread()> _
        Shared Sub Main()
            Application.Run(New Form1)
        End Sub

        Private Sub menuOpen_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuOpen.Click
            If Me.openFileDialog1.ShowDialog(Me) = DialogResult.OK Then
                Dim file As String = Me.openFileDialog1.FileName
                Dim frameCount As Integer = RegisteredDecoders.GetImageInfo(file, 0).FrameCount

                'set decoder properties
                Dim frm As Form = New Parameters("PDF DEcoder Properties", _pdf)
                If frm.ShowDialog(Me) = DialogResult.OK Then
                    Me.thumbnailView1.Items.Cancel()
                    Me.thumbnailView1.Items.Clear()

                    'reset progress bar
                    Me.progressBar1.Maximum = frameCount
                    Me.progressBar1.Value = 0

                    ' Create the Thumbnail objects and pass them into the ThumbnailView all at once.
                    Dim thumbs As Thumbnail() = New Thumbnail(frameCount - 1) {}
                    Dim i As Integer = 0
                    'ORIGINAL LINE: for (int i = 0; i < frameCount; i += 1)
                    'INSTANT VB NOTE: This 'for' loop was translated to a VB 'Do While' loop:
                    Do While i < frameCount
                        thumbs(i) = New Thumbnail(file, i, "", "")
                        i += 1
                    Loop
                    Me.thumbnailView1.Items.AddRange(thumbs)

                    'open the first full size page
                    Me.workspaceViewer1.Open(file, 0)

                    _extractedImages = False
                    _currentFile = file
                    _currentResolution = _pdf.Resolution
                    Me.workspaceViewer1.Annotations.CurrentLayer.Items.Clear()
                    menuItemFind.Enabled = True

                    LoadPdfBookmarks(file)
                End If
            End If
        End Sub

        Private Sub menuGetInfo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuGetInfo.Click
            If Me.openFileDialog1.ShowDialog(Me) = DialogResult.OK Then
                Dim fs As FileStream = New FileStream(Me.openFileDialog1.FileName, FileMode.Open, FileAccess.Read, FileShare.Read)
                Dim info As ImageInfo = _pdf.GetImageInfo(fs)
                Dim pdfInfo As PdfImageInfo = CType(info, PdfImageInfo)
                Dim frm As Form = New Parameters("PDF Image Information", info)
                frm.ShowDialog()
                frm.Dispose()
            End If

        End Sub

        Private Sub menuExtractImages_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuExtractImages.Click
            If Me.openFileDialog1.ShowDialog(Me) = DialogResult.OK Then
                Me.workspaceViewer1.Images.Clear()
                Me.thumbnailView1.Items.Cancel()
                Me.thumbnailView1.Items.Clear()

                Dim fs As FileStream = New FileStream(Me.openFileDialog1.FileName, FileMode.Open, FileAccess.Read, FileShare.Read)

                ' Document objects must be disposed.  This makes sure it happens even if there
                ' is an exception
                'INSTANT VB NOTE: The following 'using' block is replaced by its pre-VB.NET 2005 equivalent:
                '				using (Document document = New Document(fs))
                Dim document As Document = New Document(fs)
                Try
                    'reset progress bar, estimate one image per page
                    Me.progressBar1.Maximum = 0
                    Me.progressBar1.Value = 0


                    Dim i As Integer = 0
                    'ORIGINAL LINE: for (int i = 0; i < document.Pages.Count; i += 1)
                    'INSTANT VB NOTE: This 'for' loop was translated to a VB 'Do While' loop:
                    Do While i < document.Pages.Count

                        Dim extractedImages As ExtractedImageInfo() = document.Pages(i).ExtractImages()
                        ' check progressbar
                        Me.progressBar1.Maximum += extractedImages.Length
                        For Each info As ExtractedImageInfo In extractedImages
                            Me.workspaceViewer1.Images.Add(CType(info.Image.Clone(), AtalaImage))
                            Me.thumbnailView1.Items.Add(info.Image, "")
                        Next info
                        i += 1
                    Loop
                Finally
                    Dim disp As IDisposable = document
                    disp.Dispose()
                End Try
                'INSTANT VB NOTE: End of the original C# 'using' block
                _extractedImages = True
                fs.Close()
            End If
        End Sub

        Private Sub menuSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuSave.Click
            If Me.saveFileDialog1.ShowDialog(Me) = DialogResult.OK Then
                Dim encoder As ImageEncoder = Nothing
                Select Case Me.saveFileDialog1.FilterIndex
                    Case 1
                        'for more control over saving, use the PdfCollection and PdfImage classes
                        encoder = New PdfEncoder
                    Case 2
                        encoder = New TiffEncoder
                    Case 3
                        encoder = New JpegEncoder
                End Select
                Me.workspaceViewer1.Save(Me.saveFileDialog1.FileName, encoder)
            End If
        End Sub

        Private Sub menuView_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuView.Click
            Select Case Me.workspaceViewer1.AutoZoom
                Case AutoZoomMode.FitToWidth
                    menuViewFitWidth.Checked = True
                    menuViewFullSize.Checked = False
                    menuViewBestFit.Checked = False
                Case AutoZoomMode.BestFit
                    menuViewFitWidth.Checked = False
                    menuViewFullSize.Checked = False
                    menuViewBestFit.Checked = True
                Case Else
                    menuViewFitWidth.Checked = False
                    menuViewFullSize.Checked = True
                    menuViewBestFit.Checked = False
            End Select
        End Sub

        Private Sub menuViewFullSize_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuViewFullSize.Click
            Me.workspaceViewer1.AutoZoom = AutoZoomMode.None
            Me.workspaceViewer1.Zoom = 1.0
        End Sub

        Private Sub menuViewFitWidth_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuViewFitWidth.Click
            Me.workspaceViewer1.AutoZoom = AutoZoomMode.FitToWidth
        End Sub

        Private Sub menuViewBestFit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuViewBestFit.Click
            Me.workspaceViewer1.AutoZoom = AutoZoomMode.BestFit
        End Sub

        Private Sub menuItem6_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuItem6.Click
            Dim aboutBox As AtalaDemos.AboutBox.About = New AtalaDemos.AboutBox.About("About Atalasoft DotImage PDF Demo", "DotImage PDF Demo")
            aboutBox.Description = "Demonstrates how to view and save PDF files with DotImage and DotImage PDFRasterizer.  Rasterizers a small thumbnail of each page in the PDF asynchronously while loading the first page in the PDF.  Demonstrates use of the ThumbnailView control." & ControlChars.CrLf & ControlChars.CrLf & ""
            aboutBox.ShowDialog()

        End Sub

        Private Sub workspaceViewer1_ChangedImage(ByVal sender As Object, ByVal e As Atalasoft.Imaging.ImageEventArgs) Handles workspaceViewer1.ImageChanged
            If Not e.Image Is Nothing Then
                Me.statusBar1.Panels(0).Text = e.Image.ToString()
            End If
        End Sub

        Private Sub workspaceViewer1_ProcessError(ByVal sender As Object, ByVal e As Atalasoft.Imaging.ExceptionEventArgs) Handles workspaceViewer1.ProcessError
            MessageBox.Show(Me, e.Exception.ToString())
        End Sub

        Private Sub thumbnailView1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles thumbnailView1.SelectedIndexChanged
            If Me.thumbnailView1.SelectedItems.Count > 0 Then
                Me.workspaceViewer1.ScrollPosition = New Point(0, 0)

                If _extractedImages Then
                    Me.workspaceViewer1.Image = Me.workspaceViewer1.Images(Me.thumbnailView1.SelectedIndices(0))
                Else
                    Me.workspaceViewer1.Open(Me.openFileDialog1.FileName, Me.thumbnailView1.SelectedIndices(0))
                    Me.workspaceViewer1.Annotations.CurrentLayer.Items.Clear()
                End If
            End If
        End Sub

        Private Sub thumbnailView1_ThumbnailLoad(ByVal sender As Object, ByVal e As Atalasoft.Imaging.WinControls.ThumbnailEventArgs) Handles thumbnailView1.ThumbnailCreated
            ' update the progressbar for every thumbnail load.
            Me.progressBar1.Value += 1
            If Me.progressBar1.Value = Me.progressBar1.Maximum Then
                Me.progressBar1.Value = 0
            End If
        End Sub

#Region "Printing"

        Private docToPrint As Document = Nothing
        Private imagesToPrint As Pages = Nothing
        Private current As Integer = 0

        Private Sub menuPrint_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuPrint.Click
            If Me.openFileDialog1.FileName = "" Then ' make sure an image is loaded
                Return
            End If

            Dim fs As FileStream = New FileStream(Me.openFileDialog1.FileName, FileMode.Open, FileAccess.Read, FileShare.Read)
            docToPrint = New Document(fs)
            Me.imagesToPrint = docToPrint.Pages
            ' Use System.Drawing.Print.PrintDocument 
            Dim thePrintDoc As PrintDocument = New PrintDocument
            AddHandler thePrintDoc.PrintPage, AddressOf thePrintDoc_PrintPage
            Me.current = 0
            thePrintDoc.Print()
            fs.Close()
        End Sub

        Private Sub thePrintDoc_PrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs)
            e.HasMorePages = True
            Dim p As Page = imagesToPrint(current)
            current += 1
            ' fit to page, only when image is too large. 
            Dim newX As Single = CSng(e.PageBounds.Width / p.Width)
            Dim newY As Single = CSng(e.PageBounds.Height / p.Height)
            If Not (newX > 1 AndAlso newY > 1) Then
                e.Graphics.ScaleTransform(newX, newY)
            End If
            ' Draw pdf image onto graphics object here.
            Dim rs As RenderSettings = New RenderSettings
            rs.AnnotationSettings = AnnotationRenderSettings.RenderAll
            p.Draw(e.Graphics, PageBoundary.Default, rs, 500)
            If current >= imagesToPrint.Count Then
                e.HasMorePages = False
                docToPrint.Dispose() ' make sure to dispose
                docToPrint = Nothing
            End If
        End Sub

#End Region

        Private Sub menuItemFind_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles menuItemFind.Click
            If _findDialog Is Nothing AndAlso Not _currentFile Is Nothing Then
                _pdfDocSearch = New PdfDocumentSearch(GetCurrentPage())
                _findDialog = New FindDialog
                AddHandler _findDialog.Closed, AddressOf findDialog_Closed
                AddHandler _findDialog.OnFindNext, AddressOf findDialog_OnFindNext
                _findDialog.Show()
            End If
        End Sub

        Private Sub findDialog_Closed(ByVal sender As Object, ByVal e As EventArgs)
            _findDialog = Nothing
        End Sub

        Private Function GetCurrentPage() As Integer
            If Me.thumbnailView1.SelectedIndices.Length > 0 Then
                Return Me.thumbnailView1.SelectedIndices(0)
            End If
            Return 0
        End Function


        ''' <summary>
        ''' This is called when the next button is clicked on the find dialog
        ''' </summary>
        ''' <param name="text">The text in the find dialog</param>
        ''' <param name="matchCase">true if the search should match case</param>
        ''' <param name="wholeWord">true if the search should only find whole words</param>
        Private Sub findDialog_OnFindNext(ByVal findText As String, ByVal matchCase As Boolean, ByVal wholeWord As Boolean)
            ' find and highlight the text
            Dim found As Boolean = False
            If Not _currentFile Is Nothing Then
                Dim findHighlighter As PdfFindHighlighter = New PdfFindHighlighter(workspaceViewer1, thumbnailView1, _currentResolution)
                'INSTANT VB NOTE: The following 'using' block is replaced by its pre-VB.NET 2005 equivalent:
                '				using (FileStream fs = New FileStream(_currentFile, FileMode.Open, FileAccess.Read, FileShare.Read))
                Dim fs As FileStream = New FileStream(_currentFile, FileMode.Open, FileAccess.Read, FileShare.Read)
                Try
                    found = _pdfDocSearch.FindNext(fs, findText, matchCase, wholeWord, New PdfDocumentSearch.FindTextHandler(AddressOf findHighlighter.HighlightFoundText))
                Finally
                    Dim disp As IDisposable = fs
                    disp.Dispose()
                End Try
                'INSTANT VB NOTE: End of the original C# 'using' block
                If (Not found) Then
                    MessageBox.Show("The specified text was not found.", "PDF Viewer Search", MessageBoxButtons.OK)
                End If
            End If
        End Sub

#Region "Bookmark Code"

        Private Sub treeBookmarks_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles treeBookmarks.AfterSelect
            Dim bm As PdfBookmark = TryCast(e.Node.Tag, PdfBookmark)
            If bm IsNot Nothing AndAlso bm.ClickAction.Count > 0 Then
                Dim action As PdfAction = bm.ClickAction(0)
                If action.ActionType = PdfActionType.GoToView Then
                    Dim gotoView As PdfGoToViewAction = TryCast(action, PdfGoToViewAction)
                    If gotoView IsNot Nothing AndAlso gotoView.Destination IsNot Nothing AndAlso gotoView.Destination.Page IsNot Nothing Then
                        Dim page As PdfIndexedPageReference = TryCast(gotoView.Destination.Page, PdfIndexedPageReference)
                        If page IsNot Nothing Then
                            If page.PageIndex >= 0 AndAlso page.PageIndex < Me.thumbnailView1.Items.Count Then
                                ' Selecting the thumbnail will load the page.
                                Dim pageIndex As Integer = GetCurrentPage()
                                If pageIndex <> page.PageIndex Then
                                    Me.thumbnailView1.ClearSelection()
                                    Me.thumbnailView1.Items(page.PageIndex).Selected = True
                                    Me.thumbnailView1.ScrollTo(page.PageIndex)
                                End If

                                '                                
                                ' The bookmark coordinates are in PDF page coordinates.
                                ' (0,0) is the lower left corner, increases up and to the right
                                ' Units are 1/72"
                                '                                
                                Dim y As Integer = (If(gotoView.Destination.Top.HasValue, Convert.ToInt32((gotoView.Destination.Top.Value / 72.0) * Me.workspaceViewer1.Image.Resolution.Y), 0))
                                y = Me.workspaceViewer1.Image.Height - y
                                Me.workspaceViewer1.ScrollPosition = New Point(0, -y)
                            End If
                        End If
                    End If
                End If
            End If
        End Sub

        Private Sub LoadPdfBookmarks(ByVal fileName As String)
            Me.treeBookmarks.Nodes.Clear()

            Dim doc As New PdfDocument(fileName)
            If doc.BookmarkTree IsNot Nothing Then
                For Each bookmark As PdfBookmark In doc.BookmarkTree.Bookmarks
                    AddBookMark(Nothing, bookmark)
                Next bookmark
            End If
        End Sub

        Private Sub AddBookMark(ByVal parent As TreeNode, ByVal bookMark As PdfBookmark)
            Dim node As New TreeNode(bookMark.Text)
            node.ForeColor = bookMark.Color
            node.Tag = bookMark

            If parent Is Nothing Then
                Me.treeBookmarks.Nodes.Add(node)
            Else
                parent.Nodes.Add(node)
            End If

            If bookMark.Children IsNot Nothing Then
                For Each bm As PdfBookmark In bookMark.Children
                    AddBookMark(node, bm)
                Next bm
            End If
        End Sub
#End Region
    End Class
End Namespace
