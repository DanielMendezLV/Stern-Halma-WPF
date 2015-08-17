Public Class CUFichasBlack
    Inherits UserControl
    Public Color As String = "Black"

    Public Sub fichas(ByVal sender As Object, e As EventArgs) Handles BlackPiece.MouseUp
        CUListener.sharedColor = "Black"
    End Sub


End Class
