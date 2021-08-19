<%@ Control Language="c#" AutoEventWireup="false" Codebehind="WC_Menu.ascx.cs" Inherits="TPA.interfaz_empleado.WebControls.WC_Menu" %>
<script language="JavaScript" type="text/JavaScript">
<!--

function cambiarEstiloClick(lblMenu) 
{     
    var links = document.documentElement.getElementsByTagName("a");      
    if(links.length > 0)
    {
        for(var i = 0; i < links.length; i++)    
        {     
            if(links[i].className  == 'textSubmenuClick' || links[i].className  == 'textSubmenuOver' || links[i].className  == 'textSubmenu' )  
                links[i].className = 'textSubmenu';
        }
    }    
    
	if(lblMenu != null)
		lblMenu.className = 'textSubmenuClick';
	
}

function cambiarEstiloOver(lblMenu) 
{ 
    if(lblMenu != null)
        if(lblMenu.className  != 'textSubmenuClick')
            lblMenu.className = 'textSubmenuOver';
	    
}

function cambiarEstiloNormal(lblMenu) 
{
     if(lblMenu != null)
        if(lblMenu.className  != 'textSubmenuClick')
            lblMenu.className = 'textSubmenu';
}


//-->
</script>
<asp:DataList id="dtlMenu" runat="server" CssClass="tableMenu" CellPadding="0" CellSpacing="0"
	Width="100%">
	<ItemTemplate>
		<asp:HyperLink id="hplMenu" runat="server"></asp:HyperLink>
		<asp:Label id="lblMenu" runat="server">Label</asp:Label>
		</TD>
	</ItemTemplate>
	<ItemStyle Height="32"></ItemStyle>
</asp:DataList>
