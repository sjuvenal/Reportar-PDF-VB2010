Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.IO
Imports System.Data.SqlClient
Public Class frmlistapdf
    Dim dtproduc As New DataTable
    Dim cn As New SqlConnection("server=localhost; database=DB_PDF; user=sas;password=123456")
    Dim Logo As String = Application.StartupPath & "\Logotipo.gif"
    Dim RutaImagen As String = Application.StartupPath & "\Sinimagen.PNG"
    Dim strCon As String = "DATOS..."
    Private Sub frmpdf_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Call listarproductos()
    End Sub
    Sub listarproductos()
        Dim da As New SqlDataAdapter("select proid Id,procod codigo,pronom Producto,procat Categoria,prouni Unidad,propre Precio,prosto stock,proimg Imagen from producto", cn)
        da.Fill(dtproduc)
        With DataGridView1
            .DataSource = dtproduc
            .Columns(0).Visible = False
            .Columns(1).Width = 50
            .Columns(2).Width = 100
            .Columns(3).Width = 70
            .Columns(4).Width = 50
            .Columns(5).Width = 50
            .Columns(6).Width = 50
            .Columns(7).Width = 150
        End With
    End Sub

    Private Sub btnpdf_Click(sender As System.Object, e As System.EventArgs) Handles btnpdf.Click
        If DataGridView1.RowCount > 0 Then
            Try
                'Intentar generar el documento.
                Dim doc As New Document(PageSize.A4, 10, 10, 85, 10)
                'Dim doc As New Document(PageSize.A4, 10, 10, 20, 20) 'vertical
                'Path que guarda el reporte en el escritorio de windows (Desktop).
                Dim filename As String = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\ListaLinea.pdf"
                Dim file As New FileStream(filename, FileMode.Create, FileAccess.Write, FileShare.ReadWrite)
                Dim writerPdf As PdfWriter = PdfWriter.GetInstance(doc, file)
                Dim ev As New itsEvents()
                writerPdf.PageEvent = ev
                doc.Open()
                Call SubLineas10(doc)
                doc.Close()
                'If rep <> 0 Then
                Process.Start(filename)
                'Else
                '    MsgBox("Codigos no Encontrados", vbCritical, "Error Exportar")
                'End If
            Catch ex As Exception
                'Si el intento es fallido, mostrar MsgBox.
                MessageBox.Show("No se puede generar el documento PDF.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            MsgBox("Seleccione Lineas A Exportar PDF", vbCritical, "Error : Exportar PDF")
        End If
    End Sub
    Sub SubLineas10(ByVal reporte1 As Document)

        Dim subl As Integer = 0
        Dim cod As Integer = 0
        '---buscra la cantidad de linias
        'Dim reporte1 As iTextSharp.text.Document = New iTextSharp.text.Document(iTextSharp.text.PageSize.A4) 'tipo carta 



        Dim Frase As iTextSharp.text.Phrase
        'Dim Chuck As iTextSharp.text.Chunk
        Dim Parrafo As iTextSharp.text.Paragraph
        'Dim Header_Footer As iTextSharp.text.Header

        'iTextSharp.text.pdf.PdfWriter.GetInstance(reporte1, New FileStream("subLineas.pdf", FileMode.Create))

        Dim Tabla1 As iTextSharp.text.pdf.PdfPTable
        Dim Tabla2 As iTextSharp.text.pdf.PdfPTable
        Dim Celda1 As iTextSharp.text.pdf.PdfPCell
        Dim Celda2 As iTextSharp.text.pdf.PdfPCell

        Parrafo = New iTextSharp.text.Paragraph()
        Parrafo.Alignment = iTextSharp.text.Element.ALIGN_CENTER
        Parrafo.Leading = 10.0!

        'Creo el encabezado

        'ahora agregamos un nuevo parrafo para agregar el pie de pagina
        Parrafo = New iTextSharp.text.Paragraph("locaso")
        Parrafo.Alignment = iTextSharp.text.Element.ALIGN_CENTER
        Parrafo.Leading = 8.0!


        'Dim encabezado As New Paragraph("PDF", New Font(Font.Name = "Tahoma", 20, Font.Bold))
        'encabezado.Alignment = Element.ALIGN_CENTER
        'Se crea el texto abajo del encabezado.  
        '' Dim texto As New Phrase("Lista de Precios Del:" + Now.Date() & "-" & TimeOfDay, New Font(Font.Name = "Tahoma", 14, Font.Bold))

        Dim imagendemo As iTextSharp.text.Image 'Declaracion de una imagen
        imagendemo = iTextSharp.text.Image.GetInstance(Logo) 'Dirreccion a la imagen que se hace referencia
        imagendemo.SetAbsolutePosition(450, 750) 'Posicion en el eje cartesiano
        imagendemo.ScaleAbsoluteWidth(120) 'Ancho de la imagen
        imagendemo.ScaleAbsoluteHeight(70) 'Altura de la imagen


        Tabla1 = New iTextSharp.text.pdf.PdfPTable(4)
        Tabla1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER
        Tabla1.WidthPercentage = 100.0!
        Tabla1.SetWidths(New Single() {50.0!, 50.0!, 50.0!, 50.0!})

        'ENCABEZADO DE LISTA DE CODIGOS *************************************************************************************************************
        Frase = New iTextSharp.text.Phrase("IMAGEN", iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA_BOLDOBLIQUE, 12))
        Celda1 = New iTextSharp.text.pdf.PdfPCell(Frase)
        Celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER
        Celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE
        Tabla1.AddCell(Celda1)


        Frase = New iTextSharp.text.Phrase("DETALLE MERCADERIA", iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA_BOLDOBLIQUE, 12))
        Celda1 = New iTextSharp.text.pdf.PdfPCell(Frase)
        Celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER
        Celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE
        Tabla1.AddCell(Celda1)

        Frase = New iTextSharp.text.Phrase("IMAGEN", iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA_BOLDOBLIQUE, 12))
        Celda1 = New iTextSharp.text.pdf.PdfPCell(Frase)
        Celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER
        Celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE

        Tabla1.AddCell(Celda1)

        Frase = New iTextSharp.text.Phrase("DETALLE MERCADERIA", iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA_BOLDOBLIQUE, 12))
        Celda1 = New iTextSharp.text.pdf.PdfPCell(Frase)
        Celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER
        Celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE
        Tabla1.AddCell(Celda1)

        'For Sublin As Integer = 0 To 1

        'SubLinProductos = OBjSubLineaCN.BuscarSubLineaProduct(3, DataGridView1.Item(0, Sublin).Value, 8)
        If DataGridView1.Rows.Count > 0 Then

            subl = subl + 1

            Frase = New iTextSharp.text.Phrase("LISTA DE PRODUCTOS", iTextSharp.text.FontFactory.GetFont("times", 12, Font.Bold, BaseColor.WHITE))
            Celda1 = New iTextSharp.text.pdf.PdfPCell(Frase)
            Celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER
            Celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE
            Celda1.Colspan = 4
            Celda1.BackgroundColor = BaseColor.RED
            Tabla1.AddCell(Celda1)

            For Prod As Integer = 0 To DataGridView1.Rows.Count - 1

                cod = cod + 1

                If Prod <= DataGridView1.Rows.Count - 1 Then
                    Dim Img As Image
                    Try
                        Img = Image.GetInstance(DataGridView1.Item("imagen", Prod).Value)
                    Catch ex As Exception
                        Img = Image.GetInstance(RutaImagen)
                    End Try
                    Img.SetAbsolutePosition(6, 760) 'Posicion en el eje cartesiano
                    Img.ScaleAbsoluteWidth(100) 'Ancho de la imagen
                    Img.ScaleAbsoluteHeight(70) 'Altura de la imagen
                    Celda1 = New iTextSharp.text.pdf.PdfPCell(Img)
                    Celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER
                    Celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE
                    Tabla1.AddCell(Celda1)

                    ''cremos la tabla
                    Tabla2 = New iTextSharp.text.pdf.PdfPTable(2)
                    Tabla2.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER
                    Tabla2.WidthPercentage = 25.0!
                    Tabla2.SetWidths(New Single() {11.0!, 14.0!})


                    Frase = New iTextSharp.text.Phrase("CODIGO ", iTextSharp.text.FontFactory.GetFont("times", 7, Font.Bold, BaseColor.BLACK))
                    Celda2 = New iTextSharp.text.pdf.PdfPCell(Frase)
                    Celda2.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT
                    Celda2.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE
                    Celda2.Border = 0
                    Celda2.BackgroundColor = New iTextSharp.text.BaseColor(250, 250, 210)
                    Tabla2.AddCell(Celda2)

                    Frase = New iTextSharp.text.Phrase(": " & DataGridView1.Item("codigo", Prod).Value & " - " & DataGridView1.Item("producto", Prod).Value, iTextSharp.text.FontFactory.GetFont("times", 8, Font.Bold, BaseColor.BLACK))
                    Celda2 = New iTextSharp.text.pdf.PdfPCell(Frase)
                    Celda2.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT
                    Celda2.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE
                    Celda2.Border = 0
                    Celda2.BackgroundColor = New iTextSharp.text.BaseColor(152, 251, 152)
                    Tabla2.AddCell(Celda2)

                    Frase = New iTextSharp.text.Phrase("PRECIO", iTextSharp.text.FontFactory.GetFont("times", 7, Font.Italic, BaseColor.BLUE))
                    Celda2 = New iTextSharp.text.pdf.PdfPCell(Frase)
                    Celda2.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT
                    Celda2.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE
                    Celda2.Border = 0
                    Celda2.BackgroundColor = New iTextSharp.text.BaseColor(250, 250, 210)
                    Tabla2.AddCell(Celda2)

                    Frase = New iTextSharp.text.Phrase(": " & DataGridView1.Item("precio", Prod).Value, iTextSharp.text.FontFactory.GetFont("times", 8, Font.Italic, BaseColor.BLUE))
                    Celda2 = New iTextSharp.text.pdf.PdfPCell(Frase)
                    Celda2.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT
                    Celda2.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE
                    Celda2.Border = 0
                    Celda2.BackgroundColor = New iTextSharp.text.BaseColor(250, 250, 210)
                    Tabla2.AddCell(Celda2)


                    Frase = New iTextSharp.text.Phrase("UNIDAD ", iTextSharp.text.FontFactory.GetFont("times", 7, Font.Italic, BaseColor.BLUE))
                    Celda2 = New iTextSharp.text.pdf.PdfPCell(Frase)
                    Celda2.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT
                    Celda2.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE
                    Celda2.Border = 0
                    Tabla2.AddCell(Celda2)

                    Frase = New iTextSharp.text.Phrase(": " & DataGridView1.Item("unidad", Prod).Value, iTextSharp.text.FontFactory.GetFont("times", 8, Font.Italic, BaseColor.BLUE))
                    Celda2 = New iTextSharp.text.pdf.PdfPCell(Frase)
                    Celda2.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT
                    Celda2.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE
                    Celda2.Border = 0
                    Tabla2.AddCell(Celda2)

                    Frase = New iTextSharp.text.Phrase("EMPAQUE ", iTextSharp.text.FontFactory.GetFont("times", 7, Font.Bold, BaseColor.BLUE))
                    Celda2 = New iTextSharp.text.pdf.PdfPCell(Frase)
                    Celda2.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT
                    Celda2.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE
                    Celda2.Border = 0
                    Celda2.BackgroundColor = New iTextSharp.text.BaseColor(250, 250, 210)
                    Tabla2.AddCell(Celda2)

                    Frase = New iTextSharp.text.Phrase(": sin empaque", iTextSharp.text.FontFactory.GetFont("times", 8, Font.Bold, BaseColor.BLUE))
                    Celda2 = New iTextSharp.text.pdf.PdfPCell(Frase)
                    Celda2.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT
                    Celda2.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE
                    Celda2.Border = 0
                    Celda2.BackgroundColor = New iTextSharp.text.BaseColor(250, 250, 210)
                    Tabla2.AddCell(Celda2)

                    Frase = New iTextSharp.text.Phrase("STOCK ", iTextSharp.text.FontFactory.GetFont("times", 7, Font.Bold, BaseColor.BLUE))
                    Celda2 = New iTextSharp.text.pdf.PdfPCell(Frase)
                    Celda2.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT
                    Celda2.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE
                    Celda2.Border = 0
                    Tabla2.AddCell(Celda2)

                    Frase = New iTextSharp.text.Phrase(": " & DataGridView1.Item("stock", Prod).Value, iTextSharp.text.FontFactory.GetFont("times", 8, Font.Bold, BaseColor.BLUE))
                    Celda2 = New iTextSharp.text.pdf.PdfPCell(Frase)
                    Celda2.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT
                    Celda2.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE
                    Celda2.Border = 0
                    'Celda2.BackgroundColor = BaseColor.ORANGE
                    Tabla2.AddCell(Celda2)

                    Frase = New iTextSharp.text.Phrase("CATEGORIA :", iTextSharp.text.FontFactory.GetFont("times", 7, Font.Bold, BaseColor.BLUE))
                    Celda2 = New iTextSharp.text.pdf.PdfPCell(Frase)
                    Celda2.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT
                    Celda2.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE
                    Celda2.Border = 0
                    Celda2.BackgroundColor = New iTextSharp.text.BaseColor(250, 250, 210)
                    Tabla2.AddCell(Celda2)

                    Frase = New iTextSharp.text.Phrase(": " & DataGridView1.Item("Categoria", Prod).Value, iTextSharp.text.FontFactory.GetFont("times", 8, Font.Bold, BaseColor.BLUE))
                    Celda2 = New iTextSharp.text.pdf.PdfPCell(Frase)
                    Celda2.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT
                    Celda2.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE
                    Celda2.Border = 0
                    Celda2.BackgroundColor = New iTextSharp.text.BaseColor(250, 250, 210)
                    Tabla2.AddCell(Celda2)
                    Tabla1.AddCell(Tabla2)
                Else
                    Frase = New iTextSharp.text.Phrase("", iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA_BOLDOBLIQUE, 12))
                    Celda1 = New iTextSharp.text.pdf.PdfPCell(Frase)
                    Celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER
                    Celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE
                    Celda1.Border = 0
                    Tabla1.AddCell(Celda1)

                    Frase = New iTextSharp.text.Phrase("", iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA_BOLDOBLIQUE, 12))
                    Celda1 = New iTextSharp.text.pdf.PdfPCell(Frase)
                    Celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER
                    Celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE
                    Celda1.Border = 0
                    Tabla1.AddCell(Celda1)

                End If

                Prod = Prod + 1

                'COLUMN 03 IMAGEN DEL CODIGO
                If Prod <= DataGridView1.Rows.Count - 1 Then

                    Dim ImgK As Image
                    Try
                        ImgK = Image.GetInstance(DataGridView1.Item("imagen", Prod).Value)

                    Catch ex As Exception
                        ImgK = Image.GetInstance(RutaImagen)
                    End Try
                    'Dim imagendemo As iTextSharp.text.Image 'Declaracion de una imagen
                    'Img = iTextSharp.text.Image.GetInstance(Logo) 'Dirreccion a la imagen que se hace referencia
                    ImgK.SetAbsolutePosition(6, 760) 'Posicion en el eje cartesiano
                    ImgK.ScaleAbsoluteWidth(100) 'Ancho de la imagen
                    ImgK.ScaleAbsoluteHeight(70) 'Altura de la imagen
                    'agrego la imagen a la celda         
                    Celda1 = New iTextSharp.text.pdf.PdfPCell(ImgK)
                    Celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER
                    Celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE
                    Tabla1.AddCell(Celda1)

                    'COLUMAN 04 DETALLE DEL CODGIO

                    Frase = New iTextSharp.text.Phrase(1, iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA_BOLDOBLIQUE, 12))
                    Celda1 = New iTextSharp.text.pdf.PdfPCell(Frase)
                    Celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER
                    Celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE
                    ''cremos la tabla
                    'Datos del CODIGO
                    Tabla2 = New iTextSharp.text.pdf.PdfPTable(2)
                    Tabla2.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER
                    Tabla2.WidthPercentage = 25.0!
                    'Tabla2.

                    Tabla2.SetWidths(New Single() {11.0!, 14.0!})

                    Frase = New iTextSharp.text.Phrase("CODIGO ", iTextSharp.text.FontFactory.GetFont("Tahoma", 7, Font.Bold, BaseColor.BLUE))
                    cod = cod + 1

                    Celda2 = New iTextSharp.text.pdf.PdfPCell(Frase)
                    Celda2.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT
                    Celda2.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE
                    Celda2.Border = 0
                    Celda2.BackgroundColor = New iTextSharp.text.BaseColor(250, 250, 210)
                    Tabla2.AddCell(Celda2)

                    Frase = New iTextSharp.text.Phrase(": " & DataGridView1.Item("codigo", Prod).Value & " - " & DataGridView1.Item("producto", Prod).Value, iTextSharp.text.FontFactory.GetFont("Tahoma", 8, Font.Bold, BaseColor.BLUE))
                    Celda2 = New iTextSharp.text.pdf.PdfPCell(Frase)
                    Celda2.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT
                    Celda2.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE
                    Celda2.Border = 0
                    Celda2.BackgroundColor = New iTextSharp.text.BaseColor(250, 250, 210)
                    Tabla2.AddCell(Celda2)


                    Frase = New iTextSharp.text.Phrase("PRECIO", iTextSharp.text.FontFactory.GetFont("Tahoma", 7, Font.Italic, BaseColor.BLUE))
                    Celda2 = New iTextSharp.text.pdf.PdfPCell(Frase)
                    Celda2.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT
                    Celda2.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE
                    Celda2.Border = 0
                    Celda2.BackgroundColor = New iTextSharp.text.BaseColor(250, 250, 210)
                    Tabla2.AddCell(Celda2)

                    Frase = New iTextSharp.text.Phrase(": " & DataGridView1.Item("precio", Prod).Value, iTextSharp.text.FontFactory.GetFont("Tahoma", 8, Font.Italic, BaseColor.BLUE))
                    Celda2 = New iTextSharp.text.pdf.PdfPCell(Frase)
                    Celda2.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT
                    Celda2.VerticalAlignment = iTextSharp.text.Element.ALIGN_LEFT
                    Celda2.Border = 0
                    Celda2.BackgroundColor = New iTextSharp.text.BaseColor(250, 250, 210)
                    Tabla2.AddCell(Celda2)

                    Frase = New iTextSharp.text.Phrase("UNIDAD ", iTextSharp.text.FontFactory.GetFont("Tahoma", 7, Font.Italic, BaseColor.BLUE))
                    Celda2 = New iTextSharp.text.pdf.PdfPCell(Frase)
                    Celda2.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT
                    Celda2.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE
                    Celda2.Border = 0
                    Tabla2.AddCell(Celda2)

                    Frase = New iTextSharp.text.Phrase(": " & DataGridView1.Item("unidad", Prod).Value, iTextSharp.text.FontFactory.GetFont("Tahoma", 8, Font.Italic, BaseColor.BLUE))
                    Celda2 = New iTextSharp.text.pdf.PdfPCell(Frase)
                    Celda2.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT
                    Celda2.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE
                    Celda2.Border = 0
                    Tabla2.AddCell(Celda2)

                    Frase = New iTextSharp.text.Phrase("EMPAQUE ", iTextSharp.text.FontFactory.GetFont("Tahoma", 7, Font.Bold, BaseColor.BLUE))
                    Celda2 = New iTextSharp.text.pdf.PdfPCell(Frase)
                    Celda2.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT
                    Celda2.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE
                    Celda2.Border = 0
                    Celda2.BackgroundColor = New iTextSharp.text.BaseColor(250, 250, 210)
                    Tabla2.AddCell(Celda2)

                    Frase = New iTextSharp.text.Phrase(": sin empaque", iTextSharp.text.FontFactory.GetFont("Tahoma", 8, Font.Bold, BaseColor.BLUE))
                    Celda2 = New iTextSharp.text.pdf.PdfPCell(Frase)
                    Celda2.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT
                    Celda2.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE
                    Celda2.Border = 0
                    Celda2.BackgroundColor = New iTextSharp.text.BaseColor(250, 250, 210)
                    Tabla2.AddCell(Celda2)

                    Frase = New iTextSharp.text.Phrase("STOCK ", iTextSharp.text.FontFactory.GetFont("Tahoma", 7, Font.Bold, BaseColor.BLUE))
                    Celda2 = New iTextSharp.text.pdf.PdfPCell(Frase)
                    Celda2.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT
                    Celda2.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE
                    Celda2.Border = 0
                    Tabla2.AddCell(Celda2)

                    Frase = New iTextSharp.text.Phrase(": " & DataGridView1.Item("stock", Prod).Value, iTextSharp.text.FontFactory.GetFont("Tahoma", 8, Font.Bold, BaseColor.BLUE))
                    Celda2 = New iTextSharp.text.pdf.PdfPCell(Frase)
                    Celda2.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT
                    Celda2.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE
                    Celda2.Border = 0
                    Tabla2.AddCell(Celda2)

                    Frase = New iTextSharp.text.Phrase("CATEGORIA ", iTextSharp.text.FontFactory.GetFont("Tahoma", 7, Font.Bold, BaseColor.BLUE))
                    Celda2 = New iTextSharp.text.pdf.PdfPCell(Frase)
                    Celda2.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT
                    Celda2.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE
                    Celda2.Border = 0
                    Celda2.BackgroundColor = New iTextSharp.text.BaseColor(250, 250, 210)
                    Tabla2.AddCell(Celda2)

                    Frase = New iTextSharp.text.Phrase(": " & DataGridView1.Item("categoria", Prod).Value, iTextSharp.text.FontFactory.GetFont("Tahoma", 8, Font.Bold, BaseColor.BLUE))
                    Celda2 = New iTextSharp.text.pdf.PdfPCell(Frase)
                    Celda2.HorizontalAlignment = iTextSharp.text.Element.ALIGN_LEFT
                    Celda2.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE
                    Celda2.Border = 0
                    Celda2.BackgroundColor = New iTextSharp.text.BaseColor(250, 250, 210)
                    Tabla2.AddCell(Celda2)
                    Tabla1.AddCell(Tabla2)
                Else
                    Frase = New iTextSharp.text.Phrase("", iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA_BOLDOBLIQUE, 12))
                    Celda1 = New iTextSharp.text.pdf.PdfPCell(Frase)
                    Celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER
                    Celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE
                    Celda1.Border = 0
                    Tabla1.AddCell(Celda1)

                    Frase = New iTextSharp.text.Phrase("", iTextSharp.text.FontFactory.GetFont(iTextSharp.text.FontFactory.HELVETICA_BOLDOBLIQUE, 12))
                    Celda1 = New iTextSharp.text.pdf.PdfPCell(Frase)
                    Celda1.HorizontalAlignment = iTextSharp.text.Element.ALIGN_CENTER
                    Celda1.VerticalAlignment = iTextSharp.text.Element.ALIGN_MIDDLE
                    Celda1.Border = 0
                    Tabla1.AddCell(Celda1)
                End If
            Next
        End If
        'Next
        reporte1.Add(imagendemo)
        'reporte1.Add(encabezado)
        'reporte1.Add(texto)
        reporte1.Add(Tabla1)

    End Sub
    Public Class itsEvents
        Inherits PdfPageEventHelper
        ' This is the contentbyte object of the writer
        Private cb As PdfContentByte

        ' this is the BaseFont we are going to use for the header / footer
        Private bf As BaseFont = Nothing

        Public tituloReporte As String
        Public fechaReporte As String

        ' we will put the final number of pages in a template
        Protected total As PdfTemplate
        Protected helv As BaseFont
        ' we override the onOpenDocument method
        Public Overrides Sub OnOpenDocument(ByVal writer As PdfWriter, ByVal document As Document)
            Try
                bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED)
                cb = writer.DirectContent
                total = writer.DirectContent.CreateTemplate(100, 100)
                total.BoundingBox = New Rectangle(-20, -20, 100, 100)
                helv = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.WINANSI, BaseFont.NOT_EMBEDDED)
            Catch ex As DocumentException
                Throw ex
            Catch ex As IOException
                Throw ex
            End Try
        End Sub
        'Public Overrides Sub OnStartPage(ByVal writer As PdfWriter, ByVal document As Document)
        '    cb.PdfDocument.SetMargins(10, 10, 85, 10)
        'End Sub
        ' we override the onEndPage method
        Public Overrides Sub OnEndPage(ByVal writer As PdfWriter, ByVal documento As Document)
            '#Region "--[Encabezados]--"
            Dim font24B As Font = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 24, iTextSharp.text.Font.BOLD, BaseColor.GRAY)
            Dim font15B As Font = FontFactory.GetFont(FontFactory.HELVETICA, 15, iTextSharp.text.Font.BOLD)

            Dim Titulo As New Phrase("CATALAGO", font24B)
            Dim tipo As New Phrase(tituloReporte, font15B)
            Dim fechas As New Phrase(fechaReporte, font15B)

            cb.BeginText()
            cb.SetTextMatrix(280, 300)
            Dim ct As New ColumnText(cb)
            ct.SetSimpleColumn(Titulo, documento.Left, 0, documento.Right, documento.Top + 38, 0, _
             Element.ALIGN_CENTER)
            ct.Go()

            Dim ci As New ColumnText(cb)
            ci.SetSimpleColumn(tipo, documento.Left, 0, documento.Right, documento.Top + 20, 0, _
             Element.ALIGN_CENTER)
            ci.Go()

            Dim cf As New ColumnText(cb)
            cf.SetSimpleColumn(fechas, documento.Left, 0, documento.Right, documento.Top + 5, 0, _
             Element.ALIGN_CENTER)
            cf.Go()
            cb.EndText()
            '#End Region

            cb.SaveState()
            Dim text As String = "Página " + writer.PageNumber.ToString().PadLeft(1, " "c) + " de "
            Dim textBase As Single = documento.Bottom - 20
            cb.BeginText()
            cb.SetFontAndSize(helv, 12)

            cb.SetTextMatrix(255, 30)
            cb.ShowText(text)
            cb.EndText()
            cb.AddTemplate(total, 330, 30)

            cb.RestoreState()
        End Sub
        ' we override the onCloseDocument method
        Public Overrides Sub OnCloseDocument(ByVal writer As PdfWriter, ByVal document As Document)
            If writer.PageNumber > 0 Then
                writer.ViewerPreferences = PdfWriter.PageLayoutTwoPageLeft
            Else
                writer.ViewerPreferences = PdfWriter.PageLayoutSinglePage
            End If

            total.BeginText()
            total.SetFontAndSize(helv, 12)
            total.SetTextMatrix(0, 0)
            Dim pageNumber As Integer = writer.PageNumber - 1
            total.ShowText("   " & Convert.ToString(pageNumber))
            total.EndText()
        End Sub
    End Class

    Private Sub btnssalir_Click(sender As System.Object, e As System.EventArgs) Handles btnssalir.Click
        Me.Close()
    End Sub
End Class