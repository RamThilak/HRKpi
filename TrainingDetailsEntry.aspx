<%@ Page Title="" Language="C#" MasterPageFile="~/MainMenu.Master" AutoEventWireup="true" CodeBehind="TrainingDetailsEntry.aspx.cs" Inherits="HRKpi.TrainingDetailsEntry" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <div class="container py-5" style="width: 51%; height: 638px;">  
       <div class="card" style="left: 0px; top: 0px; width: 645px; height: 617px;">
      
           <div class="card-header bg-blue text-white">
            <h4 class="text-uppercase text-center">TRAINING DETAILS ENTRY</h4>
          </div>
        
         <div class="card-body">
            <form>

                 <div>
                       <asp:Label ID="lblerr" runat="server" ForeColor="Red"></asp:Label>   
                 </div>

                

                 <div>



                     <table class="w-100" style="height: 521px">
                         
                         <tr>
                             <td style="width: 124px">
                                 <asp:Label ID="Label2" runat="server" Font-Size="Small" Text="Company"></asp:Label>
                             </td>
                             <td colspan="3">
                                 <asp:DropDownList ID="cbocompany" runat="server" AppendDataBoundItems="True" Width="411px" Font-Size="Small" Height="20px" AutoPostBack="True" OnSelectedIndexChanged="cbocompany_SelectedIndexChanged">
                                 <asp:ListItem Value="0">--Select Company--</asp:ListItem>
                                 </asp:DropDownList>

                             </td>
                         </tr>

                         <tr>
                             <td style="width: 124px">
                                 <asp:Label ID="Label8" runat="server" Font-Size="Small" Text="Location"></asp:Label>
                             </td>
                             <td colspan="3">
                                 <asp:DropDownList ID="cbolocation" runat="server" AppendDataBoundItems="True" Width="411px" Font-Size="Small" Height="20px">
                                 <asp:ListItem Value="0">--Select Location--</asp:ListItem>
                                 </asp:DropDownList>

                             </td>
                         </tr>

                         <tr>
                             <td style="width: 124px">
                                 <asp:Label ID="Label9" runat="server" Font-Size="Small" Text="Training Topic"></asp:Label>
                             </td>
                             <td colspan="3">
                                 <asp:TextBox ID="txttopic" runat="server" MaxLength="150" Width="401px" Font-Size="Small"></asp:TextBox>

                             </td>
                         </tr>

                         <tr>
                             <td style="width: 124px">
                                 <asp:Label ID="Label10" runat="server" Font-Size="Small" Text="Eligible Department"></asp:Label>
                             </td>
                             <td colspan="3">
                                 <asp:DropDownList ID="cbodept" runat="server" AppendDataBoundItems="True" Width="411px" Font-Size="Small" Height="20px">
                                 <asp:ListItem Value="0">--Select Department--</asp:ListItem>
                                 </asp:DropDownList>

                             </td>
                         </tr>

                         <tr>
                             <td style="width: 124px">
                                 <asp:Label ID="Label12" runat="server" Font-Size="Small" Text="Eligible Grade"></asp:Label>
                             </td>
                             <td colspan="3">
                                 <asp:DropDownList ID="cbograde" runat="server" AppendDataBoundItems="True" Width="411px" Font-Size="Small" Height="20px">
                                 <asp:ListItem Value="0">--Select Grade--</asp:ListItem>
                                 </asp:DropDownList>

                             </td>
                         </tr>

                          <tr>
                             <td style="width: 124px">
                                 <asp:Label ID="Label1" runat="server" Font-Size="Small" Text="Training Date"></asp:Label>
                             </td>
                             <td colspan="3">
                                 <asp:TextBox ID="txtdt" runat="server" MaxLength="10" Font-Size="Small"></asp:TextBox>
                                 <cc2:calendarextender id="Dtdob" runat="server" targetcontrolid="txtdt" format="dd/MM/yyyy" /> 
                             </td>
                          
                            
                         </tr>                       
                         


                        
                          <tr>
                             <td style="width: 124px; height: 29px;">
                                 <asp:Label ID="Label7" runat="server" Font-Size="Small" Text="Duration ( Hrs )"></asp:Label>
                             </td>
                             <td style="height: 29px;" colspan="3">
                                 <asp:TextBox ID="txthr" runat="server" onkeypress='return event.charCode == 46 || (event.charCode >= 48 && event.charCode <= 57)' Font-Size="Small"></asp:TextBox>

                             </td>
                         </tr>                       
                         


                        
                         <tr>
                             <td style="width: 124px">
                                 <asp:Label ID="Label6" runat="server" Font-Size="Small" Text="Training Type"></asp:Label>
                             </td>
                             <td colspan="3">

                                 <asp:DropDownList ID="cbotype" runat="server" Font-Size="Small" Height="22px" Width="412px">
                                     <asp:ListItem Value="0">--Select Type--</asp:ListItem>
                                     <asp:ListItem Value="E">External</asp:ListItem>
                                     <asp:ListItem Value="I">Internal</asp:ListItem>
                                 </asp:DropDownList>
                             <td>&nbsp;</td>
                         </tr>
                        
                         <tr>
                             <td style="width: 124px">
                                 <asp:Label ID="Label13" runat="server" Font-Size="Small" Text="Batch Size"></asp:Label>
                             </td>
                              <td colspan="3">
                                  <asp:TextBox ID="txtsize" runat="server" Font-Size="Small" onkeypress='return (event.charCode >= 48 && event.charCode <= 57)' AutoPostBack="True" OnTextChanged="txtsize_TextChanged"></asp:TextBox>
                             </td>
                         </tr>
                        
                         <tr>
                             <td style="width: 124px">
                                 <asp:Label ID="Label14" runat="server" Font-Size="Small" Text="Venue"></asp:Label>
                             </td>
                              <td colspan="3">
                                  <asp:TextBox ID="txtvenue" runat="server" Font-Size="Small" MaxLength="150" Width="401px" ></asp:TextBox>
                             </td>
                         </tr>
                        
                         <tr>
                             <td style="width: 124px">
                                 <asp:Label ID="Label15" runat="server" Font-Size="Small" Text="Unit"></asp:Label>
                             </td>
                              <td>
                                  <asp:DropDownList ID="cbounit" runat="server" Font-Size="Small" Height="20px" Width="139px" AutoPostBack="True" OnSelectedIndexChanged="cbounit_SelectedIndexChanged">
                                      <asp:ListItem Value="0">--Select--</asp:ListItem>
                                      <asp:ListItem>Batch</asp:ListItem>
                                      <asp:ListItem Value="Person">Per Person</asp:ListItem>
                                  </asp:DropDownList>
                             </td>
                              <td>&nbsp;</td>
                              <td>&nbsp;</td>
                         </tr>
                        
                         <tr>
                             <td style="width: 124px">
                                 <asp:Label ID="Label16" runat="server" Font-Size="Small" Text="Cost/Unit (Rs.)"></asp:Label>
                             </td>
                              <td>
                                  <asp:TextBox ID="txtunitcost" runat="server" Width="129px" onkeypress='return event.charCode == 46 || (event.charCode >= 48 && event.charCode <= 57)' AutoPostBack="True" OnTextChanged="txtunitcost_TextChanged" Font-Size="Small"></asp:TextBox>
                             </td>
                              <td>&nbsp;</td>
                              <td>&nbsp;</td>
                         </tr>
                        
                         <tr>
                             <td style="width: 124px">
                                 <asp:Label ID="Label17" runat="server" Font-Size="Small" Text="Total Cost(Rs.)"></asp:Label>
                             </td>
                              <td>
                                  <asp:TextBox ID="txttotcost" runat="server" Width="133px" ReadOnly="True" Font-Size="Small" ></asp:TextBox>
                             </td>
                              <td>&nbsp;</td>
                              <td>&nbsp;</td>
                         </tr>
                        
                         <tr>
                             <td style="width: 124px">
                                 <asp:Label ID="Label18" runat="server" Font-Size="Small" Text="Faculty Name"></asp:Label>
                             </td>
                              <td colspan="3">
                                  <asp:TextBox ID="txtfacultyname" runat="server" Font-Size="Small" MaxLength="150" Width="401px"></asp:TextBox>
                             </td>
                         </tr>
                        
                         <tr>
                             <td style="width: 124px; height: 48px;">
                                 <asp:Label ID="Label19" runat="server" Font-Size="Small" Text="Remarks"></asp:Label>
                             </td>
                              <td colspan="3" style="height: 48px">
                                  <asp:TextBox ID="txtremarks" runat="server" MaxLength="1000" TextMode="MultiLine" Width="488px" Height="54px" Font-Size="Small"></asp:TextBox>
                             </td>
                         </tr>
                        
                         <tr>
                             <td style="width: 124px">&nbsp;</td>
                              <td colspan="3">
                                  &nbsp;</td>
                         </tr>
                        
                         <tr>
                             <td style="text-align: center;" colspan="4">
                                  <asp:Button ID="Button1" runat="server" OnClick="cmdsave_Click" Text="Save" Font-Bold="True" Width="80px" />
&nbsp;&nbsp;&nbsp;
                                  <asp:Button ID="cmdexit" runat="server" OnClick="cmdexit_Click" Text="Exit" Font-Bold="True" Width="80px" />
                             </td>
                         </tr>
                     </table>



                 </div>

   
                </form>

             </div>
           </div>
         </div>


</asp:Content>


