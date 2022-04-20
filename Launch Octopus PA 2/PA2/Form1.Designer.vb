<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtX1 = New System.Windows.Forms.TextBox()
        Me.txtY1 = New System.Windows.Forms.TextBox()
        Me.txtX2 = New System.Windows.Forms.TextBox()
        Me.txtY2 = New System.Windows.Forms.TextBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.ErrorImage = Nothing
        Me.PictureBox1.InitialImage = Nothing
        Me.PictureBox1.Location = New System.Drawing.Point(121, 174)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(61, 96)
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'Timer1
        '
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(0, 17)
        Me.Label1.TabIndex = 1
        '
        'txtX1
        '
        Me.txtX1.Location = New System.Drawing.Point(31, 13)
        Me.txtX1.Name = "txtX1"
        Me.txtX1.Size = New System.Drawing.Size(100, 22)
        Me.txtX1.TabIndex = 2
        '
        'txtY1
        '
        Me.txtY1.Location = New System.Drawing.Point(147, 13)
        Me.txtY1.Name = "txtY1"
        Me.txtY1.Size = New System.Drawing.Size(100, 22)
        Me.txtY1.TabIndex = 3
        '
        'txtX2
        '
        Me.txtX2.Location = New System.Drawing.Point(31, 52)
        Me.txtX2.Name = "txtX2"
        Me.txtX2.Size = New System.Drawing.Size(100, 22)
        Me.txtX2.TabIndex = 4
        '
        'txtY2
        '
        Me.txtY2.Location = New System.Drawing.Point(147, 52)
        Me.txtY2.Name = "txtY2"
        Me.txtY2.Size = New System.Drawing.Size(100, 22)
        Me.txtY2.TabIndex = 5
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(456, 396)
        Me.Controls.Add(Me.txtY2)
        Me.Controls.Add(Me.txtX2)
        Me.Controls.Add(Me.txtY1)
        Me.Controls.Add(Me.txtX1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.PictureBox1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents Timer1 As Timer
    Friend WithEvents Label1 As Label
    Friend WithEvents txtX1 As TextBox
    Friend WithEvents txtY1 As TextBox
    Friend WithEvents txtX2 As TextBox
    Friend WithEvents txtY2 As TextBox
End Class
