<%@ Page Title="" Language="C#" MasterPageFile="~/PMS_Approve_Mst.Master" AutoEventWireup="true" CodeBehind="PMS_approve.aspx.cs" Inherits="HRKpi.PMS_approve" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

     <style type="text/css">
         
             #insidetable tr:nth-child(even) {background-color: #f2f2f2;}
             #insidetable2 tr:nth-child(even) {background-color: #f2f2f2;}
             #insidetable3 tr:nth-child(even) {background-color: #f2f2f2;}
         
       
        .auto-style1 {
            width: 100%;
            border:thin;
        }
        .auto-style2 {
            margin-left: 0px;
        }
        .auto-style3 {
            width: 105px;
        }
        .auto-style4 {
            width: 200px;
        }
        .auto-style8 {
            width: 3%;
            border-bottom: 1px solid #ddd;
            border-left: 1px solid #ddd;
            border-right: 1px solid #ddd;
            border-top: 1px solid #ddd;
            border-collapse: collapse;
        }
        .auto-style9 {
            width: 20%;
            border-bottom: 1px solid #ddd;
            border-left: 1px solid #ddd;
            border-right: 1px solid #ddd;
            border-top: 1px solid #ddd;
            border-collapse: collapse;
        }
        .auto-style10 {
            width: 7%;
            border-bottom: 1px solid #ddd;
            border-left: 1px solid #ddd;
            border-right: 1px solid #ddd;
            border-top: 1px solid #ddd;
            border-collapse: collapse;
            text-align:center;
        }
        .auto-style11 {
            width: 7%;
            border-bottom: 1px solid #ddd;
            border-left: 1px solid #ddd;
            border-right: 1px solid #ddd;
            border-top: 1px solid #ddd;
            border-collapse: collapse;
             text-align:center;
        }
        .auto-style12 {
           width: 5%;
            border-bottom: 1px solid #ddd;
            border-left: 1px solid #ddd;
            border-right: 1px solid #ddd;
            border-top: 1px solid #ddd;
            border-collapse: collapse;
             text-align:center;
        }
                
         /*legend { color: #E62644; font-size:10px; font-weight:bold; }*/
        span {
            font-family: Helvetica;
            font-variant:normal;
            text-transform:capitalize;  
            }       
        strong{
             font-family: Helvetica;
        }

        .auto-style21 {
            width: 99%;
            height: 26px;
        }
        
        .auto-style23 {
            width: 105px;
            height: 26px;
        }
        .auto-style24 {
            height: 26px;
        }

        .auto-style27 {
            width: 5%;
            border-bottom: 1px solid #ddd;
            border-left: 1px solid #ddd;
            border-right: 1px solid #ddd;
            border-top: 1px solid #ddd;
            border-collapse: collapse;
            text-align: center;
        }

        .auto-style28 {
            width: 20%;
            border-bottom: 1px solid #ddd;
            border-left: 1px solid #ddd;
            border-right: 1px solid #ddd;
            border-top: 1px solid #ddd;
            border-collapse: collapse;
            text-align: center;
        }
        
        .auto-style29 {
            width: 100%;
        }
        .auto-style30 {
            width: 10%;
        }

        .auto-style31 {
            width: 20%;
            /* border-bottom: 1px solid black;
    border-top: 1px solid black;
    border-collapse: collapse; */
             border-bottom: 1px solid #ddd;
            border-left: 1px solid #ddd;
            border-right: 1px solid #ddd;
            border-top: 1px solid #ddd;
            border-collapse: collapse;
        }

        .auto-style34 {
               border-bottom: 1px solid #ddd;
            border-left: 1px solid #ddd;
            border-right: 1px solid #ddd;
            border-top: 1px solid #ddd;
            border-collapse: collapse;
            width: 7%;
           
            text-align: center;
        }
        .auto-style35 {
            border-bottom: 1px solid #ddd;
            border-left: 1px solid #ddd;
            border-right: 1px solid #ddd;
            border-top: 1px solid #ddd;
            border-collapse: collapse;
            width: 15%;
            
            text-align: center;
        }
        .auto-style36 {
              border-bottom: 1px solid #ddd;
            border-left: 1px solid #ddd;
            border-right: 1px solid #ddd;
            border-top: 1px solid #ddd;
            border-collapse: collapse;
            width: 3%;
            
            text-align: center;
        }
        .auto-style37 {
            border-bottom: 1px solid #ddd;
            border-left: 1px solid #ddd;
            border-right: 1px solid #ddd;
            border-top: 1px solid #ddd;
            border-collapse: collapse;
            
            width: 20%;
           
            text-align: center;
        }
        .auto-style38 {
            border-bottom: 1px solid #ddd;
            border-left: 1px solid #ddd;
            border-right: 1px solid #ddd;
            border-top: 1px solid #ddd;
            border-collapse: collapse;
        }
        .auto-style39 {
           border-bottom: 1px solid #ddd;
            border-left: 1px solid #ddd;
            border-right: 1px solid #ddd;
            border-top: 1px solid #ddd;
            border-collapse: collapse;
            width: 10%;
            text-align: center;
        }

        .auto-style51 {
            width: 8%;
        }

        .auto-style52 {
            width: 10%;
        }


       /* .aspNetDisabled {
            color: #FFF;
            background-color: #000;
        }*/

        :disabled, [disabled] {
            /* -ms-opacity: 0.5;
    opacity:0.5;*/
            color: black;
            background-color: white;
            /*        background-color: #000;*/
        }

        .auto-style16 {
            width: 49%;
        }
        .auto-style17 {
            width: 49%;
        }
