Public Class CUFichasYellow
    Inherits UserControl
    Public Color As String = "Yellow"


    Public Sub fichas(ByVal sender As Object, e As EventArgs) Handles YellowPiece.MouseUp
        CUListener.sharedColor = "Yellow"
    End Sub

End Class
