﻿Imports System.IO
Imports System.Data
Imports System.Text
Imports System
Imports System.Data.SqlClient
Imports CointechToCsPlug.csfacturacion

Public Class Form1
    Dim claseob As New QueryManager()
    Public Ticket, DatosGrals, DatosEmisor, DatosDelConcepto, ImpuestosComprobante, Sucursal, RutaTXT, CancelaCreacion, DiasVenci, FGeneracion, fechaFin, localesFaltantes As String
    Public RFC, Emisor, Regimen, Pruebas, DFecha, TicektM, LocalM, ValTicketM, Ad_RefId, FGeneracionJAP As String
    Public MailNotifica As String
    Public NoRegistros As Integer
    Public MailAdmin, MailTodos As String
    Public reader As New System.Configuration.AppSettingsReader

#Region "Eventos"

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load



        GetVarDatosEmisor()

        lbl_title.BackColor = Color.Transparent
        lblEstatus.BackColor = Color.Transparent
        lblDetenido.BackColor = Color.Transparent
        lblProcesando.BackColor = Color.Transparent
        lblAuto.BackColor = Color.Transparent
        lblTktCont.BackColor = Color.Transparent
        lblTktIni.BackColor = Color.Transparent
        Label1.BackColor = Color.Transparent
        Label3.BackColor = Color.Transparent
        chkFecha.BackColor = Color.Transparent

        lblDetenido.Visible = False
        lblProcesando.Visible = False

        txtLogs.ScrollBars = ScrollBars.Vertical

        MailAdmin = reader.GetValue("MailAdmin", GetType(String))
        MailTodos = reader.GetValue("MailTodos", GetType(String))

        Timer1.Enabled = True
        Timer1.Interval = 5000

        txtLogs.Text = txtLogs.Text + "INICIA Proceso Automatico . . ." + DateAndTime.Now.ToString + vbCrLf
        Me.Refresh()

        FGeneracion = (Date.Now.AddDays(-1).ToShortDateString).ToString
        fechaFin = (Date.Now.AddDays(-1).ToShortDateString).ToString
        DFecha = claseob.FechaJaponesa((Date.Now.AddDays(-1).ToShortDateString).ToString)

        FGeneracionJAP = claseob.FechaJaponesa(FGeneracion)

        Ad_RefId = ""
        

    End Sub

    Private Sub SalidaTXTToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SalidaTXTToolStripMenuItem.Click
        FRM_Adm_SalidaTXT.Show()

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick



        If CancelaCreacion = "0" Then

            Timer1.Enabled = False
        Else
            'inica la generacion de archivos txt

            ValidaDirectorios()

            Timer1.Enabled = False

            InitalVars()
            lblProcesando.Visible = True
            Me.Refresh()



            GetCefs()
            UpdateLocalesProcesados()

            lblProcesando.Text = "Procesado . . . "
            Me.Refresh()

            GetCefSinDatos()



            Timer2.Enabled = True ' Activamos el control al iniciar el formulario
            Timer2.Interval = 20000 ' Un segundo de intervalo ' Recuerda que es en milisegundos

            txtLogs.Text = txtLogs.Text + "Terminando Proceso Automatico . . ." + DateAndTime.Now.ToString + vbCrLf
            Me.Refresh()

        End If

    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click

        lblAuto.Visible = False
        lblDetenido.Visible = True
        CancelaCreacion = "0"

        txtLogs.Text = txtLogs.Text + "Cancela Proceso Automatico * * *" + vbCrLf
        Me.Refresh()
    End Sub

    Private Sub btnProcesar_Click(sender As Object, e As EventArgs) Handles btnProcesar.Click

        If chkRefid_E.Checked = True Then
            Ad_RefId = "_E"
        End If

        If txtLocal.Text <> "" And txtTicket.Text <> "" Then
            ValTicketM = 1
        End If

        If chkFecha.Checked Then
            FGeneracion = DTPFecha.Value.ToShortDateString.ToString
            fechaFin = DTPFechaFin.Value.ToShortDateString().ToString()
            DFecha = claseob.FechaJaponesa(DTPFecha.Value.ToShortDateString.ToString)

        Else
            FGeneracion = (Date.Now.AddDays(-1).ToShortDateString).ToString
            DFecha = claseob.FechaJaponesa((Date.Now.AddDays(-1).ToShortDateString).ToString)

        End If

        txtLogs.Text = txtLogs.Text + "Inicia Proceso manual  + + +" + vbCrLf
        Me.Refresh()

        InitalVars()
        lblProcesando.Visible = True
        Me.Refresh()

        GetCefs()
        UpdateLocalesProcesados()

        lblProcesando.Text = "Procesado . . . "
        Me.Refresh()

        Timer2.Enabled = True ' Activamos el control al iniciar el formulario
        Timer2.Interval = 20000 ' Un segundo de intervalo ' Recuerda que es en milisegundos

        txtLogs.Text = txtLogs.Text + "Terminando Proceso Automatico . . ." + DateAndTime.Now.ToString + vbCrLf
        Me.Refresh()

    End Sub

    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick

        SaveLog()
        Me.Close()

    End Sub

    Private Sub DTPFecha_ValueChanged(sender As Object, e As EventArgs) Handles DTPFecha.ValueChanged
        chkFecha.Checked = True
    End Sub

    Private Sub ValidaDirectorios()

        Dim da As New DataTable

        ' Obtienes mail para notificar errores.
        claseob.consultar("select * from ADM_Config where IdConfig in(12) and visible='1'", "ADM_Config")
        da = claseob.ds.Tables("ADM_Config")
        MailNotifica = da.Rows(0).Item(2).ToString

        claseob.consultar("select  SaveTicket  from FactElect group by SaveTicket", "FactElect")
        da = claseob.ds.Tables("FactElect")

        For i As Integer = 0 To (Convert.ToInt32(da.Rows.Count) - 1)

			'If My.Computer.FileSystem.DirectoryExists(da.Rows(i).Item(0).ToString.ToUpper) <> True Then
			If My.Computer.FileSystem.DirectoryExists(da.Rows(i).Item(0).ToString.ToUpper) <> True Then

				claseob.insertar("exec 	[SP_SendMailFlujo] '" + MailNotifica + "', 'Error Unidades', 'Hay un error en las unidades mapeadas, servidor pruebas, Cointech to CS Plug  <br> No encontro: " + da.Rows(i).Item(0).ToString.ToUpper + "'")
				End

			End If

		Next

    End Sub

