Public Class Agent_Commission
#Region "Global Veriables"
    Dim Conn As DbConnection = New SqlConnection
    Dim _objBusi As New Business
#End Region
#Region "Page Events"
    Private Sub Agent_Commission_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        If Conn.State = ConnectionState.Open Then
            Conn.Close()
        End If
    End Sub
    Private Sub Agent_Commission_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Conn.ConnectionString = My.Settings.SQL_Conn
        If Conn.State = ConnectionState.Closed Then
            Conn.Open()
        End If
        getACDetails()
        popAC()
        Me.gbAddAgentCommission.Visible = False
        Me.cbStatus.SelectedIndex = 0
        Me.cbStaffName.SelectedIndex = 0
    End Sub
#End Region
#Region "Data Bind"
    Private Sub popAC()
        'Agent         
        Dim _scInsurer As SqlCommand = CType(Conn.CreateCommand(), SqlCommand)
        Dim _ds As New DataSet
        _scInsurer.CommandType = CommandType.Text
        _scInsurer.CommandText = "Select * from tb_Agent where status='Active'"
        Dim _sda As New SqlDataAdapter(_scInsurer)
        _sda.Fill(_ds, "tb_Agent")

        Me.cbAgentName.DataSource = _ds.Tables(0)
        Me.cbAgentName.DisplayMember = "agent_name"
        Me.cbAgentName.ValueMember = "tb_agent_id"
        'Agent END
        'Product Name Type
        Dim _scProPlan As SqlCommand = CType(Conn.CreateCommand(), SqlCommand)
        Dim _dsProPlan As New DataSet
        _scProPlan.CommandType = CommandType.Text
        _scProPlan.CommandText = "Select * from tb_product_Plan_Type where status='Active'"
        Dim _sdaProPlan As New SqlDataAdapter(_scProPlan)
        _sdaProPlan.Fill(_dsProPlan, "tb_product_Plan_Type")
        Me.cbProductName.DataSource = _dsProPlan.Tables(0)
        Me.cbProductName.DisplayMember = "product_Plan_Type"
        Me.cbProductName.ValueMember = "product_Plan_Type"
        'END
    End Sub
    Private Sub getACDetails()
        SharedData.ReadyToHideMarquee = False
        StartMarqueeThread()
        Dim _ds As New DataSet
        Dim _scGetPlan As SqlCommand = CType(Conn.CreateCommand(), SqlCommand)
        _scGetPlan.CommandType = CommandType.Text
        '_scGetPlan.CommandText = "select tb_agent_commission_id,(select agent_name from tb_agent where tb_agent.tb_agent_id=tb_agent_commission.tb_agent_id) as Agent_Name,(select product_plan_type from tb_product_plan_type where  tb_product_plan_type.tb_product_plan_type_id=tb_agent_commission.Product_Name)as Product_Name , Deduction_Code, Staff_Name, Percentage, Status from tb_agent_commission"
        _scGetPlan.CommandText = "select tb_agent_commission_id,(select agent_name from tb_agent where tb_agent.tb_agent_id=tb_agent_commission.tb_agent_id) as Agent_Name, Product_Name , Staff_Name, Percentage, Status from tb_agent_commission"
        Dim _sdaGetPlan As New SqlDataAdapter(_scGetPlan)
        _sdaGetPlan.Fill(_ds, "tb_agent_commission")
        SyncLock SharedData
            SharedData.ReadyToHideMarquee = True
        End SyncLock
        Application.DoEvents()
        With Me.dgvAgentCommission
            .DataSource = _ds
            .DataMember = "tb_agent_commission"
            .Columns(0).Visible = False 'tb_agent_commission_id
            .Columns(1).HeaderText = "Agent Name"
            .Columns(2).HeaderText = "Product Name"
            '.Columns(3).HeaderText = "Deduction Code"
            .Columns(3).HeaderText = "Level"
            .Columns(4).HeaderText = "Percentage"
            .Columns(5).HeaderText = "Status"
        End With
    End Sub
