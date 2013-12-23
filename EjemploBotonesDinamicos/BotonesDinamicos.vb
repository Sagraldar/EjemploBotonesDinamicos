Public Class BotonesDinamicos

    Private Panel As Panel

    Public Sub New()
        Panel = New Panel
    End Sub

    Public Property PanelBotones() As Panel
        Get
            Return Panel
        End Get
        Set(ByVal value As Panel)
            Panel = value
        End Set
    End Property

    Public Sub CrearBotonesPanel(ByVal Contenedor As Control, _
                                 ByVal nBotones As Short, ByVal nColumnas As Short)
        'esto es un bucle en plan guarro, no salen bien alineados, pero porque lo he hecho muy rápido.

        Dim margenIz As Integer = 2
        Dim MargenSup As Integer = 2

        Dim nFilas As Short = (nBotones / nColumnas)

        Dim ancho As Short = (Contenedor.Width - nColumnas * 2 - 4) / nColumnas
        Dim alto As Short = (Contenedor.Height - nFilas * 2 - 4) / nFilas

        Dim fila As Short = 1
        Dim columna As Short = 1

        For indice As Integer = 0 To nBotones - 1
            'creamos el boton y le asignamos el evento click
            Dim boton As New Button
            AddHandler boton.Click, AddressOf Me.eventoBotonesDinamicos_Click
            'le damos tamaño y forma

            boton.Size = New Size(ancho, alto)

            boton.Top = MargenSup
            boton.Left = margenIz

            'le damos un nombre para identificarlo
            boton.Name = "boton" & (indice + 1).ToString()
            'le damos a cada uno un texto
            boton.Text = "boton" & (indice + 1).ToString()

            columna += 1
            margenIz += 2 + ancho

            If columna > nColumnas Then
                columna = 1
                fila += 1
                MargenSup += 2 + alto
                margenIz = 2
            End If

            '   AGREGAMOS LOS BOTONES DENTRO DEL CONTENEDOR
            Contenedor.Controls.Add(boton)
        Next
    End Sub

    Private Sub eventoBotonesDinamicos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim botonClickado As Button = DirectCast(sender, Button)
        Dim indice As Integer = 0

        Panel.Controls.Remove(botonClickado)
        botonClickado.Dispose()

        For Each boton As Button In Panel.Controls
            'le damos un nombre para identificarlo
            boton.Name = "boton" & (indice + 1).ToString()
            'le damos a cada uno un texto
            boton.Text = "boton" & (indice + 1).ToString()
            indice += 1
        Next
    End Sub
End Class
