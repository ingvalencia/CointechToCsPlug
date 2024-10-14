<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.ArchivoToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ReportesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.ConfiguracionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SalidaTXTToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.lbl_title = New System.Windows.Forms.Label()
        Me.btnProcesar = New System.Windows.Forms.Button()
        Me.lblProcesando = New System.Windows.Forms.Label()
        Me.lblDetenido = New System.Windows.Forms.Label()
        Me.lblEstatus = New System.Windows.Forms.Label()
        Me.lblAuto = New System.Windows.Forms.Label()
        Me.Timer2 = New System.Windows.Forms.Timer(Me.components)
        Me.txtLogs = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblTktIni = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblTktCont = New System.Windows.Forms.Label()
        Me.chkFecha = New System.Windows.Forms.CheckBox()
        Me.DTPFecha = New System.Windows.Forms.DateTimePicker()
        Me.txtLocal = New System.Windows.Forms.TextBox()
        Me.txtTicket = New System.Windows.Forms.TextBox()
        Me.Local = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtFechaLocal = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.chkLocal = New System.Windows.Forms.CheckBox()
        Me.chkRefid_E = New System.Windows.Forms.CheckBox()
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker()
        Me.DTPFechaFin = New System.Windows.Forms.DateTimePicker()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ArchivoToolStripMenuItem, Me.ReportesToolStripMenuItem, Me.ConfiguracionToolStripMenuItem})
        resources.ApplyResources(Me.MenuStrip1, "MenuStrip1")
        Me.MenuStrip1.Name = "MenuStrip1"
        '
        'ArchivoToolStripMenuItem
        '
        Me.ArchivoToolStripMenuItem.Name = "ArchivoToolStripMenuItem"
        resources.ApplyResources(Me.ArchivoToolStripMenuItem, "ArchivoToolStripMenuItem")
        '
        'ReportesToolStripMenuItem
        '
        Me.ReportesToolStripMenuItem.Name = "ReportesToolStripMenuItem"
        resources.ApplyResources(Me.ReportesToolStripMenuItem, "ReportesToolStripMenuItem")
        '
        'ConfiguracionToolStripMenuItem
        '
        Me.ConfiguracionToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.SalidaTXTToolStripMenuItem})
        Me.ConfiguracionToolStripMenuItem.Name = "ConfiguracionToolStripMenuItem"
        resources.ApplyResources(Me.ConfiguracionToolStripMenuItem, "ConfiguracionToolStripMenuItem")
        '
        'SalidaTXTToolStripMenuItem
        '
        Me.SalidaTXTToolStripMenuItem.Name = "SalidaTXTToolStripMenuItem"
        resources.ApplyResources(Me.SalidaTXTToolStripMenuItem, "SalidaTXTToolStripMenuItem")
        '
        'Timer1
        '
        '
        'btnCancelar
        '
        Me.btnCancelar.BackColor = System.Drawing.Color.Coral
        Me.btnCancelar.FlatAppearance.BorderColor = System.Drawing.Color.WhiteSmoke
        Me.btnCancelar.FlatAppearance.BorderSize = 0
        Me.btnCancelar.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        resources.ApplyResources(Me.btnCancelar, "btnCancelar")
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.UseVisualStyleBackColor = False
        '
        'lbl_title
        '
        resources.ApplyResources(Me.lbl_title, "lbl_title")
        Me.lbl_title.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lbl_title.Name = "lbl_title"
        '
        'btnProcesar
        '
        Me.btnProcesar.BackColor = System.Drawing.SystemColors.ActiveCaption
        resources.ApplyResources(Me.btnProcesar, "btnProcesar")
        Me.btnProcesar.Name = "btnProcesar"
        Me.btnProcesar.UseVisualStyleBackColor = False
        '
        'lblProcesando
        '
        resources.ApplyResources(Me.lblProcesando, "lblProcesando")
        Me.lblProcesando.ForeColor = System.Drawing.Color.DodgerBlue
        Me.lblProcesando.Name = "lblProcesando"
        '
        'lblDetenido
        '
        resources.ApplyResources(Me.lblDetenido, "lblDetenido")
        Me.lblDetenido.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.lblDetenido.Name = "lblDetenido"
        '
        'lblEstatus
        '
        resources.ApplyResources(Me.lblEstatus, "lblEstatus")
        Me.lblEstatus.Name = "lblEstatus"
        '
        'lblAuto
        '
        resources.ApplyResources(Me.lblAuto, "lblAuto")
        Me.lblAuto.ForeColor = System.Drawing.Color.Green
        Me.lblAuto.Name = "lblAuto"
        '
        'Timer2
        '
        '
        'txtLogs
        '
        resources.ApplyResources(Me.txtLogs, "txtLogs")
        Me.txtLogs.Name = "txtLogs"
        '
        'Label1
        '
        resources.ApplyResources(Me.Label1, "Label1")
        Me.Label1.Name = "Label1"
        '
        'lblTktIni
        '
        resources.ApplyResources(Me.lblTktIni, "lblTktIni")
        Me.lblTktIni.Name = "lblTktIni"
        '
        'Label3
        '
        resources.ApplyResources(Me.Label3, "Label3")
        Me.Label3.Name = "Label3"
        '
        'lblTktCont
        '
        resources.ApplyResources(Me.lblTktCont, "lblTktCont")
        Me.lblTktCont.Name = "lblTktCont"
        '
        'chkFecha
        '
        resources.ApplyResources(Me.chkFecha, "chkFecha")
        Me.chkFecha.Name = "chkFecha"
        Me.chkFecha.UseVisualStyleBackColor = True
        '
        'DTPFecha
        '
        resources.ApplyResources(Me.DTPFecha, "DTPFecha")
        Me.DTPFecha.Name = "DTPFecha"
        '
        'txtLocal
        '
        resources.ApplyResources(Me.txtLocal, "txtLocal")
        Me.txtLocal.Name = "txtLocal"
        '
        'txtTicket
        '
        resources.ApplyResources(Me.txtTicket, "txtTicket")
        Me.txtTicket.Name = "txtTicket"
        '
        'Local
        '
        resources.ApplyResources(Me.Local, "Local")
        Me.Local.Name = "Local"
        '
        'Label4
        '
        resources.ApplyResources(Me.Label4, "Label4")
        Me.Label4.Name = "Label4"
        '
        'txtFechaLocal
        '
        resources.ApplyResources(Me.txtFechaLocal, "txtFechaLocal")
        Me.txtFechaLocal.Name = "txtFechaLocal"
        '
        'Label2
        '
        resources.ApplyResources(Me.Label2, "Label2")
        Me.Label2.Name = "Label2"
        '
        'chkLocal
        '
        resources.ApplyResources(Me.chkLocal, "chkLocal")
        Me.chkLocal.Name = "chkLocal"
        Me.chkLocal.UseVisualStyleBackColor = True
        '
        'chkRefid_E
        '
        resources.ApplyResources(Me.chkRefid_E, "chkRefid_E")
        Me.chkRefid_E.Name = "chkRefid_E"
        Me.chkRefid_E.UseVisualStyleBackColor = False
        '
        'DTPFechaFin
        '
        resources.ApplyResources(Me.DTPFechaFin, "DTPFechaFin")
        Me.DTPFechaFin.Name = "DTPFechaFin"
        '
        'Label5
        '
        resources.ApplyResources(Me.Label5, "Label5")
        Me.Label5.Name = "Label5"
        '
        'Label6
        '
        resources.ApplyResources(Me.Label6, "Label6")
        Me.Label6.Name = "Label6"
        '
        'Label7
        '
        resources.ApplyResources(Me.Label7, "Label7")
        Me.Label7.Name = "Label7"
        '
        'Form1
        '
        resources.ApplyResources(Me, "$this")
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.BackgroundImage = Global.CointechToCsPlug.My.Resources.Resources.Coint_CS
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.DTPFechaFin)
        Me.Controls.Add(Me.chkRefid_E)
        Me.Controls.Add(Me.chkLocal)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtFechaLocal)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Local)
        Me.Controls.Add(Me.txtTicket)
        Me.Controls.Add(Me.txtLocal)
        Me.Controls.Add(Me.DTPFecha)
        Me.Controls.Add(Me.chkFecha)
        Me.Controls.Add(Me.lblTktCont)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblTktIni)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtLogs)
        Me.Controls.Add(Me.lblProcesando)
        Me.Controls.Add(Me.lblDetenido)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.lblEstatus)
        Me.Controls.Add(Me.lbl_title)
        Me.Controls.Add(Me.lblAuto)
        Me.Controls.Add(Me.btnProcesar)
        Me.Controls.Add(Me.btnCancelar)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Form1"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents ArchivoToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ReportesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ConfiguracionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SalidaTXTToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents lbl_title As System.Windows.Forms.Label
    Friend WithEvents btnProcesar As System.Windows.Forms.Button
    Friend WithEvents lblDetenido As System.Windows.Forms.Label
    Friend WithEvents lblEstatus As System.Windows.Forms.Label
    Friend WithEvents lblAuto As System.Windows.Forms.Label
    Friend WithEvents lblProcesando As System.Windows.Forms.Label
    Friend WithEvents Timer2 As System.Windows.Forms.Timer
    Friend WithEvents txtLogs As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblTktIni As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblTktCont As System.Windows.Forms.Label
    Friend WithEvents chkFecha As System.Windows.Forms.CheckBox
    Friend WithEvents DTPFecha As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtLocal As System.Windows.Forms.TextBox
    Friend WithEvents txtTicket As System.Windows.Forms.TextBox
    Friend WithEvents Local As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtFechaLocal As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents chkLocal As System.Windows.Forms.CheckBox
    Friend WithEvents chkRefid_E As System.Windows.Forms.CheckBox
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents DTPFechaFin As DateTimePicker
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
End Class
