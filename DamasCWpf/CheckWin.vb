Public Class CheckWin
    Dim hilo_p As MainWindow
    Dim estadoColorin(120) As Boolean
    Dim allipses(120) As Ellipse
    Dim posicionFichas()() As Integer = New Integer(120)() {}
    Dim tipoColorEllipse(120) As String
    Dim objeto As Object

    'Constructor metodo new aca se pasan los atributos necesarios
    Public Sub New(ByRef hilo_Principal As MainWindow, ByRef estadoColor() As Boolean, ByRef allip() As Ellipse, ByRef posicionFichs()() As Integer, ByRef tipoC() As String)
        hilo_p = hilo_Principal
        estadoColorin = estadoColor
        allipses = allip
        posicionFichas = posicionFichs
        tipoColorEllipse = tipoC
    End Sub

    'Aca se envian los metodos para chequear a los ganadores de 3 
    Public Sub CheckWinAllPlayers3()
        AllCheck(55, 404, 4, "Purple", 15)
        AllCheck(327, 540, 4, "Blue", 15)
        AllCheck(327, 268, 4, "Yellow", 15)

    End Sub

    'Aca se envian los parametros para verificar las posiciones de los triangulos inversos o los normales
    Public Sub CheckWinAllPlayers6()
        AllCheck(55, 404, 3, "Purple", 10)
        AllCheck(361, 557, 3, "Blue", 10)
        AllCheck(361, 251, 3, "Yellow", 10)
        CheckPos6Inverse(91, 100, "Brown")
        CheckPos6Inverse(101, 110, "Black")
        CheckPos6Inverse(111, 120, "Green")
    End Sub


    'Aca se verifican que las posiciones especificas tengan o este llenas del color que se verifica
    Public Sub AllCheck(ByVal topi As Integer, ByVal lefit As Integer, ByVal limite As Integer, ByVal color As String, ByVal juego As Integer)
        Dim left As Double
        Dim contador As Integer = 0
        Dim contadorVerificador As Integer = 0
        Dim top As Double = topi
        Dim variable As Double = lefit
        For intX = limite To 0 Step -1
            For intY = 0 To limite - intX
                If contador < 15 Then
                    left = intY * 34
                    left = left + variable
                    If Verificador(left, top, color) = True Then
                        contadorVerificador = contadorVerificador + 1
                        If contadorVerificador = juego Then
                            hilo_p.FelicitarGanador(color)
                        End If
                    End If
                End If
                contador += 1
            Next
            top += 34
            variable = variable - 17
        Next
    End Sub


    'Este es el verificador de color, verifica por posicion y envia un color como parametro retornando un boolean
    Public Function Verificador(ByVal left As Integer, ByVal top As Integer, ByVal color As String) As Boolean
        For index2 = 0 To 120
            If posicionFichas(index2)(1) = left And posicionFichas(index2)(0) = top Then
                If tipoColorEllipse(index2) = color Then
                    Return True
                End If
            End If
        Next
        Return False
    End Function


    'Este metodo verifica los minitriangulos creados
    Public Sub CheckPos6Inverse(ByVal inicio As Integer, ByVal final As Integer, ByVal color As String)
        Dim contador As Integer = 0
        For index2 = inicio To final
            If Verificador(posicionFichas(index2)(1), posicionFichas(index2)(0), color) = True Then
                contador = contador + 1
            End If
        Next
        If contador = 10 Then
            hilo_p.FelicitarGanador(color)
        End If
    End Sub

End Class
