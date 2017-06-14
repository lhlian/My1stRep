'********************************************
'*********    Created By SRI     ************
'********* Created Date 01062011 ************
'****** Purpose Agent Master Details ******
'********************************************
Public Class Agent
#Region "Global Veriables"
    Dim Conn As DbConnection = New SqlConnection
    Dim _objBusi As New Business
    Dim ID As String
#End Region
#Region "Page Events"
    Private Sub Agent_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        If Conn.State = ConnectionState.Open Then
            Conn.Close()
        End If
    End Sub
    Private Sub Agent_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Conn.ConnectionString = My.Settings.SQL_Conn
        If Conn.State = ConnectionState.Closed Then
            Conn.Open()
        End If
        gbAgent.Visible = False
        getAgentdetails()
        Me.lblAgentId.Visible = False
        Me.cbStatus.SelectedIndex = 0
        Me.txtState.Enabled = False
        popCB()
        Me.btnPrint.Enabled = False
    End Sub
#End Region
#Region "Data Bind"
    Private Sub popCB()
        Dim dtProduct As New DataTable
        dtProduct = _objBusi.getDetails("AGENT", "", "", "", "", "PRODUCT", Conn)
        If dtProduct.Rows.Count > 0 Then
            Me.cbProduct.DataSource = dtProduct
            Me.cbProduct.DisplayMember = "PRODUCT_PLAN_TYPE"
            Me.cbProduct.ValueMember = "TB_PRODUCT_PLAN_TYPE_ID"
        End If
    End Sub
    Private Sub getManager()
        Dim dt As New DataTable
        dt = _objBusi.getDetails("AGENT", "", "", "", "", "MANAGER", Conn)
        If dt.Rows.Count > 0 Then
            Me.cbManager.DataSource = dt
            Me.cbManager.DisplayMember = "AGENT_NAME"
            Me.cbManager.ValueMember = "TB_AGENT_ID"
        Else
            Me.cbManager.DisplayMember = Nothing
            Me.cbManager.ValueMember = Nothing
        End If
        If Not Me.cbManager.SelectedValue.ToString = "System.Data.DataRowView" Then
            Dim dt1 As New DataTable
            dt1 = _objBusi.getDetails("AGENT", Me.cbManager.SelectedValue, "", "", "", "SUPERVISOR", Conn)
            If dt1.Rows.Count > 0 Then
                Me.cbSupervisor.DataSource = dt1
                Me.cbSupervisor.DisplayMember = "AGENT_NAME"
                Me.cbSupervisor.ValueMember = "TB_AGENT_ID"
            Else
                Me.cbSupervisor.DisplayMember = Nothing
                Me.cbSupervisor.ValueMember = Nothing
            End If
        End If
    End Sub
#Region "Agent Details"
    Private Sub getAgentdetails()
        Dim _sqlCmdget_ins As SqlCommand = CType(Conn.CreateCommand(), SqlCommand)
        _sqlCmdget_ins.CommandType = CommandType.Text
        _sqlCmdget_ins.CommandText = "SELECT tb_Agent_id, AGENT_CODE, Agent_name, add1, email, tel, status FROM tb_Agent "
        Dim adp As New SqlDataAdapter(_sqlCmdget_ins)
        Dim ds As New DataSet
        adp.Fill(ds, "tb_Agent")
        With Me.dgvAgent
            .DataSource = ds
            .DataMember = "tb_Agent"
            .Columns(0).HeaderText = "View Info"
            .Columns(1).Visible = False
            .Columns(2).HeaderText = "Agent Code"
            .Columns(3).HeaderText = "Agent Name"
            .Columns(4).HeaderText = "Address"
            .Columns(5).HeaderText = "Email"
            .Columns(6).HeaderText = "Telephone"
            .Columns(7).HeaderText = "Status"
            .Columns(0).DisplayIndex = 7
        End With
    End Sub
