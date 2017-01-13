<%@ Page Language="C#" MasterPageFile="~/QM.Master" AutoEventWireup="true" CodeBehind="AddCustomerQueue.aspx.cs" Inherits="QueueManagement.AddCustomerQueue" Title="Add customer" %>
  

      
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
 <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" />
 
    <table width="950px">
    <tr>
        <td align="center" style="width: 226px"><asp:Label ID="lblClientName" runat="server"  
                ForeColor="White" />
               - <asp:Label ID="lblUserName" runat="server" ForeColor="White" />
                
                </td>
                <td align="right">
                <asp:Button ID="btnReport" runat="server" Height="25px" Width="100px" Text="Reports" Visible="false"
                 onclick="btnReport_Click" Font-Size="Small" CausesValidation="false" />
                </td>
        <td align="right"> 
        <%--<asp:Button ID="QMLoginStatus" runat="server" LogoutAction="RedirectToLoginPage"  
                LogoutPageUrl="~/Login.aspx" Font-Size="Medium" />--%>
            <asp:LoginStatus ID="QMLoginStatus" runat="server" LogoutAction="RedirectToLoginPage" 
                LogoutPageUrl="~/Login.aspx" Font-Size="Medium" LoginText="Logout"  /></td>
    </tr>
  <%--  <tr>
    <td align="center"><asp:Label ID="lblUserName" runat="server" ForeColor="White" /></td>
        <td align="right"> 
            </td>
    </tr>--%>
    
    </table>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Content" runat="server">
    
   
