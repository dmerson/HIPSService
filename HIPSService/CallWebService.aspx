<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CallWebService.aspx.cs" Inherits="HIPSService.CallWebService" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Scripts/jquery-2.0.3.js"></script>
    <script>
        
        function encryptService()
        {
            dta = "{ssn:'" + $( "#txtSSN" ).val() + "',dob:'" + $( "#txtDOB" ).val() + "'}";
            
            $.ajax( {
                type: "POST",
                url: "BasicHips.asmx/Encrypt",
                data: dta,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function ( msg )
                {
                    $("#answer").html("ssn =" + msg.d.SSN + ", dob:" + msg.d.DOB);
                },
                error: function (msg)
                    {
                    alert("Error" + msg.error);
                    }
            } );
        }
        function decryptService()
        {
            dta = "{ssn:'" + $( "#txtSSN" ).val() + "',dob:'" + $( "#txtDOB" ).val() + "'}";

            $.ajax( {
                type: "POST",
                url: "BasicHips.asmx/Decrypt",
                data: dta,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function ( msg )
                {
                    $( "#answer" ).html( "ssn =" + msg.d.SSN + ", dob:" + msg.d.DOB );
                },
                error: function ( msg )
                {
                    alert( "Error" + msg.error );
                }
            } );
        }
        $( document ).ready( function ()
        {
            
        } );
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div>
                SSN:<input id="txtSSN" runat="server" value ="216121212"/>
            </div>
            <div>
                Dob:<input id="txtDOB" runat="server" value="1/1/1990"/>
            </div>
            <div>
                <input type="button" value ="Encrypt" onclick="encryptService()"/>
                <input type="button" value ="Decrypt" onclick="decryptService()"/>
            </div>
        </div>
        <div id="answer"></div>
    </form>
</body>
</html>
