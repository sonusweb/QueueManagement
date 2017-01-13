<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Customer.aspx.cs" Inherits="QueueManagement.Customer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Label ID="lblClientName" runat="server" Text="Client Name" />
    <br />
    <br />
    <asp:Panel ID="Panel1" runat="server" Width="466px" BorderStyle="None" 
    GroupingText="Add Customer"   
    ToolTip="Add Customer" BackColor="White" ForeColor="#003366" >
        
<table>
    <tr>
        <td><asp:Label ID="Label1" runat="server" Text="Name:"></asp:Label>
        </td>
        <td colspan="2">
        <asp:TextBox ID="tbxName" runat="server" Width="226px"></asp:TextBox></td>
    </tr>
    <tr>
        <td><asp:Label ID="Label2" runat="server" Text="Mobile:"/>
        </td>
        <td colspan="2">
        <asp:TextBox ID="tbxMobile" runat="server" Height="19px" Width="226px" />
        </td>
    </tr>
    <tr>
    <td ><asp:Label ID="Label3" runat="server" Text="Party Size:"/>
        </td>
        <td colspan="2">
        <asp:TextBox ID="tbxPartySize" runat="server" Width="71px" />
        </td>
        
        
    </tr>
    <tr>
    <td>
        <asp:CheckBox ID="cbxOnlyCall" runat="server" Text="Only Voice Call" />

        </td>
        <td colspan="2">
        <asp:CheckBox ID="cbxSmsMarket" runat="server" Text="SMS Marketing" />
        </td>
    
    </tr>
    <tr>
    <td><asp:Label ID="Label4" runat="server" Text="Notes:" /></td>
    <td colspan="2">
        <asp:TextBox ID="tbxNotes" runat="server" Height="80px" 
            TextMode="MultiLine" Width="278px" />
    </td>
    </tr>
    <tr>
    <td>
    <asp:Label ID="Label5" runat="server" Text="Waiting Time:" />
    </td>
    <td  >
        <asp:TextBox ID="tbxWaitTime" runat="server" />
    </td>
    <td>
        <asp:Button ID="btnPlus" runat="server" Text="+" />
        <asp:Button ID="btnMinus" runat="server" Text="-" />
    </td>
    </tr>
    
    <tr>
    <td colspan="3" align="right">
        <asp:Button ID="btnAddCustomer" runat="server" Text="Add to Queue" 
            BackColor="#003366" ForeColor="#FFFF99" onclick="btnAddCustomer_Click" />
        </td>
    </tr>
   </table>
</asp:Panel>


    </form>
</body>
</html>
