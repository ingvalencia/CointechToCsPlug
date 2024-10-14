Public Class FRM_Adm_SalidaTXT

    Dim claseob As New QueryManager()

    Private Sub FRM_Adm_SalidaTXT_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Me.Hide()

        txtRuta.Enabled = False

        Get_Ruta()

        Me.Show()
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Dim Valida As Integer

        Try
            Valida = claseob.insertar("update 	 ADM_Config set Data ='" + txtRuta.Text + "' where IdConfig='1'; update ADM_Config set Data='" + txtDias.Text + "' where IdConfig='4'")
          
            lblResultado.Text = "Actualizado . . ."
           
        Catch ex As Exception

        End Try

    End Sub

    Private Sub Get_Ruta()

        Dim da As New DataTable

        'Obtiene los datos para generar los ticktes
        claseob.consultar("select  * from ADM_Config where IdConfig in (1,4) and visible='1'", "ADM_Config")
        'claseob.consultar("select * from VW_GD_POS where clocal in (" + CEF + ") and fecha='2017-11-01'  ", "VW_GD_POS")
        da = claseob.ds.Tables("ADM_Config")

        lblDesc.Text = da.Rows(0).Item(3).ToString
        txtRuta.Text = da.Rows(0).Item(2).ToString

        lblDias.Text = da.Rows(1).Item(3).ToString
        txtDias.Text = da.Rows(1).Item(2).ToString


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        With FolderBrowserDialog1

            .Reset() ' resetea  

            ' leyenda  
            .Description = " Seleccionar una carpeta "
            ' Path " Mis documentos "  
            .SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)

            ' deshabilita el botón " crear nueva carpeta "  
            .ShowNewFolderButton = False
            '.RootFolder = Environment.SpecialFolder.Desktop  
            '.RootFolder = Environment.SpecialFolder.StartMenu  

            Dim ret As DialogResult = .ShowDialog ' abre el diálogo  

            txtRuta.Text = .SelectedPath.ToString

            .Dispose()


        End With


    End Sub

    Private Sub txtDias_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtDias.KeyPress
        If Char.IsDigit(e.KeyChar) Then
            e.Handled = False
        ElseIf Char.IsControl(e.KeyChar) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub
End Class