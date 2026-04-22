Imports System
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports Atalasoft.Imaging.WinControls
Imports Atalasoft.Annotate.UI
Imports Atalasoft.Annotate
Imports Atalasoft.Pdf.TextExtract
Imports Atalasoft.Utils.Geometry

Namespace PdfDemo
    Public Class PdfFindHighlighter

        Private _viewer As AnnotateViewer
        Private _thumbnailViewer As ThumbnailView
        Private _resolution As Int32
        Private _hiColor As Color = Color.LightBlue


        ''' <summary>
        ''' A class to highlight pdf documents that are in an annotation viewer and have a thumbnail viewer
        ''' </summary>
        ''' <param name="viewer">An annotation viewer with one layer for putting highlights on</param>
        ''' <param name="thumbnailViewer">An optional thumbnail viewer (can be null)</param>
        ''' <param name="resolution">The resolution of the image that the viewer is showing</param>
        Public Sub New(ByVal viewer As AnnotateViewer, ByVal thumbnailViewer As ThumbnailView, ByVal resolution As Int32)
            _viewer = viewer
            _thumbnailViewer = thumbnailViewer
            _resolution = resolution
        End Sub


        ''' <summary>
        ''' The color to draw the highlights in (will be automatically made translucent)
        ''' </summary>
        Public Property HighlightColor() As Color
            Get
                Return _hiColor
            End Get

            Set(ByVal Value As Color)
                _hiColor = Value
            End Set
        End Property

        ''' <summary>
        ''' Highlights the found text on the PDF, makes sure the page is selected and the scroll
        ''' position is set so that the highlight can be seen
        ''' </summary>
        ''' <param name="pdfPage">The PDF Page with the text on it</param>
        ''' <param name="pageNum">The page number (0-based)</param>
        ''' <param name="charIndex">The index of the first character to highlight</param>
        ''' <param name="len">The length of the text to highlight</param>
        Public Sub HighlightFoundText(ByVal pdfPage As PdfTextPage, ByVal pageNum As Int32, ByVal charIndex As Int32, ByVal len As Int32)

            SelectThumb(_thumbnailViewer, pageNum)
            HighlightChars(_viewer, pdfPage, _hiColor, charIndex, len)

        End Sub

        ''' <summary>
        ''' Select the given thumb
        ''' </summary>
        ''' <param name="page">A 0-based index to the page to select</param>
        Private Sub SelectThumb(ByVal tv As ThumbnailView, ByVal page As Int32)

            If (Not tv Is Nothing) And (Not (tv.Items(page).Selected)) Then
                tv.ClearSelection()
                tv.Items(page).Selected = True
                tv.ScrollTo(page)
            End If
        End Sub

        ''' <summary>
        ''' Puts an annotation over the characters in a PDF page
        ''' </summary>
        ''' <param name="p">A PDF Text page</param>
        ''' <param name="hiColor">The highlight color</param>
        ''' <param name="index">The index of the first character within the page</param>
        ''' <param name="len">The number of characters to highlight</param>
        Private Sub HighlightChars(ByVal v As AnnotateViewer, ByVal p As PdfTextPage, ByVal hiColor As Color, ByVal index As Int32, ByVal len As Int32)

            '' take off all existing hilights
            Dim layer As LayerAnnotation = v.Annotations.CurrentLayer
            layer.Items.Clear()

            '' a single phrase can span a line and need more than one box to
            '' highlight it
            Dim pdfBoxes As QuadrilateralF() = p.GetBoxes(index, len)
            For Each b As QuadrilateralF In pdfBoxes
                HighlightPDFBox(p, v, layer, b.Bounds, hiColor, 2)
            Next
        End Sub

        ''' <summary>
        ''' Puts a highlight rectangle over the box which is expressed in PDF coordinate space
        ''' </summary>
        ''' <param name="p">A PDF Text page</param>
        ''' <param name="v">The viewer to put the annotation on</param>
        ''' <param name="layer">The layer to put the annotation on</param>
        ''' <param name="pdfBox">The box to highlight in PDF coordinate space</param>
        ''' <param name="hiColor">The color to highlight with</param>
        ''' <param name="padding">Extra padding around the rectangle in pixels</param>
        Private Sub HighlightPDFBox(ByVal p As PdfTextPage, ByVal v As AnnotateViewer, ByVal layer As LayerAnnotation, ByVal pdfBox As RectangleF, ByVal hiColor As Color, ByVal padding As Int32)
            Dim imageBox As RectangleF = p.ConvertPdfUnitsToPixels(New QuadrilateralF(pdfBox), v.Image.Resolution).Bounds
            imageBox.X = imageBox.X - padding
            imageBox.Y = imageBox.Y - padding
            imageBox.Width = imageBox.Width + 2 * padding
            imageBox.Height = imageBox.Height + 2 * padding
            HighlightImageBox(v, layer, imageBox, hiColor)
        End Sub

        ''' <summary>
        ''' Puts a highlight rectangle over the box which is expressed in the image's coordinate space
        ''' </summary>
        ''' <param name="layer">The layer to put the annotation on</param>
        ''' <param name="imageBox">The box to highlight in Image coordinate space</param>
        ''' <param name="hiColor">The color to highlight with</param>
        Private Sub HighlightImageBox(ByVal v As AnnotateViewer, ByVal layer As LayerAnnotation, ByVal imageBox As RectangleF, ByVal hiColor As Color)

            CreateHighlightAnnotation(layer, imageBox, hiColor)
            EnsureAreaIsVisible(v, imageBox)

        End Sub

        ''' <summary>
        ''' Put an annotation on the layer that highlights the rectangle in the color passed in
        ''' </summary>
        ''' <param name="la">The layer on which to put the annotation</param>
        ''' <param name="imageBox">The bounds of the annotation</param>
        ''' <param name="hiColor">The color of the annotation</param>
        Private Sub CreateHighlightAnnotation(ByVal layer As LayerAnnotation, ByVal imageBox As RectangleF, ByVal hiColor As Color)

            Dim ra As RectangleAnnotation = New RectangleAnnotation(imageBox, _
                New AnnotationBrush(hiColor), Nothing)
            ra.Translucent = True
            ra.Data.CanMove = False
            ra.Data.CanResize = False
            ra.Data.CanSelect = False
            ra.Data.CanRotate = False
            layer.Items.Add(ra)
        End Sub

        ''' <summary>
        ''' Scrolls the image to make sure that the box is visible
        ''' </summary>
        ''' <param name="imageBox">The box to make sure is visible</param>
        Private Sub EnsureAreaIsVisible(ByVal v As AnnotateViewer, ByVal imageBox As RectangleF)

            Dim sp As Point = v.ScrollPosition

            Dim x As Int32 = GetAdjustedScrollPos(sp.X, imageBox.Left, imageBox.Right, v.ClientSize.Width, v.Zoom)
            Dim y As Int32 = GetAdjustedScrollPos(sp.Y, imageBox.Top, imageBox.Bottom, v.ClientSize.Height, v.Zoom)

            v.ScrollPosition = New Point(x, y)
        End Sub


        ''' <summary>
        ''' Figures out the new scroll position based on the current scroll and the area that should be shown.
        ''' If the area is shown under the current position, it is not adjusted.
        ''' </summary>
        ''' <param name="clientScrollPos">The current scroll, in display pixels.  If the area is shown with this scroll, it is not adjusted</param>
        ''' <param name="min">The minimum position to be visible, in image pixels</param>
        ''' <param name="max">The maximum position to be visible, in image pixels</param>
        ''' <param name="clientSize">The size of the client window in display pixels</param>
        ''' <param name="zoomLevel">The zoom level of the viewer</param>
        ''' <returns>The new scroll position adjusted if necessary</returns>
        Private Function GetAdjustedScrollPos(ByVal clientScrollPos As Int32, ByVal min As Single, ByVal max As Single, ByVal clientSize As Int32, ByVal zoomLevel As Double) As Int32

            Dim clientMin As Int32 = (min * zoomLevel)
            Dim clientMax As Int32 = (max * zoomLevel)
            If (clientMin < -clientScrollPos) Then
                clientScrollPos = -clientMin
            ElseIf (clientMax > -clientScrollPos + clientSize) Then
                clientScrollPos = -clientMin
            End If

            Return clientScrollPos

        End Function

    End Class
End Namespace