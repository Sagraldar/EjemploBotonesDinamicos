Imports System.Reflection

Public Class BotonesDinamicos

    Private Formulario As Form

    Public Sub New(ByVal Form As Form)
        Formulario = Form
    End Sub

    Public Sub CrearBotonesPanel(ByVal Contenedor As Control, _
                                 ByVal nBotones As Short, ByVal nColumnas As Short, _
                                 ByVal TipoEvento As String, ByVal Evento As String)

        Dim margenIz As Integer = 2
        Dim MargenSup As Integer = 2

        Dim nFilas As Short = (nBotones / nColumnas)

        Dim ancho As Short = (Contenedor.Width - nColumnas * 2 - 4) / nColumnas
        Dim alto As Short = (Contenedor.Height - nFilas * 2 - 4) / nFilas

        '   SOLUCIONO EL PROBLEMA DEL ESTILO DECLARANDO UNA FILA Y UNA COLUMNA QUE VOY CONTROLANDO EN EL BUCLE
        Dim fila As Short = 1
        Dim columna As Short = 1

        For indice As Integer = 0 To nBotones - 1
            '   CREAMOS EL BOTON
            Dim boton As New Button

            '   HE AQUI EL PROBLEMA PRINCIPAL, YA QUE EN EL ADDRESSOF NO ERA CAPAZ DE LLAMAR MAS QUE A UN EVENTO CONCRETO,
            '   LO QUE SUPONIA TENER QUE CREAR EL EVENTO ESPECIFICO Y PONERLO EN EL CODIGO A PINCHO, Y YA SABES COMO 
            '   ODIO HACERLO A PINCHO. YO QUERIA ELEGANCIA! Y ME DIJE, QUIZA REFLECTION ME PERMITA HACER QUE EL OBJETO
            '   APUNTE AL EVENTO DE FORMA DINAMICA, SIN MAYOR PROBLEMA Y PUDIENDO DECIRLE CON UN PARAMETRO STRING
            '   EL NOMBRE DEL METODO Y SU TIPO. ESTO AHORRARIA TIEMPO Y SE PODRIAN AGREGAR EVENTOS EN TIEMPO DE EJECUCION
            '
            'AddHandler boton.Click, AddressOf Me.eventoBotonesDinamicos_Click
            '   EQUILICUA, ESTE ES EL METODO!! LE DIGO EN QUE FORM ESTA DECLARADO EL EVENTO, CUAL ES EL OBJETO QUE APUNTARA A EL
            '   QUE TIPO DE EVENTO ES (CLICK, LOAD, ETC) Y EL NOMBRE DEL EVENTO. MAS FACIL IMPOSIBLE, CABALLERO

            DinamicAddHandler(Formulario, boton, TipoEvento, Evento)

            '   LE DAMOS PROPIEDADES AL BOTON
            boton.Size = New Size(ancho, alto)
            boton.Top = MargenSup
            boton.Left = margenIz
            boton.Name = "boton" & (indice + 1).ToString()
            boton.Text = "boton" & (indice + 1).ToString()

            '   Y TRATAMOS LA FILA Y LA COLUMNA PARA QUE NOS QUEDE TODO BONITO
            columna += 1
            margenIz += 2 + ancho

            '   ESTO FUNCIONA MEJOR QUE TU IF PORQUE LA DIVISION QUE HACIA LA PAROXIMABA A 0 Y
            '   EL PRIMER BOTON ENTRABA AQUI, HACIA UN SALTO DE LINEA Y YA CONTINUABA NORMAL
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

    '   AHORA VAMOS A DONDE HAY CHICHA DE VERDAD
    '   LOS PARAMETROS ESTAN EXPLICADOS EN SU INVOCACION, PASEMOS A LO HARDCORE
    Public Sub DinamicAddHandler(ByVal Formulario As Form, ByVal Control As Control, _
                                 ByVal TipoEvento As String, ByVal Evento As String)

        '   CON REFLECTION BUSCAMOS EL TIPO DEL CONTROL QUE LE HEMOS PASADO, EN NUESTRO EJEMPLO BUTTON
        '   RECOGEMOS LOS EVENTOS DE BUTTON Y BUSCAMOS UNO CONCRETO CON EL PARAMETRO TIPO EVENTO, GUARDANDO
        '   EL MISMO EN EL OBJETO EV
        Dim ev As EventInfo = Control.GetType.GetEvent(TipoEvento)

        '   TAMBIEN CON REFLECTION BUSCAMOS EL TIPO DEL FORMULARIO QUE LE HEMOS PASADO AL CONSTRUCTOR Y BUSCAMOS
        '   TODOS LOS METODOS QUE DISPONE, INCLUYENDO PRIVADOS Y METODOS DE INSTANCIA, FILTRANDOLOS POR EL NOMBRE
        '   DEL EVENTO QUE LE HEMOS PASADO, DECLARADO EN EL FORM TODODINAMICO
        Dim method As MethodInfo = Formulario.GetType().GetMethod(Evento, BindingFlags.NonPublic Or BindingFlags.Instance)

        '   CREAMOS EL HANDLER MEDIANTE UN DELEGADO, CON SU TIPO, EL FORMULARIO Y EL EVENTO DECLARADO EN EL MISMO
        Dim handler As [Delegate] = [Delegate].CreateDelegate(ev.EventHandlerType, Formulario, method)

        '   AGREGAMOS EL HANDLER AL CONTROL, EN NUESTRO CASO UNO A UNO TODOS LOS BOTONES
        ev.AddEventHandler(Control, handler)

        '   Y CON ESO ACABAMOS, COMO VES ES UN MUNDO DE POSIBILIDADES Y ES MUY UTILIZADO POR FRAMEWORKS, PARA QUE TE HAGAS UNA
        '   IDEA EL ATRIBUTO ONCLICK DE ANDROID USARA ESTO, SEGURAMENTE. HE CREADO UN FRAMEWORK DE VB!!!
    End Sub

    '   EVENTO PRIMITIVO QUE TU TENIAS EN EL CODIGO ORIGINAL Y CON EL QUE YO ME PELEABA
    '
    'Private Sub eventoBotonesDinamicos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim botonClickado As Button = DirectCast(sender, Button)
    '    Dim indice As Integer = 0

    '    Panel.Controls.Remove(botonClickado)
    '    botonClickado.Dispose()

    '    For Each boton As Button In Panel.Controls
    '        'le damos un nombre para identificarlo
    '        boton.Name = "boton" & (indice + 1).ToString()
    '        'le damos a cada uno un texto
    '        boton.Text = "boton" & (indice + 1).ToString()
    '        indice += 1
    '    Next
    'End Sub
End Class
