Public Class WhatDo
    Dim mainWindo As MainWindow
    'Metodo constructor que crea la ventana que hacer despues de ljuego
    Public Sub New(ByVal mainWn As MainWindow, ByVal nombreGanador As String, ByVal colorGanador As String, ByVal cantidad As Integer)
        InitializeComponent()
        mainWindo = mainWn
        lblGanador.Content = lblGanador.Content + nombreGanador
        lblCantidadMoves.Content = lblCantidadMoves.Content & cantidad
        Pintar(colorGanador)
        WriteTxt(nombreGanador, colorGanador, cantidad)
    End Sub

    'Este metodo lo pone en el txt los datos
    Public Sub WriteTxt(ByVal nombreGanador As String, ByVal ColorGanador As String, ByVal cantidad As Integer)
        Dim ejecutarrutacompleta = Microsoft.VisualBasic.Right(My.Application.Info.DirectoryPath.Length - 10, My.Application.Info.DirectoryPath.Length)
        Dim RutaDeaplicacion As String = Microsoft.VisualBasic.Left(My.Application.Info.DirectoryPath, ejecutarrutacompleta)
        Dim EjecutarArchivo = RutaDeaplicacion & "\Text\"
        Dim FILE_NAME As String = EjecutarArchivo & "Ganadores.txt"
        If System.IO.File.Exists(FILE_NAME) = True Then
            Dim objWriter As New System.IO.StreamWriter(FILE_NAME)
            objWriter.WriteLine("Gano: " & nombreGanador)
            objWriter.WriteLine("Color: " & ColorGanador)
            objWriter.WriteLine("Cantidad Moves: " & cantidad)
            objWriter.Close()
        Else
            MsgBox("File Doesn't Exist")
        End If
    End Sub


    'Este metodo resetea el juego
    Public Sub click_Reset(ByVal res As Object, ByVal e As EventArgs) Handles btnReset.Click
        mainWindo.Reset()
        Me.Close()
    End Sub

    'Este metodo pinta el elpise del color ganador
    Public Sub Pintar(ByVal colorPaint As String)
        If colorPaint = "Blue" Then
            eye1.Fill = Brushes.Blue
            eye2.Fill = Brushes.Blue
            eye3.Fill = Brushes.Blue
        ElseIf colorPaint = "Yellow" Then
            eye1.Fill = Brushes.Yellow
            eye2.Fill = Brushes.Yellow
            eye3.Fill = Brushes.Yellow
        ElseIf colorPaint = "Brown" Then
            eye1.Fill = Brushes.Brown
            eye3.Fill = Brushes.Brown
            eye2.Fill = Brushes.Brown
        ElseIf colorPaint = "Black" Then
            eye1.Fill = Brushes.Black
            eye1.Stroke = Brushes.White
            eye2.Stroke = Brushes.White
            eye3.Stroke = Brushes.White
            eye3.Fill = Brushes.Black
            eye2.Fill = Brushes.Black
        ElseIf colorPaint = "Purple" Then
            eye1.Fill = Brushes.Purple
            eye3.Fill = Brushes.Purple
            eye2.Fill = Brushes.Purple
        ElseIf colorPaint = "Green" Then
            eye1.Fill = Brushes.Green
            eye2.Fill = Brushes.Green
            eye3.Fill = Brushes.Green
        End If
    End Sub

    'Aca regresa al menu principal donde se ponen los usuarios
    Public Sub click_Cambio(ByVal res As Object, ByVal e As EventArgs) Handles btnCambio.Click
        mainWindo.RegresarIngreso()
        mainWindo.Close()
        Me.Close()
    End Sub

End Class
