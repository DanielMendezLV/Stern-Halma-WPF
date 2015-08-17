Public Class IngresoUsuariosWPF

    Dim txtMatriz() As TextBox
    Dim lblMatriz() As Label
    Dim blnTipo3 As Boolean
    Dim blnTipo6 As Boolean
    Dim blnAbrir As Boolean = False

    'Metodo constructor
    Public Sub New()
        ' Llamada necesaria para el diseñador.
        InitializeComponent()
        txtMatriz = New TextBox() {txtJugador1, txtJugador2, txtJugador3, txtJugador4, txtJugador5, txtJugador6}
        lblMatriz = New Label() {lblJugador1, lblJugador2, lblJugador3, lblJugador4, lblJugador5, lblJugador6}
        Me.Show()
        MakeInvisible(5)
        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
    End Sub

    'Sobrecarga del metodo constructor
    Public Sub New(ByVal txt() As TextBox, ByVal blnTipo6 As Boolean, ByVal blnTipo3 As Boolean)
        ' This call is required by the designer.
        InitializeComponent()
        txtMatriz = New TextBox() {txtJugador1, txtJugador2, txtJugador3, txtJugador4, txtJugador5, txtJugador6}
        lblMatriz = New Label() {lblJugador1, lblJugador2, lblJugador3, lblJugador4, lblJugador5, lblJugador6}
        SetTipoGame(blnTipo6, blnTipo3)
        MakeInvisible(5)
        If GetTipo6() Then
            MakeVisible(5)
        ElseIf GetTipo3() Then
            MakeVisible(2)
        End If
        Me.Show()
        For index = 0 To 5
            txtMatriz(index).Text = txt(index).Text
        Next
    End Sub


    'Este metodo los vuelve invisibles
    Public Sub MakeInvisible(ByVal limite As Integer)
        For index = 0 To limite
            txtMatriz(index).Visibility = True
            btn_Limpiar.Visibility = True
        Next
        For index = 0 To limite
            lblMatriz(index).Visibility = True
            btn_Limpiar.Visibility = True
        Next
    End Sub

    'Este metodo los vuelve visibles
    Public Sub MakeVisible(ByVal limite As Integer)
        For index = 0 To limite
            txtMatriz(index).Visibility = False
            btn_Limpiar.Visibility = False
        Next
        For indexs = 0 To limite
            lblMatriz(indexs).Visibility = False
            btn_Limpiar.Visibility = False
        Next
    End Sub

    'Este es el metodo del click al aceptar en el axml
    Public Sub ClickAceptar(ByVal objeto As Object, e As EventArgs) Handles btnAceptar.Click
        If SeeEmpty() = True Then
            Dim window As MainWindow = New MainWindow(txtMatriz)
            window.Show()
            If GetTipo6() Then
                'Me.MakeInvisible(True)
                window.Load6Players()
                IClose()
            ElseIf GetTipo3() Then
                'Me.MakeInvisible(True)
                window.Load3Players()
                IClose()
            End If
        End If
    End Sub

    'Este metodo comprueba los vacios
    Public Function SeeEmpty() As Boolean
        'Tester() 'Activar para ahorrar tiempo al programador
        If GetTipo3() = True Then
            If txtJugador1.Text <> "" And txtJugador2.Text <> "" And txtJugador3.Text <> "" Then
                Return True
            End If
            MsgBox("Empty Spaces", MsgBoxStyle.Critical)
        End If
        If GetTipo6() Then
            If txtJugador1.Text <> "" And txtJugador2.Text <> "" And txtJugador3.Text <> "" And txtJugador4.Text <> "" And txtJugador5.Text <> "" And txtJugador6.Text <> "" Then
                Return True
            End If
            MsgBox("Empty Spaces", MsgBoxStyle.Critical)
        End If
        Return False
    End Function

    'Metodo de clickeo al hacer click en el button 3
    Public Sub ClickButton3Players(ByVal objecto As Object, e As EventArgs) Handles btn3Jugadores.Click
        MakeInvisible(5)
        MakeVisible(2)
        SetTipoGame(False, True)
    End Sub

    'Metodo de clickeo al hacer click en el button 6
    Public Sub ClickButton6Players(ByVal objecto As Object, e As EventArgs) Handles btn6Jugadores.Click
        MakeInvisible(5)
        MakeVisible(5)
        SetTipoGame(True, False)
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

    'Aqui se cierra la ventana creada
    Public Sub IClose()
        Me.Close()
    End Sub

    'Aca se limpian los txt del menu pricipal
    Public Sub Click_BtnClear() Handles btn_Limpiar.Click
        For index = 0 To UBound(txtMatriz)
            txtMatriz(index).Text = ""
        Next
    End Sub


    'Metodo para ahorrar trabajo al programador
    Public Sub Tester()
        For index = 0 To UBound(txtMatriz)
            txtMatriz(index).Text = "..."
        Next
    End Sub
End Class
