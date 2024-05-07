<%@ Page Title="" Language="C#" MasterPageFile="~/MainMenu.Master" AutoEventWireup="true" CodeBehind="TrainingReport.aspx.cs" Inherits="HRKpi.TrainingReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <div class="container py-5" style="width: 61%; height: 261px;">  
       <div class="card" style="left: 0px; top: 0px; width: 645px; height: 275px;">
      
           <div class="card-header bg-blue text-white">
            <h4 class="text-uppercase text-center">KPI Reports</h4>
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
                                 <asp:DropDownList ID="cbocompany" runat="server" AppendDataBoundItems="True" Width="374px" Font-Size="Small" Height="23px">
                                 <asp:ListItem Value="0">--Select Company--</asp:ListItem>
                                 </asp:DropDownList>

                             </td>
                         </tr>

                          <tr>
                             <td style="width: 181px">
                                 <asp:Label ID="Label1" runat="server" Font-Size="Small" Text="From Date"></asp:Label>
                             </td>
                             <td style="width: 345px">
                                 <asp:TextBox ID="txtfdt" runat="server" MaxLength="10" Font-Size="Small"></asp:TextBox>
                                 <cc2:calendarextender id="Dtfdt" runat="server" targetcontrolid="txtfdt" format="dd/MM/yyyy" /> 

                             </td>
                         </tr>                       
                         


                        
                          <tr>
                             <td style="width: 181px">
                                 <asp:Label ID="Label8" runat="server" Font-Size="Small" Text="To Date"></asp:Label>
                             </td>
                             <td style="width: 345px">
                                 <asp:TextBox ID="txttdt" runat="server" MaxLength="10" Font-Size="Small"></asp:TextBox>
                                 <cc2:calendarextender id="txttdt_CalendarExtender" runat="server" targetcontrolid="txttdt" format="dd/MM/yyyy" /> 

                              </td>
                         </tr>                       
                         


                        
                         <tr>
                             <td style="width: 181px">
                                 <asp:Label ID="Label6" runat="server" Font-Size="Small" Text="File Name"></asp:Label>
                             </td>
                             <td style="width: 345px">

                                 <asp:TextBox ID="txtsheetname" runat="server" Font-Size="Small" MaxLength="15" Width="362px" Height="16px"></asp:TextBox>
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



