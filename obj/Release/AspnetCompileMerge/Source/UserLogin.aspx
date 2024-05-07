<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserLogin.aspx.cs" Inherits="HRKpi.UserLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> Performance Management System - HR Portal</title>
     <link href="Content/BootstrapStyleSheet.css" rel="stylesheet" />
    <meta name ="viewport" content ="width=device-width,initial-scale=1" />  


    <style type="text/css">  


html, body {
  margin:0px;
  height:100%;
}

.box {
  background-color:lightblue;
  height:100vh;
  background:lightblue;
  position:absolute;
  top:0px;
  right:0px;
  bottom:0px;
  left:0px;
/*  text-align: center;*/
}
   .centered {
  position: fixed;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
}
  
       
  
        .auto-style1 {
            position: relative;
            width: 100%;
            min-height: 1px;
            -webkit-box-flex: 0;
            -ms-flex: 0 0 41.666667%;
            flex: 0 0 41.666667%;
            max-width: 41.666667%;
            left: 0px;
            top: 0px;
            padding-left: 15px;
            padding-right: 15px;
        }
  
       
  
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div class="container">

        <div class="box" style="background-image: url('images/perf1.jpg')" >

           <div class="card centered" style="left: 50%; top: 50%; width: 348px" >
            <div class="card-header" style="text-align: center; font-size: medium; font-weight: bold; color: #FFFF00; background-color: #0000FF;">
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="Large" Text="HR Portal Login"></asp:Label>
            </div>
             <div class="card-body">

                  <div class="container" >
                    <div class="row">
                         <div class="col-lg-12">
                             <asp:RadioButton ID="Kpi_Button" runat="server" Text ="KPI" GroupName="GrpLogin" Font-Bold="true" />
                             <asp:RadioButton ID="pep_Button" runat="server" Text="PEP" GroupName="GrpLogin" Font-Bold="true" />
                       </div>
                    </div>
                 <div class="container" >
                    <div class="row">
                         <div class="auto-style1">
                             <p>User Name</p>
                         </div>
                         <div class="col-lg-7">
                            <asp:TextBox ID="TxtUserName" runat="server" Width="159px" MaxLength="15"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Empty User Name" ForeColor="Red" ControlToValidate="TxtUserName"></asp:RequiredFieldValidator>
                       </div>
                    </div>
                      <div class="row">
                        <div class="col-lg-5">
                             <p>Password</p>
                         </div>
                         <div class="col-lg-7">
                            <asp:TextBox ID="TxtPassword"  runat="server" TextMode="Password" Width="162px" MaxLength="10"></asp:TextBox>
                             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Empty Password" ForeColor="Red" ControlToValidate="TxtPassword"></asp:RequiredFieldValidator>
                       </div>

                           <asp:Label ID="lblerr" runat="server" ForeColor="Red" Text="Label"></asp:Label>

                    </div>
                    
                     
              </div>
                  <div style="text-align: right">
                      <asp:Button ID="button_sumbit" runat="server" Text="Submit" OnClick="buttonsubmit_click" />
                  </div>
            </div>
          </div>

        </div>
       </div>
    </form>

    
      <script src="Scripts/bootstrap.js"></script>
    <script src="Scripts/jquery-3.6.0.js"></script>


</body>

</html>
