<%@ Page Title="" Language="C#" MasterPageFile="~/MainMenu.Master" AutoEventWireup="true" CodeBehind="UploadImage.aspx.cs" Inherits="HRKpi.UploadImage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


 <div class="container py-5" style="width: 66%; height: 333px;">  
       <div class="card" style="left: 0px; top: 0px; width: 869px; height: 328px;">
      
           <div class="card-header bg-blue text-white">
            <h4 class="text-uppercase text-center">PEP EMPLOYEE IMAGE UPLOAD</h4>
          </div>
        
         <div class="card-body">
            <form>

                 <div>
                       <asp:Label ID="lblerr" runat="server" ForeColor="Red"></asp:Label>   
                 </div>

                 <div style="margin-top:15px; text-align: right;">  
                         <asp:Button ID="button_add" runat="server" Font-Bold="True" Font-Size="Small" Text="Upload Image" OnClick="button_add_Click" />
&nbsp;
                         <asp:Button ID="button_exit" runat="server" Font-Bold="True" Font-Size="Small" OnClick="button_exit_click" Text="Exit" />
                         &nbsp;  
                        
                 </div> 

                 <div>



                     <table class="w-100" style="height: 214px">
                         
                         <tr>
                             <td style="width: 178px; height: 34px;">
                                 <asp:Label ID="Label2" runat="server" Font-Size="Small" Text="Company "></asp:Label>
                             </td>
                             <td style="width: 288px; height: 34px;">
                                 <asp:DropDownList ID="cbocompany" runat="server" AppendDataBoundItems="True" Width="265px" Font-Size="Small" Height="19px" AutoPostBack="True" OnSelectedIndexChanged="cbocompany_SelectedIndexChanged">
                                 <asp:ListItem Value="0">--Select Company--</asp:ListItem>
                                 </asp:DropDownList>

                             </td>
                             <td style="width: 29px; height: 34px;">
                                 &nbsp;</td>
                             <td style="width: 240px; vertical-align: middle;" rowspan="5">
                                 <asp:Image ID="Image2" runat="server" Height="177px" ImageUrl="~/images/blank_photo.jpg" BorderStyle="Outset" ImageAlign="Middle" Width="186px" />

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
                             <td style="width: 29px; height: 37px;">

                                 &nbsp;<td style="width: 118px; height: 37px;"></td>
                             <td style="height: 37px"></td>
                         </tr>
                         <tr>
                             <td style="width: 178px; height: 30px;">
                                 <asp:Label ID="Label4" runat="server" Font-Size="Small" Text="Employee Name"></asp:Label>
                             </td>
                             <td style="width: 288px; height: 30px;">

                                 <asp:Label ID="lblname" runat="server" Font-Bold="True" Font-Size="Small" Text="Label"></asp:Label>
                             <td style="width: 29px; height: 30px;">

                                 &nbsp;<td colspan="2" style="height: 30px">

                                 &nbsp;</td>
                         </tr>
                         <tr>
                             <td style="width: 178px; height: 67px;">
                                 <asp:Label ID="Label5" runat="server" Font-Size="Small" Text="Select Image"></asp:Label>
                             </td>
                             <td style="width: 288px; height: 67px;">

                                 <asp:FileUpload ID="FileUpload1" runat="server" Width="379px" />
                             <td style="width: 29px; height: 67px;">

                                 &nbsp;<td style="width: 118px; height: 67px;"></td>
                             <td style="height: 67px"></td>
                         </tr>
                         <tr>
                             <td style="width: 178px; ">
                                 &nbsp;</td>
                             <td style="width: 288px; ">
                                 <asp:Label ID="lblpath" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="Medium" ForeColor="#FF3300" Visible="False"></asp:Label>
                                  <asp:Label ID="lblfilename" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="Medium" ForeColor="#FF3300" Visible="False"></asp:Label><td style="width: 29px; ">
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


