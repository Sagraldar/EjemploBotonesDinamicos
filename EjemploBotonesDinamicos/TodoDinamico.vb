Public Class TodoDinamico

    Private Botonador As New BotonesDinamicos(Me)

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        '   LLAMO AL METODO QUE GENERA LOS BOTONES DINAMICOS EN EL PANEL
        Botonador.CrearBotonesPanel(Panel1, 9, 3, "Click", "eventoBotonesDinamicos_Click")
    End Sub

    '   EVENTO QUE NO ESTA ASIGNADO A NADIE EN PRINCIPIO, PERO SE ASIGNA EN TIEMPO DE EJECUCION
    '   NADA MAS CREARSE LOS BOTONES DINAMICAMENTE
    '   EL EVENTO ELIMINA EL BOTON PULSADO MEDIANTE UN DISPOSE Y LOS REORDENA
    '   FALTARIA UN METODO PARA ELIMINAR EL HANDLER DEL BOTON, PERO COMO NO LO VAMOS A USAR NO LO HE
    '   DESARROLLADO
    Private Sub eventoBotonesDinamicos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim botonClickado As Button = DirectCast(sender, Button)
        Dim indice As Integer = 0

        Panel1.Controls.Remove(botonClickado)
        botonClickado.Dispose()

        For Each boton As Button In Panel1.Controls
            boton.Name = "boton" & (indice + 1).ToString()
            boton.Text = "boton" & (indice + 1).ToString()
            indice += 1
        Next
    End Sub
End Class