#End Region
#End Region
#Region "Click Events"
    Private Sub btnSubmit_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        If Me.btnSubmit.Name = "Update" Then
            Update_Agent_details()
            Me.btnNew.Enabled = True
            Me.btnDelete.Enabled = True
            Me.btnEdit.Enabled = True
            Me.btnAdd.Enabled = True
        Else
            If Me.txtNRIC.Text.Trim() = "" Then
                MsgBox("NRIC Can't be Blank!")
                Me.txtNRIC.Focus()
                Exit Sub
            End If
            Dim dt As New DataTable
            dt = _objBusi.fgetDetailsII("AGENT", Me.txtNRIC.Text.Trim(), Me.cbProduct.SelectedValue, "", "", "", "", "", "", "", "CHKEXISTS", Conn)
            If dt.Rows.Count > 0 Then
                MsgBox("This Agent already exists on plan, Please choose a different plan!" + Me.txtNRIC.Text.Trim())
                Exit Sub
            End If
            Insert_Agent_details()
            Me.btnNew.Enabled = True
            Me.btnDelete.Enabled = True
            Me.btnEdit.Enabled = True
            Me.btnAdd.Enabled = True
        End If
    End Sub
    Private Sub btnCancel_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
    Private Sub btnNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNew.Click
        clear_all()
        gbAgent.Visible = True
        popCB()
        Me.btnSubmit.Text = "Submit"
        Me.btnSubmit.Name = "Submit"
        Me.lblAgentId.Visible = False
        Me.btnSubmit.Enabled = True
        Me.btnPrint.Enabled = False
        Me.btnAdd.Enabled = False
        Me.btnEdit.Enabled = False
        Me.btnDelete.Enabled = False
    End Sub
    Private Sub btnEdit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        clear_all()
        If Me.dgvAgent.SelectedRows.Count > 0 Then
            Dim ins_id As Integer
            Me.btnPrint.Enabled = True
            ins_id = Me.dgvAgent.SelectedRows(0).Cells(1).Value.ToString.Trim
            Dim _sqlCmdget_ins As SqlCommand = CType(Conn.CreateCommand(), SqlCommand)
            _sqlCmdget_ins.CommandType = CommandType.Text
            _sqlCmdget_ins.CommandText = "SELECT * FROM tb_Agent where tb_Agent_id='" & ins_id & "'"
            Dim adp As New SqlDataAdapter(_sqlCmdget_ins)
            Dim ds As New DataSet
            adp.Fill(ds, "tb_Agent")
            Me.lblAgentId.Text = ds.Tables(0).Rows(0)("tb_Agent_id")
            Me.lblAgentId.Visible = False
            ID = ds.Tables(0).Rows(0)("Agent_id")
            Me.txtAgent.Text = ds.Tables(0).Rows(0)("Agent_name")

            If Not IsDBNull(ds.Tables(0).Rows(0)("NRIC")) Then
                Me.txtNRIC.Text = ds.Tables(0).Rows(0)("NRIC")
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("race")) Then
                Select Case ds.Tables(0).Rows(0)("race")
                    Case "M"
                        Me.rdiRace_Malay.Checked = True
                    Case "C"
                        Me.rdiRace_Chinese.Checked = True
                    Case "I"
                        Me.rdiRace_Indian.Checked = True
                    Case "O"
                        Me.rdiRace_Others.Checked = True
                End Select
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0)("Marital_Status")) Then
                Select Case ds.Tables(0).Rows(0)("Marital_Status")
                    Case "S"
                        Me.rdiMaritalStatus_Single.Checked = True
                    Case "M"
                        Me.rdiMaritalStatus_Married.Checked = True
                    Case "W"
                        Me.rdiMaritalStatus_Widowed.Checked = True
                    Case "D"
                        Me.rdiMaritalStatus_Divorced.Checked = True
                End Select

            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("DOB")) Then
                Me.dtpDOB.Value = ds.Tables(0).Rows(0)("DOB")
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("appointment_date")) Then
                Me.dtpAppointmentDate.Value = ds.Tables(0).Rows(0)("appointment_date")
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("application_date")) Then
                Me.dtpDateOfApplication.Value = ds.Tables(0).Rows(0)("application_date")
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("Age")) Then
                Me.txtAge.Text = ds.Tables(0).Rows(0)("Age")
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("GENDER")) Then
                If ds.Tables(0).Rows(0)("GENDER") = "MALE" Then
                    Me.rdiSex_Male.Checked = True
                Else
                    Me.rdiSex_Female.Checked = True
                End If
            End If


            If Not IsDBNull(ds.Tables(0).Rows(0)("contact_office")) Then
                Me.txtContactOffice.Text = ds.Tables(0).Rows(0)("contact_office")
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0)("MOBILE")) Then
                Me.txtMobile.Text = ds.Tables(0).Rows(0)("MOBILE")
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0)("Designation")) Then
                Me.txtDesig.Text = ds.Tables(0).Rows(0)("Designation")
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("add1")) Then
                Me.txtAdd1.Text = ds.Tables(0).Rows(0)("add1")
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("add2")) Then
                Me.txtAdd2.Text = ds.Tables(0).Rows(0)("add2")
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0)("Town")) Then
                Me.txtTown.Text = ds.Tables(0).Rows(0)("Town")
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("state")) Then
                Me.txtState.Text = ds.Tables(0).Rows(0)("state")
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("Postcode")) Then
                Me.txtPostCode.Text = ds.Tables(0).Rows(0)("Postcode")
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("BankAc")) Then
                Me.txtBankAc.Text = ds.Tables(0).Rows(0)("BankAc")
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("Email")) Then
                Me.txtEmail.Text = ds.Tables(0).Rows(0)("Email")
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("tel")) Then
                Me.txtTel.Text = ds.Tables(0).Rows(0)("tel")
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("fax")) Then
                Me.txtFax.Text = ds.Tables(0).Rows(0)("fax")
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("Agent_code")) Then
                Me.txtAgentCode.Text = ds.Tables(0).Rows(0)("Agent_code")
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0)("Agent")) Then
                If ds.Tables(0).Rows(0)("Agent") Then
                    Me.rbAgentLevel_Agent.Checked = True
                Else
                    Me.rbAgentLevel_Agent.Checked = False
                End If
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("Supervisor")) Then
                If ds.Tables(0).Rows(0)("Supervisor") Then
                    Me.rbAgentLevel_Supervisor.Checked = True
                Else
                    Me.rbAgentLevel_Supervisor.Checked = False
                End If
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("Manager")) Then
                If ds.Tables(0).Rows(0)("Manager") Then
                    Me.rbAgentLevel_Manager.Checked = True
                Else
                    Me.rbAgentLevel_Manager.Checked = False
                End If
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("status")) Then
                If ds.Tables(0).Rows(0)("status") = "Active" Then
                    Me.cbStatus.SelectedIndex = 0
                Else
                    Me.cbStatus.SelectedIndex = 1
                End If
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("Percentage")) Then
                Me.txtPercentage.Text = ds.Tables(0).Rows(0)("Percentage")
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("Product_name")) Then
                Me.cbProduct.SelectedValue = ds.Tables(0).Rows(0)("Product_name")
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("Manager_ID")) Then
                Me.cbManager.SelectedValue = ds.Tables(0).Rows(0)("Manager_ID")
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("Supervisor_ID")) Then
                Me.cbSupervisor.SelectedValue = ds.Tables(0).Rows(0)("Supervisor_ID")
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("Spouse_Name")) Then
                Me.txtSpouseName.Text = ds.Tables(0).Rows(0)("Spouse_Name")
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("Spouse_Desig")) Then
                Me.txtSDesig.Text = ds.Tables(0).Rows(0)("Spouse_Desig")
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("Spouse_Tel")) Then
                Me.txtSTel.Text = ds.Tables(0).Rows(0)("Spouse_Tel")
            End If
            gbAgent.Visible = True
            Me.btnSubmit.Text = "Update"
            Me.btnSubmit.Name = "Update"
            Me.btnNew.Enabled = False
            Me.btnAdd.Enabled = False
            Me.btnDelete.Enabled = False
            Me.btnSubmit.Enabled = True
            Me.btnPrint.Enabled = False
        Else
            MsgBox("Please select the row for edit!")
        End If
    End Sub
    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If Me.dgvAgent.SelectedRows.Count > 0 Then
            If MsgBox("Are you sure you want to delete?", vbYesNo, "Delete") = vbYes Then
                Dim ins_id As Integer
                Dim sRes As String
                ins_id = Me.dgvAgent.SelectedRows(0).Cells(1).Value.ToString.Trim
                Dim _sqlCmddel_ins As SqlCommand = CType(Conn.CreateCommand(), SqlCommand)
                _sqlCmddel_ins.CommandType = CommandType.Text
                _sqlCmddel_ins.CommandText = "Delete FROM tb_Agent where tb_Agent_id='" & ins_id & "'"
                sRes = _sqlCmddel_ins.ExecuteNonQuery()
                If sRes = "1" Then
                    MsgBox("Successfully Deleted the Agent!")
                    getAgentdetails()
                Else
                    MsgBox("Error while Deleting the Agent! or Currently our server busy please try again..")
                End If
            End If
        Else
            MsgBox("Please select the row for delete!")
        End If
    End Sub
    Private Sub dgvAgent_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvAgent.CellContentClick
        With Me.dgvAgent
            If e.ColumnIndex = 0 Then
                If .Rows.Count = 0 Then
                    Exit Sub
                End If
                Dim View_Agent_Details As New View_Agent_Details
                View_Agent_Details.lblAID.Text = .Rows(e.RowIndex).Cells(1).Value.ToString()
                View_Agent_Details.Show()
            End If
        End With
    End Sub
    Private Sub btnState_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnState.Click
        Dim frmSearch_State As New frmSearch_Param
        frmSearch_State.lblForm_Flag.Text = "S"
        frmSearch_State.ShowDialog()
        Me.txtState.Text = My.Computer.Clipboard.GetText()
        My.Computer.Clipboard.Clear()
    End Sub
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        PrintLetters.PrintProposer("rAgent.rpt", ID, "AGENT")
        gbAgent.Visible = False
        clear_all()
    End Sub
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        clear_all()
        If Me.dgvAgent.SelectedRows.Count > 0 Then
            Dim ins_id As Integer
            Me.btnPrint.Enabled = True
            ins_id = Me.dgvAgent.SelectedRows(0).Cells(1).Value.ToString.Trim
            Dim _sqlCmdget_ins As SqlCommand = CType(Conn.CreateCommand(), SqlCommand)
            _sqlCmdget_ins.CommandType = CommandType.Text
            _sqlCmdget_ins.CommandText = "SELECT * FROM tb_Agent where tb_Agent_id='" & ins_id & "'"
            Dim adp As New SqlDataAdapter(_sqlCmdget_ins)
            Dim ds As New DataSet
            adp.Fill(ds, "tb_Agent")
            Me.lblAgentId.Text = ds.Tables(0).Rows(0)("tb_Agent_id")
            Me.lblAgentId.Visible = False
            ID = ds.Tables(0).Rows(0)("Agent_id")
            Me.txtAgent.Text = ds.Tables(0).Rows(0)("Agent_name")
            Me.txtAgentCode.ReadOnly = True

            If Not IsDBNull(ds.Tables(0).Rows(0)("NRIC")) Then
                Me.txtNRIC.Text = ds.Tables(0).Rows(0)("NRIC")
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("race")) Then
                Select Case ds.Tables(0).Rows(0)("race")
                    Case "M"
                        Me.rdiRace_Malay.Checked = True
                    Case "C"
                        Me.rdiRace_Chinese.Checked = True
                    Case "I"
                        Me.rdiRace_Indian.Checked = True
                    Case "O"
                        Me.rdiRace_Others.Checked = True
                End Select
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0)("Marital_Status")) Then
                Select Case ds.Tables(0).Rows(0)("Marital_Status")
                    Case "S"
                        Me.rdiMaritalStatus_Single.Checked = True
                    Case "M"
                        Me.rdiMaritalStatus_Married.Checked = True
                    Case "W"
                        Me.rdiMaritalStatus_Widowed.Checked = True
                    Case "D"
                        Me.rdiMaritalStatus_Divorced.Checked = True
                End Select

            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("DOB")) Then
                Me.dtpDOB.Value = ds.Tables(0).Rows(0)("DOB")
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("appointment_date")) Then
                Me.dtpAppointmentDate.Value = ds.Tables(0).Rows(0)("appointment_date")
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("application_date")) Then
                Me.dtpDateOfApplication.Value = ds.Tables(0).Rows(0)("application_date")
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("Age")) Then
                Me.txtAge.Text = ds.Tables(0).Rows(0)("Age")
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("GENDER")) Then
                If ds.Tables(0).Rows(0)("GENDER") = "MALE" Then
                    Me.rdiSex_Male.Checked = True
                Else
                    Me.rdiSex_Female.Checked = True
                End If
            End If


            If Not IsDBNull(ds.Tables(0).Rows(0)("contact_office")) Then
                Me.txtContactOffice.Text = ds.Tables(0).Rows(0)("contact_office")
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0)("MOBILE")) Then
                Me.txtMobile.Text = ds.Tables(0).Rows(0)("MOBILE")
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0)("Designation")) Then
                Me.txtDesig.Text = ds.Tables(0).Rows(0)("Designation")
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("add1")) Then
                Me.txtAdd1.Text = ds.Tables(0).Rows(0)("add1")
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("add2")) Then
                Me.txtAdd2.Text = ds.Tables(0).Rows(0)("add2")
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0)("Town")) Then
                Me.txtTown.Text = ds.Tables(0).Rows(0)("Town")
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("state")) Then
                Me.txtState.Text = ds.Tables(0).Rows(0)("state")
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("Postcode")) Then
                Me.txtPostCode.Text = ds.Tables(0).Rows(0)("Postcode")
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("BankAc")) Then
                Me.txtBankAc.Text = ds.Tables(0).Rows(0)("BankAc")
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("Email")) Then
                Me.txtEmail.Text = ds.Tables(0).Rows(0)("Email")
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("tel")) Then
                Me.txtTel.Text = ds.Tables(0).Rows(0)("tel")
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("fax")) Then
                Me.txtFax.Text = ds.Tables(0).Rows(0)("fax")
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("Agent_code")) Then
                Me.txtAgentCode.Text = ds.Tables(0).Rows(0)("Agent_code")
            End If

            If Not IsDBNull(ds.Tables(0).Rows(0)("Agent")) Then
                If ds.Tables(0).Rows(0)("Agent") Then
                    Me.rbAgentLevel_Agent.Checked = True
                Else
                    Me.rbAgentLevel_Agent.Checked = False
                End If
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("Supervisor")) Then
                If ds.Tables(0).Rows(0)("Supervisor") Then
                    Me.rbAgentLevel_Supervisor.Checked = True
                Else
                    Me.rbAgentLevel_Supervisor.Checked = False
                End If
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("Manager")) Then
                If ds.Tables(0).Rows(0)("Manager") Then
                    Me.rbAgentLevel_Manager.Checked = True
                Else
                    Me.rbAgentLevel_Manager.Checked = False
                End If
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("status")) Then
                If ds.Tables(0).Rows(0)("status") = "Active" Then
                    Me.cbStatus.SelectedIndex = 0
                Else
                    Me.cbStatus.SelectedIndex = 1
                End If
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("Percentage")) Then
                Me.txtPercentage.Text = ds.Tables(0).Rows(0)("Percentage")
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("Product_name")) Then
                Me.cbProduct.SelectedValue = ds.Tables(0).Rows(0)("Product_name")
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("Manager_ID")) Then
                Me.cbManager.SelectedValue = ds.Tables(0).Rows(0)("Manager_ID")
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("Supervisor_ID")) Then
                Me.cbSupervisor.SelectedValue = ds.Tables(0).Rows(0)("Supervisor_ID")
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("Spouse_Name")) Then
                Me.txtSpouseName.Text = ds.Tables(0).Rows(0)("Spouse_Name")
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("Spouse_Desig")) Then
                Me.txtSDesig.Text = ds.Tables(0).Rows(0)("Spouse_Desig")
            End If
            If Not IsDBNull(ds.Tables(0).Rows(0)("Spouse_Tel")) Then
                Me.txtSTel.Text = ds.Tables(0).Rows(0)("Spouse_Tel")
            End If
            gbAgent.Visible = True
            Me.btnSubmit.Text = "Submit"
            Me.btnSubmit.Name = "Submit"
            Me.btnAdd.Enabled = True
            Me.btnNew.Enabled = False
            Me.btnEdit.Enabled = False
            Me.btnDelete.Enabled = False
            Me.btnSubmit.Enabled = True
            Me.btnPrint.Enabled = False
        Else
            MsgBox("Please select the row for add another plan for agent!")
        End If
    End Sub