#End Region
#Region "Click Events"
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
    Private Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        If Me.btnSubmit.Name = "Update" Then
            Update_Plan_Type()
        Else
            Insert_Plan_Type()
        End If
    End Sub
    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        Me.gbAddAgentCommission.Visible = True
        Me.btnSubmit.Text = "Submit"
        Me.btnSubmit.Name = "Submit"
        Me.btnEdit.Enabled = False
        Me.btnDelete.Enabled = False
    End Sub
    Private Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        If Me.dgvAgentCommission.SelectedRows.Count > 0 Then
            Dim ac_id As Integer
            ac_id = Me.dgvAgentCommission.SelectedRows(0).Cells(0).Value.ToString.Trim
            Dim _sqlCmdget_Pro As SqlCommand = CType(Conn.CreateCommand(), SqlCommand)
            _sqlCmdget_Pro.CommandType = CommandType.Text
            _sqlCmdget_Pro.CommandText = "SELECT * FROM tb_agent_commission where tb_agent_commission_id='" & ac_id & "'"
            Dim adp As New SqlDataAdapter(_sqlCmdget_Pro)
            Dim ds As New DataSet
            adp.Fill(ds, "tb_agent_commission")
            Me.lblACID.Text = ds.Tables(0).Rows(0)("tb_agent_commission_id")
            Me.lblACID.Visible = False
            Me.cbProductName.SelectedValue = ds.Tables(0).Rows(0)("Product_Name")
            'Me.txtDeductionCode.Text = ds.Tables(0).Rows(0)("Deduction_Code")
            Me.txtPercentage.Text = ds.Tables(0).Rows(0)("Percentage")
            Me.cbStaffName.SelectedValue = ds.Tables(0).Rows(0)("Staff_Name")
            Me.cbAgentName.SelectedValue = ds.Tables(0).Rows(0)("tb_Agent_ID")
            If ds.Tables(0).Rows(0)("Status").ToString.Trim() = "Active" Then
                Me.cbStatus.SelectedIndex = 0
            Else
                Me.cbStatus.SelectedIndex = 1
            End If
            gbAddAgentCommission.Visible = True
            Me.btnSubmit.Text = "Update"
            Me.btnSubmit.Name = "Update"
            Me.btnNew.Enabled = False
            Me.btnDelete.Enabled = False
        Else
            MsgBox("Please select the row for edit!")
        End If
    End Sub
    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If Me.dgvAgentCommission.SelectedRows.Count > 0 Then
            If MsgBox("Are you sure you want to delete?", vbYesNo, "Delete") = vbYes Then
                Dim Ac_id As Integer
                Dim sRes As String
                Ac_id = Me.dgvAgentCommission.SelectedRows(0).Cells(0).Value.ToString.Trim
                Dim _sqlCmddel_pro As SqlCommand = CType(Conn.CreateCommand(), SqlCommand)
                _sqlCmddel_pro.CommandType = CommandType.Text
                _sqlCmddel_pro.CommandText = "Delete FROM tb_agent_commission where tb_agent_commission_id='" & Ac_id & "'"
                sRes = _sqlCmddel_pro.ExecuteNonQuery()
                If sRes = "1" Then
                    MsgBox("Successfully Deleted the Agent Commission!")
                    getACDetails()
                Else
                    MsgBox("Error while Deleting the Agent Commission! or Currently our server busy please try again..")
                End If
            End If
        Else
            MsgBox("Please select the row for delete!")
        End If
    End Sub
#End Region
#Region "General Functions/Subs"
    Private Sub Clear()
        Me.cbProductName.SelectedIndex = 0
        Me.txtPercentage.Text = ""
        'Me.txtDeductionCode.Text = ""
        Me.cbStaffName.SelectedIndex = 0
        Me.cbStatus.SelectedIndex = 0
        Me.cbAgentName.SelectedIndex = 0
    End Sub
