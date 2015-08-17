Public Class CUFichasBrown
    Inherits UserControl
    Public Color As String = "Brown"


    Public Sub fichas(ByVal sender As Object, e As EventArgs) Handles BrownPiece.MouseUp
        CUListener.sharedColor = "Brown"
    End Sub

End Class