#End Region
#Region "Insert Agent"
    Private Function Insert_Agent_details() As String
        Dim agent_code, desig, Agent_name, add1, add2, town, agent, supervisor, Manager, postcode, email, bank_ac, agent_level, state, tel, fax, status, s_name, s_desig, s_Tel, created_by, sRes, ins_id As String
        Dim Manager_ID, Supervisor_ID, Percentage, Product_Name As String

        If Me.txtNRIC.Text.Trim() = "" Then
            MsgBox("NRIC Can't be Blank!")
            Me.txtNRIC.Focus()
            Exit Function
        End If
        If Me.txtAge.Text.Trim() = "" Then
            MsgBox("Please do key DOB!")
            Me.dtpDOB.Focus()
            Exit Function
        End If
        If Me.txtAgentCode.Text.Trim() = "" Then
            MsgBox("Agent Code Can't be Blank!")
            Me.txtAgentCode.Focus()
            Exit Function
        End If
        If Me.txtAgent.Text.Trim() = "" Then
            MsgBox("Agent Name Can't be Blank!")
            Me.txtAgent.Focus()
            Exit Function
        End If
        If Me.txtAdd1.Text.Trim() = "" Then
            MsgBox("Address1 Can't be Blank!")
            Me.txtAdd1.Focus()
            Exit Function
        End If
        If Me.txtPostCode.Text.Trim() = "" Then
            MsgBox("Post Code Can't be Blank!")
            Me.txtPostCode.Focus()
            Exit Function
        End If
        If Me.txtBankAc.Text.Trim() = "" Then
            MsgBox("Bank A/c Can't be Blank!")
            Me.txtTown.Focus()
            Exit Function
        End If
        If Me.txtEmail.Text.Trim() = "" Then
            MsgBox("Email Can't be Blank!")
            Me.txtEmail.Focus()
            Exit Function
        End If
        If Me.txtTel.Text.Trim() = "" Then
            MsgBox("Telephone Number Can't be Blank!")
            Me.txtTel.Focus()
            Exit Function
        End If
        If Me.rbAgentLevel_Agent.Checked = False Then
            If Me.rbAgentLevel_Manager.Checked = False Then
                If Me.rbAgentLevel_Supervisor.Checked = False Then
                    MsgBox("Level Can't be Blank!")
                    Me.txtTel.Focus()
                    Exit Function
                End If
            End If
        End If
        If Me.txtPercentage.Text.ToString.Trim() = "" Then
            MsgBox("Percentage Can't be Blank!")
            Me.txtPercentage.Focus()
            Exit Function
        End If
        Agent_name = Me.txtAgent.Text.Trim()
        desig = Me.txtDesig.Text.Trim()
        add1 = Me.txtAdd1.Text.Trim()
        add2 = Me.txtAdd2.Text.Trim()
        town = Me.txtTown.Text.Trim()
        state = Me.txtState.Text.Trim()
        postcode = Me.txtPostCode.Text.Trim()
        bank_ac = Me.txtBankAc.Text.Trim()
        email = Me.txtEmail.Text.Trim()
        tel = Me.txtTel.Text.Trim()
        fax = Me.txtFax.Text.Trim()
        agent_code = Me.txtAgentCode.Text.Trim()
        s_name = Me.txtSpouseName.Text.Trim()
        s_desig = Me.txtSDesig.Text.Trim()
        s_Tel = Me.txtSTel.Text.Trim()
        status = Me.cbStatus.SelectedIndex

        Dim Race, MStatus, Gender As String
        Dim Age As Integer
        If Me.rdiRace_Malay.Checked = True Then
            Race = "M"
        ElseIf Me.rdiRace_Chinese.Checked = True Then
            Race = "C"
        ElseIf Me.rdiRace_Indian.Checked = True Then
            Race = "I"
        ElseIf Me.rdiRace_Others.Checked = True Then
            Race = "O"
        End If

        If Me.rdiMaritalStatus_Single.Checked = True Then
            MStatus = "S"
        ElseIf Me.rdiMaritalStatus_Married.Checked = True Then
            MStatus = "M"
        ElseIf Me.rdiMaritalStatus_Widowed.Checked = True Then
            MStatus = "W"
        ElseIf Me.rdiMaritalStatus_Divorced.Checked = True Then
            MStatus = "D"
        End If

        If Me.rdiSex_Male.Checked = True Then
            Gender = "MALE"
        ElseIf Me.rdiSex_Female.Checked = True Then
            Gender = "FEMALE"
        End If

        Select Case status
            Case "0"
                status = "Active"
            Case "1"
                status = "Inactive"
        End Select
        Age = Me.txtAge.Text

        If Me.rbAgentLevel_Agent.Checked Then
            agent = 1
            Manager_ID = Me.cbManager.SelectedValue
            Supervisor_ID = Me.cbSupervisor.SelectedValue
        ElseIf Me.rbAgentLevel_Supervisor.Checked Then
            supervisor = 1
            Manager_ID = Me.cbManager.SelectedValue
            Supervisor_ID = 0
        ElseIf Me.rbAgentLevel_Manager.Checked Then
            Manager = 1
            Manager_ID = 0
            Supervisor_ID = 0
        Else
            agent = 0
            Manager_ID = 0
            Supervisor_ID = 0
        End If
        Percentage = Me.txtPercentage.Text.ToString.Trim
        Product_Name = Me.cbProduct.SelectedValue
        created_by = My.Settings.Username
        SharedData.ReadyToHideMarquee = False
        StartMarqueeThread()
        Dim _scID As SqlCommand = CType(Conn.CreateCommand(), SqlCommand)
        _scID.CommandType = CommandType.Text
        _scID.CommandText = "select  max(tb_Agent_id) + 1 as Agent_id from tb_Agent"
        Dim adp As New SqlDataAdapter(_scID)
        Dim ds As New DataSet
        adp.Fill(ds, "tb_Agent")
        ins_id = "MA" & ds.Tables(0).Rows(0)("Agent_id")
        ID = ins_id
        Dim _sqlCmd_Ins As SqlCommand = CType(Conn.CreateCommand(), SqlCommand)
        Try
            _sqlCmd_Ins.CommandType = CommandType.Text
            _sqlCmd_Ins.CommandText = "INSERT INTO tb_Agent (Agent_id,Manager_ID, Supervisor_ID, agent_code, Agent_name, Designation, add1, add2, town, state,postcode, BankAc," & _
                                                            " Email, tel, fax, agent, supervisor, manager, percentage, Product_Name, spouse_name, spouse_desig, spouse_tel, " & _
                                                            " status, created_by, created_dt, nric, dob, age, race, gender, marital_status, application_date, appointment_date, contact_office, mobile) " & _
                                                            "VALUES ('" & ins_id & "', '" & Manager_ID & "', '" & Supervisor_ID & "', '" & agent_code & "', '" & Agent_name & "', '" & _
                                                                    desig & "', '" & add1 & "', '" & add2 & "', '" & town & "', '" & state & "', '" & _
                                                                    postcode & "', '" & bank_ac & "', '" & email & "', '" & tel & "', '" & _
                                                                    fax & "', '" & agent & "', '" & supervisor & "', '" & Manager & "', '" & _
                                                                   Percentage & "', '" & Product_Name & "', '" & s_name & "', '" & s_desig & "', '" & s_Tel & "', '" & status & "', '" & _
                                                                    created_by & "', '" & Format(Now(), "MM/dd/yyyy") & "', '" & _
                                                                    Me.txtNRIC.Text.Trim() & "', '" & Format(Me.dtpDOB.Value, "MM/dd/yyyy") & "', '" & Age & "', '" & Race & "', '" & Gender & "', '" & MStatus & "', '" & _
                                                                    Format(Me.dtpDateOfApplication.Value, "MM/dd/yyyy") & "', '" & Format(Me.dtpAppointmentDate.Value, "MM/dd/yyyy") & "', '" & Me.txtContactOffice.Text.Trim() & "', '" & Me.txtMobile.Text.Trim() & " ' )"


            sRes = _sqlCmd_Ins.ExecuteNonQuery()

            SyncLock SharedData
                SharedData.ReadyToHideMarquee = True
            End SyncLock
            Application.DoEvents()
            If sRes = "1" Then
                MsgBox("Successfully Added the Agent!")
                Me.btnPrint.Enabled = True
                Me.btnSubmit.Enabled = False
                getAgentdetails()
            Else
                MsgBox("Error while Adding the Agent! or Currently our server busy please try again..")
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Function
    Private Function clear_all() As Boolean
        Me.txtNRIC.Text = ""
        Me.txtMobile.Text = ""
        Me.txtContactOffice.Text = ""
        Me.txtAgent.Text = ""
        Me.txtAdd1.Text = ""
        Me.txtAdd2.Text = ""
        Me.txtPostCode.Text = ""
        Me.txtTown.Text = ""
        Me.txtFax.Text = ""
        Me.txtState.Text = ""
        Me.txtTel.Text = ""
        Me.txtAgentCode.Text = ""
        Me.txtBankAc.Text = ""
        Me.txtBankAc.Text = ""
        Me.txtDesig.Text = ""
        Me.txtSDesig.Text = ""
        Me.txtSpouseName.Text = ""
        Me.txtSTel.Text = ""
        Me.txtEmail.Text = ""
        Me.cbStatus.SelectedIndex = 0
        Me.txtAge.Text = ""
        Me.dtpDOB.Value = Now()
    End Function
