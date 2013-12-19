Public Class Form1

    Private botonador As New BotonesDinamicos

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        botonador.CrearBotonesPanel(Panel1, 24, 3)
    End Sub
End Class