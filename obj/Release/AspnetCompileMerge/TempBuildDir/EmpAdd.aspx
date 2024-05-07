<%@ Page Title="" Language="C#" MasterPageFile="~/MainMenu.Master" AutoEventWireup="true" CodeBehind="EmpAdd.aspx.cs" Inherits="HRKpi.EmpAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <div class="container py-5" style="width: 60%; height: 470px;">  
       <div class="card" style="left: 0px; top: 0px; width: 755px; height: 459px;">
      
           <div class="card-header bg-blue text-white">
            <h4 class="text-uppercase text-center">KPI USER MANAGEMENT</h4>
          </div>
        
         <div class="card-body">
            <form>

                 <div>
                       <asp:Label ID="lblerr" runat="server" ForeColor="Red"></asp:Label>   
                 </div>

                 <div style="margin-top:15px; text-align: right;">  
                         &nbsp;  
                        <button type="submit" id="cmdExit" class="btn btn-primary rounded-0" onserverclick="cmdExit_click" runat="server" causesvalidation="false" >Exit </button>  
                 </div> 

                 <div>



                     <table class="w-100" style="height: 348px">
                         
                         <tr>
                             <td style="width: 325px">
                                 <asp:Label ID="Label2" runat="server" Font-Size="Small" Text="Company "></asp:Label>
                             </td>
                             <td style="width: 345px">
                                 <asp:DropDownList ID="cbocompany" runat="server" AppendDataBoundItems="True" Width="163px" Font-Size="Small" Height="23px">
                                 <asp:ListItem Value="0">--Select Company--</asp:ListItem>
                                 </asp:DropDownList>

                             </td>
                         </tr>
                        
                         <tr>
                             <td style="width: 325px">
                                 <asp:Label ID="Label5" runat="server" Font-Size="Small" Text="Choose Excel File"></asp:Label>
                             </td>
                             <td style="width: 345px">

                                 <asp:FileUpload ID="FileUpload1" runat="server" Font-Size="Small" Width="385px" />
                             <td>&nbsp;</td>
                         </tr>
                         <tr>
                             <td style="width: 325px">
                                 <asp:Label ID="Label6" runat="server" Font-Size="Small" Text="Sheet Name"></asp:Label>
                             </td>
                             <td style="width: 345px">

                                 <asp:TextBox ID="txtsheetname" runat="server" Font-Size="Small" MaxLength="15" Width="173px"></asp:TextBox>
                             <td>&nbsp;</td>
                         </tr>
                         <tr>
                             <td style="width: 325px">
                                 &nbsp;</td>
                             <td style="width: 345px">

                                 &nbsp;<td>&nbsp;</td>
                         </tr>
                         <tr>
                             <td style="width: 325px; text-align: center;">
                                    <button type="submit" id="cmdSave" class="btn btn-primary rounded-0" onserverclick="cmdSave_click" runat="server" style="width: 260px">Upload Employee Detail</button>  
                            </td>
                             <td style="width: 325px; text-align: center;">
                                    <button type="submit" id="cmdactivate" class="btn btn-primary rounded-0" onserverclick="cmdActivate_click" runat="server" style="width: 250px">Actvate / Deacivate Employee</button>  
                                <td>&nbsp;</td>
                         </tr>
                         <tr>
                             <td style="width: 325px; text-align: center;">
                                    &nbsp;</td>
                             <td style="width: 325px; text-align: center;">
                                    &nbsp;<td>&nbsp;</td>
                         </tr>
                         <tr>
                             <td style="width: 325px; text-align: center;">
                                   <button type="submit" id="cmdresign" class="btn btn-primary rounded-0" onserverclick="cmdResign_click" runat="server" style="width: 250px">Resignation Detail Upload</button>  </td>
                             <td style="width: 345px; text-align: center;">

                                 <button type="submit" id="Buttonopen" class="btn btn-primary rounded-0" onserverclick="cmdOpen_click" runat="server" style="width: 250px">Reset Annual Objective Status</button>  </td><td>&nbsp;</td>
                         </tr>
                         <tr>
                             <td style="width: 325px; text-align: center;">
                                   &nbsp;</td>
                             <td style="width: 345px; text-align: center;">

                                 &nbsp;</td><td>&nbsp;</td>
                         </tr>
                         <tr>
                             <td style="width: 325px; text-align: center;"><button type="submit" id="ButtonApprove" class="btn btn-primary rounded-0" onserverclick="cmdapprove_click" runat="server" style="width: 250px">Change Approval Structure</button> </td>
                              <td> </td><td>&nbsp;</td></td>
                         </tr>
                         <tr>
                             <td style="width: 325px">&nbsp;</td>
                              <td>&nbsp;</td>
                         </tr>
                     </table>



                 </div>

   
                </form>

             </div>
           </div>
         </div>


</asp:Content>

