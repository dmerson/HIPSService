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
            New DOB:<span id="spanNewDOB" runat="server"></span>

        </div>
        <div >
            Stamp:<span id="spanStamp" runat="server"></span>

        </div>
        <div >
            New SSN:<span id="spanNewSSNs" runat="server"></span>

        </div>
        <div >
            Unencrypt DOB:<span id="spanOldDOB" runat="server"></span>

        </div>
        <div >
            Unencrypt AAN:<span id="spanOldSSN" runat="server"></span>

        </div>

    </form>
</body>
</html>