#End Region

#Region "Metodos"

    Private Sub InitalVars()

        txtLogs.Text = txtLogs.Text + "InitalVars" + vbCrLf
        Me.Refresh()

        Try
            Dim da As New DataTable

            'Obtiene la ruta donde almacenara los txt
            claseob.consultar("select * from ADM_Config where IdConfig in(1,4) and visible='1'", "ADM_Config")
            da = claseob.ds.Tables("ADM_Config")
            RutaTXT = da.Rows(0).Item(2).ToString
            DiasVenci = da.Rows(1).Item(2).ToString

        Catch ex As Exception

            txtLogs.Text = txtLogs.Text + " Error:InitalVars: " + ex.ToString + vbCrLf
            Me.Refresh()

        End Try


    End Sub

    Public Sub GetCefs()

        Try
            'txtLogs.Text = txtLogs.Text + "GetCefs" + vbCrLf
            'Me.Refresh()

            Dim NoRegistros As Integer
            Dim da As New DataTable
            Dim CEFS As String = ""

            claseob.consultar("select clocal from FA_POS where  fecha ='" + FGeneracion + "'group by clocal ", "FA_POS")
            da = claseob.ds.Tables("FA_POS")
            NoRegistros = da.Rows.Count

            Dim index As Integer = 0
            While index <= (NoRegistros - 1)

                CEFS = CEFS + "'" + da.Rows(index).Item(0).ToString.Trim + "'"

                If index <> (NoRegistros - 1) Then
                    CEFS = CEFS + ","
                End If

                index += 1
            End While
            Dim HorainicioLocalesfaltantes = DateTime.Now.ToString("hh:mm tt")

            'If HorainicioLocalesfaltantes = "11:50 p. m." Then
            'LocalesFaltantesTicket(FGeneracion)
            'End If

            GetTickts("", "")

        Catch ex As Exception

            txtLogs.Text = txtLogs.Text + "Error:GetCefs" + ex.ToString + vbCrLf
            Me.Refresh()

        End Try

    End Sub

    Public Sub GetTickts(CEF As String, FechaSD As String)
        Try
            Dim da1 As New DataTable
            Dim da As New DataTable
            Dim Concat_ As String = ""
            Dim contador As Integer
            Dim i_ As Integer
            Dim resultadoVeces As Integer
            Dim text_ As String

            If chkLocal.Checked = True Then
                CEF = txtFechaLocal.Text
            End If

            ' Obtiene los datos para generar los tickets
            If ValTicketM = 1 Then
                claseob.consultarSP("SP_SELEC_TICKETS", New List(Of SqlParameter) From {
                New SqlParameter("@Feci", SqlDbType.Date) With {.Value = "2024-04-01"},
                New SqlParameter("@Fecf", SqlDbType.Date) With {.Value = "2024-04-30"},
                New SqlParameter("@CEF", SqlDbType.VarChar) With {.Value = If(String.IsNullOrEmpty(CEF), DBNull.Value, CEF)}
            }, "ResultadosTickets")
            Else
                If CEF = "" Then
                    txtLogs.Text = txtLogs.Text + "Entrando en condición... G1" + vbCrLf
                    txtLogs.Text = txtLogs.Text + "Parámetros: Feci=" & FGeneracion & ", Fecf=" & fechaFin & ", CEF=" & CEF & vbCrLf

                    If chkFecha.Checked Then
                        claseob.consultarSP("SP_SELEC_TICKETS", New List(Of SqlParameter) From {
                        New SqlParameter("@Feci", SqlDbType.Date) With {.Value = FGeneracion},
                        New SqlParameter("@Fecf", SqlDbType.Date) With {.Value = fechaFin},
                        New SqlParameter("@CEF", SqlDbType.VarChar) With {.Value = ""}
                    }, "ResultadosTickets")
                    Else
                        claseob.consultarSP("SP_SELEC_TICKETS", New List(Of SqlParameter) From {
                        New SqlParameter("@Feci", SqlDbType.Date) With {.Value = FGeneracion},
                        New SqlParameter("@Fecf", SqlDbType.Date) With {.Value = FGeneracion},
                        New SqlParameter("@CEF", SqlDbType.VarChar) With {.Value = DBNull.Value}
                    }, "ResultadosTickets")
                    End If
                Else
                    'txtLogs.Text = txtLogs.Text + "Entrando en condición... G1" + vbCrLf
                    'txtLogs.Text = txtLogs.Text + "Parámetros: Feci=" & FGeneracion & ", Fecf=" & fechaFin & ", CEF=" & CEF & vbCrLf
                    claseob.consultarSP("SP_SELEC_TICKETS", New List(Of SqlParameter) From {
                    New SqlParameter("@Feci", SqlDbType.Date) With {.Value = FGeneracion},
                    New SqlParameter("@Fecf", SqlDbType.Date) With {.Value = fechaFin},
                    New SqlParameter("@CEF", SqlDbType.VarChar) With {.Value = CEF}
                }, "ResultadosTickets")
                End If
            End If

            da = claseob.ds.Tables("ResultadosTickets")
            NoRegistros = da.Rows.Count

            lblTktIni.Text = NoRegistros
            txtLogs.Text = txtLogs.Text + "Tickets por generar: " + NoRegistros.ToString + vbCrLf
            Me.Refresh()

            For i As Integer = 0 To (NoRegistros - 1)
                If da.Rows(i).Item(4) <> "0" Then
                    ' Llama a los métodos que construyen el ticket txt
                    Sucursal = GetSucursal((da.Rows(i).Item(6).ToString), (da.Rows(i).Item(4).ToString), (da.Rows(i).Item(7).ToString))
                    DatosGrals = GetDatosGrals((da.Rows(i).Item(6).ToString), (da.Rows(i).Item(4).ToString), ((da.Rows(i).Item(5) / 1.16).ToString), (da.Rows(i).Item(5).ToString), (da.Rows(i).Item(14).ToString), (da.Rows(i).Item(16).ToString))
                    GetDatosEmisor()
                    DatosDelConcepto = GetDatosDelConcepto((da.Rows(i).Item(5) / 1.16).ToString, (da.Rows(i).Item(6).ToString.Trim) + "-" + (da.Rows(i).Item(4).ToString.Trim))
                    ImpuestosComprobante = GetImpuestosComprobante(((da.Rows(i).Item(5) / 1.16) * 0.16).ToString)

                    ' Construye los resultados de los métodos
                    Ticket = Sucursal + vbCrLf + vbCrLf + DatosGrals + vbCrLf + vbCrLf + DatosEmisor + vbCrLf + vbCrLf + DatosDelConcepto + vbCrLf + vbCrLf + ImpuestosComprobante

                    ' Guardar el contenido del ticket en el log antes de llamar a SaveTicket
                    Try
                        Dim logTicketPath As String = "C:\Users\PROGRAMADOR 1\Downloads\LOGS\log_envio_ticket_" + DateTime.Now.ToString("yyyyMMdd") + ".txt"
                        Dim logTicketContent As String = "Ticket: " + Ticket + vbCrLf + "Nombre Archivo: " + (da.Rows(i).Item(6).ToString + "_" + da.Rows(i - 0).Item(4).ToString) + vbCrLf + "Datos: " + (da.Rows(i).Item(4).ToString + "|" + da.Rows(i).Item(6).ToString + "|" + da.Rows(i - 0).Item(5).ToString) + "|" + da.Rows(i - 0).Item(7).ToString + vbCrLf + "-------------------------" + vbCrLf
                        File.AppendAllText(logTicketPath, logTicketContent)
                    Catch ex As Exception
                        txtLogs.Text = txtLogs.Text + "Error al guardar el log del ticket: " + ex.ToString + vbCrLf
                    End Try

                    ' Llamada al método SaveTicket original
                    SaveTicket(Ticket, (da.Rows(i).Item(6).ToString + "_" + da.Rows(i).Item(4).ToString), (da.Rows(i).Item(4).ToString + "|" + da.Rows(i).Item(6).ToString + "|" + da.Rows(i - 0).Item(5).ToString) + "|" + da.Rows(i - 0).Item(7).ToString, da.Rows(i - 0).Item(17).ToString)

                    lblTktCont.Text = (i + 1).ToString
                    Me.Refresh()
                Else
                    Dim text() As String = {"", "<tr><td>  Error </td><td> Fecha Generada : ", FGeneracion, " </td><td> Numero de Comprobante :", da.Rows(i).Item(4).ToString, " </td><td> CEF: ", da.Rows(i).Item(6).ToString, "</td> <td> Importe: ", da.Rows(i).Item(5).ToString, "</td></tr>"}
                    Concat_ += String.Concat(text)
                End If
            Next

            ' Guarda en log_mail.txt en lugar de enviar el correo
            Dim logPath As String = "C:\Users\PROGRAMADOR 1\Downloads\LOGS\log_mail_" + DateTime.Now.ToString("yyyyMMdd") + ".txt"
            Dim logContent As String = "Total Tickets por generar: " + NoRegistros.ToString + vbCrLf +
                               "Fecha que se ha generado: " + FGeneracion + vbCrLf +
                               "CEF: " + CEF + vbCrLf +
                               "Detalles: <table border=1>" + Concat_ + "</table>"
            Try
                File.AppendAllText(logPath, logContent)
            Catch ex As Exception
                txtLogs.Text = txtLogs.Text + "Error al guardar el log en " + logPath + ": " + ex.ToString + vbCrLf
            End Try

            lblProcesando.Text = "Procesado ..."
        Catch ex As Exception
            txtLogs.Text = txtLogs.Text + "Error: GetTickts " + ex.ToString + vbCrLf
            Me.Refresh()
        End Try
    End Sub

    Public Sub LocalesFaltantesTicket(FGeneracion_ As String)
        '30/05/2021 ( dia smes y año )
        ' Obtener la cadena del dia de la fecha generada
        Dim cadenaDia As String = FGeneracion_.Substring(0, 2)
        Dim DiaString As String
        Dim cadenaMesAno As String = FGeneracion_.Substring(2, 8)
        Dim ArmarfechaGeneracion_ As String
        Dim Dia As Integer = Convert.ToInt16(cadenaDia).ToString
        Dim da_ As New DataTable
        Dim NoRegistros_ As Integer
        Dim Local_ As String
        For i_ As Integer = 1 To Dia

            DiaString = Convert.ToString(i_)
            ArmarfechaGeneracion_ = DiaString + cadenaMesAno
            ' COnsulta para obtener todos los locales
            claseob.consultar(" select clocal from FA_POS where  fecha = '" + ArmarfechaGeneracion_ + "' group by clocal", "FA_POS")

            da_ = claseob.ds.Tables("FA_POS")
            NoRegistros_ = da_.Rows.Count

            For j As Integer = 0 To NoRegistros_ - 1

                Local_ = da_.Rows(j).Item(0).ToString
                'localesFaltantes = GetLocalesFaltantesTickets(da.Rows(j).Item(0).ToString, ArmarfechaGeneracion_)

                ''
               

                Try

                    Dim da As New DataTable
                    Dim Concatenar As String

                    claseob.consultar_Localesfaltantes("select * from (select fp.*, lt.fechacreacion " + _
                                                          " from vw_fa_pos fp left join adm_logtickets lt on fp.numero_comprobante = lt.numero_comprobante and fp.clocal=lt.clocal AND lt.fechaticket = fp.fecha " + _
                                                          " where fp.fecha >= DateAdd(Day, -30, GETDATE()) " + _
                                                          " AND datepart(YEAR,fp.fecha) =datepart(YEAR,GETDATE())	) t " + _
                                                          " where importe > 1.0 and fecha = '" + ArmarfechaGeneracion_ + "' and Clocal='" + Local_ + "'" + _
                                                          " order by fecha,numero_comprobante, clocal ", "VW_FA_POS")



                    da = claseob.ds.Tables("VW_FA_POS")

                    'numeroComprobante = da.Rows(0)("numero_comprobante")

                    NoRegistros = da.Rows.Count

                    lblTktIni.Text = NoRegistros


                    For i As Integer = 0 To (NoRegistros - 1)

                        If da.Rows(i).Item(4) <> "0" Then

                            'llama los metodos que construllen el tickt txt
                            Sucursal = GetSucursal((da.Rows(i).Item(6).ToString), (da.Rows(i).Item(4).ToString), (da.Rows(i).Item(7).ToString))
                            DatosGrals = GetDatosGrals((da.Rows(i).Item(6).ToString), (da.Rows(i).Item(4).ToString), ((da.Rows(i).Item(5) / 1.16).ToString), (da.Rows(i).Item(5).ToString), (da.Rows(i).Item(14).ToString), (da.Rows(i).Item(16).ToString))
                            GetDatosEmisor()
                            DatosDelConcepto = GetDatosDelConcepto((da.Rows(i).Item(5) / 1.16).ToString, (da.Rows(i).Item(6).ToString.Trim) + "-" + (da.Rows(i).Item(4).ToString.Trim))
                            ImpuestosComprobante = GetImpuestosComprobante(((da.Rows(i).Item(5) / 1.16) * 0.16).ToString)

                            'construlle los resultados de los metodos 
                            Ticket = Sucursal + vbCrLf + vbCrLf + DatosGrals + vbCrLf + vbCrLf + DatosEmisor + vbCrLf + vbCrLf + DatosDelConcepto + vbCrLf + vbCrLf + ImpuestosComprobante

                            SaveTicket(Ticket, (da.Rows(i).Item(6).ToString + "_" + da.Rows(i - 0).Item(4).ToString), (da.Rows(i).Item(4).ToString + "|" + da.Rows(i).Item(6).ToString + "|" + da.Rows(i - 0).Item(5).ToString) + "|" + da.Rows(i - 0).Item(7).ToString, da.Rows(i - 0).Item(17).ToString)

                            lblTktCont.Text = (i + 1).ToString

                           

                            Me.Refresh()

                           

                        End If

                        If da.Rows(i).Item(4) = "0" Then

                            Dim text() As String = {"", "<tr><td>  Error </td><td> Fecha Generada : ", ArmarfechaGeneracion_, " </td><td> Numero de Comprobante :", da.Rows(i).Item(4).ToString, " </td><td> CEF: ", da.Rows(i).Item(6).ToString, "</td> <td> Importe: ", da.Rows(i).Item(5).ToString, "</td></tr>"}
                            Concatenar += String.Concat(text)

                        End If

                    Next

                    If NoRegistros.ToString <> 0 Then

                        txtLogs.Text = "Fecha " + ArmarfechaGeneracion_ + "\n"
                        txtLogs.Text = txtLogs.Text + "Local por generar: " + Local_

                        claseob.SendMail(MailTodos, "Local FALTANTE '" + Local_ + "' CointechToCsPlug", ("<html>Total Tickets Procesados: " + NoRegistros.ToString + vbCrLf + "  <br/> Fecha que se ha generado " + ArmarfechaGeneracion_ + " <br /> Local a Procesar : " + Local_ + "  <table border=1>" + Concatenar + "</table></html>"), "", "")

                        lblProcesando.Text = "Procesado ..."

                    End If

                Catch ex As Exception

                    txtLogs.Text = txtLogs.Text + "Error: GetTickts " + ex.ToString + vbCrLf
                    Me.Refresh()

                End Try

            Next

        Next

    End Sub


    Public Sub GetErroresNum_Comrprobante(FGeneracion_ As String)

        'Dim da1 As New DataTable
        'Dim da As New DataTable
        'Dim row1 As DataRow


        'claseob.consultar(" select * from FA_POS where" + _
        '                  " fecha = '" + FGeneracion + "' and Numero_Comprobante = 0" + _
        '                  " ORDER BY clocal", "FA_POS")

        'da = claseob.ds.Tables("FA_POS")

        'For Each row1 As Integer In da.Rows
        '    row1("Numero_Comprobante")

        'Next


    End Sub

    Private Function GetLocalesFaltantesTickets(sucursal As String, fechaTicket As String)

        Try
            Dim Resultado As String
            Dim da As New DataTable
            Dim Concatenar As String

            claseob.consultar("select * from (select fp.*, lt.fechacreacion " + _
                                                  " from vw_fa_pos fp left join adm_logtickets lt on fp.numero_comprobante = lt.numero_comprobante and fp.clocal=lt.clocal AND lt.fechaticket = fp.fecha " + _
                                                  " where fp.fecha >= DateAdd(Day, -30, GETDATE()) " + _
                                                  " AND datepart(YEAR,fp.fecha) =datepart(YEAR,GETDATE())	) t " + _
                                                  " where importe > 1.0 and fecha = '" + fechaTicket + "' and Clocal='" + sucursal + "'" + _
                                                  " order by fecha,numero_comprobante, clocal ", "VW_FA_POS")



            da = claseob.ds.Tables("VW_FA_POS")

            'numeroComprobante = da.Rows(0)("numero_comprobante")

            NoRegistros = da.Rows.Count

            lblTktIni.Text = NoRegistros
            txtLogs.Text = txtLogs.Text + "Tickets por generar: " + NoRegistros.ToString + vbCrLf

            For i As Integer = 0 To (NoRegistros - 1)

                If da.Rows(i).Item(4) <> "0" Then

                    'llama los metodos que construllen el tickt txt
                    sucursal = GetSucursal((da.Rows(i).Item(6).ToString), (da.Rows(i).Item(4).ToString), (da.Rows(i).Item(7).ToString))
                    DatosGrals = GetDatosGrals((da.Rows(i).Item(6).ToString), (da.Rows(i).Item(4).ToString), ((da.Rows(i).Item(5) / 1.16).ToString), (da.Rows(i).Item(5).ToString), (da.Rows(i).Item(14).ToString), (da.Rows(i).Item(16).ToString))
                    GetDatosEmisor()
                    DatosDelConcepto = GetDatosDelConcepto((da.Rows(i).Item(5) / 1.16).ToString, (da.Rows(i).Item(6).ToString.Trim) + "-" + (da.Rows(i).Item(4).ToString.Trim))
                    ImpuestosComprobante = GetImpuestosComprobante(((da.Rows(i).Item(5) / 1.16) * 0.16).ToString)

                    'construlle los resultados de los metodos 
                    Ticket = sucursal + vbCrLf + vbCrLf + DatosGrals + vbCrLf + vbCrLf + DatosEmisor + vbCrLf + vbCrLf + DatosDelConcepto + vbCrLf + vbCrLf + ImpuestosComprobante

                    SaveTicket(Ticket, (da.Rows(i).Item(6).ToString + "_" + da.Rows(i - 0).Item(4).ToString), (da.Rows(i).Item(4).ToString + "|" + da.Rows(i).Item(6).ToString + "|" + da.Rows(i - 0).Item(5).ToString) + "|" + da.Rows(i - 0).Item(7).ToString, da.Rows(i - 0).Item(17).ToString)

                    lblTktCont.Text = (i + 1).ToString
                    Me.Refresh()

                End If

                If da.Rows(i).Item(4) = "0" Then

                    Dim text() As String = {"", "<tr><td>  Error </td><td> Fecha Generada : ", fechaTicket, " </td><td> Numero de Comprobante :", da.Rows(i).Item(4).ToString, " </td><td> CEF: ", da.Rows(i).Item(6).ToString, "</td> <td> Importe: ", da.Rows(i).Item(5).ToString, "</td></tr>"}
                    Concatenar += String.Concat(text)

                End If

            Next



            claseob.SendMail(MailTodos, "Notifcación de ERRORES CointechToCsPlug", ("<html>Total Tickets por generar: " + NoRegistros.ToString + vbCrLf + "  <br/> Fecha que se ha generado " + fechaTicket + "  <table border=1>" + Concatenar + "</table></html>"), "", "")

            lblProcesando.Text = "Procesado ..."


        Catch ex As Exception

            txtLogs.Text = txtLogs.Text + "Error: GetTickts " + ex.ToString + vbCrLf
            Me.Refresh()

        End Try


    End Function

    Private Function GetSucursal(sucursal As String, refId As String, fechaTicket As String) As String

        Dim Resultado As String

        Try

            'txtLogs.Text = txtLogs.Text + "GetSucursal: " + sucursal + vbCrLf
            'Me.Refresh()
            '"fechaEnvio|" + DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss") + vbCrLf + _
            '"fechaEnvio|" + DTPFecha.Value.ToString("yyyy-MM-dd") + DateTime.Now.ToString("THH:mm:ss") + vbCrLf + _

            Resultado = "aliasSucursal|" + sucursal + vbCrLf + _
                  "refID|" + sucursal.Trim + "-" + refId.ToString.Trim + Ad_RefId + vbCrLf + _
                  "fechaEnvio|" + fechaTicket.Substring(6, 4) + "-" + fechaTicket.Substring(3, 2) + "-" + fechaTicket.Substring(0, 2) + DateTime.Now.ToString("THH:mm:ss") + vbCrLf + _
                  "fechaVigencia|" + (DateTime.Now.AddDays(Convert.ToInt32(DiasVenci))).ToString("yyyy-MM-ddTHH:mm:ss")
            Return Resultado

        Catch ex As Exception
            Return "Error Sucursal"

            'txtLogs.Text = txtLogs.Text + "GetSucursal " + ex.ToString + vbCrLf
            'Me.Refresh()

        End Try

    End Function

    Private Function GetDatosGrals(sucursal As String, refId As String, subtotal As String, total As String, Cp As String, FPago As String) As String

        Dim Resultado As String

        Try

            'txtLogs.Text = txtLogs.Text + "GetDatosGrals: " + refId + vbCrLf
            'Me.Refresh()

            'Dim FormaPago As String = "01"

            'If FPago = "Credito" Then
            '    FormaPago = "04"
            'ElseIf FPago = "AMEX" Then
            '    FormaPago = "04"
            'ElseIf FPago = "Debito" Then
            '    FormaPago = "28"
            'End If



            Resultado = "[Datos Generales]" + vbCrLf + _
                 "Version|3.3" + vbCrLf + _
                 "Serie|" + sucursal.Trim + vbCrLf + _
                 "Folio|" + sucursal.Trim + "-" + refId + vbCrLf + _
                 "FormaPago|" + FPago + vbCrLf + _
                 "CondicionesDePago|" + vbCrLf + _
                 "Subtotal|" + Convert.ToDouble(subtotal).ToString("####0.00") + vbCrLf + _
                 "Descuento|0.00" + vbCrLf + _
                 "Moneda|MXN" + vbCrLf + _
                 "TipoCambio|1" + vbCrLf + _
                 "Total|" + Convert.ToDouble(total).ToString("####0.00") + vbCrLf + _
                 "TipoDeComprobante|I" + vbCrLf + _
                 "MetodoPago|PUE" + vbCrLf + _
                 "LugarExpedicion|" + Cp
            Return Resultado
        Catch ex As Exception

            Return "Error Datos Grals..."

            txtLogs.Text = txtLogs.Text + "GetDatosGrals " + ex.ToString + vbCrLf
            Me.Refresh()


        End Try


    End Function

    Public Sub GetDatosEmisor()

        Try

            'txtLogs.Text = txtLogs.Text + "GetDatosEmisor " + vbCrLf
            'Me.Refresh()


            If Pruebas = 1 Then

                DatosEmisor = "[DATOS DEL EMISOR]" + vbCrLf + _
                              "emRfc|AAA010101AAA" + vbCrLf + _
                              "emNombre|Razon social de Pruebas S.A de C.V." + vbCrLf + _
                              "emRegimenFiscal|601"


            Else

                DatosEmisor = "[DATOS DEL EMISOR]" + vbCrLf + _
                             "emRfc|" + RFC + vbCrLf + _
                             "emNombre|" + Emisor + vbCrLf + _
                             "emRegimenFiscal|" + Regimen

            End If

        Catch ex As Exception

            txtLogs.Text = txtLogs.Text + "GetDatosEmisor " + ex.ToString + vbCrLf
            Me.Refresh()


        End Try

    End Sub

    Private Function GetDatosDelConcepto(ValorUnitario As String, NoIdentificacion As String) As String

        '' Notas del grupo

        '' NoIdentificacion|31-389-0                 ---> por definir  ****
        '' Descripcion| Abaco Madera CHL 100 aros    ---> por definir  ****
        '' ValorUnitario|103.4000                    ---> importe sin iva
        '' trasladoConcBase|517.00                   ---> en caso de haber descuento se calcula sin iva menos descuento
        '' trasladoConcImporte|82.7200               ---> calculo de iva sobre traslado base

        Dim Resultado As String

        Try


            'txtLogs.Text = txtLogs.Text + "GetDatosDelConcepto: " + NoIdentificacion + vbCrLf
            'Me.Refresh()


            Resultado = "[Datos del Concepto]" + vbCrLf + _
                        "ClaveProdServ|01010101" + vbCrLf + _
                        "NoIdentificacion|" + NoIdentificacion + "_" + FGeneracionJAP + vbCrLf + _
                        "Cantidad|1" + vbCrLf + _
                        "ClaveUnidad|E48" + vbCrLf + _
                        "Unidad|Unidad de Servicio" + vbCrLf + _
                        "Descripcion|Servicios de consumo en Recorcholis del día " + FGeneracion + " " + NoIdentificacion + vbCrLf + _
                        "ValorUnitario|" + Convert.ToDouble(ValorUnitario).ToString("####0.00") + vbCrLf + _
                        "Importe|" + Convert.ToDouble(ValorUnitario).ToString("####0.00") + vbCrLf + _
                        "conDescuento|0.00" + vbCrLf + _
                        vbCrLf + _
                        "trasladoConcImpuesto|002" + vbCrLf + _
                        "trasladoConcBase|" + Convert.ToDouble(ValorUnitario).ToString("####0.00") + vbCrLf + _
                        "trasladoConcTasaOCuota|0.1600" + vbCrLf + _
                        "trasladoConcTipoFactor|Tasa" + vbCrLf + _
                        "trasladoConcImporte|" + Convert.ToDouble(ValorUnitario * 0.16).ToString("####0.00")

            Return Resultado

        Catch ex As Exception

            Return "Error Datos del concept"

            txtLogs.Text = txtLogs.Text + "GetDatosDelConcepto " + ex.ToString + vbCrLf
            Me.Refresh()

        End Try


    End Function

    Private Function GetImpuestosComprobante(impuesto As String) As String

        Dim Resultado As String

        Try

            'txtLogs.Text = txtLogs.Text + "GetImpuestosComprobante " + vbCrLf
            'Me.Refresh()

            Resultado = "[DATOS DEL IMPUESTO] " + vbCrLf + _
                               "TotalImpuestosRetenidos|0.00" + vbCrLf + _
                               "TotalImpuestosTrasladados|" + Convert.ToDouble(impuesto).ToString("####0.00") + vbCrLf + _
                               vbCrLf + _
                               "[Datos traslado]" + vbCrLf + _
                               "trasladadoImpuesto|002" + vbCrLf + _
                               "trasladadoTipoFactor|Tasa" + vbCrLf + _
                               "trasladadoTasaOCuota|0.1600" + vbCrLf + _
                               "trasladadoImporte|" + Convert.ToDouble(impuesto).ToString("####0.00")
            Return Resultado
        Catch ex As Exception

            Return "Error Impuestos Comprobante "

            txtLogs.Text = txtLogs.Text + "GetImpuestosComprobante " + ex.ToString + vbCrLf
            Me.Refresh()


        End Try

    End Function

    Private Sub SaveTicket(ByVal Ticket As String, ByVal NameFile As String, log As String, Ruta As String)

        Try

            ' Inicializa el servicio de facturación y prepara la respuesta
            Dim CSFact As New CointechToCsPlug.csfacturacion.csticketService
            Dim ResFact As New CointechToCsPlug.csfacturacion.respuestaUpload

            ' Convierte el ticket en base64 y lo sube al servicio
            Dim cadenaADevolver As String = Convert.ToBase64String(New UTF8Encoding(True).GetBytes(Ticket))
            ResFact = CSFact.Uploadarchivo("Rec.9808", Convert.FromBase64String(cadenaADevolver))

            ' Aquí accedemos a las propiedades de la respuesta (Msj y Resultado)
            Dim responseMessage As String = If(ResFact.Msj, "Mensaje no disponible")
            Dim responseResult As String = If(ResFact.Resultado.ToString(), "Resultado no disponible")

            ' Almacena la respuesta detallada en el archivo log_ws.txt
            Dim logWSPath As String = "C:\Users\PROGRAMADOR 1\Downloads\LOGS\log_ws_" + DateTime.Now.ToString("yyyyMMdd") + ".txt"
            Dim logWSContent As String = "Webservice Response for Ticket: " + NameFile + vbCrLf +
                                     "Response Message: " + responseMessage + vbCrLf +
                                     "Response Result: " + responseResult + vbCrLf +
                                     "--------------------------------------------------------" + vbCrLf
            Try
                ' Guarda el contenido en el archivo log_ws.txt
                File.AppendAllText(logWSPath, logWSContent)
            Catch ex As Exception
                txtLogs.Text = txtLogs.Text + "Error al guardar el log en " + logWSPath + ": " + ex.ToString + vbCrLf
            End Try



            'Descomentar toda esta seccion y comentar el webservice para continuar con consumo por archivos
            ''Dim RutaSala As String = RutaTXT + "\" + Ruta
            'Dim path As String = Ruta + "\" + "Pos_" + NameFile + "_File_D" + DFecha + ".txt"

            'Dim exists As Boolean = System.IO.Directory.Exists(Ruta)
            'Dim existsFile As Boolean = System.IO.File.Exists(Ruta + "\" + "Pos_" + NameFile + "_File_D" + DFecha + ".txt")

            'If exists = False Then
            '	MkDir(Ruta)
            'End If

            'If existsFile = False Then
            '	Dim fs As FileStream = File.Create(path)
            '	Dim info As Byte() = New UTF8Encoding(True).GetBytes(Ticket)
            '	fs.Write(info, 0, info.Length)
            '	fs.Close()
            'Else
            '	File.Delete(path)
            '	Dim fs As FileStream = File.Create(path)
            '	Dim info As Byte() = New UTF8Encoding(True).GetBytes(Ticket)
            '	fs.Write(info, 0, info.Length)
            '	fs.Close()
            '	'Dim fic As String = path
            '	'Dim sw As New System.IO.StreamWriter(fic, True)
            '	'sw.WriteLine(vbCrLf + Ticket)
            '	'sw.Close()
            'End If



            'Graba log 

            Dim ArrCadena As String() = log.Split("|")


            'Descomentar para que inserte en tabla ADM_LogTickets
            claseob.insertar("exec SP_ADM_LogTickets 'Insert', '', '" + ArrCadena(0).Trim + "', '" + ArrCadena(1).Trim + "','" + ArrCadena(2).Trim + "', " + "'Creo File','" + ResFact.Msj + "', '" + ArrCadena(3).Substring(0, 10).Trim + "'")

        Catch ex As Exception

            txtLogs.Text = txtLogs.Text + "SaveTicket " + ex.ToString + vbCrLf
            Me.Refresh()

        End Try
    End Sub

    Private Sub UpdateLocalesProcesados()

        Try

            Dim Sql As String

            Sql = "update [FactElect] set CreaTXT='0' " + _
                   "where Clocal in ( " + _
                   "	select Clocal from ADM_LogTickets  where FechaTicket='" + (Date.Now.AddDays(-1).ToShortDateString).ToString + "'" + _
                   "	group by Clocal" + _
                   ")"

            claseob.insertar(Sql)

        Catch ex As Exception

        End Try

    End Sub

    Public Sub GetCefSinDatos()

        Try

            txtLogs.Text = txtLogs.Text + "Cef atrazado " + vbCrLf

            Dim NoRegistros As Integer
            Dim da As New DataTable
            Dim CEFS As String = ""
            Dim Fecha As String

            claseob.consultar("select  Id_Log Clocal, clocal, CONVERT (date, fechaticket)  from [dbo].[ADM_LogTickets] where Accion='Dia sin datos'  and Adicionales=''", "ADM_LogTickets")
            da = claseob.ds.Tables("ADM_LogTickets")
            NoRegistros = da.Rows.Count

            If NoRegistros > 0 Then

                Dim index As Integer = 0
                While index <= (NoRegistros - 1)

                    txtLogs.Text = txtLogs.Text + "Cef atrazado " + da.Rows(index).Item(1).ToString.Trim + "  del día " + Convert.ToDateTime(da.Rows(index).Item(2)).ToShortDateString + vbCrLf
                    Me.Refresh()

                    Fecha = claseob.FechaJaponesa(Convert.ToDateTime(da.Rows(index).Item(2)).ToShortDateString)
                    GetTickts(da.Rows(index).Item(1).ToString.Trim, Fecha)
                    claseob.insertar("Update 	[ADM_LogTickets] set Adicionales= ('Completado : ' +  convert(varchar,  GETDATE(), 121)) where Id_Log='" + da.Rows(index).Item(0).ToString.Trim + "'")

                    index += 1
                End While

            End If

        Catch ex As Exception

        End Try
    End Sub

    'Private Sub GetNotProcessed()

    '    Try

    '        Dim NoRegistros As Integer

    '        Dim da As New DataTable
    '        Dim sql As String

    '        sql = "select Clocal, LugarExpedicion as Sin_Datos from FactElect  where Clocal  COLLATE Modern_Spanish_CI_AS not in " + _
    '                "(" + _
    '                "select clocal from [FA_POS] " + _
    '                "where fecha='" + (Date.Now.AddDays(-1).ToShortDateString).ToString + "'" + _
    '                "group by clocal" + _
    '                ")"

    '        claseob.consultar(sql, "FactElect")
    '        'claseob.consultar("select * from VW_FA_POS where clocal in (" + CEF + ") and fecha='2017-11-01'  ", "VW_FA_POS")
    '        da = claseob.ds.Tables("FactElect")

    '        NoRegistros = da.Rows.Count

    '        txtLogs.Text = txtLogs.Text + "GetNotProcessed " + NoRegistros + vbCrLf
    '        Me.Refresh()

    '        Dim index As Integer = 0

    '        While index <= (NoRegistros - 1)

    '            claseob.insertar("exec SP_ADM_LogTickets 'Insert', '', '', '" + da.Rows(index).Item(0).ToString + "','', " + "'Sin Datos','No encontro datos sobre estos locales',''")
    '            index += 1

    '        End While

    '    Catch ex As Exception

    '        txtLogs.Text = txtLogs.Text + "GetNotProcessed " + ex.ToString + vbCrLf
    '        Me.Refresh()

    '    End Try

    'End Sub

    'Private Sub UpdateCFSna()

    '    Dim Sql As String

    '    Try

    '        txtLogs.Text = txtLogs.Text + "UpdateCFSna " + vbCrLf
    '        Me.Refresh()

    '        Sql = "update [FactElect] set CreaTXT='0' where Clocal in (" + _
    '              "select Clocal from [dbo].[ADM_LogTickets] " + _
    '              "where fechacreacion>'" + claseob.FechaJaponesa(Date.Now.ToShortDateString).ToString + " 00:00' and Accion='Sin Datos' " + _
    '              "group by   Clocal)"

    '        claseob.insertar(Sql)

    '    Catch ex As Exception

    '        txtLogs.Text = txtLogs.Text + "UpdateCFSna " + ex.ToString + vbCrLf
    '        Me.Refresh()

    '    End Try


    'End Sub

    Private Sub SaveLog()

        Try

            Dim Nombre As String = DateAndTime.Now.ToString
            Nombre = Nombre.ToString.Replace("/", "")
            Nombre = Nombre.ToString.Replace(":", "")
            Nombre = Nombre.ToString.Replace(".", "")
            Nombre = Nombre.ToString.Replace(" ", "")

            Nombre = Nombre.Trim

            Dim path As String = "C:\logs\" + "Log_" + Nombre.Trim + ".txt"
            'Dim path As String = RutaXML + "\logs\" + "Log_" + Nombre.Trim + ".txt"

            Dim fic As String = path
            Dim sw As New System.IO.StreamWriter(fic, True)
            sw.WriteLine(txtLogs.Text)
            sw.Close()

        Catch ex As Exception

        End Try


    End Sub

    Private Sub GetVarDatosEmisor()
        Dim da As New DataTable

        Try

            txtLogs.Text = txtLogs.Text + "GetDatosEmisor " + vbCrLf
            Me.Refresh()

            claseob.consultar("select * from  [dbo].[ADM_Config] where IdConfig in (5,6,7,8)", "ADM_Config")
            da = claseob.ds.Tables("ADM_Config")

            RFC = da.Rows(0).Item(2).ToString
            Emisor = da.Rows(1).Item(2).ToString
            Regimen = da.Rows(2).Item(2).ToString
            Pruebas = da.Rows(3).Item(2).ToString

            txtLogs.Text = txtLogs.Text + "Pruebas " + Pruebas + vbCrLf
            Me.Refresh()

        Catch ex As Exception

        End Try

    End Sub



#End Region

    Private Sub btnTicket_Click(sender As Object, e As EventArgs)
        ValTicketM = "1"

    End Sub

End Class