#End Region
#Region "Update"
    Private Function Update_Agent_details() As String
        Dim agent_code, desig, Agent_name, add1, add2, town, agent, supervisor, Manager, postcode, email, bank_ac, agent_level, state, tel, fax, status, s_name, s_desig, s_Tel, created_by, sRes, ins_id As String
        Dim Manager_ID, Supervisor_ID, Percentage, Product_Name As String
        If Me.txtNRIC.Text.Trim() = "" Then
            MsgBox("NRIC Can't be Blank!")
            Me.txtNRIC.Focus()
            Exit Function
        End If
        If Me.txtAge.Text.Trim() = "" Then
            MsgBox("Please do key DOB!")
            Me.dtpDOB.Focus()
            Exit Function
        End If
        If Me.txtAgentCode.Text.Trim() = "" Then
            MsgBox("Agent Code Can't be Blank!")
            Me.txtAgentCode.Focus()
            Exit Function
        End If
        If Me.txtAgent.Text.Trim() = "" Then
            MsgBox("Agent Name Can't be Blank!")
            Me.txtAgent.Focus()
            Exit Function
        End If
        If Me.txtAdd1.Text.Trim() = "" Then
            MsgBox("Address1 Can't be Blank!")
            Me.txtAdd1.Focus()
            Exit Function
        End If
        If Me.txtPostCode.Text.Trim() = "" Then
            MsgBox("Post Code Can't be Blank!")
            Me.txtPostCode.Focus()
            Exit Function
        End If
        If Me.txtBankAc.Text.Trim() = "" Then
            MsgBox("Bank A/c Can't be Blank!")
            Me.txtTown.Focus()
            Exit Function
        End If
        If Me.txtTel.Text.Trim() = "" Then
            MsgBox("Telephone Number Can't be Blank!")
            Me.txtTel.Focus()
            Exit Function
        End If

        If Me.txtEmail.Text.Trim() = "" Then
            MsgBox("Email Can't be Blank!")
            Me.txtEmail.Focus()
            Exit Function
        End If
        If Me.rbAgentLevel_Agent.Checked = False Then
            If Me.rbAgentLevel_Manager.Checked = False Then
                If Me.rbAgentLevel_Supervisor.Checked = False Then
                    MsgBox("Level Can't be Blank!")
                    Me.txtTel.Focus()
                    Exit Function
                End If
            End If
        End If
        If Me.txtPercentage.Text.ToString.Trim() = "" Then
            MsgBox("Percentage Can't be Blank!")
            Me.txtPercentage.Focus()
            Exit Function
        End If
        Agent_name = Me.txtAgent.Text.Trim()
        desig = Me.txtDesig.Text.Trim()
        add1 = Me.txtAdd1.Text.Trim()
        add2 = Me.txtAdd2.Text.Trim()
        town = Me.txtTown.Text.Trim()
        state = Me.txtState.Text.Trim()
        postcode = Me.txtPostCode.Text.Trim()
        bank_ac = Me.txtBankAc.Text.Trim()
        email = Me.txtEmail.Text.Trim()
        tel = Me.txtTel.Text.Trim()
        fax = Me.txtFax.Text.Trim()
        agent_code = Me.txtAgentCode.Text.Trim()
        s_name = Me.txtSpouseName.Text.Trim()
        s_desig = Me.txtSDesig.Text.Trim()
        s_Tel = Me.txtSTel.Text.Trim()
        status = Me.cbStatus.SelectedIndex
        Select Case status
            Case "0"
                status = "Active"
            Case "1"
                status = "Inactive"
        End Select
        If Me.rbAgentLevel_Agent.Checked Then
            agent = 1
            Manager_ID = Me.cbManager.SelectedValue
            Supervisor_ID = Me.cbSupervisor.SelectedValue
        ElseIf Me.rbAgentLevel_Supervisor.Checked Then
            supervisor = 1
            Manager_ID = Me.cbManager.SelectedValue
            Supervisor_ID = 0
        ElseIf Me.rbAgentLevel_Manager.Checked Then
            Manager = 1
            Manager_ID = 0
            Supervisor_ID = 0
        Else
            agent = 0
            Manager_ID = 0
            Supervisor_ID = 0
        End If
        Percentage = Me.txtPercentage.Text.ToString.Trim
        Product_Name = Me.cbProduct.SelectedValue
        created_by = My.Settings.Username
        Dim Race, MStatus, Gender As String
        Dim Age As Integer
        If Me.rdiRace_Malay.Checked = True Then
            Race = "M"
        ElseIf Me.rdiRace_Chinese.Checked = True Then
            Race = "C"
        ElseIf Me.rdiRace_Indian.Checked = True Then
            Race = "I"
        ElseIf Me.rdiRace_Others.Checked = True Then
            Race = "O"
        End If

        If Me.rdiMaritalStatus_Single.Checked = True Then
            MStatus = "S"
        ElseIf Me.rdiMaritalStatus_Married.Checked = True Then
            MStatus = "M"
        ElseIf Me.rdiMaritalStatus_Widowed.Checked = True Then
            MStatus = "W"
        ElseIf Me.rdiMaritalStatus_Divorced.Checked = True Then
            MStatus = "D"
        End If

        If Me.rdiSex_Male.Checked = True Then
            Gender = "MALE"
        ElseIf Me.rdiSex_Female.Checked = True Then
            Gender = "FEMALE"
        End If
        Select Case status
            Case "0"
                status = "Active"
            Case "1"
                status = "Inactive"
        End Select
        Age = Me.txtAge.Text

        SharedData.ReadyToHideMarquee = False
        StartMarqueeThread()
        Dim _sqlCmd_Ins As SqlCommand = CType(Conn.CreateCommand(), SqlCommand)
        Try
            _sqlCmd_Ins.CommandType = CommandType.Text
            _sqlCmd_Ins.CommandText = "UPDATE tb_Agent SET  NRIC='" & Me.txtNRIC.Text.Trim() & "', DOB='" & Format(Me.dtpDOB.Value, "MM/dd/yyyy") & "', age='" & Age & "', race='" & Race & "', gender='" & Gender & "', Marital_status='" & MStatus & "', application_date='" & Format(Me.dtpDateOfApplication.Value, "MM/dd/yyyy") & "', appointment_date='" & Format(Me.dtpAppointmentDate.Value, "MM/dd/yyyy") & "', contact_office='" & Me.txtContactOffice.Text.Trim() & "', Mobile='" & Me.txtMobile.Text.Trim() & "', Manager_ID='" & Manager_ID & "', Supervisor_ID='" & Supervisor_ID & "', Agent_name='" & Agent_name & "', agent_code='" & agent_code & "', designation='" & desig & "', add1='" & add1 & "', add2='" & add2 & "', town='" & town & "', state='" & state & "', postcode='" & postcode & "', BankAc='" & bank_ac & "', email='" & email & "', tel='" & tel & "', fax='" & fax & "', agent='" & agent & "', supervisor='" & supervisor & "', manager='" & Manager & "', Percentage='" & Percentage & "', Product_Name='" & Product_Name & "', spouse_name='" & s_name & "', Spouse_Desig='" & s_desig & "', Spouse_tel='" & s_Tel & "', status='" & status & "', modified_by='" & My.Settings.Username & "', Modified_dt='" & Format(Now(), "MM/dd/yyyy") & "' where tb_Agent_id='" & lblAgentId.Text & "'"
            sRes = _sqlCmd_Ins.ExecuteNonQuery()
            SyncLock SharedData
                SharedData.ReadyToHideMarquee = True
            End SyncLock
            Application.DoEvents()
            If sRes = "1" Then
                MsgBox("Successfully Updated the Agent details!")
                Me.btnPrint.Enabled = True
                Me.btnSubmit.Enabled = False
                getAgentdetails()
            Else
                MsgBox("Error while Updating the Agent details! or Currently our server busy please try again..")
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try
    End Function
