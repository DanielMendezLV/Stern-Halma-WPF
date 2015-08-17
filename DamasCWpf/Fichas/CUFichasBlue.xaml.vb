Public Class CUFichasBlue
    Inherits UserControl
    Public Color As String = "Blue"

    Public Sub fichas(ByVal sender As Object, e As EventArgs) Handles FichaBlue.MouseUp
        CUListener.sharedColor = "Blue"
    End Sub

End Class
