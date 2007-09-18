<%@ Control language="C#" Inherits="DnnCart.ViewProduct" AutoEventWireup="true" Codebehind="ViewProduct.ascx.cs" %>
<%@ Register TagPrefix="dnn" TagName="Audit" Src="~/controls/ModuleAuditControl.ascx" %>
<asp:datalist id="lstContent" datakeyfield="ProductId" runat="server" cellpadding="4" OnItemDataBound="lstContent_ItemDataBound">
  <itemtemplate>
    <table cellpadding="4" width="100%">
      <tr>
        <td valign="top" width="100%" align="left">
          <asp:HyperLink ID="HyperLink1" NavigateUrl='<%# EditUrl("ProductId",((int)DataBinder.Eval(Container.DataItem,"ProductId")).ToString()) %>' Visible="<%# IsEditable %>" runat="server"><asp:Image ID="Image1" runat="server" ImageUrl="~/images/edit.gif" AlternateText="Edit" Visible="<%# IsEditable%>" resourcekey="Edit"/></asp:HyperLink>
          <asp:Label ID="lblContent" runat="server" CssClass="Normal"/>
        </td>
      </tr>
    </table>
  </itemtemplate>
</asp:datalist>
