<%@ Page Language="C#" MasterPageFile="~/QM.Master" AutoEventWireup="true" CodeBehind="ReportAvgWaitTime.aspx.cs" Inherits="QueueManagement.ReportAvgWaitTime" %>
<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
 
    <table width="950px">
    <tr>
        <td align="center"><asp:Label ID="lblClientName" runat="server"  
                ForeColor="White" />
                
                </td>
                 <td align="right">
                <asp:Button ID="btnAddQueue" runat="server" Height="25px" 
                Text="Manage Queue" Font-Size="Small" onclick="btnAddQueue_Click" /></td>
                
               
                
        <td align="right"> 
            <asp:LoginStatus ID="QMLoginStatus" runat="server" LogoutAction="RedirectToLoginPage" 
                LogoutPageUrl="~/Login.aspx" Font-Size="Medium" /></td>
    </tr>
    <tr>
    <td align="center"><asp:Label ID="lblUserName" runat="server" ForeColor="White" /></td>
        <td align="right"> 
            </td>
    </tr>
    
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
<table><tr> <td align="right">
                <asp:Button ID="btnReport" runat="server" Height="25px" Width="100px" Text="Customer List" Visible="true"
                 onclick="btnReport_Click" Font-Size="Small" />
                </td>
                <td align="right">
                <asp:Button ID="btnReport2" runat="server" Height="25px" Width="110px" 
                    Text="Walk-away List" onclick="btnReport2_Click" Font-Size="Small" /></td></tr></table>
 <asp:Label ID="lblMessage" runat="server" />
 <rsweb:ReportViewer ID="ReportViewer1"
     runat="server" Font-Names="Verdana" Font-Size="8pt" Height="400px" 
        Width="800px">
     <LocalReport ReportPath="rptAvgWaitTime.rdlc">
         <DataSources>
             <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" 
                 Name="dsQueue_spQM_GetAvgTimeByDateClient" />
         </DataSources>
     </LocalReport>
 </rsweb:ReportViewer>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
        TypeName="QueueManagement.dsQueueTableAdapters.spQM_GetAvgTimeByDateClientTableAdapter">
        <SelectParameters>
            <asp:Parameter Name="ClientID" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
 </asp:Content>
