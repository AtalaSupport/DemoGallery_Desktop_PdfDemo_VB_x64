Imports System
Imports System.IO
Imports Atalasoft.Pdf.TextExtract

Namespace PdfDemo
    Public Class PdfDocumentSearch
        Public Delegate Sub FindTextHandler(ByVal pdfPage As PdfTextPage, ByVal pageNum As Int32, ByVal charIndex As Int32, ByVal len As Int32)

        Private _currentFindPos As Int32 = 0
        Private _currentFindPage As Int32 = -1

        ''' <summary>
        ''' Starts a PDF document search on the given page.
        ''' </summary>
        ''' <param name="startPage">The page to start on</param>
        Public Sub New(ByVal startPage As Int32)
            _currentFindPage = startPage
        End Sub

        ''' <summary>
        ''' Finds the next instance of the text in the stream representing a PDF
        ''' </summary>
        ''' <param name="stream">A stream with a PDF in it</param>
        ''' <param name="findText">The text to find</param>
        ''' <param name="matchCase">true if the search should match the case of the text</param>
        ''' <param name="wholeWord">true if only whole words should be found</param>
        ''' <param name="onFindText">A delegate to call when text is found</param>
        ''' <returns>true if the text was found, false otherwise</returns>
        Public Function FindNext(ByVal stream As Stream, ByVal findText As String, ByVal matchCase As Boolean, ByVal wholeWord As Boolean, ByVal onFindText As FindTextHandler) As Boolean
            Dim findPage As Int32 = _currentFindPage

            '' Document objects must be disposed.  
            '' This makes sure it happens even if there is an exception
            Dim document As PdfTextDocument = New PdfTextDocument(stream)
            Try
                Dim found As Boolean = False
                Dim loopedToBeginning As Boolean = False
                While (Not found)
                    Dim p As PdfTextPage = document.GetPage(findPage)
                    If p.CharCount > 0 Then
                        Dim srchRes As PdfSearchResults = p.Search(_currentFindPos, findText, matchCase, wholeWord)
                        Try

                            srchRes.FindNext()

                            '' if you found something, tell the delegate where it is
                            If (srchRes.HasResults) Then
                                onFindText(p, findPage, srchRes.StartIndex, srchRes.CharCount)
                                _currentFindPos = srchRes.StartIndex + 1
                                _currentFindPage = findPage
                                found = True

                            ElseIf (loopedToBeginning) Then
                                '' if we are back at the start page, then the text is not in the doc
                                Exit While
                            Else
                                '' otherwise go to the next page
                                findPage = (findPage + 1) Mod document.PageCount
                                _currentFindPos = 0
                                If (findPage = _currentFindPage) Then
                                    loopedToBeginning = True
                                End If

                            End If
                        Finally
                            srchRes.Dispose()
                        End Try
                    Else
                        System.Windows.Forms.MessageBox.Show("This PDF file does not contain text data.", "No Text Data")
                        Exit While
                    End If
                End While
                Return found
            Finally
                document.Dispose()
            End Try

        End Function


    End Class
End Namespace

