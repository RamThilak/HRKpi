<%@ Page Title="" Language="C#" MasterPageFile="~/MainMenu.Master" AutoEventWireup="true" CodeBehind="Objective_final_view.aspx.cs" Inherits="HRKpi.Objective_final_view" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    
     <div class="container py-5" style="width: 66%; height: 462px;">  
       <div class="card" style="left: 0px; top: 0px; width: 869px; height: 459px;">
      
           <div class="card-header bg-blue text-white">
            <h4 class="text-uppercase text-center">Annual Objective EVALUATION VIEW</h4>
          </div>
        
         <div class="card-body">
            <form>

                 <div>
                       <asp:Label ID="lblerr" runat="server" ForeColor="Red"></asp:Label>   
                 </div>

                 <div style="margin-top:15px; text-align: right;">  
&nbsp;
                         &nbsp;  
                        
                 </div> 

                
                   
                 <div>
                      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
               
                   <ContentTemplate>

                     <table class="w-100" style="height: 348px">
                         
                         <tr>
                             <td style="width: 178px; height: 42px;">
                                 <asp:Label ID="Label12" runat="server" Font-Size="Small" Text="Year"></asp:Label>
                             </td>
                             <td style="width: 461px; height: 42px;">
                                 <asp:DropDownList ID="cboyr" runat="server" Font-Size="Small" OnSelectedIndexChanged="cboyr_SelectedIndexChanged" AutoPostBack="True">
                                     <asp:ListItem Value="0">--Select Year--</asp:ListItem>
                                     <asp:ListItem>2021</asp:ListItem>
                                     <asp:ListItem>2022</asp:ListItem>
                                     <asp:ListItem>2023</asp:ListItem>
                                 </asp:DropDownList>

                                 <asp:Label ID="lblyr" runat="server" Visible="False"></asp:Label>

                             </td>
                         </tr>
                         
                         <tr>
                             <td style="width: 178px">
                                 <asp:Label ID="Label2" runat="server" Font-Size="Small" Text="Company "></asp:Label>
                             </td>
                             <td style="width: 461px">
                                 <asp:DropDownList ID="cbocompany" runat="server" AppendDataBoundItems="True" Width="328px" Font-Size="Small" Height="21px" AutoPostBack="True" OnSelectedIndexChanged="cbocompany_SelectedIndexChanged">
                                 <asp:ListItem Value="0">--Select Company--</asp:ListItem>
                                 </asp:DropDownList>

                                 <asp:Label ID="lblcomp" runat="server" Visible="False"></asp:Label>

                             </td>
                         </tr>
                        
                         <tr>
                             <td style="width: 178px">
                                 <asp:Label ID="Label4" runat="server" Font-Size="Small" Text="Employee Name"></asp:Label>
                             </td>
                             <td style="width: 461px">

                                 <asp:DropDownList ID="cboemp" runat="server" AppendDataBoundItems="True" Width="328px" Font-Size="Small" Height="24px" AutoPostBack="True" OnSelectedIndexChanged="cboemp_SelectedIndexChanged">
                                 <asp:ListItem Value="0">--Select Employee--</asp:ListItem>
                                 </asp:DropDownList>

                                 &nbsp;
                                 <asp:Label ID="lblempcode" runat="server" Visible="False"></asp:Label>
                             <td style="width: 118px">&nbsp;</td>
                             <td>&nbsp;</td>
                         </tr>
                         <tr>
                             <td style="width: 178px">
                                 <asp:Label ID="Label3" runat="server" Font-Size="Small" Text="Employee Code"></asp:Label>
                             </td>
                             <td style="width: 461px">

                                 <asp:Label ID="lblcode" runat="server" Font-Bold="True" Font-Size="Small" Text="Label"></asp:Label>
                             <td colspan="2">

                                 &nbsp;</td>
                         </tr>
                         <tr>
                             <td style="width: 178px">
                                 <asp:Label ID="Label11" runat="server" Font-Size="Small" Text="Reviewer Name"></asp:Label>
                             </td>
                             <td style="width: 461px">

                                 <asp:Label ID="lblreviewer" runat="server" Font-Bold="True" Font-Size="Small" Text="Label"></asp:Label>
                             
                             <td style="width: 118px">&nbsp;</td>
                             <td>&nbsp;</td>
                         </tr>
                         <tr>
                             <td style="width: 178px; ">
                                 <asp:Label ID="Label6" runat="server" Font-Size="Small" Text="Approver Name"></asp:Label>
                             </td>
                             <td style="width: 461px; ">

                                 <asp:Label ID="lblapprover" runat="server" Font-Bold="True" Font-Size="Small" Text="Label"></asp:Label>
                             
                             <td style="width: 118px">&nbsp;</td>
                             <td>&nbsp;</td>
                         </tr>
                         <tr>
                             <td style="width: 178px; ">
                                   &nbsp;</td>
                             <td style="width: 461px; text-align: center;">

                                 <asp:Button ID="button_getdetail" runat="server" Font-Size="Small" Height="31px" Text="Get Detail" OnClick="button_getdetail_Click" />
                             </td><td style="width: 118px">&nbsp;</td>
                             <td>&nbsp;</td>
                         </tr>
                         <tr>
                             <td style="width: 178px">&nbsp;</td>
                              <td style="width: 461px">&nbsp;</td>
                         </tr>
                     </table>

                              </ContentTemplate>
        </asp:UpdatePanel>

                 </div>
                       
                 
   
                </form>

             </div>
           </div>
         </div>

</asp:Content>