#End Region
#Region "Insert"
    Private Sub Insert_Plan_Type()
        Dim status, Percentage, Product_Name, Agent_Name, Deduction_Code, Staff_Name, S_NAME As String
        Dim Manager_ID, Supervisor_ID As Integer
        If Me.txtPercentage.Text.ToString.Trim() = "" Then
            MsgBox("Percentage Can't be Blank!")
            Me.txtPercentage.Focus()
            Exit Sub
        End If
        'If Me.txtDeductionCode.Text.ToString.Trim() = "" Then
        '    MsgBox("Deduction Code Can't be Blank!")
        '    Me.txtDeductionCode.Focus()
        '    Exit Sub
        'End If
        Product_Name = Me.cbProductName.SelectedValue
        'Deduction_Code = Me.txtDeductionCode.Text.ToString.Trim()
        Agent_Name = Me.cbAgentName.SelectedValue
        Staff_Name = Me.cbStaffName.SelectedIndex
        Percentage = Me.txtPercentage.Text.ToString.Trim()
        Manager_ID = Me.cbManager.SelectedValue
        Supervisor_ID = Me.cbSupervisor.SelectedValue
        Select Case Staff_Name
            Case "0"
                Staff_Name = "Agent"
            Case "1"
                Staff_Name = "Supervisor"
            Case "2"
                Staff_Name = "Manager"
        End Select
        status = Me.cbStatus.SelectedIndex
        Select Case status
            Case "0"
                status = "Active"
            Case "1"
                status = "Inactive"
        End Select
        Dim sRes As String
        SharedData.ReadyToHideMarquee = False
        StartMarqueeThread()
        
        Dim _scIns As SqlCommand = CType(Conn.CreateCommand(), SqlCommand)
        _scIns.CommandType = CommandType.Text
        _scIns.CommandText = "Insert into tb_agent_commission (tb_agent_id, Manager_ID, Supervisor_ID, product_name, staff_name, percentage, status, created_dt, created_by) " & _
                            "Values ('" & Agent_Name & "', '" & Manager_ID & "', '" & Supervisor_ID & "', '" & Product_Name & "', '" & Staff_Name & "', '" & Percentage & "', '" & status & "', '" & Format(Now(), "MM/dd/yyyy") & "', '" & My.Settings.Username & "')"
        sRes = _scIns.ExecuteNonQuery()
        SyncLock SharedData
            SharedData.ReadyToHideMarquee = True
        End SyncLock
        Application.DoEvents()
        If sRes = "1" Then
            MsgBox("Succesfully Inserted Agent Commission")
            getACDetails()
            Clear()
            Me.btnEdit.Enabled = True
            Me.btnDelete.Enabled = True
            Me.gbAddAgentCommission.Visible = False
        Else
            MsgBox("Error while Inserting Agent Commission")
        End If
    End Sub