#End Region
#Region "Change Events"
    Private Sub rbAgentLevel_Agent_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbAgentLevel_Agent.CheckedChanged
        If Me.rbAgentLevel_Agent.Checked Then
            Me.cbManager.Enabled = True
            Me.cbSupervisor.Enabled = True
            getManager()
        End If
    End Sub
    Private Sub rbAgentLevel_Supervisor_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbAgentLevel_Supervisor.CheckedChanged
        If Me.rbAgentLevel_Supervisor.Checked Then
            Me.cbManager.Enabled = True
            Me.cbSupervisor.Enabled = False
            getManager()
        End If
    End Sub
    Private Sub rbAgentLevel_Manager_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbAgentLevel_Manager.CheckedChanged
        If Me.rbAgentLevel_Manager.Checked Then
            Me.cbManager.Enabled = False
            Me.cbSupervisor.Enabled = False
        End If
    End Sub
    Private Sub cbManager_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbManager.SelectedIndexChanged
        If Not Me.cbManager.SelectedValue.ToString = "System.Data.DataRowView" Then
            Dim dt As New DataTable
            dt = _objBusi.getDetails("AGENT", Me.cbManager.SelectedValue, "", "", "", "SUPERVISOR", Conn)
            If dt.Rows.Count > 0 Then
                Me.cbSupervisor.DataSource = dt
                Me.cbSupervisor.DisplayMember = "AGENT_NAME"
                Me.cbSupervisor.ValueMember = "TB_AGENT_ID"
            Else
                Me.cbSupervisor.DisplayMember = Nothing
                Me.cbSupervisor.ValueMember = Nothing
            End If
        End If
    End Sub
#End Region
#Region "General"
    Private Sub dtpDOB_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpDOB.Leave
        Me.txtAge.Text = Math.Floor(DateDiff(DateInterval.Day, Me.dtpDOB.Value, Now()) / 365.25)
    End Sub
#End Region
#Region "Progress Bar"
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