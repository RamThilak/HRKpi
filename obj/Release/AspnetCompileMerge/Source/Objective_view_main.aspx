<%@ Page Title="" Language="C#" MasterPageFile="~/PMS_Approve_Mst.Master" AutoEventWireup="true" CodeBehind="Objective_view_main.aspx.cs" Inherits="HRKpi.Objective_view_main" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }

        .auto-style2 {
            width: 97px;
        }
             .hideGridColumn
    {
        display:none;
    }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      &nbsp;<br />
    <table class="auto-style1">
        <tr>
            <td class="auto-style2">
                <asp:Label ID="Label1" runat="server" Text="Company"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="cbocompany" runat="server" AppendDataBoundItems="True" Height="16px" Width="260px">
                    <asp:ListItem Value="0">Select</asp:ListItem>                  
                </asp:DropDownList>
              
            </td>
        </tr>
        <tr>
            <td class="auto-style2">
                <asp:Label ID="Label3" runat="server" Text="Fin Year"></asp:Label>
            </td>
            <td class="auto-style5">
                <asp:DropDownList ID="ddl_year" runat="server" Width="139px">
                    <asp:ListItem Value="0">Select Year</asp:ListItem>
                    <asp:ListItem Value="2021">2021</asp:ListItem>       
                     <asp:ListItem Value="2021">2022</asp:ListItem>       
                     <asp:ListItem Value="2021">2023</asp:ListItem>       
                </asp:DropDownList>           
              
              
            </td>

        </tr>
        
        <tr>
            <td class="auto-style2">
                <asp:Label ID="Label4" runat="server" Text="KPI Status"></asp:Label>
            </td>
            <td class="auto-style5">
                <asp:DropDownList ID="ddl_status" runat="server" Width="259px" Height="18px">
                    <asp:ListItem Value="0">--Select All --</asp:ListItem>
                    <asp:ListItem Value="Pending for Submission">Pending for Submission</asp:ListItem>                   
                    <asp:ListItem>Pending for Approval</asp:ListItem>
                    <asp:ListItem>Approved</asp:ListItem>
                    <asp:ListItem>ReSubmitted for correction</asp:ListItem>
                    <asp:ListItem>KPI Reviewed</asp:ListItem>
                    <asp:ListItem>Not yet prepared</asp:ListItem>
                </asp:DropDownList>           
              
              
            </td>

        </tr>
        
        <tr>
            <td class="auto-style2">
                <asp:Label ID="Label5" runat="server" Text="Rating Desc"></asp:Label>
            </td>
            <td class="auto-style5">
                <asp:DropDownList ID="ddl_rating" runat="server" Width="259px" Height="18px">
                    <asp:ListItem Value="0">--Select All --</asp:ListItem>
                    <asp:ListItem Value="To Be Reviewed">To Be Reviewed</asp:ListItem>                   
                    <asp:ListItem>Does Not Meet Expectation</asp:ListItem>
                    <asp:ListItem>Meets Some Expectation</asp:ListItem>
                    <asp:ListItem>Meets Expectation</asp:ListItem>
                    <asp:ListItem>Exceeds Some Expectation</asp:ListItem>
                    <asp:ListItem>Exceeds Expectation</asp:ListItem>
                </asp:DropDownList>           
              
                &nbsp;           
              
                <asp:Button ID="button_getlist" runat="server" Text="Get List" Font-Bold="True" Height="25px" OnClick="button_getlist_click" Width="85px" />
              
              
            &nbsp;           
              
                <asp:Button ID="button_export" runat="server" Text="Export List" Font-Bold="True" Height="25px" OnClick="button_export_click" Width="85px" />
              
              
            </td>

        </tr>
        
        <tr>
            <td class="auto-style2">
                &nbsp;</td>
            <td>
           
              
                &nbsp;</td>

        </tr>
        
        <tr>
            <td colspan="2">
                <asp:Label ID="lblerr" runat="server" ForeColor="Red" Text="Label" Visible="False"></asp:Label>
                <br />
                <asp:GridView ID="GridView1" runat="server" OnRowDataBound="GridView1_RowDataBound" AutoGenerateColumns="False" CellPadding="4"  ForeColor="#333333" GridLines="None" Width="100%">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="branch_name" HeaderText="Branch" SortExpression="branch_name" />
                        <asp:BoundField DataField="company_id" HeaderText="CID" SortExpression="CID" HeaderStyle-CssClass = "hideGridColumn" ItemStyle-CssClass="hideGridColumn">
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="emp_code" HeaderText="Emp. code" SortExpression="emp_code" HeaderStyle-CssClass = "hideGridColumn" ItemStyle-CssClass="hideGridColumn" />
                        <asp:BoundField DataField="emp_name" HeaderText="Name" SortExpression="emp_name" HeaderStyle-CssClass = "hideGridColumn" ItemStyle-CssClass="hideGridColumn" />
                        <asp:TemplateField HeaderText="Emp. Code" SortExpression="emp_code">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("emp_code") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="lnk_id" runat="server" Enabled="false" NavigateUrl='<%# Eval("emp_code", "~/PMS_Approve.aspx?emp_code=" + Eval("emp_code") +"&company_id=" + Eval("company_id") +"") %>'
                                    Text='<%# Eval("emp_code") %>' />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name" SortExpression="emp_name">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("emp_Name") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:HyperLink ID="lnk_name" runat="server" Enabled="false" NavigateUrl='<%# Eval("emp_code", "~/PMS_approve.aspx?emp_code=" + Eval("emp_code") +"&company_id=" + Eval("company_id") +"") %>'
                                    Text='<%# Bind("emp_Name") %>' />
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Dept_Name" HeaderText="Department" SortExpression="Dept_Name" />
                        <asp:BoundField DataField="Desig_Name" HeaderText="Designation" SortExpression="Desig_Name" />
                        <%--<asp:BoundField DataField="KPI_Status" HeaderText="KPI Status" ReadOnly="True" SortExpression="KPI_Status" />--%>
                        <asp:TemplateField HeaderText="KPI Status" SortExpression="kpistatus">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("KPI_Status") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblstatus" runat="server" Text='<%# Bind("KPI_Status") %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                       <asp:BoundField DataField="scoredesc" HeaderText="Rating Desc" SortExpression="scoredesc" />
                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#F5F7FB" />
                    <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                    <SortedDescendingCellStyle BackColor="#E9EBEF" />
                    <SortedDescendingHeaderStyle BackColor="#4870BE" />
                </asp:GridView>
              
            </td>
        </tr>
    </table>
    <br />
</asp:Content>
