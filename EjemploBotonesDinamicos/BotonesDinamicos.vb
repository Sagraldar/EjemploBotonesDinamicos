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
                                 ByVal nBotones As Short, ByVal nBotonesFila As Short)
        'esto es un bucle en plan guarro, no salen bien alineados, pero porque lo he hecho muy rápido.

        Dim margenIz As Integer = 2
        Dim MargenSup As Integer = 2

        Dim ancho As Short = (Contenedor.Width - nBotonesFila * 2) / nBotonesFila
        Dim alto As Short = (Contenedor.Height - nBotonesFila * 2) / nBotonesFila

        Dim fila As Short = 0

        For indice As Integer = 0 To nBOtones
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

            'hacemos que cada 8 botones haya un salto de linea
            If indice Mod nBotonesFila = 0 Then
                MargenSup = 2 * (fila + 1) + alto * fila
                margenIz = 2
                fila += 1
            Else
                margenIz += ancho + 2
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
