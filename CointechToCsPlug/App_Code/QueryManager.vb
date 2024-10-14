'Librerias para envio de mail
Imports System.Net.Mail
Imports System.Net
Imports System.IO
'Librerias usadas para mail
Imports System.Net.Security
Imports System.Security.Cryptography.X509Certificates
Imports Microsoft.VisualBasic

'librerias CFDI
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

'Librerias para Encriptar
Imports System.Security.Cryptography

'Libreria para formateo de fecha
Imports System.Globalization
' LIbreria para imporar ConfigurationManager 
Imports System.Configuration
Imports System.Data.SqlClient



Public Class QueryManager

    ' Produccion
    Private cadena As String = "Initial Catalog=GSSAP2010; " & _
                                    "Data Source=192.168.0.174; UID=SA;PWD=P@ssw0rd;"
    ' Desarrollo de pruebas 
    'Private cadena As String = "Initial Catalog=GSSAP2010_PRUEBAS; " & _
    '                                "Data Source=192.168.0.174; UID=SA;PWD=P@ssw0rd;"

    'Variables para envío de correo por SMTP
    Private SmtpServer As String = ConfigurationManager.AppSettings("SmtpServer").ToString
    Private SmtpUser As String = ConfigurationManager.AppSettings("SmtpUser").ToString
    Private SmtpPass As String = ConfigurationManager.AppSettings("SmtpPass").ToString
    Private SmtpPort As Integer = Convert.ToInt32(ConfigurationManager.AppSettings("SmtpPort"))
    Private SmtpSsl As Boolean = Convert.ToBoolean(ConfigurationManager.AppSettings("SmtpSsl"))

    Public cn As SqlConnection
    Public cmb As SqlCommandBuilder

    Private Sub conectar()
        cn = New SqlConnection(cadena)
    End Sub

    Public Sub New()
        conectar()
    End Sub

    Private comando As SqlCommand

    '+----------------------------------------------------------------------------
    '+	Evento   para insertar registro... 
    '+----------------------------------------------------------------------------

    Public Function ExecSP(StorProcedure As String) As Boolean

        Try
            cn.Open()
            comando = New SqlCommand(StorProcedure, cn)
            comando.CommandTimeout = 60
            Dim i As Integer = comando.ExecuteNonQuery()
            cn.Close()

            If i > 0 Then
                Return True
            Else
                Return False

            End If

        Catch ex As Exception
            'SaveError("Error... ----Query ejecutado " + StorProcedure + "-----Error:" + ex.ToString)
            'MsgBox("Error al Insertar, Verifique que no este duplicando la llave primaria", "Error!")

        End Try
        Return True

    End Function

    '+----------------------------------------------------------------------------
    '+	Evento   para insertar registro... 
    '+----------------------------------------------------------------------------

    Public Function insertar(sql As String) As Boolean


        cn.Close()

        cn.Open()
        comando = New SqlCommand(sql, cn)
        comando.CommandTimeout = 200
        Dim i As Integer = comando.ExecuteNonQuery()
        cn.Close()

        If i > 0 Then
            Return True
        Else
            Return False

        End If

        'Try
        '    cn.Close()

        '    cn.Open()
        '    comando = New SqlCommand(sql, cn)
        '    comando.CommandTimeout = 120
        '    Dim i As Integer = comando.ExecuteNonQuery()
        '    cn.Close()

        '    If i > 0 Then
        '        Return True
        '    Else
        '        Return False

        '    End If

        'Catch ex As Exception
        '    SaveError("Error... ----Query ejecutado " + sql + "-----Error:" + ex.ToString)
        '    'MsgBox("Error al Insertar, Verifique que no este duplicando la llave primaria", "Error!")

        'End Try

        Return True

    End Function

    '+----------------------------------------------------------------------------
    '+	Evento   para insertar registro... 
    '+----------------------------------------------------------------------------

    Public Function insertar2(sql As String) As Boolean


        cn.Open()
        comando = New SqlCommand(sql, cn)
        comando.CommandTimeout = 60
        Dim i As Integer = comando.ExecuteNonQuery()
        cn.Close()

        Return True

    End Function

    '+----------------------------------------------------------------------------
    '+	Evento   para eliminar registro... 
    '+----------------------------------------------------------------------------

    Public Function eliminar(tabla As String, condicion As String) As Boolean
        cn.Open()
        Dim sql As String = " delete from " & tabla & " where " & condicion

        comando = New SqlCommand(sql, cn)
        comando.CommandTimeout = 60
        Dim i As Integer = comando.ExecuteNonQuery()
        cn.Close()

        If i > 0 Then
            Return True
        Else
            Return False
            SaveError("Error:----Query ejecutado " + sql + "Error:")
        End If
    End Function

    '+----------------------------------------------------------------------------
    '+	Evento   para actualizar registro... 
    '+----------------------------------------------------------------------------

    Public Function actualizar(tabla As String, campos As String, condicion As String) As Boolean
        cn.Open()
        Dim sql As String = "update " & tabla & " set " & campos & " where " & condicion
        comando = New SqlCommand(sql, cn)
        comando.CommandTimeout = 60
        Dim i As Integer = comando.ExecuteNonQuery()
        cn.Close()

        If i > 0 Then
            Return True

        Else
            Return False
            SaveError("Error... ----Query ejecutado " + sql + "-----Error:")
        End If

    End Function

    '+----------------------------------------------------------------------------
    '+	Evento   para conusltar... 
    '+----------------------------------------------------------------------------

    Public ds As New DataSet()
    Public da As SqlDataAdapter

    Public Sub consultar_Localesfaltantes(sql As String, tabla As String)

        Try
            'ds.Clear()
            cn.Close()

            cn.Open()
           
            da = New SqlDataAdapter(sql, cn)
            da.SelectCommand.CommandTimeout = 1800
            cmb = New SqlCommandBuilder(da)
            da.Fill(ds, tabla)

        Catch ex As Exception

            SaveError(ex.ToString)

        End Try

    End Sub
    Public Sub consultar(sql As String, tabla As String)
        ' comando.CommandTimeout = 200 
        Try
            ds.Clear()
            
            da = New SqlDataAdapter(sql, cn)
            da.SelectCommand.CommandTimeout = 1800
            cmb = New SqlCommandBuilder(da)

            da.Fill(ds, tabla)

        Catch ex As Exception

            SaveError(ex.ToString)

        End Try

    End Sub

    Public Function consultar2(tabla As String) As DataTable
        Dim sql As String = "select * from " & tabla & " where Visible='1'"
        da = New SqlDataAdapter(sql, cn)
        Dim dts As New DataSet()
        da.Fill(dts, tabla)
        Dim dt As New DataTable()
        dt = dts.Tables(tabla)
        Return dt
    End Function

    'Se realizo un nuevo metodo que permite hacer consultas a SP
    Public Sub consultarSP(storedProcedure As String, parametros As List(Of SqlParameter), tabla As String)
        Try
            If cn Is Nothing Then
                conectar()
            End If

            If cn.State = ConnectionState.Closed Then
                cn.Open()
            End If

            ' Limpiar y re-inicializar el DataSet
            If ds Is Nothing Then
                ds = New DataSet()
            Else
                ds.Clear()
            End If

            comando = New SqlCommand(storedProcedure, cn)
            comando.CommandType = CommandType.StoredProcedure
            comando.CommandTimeout = 1800

            ' Agregar los parámetros al comando
            If parametros IsNot Nothing Then
                comando.Parameters.AddRange(parametros.ToArray())
            End If

            da = New SqlDataAdapter(comando)
            cmb = New SqlCommandBuilder(da)

            ' Llenar el DataSet con la tabla especificada
            da.Fill(ds, tabla)

        Catch ex As Exception
            SaveError(ex.ToString())
        Finally
            If cn.State = ConnectionState.Open Then
                cn.Close()
            End If
        End Try
    End Sub




    '+----------------------------------------------------------------------------
    '+	Evento fecha en formato japones... 
    '+----------------------------------------------------------------------------
    Public Function FechaJaponesa(Fecha As String) As String

        'Dim dia, mes, year, FechaOK As String

        'dia = Fecha.Substring(0, 2).ToString
        'mes = Fecha.Substring(3, 2).ToString
        'year = Fecha.Substring(6, 4).ToString

        'FechaOK = year + mes + dia

        Try

            Dim Fecha1 As DateTime = DateTime.ParseExact(Fecha, "dd/MM/yyyy", CultureInfo.InvariantCulture)
            Dim FechaOK As String = Fecha1.ToString("yyyyMMdd", CultureInfo.InvariantCulture)

            Return FechaOK

        Catch ex As Exception

        End Try



    End Function


    '+----------------------------------------------------------------------------
    '+	Funcion   para Enviar un correo  
    '+----------------------------------------------------------------------------

    Public Function SendMail(Email As String, Asunto As String, Body As String, Adjunto As String, Adjunto2 As String) As String

        Dim Result As String = "Mensaje enviado"

        Dim message As New MailMessage
        Dim smtp As New SmtpClient


        'message.From = New MailAddress("notificaciones@kataplum.com.mx")
        'message.From = New MailAddress("jalanis@grupodiniz.com.mx")
        message.From = New MailAddress(SmtpUser)
        message.To.Add(Email.ToString)
        'message.Body = "Estimado " + Nombre + vbCrLf + vbCrLf + "Adjuntamos la cotizacion generada a traves de el Portal de Raven, esperamos porder contar con usted." + vbCrLf + vbCrLf + "Para Mayores informes contacte a su agente de ventas." + vbCrLf + vbCrLf + "No Conteste a este mail fue, creado atumaticamente a través de:" + vbCrLf + " B-Focus Systems."
        message.Body = Body
        'message.Body = GridViewToHtml(GridDetaVede)
        message.Subject = Asunto
        message.Priority = MailPriority.Normal
        message.IsBodyHtml = False


        If Adjunto.Length > 2 Then
            Dim archivoAdjunto As New System.Net.Mail.Attachment(Adjunto)
            message.Attachments.Add(archivoAdjunto)

        End If
        If Adjunto2.Length > 2 Then
            Dim archivoAdjunto2 As New System.Net.Mail.Attachment(Adjunto2)
            message.Attachments.Add(archivoAdjunto2)
        End If

        message.IsBodyHtml = True

        smtp.EnableSsl = SmtpSsl
        smtp.Port = SmtpPort
        smtp.Host = SmtpServer
        smtp.Credentials = New Net.NetworkCredential(SmtpUser, SmtpPass)

        Try
            smtp.Send(message)
            Return Result
        Catch ex As Exception
            SaveError("Error... :" + ex.ToString)
            Return "Menseje no enviado"

        End Try

    End Function

    '+----------------------------------------------------------------------------
    '+	Funcion para almacenar errores 
    '+----------------------------------------------------------------------------

    Public Function SaveError(Mensaje As String) As String
        Dim Fecha, Hora As String


        Fecha = FechaJaponesa(DateTime.Now).ToString

        Hora = TimeOfDay.ToString.ToString.Substring(10, 14)
        Hora = Hora.Replace(" ", "").Replace(":", "").Replace(" ", "").Replace(".", "")

        'Const fic As String = ruta.ToString
        'Dim fic As String = ruta.ToString
        'Dim fic As String = "T:\Inetpub\wwwapps\Reporter\error\" + "error" + ".txt"
        'Dim texto As String = Mensaje

        'Dim sw As New System.IO.StreamWriter(fic)
        'sw.WriteLine(texto)
        'sw.Close()

        Return "Almacenado"
    End Function


    '+----------------------------------------------------------------------------
    '+	Funcion para Encriptar
    '+----------------------------------------------------------------------------

    Public Function Encripta(ByVal Input As String) As String

        Dim IV() As Byte = ASCIIEncoding.ASCII.GetBytes("qualityi") 'La clave debe ser de 8 caracteres
        Dim EncryptionKey() As Byte = Convert.FromBase64String("rpaSPvIvVLlrcmtzPU9/c67Gkj7yL1S5") 'No se puede alterar la cantidad de caracteres pero si la clave
        Dim buffer() As Byte = Encoding.UTF8.GetBytes(Input)
        Dim des As TripleDESCryptoServiceProvider = New TripleDESCryptoServiceProvider
        des.Key = EncryptionKey
        des.IV = IV

        Return Convert.ToBase64String(des.CreateEncryptor().TransformFinalBlock(buffer, 0, buffer.Length()))

    End Function


    '+----------------------------------------------------------------------------
    '+	Funcion para Desencriptar
    '+----------------------------------------------------------------------------

    Public Function Desencripta(ByVal Input As String) As String

        Dim IV() As Byte = ASCIIEncoding.ASCII.GetBytes("qualityi") 'La clave debe ser de 8 caracteres
        Dim EncryptionKey() As Byte = Convert.FromBase64String("rpaSPvIvVLlrcmtzPU9/c67Gkj7yL1S5") 'No se puede alterar la cantidad de caracteres pero si la clave
        Dim buffer() As Byte = Convert.FromBase64String(Input)
        Dim des As TripleDESCryptoServiceProvider = New TripleDESCryptoServiceProvider
        des.Key = EncryptionKey
        des.IV = IV
        Return Encoding.UTF8.GetString(des.CreateDecryptor().TransformFinalBlock(buffer, 0, buffer.Length()))

    End Function


    '+----------------------------------------------------------------------------
    '+	Funcion   para Enviar un correo  
    '+----------------------------------------------------------------------------

    Public Function SenError(Email As String, Asunto As String, Body As String) As String

        Dim Result As String = "Mensaje enviado"

        Dim message As New MailMessage
        Dim smtp As New SmtpClient


        message.From = New MailAddress("info@itsconsultoria.com.mx")
        message.To.Add(Email.ToString)
        'message.Body = "Estimado " + Nombre + vbCrLf + vbCrLf + "Adjuntamos la cotizacion generada a traves de el Portal de Raven, esperamos porder contar con usted." + vbCrLf + vbCrLf + "Para Mayores informes contacte a su agente de ventas." + vbCrLf + vbCrLf + "No Conteste a este mail fue, creado atumaticamente a través de:" + vbCrLf + " B-Focus Systems."
        message.Body = Body
        'message.Body = GridViewToHtml(GridDetaVede)
        message.Subject = Asunto
        message.Priority = MailPriority.Normal


        message.IsBodyHtml = True

        'smtp.EnableSsl = True
        'smtp.Port = "587"
        'smtp.Host = "smtp.1and1.mx"
        'smtp.Credentials = New Net.NetworkCredential("info@business-focus.mx", "Mexico2014")


        smtp.EnableSsl = False
        smtp.Port = "26"
        smtp.Host = "mail.itsconsultoria.com.mx"
        smtp.Credentials = New Net.NetworkCredential("info@itsconsultoria.com.mx", "info2016*")

        'smtp.Send(message)

        Try
            smtp.Send(message)
            Return Result
        Catch ex As Exception
            SaveError("Error... :" + ex.ToString)
            Return "Menseje no enviado"

        End Try

    End Function


End Class
