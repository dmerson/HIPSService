<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="HIPSService.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            SSN:<input id="txtSSN" runat="server" value ="216121212"/>
        </div>
        <div>
            Dob:<input id="txtDOB" runat="server" value="1/1/1990"/>
        </div>
        <div>
            Key:<input id="txtKeys" runat="server" value="1/1/1991"/>
        </div>
        <div>
            <asp:Button runat="server" ID="btnGenerate" Text="Generate" OnClick="btnGenerate_Click"/>
        </div>
        <div >
            Pin:<span id="spanPin" runat="server"></span>

        </div>
       
        <div >
            Stamp:<span id="spanStamp" runat="server"></span>

        </div>
        <div >
            Full Object Encrypt SSN:<span id="spanFullObjectSSN" runat="server"></span>

        </div>
        <div >
            Full Object Encrypt DOB:<span id="spanFullObjectDOB" runat="server"></span>

        </div>
        <div >
            Full Object Decrypt SSN:<span id="spanFullObjectRealSSN" runat="server"></span>

        </div>
        <div >
            Full Object Decrypt DOB:<span id="spanFullObjectRealDOB" runat="server"></span>

        </div>

    </form>
</body>
</html>
