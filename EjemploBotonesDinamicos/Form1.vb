Public Class Form1

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim coordenadaX As Integer = 10
        Dim coordenadaY As Integer = 10
        'esto es un bucle en plan guarro, no salen bien alineados, pero porque lo he hecho muy rápido.
        For indice As Integer = 0 To 32
            'creamos el boton y le asignamos el evento click
            Dim botonDinamico As New Button
            AddHandler botonDinamico.Click, AddressOf Me.eventoBotonesDinamicos_Click
            'le damos tamaño y forma
            botonDinamico.Size = New Size(100, 30)
            botonDinamico.Top = coordenadaY
            botonDinamico.Left = coordenadaX

            'le damos un nombre para identificarlo
            botonDinamico.Name = "boton" & (indice + 1).ToString()
            'le damos a cada uno un texto
            botonDinamico.Text = "boton" & (indice + 1).ToString()

            'agregamos los botones dentro del panel

            coordenadaX = coordenadaX + 105
            'hacemos que cada 8 botones haya un salto de linea
            If indice Mod 8 = 0 Then
                coordenadaY = coordenadaY + 35
                coordenadaX = 10
            End If
            Me.Panel1.Controls.Add(botonDinamico)
        Next

    End Sub
    Private Sub eventoBotonesDinamicos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim botonClickado As Button = DirectCast(sender, Button)
        Dim indice As Integer = 0

        Panel1.Controls.Remove(botonClickado)
        botonClickado.Dispose()

        For Each boton As Button In Panel1.Controls
            'le damos un nombre para identificarlo
            boton.Name = "boton" & (indice + 1).ToString()
            'le damos a cada uno un texto
            boton.Text = "boton" & (indice + 1).ToString()
            indice += 1
        Next
    End Sub

End Class