<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FRM_Adm_SalidaTXT
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FRM_Adm_SalidaTXT))
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtRuta = New System.Windows.Forms.TextBox()
        Me.lblResultado = New System.Windows.Forms.Label()
        Me.lblDesc = New System.Windows.Forms.Label()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
        Me.txtDias = New System.Windows.Forms.TextBox()
        Me.lblDias = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnGuardar
        '
        Me.btnGuardar.Location = New System.Drawing.Point(357, 183)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(75, 23)
        Me.btnGuardar.TabIndex = 0
        Me.btnGuardar.Text = "Guardar"
        Me.btnGuardar.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 66)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(125, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Ruta de almacenamiento"
        '
        'txtRuta
        '
        Me.txtRuta.Location = New System.Drawing.Point(160, 66)
        Me.txtRuta.Name = "txtRuta"
        Me.txtRuta.Size = New System.Drawing.Size(237, 20)
        Me.txtRuta.TabIndex = 2
        '
        'lblResultado
        '
        Me.lblResultado.AutoSize = True
        Me.lblResultado.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblResultado.ForeColor = System.Drawing.Color.Green
        Me.lblResultado.Location = New System.Drawing.Point(44, 183)
        Me.lblResultado.Name = "lblResultado"
        Me.lblResultado.Size = New System.Drawing.Size(28, 24)
        Me.lblResultado.TabIndex = 3
        Me.lblResultado.Text = "..."
        '
        'lblDesc
        '
        Me.lblDesc.AutoSize = True
        Me.lblDesc.Location = New System.Drawing.Point(12, 93)
        Me.lblDesc.Name = "lblDesc"
        Me.lblDesc.Size = New System.Drawing.Size(19, 13)
        Me.lblDesc.TabIndex = 4
        Me.lblDesc.Text = " ..."
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(403, 66)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(29, 20)
        Me.Button1.TabIndex = 5
        Me.Button1.Text = "V"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'txtDias
        '
        Me.txtDias.Location = New System.Drawing.Point(215, 128)
        Me.txtDias.Name = "txtDias"
        Me.txtDias.Size = New System.Drawing.Size(50, 20)
        Me.txtDias.TabIndex = 7
        '
        'lblDias
        '
        Me.lblDias.AutoSize = True
        Me.lblDias.Location = New System.Drawing.Point(12, 131)
        Me.lblDias.Name = "lblDias"
        Me.lblDias.Size = New System.Drawing.Size(39, 13)
        Me.lblDias.TabIndex = 8
        Me.lblDias.Text = "Label3"
        '
        'FRM_Adm_SalidaTXT
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(496, 243)
        Me.Controls.Add(Me.lblDias)
        Me.Controls.Add(Me.txtDias)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.lblDesc)
        Me.Controls.Add(Me.lblResultado)
        Me.Controls.Add(Me.txtRuta)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnGuardar)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "FRM_Adm_SalidaTXT"
        Me.Text = "Configuracion"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnGuardar As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtRuta As System.Windows.Forms.TextBox
    Friend WithEvents lblResultado As System.Windows.Forms.Label
    Friend WithEvents lblDesc As System.Windows.Forms.Label
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents txtDias As System.Windows.Forms.TextBox
    Friend WithEvents lblDias As System.Windows.Forms.Label
End Class
