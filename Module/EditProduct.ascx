<%@ Control language="C#" Inherits="DnnCart.EditProduct" AutoEventWireup="true" Codebehind="EditProduct.ascx.cs" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="TextEditor" Src="~/controls/TextEditor.ascx"%>
<%@ Register TagPrefix="dnn" TagName="Audit" Src="~/controls/ModuleAuditControl.ascx" %>
<table width="650" cellspacing="0" cellpadding="0" border="0" summary="Edit Table">
	<tr valign="top">
		<td class="SubHead" colspan="2">
    		<dnn:label id="lblDetails" runat="server" controlname="lblDetails" suffix=":"></dnn:label>
    	</td>
	</tr>
	<Tr>
		<td>
		    <dnn:Label ID="lblProductName" runat="server" ResourceKey="lblProductName" />
		</td>
		<td>
		    <asp:TextBox ID="txtProductName" runat="server"></asp:TextBox>
		</td>
	</Tr>
    <tr>
        <td>
    	    <dnn:Label ID="lblProductCost" runat="server" ResourceKey="lblProductCost" />
        </td>
        <td>
    	    <asp:TextBox ID="txtProductCost" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
    	    <dnn:Label ID="lblProductPrice" runat="server" ResourceKey="lblProductPrice" />
    	</td>
    	<td>
    	    <asp:TextBox ID="txtProductPrice" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td>
            <dnn:Label ID="lblShortDescription" runat="server" ResourceKey="lblShortDescription" />
        </td>
        <td>
            <dnn:texteditor id="txtShortDescription" runat="server" height="300" width="500" />
            <asp:RequiredFieldValidator ID="valShortDescription" ResourceKey="valShortDescription.ErrorMessage" ControlToValidate="txtShortDescription"
				CssClass="NormalRed" Display="Dynamic" ErrorMessage="<br>Short Description is required" runat="server" />f
        </td>
    </tr>
    <tr>
        <td>
		   	<dnn:Label ID="lblLongDescription" runat="server" ResourceKey="lblLongDescription" />
		</td>
		<td>
		    <dnn:texteditor id="txtLongDescription" runat="server" height="400" width="500" />
		</td>
	</tr>
</table>
<p>
    <asp:linkbutton cssclass="CommandButton" id="cmdUpdate" OnClick="cmdUpdate_Click" resourcekey="cmdUpdate" runat="server" borderstyle="none" text="Update"></asp:linkbutton>&nbsp;
    <asp:linkbutton cssclass="CommandButton" id="cmdCancel" OnClick="cmdCancel_Click" resourcekey="cmdCancel" runat="server" borderstyle="none" text="Cancel" causesvalidation="False"></asp:linkbutton>&nbsp;
    <asp:linkbutton cssclass="CommandButton" id="cmdDelete" OnClick="cmdDelete_Click" resourcekey="cmdDelete" runat="server" borderstyle="none" text="Delete" causesvalidation="False"></asp:linkbutton>&nbsp;
</p>
<dnn:audit id="ctlAudit" runat="server" />