#End Region
#Region "Update"
    Private Sub Update_Plan_Type()
        Dim status, Percentage, Product_Name, Agent_Name, Deduction_Code, Staff_Name, sRes, Manager_ID, Supervisor_ID, S_Name As String
        If Me.txtPercentage.Text.ToString.Trim() = "" Then
            MsgBox("Percentage Can't be Blank!")
            Me.txtPercentage.Focus()
            Exit Sub
        End If
        'If Me.txtDeductionCode.Text.ToString.Trim() = "" Then
        '    MsgBox("Deduction Code Can't be Blank!")
        '    Me.txtDeductionCode.Focus()
        '    Exit Sub
        'End If
        Product_Name = Me.cbProductName.SelectedValue
        'Deduction_Code = Me.txtDeductionCode.Text.ToString.Trim()
        Agent_Name = Me.cbAgentName.SelectedValue
        Staff_Name = Me.cbStaffName.SelectedIndex
        Percentage = Me.txtPercentage.Text.ToString.Trim()
        Manager_ID = Me.cbManager.SelectedValue
        Supervisor_ID = Me.cbSupervisor.SelectedValue
        status = Me.cbStatus.SelectedIndex
        Select Case Staff_Name
            Case "0"
                Staff_Name = "Agent"
            Case "1"
                Staff_Name = "Supervisor"
            Case "2"
                Staff_Name = "Manager"
        End Select
        Select Case status
            Case "0"
                status = "Active"
            Case "1"
                status = "Inactive"
        End Select
        SharedData.ReadyToHideMarquee = False
        StartMarqueeThread()
        
        Dim _scUps As SqlCommand = CType(Conn.CreateCommand(), SqlCommand)
        _scUps.CommandType = CommandType.Text
        _scUps.CommandText = "Update tb_agent_commission Set tb_agent_id='" & Agent_Name & "', Manager_ID='" & Manager_ID & "', Supervisor_ID='" & Supervisor_ID & "', product_name='" & Product_Name & "', Staff_Name='" & Staff_Name & "', Percentage='" & Percentage & "', status='" & status & "', modified_dt='" & Format(Now(), "MM/dd/yyyy") & "', modified_by='" & My.Settings.Username & "' where tb_agent_commission_id='" & Me.lblACID.Text & "'"
        sRes = _scUps.ExecuteNonQuery()
        SyncLock SharedData
            SharedData.ReadyToHideMarquee = True
        End SyncLock
        Application.DoEvents()
        If sRes = "1" Then
            MsgBox("Succesfully updated Agent Commission")
            getACDetails()
            Clear()
            Me.btnNew.Enabled = True
            Me.btnDelete.Enabled = True
            Me.gbAddAgentCommission.Visible = False
        Else
            MsgBox("Error while Updating Agent Commission")
        End If
    End Sub
#End Region
#Region "Change Events"
    Private Sub cbStaffName_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbStaffName.SelectedIndexChanged
        Select Case Me.cbStaffName.SelectedIndex
            Case "0"
                Me.cbManager.Enabled = True
                Me.cbSupervisor.Enabled = True
                Dim dt As New DataTable
                dt = _objBusi.getDetails("AGENT", Me.cbAgentName.SelectedValue, "", "", "", "MANAGER", Conn)
                If dt.Rows.Count > 0 Then
                    Me.cbManager.DataSource = dt
                    Me.cbManager.DisplayMember = "AGENT_NAME"
                    Me.cbManager.ValueMember = "TB_AGENT_ID"
                End If
            Case "1"
                Me.cbManager.Enabled = True
                Me.cbSupervisor.Enabled = False
            Case "2"
                Me.cbManager.Enabled = False
                Me.cbSupervisor.Enabled = False
        End Select
    End Sub
    Private Sub cbManager_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbManager.SelectedIndexChanged
        If Not Me.cbManager.SelectedValue.ToString = "System.Data.DataRowView" Then
            Dim dt As New DataTable
            dt = _objBusi.getDetails("AGENT", Me.cbAgentName.SelectedValue, Me.cbManager.SelectedValue, "", "", "SUPERVISOR", Conn)
            If dt.Rows.Count > 0 Then
                Me.cbSupervisor.DataSource = dt
                Me.cbSupervisor.DisplayMember = "NAME"
                Me.cbSupervisor.ValueMember = "TB_AGENT_COMMISSION_ID"
            End If
        End If
    End Sub
#End Region
#Region "Progress Bar"
    'SharedData.ReadyToHideMarquee = False
    'StartMarqueeThread()
    'EVENTS PLACE
    'SyncLock SharedData
    'SharedData.ReadyToHideMarquee = True
    'End SyncLock
    'Application.DoEvents()
    Private Shared SharedData As New SharedObject()
    Protected Overridable Sub StartMarqueeThread()
        Dim t As New Threading.Thread(AddressOf ShowMarqueeForm)
        t.Start()
    End Sub
    Protected Overridable Sub ShowMarqueeForm()
        Dim L As New Loading()
        L.Show()
        L.Update()
        Do
            SyncLock SharedData
                If SharedData.ReadyToHideMarquee Then
                    Exit Do
                End If
            End SyncLock
            Application.DoEvents()
        Loop
    End Sub
    Private Class SharedObject
        Public ReadyToHideMarquee As Boolean
    End Class
#End Region
End Class