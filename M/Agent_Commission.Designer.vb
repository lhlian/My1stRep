<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Agent_Commission
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnNew = New System.Windows.Forms.Button
        Me.gbAddAgentCommission = New System.Windows.Forms.GroupBox
        Me.cbSupervisor = New System.Windows.Forms.ComboBox
        Me.lblSupervisor1 = New System.Windows.Forms.Label
        Me.lblSupervisor = New System.Windows.Forms.Label
        Me.cbManager = New System.Windows.Forms.ComboBox
        Me.lblManager1 = New System.Windows.Forms.Label
        Me.lblManager = New System.Windows.Forms.Label
        Me.lblACID = New System.Windows.Forms.Label
        Me.cbProductName = New System.Windows.Forms.ComboBox
        Me.lblPercentage1 = New System.Windows.Forms.Label
        Me.lblPercentage = New System.Windows.Forms.Label
        Me.txtPercentage = New System.Windows.Forms.TextBox
        Me.cbStaffName = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.lblEmployee = New System.Windows.Forms.Label
        Me.cbAgentName = New System.Windows.Forms.ComboBox
        Me.lblAgentName1 = New System.Windows.Forms.Label
        Me.lblAgentName = New System.Windows.Forms.Label
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnSubmit = New System.Windows.Forms.Button
        Me.lblStatus1 = New System.Windows.Forms.Label
        Me.cbStatus = New System.Windows.Forms.ComboBox
        Me.lblStatus = New System.Windows.Forms.Label
        Me.lblProductName1 = New System.Windows.Forms.Label
        Me.lblProductName = New System.Windows.Forms.Label
        Me.gbAgentCommissionDetails = New System.Windows.Forms.GroupBox
        Me.dgvAgentCommission = New System.Windows.Forms.DataGridView
        Me.btnDelete = New System.Windows.Forms.Button
        Me.btnEdit = New System.Windows.Forms.Button
        Me.gbAddAgentCommission.SuspendLayout()
        Me.gbAgentCommissionDetails.SuspendLayout()
        CType(Me.dgvAgentCommission, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnNew
        '
        Me.btnNew.Location = New System.Drawing.Point(194, 175)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(75, 23)
        Me.btnNew.TabIndex = 11
        Me.btnNew.Text = "New"
        Me.btnNew.UseVisualStyleBackColor = True
        '
        'gbAddAgentCommission
        '
        Me.gbAddAgentCommission.Controls.Add(Me.cbSupervisor)
        Me.gbAddAgentCommission.Controls.Add(Me.lblSupervisor1)
        Me.gbAddAgentCommission.Controls.Add(Me.lblSupervisor)
        Me.gbAddAgentCommission.Controls.Add(Me.cbManager)
        Me.gbAddAgentCommission.Controls.Add(Me.lblManager1)
        Me.gbAddAgentCommission.Controls.Add(Me.lblManager)
        Me.gbAddAgentCommission.Controls.Add(Me.lblACID)
        Me.gbAddAgentCommission.Controls.Add(Me.cbProductName)
        Me.gbAddAgentCommission.Controls.Add(Me.lblPercentage1)
        Me.gbAddAgentCommission.Controls.Add(Me.lblPercentage)
        Me.gbAddAgentCommission.Controls.Add(Me.txtPercentage)
        Me.gbAddAgentCommission.Controls.Add(Me.cbStaffName)
        Me.gbAddAgentCommission.Controls.Add(Me.Label3)
        Me.gbAddAgentCommission.Controls.Add(Me.lblEmployee)
        Me.gbAddAgentCommission.Controls.Add(Me.cbAgentName)
        Me.gbAddAgentCommission.Controls.Add(Me.lblAgentName1)
        Me.gbAddAgentCommission.Controls.Add(Me.lblAgentName)
        Me.gbAddAgentCommission.Controls.Add(Me.btnCancel)
        Me.gbAddAgentCommission.Controls.Add(Me.btnSubmit)
        Me.gbAddAgentCommission.Controls.Add(Me.lblStatus1)
        Me.gbAddAgentCommission.Controls.Add(Me.cbStatus)
        Me.gbAddAgentCommission.Controls.Add(Me.lblStatus)
        Me.gbAddAgentCommission.Controls.Add(Me.lblProductName1)
        Me.gbAddAgentCommission.Controls.Add(Me.lblProductName)
        Me.gbAddAgentCommission.Location = New System.Drawing.Point(12, 224)
        Me.gbAddAgentCommission.Name = "gbAddAgentCommission"
        Me.gbAddAgentCommission.Size = New System.Drawing.Size(728, 272)
        Me.gbAddAgentCommission.TabIndex = 10
        Me.gbAddAgentCommission.TabStop = False
        Me.gbAddAgentCommission.Text = "Add Agent Commission "
        '
        'cbSupervisor
        '
        Me.cbSupervisor.FormattingEnabled = True
        Me.cbSupervisor.Location = New System.Drawing.Point(319, 146)
        Me.cbSupervisor.Name = "cbSupervisor"
        Me.cbSupervisor.Size = New System.Drawing.Size(265, 21)
        Me.cbSupervisor.TabIndex = 86
        '
        'lblSupervisor1
        '
        Me.lblSupervisor1.AutoSize = True
        Me.lblSupervisor1.Location = New System.Drawing.Point(283, 146)
        Me.lblSupervisor1.Name = "lblSupervisor1"
        Me.lblSupervisor1.Size = New System.Drawing.Size(10, 13)
        Me.lblSupervisor1.TabIndex = 85
        Me.lblSupervisor1.Text = ":"
        '
        'lblSupervisor
        '
        Me.lblSupervisor.AutoSize = True
        Me.lblSupervisor.Location = New System.Drawing.Point(83, 144)
        Me.lblSupervisor.Name = "lblSupervisor"
        Me.lblSupervisor.Size = New System.Drawing.Size(57, 13)
        Me.lblSupervisor.TabIndex = 84
        Me.lblSupervisor.Text = "Supervisor"
        '
        'cbManager
        '
        Me.cbManager.FormattingEnabled = True
        Me.cbManager.Location = New System.Drawing.Point(319, 117)
        Me.cbManager.Name = "cbManager"
        Me.cbManager.Size = New System.Drawing.Size(265, 21)
        Me.cbManager.TabIndex = 83
        '
        'lblManager1
        '
        Me.lblManager1.AutoSize = True
        Me.lblManager1.Location = New System.Drawing.Point(283, 119)
        Me.lblManager1.Name = "lblManager1"
        Me.lblManager1.Size = New System.Drawing.Size(10, 13)
        Me.lblManager1.TabIndex = 82
        Me.lblManager1.Text = ":"
        '
        'lblManager
        '
        Me.lblManager.AutoSize = True
        Me.lblManager.Location = New System.Drawing.Point(83, 117)
        Me.lblManager.Name = "lblManager"
        Me.lblManager.Size = New System.Drawing.Size(49, 13)
        Me.lblManager.TabIndex = 81
        Me.lblManager.Text = "Manager"
        '
        'lblACID
        '
        Me.lblACID.AutoSize = True
        Me.lblACID.Location = New System.Drawing.Point(704, 221)
        Me.lblACID.Name = "lblACID"
        Me.lblACID.Size = New System.Drawing.Size(18, 13)
        Me.lblACID.TabIndex = 14
        Me.lblACID.Text = "ID"
        Me.lblACID.Visible = False
        '
        'cbProductName
        '
        Me.cbProductName.FormattingEnabled = True
        Me.cbProductName.Location = New System.Drawing.Point(319, 37)
        Me.cbProductName.Name = "cbProductName"
        Me.cbProductName.Size = New System.Drawing.Size(265, 21)
        Me.cbProductName.TabIndex = 77
        '
        'lblPercentage1
        '
        Me.lblPercentage1.AutoSize = True
        Me.lblPercentage1.Location = New System.Drawing.Point(283, 175)
        Me.lblPercentage1.Name = "lblPercentage1"
        Me.lblPercentage1.Size = New System.Drawing.Size(10, 13)
        Me.lblPercentage1.TabIndex = 72
        Me.lblPercentage1.Text = ":"
        '
        'lblPercentage
        '
        Me.lblPercentage.AutoSize = True
        Me.lblPercentage.Location = New System.Drawing.Point(83, 173)
        Me.lblPercentage.Name = "lblPercentage"
        Me.lblPercentage.Size = New System.Drawing.Size(69, 13)
        Me.lblPercentage.TabIndex = 71
        Me.lblPercentage.Text = "Percentage *"
        '
        'txtPercentage
        '
        Me.txtPercentage.Location = New System.Drawing.Point(319, 173)
        Me.txtPercentage.Name = "txtPercentage"
        Me.txtPercentage.Size = New System.Drawing.Size(265, 20)
        Me.txtPercentage.TabIndex = 70
        '
        'cbStaffName
        '
        Me.cbStaffName.FormattingEnabled = True
        Me.cbStaffName.Items.AddRange(New Object() {"Agent", "Supervisor", "Manager"})
        Me.cbStaffName.Location = New System.Drawing.Point(319, 90)
        Me.cbStaffName.Name = "cbStaffName"
        Me.cbStaffName.Size = New System.Drawing.Size(265, 21)
        Me.cbStaffName.TabIndex = 69
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(283, 92)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(10, 13)
        Me.Label3.TabIndex = 68
        Me.Label3.Text = ":"
        '
        'lblEmployee
        '
        Me.lblEmployee.AutoSize = True
        Me.lblEmployee.Location = New System.Drawing.Point(83, 90)
        Me.lblEmployee.Name = "lblEmployee"
        Me.lblEmployee.Size = New System.Drawing.Size(40, 13)
        Me.lblEmployee.TabIndex = 67
        Me.lblEmployee.Text = "Level *"
        '
        'cbAgentName
        '
        Me.cbAgentName.FormattingEnabled = True
        Me.cbAgentName.Location = New System.Drawing.Point(319, 63)
        Me.cbAgentName.Name = "cbAgentName"
        Me.cbAgentName.Size = New System.Drawing.Size(265, 21)
        Me.cbAgentName.TabIndex = 66
        '
        'lblAgentName1
        '
        Me.lblAgentName1.AutoSize = True
        Me.lblAgentName1.Location = New System.Drawing.Point(283, 65)
        Me.lblAgentName1.Name = "lblAgentName1"
        Me.lblAgentName1.Size = New System.Drawing.Size(10, 13)
        Me.lblAgentName1.TabIndex = 65
        Me.lblAgentName1.Text = ":"
        '
        'lblAgentName
        '
        Me.lblAgentName.AutoSize = True
        Me.lblAgentName.Location = New System.Drawing.Point(83, 63)
        Me.lblAgentName.Name = "lblAgentName"
        Me.lblAgentName.Size = New System.Drawing.Size(73, 13)
        Me.lblAgentName.TabIndex = 64
        Me.lblAgentName.Text = "Agent Name *"
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(383, 231)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 62
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnSubmit
        '
        Me.btnSubmit.Location = New System.Drawing.Point(272, 231)
        Me.btnSubmit.Name = "btnSubmit"
        Me.btnSubmit.Size = New System.Drawing.Size(75, 23)
        Me.btnSubmit.TabIndex = 61
        Me.btnSubmit.Text = "Submit"
        Me.btnSubmit.UseVisualStyleBackColor = True
        '
        'lblStatus1
        '
        Me.lblStatus1.AutoSize = True
        Me.lblStatus1.Location = New System.Drawing.Point(283, 202)
        Me.lblStatus1.Name = "lblStatus1"
        Me.lblStatus1.Size = New System.Drawing.Size(10, 13)
        Me.lblStatus1.TabIndex = 60
        Me.lblStatus1.Text = ":"
        '
        'cbStatus
        '
        Me.cbStatus.FormattingEnabled = True
        Me.cbStatus.Items.AddRange(New Object() {"Active", "Inactive"})
        Me.cbStatus.Location = New System.Drawing.Point(319, 199)
        Me.cbStatus.Name = "cbStatus"
        Me.cbStatus.Size = New System.Drawing.Size(265, 21)
        Me.cbStatus.TabIndex = 59
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Location = New System.Drawing.Point(83, 200)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(37, 13)
        Me.lblStatus.TabIndex = 58
        Me.lblStatus.Text = "Status"
        '
        'lblProductName1
        '
        Me.lblProductName1.AutoSize = True
        Me.lblProductName1.Location = New System.Drawing.Point(283, 39)
        Me.lblProductName1.Name = "lblProductName1"
        Me.lblProductName1.Size = New System.Drawing.Size(10, 13)
        Me.lblProductName1.TabIndex = 1
        Me.lblProductName1.Text = ":"
        '
        'lblProductName
        '
        Me.lblProductName.AutoSize = True
        Me.lblProductName.Location = New System.Drawing.Point(83, 37)
        Me.lblProductName.Name = "lblProductName"
        Me.lblProductName.Size = New System.Drawing.Size(82, 13)
        Me.lblProductName.TabIndex = 0
        Me.lblProductName.Text = "Product Name *"
        '
        'gbAgentCommissionDetails
        '
        Me.gbAgentCommissionDetails.Controls.Add(Me.dgvAgentCommission)
        Me.gbAgentCommissionDetails.Location = New System.Drawing.Point(12, 24)
        Me.gbAgentCommissionDetails.Name = "gbAgentCommissionDetails"
        Me.gbAgentCommissionDetails.Size = New System.Drawing.Size(728, 144)
        Me.gbAgentCommissionDetails.TabIndex = 9
        Me.gbAgentCommissionDetails.TabStop = False
        Me.gbAgentCommissionDetails.Text = "Agent Commission Details"
        '
        'dgvAgentCommission
        '
        Me.dgvAgentCommission.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvAgentCommission.Location = New System.Drawing.Point(23, 20)
        Me.dgvAgentCommission.Name = "dgvAgentCommission"
        Me.dgvAgentCommission.Size = New System.Drawing.Size(666, 105)
        Me.dgvAgentCommission.TabIndex = 0
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(407, 175)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(75, 23)
        Me.btnDelete.TabIndex = 13
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'btnEdit
        '
        Me.btnEdit.Location = New System.Drawing.Point(296, 175)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(75, 23)
        Me.btnEdit.TabIndex = 12
        Me.btnEdit.Text = "Edit"
        Me.btnEdit.UseVisualStyleBackColor = True
        '
        'Agent_Commission
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(764, 556)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnEdit)
        Me.Controls.Add(Me.btnNew)
        Me.Controls.Add(Me.gbAddAgentCommission)
        Me.Controls.Add(Me.gbAgentCommissionDetails)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "Agent_Commission"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Agent_Commission"
        Me.gbAddAgentCommission.ResumeLayout(False)
        Me.gbAddAgentCommission.PerformLayout()
        Me.gbAgentCommissionDetails.ResumeLayout(False)
        CType(Me.dgvAgentCommission, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents gbAddAgentCommission As System.Windows.Forms.GroupBox
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnSubmit As System.Windows.Forms.Button
    Friend WithEvents lblStatus1 As System.Windows.Forms.Label
    Friend WithEvents cbStatus As System.Windows.Forms.ComboBox
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents lblProductName1 As System.Windows.Forms.Label
    Friend WithEvents lblProductName As System.Windows.Forms.Label
    Friend WithEvents gbAgentCommissionDetails As System.Windows.Forms.GroupBox
    Friend WithEvents dgvAgentCommission As System.Windows.Forms.DataGridView
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnEdit As System.Windows.Forms.Button
    Friend WithEvents lblPercentage1 As System.Windows.Forms.Label
    Friend WithEvents lblPercentage As System.Windows.Forms.Label
    Friend WithEvents txtPercentage As System.Windows.Forms.TextBox
    Friend WithEvents cbStaffName As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblEmployee As System.Windows.Forms.Label
    Friend WithEvents cbAgentName As System.Windows.Forms.ComboBox
    Friend WithEvents lblAgentName1 As System.Windows.Forms.Label
    Friend WithEvents lblAgentName As System.Windows.Forms.Label
    Friend WithEvents lblACID As System.Windows.Forms.Label
    Friend WithEvents cbProductName As System.Windows.Forms.ComboBox
    Friend WithEvents cbSupervisor As System.Windows.Forms.ComboBox
    Friend WithEvents lblSupervisor1 As System.Windows.Forms.Label
    Friend WithEvents lblSupervisor As System.Windows.Forms.Label
    Friend WithEvents cbManager As System.Windows.Forms.ComboBox
    Friend WithEvents lblManager1 As System.Windows.Forms.Label
    Friend WithEvents lblManager As System.Windows.Forms.Label
End Class
