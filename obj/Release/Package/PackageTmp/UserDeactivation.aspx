<%@ Page Title="" Language="C#" MasterPageFile="~/MainMenu.Master" AutoEventWireup="true" CodeBehind="UserDeactivation.aspx.cs" Inherits="HRKpi.UserDeactivation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <div class="container py-5" style="width: 66%; height: 333px;">  
       <div class="card" style="left: 0px; top: 0px; width: 869px; height: 314px;">
      
           <div class="card-header bg-blue text-white">
            <h4 class="text-uppercase text-center">PEP USER DEACTIVATION</h4>
          </div>
        
         <div class="card-body">
            <form>

                 <div>
                       <asp:Label ID="lblerr" runat="server" ForeColor="Red"></asp:Label>   
                 </div>

                 <div style="margin-top:15px; text-align: right;">  
                         <asp:Button ID="button_add" runat="server" Font-Bold="True" Font-Size="Small" Text="Deactivate" OnClick="button_add_Click" />
&nbsp;
                         <asp:Button ID="button_exit" runat="server" Font-Bold="True" Font-Size="Small" OnClick="button_exit_click" Text="Exit" />
                         &nbsp;  
                        
                 </div> 

                 <div>



                     <table class="w-100" style="height: 203px">
                         
                         <tr>
                             <td style="width: 178px; height: 34px;">
                                 <asp:Label ID="Label2" runat="server" Font-Size="Small" Text="Company "></asp:Label>
                             </td>
                             <td style="width: 288px; height: 34px;">
                                 <asp:DropDownList ID="cbocompany" runat="server" AppendDataBoundItems="True" Width="265px" Font-Size="Small" Height="19px">
                                 <asp:ListItem Value="0">--Select Company--</asp:ListItem>
                                 </asp:DropDownList>

                             </td>
                         </tr>
                        
                         <tr>
                             <td style="width: 178px; height: 37px;">
                                 <asp:Label ID="Label3" runat="server" Font-Size="Small" Text="Employee Code"></asp:Label>
                             </td>
                             <td style="width: 288px; height: 37px;">

                                 <asp:TextBox ID="txtcode" runat="server" Font-Size="Small" Width="153px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;
                                 <asp:Button ID="button_getdetail" runat="server" Font-Size="Small" Height="31px" Text="Get Detail" OnClick="button_getdetail_Click" />
                             <td style="width: 118px; height: 37px;"></td>
                             <td style="height: 37px"></td>
                         </tr>
                         <tr>
                             <td style="width: 178px; height: 30px;">
                                 <asp:Label ID="Label4" runat="server" Font-Size="Small" Text="Employee Name"></asp:Label>
                             </td>
                             <td style="width: 288px; height: 30px;">

                                 <asp:Label ID="lblname" runat="server" Font-Bold="True" Font-Size="Small" Text="Label"></asp:Label>
                             <td colspan="2" style="height: 30px">

                                 <asp:Label ID="lblmailid" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
                             </td>
                         </tr>
                         <tr>
                             <td style="width: 178px; height: 67px;">
                                 <asp:Label ID="Label5" runat="server" Font-Size="Small" Text="Deactivation Remarks"></asp:Label>
                             </td>
                             <td style="width: 288px; height: 67px;">

                                 <asp:TextBox ID="txtremarks" runat="server" Height="49px" MaxLength="250" TextMode="MultiLine" Width="328px"></asp:TextBox>

                             <td style="width: 118px; height: 67px;"></td>
                             <td style="height: 67px"></td>
                         </tr>
                         <tr>
                             <td style="width: 178px; ">
                                 &nbsp;</td>
                             <td style="width: 288px; ">
                                 &nbsp;<td style="width: 118px">&nbsp;</td>
                             <td>&nbsp;</td>
                         </tr>
                         </table>



                 </div>

   
                </form>

             </div>
           </div>
         </div>


</asp:Content>


