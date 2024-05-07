<%@ Page Title="" Language="C#" MasterPageFile="~/MainMenu.Master" AutoEventWireup="true" CodeBehind="ChangeKPIApprover.aspx.cs" Inherits="HRKpi.ChangeKPIApprover" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <div class="container py-5" style="width: 66%; height: 462px;">  
       <div class="card" style="left: 0px; top: 0px; width: 869px; height: 459px;">
      
           <div class="card-header bg-blue text-white">
            <h4 class="text-uppercase text-center">PEP USER MANAGEMENT</h4>
          </div>
        
         <div class="card-body">
            <form>

                 <div>
                       <asp:Label ID="lblerr" runat="server" ForeColor="Red"></asp:Label>   
                 </div>

                 <div style="margin-top:15px; text-align: right;">  
                         <asp:Button ID="button_add" runat="server" Font-Bold="True" Font-Size="Small" Text="Change Approver" OnClick="button_add_Click" />
&nbsp;
                         <asp:Button ID="button_exit" runat="server" Font-Bold="True" Font-Size="Small" OnClick="button_exit_click" Text="Exit" />
                         &nbsp;  
                        
                 </div> 

                 <div>



                     <table class="w-100" style="height: 348px">
                         
                         <tr>
                             <td style="width: 178px">
                                 <asp:Label ID="Label2" runat="server" Font-Size="Small" Text="Company "></asp:Label>
                             </td>
                             <td style="width: 288px">
                                 <asp:DropDownList ID="cbocompany" runat="server" AppendDataBoundItems="True" Width="265px" Font-Size="Small" Height="19px">
                                 <asp:ListItem Value="0">--Select Company--</asp:ListItem>
                                 </asp:DropDownList>

                             </td>
                         </tr>
                        
                         <tr>
                             <td style="width: 178px">
                                 <asp:Label ID="Label3" runat="server" Font-Size="Small" Text="Employee Code"></asp:Label>
                             </td>
                             <td style="width: 288px">

                                 <asp:TextBox ID="txtcode" runat="server" Font-Size="Small" Width="153px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;
                                 <asp:Button ID="button_getdetail" runat="server" Font-Size="Small" Height="31px" Text="Get Detail" OnClick="button_getdetail_Click" />
                             <td style="width: 118px">&nbsp;</td>
                             <td>&nbsp;</td>
                         </tr>
                         <tr>
                             <td style="width: 178px">
                                 <asp:Label ID="Label4" runat="server" Font-Size="Small" Text="Employee Name"></asp:Label>
                             </td>
                             <td style="width: 288px">

                                 <asp:Label ID="lblname" runat="server" Font-Bold="True" Font-Size="Small" Text="Label"></asp:Label>
                             <td colspan="2">

                                 <asp:Label ID="lblmailid" runat="server" Font-Bold="True" Font-Size="Small"></asp:Label>
                             </td>
                         </tr>
                         <tr>
                             <td style="width: 178px">
                                 <asp:Label ID="Label11" runat="server" Font-Size="Small" Text="Approver 1 Detail"></asp:Label>
                             </td>
                             <td style="width: 288px">

                                 <asp:Label ID="lblapp1" runat="server" Font-Bold="True" Font-Size="Small" Text="Label"></asp:Label>
                             
                             <td style="width: 118px">&nbsp;</td>
                             <td>&nbsp;</td>
                         </tr>
                         <tr>
                             <td style="width: 178px; ">
                                 <asp:Label ID="Label12" runat="server" Font-Size="Small" Text="Approver 2 Detail"></asp:Label>
                             </td>
                             <td style="width: 288px; ">

                                 <asp:Label ID="lblapp2" runat="server" Font-Bold="True" Font-Size="Small" Text="Label"></asp:Label>
                             
                             <td style="width: 118px">&nbsp;</td>
                             <td>&nbsp;</td>
                         </tr>
                         <tr>
                             <td style="width: 178px; ">
                                 <asp:Label ID="Label7" runat="server" Font-Size="Small" Text="Level 1 Approver Company"></asp:Label>
                             </td>
                             <td style="width: 288px; ">

                                 <asp:DropDownList ID="cbolevel1company" runat="server" AppendDataBoundItems="True" Width="268px" Font-Size="Small" Height="19px" AutoPostBack="True" OnSelectedIndexChanged="cbolevel1company_SelectedIndexChanged">
                                 <asp:ListItem Value="0">--Select Company--</asp:ListItem>
                                 </asp:DropDownList>

                             </td><td style="width: 118px">
                                 <asp:Label ID="Label8" runat="server" Font-Size="Small" Text="Level 1 Approver "></asp:Label>
                             </td>
                             <td>
                                 <asp:DropDownList ID="cbolevel1emp" runat="server" AppendDataBoundItems="True" Width="268px" Font-Size="Small" Height="19px">
                                 <asp:ListItem Value="0">--Select Name--</asp:ListItem>
                                 </asp:DropDownList>

                             </td>
                         </tr>
                         <tr>
                             <td style="width: 178px; ">
                                 <asp:Label ID="Label9" runat="server" Font-Size="Small" Text="Level 2 Approver Company"></asp:Label>
                             </td>
                              <td style="width: 288px"> 
                                 <asp:DropDownList ID="cbolevel2company" runat="server" AppendDataBoundItems="True" Width="268px" Font-Size="Small" Height="19px" AutoPostBack="True" OnSelectedIndexChanged="cbolevel2company_SelectedIndexChanged">
                                 <asp:ListItem Value="0">--Select Company--</asp:ListItem>
                                 </asp:DropDownList>

                             </td><td style="width: 118px">
                                 <asp:Label ID="Label10" runat="server" Font-Size="Small" Text="Level 2 Approver "></asp:Label>
                             </td></td>
                             <td>
                                 <asp:DropDownList ID="cbolevel2emp" runat="server" AppendDataBoundItems="True" Width="268px" Font-Size="Small" Height="19px">
                                 <asp:ListItem Value="0">--Select Name--</asp:ListItem>
                                 </asp:DropDownList>

                             </td>
                         </tr>
                         <tr>
                             <td style="width: 178px">&nbsp;</td>
                              <td style="width: 288px">&nbsp;</td>
                         </tr>
                     </table>



                 </div>

   
                </form>

             </div>
           </div>
         </div>


</asp:Content>


