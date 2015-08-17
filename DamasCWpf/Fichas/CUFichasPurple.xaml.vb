Public Class CUFichasPurple
    Inherits UserControl
    Public Color As String = "Purple"


    Public Sub fichas(ByVal sender As Object, e As EventArgs) Handles PurplePiece.MouseUp
        CUListener.sharedColor = "Purple"
    End Sub

End Class
