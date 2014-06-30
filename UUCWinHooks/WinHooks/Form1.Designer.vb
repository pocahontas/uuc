<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form remplace la méthode Dispose pour nettoyer la liste des composants.
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

    'Requise par le Concepteur Windows Form
    Private components As System.ComponentModel.IContainer

    'REMARQUE : la procédure suivante est requise par le Concepteur Windows Form
    'Elle peut être modifiée à l'aide du Concepteur Windows Form.  
    'Ne la modifiez pas à l'aide de l'éditeur de code.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
		Me.Btn_hook = New System.Windows.Forms.Button()
		Me.Btn_unhook = New System.Windows.Forms.Button()
		Me.TextBox1 = New System.Windows.Forms.TextBox()
		Me.Btn_Hook_KB = New System.Windows.Forms.Button()
		Me.Btn_Unhook_KB = New System.Windows.Forms.Button()
		Me.Btn_Unhook_Mouse = New System.Windows.Forms.Button()
		Me.Btn_Hook_Mouse = New System.Windows.Forms.Button()
		Me.SuspendLayout()
		'
		'Btn_hook
		'
		Me.Btn_hook.Location = New System.Drawing.Point(0, 12)
		Me.Btn_hook.Name = "Btn_hook"
		Me.Btn_hook.Size = New System.Drawing.Size(104, 23)
		Me.Btn_hook.TabIndex = 0
		Me.Btn_hook.Text = "Hook Event"
		Me.Btn_hook.UseVisualStyleBackColor = True
		'
		'Btn_unhook
		'
		Me.Btn_unhook.Location = New System.Drawing.Point(110, 12)
		Me.Btn_unhook.Name = "Btn_unhook"
		Me.Btn_unhook.Size = New System.Drawing.Size(94, 23)
		Me.Btn_unhook.TabIndex = 1
		Me.Btn_unhook.Text = "Unhook Event"
		Me.Btn_unhook.UseVisualStyleBackColor = True
		'
		'TextBox1
		'
		Me.TextBox1.Location = New System.Drawing.Point(12, 60)
		Me.TextBox1.Multiline = True
		Me.TextBox1.Name = "TextBox1"
		Me.TextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both
		Me.TextBox1.Size = New System.Drawing.Size(917, 201)
		Me.TextBox1.TabIndex = 2
		'
		'Btn_Hook_KB
		'
		Me.Btn_Hook_KB.Location = New System.Drawing.Point(270, 12)
		Me.Btn_Hook_KB.Name = "Btn_Hook_KB"
		Me.Btn_Hook_KB.Size = New System.Drawing.Size(95, 23)
		Me.Btn_Hook_KB.TabIndex = 3
		Me.Btn_Hook_KB.Text = "Hook KB"
		Me.Btn_Hook_KB.UseVisualStyleBackColor = True
		'
		'Btn_Unhook_KB
		'
		Me.Btn_Unhook_KB.Location = New System.Drawing.Point(371, 12)
		Me.Btn_Unhook_KB.Name = "Btn_Unhook_KB"
		Me.Btn_Unhook_KB.Size = New System.Drawing.Size(75, 23)
		Me.Btn_Unhook_KB.TabIndex = 4
		Me.Btn_Unhook_KB.Text = "UnHook KB"
		Me.Btn_Unhook_KB.UseVisualStyleBackColor = True
		'
		'Btn_Unhook_Mouse
		'
		Me.Btn_Unhook_Mouse.Location = New System.Drawing.Point(608, 12)
		Me.Btn_Unhook_Mouse.Name = "Btn_Unhook_Mouse"
		Me.Btn_Unhook_Mouse.Size = New System.Drawing.Size(98, 23)
		Me.Btn_Unhook_Mouse.TabIndex = 6
		Me.Btn_Unhook_Mouse.Text = "UnHook Mouse"
		Me.Btn_Unhook_Mouse.UseVisualStyleBackColor = True
		'
		'Btn_Hook_Mouse
		'
		Me.Btn_Hook_Mouse.Location = New System.Drawing.Point(507, 12)
		Me.Btn_Hook_Mouse.Name = "Btn_Hook_Mouse"
		Me.Btn_Hook_Mouse.Size = New System.Drawing.Size(95, 23)
		Me.Btn_Hook_Mouse.TabIndex = 5
		Me.Btn_Hook_Mouse.Text = "Hook Mouse"
		Me.Btn_Hook_Mouse.UseVisualStyleBackColor = True
		'
		'Form1
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(953, 273)
		Me.Controls.Add(Me.Btn_Unhook_Mouse)
		Me.Controls.Add(Me.Btn_Hook_Mouse)
		Me.Controls.Add(Me.Btn_Unhook_KB)
		Me.Controls.Add(Me.Btn_Hook_KB)
		Me.Controls.Add(Me.TextBox1)
		Me.Controls.Add(Me.Btn_unhook)
		Me.Controls.Add(Me.Btn_hook)
		Me.Name = "Form1"
		Me.Text = "Form1"
		Me.ResumeLayout(False)
		Me.PerformLayout()

	End Sub
	Friend WithEvents Btn_hook As System.Windows.Forms.Button
	Friend WithEvents Btn_unhook As System.Windows.Forms.Button
	Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
	Friend WithEvents Btn_Hook_KB As System.Windows.Forms.Button
	Friend WithEvents Btn_Unhook_KB As System.Windows.Forms.Button
	Friend WithEvents Btn_Unhook_Mouse As System.Windows.Forms.Button
	Friend WithEvents Btn_Hook_Mouse As System.Windows.Forms.Button

End Class