</style>
          
   

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div id="Div_Role">

        <table id="Tbl_EMP_Dtl" runat="server" style="border: thin dotted #808080; width: 100%; background-color: #F0F8FF">

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
                    <td >
                        <asp:Label ID="lbl_fy_dtl" runat="server" Font-Bold="True" ForeColor="#0069B4"></asp:Label>
                        <asp:Label ID="lbl_fin_year" runat="server" Visible="false"></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:Panel>
         
        <asp:Label ID="Label1" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="Label2" runat="server" Visible="False"></asp:Label>
    </div>

     <%--<asp:Panel ID="pnl_JD" runat="server" GroupingText="Job Description">
        <asp:GridView ID="GV_JD" runat="server" AutoGenerateColumns="False" CellPadding="4" 
        DataSourceID="SqlDataSource_JD" Width="100%" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="S_No" HeaderText="S. No." SortExpression="S_No">
                    <HeaderStyle HorizontalAlign="Left" Width="10%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Role_Desc" HeaderText="Job Description" SortExpression="Role_Desc" >
                    <HeaderStyle HorizontalAlign="Left" />
                    </asp:BoundField>
                </Columns>
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            </asp:GridView>
    </asp:Panel>
     <asp:SqlDataSource ID="SqlDataSource_JD" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"   
             SelectCommand="Select S_No, Role_Desc From KPI_Role_Description_Mst Where Fin_Year = 2021 AND Company_Id = @cid and Emp_code = @eid and len(role_desc) > 0 ">
          <SelectParameters>
            <asp:ControlParameter ControlID="lbl_cid_pass" Name="cid" PropertyName="Text" />
            <asp:ControlParameter ControlID="lbl_emp_pass" Name="eid" PropertyName="Text" />
        </SelectParameters>
    </asp:SqlDataSource>--%>

    
    <asp:Panel ID="Pnl_KPI" runat="server" GroupingText="KPI" Visible="true">
        <table id="insidetable2" class="auto-style1" >            
            <tr>
                <td class="auto-style8" style="font-weight: bold; background-color: #0069B4; font-size: small; color: #FFFFFF;">S.No</td>
                <td class="auto-style9" style="font-weight: bold; background-color: #0069B4; font-size: small; color: #FFFFFF;">KPI / Annual Objective</td>
                <td class="auto-style10" style="font-weight: bold; background-color: #0069B4; font-size: small; color: #FFFFFF;">Unit of Measure</td>
                <td class="auto-style11" style="font-weight: bold; background-color: #0069B4; font-size: small; color: #FFFFFF;">Target</td>
                <td class="auto-style51" style="font-weight: bold; background-color: #0069B4; font-size: small; color: #FFFFFF;">Target Date</td>
                <td class="auto-style12" style="font-weight: bold; background-color: #0069B4; font-size: small; color: #FFFFFF;">Weightage</td>
                <td class="auto-style9" style="font-weight: bold; background-color: #0069B4; color: #FFFFFF;">Details of Target Achieved</td>
                <td class="auto-style27" style="font-weight: bold; background-color: #0069B4; color: #FFFFFF;">Date of Achievement</td>
                <td class="auto-style52" style="font-weight: bold; background-color: #0069B4; color: #FFFFFF;">Approver Score Against 100%</td>
                <td class="auto-style28" style="font-weight: bold; background-color: #0069B4; color: #FFFFFF;">Remarks</td>
            </tr>
            <tr>
                <td class="auto-style8">1<asp:Label ID="kpi_id_1" runat="server" Visible="False"></asp:Label>
                </td>
                <td class="auto-style9">
                    <asp:Label ID="lbl_kpi_desc_1" runat="server" Text=""></asp:Label>
                    <br />
                </td>
                <td class="auto-style10">
                    <asp:Label ID="lbl_unit_1" runat="server" Text=""></asp:Label>
                </td>
                <td class="auto-style11">
                    <asp:Label ID="lbl_target_1" runat="server" Text=""></asp:Label>
                </td>
                <td class="auto-style11">
                    <asp:Label ID="lbl_target_dt_1" runat="server"></asp:Label>
                </td>
                <td class="auto-style12">
                    <asp:Label ID="lbl_weightage_1" runat="server" Text=""></asp:Label>
                    %</td>
                <td class="auto-style9">
                    <asp:Label ID="lbl_kpi_target_dtl_1" runat="server"></asp:Label>
                </td>
                <td class="auto-style27">
                    <asp:Label ID="lbl_kpi_achieved_dt_1" runat="server"></asp:Label>
                </td>
                <td class="auto-style52">
                    <asp:TextBox ID="txt_rating_1" runat="server" Width="75%"></asp:TextBox>
                    %<asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txt_rating_1" Display="Dynamic" ErrorMessage="RangeValidator" MaximumValue="100" MinimumValue="0" SetFocusOnError="True" Type="Integer" ValidationGroup="vg_save">Maximum Rating upto 100 only</asp:RangeValidator>
                </td>
                <td class="auto-style28">
                    <asp:TextBox ID="txt_kpi_remarks_1" runat="server" Width="99%" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style8">2<asp:Label ID="kpi_id_2" runat="server" Visible="False"></asp:Label>
                </td>
                <td class="auto-style9">
                    <asp:Label ID="lbl_kpi_desc_2" runat="server" Text=""></asp:Label>
                </td>
                <td class="auto-style10">
                    <asp:Label ID="lbl_unit_2" runat="server" Text=""></asp:Label>
                </td>
                <td class="auto-style11">
                    <asp:Label ID="lbl_target_2" runat="server" Text=""></asp:Label>
                </td>
                <td class="auto-style11">
                    <asp:Label ID="lbl_target_dt_2" runat="server"></asp:Label>
                </td>
                <td class="auto-style12">
                    <asp:Label ID="lbl_weightage_2" runat="server" Text=""></asp:Label>
                    %</td>
                <td class="auto-style9">
                    <asp:Label ID="lbl_kpi_target_dtl_2" runat="server"></asp:Label>
                </td>
                <td class="auto-style27">
                    <asp:Label ID="lbl_kpi_achieved_dt_2" runat="server"></asp:Label>
                </td>
                <td class="auto-style52">
                    <asp:TextBox ID="txt_rating_2" runat="server" Width="75%"></asp:TextBox>
                    %<asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txt_rating_2" Display="Dynamic" ErrorMessage="RangeValidator" MaximumValue="100" MinimumValue="0" SetFocusOnError="True" Type="Integer" ValidationGroup="vg_save">Maximum Rating upto 100 only</asp:RangeValidator>
                </td>
                <td class="auto-style28">
                    <asp:TextBox ID="txt_kpi_remarks_2" runat="server" Width="99%" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style8">3<asp:Label ID="kpi_id_3" runat="server" Visible="False"></asp:Label>
                </td>
                <td class="auto-style9">
                    <asp:Label ID="lbl_kpi_desc_3" runat="server" Text=""></asp:Label>
                </td>
                <td class="auto-style10">
                    <asp:Label ID="lbl_unit_3" runat="server" Text=""></asp:Label>
                </td>
                <td class="auto-style11">
                    <asp:Label ID="lbl_target_3" runat="server" Text=""></asp:Label>
                </td>
                <td class="auto-style11">
                    <asp:Label ID="lbl_target_dt_3" runat="server"></asp:Label>
                </td>
                <td class="auto-style12">
                    <asp:Label ID="lbl_weightage_3" runat="server" Text=""></asp:Label>
                    %</td>
                <td class="auto-style9">
                    <asp:Label ID="lbl_kpi_target_dtl_3" runat="server"></asp:Label>
                </td>
                <td class="auto-style27">
                    <asp:Label ID="lbl_kpi_achieved_dt_3" runat="server"></asp:Label>
                </td>
                <td class="auto-style52">
                    <asp:TextBox ID="txt_rating_3" runat="server" Width="75%"></asp:TextBox>
                    %<asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="txt_rating_3" Display="Dynamic" ErrorMessage="RangeValidator" MaximumValue="100" MinimumValue="0" SetFocusOnError="True" Type="Integer" ValidationGroup="vg_save">Maximum Rating upto 100 only</asp:RangeValidator>
                </td>
                <td class="auto-style28">
                    <asp:TextBox ID="txt_kpi_remarks_3" runat="server" Width="99%" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style8">4<asp:Label ID="kpi_id_4" runat="server" Visible="False"></asp:Label>
                </td>
                <td class="auto-style9">
                    <asp:Label ID="lbl_kpi_desc_4" runat="server" Text=""></asp:Label>
                </td>
                <td class="auto-style10">
                    <asp:Label ID="lbl_unit_4" runat="server" Text=""></asp:Label>
                </td>
                <td class="auto-style11">
                    <asp:Label ID="lbl_target_4" runat="server" Text=""></asp:Label>
                </td>
                <td class="auto-style11">
                    <asp:Label ID="lbl_target_dt_4" runat="server"></asp:Label>
                </td>
                <td class="auto-style12">
                    <asp:Label ID="lbl_weightage_4" runat="server" Text=""></asp:Label>
                    %</td>
                <td class="auto-style9">
                    <asp:Label ID="lbl_kpi_target_dtl_4" runat="server"></asp:Label>
                </td>
                <td class="auto-style27">
                    <asp:Label ID="lbl_kpi_achieved_dt_4" runat="server"></asp:Label>
                </td>
                <td class="auto-style52">
                    <asp:TextBox ID="txt_rating_4" runat="server" Width="75%"></asp:TextBox>
                    %<asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="txt_rating_4" Display="Dynamic" ErrorMessage="RangeValidator" MaximumValue="100" MinimumValue="0" SetFocusOnError="True" Type="Integer" ValidationGroup="vg_save">Maximum Rating upto 100 only</asp:RangeValidator>
                </td>
                <td class="auto-style28">
                    <asp:TextBox ID="txt_kpi_remarks_4" runat="server" Width="99%" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style8">5<asp:Label ID="kpi_id_5" runat="server" Visible="False"></asp:Label>
                </td>
                <td class="auto-style9">
                    <asp:Label ID="lbl_kpi_desc_5" runat="server" Text=""></asp:Label>
                </td>
                <td class="auto-style10">
                    <asp:Label ID="lbl_unit_5" runat="server" Text=""></asp:Label>
                </td>
                <td class="auto-style11">
                    <asp:Label ID="lbl_target_5" runat="server" Text=""></asp:Label>
                </td>
                <td class="auto-style11">
                    <asp:Label ID="lbl_target_dt_5" runat="server"></asp:Label>
                </td>
                <td class="auto-style12">
                    <asp:Label ID="lbl_weightage_5" runat="server" Text=""></asp:Label>
                    % </td>
                <td class="auto-style9">
                    <asp:Label ID="lbl_kpi_target_dtl_5" runat="server"></asp:Label>
                </td>
                <td class="auto-style27">
                    <asp:Label ID="lbl_kpi_achieved_dt_5" runat="server"></asp:Label>
                </td>
                <td class="auto-style52">
                    <asp:TextBox ID="txt_rating_5" runat="server" Width="75%"></asp:TextBox>
                    %<asp:RangeValidator ID="RangeValidator5" runat="server" ControlToValidate="txt_rating_5" Display="Dynamic" ErrorMessage="RangeValidator" MaximumValue="100" MinimumValue="0" SetFocusOnError="True" Type="Integer" ValidationGroup="vg_save">Maximum Rating upto 100 only</asp:RangeValidator>
                </td>
                <td class="auto-style28">
                    <asp:TextBox ID="txt_kpi_remarks_5" runat="server" Width="99%" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Panel ID="Pnl_Individual_Factors" runat="server" GroupingText="Individual Factors">
        <table id="insidetable3" class="auto-style29" style="border-style: double; border-width: thin; background-color: #F0F8FF;">
            <tr style="font-weight: bold; background-color: #0069B4; font-size: small; color: #FFFFFF;">
                <td class="auto-style30">Productivity</td>
                <td class="auto-style30">Willingness to take Responsibility</td>
                <td class="auto-style30">Quality of Work</td>
                <td class="auto-style30">On Time Delivery / Schedule Adherence</td>
                <td class="auto-style30">Innovation / Creativity</td>
                <td class="auto-style30">Learning Initiative</td>
                <td class="auto-style30">Communication </td>
                <td class="auto-style30">Team Player</td>
                <td class="auto-style30">Leadership Skills</td>
                <td class="auto-style30">Customer Interaction(Internal / External)</td>
            </tr>
            <tr>
                <td class="auto-style30">
                    <asp:DropDownList ID="ddl1" runat="server" Width="100%">
                        <asp:ListItem Value="0">Select</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>1</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="auto-style30">
                    <asp:DropDownList ID="ddl2" runat="server" Width="100%">
                        <asp:ListItem Value="0">Select</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>1</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="auto-style30">
                    <asp:DropDownList ID="ddl3" runat="server" Width="100%">
                        <asp:ListItem Value="0">Select</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>1</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="auto-style30">
                    <asp:DropDownList ID="ddl4" runat="server" Width="100%">
                        <asp:ListItem Value="0">Select</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>1</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="auto-style30">
                    <asp:DropDownList ID="ddl5" runat="server" Width="100%">
                        <asp:ListItem Value="0">Select</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>1</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="auto-style30">
                    <asp:DropDownList ID="ddl6" runat="server" Width="100%">
                        <asp:ListItem Value="0">Select</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>1</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="auto-style30">
                    <asp:DropDownList ID="ddl7" runat="server" Width="100%">
                        <asp:ListItem Value="0">Select</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>1</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="auto-style30">
                    <asp:DropDownList ID="ddl8" runat="server" Width="100%">
                        <asp:ListItem Value="0">Select</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>1</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="auto-style30">
                    <asp:DropDownList ID="ddl9" runat="server" Width="100%">
                        <asp:ListItem Value="0">Select</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>1</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="auto-style30">
                    <asp:DropDownList ID="ddl10" runat="server" Width="100%">
                        <asp:ListItem Value="0">Select</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>1</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <asp:Panel ID="Panel4" runat="server" BorderStyle="None">
            <strong>Rating Scale: </strong>5 being the Highest Score and 1 being the Lowest Score
        </asp:Panel>
    </asp:Panel>
    <%--<asp:Panel ID="Pnl_Achievements" runat="server" GroupingText="Special Achievements">
        <table class="auto-style29" style="border-style: double; border-width: thin; background-color: #F0F8FF;">
            <tr style="font-weight: bold; background-color: #0069B4; font-size: small; color: #FFFFFF;">
                <td class="auto-style32">Details of Special Achievements </td>
                <td class="auto-style33">Benefit for the Organization</td>
                <td class="auto-style31">Involved internal stakeholders</td>
                <td>Rating</td>
            </tr>
            <tr>
                <td class="auto-style32">
                    <asp:Label ID="lbl_spl_ach_dtl" runat="server" Width="100%"></asp:Label>
                </td>
                <td class="auto-style33">
                    <asp:Label ID="lbl_benefit" runat="server" Width="100%"></asp:Label>
                </td>
                <td class="auto-style31">
                    <asp:Label ID="lbl_involved" runat="server" Width="100%"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddl_spl_rating" runat="server">
                        <asp:ListItem Value="-1">Select</asp:ListItem>
                        <asp:ListItem Value="1">Agree</asp:ListItem>
                        <asp:ListItem Value="2">Partially Agree</asp:ListItem>
                        <asp:ListItem Value="3">Disagree</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </asp:Panel>--%>

     <asp:Panel ID="Pnl_Achievements" runat="server" GroupingText="Special Achievements">
        <table id="insidetable" class="auto-style1">
            <tr>
                <td class="auto-style36" style="background-color: #0069B4; color: #FFFFFF;">S.No</td>
                <td class="auto-style37" style="background-color: #0069B4; color: #FFFFFF;">Details of Special Achievement</td>
                <td class="auto-style31" style="background-color: #0069B4; color: #FFFFFF;">Benefit to the Organization</td>
                <td class="auto-style34" style="background-color: #0069B4; color: #FFFFFF;">Your Role</td>
                <td class="auto-style38" style="background-color: #0069B4; color: #FFFFFF;">Remarks(if any)</td>
                <td class="auto-style35" style="background-color: #0069B4; color: #FFFFFF;">Details of the other Stakeholders for Verification</td>
                <td class="auto-style39" style="background-color: #0069B4; color: #FFFFFF;">Rating</td>
            </tr>
            <tr id="spl_row_1" runat="server" visible="false">
                <td class="auto-style36">
                    <asp:Label ID="lbl_spl_1" runat="server" Text="1"></asp:Label>
                </td>
                <td class="auto-style37">
                    <asp:TextBox ID="txt_ach_dtl_1" runat="server" TextMode="MultiLine" Width="99%" ReadOnly="True"></asp:TextBox>
                </td>
                <td class="auto-style31">
                    <asp:TextBox ID="txt_ach_benefit_1" runat="server" TextMode="MultiLine" Width="99%" ReadOnly="True"></asp:TextBox>
                </td>
                <td class="auto-style34">
                    <asp:DropDownList ID="ddl_ach_role_1" runat="server" Enabled="False">
                        <asp:ListItem Value="-1">Select</asp:ListItem>
                        <asp:ListItem Value="1">Individual Contributor</asp:ListItem>
                        <asp:ListItem Value="2">Part of Team</asp:ListItem>
                        <asp:ListItem Value="3">Idea Contributor</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="auto-style38">
                    <asp:TextBox ID="txt_ach_remark_1" runat="server" TextMode="MultiLine" Width="99%" ReadOnly="True"></asp:TextBox>
                </td>
                <td class="auto-style35">
                    <asp:TextBox ID="txt_ach_stakeholder_1" runat="server" TextMode="MultiLine" Width="99%" ReadOnly="True"></asp:TextBox>
                </td>
                <td class="auto-style39">
                    <asp:DropDownList ID="ddl_spl_rating_1" runat="server">
                        <asp:ListItem Value="-1">Select</asp:ListItem>
                        <asp:ListItem Value="1">Agree</asp:ListItem>
                
                        <asp:ListItem Value="3">Disagree</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="spl_row_2" runat="server" visible="false">
                <td class="auto-style36">
                    <asp:Label ID="lbl_spl_2" runat="server" Text="2"></asp:Label>
                </td>
                <td class="auto-style37">
                    <asp:TextBox ID="txt_ach_dtl_2" runat="server" TextMode="MultiLine" Width="99%" ReadOnly="True"></asp:TextBox>
                </td>
                <td class="auto-style31">
                    <asp:TextBox ID="txt_ach_benefit_2" runat="server" TextMode="MultiLine" Width="99%" ReadOnly="True"></asp:TextBox>
                </td>
                <td class="auto-style34">
                    <asp:DropDownList ID="ddl_ach_role_2" runat="server" Enabled="False">
                        <asp:ListItem Value="-1">Select</asp:ListItem>
                        <asp:ListItem Value="1">Individual Contributor</asp:ListItem>
                        <asp:ListItem Value="2">Part of Team</asp:ListItem>
                        <asp:ListItem Value="3">Idea Contributor</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="auto-style38">
                    <asp:TextBox ID="txt_ach_remark_2" runat="server" TextMode="MultiLine" Width="99%" ReadOnly="True"></asp:TextBox>
                </td>
                <td class="auto-style35">
                    <asp:TextBox ID="txt_ach_stakeholder_2" runat="server" TextMode="MultiLine" Width="99%" ReadOnly="True"></asp:TextBox>
                </td>
                <td class="auto-style39">
                    <asp:DropDownList ID="ddl_spl_rating_2" runat="server">
                        <asp:ListItem Value="-1">Select</asp:ListItem>
                        <asp:ListItem Value="1">Agree</asp:ListItem>
                 
                        <asp:ListItem Value="3">Disagree</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="spl_row_3" runat="server" visible="false">
                <td class="auto-style36">
                    <asp:Label ID="lbl_spl_3" runat="server" Text="3"></asp:Label>
                </td>
                <td class="auto-style37">
                    <asp:TextBox ID="txt_ach_dtl_3" runat="server" TextMode="MultiLine" Width="99%" ReadOnly="True"></asp:TextBox>
                </td>
                <td class="auto-style31">
                    <asp:TextBox ID="txt_ach_benefit_3" runat="server" TextMode="MultiLine" Width="99%" ReadOnly="True"></asp:TextBox>
                </td>
                <td class="auto-style34">
                    <asp:DropDownList ID="ddl_ach_role_3" runat="server" Enabled="False">
                        <asp:ListItem Value="-1">Select</asp:ListItem>
                        <asp:ListItem Value="1">Individual Contributor</asp:ListItem>
                        <asp:ListItem Value="2">Part of Team</asp:ListItem>
                        <asp:ListItem Value="3">Idea Contributor</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td class="auto-style38">
                    <asp:TextBox ID="txt_ach_remark_3" runat="server" TextMode="MultiLine" Width="99%" ReadOnly="True"></asp:TextBox>
                </td>
                <td class="auto-style35">
                    <asp:TextBox ID="txt_ach_stakeholder_3" runat="server" TextMode="MultiLine" Width="99%" ReadOnly="True"></asp:TextBox>
                </td>
                <td class="auto-style39">
                    <asp:DropDownList ID="ddl_spl_rating_3" runat="server">
                        <asp:ListItem Value="-1">Select</asp:ListItem>
                        <asp:ListItem Value="1">Agree</asp:ListItem>
                        
                        <asp:ListItem Value="3">Disagree</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </asp:Panel>

    

    <%--   <asp:Panel ID="pnl_overall" runat="server" GroupingText="Overall Rating">
          <table class="auto-style29">
         <tr style="text-align:left;">
             <td>Rating against Overall Performance&nbsp;
                 <asp:DropDownList ID="ddl_over_rating" runat="server">
                     <asp:ListItem Value="-1">Select</asp:ListItem>
                     <asp:ListItem Value="5">Exceeds Expectation</asp:ListItem>
                     <asp:ListItem Value="4">Exceeds Some Expectation</asp:ListItem>
                     <asp:ListItem Value="3">Meets Expectation</asp:ListItem>
                     <asp:ListItem Value="2">Meets Some Expectation</asp:ListItem>
                     <asp:ListItem Value="1">Does Not Meet Expectation</asp:ListItem>
                 </asp:DropDownList>
                 <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddl_over_rating" ErrorMessage="Required"></asp:RequiredFieldValidator>
             </td>
         </tr>
     </table>
     </asp:Panel>
    --%>

    <br />

    <table style="width: 100%;">
        <tr>
            <td class="auto-style4">Reviewer Remarks</td>
            <td class="auto-style21">
                <asp:TextBox ID="txt_review_remarks" runat="server" TextMode="MultiLine" Width="99.7%" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style4">&nbsp;</td>
            <td class="auto-style21">&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style4">Approver Remarks</td>
            <td class="auto-style21">
                <asp:TextBox ID="txt_approver_remarks" runat="server" TextMode="MultiLine" Width="99.7%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style4">Status</td>
            <td class="auto-style21">
                <asp:DropDownList ID="ddl_status" runat="server" OnSelectedIndexChanged="ddl_status_SelectedIndexChanged">
                    <asp:ListItem Value="0">Select</asp:ListItem>
                    <asp:ListItem Value="1">Draft</asp:ListItem>
                    <asp:ListItem Value="2">Submit</asp:ListItem>
                    <asp:ListItem Value="3">Approved</asp:ListItem>
                    <asp:ListItem Value="4">Re-Submit</asp:ListItem>
                    <asp:ListItem Value="5">Cancel</asp:ListItem>
                    <asp:ListItem Value="6">Reviewed</asp:ListItem>
                </asp:DropDownList>
                <asp:RequiredFieldValidator ID="rfv_status" runat="server" ControlToValidate="ddl_status" ErrorMessage="Status Required" ForeColor="#CC0000" InitialValue="0" ValidationGroup="vg_save"></asp:RequiredFieldValidator>
            &nbsp;
            </td>
        </tr>
        <tr>
            <td class="auto-style4">Final Rating</td>
            <td class="auto-style21">
                <asp:Label ID="Lbl_Overall_Rating" runat="server" Font-Bold="True"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="Btn_Save" runat="server" Text="Save" OnClick="Btn_Save_Click" ValidationGroup="vg_save" Enabled="false" style="height: 26px" />
                <asp:Label ID="lbl_cid" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lbl_eid" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lbl_emp_pass" runat="server" Visible="False"></asp:Label>
                <asp:Label ID="lbl_cid_pass" runat="server" Text="" Visible="False"></asp:Label>
                <asp:Label ID="lbl_approver_type" runat="server" Text="" Visible="False"></asp:Label>
                 <asp:Button ID="Button_back" runat="server" Text="Back" OnClientClick="JavaScript:window.history.back(1); return false;"/>
            </td>
        </tr>
    </table>

    <br />
    <asp:Button ID="Btn_Verification" runat="server" Text="Rating Review" OnClick="Btn_Verification_Click" Visible="false" />
   
    <asp:Panel ID="pnl_overall" runat="server" GroupingText="Overall Rating" Visible="false">
        <table class="auto-style29">
            <tr style="text-align: left;">
                <td>Overall Percentage
                 <asp:Label ID="lbl_overall_percentage" runat="server" Font-Bold="True"></asp:Label>
                    &nbsp;and Rating against Overall Performance&nbsp;&nbsp;<asp:DropDownList ID="ddl_over_rating" runat="server" Visible="false">
                        <asp:ListItem Value="-1">Select</asp:ListItem>
                        <asp:ListItem Value="5">Exceeds Expectation</asp:ListItem>
                        <asp:ListItem Value="4">Exceeds Some Expectation</asp:ListItem>
                        <asp:ListItem Value="3">Meets Expectation</asp:ListItem>
                        <asp:ListItem Value="2">Meets Some Expectation</asp:ListItem>
                        <asp:ListItem Value="1">Does Not Meet Expectation</asp:ListItem>
                    </asp:DropDownList>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddl_over_rating" Enabled="false" ErrorMessage="Required"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr style="text-align: left;">
                <td>
                    <asp:Button ID="btn_dispute" runat="server" Text="Dispute on Review" OnClick="btn_dispute_Click" />
                </td>
            </tr>
        </table>
    </asp:Panel>


    <asp:Panel ID="Panel1" runat="server" Visible="false">

        <table class="auto-style1" style="display:none;">
            <tr>
                <td class="auto-style23">Employee Name</td>
                <td class="auto-style24">
                    <asp:DropDownList ID="DDL_Emp" runat="server" CssClass="auto-style2" AppendDataBoundItems="True" AutoPostBack="True" DataSourceID="SqlDataSource1" DataTextField="emp_name" DataValueField="emp_code" OnSelectedIndexChanged="DDL_Emp_SelectedIndexChanged" Width="30%">
                        <asp:ListItem Value="0">Select</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">&nbsp;</td>
                <td>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="select a.emp_code, a.emp_name from PCS_Login_Mst b, PCSEmpMaster a
where a.Emp_Code = b.Emp_Code and b.Company_id = @cid and b.Approver_id = @eid and b.Approver_Company_Id = @cid">
                        <SelectParameters>
                            <asp:SessionParameter Name="cid" SessionField="cid" />
                            <asp:SessionParameter Name="eid" SessionField="eid" />
                        </SelectParameters>
                    </asp:SqlDataSource>

                   

                </td>
            </tr>
        </table>

    </asp:Panel>
</asp:Content>

