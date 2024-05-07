<%@ Page Title="" Language="C#" MasterPageFile="~/MainMenu.Master" AutoEventWireup="true" CodeBehind="View_Objectives.aspx.cs" Inherits="HRKpi.View_Objectives" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


     <div class="container-fluid">
        <asp:Label ID="lbl_fy_dtl" runat="server" Font-Bold="True" ForeColor="#0069B4"></asp:Label>
        <asp:Label ID="lbl_fin_year" runat="server" Visible="false"></asp:Label>
        <br />
        <div id="Div_Role">
            <table id="Tbl_EMP_Dtl" runat="server" style="border: thin dotted #808080; width: 100%; background-color: #F0F8FF">

                  <tr>
                    <td class="auto-style16"><strong>Company Name</strong>
                        <asp:DropDownList ID="cbocompany" runat="server" AppendDataBoundItems="True" Width="235px" Font-Size="Small" Height="22px">
                                 <asp:ListItem Value="0">--Select Company--</asp:ListItem>
                                 </asp:DropDownList>
                        <asp:Button ID="button_list" runat="server" OnClick="button_list_Click" Text="Fill Employee" />
                    </td>
                    <td class="auto-style17">
                        <strong>Fin Year</strong>
                         <asp:DropDownList ID="cboyear" runat="server" AppendDataBoundItems="True" Width="235px" Font-Size="Small" Height="22px" CssClass="mr-0">
                                 <asp:ListItem Value="0">--Select Year--</asp:ListItem>
                                 </asp:DropDownList>
                    </td>
                </tr>

                  <tr>
                    <td class="auto-style16"><strong>Employee Name</strong><asp:DropDownList ID="cboemp" runat="server" AppendDataBoundItems="True" Width="362px" Font-Size="Small" Height="22px">
                                 <asp:ListItem Value="0">--Select Employee--</asp:ListItem>
                                 </asp:DropDownList>
                        <asp:Button ID="button_list0" runat="server" OnClick="button_list0_Click" Text="Get Detail" />
                    </td>
                    <td class="auto-style17">
                        
                    </td>
                </tr>

                <tr>
                    <td class="auto-style16"><strong>Name:</strong>
                        <asp:Label ID="lbl_name" runat="server" ForeColor="#333333"></asp:Label>
                    </td>
                    <td class="auto-style17">
                        <strong>Employee Code:</strong>
                        <asp:Label ID="lbl_empcode" runat="server"></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td class="auto-style16"><strong>Designation:</strong>
                        <asp:Label ID="lbl_desig" runat="server"></asp:Label>
                    </td>
                    <td class="auto-style17">
                        <strong>Department:</strong>
                        <asp:Label ID="lbl_dept" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style16">
                        <strong>
                            <asp:Label ID="lbl_grade_dis" runat="server" Text="Grade:"></asp:Label>
                        </strong>&nbsp;<asp:Label ID="lbl_grade" runat="server"></asp:Label>
                    </td>
                    <td class="auto-style17"><strong>Date of Joining:</strong>
                        <asp:Label ID="lbl_doj" runat="server"></asp:Label>
                    </td>
                </tr>
                <%-- <tr>
                
                <td class="auto-style3" colspan="2"><strong>Company:</strong>
                    <asp:Label ID="lbl_company" runat="server" Width="80%" ></asp:Label>
                    </td>
            </tr>--%>
            </table>
            <br />
            <asp:Panel ID="Panel3" runat="server">
                <%-- class="auto-style35"--%>
                <table style="width: 100%">
                    <tr>
                        <td>
                            <asp:Label ID="Label8" runat="server" Font-Bold="True" ForeColor="#0069B4"></asp:Label>
                            <asp:Label ID="Label9" runat="server" Visible="false"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <asp:Label ID="Label10" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="Label11" runat="server" Visible="False"></asp:Label>
        </div>
        <br />
        <div class="form-group row">
            <div class="col-xl">
                <asp:GridView ID="GV_KPI" runat="server" AutoGenerateColumns="False" CellPadding="4" Width="99%" ForeColor="#333333" GridLines="None" EmptyDataText="Record not found." ShowHeaderWhenEmpty="True">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="S_No" HeaderText="S. No." SortExpression="S_No">
                            <HeaderStyle Wrap="False" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Objectives" SortExpression="Objectives">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Objectives") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <%--<asp:Label ID="Label1" runat="server" Text='<%# Bind("Objectives") %>'></asp:Label>--%>
                                <%# ((string)Eval("Objectives")).Replace("\n", "<br/>") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="UOM" HeaderText="Unit of Measurement" SortExpression="UOM">
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Target" HeaderText="Target" SortExpression="Target">
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Target_Date" HeaderText="Target Date" SortExpression="Target_Date" DataFormatString="{0:d}">
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" Wrap="False" />
                        </asp:BoundField>
                        <asp:BoundField DataField="Weightage" HeaderText="Weightage" ReadOnly="True" SortExpression="Weightage">
                            <HeaderStyle HorizontalAlign="Right" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:BoundField>

                    </Columns>
                    <EditRowStyle BackColor="#2461BF" />
                    <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                    <HeaderStyle BackColor="#0069B4" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#EFF3FB" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                </asp:GridView>
            </div>
        </div>

        <div class="form-group row">
            <div class="col-sm-2">
                <asp:Label ID="Label1" runat="server" Text="Remarks"></asp:Label>
            </div>
            <div class="col-sm-10">
                <asp:TextBox ID="txt_remarks" runat="server" TextMode="MultiLine" Width="99%" Height="45px" ReadOnly="True"></asp:TextBox>
            </div>
        </div>
        <div class="form-group row">
            <div class="col-sm-2">
                <asp:Label ID="Label3" runat="server" Text="Reviewer Remarks"></asp:Label>
            </div>
            <div class="col-sm-10">
                <asp:TextBox ID="txt_reviewer_remarks" runat="server" Height="45px" ReadOnly="True" TextMode="MultiLine" Width="99%"></asp:TextBox>
            </div>
        </div>
        <div class="form-group row">
            <div class="col-sm-2">
                <asp:Label ID="Label4" runat="server" Text="Approver Remarks"></asp:Label>
            </div>
            <div class="col-sm-10" style="left: 0px; top: 0px">
                <asp:TextBox ID="txt_approver_remarks" runat="server" Height="45px" TextMode="MultiLine" Width="99%" ReadOnly="True"></asp:TextBox>
            </div>
        </div>
        
        <div class="form-group row">
            <div class="col-sm-2" style="left: 0px; top: 0px">
                <asp:Label ID="Label2" runat="server" Text="Status"></asp:Label>
            &nbsp;&nbsp;&nbsp;
                <asp:Label ID="lblstatus" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="Medium"></asp:Label>
            </div>
            <div class="col-sm-10">
            </div>
        </div>
        <br />
        <div class="form-group row justify-content-left">
            <div class="col-sm-1" style="left: 5px; top: -52px"></div>
            <div>
            </div>
        </div>



    </div>
    

</asp:Content>
