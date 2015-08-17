Class MainWindow
    'En esta seccion se declaran las variables y matrices necesarias para el funcionamiento
    Dim posicionFichas()() As Integer = New Integer(120)() {}
    Dim fichasNegras() As CUFichasBlack = New CUFichasBlack(9) {}
    Dim fichasVerdes() As CUFichasVerdes = New CUFichasVerdes(9) {}
    Dim fichasCafes() As CUFichasBrown = New CUFichasBrown(9) {}
    Dim fichasYellow() As CUFichasYellow = New CUFichasYellow(12) {}
    Dim estadoColorin(120) As Boolean
    Dim allipses(120) As Ellipse
    Dim tipoColorEllipse(120) As String
    Dim estadoPos As Boolean = False
    Dim objeto As Object
    Dim blnEstados() As Boolean = New Boolean(5) {True, False, False, False, False, False}
    Dim blnEstados3() As Boolean = New Boolean(2) {True, False, False}
    Dim ruletaTurno As Integer = 0
    Dim color() As String = New String() {"Green", "Blue", "Black", "Purple", "Brown", "Yellow"}
    Dim color3() As String = New String() {"Blue", "Purple", "Yellow"}
    Dim contadores3(2) As Integer
    Dim contadores6(5) As Integer
    Dim blnTipo6 As Boolean
    Dim blnTipo3 As Boolean
    Dim prediccionClass As Prediction = New Prediction(Me, estadoColorin, allipses, posicionFichas)
    Dim winClass As CheckWin = New CheckWin(Me, estadoColorin, allipses, posicionFichas, tipoColorEllipse)
    Dim jugadores() As TextBox
    Dim listRectangles() As Rectangle

    'Metodo principal del mainwindow
    Public Sub New(jugador())
        ' Llamada necesaria para el diseñador.
        InitializeComponent()
        jugadores = jugador
        listRectangles = New Rectangle() {rctIndicador1,rctIndicador2,rctIndicador3,rctIndicador4}
    End Sub



    'Aca se crean las fichas negras
    Public Sub CreateBlackPieces()
        For index = 0 To 9
            Dim ficha = New CUFichasBlack
            fichasNegras(index) = ficha
        Next
    End Sub

    'Aca se crean las fichas Cafes
    Public Sub CreateBrownPieces()
        For index = 0 To 9
            Dim ficha = New CUFichasBrown
            fichasCafes(index) = ficha
        Next
    End Sub

    'Aca se crean las fichas verdes
    Public Sub CreateGreenPieces()
        For index = 0 To 9
            Dim ficha = New CUFichasVerdes
            fichasVerdes(index) = ficha
        Next
    End Sub

    'Este metodo manda a llamar a las ruletas del turno
    Public Sub MoveRuletas()
        If getTipo6() = True Then
            Ruleta6()
        Else
            Ruleta3()
        End If
    End Sub



    'Meotod mouse Upp Es cuando se manda a llamar a la primera prediccion
    Public Sub MouseUpEllipse(ByVal sender As Object, e As MouseButtonEventArgs)
        'For index = 0 To 120
        '    If sender Is allipses(index) Then
        '        MsgBox(index)
        '    End If
        'Next
            If Not (GetChibolin() Is Nothing) Then
                Move(sender)
            End If
    End Sub

    'Metodo que se da al clickear los controles de usuario o fichas
    Public Sub ClickControlUser(ByVal sender As Object, e As EventArgs)
        VolverNormales()
        SetChibolin(sender)
        Prediccion(sender)
    End Sub

    'Este metodo mueve los controles de Usuario
    Public Sub Move(ByVal sender As Object)
        Dim blnpresionado As Boolean = CheckColorMouseDown()
        If blnpresionado = True Then
            For index1 = 0 To 120
                If allipses(index1) Is sender Then
                    If allipses(index1).Fill.ToString = "#FF808080" Then
                        CambiarEstadoChibolin(False, GetChibolin())
                        CambiarColorChibolin("White", GetChibolin())
                        SendPosCanvas(GetChibolin(), posicionFichas(index1)(1), posicionFichas(index1)(0))
                        CambiarEstadoChibolin(True, GetChibolin())
                        CambiarColorChibolin(CUListener.sharedColor, GetChibolin())
                        SumarContadores(CUListener.sharedColor)
                        If GetTipo3() = True Then
                            winClass.CheckWinAllPlayers3()
                        Else
                            winClass.CheckWinAllPlayers6()
                        End If

                        SetChibolin(Nothing)
                        CUListener.blnMove = True
                        MoveRuletas()
                        VolverNormales()
                        btnModoJuego.IsEnabled = False
                    Else
                        Shake()
                        allipses(index1).Fill = Brushes.DarkRed
                        Shake()
                    End If
                End If
            Next
        End If
    End Sub

    'Este metodo suma los movimientos
    Public Sub SumarContadores(ByVal colorin As String)
        If GetTipo3() = True Then
            For index = 0 To UBound(color3)
                If color3(index) = (colorin) Then
                    contadores3(index) = contadores3(index) + 1
                End If
            Next
        Else
            For index = 0 To UBound(color)
                If color(index) = (colorin) Then
                    contadores6(index) = contadores6(index) + 1
                End If
            Next
        End If
    End Sub

    'Este es el contador especifico de los colores que permite ver si el color es el colorin
    Public Function GetContadorEspecifico(ByVal colorin As String) As Integer
        If GetTipo3() = True Then
            For index = 0 To UBound(color3)
                If color3(index) = (colorin) Then
                    Return contadores3(index)
                End If
            Next
        Else
            For index = 0 To UBound(color)
                If color(index) = (colorin) Then
                    Return contadores6(index)
                End If
            Next
        End If
        Return -1
    End Function

    'EL primero indica el valor si el caso es true o es false si es true quiere decir que esta ocupado, si es false que no
    'Si el tipo de llamado es false = se esta moviendo
    'SI el tipo de llamado es true = posicionamiento de fichas
    Public Sub CambiarEstadoChibolin(ByVal caso As Boolean, ByVal objeto As Object)
        For index2 = 0 To 120
            If posicionFichas(index2)(1) = Canvas.GetLeft(objeto) And posicionFichas(index2)(0) = Canvas.GetTop(objeto) Then
                estadoColorin(index2) = caso
            End If
        Next
    End Sub


    'Este cambia el color del chibolin al moverlo
    Public Sub CambiarColorChibolin(ByVal color As String, ByVal objeto As Object)
        For index2 = 0 To 120
            If posicionFichas(index2)(1) = Canvas.GetLeft(objeto) And posicionFichas(index2)(0) = Canvas.GetTop(objeto) Then
                tipoColorEllipse(index2) = color
            End If
        Next
    End Sub

    'Este metodo es la prediccion
    Public Sub Prediccion(ByVal ob As Object)
        Dim blnpresionado As Boolean = CheckColorMouseDown()
        If blnpresionado = True Then
            prediccionClass.SetChibolin(GetChibolin())
            prediccionClass.NormalPrediction()
            prediccionClass.PredictionAll(Canvas.GetLeft(GetChibolin), (Canvas.GetTop(GetChibolin)))
        Else
            MsgBox("No es su turno")
        End If
    End Sub

   

    'Este metodo chequea que el color al que le toca sea el correcto
    Public Function CheckColorMouseDown() As Boolean
        If GetTipo6() = True Then
            For index = 0 To UBound(color)
                If color(index) = CUListener.sharedColor Then
                    If blnEstados(index) = True Then
                        Return True
                    End If
                End If
            Next
        End If

        If GetTipo3() = True Then
            For index = 0 To UBound(color3)
                If color3(index) = CUListener.sharedColor Then
                    If blnEstados3(index) = True Then
                        Return True
                    End If
                End If
            Next
        End If
        Return False
    End Function

    'Esta es la ruleta para 6 jugadores
    Public Sub Ruleta6()
        If ruletaTurno < 5 Then
            blnEstados(ruletaTurno) = False
            ruletaTurno = ruletaTurno + 1
            blnEstados(ruletaTurno) = True
            ChangeColor()
            Return
        End If
        If ruletaTurno = 5 Then
            blnEstados(ruletaTurno) = False
            ruletaTurno = 0
            blnEstados(ruletaTurno) = True
            ChangeColor()
        End If
    End Sub

    'Esta es la ruleta para 3 jugadores
    Public Sub Ruleta3()
        If ruletaTurno < 2 Then
            blnEstados3(ruletaTurno) = False
            ruletaTurno = ruletaTurno + 1
            blnEstados3(ruletaTurno) = True
            ChangeColor()
            Return
        End If
        If ruletaTurno = 2 Then
            blnEstados3(ruletaTurno) = False
            ruletaTurno = 0
            blnEstados3(ruletaTurno) = True
            ChangeColor()
        End If
    End Sub

    'Aca cambia el color de los rectanble es decir los margenes
    Public Sub ColorSee(ByVal Color() As String, ByVal estadosColor() As Boolean)
        lblColor.Content = "Color: "
        lblJugador.Content = "Jugador: "
        lblMoves.Content = "Moves: "
        For index = 0 To UBound(Color)
            If estadosColor(index) = True Then
                If Color(index) = "Black" Then
                    For index2 = 0 To UBound(listRectangles)
                        listRectangles(index2).Fill = Brushes.Black
                    Next
                    lblColor.Content = lblColor.Content + "Black"
                    lblJugador.Content = lblJugador.Content + jugadores(index).Text
                    lblMoves.Content = lblMoves.Content & GetContadorEspecifico("Black")
                End If
                If Color(index) = "Green" Then
                    For index2 = 0 To UBound(listRectangles)
                        listRectangles(index2).Fill = Brushes.Green
                    Next
                    lblColor.Content = lblColor.Content + "Green"
                    lblJugador.Content = lblJugador.Content + jugadores(index).Text
                    lblMoves.Content = lblMoves.Content & GetContadorEspecifico("Green")
                End If
                If Color(index) = "Brown" Then
                    For index2 = 0 To UBound(listRectangles)
                        listRectangles(index2).Fill = Brushes.Brown
                    Next
                    lblColor.Content = lblColor.Content + "Brown"
                    lblJugador.Content = lblJugador.Content + jugadores(index).Text
                    lblMoves.Content = lblMoves.Content & GetContadorEspecifico("Brown")
                End If
                If Color(index) = "Yellow" Then
                    ''    indicadores(index1).Fill = Brushes.Yellow
                    For index2 = 0 To UBound(listRectangles)
                        listRectangles(index2).Fill = Brushes.Yellow
                    Next
                    lblColor.Content = lblColor.Content + "Yellow"
                    lblJugador.Content = lblJugador.Content + jugadores(index).Text
                    lblMoves.Content = lblMoves.Content & GetContadorEspecifico("Yellow")
                End If
                If Color(index) = "Blue" Then
                    For index2 = 0 To UBound(listRectangles)
                        listRectangles(index2).Fill = Brushes.Blue
                    Next
                    lblColor.Content = lblColor.Content + "Blue"
                    lblJugador.Content = lblJugador.Content + jugadores(index).Text
                    lblMoves.Content = lblMoves.Content & GetContadorEspecifico("Blue")
                End If
                If Color(index) = "Purple" Then
                    For index2 = 0 To UBound(listRectangles)
                        listRectangles(index2).Fill = Brushes.Purple
                    Next
                    lblColor.Content = lblColor.Content + "Purple"
                    lblJugador.Content = lblJugador.Content + jugadores(index).Text
                    lblMoves.Content = lblMoves.Content & GetContadorEspecifico("Purple")
                End If
            End If
        Next
    End Sub

    'Aqui manda a llamar al color del 6 o del 3
    Public Sub ChangeColor()
        If GetTipo6() = True Then
            ColorSee(color, blnEstados)
        Else
            ColorSee(color3, blnEstados3)
        End If
    End Sub

    'Vuelve todos los blancos despues de la prediccion normales
    Public Sub VolverNormales()
        For index1 = 0 To 120
            allipses(index1).Fill = Brushes.White
        Next
    End Sub


    'Carga el tablero
    Public Sub LoadBoard()
        Dim left As Double
        Dim contador As Integer = 0
        Dim top As Double = 55
        Dim variable As Double = 404
        For intX = 12 To 0 Step -1
            For intY = 0 To 12 - intX
                Dim ellipses As New Ellipse
                ellipses.Fill = Brushes.White
                ellipses.Stroke = Brushes.Black
                ellipses.Width = 34
                ellipses.Height = 34
                left = intY * 34
                left = left + variable
                Canvas.SetLeft(ellipses, left)
                Canvas.SetTop(ellipses, top)
                estadoColorin(contador) = False
                tipoColorEllipse(contador) = "White"
                posicionFichas(contador) = (New Integer() {top, left})
                allipses(contador) = ellipses
                contador += 1
                AddHandler ellipses.MouseUp, AddressOf MouseUpEllipse
                Board.Children.Add(ellipses)
            Next
            top += 34
            variable = variable - 17
        Next
        Especificos()
    End Sub

    'Crea la otra parte del tablero
    Public Sub CrearOtraParteBoard(ByVal posX As Double, ByVal posY As Double, ByVal blnEstado As Boolean, ByVal blnSigno As Boolean, _
        ByVal cuenta As Integer, ByVal estadoBoard As Boolean, ByVal color As Char)
        Dim top As Double = 191
        Dim variable As Double = 200
        Dim left As Double

        For intX = 0 To 12
            For IntY = 0 To 12 - intX
                If blnEstado = True Then
                    If intX = posX Then
                        If blnSigno = True Then
                            If IntY < posY Then
                                left = IntY * 34
                                left = left + variable
                                If estadoBoard = True Then
                                    PutTablero(left, top, cuenta)
                                    cuenta = cuenta + 1
                                Else
                                    CreateAndPut(left, top, color)
                                End If
                            End If
                        Else
                            If IntY > posY Then
                                left = IntY * 34
                                left = left + variable
                                If estadoBoard = True Then
                                    PutTablero(left, top, cuenta)
                                    cuenta = cuenta + 1
                                Else
                                    CreateAndPut(left, top, color)
                                End If

                            End If
                        End If
                    End If
                Else
                    If intX = posX Then
                        If IntY = posY Then
                            left = IntY * 34
                            left = left + variable
                            If estadoBoard = True Then
                                PutTablero(left, top, cuenta)
                                cuenta = cuenta + 1
                            Else
                                CreateAndPut(left, top, color)
                            End If

                        End If
                    End If
                End If
            Next
            variable = variable + 17
            top += 34
        Next
    End Sub


    'Crea el especifico del tablero
    Public Sub Especificos()
        CrearOtraParteBoard(0, 4, True, True, 91, True, "N")
        CrearOtraParteBoard(1, 3, True, True, 95, True, "N")
        CrearOtraParteBoard(2, 2, True, True, 98, True, "N")
        CrearOtraParteBoard(3, 0, False, True, 100, True, "N")
        CrearOtraParteBoard(0, 8, True, False, 101, True, "N")
        CrearOtraParteBoard(1, 8, True, False, 105, True, "N")
        CrearOtraParteBoard(2, 8, True, False, 108, True, "N")
        CrearOtraParteBoard(3, 8, True, False, 110, True, "N")
        CrearOtraParteBoard(9, 4, True, True, 111, True, "N")
        CrearOtraParteBoard(10, 3, True, True, 115, True, "N")
        CrearOtraParteBoard(11, 2, True, True, 118, True, "N")
        CrearOtraParteBoard(12, 0, False, True, 120, True, "N")
    End Sub


    'Carga las fichas verdes
    Public Sub LoadFichasVerdes()
        Dim top As Double = 55
        Dim variable As Double = 404
        Dim left As Double
        Dim contador As Integer = 0
        For intX = 3 To 0 Step -1
            For intY = 0 To 3 - intX
                AddHandler fichasVerdes(contador).MouseUp, AddressOf ClickControlUser
                left = intY * 34
                left = left + variable
                Board.Children.Remove(fichasVerdes(contador))
                Canvas.SetLeft(fichasVerdes(contador), left)
                Canvas.SetTop(fichasVerdes(contador), top)
                CambiarEstadoChibolin(True, fichasVerdes(contador))
                CambiarColorChibolin("Green", fichasVerdes(contador))
                Board.Children.Add(fichasVerdes(contador))
                contador = contador + 1
            Next
            top += 34
            variable = variable - 17
        Next
    End Sub

    'Coloca en el tablero 
    Public Sub PutTablero(ByVal left As Double, ByVal top As Double, ByVal cuenta As Integer)
        Dim ellipses As New Ellipse
        ellipses.Fill = Brushes.White
        ellipses.Stroke = Brushes.Black
        ellipses.Width = 34
        ellipses.Height = 34
        estadoColorin(cuenta) = False
        tipoColorEllipse(cuenta) = "White"
        posicionFichas(cuenta) = (New Integer() {top, left})
        allipses(cuenta) = ellipses
        Canvas.SetLeft(ellipses, left)
        Canvas.SetTop(ellipses, top)
        Board.Children.Add(ellipses)
        AddHandler ellipses.MouseUp, AddressOf MouseUpEllipse
    End Sub

    'Crea las fichas del color especifico
    Public Sub CreateAndPut(ByVal left As Double, ByVal top As Double, ByVal color As Char)
        If color = "B" Then
            Dim ficha As New CUFichasBlue
            Canvas.SetLeft(ficha, left)
            Canvas.SetTop(ficha, top)
            Board.Children.Add(ficha)
            CambiarEstadoChibolin(True, ficha)
            CambiarColorChibolin("Blue", ficha)
            AddHandler ficha.MouseUp, AddressOf ClickControlUser
        ElseIf color = "Y" Then
            Dim ficha As New CUFichasYellow
            Canvas.SetLeft(ficha, left)
            Canvas.SetTop(ficha, top)
            Board.Children.Add(ficha)
            CambiarEstadoChibolin(True, ficha)
            CambiarColorChibolin("Yellow", ficha)
            AddHandler ficha.MouseUp, AddressOf ClickControlUser
        ElseIf color = "P" Then
            Dim ficha As New CUFichasPurple
            Canvas.SetLeft(ficha, left)
            Canvas.SetTop(ficha, top)
            Board.Children.Add(ficha)
            CambiarColorChibolin("Purple", ficha)
            CambiarEstadoChibolin(True, ficha)
            AddHandler ficha.MouseUp, AddressOf ClickControlUser
        End If
    End Sub

    'Este metodo crea las fichas negras y cafes
    Public Sub LoadBlackAndBrownPieces(ByVal tipo As Char, ByVal cantidad As Double, ByVal tope As Double)
        Dim top As Double = tope
        Dim variable As Double = cantidad
        Dim left As Double
        Dim contador As Integer = 0

        For IntX = 3 To 0 Step -1
            For intY = 0 To 3 - IntX
                left = intY * 34
                left = left + variable
                CreateInverso1(intY, left, variable, tipo, top, contador)
                contador = contador + 1
            Next
            top += 34
            variable = variable - 17
        Next
    End Sub


    'Este crea el triangulo inverso
    Public Sub CreateInverso1(ByVal intY As Double, ByVal left As Double, ByVal variable As Double, ByVal codigo As Char, ByVal top As Double, ByVal index As Integer)
        If codigo = "L" Then
            AddHandler fichasNegras(index).MouseUp, AddressOf ClickControlUser
            Board.Children.Remove(fichasNegras(index))
            Canvas.SetLeft(fichasNegras(index), left)
            Canvas.SetTop(fichasNegras(index), top)
            Board.Children.Add(fichasNegras(index))
            CambiarEstadoChibolin(True, fichasNegras(index))
            CambiarColorChibolin("Black", fichasNegras(index))
        End If
        If codigo = "C" Then
            AddHandler fichasCafes(index).MouseUp, AddressOf ClickControlUser
            Canvas.SetLeft(fichasCafes(index), left)
            Canvas.SetTop(fichasCafes(index), top)
            Board.Children.Add(fichasCafes(index))
            CambiarEstadoChibolin(True, fichasCafes(index))
            CambiarColorChibolin("Brown", fichasCafes(index))
        End If
    End Sub


    'Carga las piezas amarillas
    Public Sub LoadYellowPieces(ByVal tipo3 As Boolean, ByVal limite As Integer)
        CrearOtraParteBoard(0, limite, True, False, 0, False, "Y")
        CrearOtraParteBoard(1, limite, True, False, 0, False, "Y")
        CrearOtraParteBoard(2, limite, True, False, 0, False, "Y")
        CrearOtraParteBoard(3, limite, True, False, 0, False, "Y")
        If tipo3 = False Then
            CrearOtraParteBoard(4, 8, False, False, 0, False, "Y")
        End If
    End Sub


    'Carga las piezas purpuras
    Public Sub LoadPurplePieces(ByVal tipo3 As Boolean)
        CrearOtraParteBoard(9, 4, True, True, 0, False, "P")
        CrearOtraParteBoard(10, 3, True, True, 0, False, "P")
        CrearOtraParteBoard(11, 2, True, True, 0, False, "P")
        CrearOtraParteBoard(12, 0, False, True, 0, False, "P")
        If tipo3 = False Then
            CrearOtraParteBoard(8, 5, True, True, 0, False, "P")
        End If
    End Sub

    'Carga las piezas azules dependiendo el tipo
    Public Sub LoadBluePieces(ByVal tipo3 As Boolean)
        If tipo3 = True Then
            CrearOtraParteBoard(0, 4, True, True, 0, False, "B")
            CrearOtraParteBoard(1, 3, True, True, 0, False, "B")
            CrearOtraParteBoard(2, 2, True, True, 0, False, "B")
            CrearOtraParteBoard(3, 0, False, True, 0, False, "B")
        Else
            CrearOtraParteBoard(0, 5, True, True, 0, False, "B")
            CrearOtraParteBoard(1, 4, True, True, 0, False, "B")
            CrearOtraParteBoard(2, 3, True, True, 0, False, "B")
            CrearOtraParteBoard(3, 2, True, True, 0, False, "B")
            CrearOtraParteBoard(4, 0, False, True, 0, False, "B")
        End If
    End Sub

    'Aca se pinta al poner sobre el rojo el borde
    Public Sub MouserEnter(ByVal sender As Button, e As EventArgs)
        sender.BorderBrush = Brushes.Red
    End Sub

    'Vuelve a ser normal el rojo
    Public Sub MouserLeaver(ByVal sender As Button, e As EventArgs)
        sender.BorderBrush = Brushes.Transparent
    End Sub


    'Carga el modo de 6 jugadores
    Public Sub Load6Players()
        CUListener.blnMove = False
        LoadBoard()
        LoadFichasVerdes()
        LoadBluePieces(True)
        LoadYellowPieces(True, 8)
        LoadPurplePieces(True)
        LoadBlackAndBrownPieces("L", 251, 361)
        LoadBlackAndBrownPieces("C", 557, 361)
        SetTipoGame(True, False)
        ruletaTurno = 0
        For index = 0 To 5
            If index = 0 Then
                blnEstados(index) = True
            Else
                blnEstados(index) = False
            End If
        Next
        ChangeColor()
    End Sub


    'Inicializa el canvas
    Public Sub InitializedCanvas() Handles Board.Initialized
        CreateBlackPieces()
        CreateGreenPieces()
        CreateBrownPieces()
    End Sub

    'Carga el modo de 3 jugadores
    Public Sub Load3Players()
        CUListener.blnMove = False
        LoadBoard()
        LoadYellowPieces(False, 7)
        LoadBluePieces(False)
        LoadPurplePieces(False)
        SetTipoGame(False, True)
        ruletaTurno = 0
        For index = 0 To 2
            If index = 0 Then
                blnEstados3(index) = True
            Else
                blnEstados3(index) = False
            End If
        Next
        ChangeColor()
    End Sub


    'Envia un chibolin o un control de usuario
    Public Sub SetChibolin(ByRef sender As Object)
        objeto = sender
    End Sub

    'Obtiene el chibolin o el control de usuario
    Public Function GetChibolin()
        Return objeto
    End Function

    'Envia los objecto a determinada posicion
    Public Sub SendPosCanvas(ByVal sender As Object, ByVal left As Double, ByVal top As Double)
        Canvas.SetLeft(sender, left)
        Canvas.SetTop(sender, top)
        Board.Children.Remove(sender)
        Board.Children.Add(sender)
    End Sub

    'Resetea el juego y los controles
    Public Sub Reset() Handles btnResetear.Click
        CUListener.blnMove = False
        Board.Children.Clear()
        Board.Children.Add(Rectangulo)
        Board.Children.Add(EllipsePrincipal)
        Board.Children.Add(btnResetear)
        Board.Children.Add(rctIndicador1)
        Board.Children.Add(rctIndicador2)
        Board.Children.Add(rctIndicador3)
        Board.Children.Add(rctIndicador4)
        Board.Children.Add(btnModoJuego)
        Board.Children.Add(lblColor)
        Board.Children.Add(lblJugador)
        Board.Children.Add(Btn_Maximized)
        Board.Children.Add(BtnClose)
        Board.Children.Add(BtnMinimalist)
        Board.Children.Add(lblMoves)
        btnModoJuego.IsEnabled = True
        ruletaTurno = 0
        If GetTipo3() = True Then
            Load3Players()
        Else
            Load6Players()
        End If
    End Sub

    'Este metodo es el que se utiliza antes d emover para poder cambiarlo
    Public Sub ChangeModo() Handles btnModoJuego.Click
        ' MsgBox(" " & CUListener.blnMove)
        If CUListener.blnMove = True Then
            MsgBox("Ya has realizado el primer movimiento")
        Else
            RegresarIngreso()
            IClose()
        End If
    End Sub

    'Este regresa a la ventana de ingreso
    Public Sub RegresarIngreso()
        Dim ingresos As IngresoUsuariosWPF = New IngresoUsuariosWPF(jugadores, GetTipo6(), GetTipo3())
        ingresos.Show()
    End Sub

    'Este metodo me cierra
    Public Sub IClose()
        Me.Close()
    End Sub

    'Envia el tipo de juego
    Public Sub SetTipoGame(ByVal blnTip6, ByVal blnTip3)
        blnTipo6 = blnTip6
        blnTipo3 = blnTip3
    End Sub

    'Retorna el tipo de juego
    Public Function GetTipo3() As Boolean
        Return blnTipo3
    End Function

    'Retorna el tipo de juego 6
    Public Function GetTipo6() As Boolean
        Return blnTipo6
    End Function

    'Este metodo hace que vibre el window cuando se confunda
    Public Sub Shake()
        For cantidad_Shack = 0 To 5
            Me.Left = Me.Left + 5
            Me.Top = Me.Top + 5
            Me.Left = Me.Left - 5
            Me.Top = Me.Top - 5

        Next
    End Sub


    'Permite el Drag
    Private Sub grid_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs) Handles grid.MouseLeftButtonDown
        'Me.DragMove()
    End Sub

    'Con este metodo me cierro
    Private Sub BtnClose_Click(sender As Object, e As RoutedEventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub

    'Con este metodo me minimizo
    Private Sub BtnMinimalist_Click(sender As Object, e As RoutedEventArgs) Handles BtnMinimalist.Click
        Me.WindowState = Windows.WindowState.Minimized
    End Sub

    'Con este metodo me maximizo
    Private Sub BtnMaximized_Click(sender As Object, e As RoutedEventArgs) Handles Btn_Maximized.Click
        'Me.WindowState = Windows.WindowState.Maximized
    End Sub

    'Este metodo felicita al ganador
    Public Sub FelicitarGanador(ByVal colorin As String)
        If GetTipo3() Then
            For index = 0 To UBound(color3)
                If color3(index) = colorin Then
                    Dim WhatDo = New WhatDo(Me, jugadores(index).Text, colorin, contadores3(index))
                    WhatDo.Show()
                End If
            Next
        Else
            For index = 0 To UBound(color)
                If color(index) = colorin Then
                    Dim WhatDo = New WhatDo(Me, jugadores(index).Text, colorin, contadores6(index))
                    WhatDo.Show()
                End If
            Next
        End If
    End Sub
End Class
