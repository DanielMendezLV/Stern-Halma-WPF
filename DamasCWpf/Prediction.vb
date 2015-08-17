Public Class Prediction
    Dim hilo_p As MainWindow
    Dim estadoColorin(120) As Boolean
    Dim allipses(120) As Ellipse
    Dim posicionFichas()() As Integer = New Integer(120)() {}
    Dim objeto As Object

    'Metodo constructor de Prediction
    Public Sub New(ByRef hilo_Principal As MainWindow, ByRef estadoColor() As Boolean, ByRef allip() As Ellipse, ByRef posicionFichs()() As Integer)
        hilo_p = hilo_Principal
        estadoColorin = estadoColor
        allipses = allip
        posicionFichas = posicionFichs
    End Sub

    'Este metodo predice en linea izquierda
    Public Function PrediccionLineaIzquierda(ByVal left As Double, ByVal top As Double) As Boolean
        For index1 = 0 To 120
            If (left + 34) = posicionFichas(index1)(1) And top = posicionFichas(index1)(0) Then
                If (estadoColorin(index1)) = True Then
                    For index2 = 0 To 120
                        If (posicionFichas(index1)(1)) + 34 = posicionFichas(index2)(1) And posicionFichas(index1)(0) = posicionFichas(index2)(0) Then
                            If estadoColorin(index2) = False Then
                                If allipses(index2).Fill.ToString = "#FF808080" Then
                                    Return False
                                End If
                                allipses(index2).Fill = Brushes.Gray
                                Return True
                            End If
                        End If
                    Next
                End If
            End If
        Next
        Return False
    End Function


    'Este metodo predice y se reenvia a si mismo si las condiciones anteriores son verdaderas
    Public Sub PredictionAll(ByVal left As Double, ByVal top As Double)
        If PrediccionLineaDerecha(left, top) Then
            PredictionAll(left - 68, top)
        End If

        If PrediccionLineaIzquierda(left, top) Then
            PredictionAll(left + 68, top)
        End If

        If PrediccionLineaInferiorDerecho(left, top) Then
            PredictionAll(left - 34, top + 68)

        End If

        If PrediccionLineaInferiorIzquierdo(left, top) Then
            PredictionAll(left + 34, top + 68)
        End If


        If PrediccionLineaSuperiorDerecha(left, top) Then
            PredictionAll(left - 34, top - 68)
        End If

        If PrediccionLineaSuperiorIzquierda(left, top) Then
            PredictionAll(left + 34, top - 68)
        End If
    End Sub


    'Esta es la prediccion de la linea derecha
    Public Function PrediccionLineaDerecha(ByVal left As Double, ByVal top As Double) As Boolean
        For index1 = 0 To 120
            If (left - 34) = posicionFichas(index1)(1) And top = posicionFichas(index1)(0) Then
                If (estadoColorin(index1)) = True Then
                    For index2 = 0 To 120
                        If (posicionFichas(index1)(1)) - 34 = posicionFichas(index2)(1) And posicionFichas(index1)(0) = posicionFichas(index2)(0) Then
                            If estadoColorin(index2) = False Then
                                If allipses(index2).Fill.ToString = "#FF808080" Then
                                    Return False
                                End If
                                allipses(index2).Fill = Brushes.Gray
                                Return True
                            End If
                        End If
                    Next
                End If
            End If
        Next
        Return False
    End Function

    'Esta es la prediccion de la linea inferior derecho
    Public Function PrediccionLineaInferiorDerecho(ByVal left As Double, ByVal top As Double) As Boolean
        For index1 = 0 To 120
            If (left - 17) = posicionFichas(index1)(1) And top + 34 = posicionFichas(index1)(0) Then
                If (estadoColorin(index1)) = True Then
                    For index2 = 0 To 120
                        If (posicionFichas(index1)(1)) - 17 = posicionFichas(index2)(1) And posicionFichas(index1)(0) + 34 = posicionFichas(index2)(0) Then
                            If estadoColorin(index2) = False Then
                                If allipses(index2).Fill.ToString = "#FF808080" Then
                                    Return False
                                End If
                                allipses(index2).Fill = Brushes.Gray
                                Return True
                            End If
                        End If
                    Next
                End If
            End If
        Next
        Return False
    End Function

    'Esta es la prediccion de la linea inferior izquierda
    Public Function PrediccionLineaInferiorIzquierdo(ByVal left As Double, ByVal top As Double) As Boolean
        For index1 = 0 To 120
            If (left + 17) = posicionFichas(index1)(1) And top + 34 = posicionFichas(index1)(0) Then
                If (estadoColorin(index1)) = True Then
                    For index2 = 0 To 120
                        If (posicionFichas(index1)(1)) + 17 = posicionFichas(index2)(1) And posicionFichas(index1)(0) + 34 = posicionFichas(index2)(0) Then
                            If estadoColorin(index2) = False Then
                                If allipses(index2).Fill.ToString = "#FF808080" Then
                                    Return False
                                End If
                                allipses(index2).Fill = Brushes.Gray
                                Return True
                            End If
                        End If
                    Next
                End If
            End If
        Next
        Return False
    End Function

    'Esta es la prediccion de la linea superior derecha
    Public Function PrediccionLineaSuperiorDerecha(ByVal left As Double, ByVal top As Double) As Boolean
        For index1 = 0 To 120
            If (left - 17) = posicionFichas(index1)(1) And top - 34 = posicionFichas(index1)(0) Then
                If (estadoColorin(index1)) = True Then
                    For index2 = 0 To 120
                        If (posicionFichas(index1)(1)) - 17 = posicionFichas(index2)(1) And posicionFichas(index1)(0) - 34 = posicionFichas(index2)(0) Then
                            If estadoColorin(index2) = False Then
                                If allipses(index2).Fill.ToString = "#FF808080" Then
                                    Return False
                                End If
                                allipses(index2).Fill = Brushes.Gray
                                Return True
                            End If
                        End If
                    Next
                End If
            End If
        Next
        Return False
    End Function

    'Esta es la prediccion de la linea superior izquierda
    Public Function PrediccionLineaSuperiorIzquierda(ByVal left As Double, ByVal top As Double) As Boolean
        For index1 = 0 To 120
            If (left + 17) = posicionFichas(index1)(1) And top - 34 = posicionFichas(index1)(0) Then
                If (estadoColorin(index1)) = True Then
                    For index2 = 0 To 120
                        If (posicionFichas(index1)(1)) + 17 = posicionFichas(index2)(1) And posicionFichas(index1)(0) - 34 = posicionFichas(index2)(0) Then
                            If estadoColorin(index2) = False Then
                                If allipses(index2).Fill.ToString = "#FF808080" Then
                                    Return False
                                End If
                                allipses(index2).Fill = Brushes.Gray
                                Return True
                            End If
                        End If
                    Next
                End If
            End If
        Next
        Return False
    End Function

    'Esta es la prediccion normal al mover
    Public Sub NormalPrediction()
        For index1 = 0 To 120
            If (Canvas.GetLeft(GetChibolin) - 17) = posicionFichas(index1)(1) And (Canvas.GetTop(GetChibolin) + 34) = posicionFichas(index1)(0) Then
                allipses(index1).Fill = Brushes.Gray
            End If

            If (Canvas.GetLeft(GetChibolin) + 17) = posicionFichas(index1)(1) And (Canvas.GetTop(GetChibolin) + 34) = posicionFichas(index1)(0) Then
                allipses(index1).Fill = Brushes.Gray
            End If

            If (Canvas.GetLeft(GetChibolin) + 17) = posicionFichas(index1)(1) And (Canvas.GetTop(GetChibolin) - 34) = posicionFichas(index1)(0) Then
                allipses(index1).Fill = Brushes.Gray
            End If

            If (Canvas.GetLeft(GetChibolin) - 17) = posicionFichas(index1)(1) And (Canvas.GetTop(GetChibolin) - 34) = posicionFichas(index1)(0) Then
                allipses(index1).Fill = Brushes.Gray
            End If

            If (Canvas.GetLeft(GetChibolin) - 34) = posicionFichas(index1)(1) And (Canvas.GetTop(GetChibolin)) = posicionFichas(index1)(0) Then
                allipses(index1).Fill = Brushes.Gray
            End If

            If (Canvas.GetLeft(GetChibolin) + 34) = posicionFichas(index1)(1) And (Canvas.GetTop(GetChibolin)) = posicionFichas(index1)(0) Then
                allipses(index1).Fill = Brushes.Gray
            End If
        Next
    End Sub



    'Envia un chibolin o un control de usuario
    Public Sub SetChibolin(ByRef sender As Object)
        objeto = sender
    End Sub

    'Obtiene el chibolin o el control de usuario
    Public Function GetChibolin()
        Return objeto
    End Function
End Class
