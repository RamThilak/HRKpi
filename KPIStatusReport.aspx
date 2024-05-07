<%@ Page Title="" Language="C#" MasterPageFile="~/MainMenu.Master" AutoEventWireup="true" CodeBehind="KPIStatusReport.aspx.cs" Inherits="HRKpi.KPIStatusReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <div class="container py-5" style="width: 61%; height: 261px;">  
       <div class="card" style="left: 0px; top: 0px; width: 645px; height: 275px;">
      
           <div class="card-header bg-blue text-white">
            <h4 class="text-uppercase text-center">GROUP HR Reports</h4>
          </div>
        
         <div class="card-body">
            <form>

                 <div>
                       <asp:Label ID="lblerr" runat="server" ForeColor="Red"></asp:Label>   
                 </div>

                

                 <div>



                     <table class="w-100">
                         
                         <tr>
                             <td style="width: 181px">
                                 <asp:Label ID="Label2" runat="server" Font-Size="Small" Text="Company"></asp:Label>
                             </td>
                             <td style="width: 345px">
                                 <asp:DropDownList ID="cbocompany" runat="server" AppendDataBoundItems="True" Width="235px" Font-Size="Small" Height="22px">
                                 <asp:ListItem Value="0">--Select Company--</asp:ListItem>
                                 </asp:DropDownList>

                             </td>
                         </tr>

                          <tr>
                             <td style="width: 181px">
                                 <asp:Label ID="Label1" runat="server" Font-Size="Small" Text="Fin Year"></asp:Label>
                             </td>
                             <td style="width: 345px">
                                 <asp:DropDownList ID="cboyear" runat="server" AppendDataBoundItems="True" Width="235px" Font-Size="Small" Height="22px" CssClass="mr-0">
                                 <asp:ListItem Value="0">--Select Year--</asp:ListItem>
                                 </asp:DropDownList>

                             </td>
                         </tr>                       
                         


                        
                          <tr>
                             <td style="width: 181px">
                                 <asp:Label ID="Label7" runat="server" Font-Size="Small" Text="Report Name"></asp:Label>
                             </td>
                             <td style="width: 345px">
                                 <asp:DropDownList ID="cboreportname" runat="server" Font-Size="Small" Height="18px" Width="404px" AutoPostBack="True" OnSelectedIndexChanged="cboreportname_SelectedIndexChanged">
                                     <asp:ListItem Value="Overall KPI Status">Overall KPI Status</asp:ListItem>
                                     <asp:ListItem>Group Emlpoyee Experience Report</asp:ListItem>
                                     <asp:ListItem>Annual Objective Evaluation Status</asp:ListItem>
                                 </asp:DropDownList>

                             </td>
                         </tr>                       
                         


                        
                          <tr>
                             <td style="width: 181px">
                                 <asp:Label ID="Label8" runat="server" Font-Size="Small" Text="Month"></asp:Label>
                             </td>
                             <td style="width: 345px">
                                 <asp:DropDownList ID="cbomonth" runat="server" Font-Size="Small">
                                     <asp:ListItem Value="0">--Select Month--</asp:ListItem>
                                     <asp:ListItem Value="10">January</asp:ListItem>
                                     <asp:ListItem Value="11">February</asp:ListItem>
                                     <asp:ListItem Value="12">March</asp:ListItem>
                                     <asp:ListItem Value="1">April</asp:ListItem>
                                     <asp:ListItem Value="2">May</asp:ListItem>
                                     <asp:ListItem Value="3">June</asp:ListItem>
                                     <asp:ListItem Value="4">July</asp:ListItem>
                                     <asp:ListItem Value="5">August</asp:ListItem>
                                     <asp:ListItem Value="6">September</asp:ListItem>
                                     <asp:ListItem Value="7">October</asp:ListItem>
                                     <asp:ListItem Value="8">November</asp:ListItem>
                                     <asp:ListItem Value="9">December</asp:ListItem>
                                 </asp:DropDownList>

                             </td>
                         </tr>                       
                         


                        
                         <tr>
                             <td style="width: 181px">
                                 <asp:Label ID="Label6" runat="server" Font-Size="Small" Text="File Name"></asp:Label>
                             </td>
                             <td style="width: 345px">

                                 <asp:TextBox ID="txtsheetname" runat="server" Font-Size="Small" MaxLength="15" Width="228px" Height="16px"></asp:TextBox>
                             <td>&nbsp;</td>
                         </tr>
                        
                         <tr>
                             <td style="width: 181px">&nbsp;</td>
                              <td>&nbsp;</td>
                         </tr>
                        
                         <tr>
                             <td style="width: 181px">&nbsp;</td>
                              <td>
                                  <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Generate Report" />
&nbsp;
                                  <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="Exit" />
                             </td>
                         </tr>
                     </table>



                 </div>

   
                </form>

             </div>
           </div>
         </div>


</asp:Content>