<table id="tblMain" >
<tr><td><asp:Label ID="lblMsgRec" runat="server" Visible="true" />
</td>
<td>
<asp:Label ID="lblMsg" runat="server" Visible="true"  /></td></tr>

    <tr valign="top"><td style="width: 235px">
         
        <asp:Panel ID="Panel1" runat="server" Width="320px" BorderStyle="None" 
        GroupingText="Add Customer"   
        ToolTip="Add Customer" BackColor="White" ForeColor="#003366" >
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
        <table id="tblForm" style="font-size:small">
            <tr style="height:auto">
                <td style="width: 232px"><asp:Label ID="Label1" runat="server" Text="Name:"></asp:Label>
                </td>
                <td colspan="2">
                <asp:TextBox ID="tbxName" runat="server" Width="226px" EnableViewState="false"></asp:TextBox></td>
                
            </tr>
            <tr style="height:auto; font-size:x-small" ><td></td>
            <td colspan="2"><asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" 
                    ErrorMessage="Name Required!" ControlToValidate="tbxName">
                </asp:RequiredFieldValidator></td>
            </tr>
            <tr style="height:auto">
                <td style="width: 232px"><asp:Label ID="Label2" runat="server" Text="Mobile:"/>
                </td>
                <td colspan="2">
                <asp:TextBox ID="tbxMobile" runat="server" Height="19px" Width="226px"  />
                

                </td>
            </tr>
            <tr style="height:auto; font-size:x-small">
                <td></td>
                <td colspan="2"><asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" 
                        ErrorMessage="Mobile Required!" ControlToValidate="tbxMobile">
                    </asp:RequiredFieldValidator></td>
                </td>
            </tr>
            <tr style="height:auto">
                <td style="width: 232px" ><asp:Label ID="Label3" runat="server" Text="Party Size:"/>
                </td>
                <td style="width: 32px" >
                <asp:TextBox ID="tbxPartySize" runat="server" Width="71px"   />
              
                </td>
                <td><asp:CheckBox ID="cbxSendSms" runat="server" Text="Send SMS"   /></td>
                   
            </tr>
           <tr style="font-size:x-small">
            <td></td>
            <td colspan="2" >
                <asp:RequiredFieldValidator id="RequiredFieldValidator4" runat="server" 
                    ErrorMessage="Party size Required!" ControlToValidate="tbxPartySize" >
                </asp:RequiredFieldValidator>
               <asp:CompareValidator id="CompareValidator1" runat="server" 
                  ErrorMessage="You must enter a number" 
                  ControlToValidate="tbxPartySize" Type="Integer" 
                  Operator="DataTypeCheck"></asp:CompareValidator>
              </td>
             </tr>
            
            <tr style="height:auto">
            <td style="width: 232px"><asp:Label ID="Label4" runat="server" Text="Notes:" /></td>
            <td colspan="2">
                <asp:TextBox ID="tbxNotes" runat="server" Height="108px" 
                    TextMode="MultiLine" Width="222px"   />
            </td>
            </tr>
            <tr style="height:auto">
            <td style="width: 232px; ">
            <asp:Label ID="Label5" runat="server" Text="Wait Time:" />
            </td>
            <td style="width: 32px"  >
                <asp:TextBox ID="tbxWaitTime" runat="server" Width="81px" Text="5"  />
                
            </td>
            <td>
                <asp:Button ID="btnPlus" runat="server" Text="+" onclick="btnPlus_Click" />
                <asp:Button ID="btnMinus" runat="server" Text="-" onclick="btnMinus_Click" />
            </td>
            </tr>
            <tr style="height:auto; font-size:x-small">
            <td></td>
            <td colspan="2">
             <asp:RequiredFieldValidator id="RequiredFieldValidator3" runat="server" 
                    ErrorMessage="Waittime Required!" ControlToValidate="tbxWaitTime">
                </asp:RequiredFieldValidator>
            <asp:CompareValidator id="CompareValidator2" runat="server" 
                  ErrorMessage="You must enter a number" 
                  ControlToValidate="tbxWaitTime" Type="Integer" 
                  Operator="DataTypeCheck"></asp:CompareValidator>
            </td>
            </tr>
            <tr style="height:auto">
            <td colspan="3" align="right">
                <asp:Button ID="btnAddCustomer" runat="server" Text="Add to Queue" 
                    BackColor="#003366" ForeColor="#FFFF99" onclick="btnAddCustomer_Click" CausesValidation=true />
                </td>
            </tr>
           </table>
           </ContentTemplate>
          </asp:UpdatePanel>
        </asp:Panel>
        
    </td>
    <td>
    <asp:Panel runat="server" ID="Panel2" GroupingText="Update Status">
    <asp:UpdatePanel runat="server" id="UpdatePanel" updatemode="Always"  >
        <ContentTemplate>
        <asp:GridView ID="grdCustomerQueue" runat="server"  AutoGenerateColumns="False" 
            Font-Size="X-Small" OnRowCommand="grdCustomerQueue_RowCommand" Font-Bold="false" 
            OnRowDataBound="grdCustomerQueue_RowDataBound" OnRowUpdating="grdCustomerQueue_RowUpdating" 
            OnSelectedIndexChanged = "grdCustomerQueue_SelectedIndexChanged" 
                CellPadding="1" ForeColor="#333333" GridLines="Vertical" ToolTip="Update Status"
            > 
            <RowStyle BackColor="#EFF3FB" />
            <Columns>
                <asp:BoundField DataField="CustomerQueueID" />
                <asp:BoundField HeaderText="Name" DataField="QCustomerName" />
                <asp:BoundField HeaderText="Party Size" DataField="PartySize" />
                <asp:BoundField HeaderText="Wait Time" DataField="WaitingTime" />
                <asp:BoundField HeaderText="Date Time" DataField="CreatedDate" />
                <asp:BoundField HeaderText="Mobile No" DataField="MobileNo"/>
                <asp:BoundField HeaderText="Status" DataField="StatusDesc" />
               <%-- <asp:ButtonField ButtonType="Button" CommandName="CustomerLeft" HeaderText="Customer Left"
                Text="Customer Left" ControlStyle-Font-Size="X-Small" />--%>
               <asp:ButtonField ButtonType="Button" CommandName="RequestMoreTime" HeaderText="Request more time"
               Text="Request more time" ControlStyle-Font-Size="X-Small"  />
                <asp:ButtonField ButtonType="Button" CommandName="Serve" HeaderText="Table ready"
                 Text="Table ready"  ControlStyle-Font-Size="X-Small" />
            </Columns>
                            
            
                            
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <EditRowStyle BackColor="#2461BF" />
            <AlternatingRowStyle BackColor="White" />
                            
            
                            
        </asp:GridView>
        </ContentTemplate>
        </asp:UpdatePanel>
        </asp:Panel>
        
     <%--  <asp:ListView ID="lvwMessageRec" runat="server" >
           <LayoutTemplate>
            <table>
            <tr runat="server" ID="itemPlaceholder"></tr>
            </table>
            </LayoutTemplate>
            <ItemTemplate>
            <%# Container.DataItem %>
          
            </ItemTemplate>

        
        </asp:ListView>--%>
       
        
       
         </td>
</tr>
<tr>
<td colspan="2">

 <asp:Repeater ID="rptMsgRec" runat="server">
            <ItemTemplate>    
                <%# Container.DataItem %><br />
            </ItemTemplate>
        </asp:Repeater>
      
        </td></tr>
   
</table>

   
 
    
    </asp:Content>
  