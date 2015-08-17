Public Class CUListener
    Private Shared colorCode As String
    Private Shared blnMoves As Boolean
    'Propiedad que comparte el color de la ficha clickeada
    Shared Property sharedColor() As String
        Get
            'sólo puede hacer referencia a variables compartidas
            Return colorCode           'Error, no se puede tener acceso a Me.myStr
        End Get
        Set(ByVal Value As String)
            colorCode = Value
        End Set
    End Property

    'Esta propiedad ve si ya se movio o hizo el primer movimiento
    Shared Property blnMove() As Boolean
        Get
            Return blnMoves
        End Get
        Set(value As Boolean)
            blnMoves = blnMove
        End Set
    End Property

End Class
