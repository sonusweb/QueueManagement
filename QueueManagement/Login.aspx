<%@ Page Language="C#" MasterPageFile="~/QM.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="QueueManagement.Login" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">

    <table>
        <tr>
        <td style="width: 252px"></td>
        <td>
    <asp:Login ID="QMLogin" runat="server" BackColor="#F7F6F3" BorderColor="#E6E2D8" 
        BorderPadding="4" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" 
        Font-Size="0.8em" ForeColor="#333333" OnAuthenticate="LoginQM_Authenticate" 
        >
        <TextBoxStyle Font-Size="1em" />
        <LoginButtonStyle BackColor="#FFFBFF" BorderColor="#CCCCCC" BorderStyle="Solid" 
            BorderWidth="1px" Font-Names="Verdana" Font-Size="1em" ForeColor="#284775" />
        <InstructionTextStyle Font-Italic="True" ForeColor="Black" />
        <TitleTextStyle BackColor="#5D7B9D" Font-Bold="True" Font-Size="1.1em" 
            ForeColor="White" />
    </asp:Login>
    </td>
    </tr>
    </table>
</asp:Content>
