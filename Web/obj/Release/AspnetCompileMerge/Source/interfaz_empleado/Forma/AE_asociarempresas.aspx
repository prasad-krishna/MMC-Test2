<%@ Page language="c#" Codebehind="AE_asociarempresas.aspx.cs" AutoEventWireup="false" Inherits="TPA.interfaz_admon.forma.AE_asociarempresas" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title></title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../css/admon.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../../scripts/Base.js" type="text/javascript"></script>
		<script language="javascript" src="../../scripts/Validaciones.js" type="text/javascript"></script>
		<script language="javascript" type="text/javascript">
		    function EliminarAsociación(EmpresaID) {
		        if (confirm("¿Esta seguro que quiere eliminar la asociación de la empresa?")) {
		            window.location = "AE_AsociarEmpresas.aspx?EmpresaID=" + EmpresaID;
		        }
		    }
		</script>
	    <style type="text/css">
            .style3
            {
                font-size: 9px;
                color: #000000;
                font-family: Verdana, Tahoma, Arial, Helvetica, sans-serif;
                height: 20px;
            }
        </style>
	</HEAD>
	<body onload="CargarConfiguracion()">
		<form id="Form1" method="post" runat="server">
			<TABLE id="tblPrincipal" cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td>
						<TABLE class="tableBorder" id="table6" width="100%" align="center">
							<tr>
								<td>
									<TABLE id="Table1" cellSpacing="0" cellPadding="3" width="100%" border="0">
										<TR>
											<TD class="titleBackBlue" colSpan="2">
                                                <asp:Label ID="lblTitulo" runat="server" 
                                                    Text="ASOCIAR E IMPORTAR ASEGURADOS"></asp:Label></TD>
										</TR>
										<TR>
											<TD colSpan="2" align="left">
                                                <asp:Label ID="lblErrorPoliza" runat="server" ForeColor="#990000"></asp:Label>
                                            &nbsp;<asp:Label ID="Label1" runat="server" ForeColor="#990000" 
                                                    Text="Si la empresa no aparece en el listado de SICAM por favor contacte al administrador del sistema SICAM" 
                                                    Font-Bold="True"></asp:Label>
                                            </TD>
										</TR>
										<TR>
											<TD colSpan="2" align="left">
                                                <asp:Label ID="Label2" runat="server" 
                                                    Text="Seleccione la empresa que creó en HC" style="font-weight: 700"></asp:Label>
                                            </TD>                                                
                                              
										</TR>
										<TR>
											<TD width="15%">
                                                <asp:Label ID="lblEmpresaHC" runat="server" Text="Empresa"></asp:Label><SPAN class="textRed">*</SPAN>
                                            </TD>											
											<TD width="85%">
                                                <asp:dropdownlist id="ddlEmpresaHC" runat="server" Width="500px" 
                                                    CssClass="textBoxSmall" 
                                                    onselectedindexchanged="ddlEmpresaHC_SelectedIndexChanged" 
                                                    AutoPostBack =true ></asp:dropdownlist></TD>
											
										</TR>
										<TR>
											<TD>
                                                &nbsp;</TD>
											<TD>&nbsp;</TD>
										</TR>
										<TR>
											<TD colspan="2">
                                                    <asp:Label ID="Label3" runat="server" 
                                                    Text="Seleccione la empresa que desea asociar en SICAM" 
                                                    style="font-weight: 700"></asp:Label>
                                            </TD>
                                           
										</TR>
										<TR>
											<TD class="textSmallBlack" width="10%">
                                                <asp:Label ID="lblEmpresaSICAM1" runat="server" Text="Empresa SICAM"></asp:Label><SPAN class="textRed">*</SPAN></TD>
                                            <td>
                                                                                        
                                                <asp:dropdownlist id="ddlEmpresaSICAM" runat="server" 
                                                    Width="500px" CssClass="textBoxSmall" AutoPostBack="True" 
                                                    onselectedindexchanged="ddlEmpresaSICAM_SelectedIndexChanged"></asp:dropdownlist>
                                                                                        
                                            </td>
										</TR>
										<TR>
											<TD class="textSmallBlack">
                                                <asp:Label ID="lblEmpresaSICAM0" runat="server" Text="Póliza"></asp:Label><SPAN class="textRed">*</SPAN></TD>
                                            <td >
                                                                                        
                                                <asp:dropdownlist id="ddlPolizas" runat="server" 
                                                    Width="300px" CssClass="textBoxSmall"></asp:dropdownlist>
                                                                                        
                                            </td>
										</TR>
										<TR>
											<TD class="textSmallBlack">
                                                                                        
                                                <asp:Label ID="lblPolizaFecha" runat="server"></asp:Label>
                                                </TD>
                                            <td>
                                                                                        
                                                &nbsp;</td>
										</TR>
										<TR>
											<TD class="textSmallBlack" colspan="2">
                                                <asp:Label ID="lblCamposObligatorios" runat="server"                                                   
                                                    
                                                    
                                                    Text="Los campos marcados con (&lt;SPAN class=&quot;textRed&quot;&gt;*&lt;/span&gt;)son obligatorios"></asp:Label>
                                                <asp:DropDownList ID="dllFechaPoliza" style =" display:none"  runat="server">
                                                </asp:DropDownList>
                                                                                        
                                            </TD>
										</TR>
										<TR>
											<TD colspan="2" align=center>
                                                <asp:button id="btnAsociar" runat="server" 
                                                    CssClass="button" Text="Asociar" onclick="btnAsociar_Click"></asp:button>
                                                &nbsp;
                                                <asp:button id="btnImportar" runat="server" CssClass="button" Text="Importar" 
                                                    Visible="False"></asp:button></TD>
										</TR>										
										<TR>
											<TD colspan="2" style =" display :none "> <span class=textRed>*</span>Nota: Las empresas que tengan consultas registradas no se les podrá 
                                                eliminar la asociación.</TD>
											
										</TR>
										<TR>
											<TD colSpan="2" height="20">
                                                <asp:GridView ID="gvEmpresas" runat="server" AllowSorting="True" 
                                                    AutoGenerateColumns="False" CssClass="grid" Width="50%" AllowPaging="True" 
                                                    onpageindexchanging="gvEmpresas_PageIndexChanging">
                                                    <Columns>                                                    
                                                     <asp:TemplateField HeaderText="Eliminar asociación" Visible =false >
                                                    <ItemTemplate>                                                    
                                                        <a href='javascript:EliminarAsociación(<%# DataBinder.Eval(Container.DataItem, "empClave") %>)' class="txtNegro">
															<img src="../../iconos/ico_borrar.gif" border="0" language="Javascript">
														</a>                                                        
                                                    </ItemTemplate>
                                                    </asp:TemplateField>                                                      
                                                    <asp:TemplateField HeaderText="Empresa HC">
                                                    <ItemTemplate>                                                    
                                                        <asp:Label runat="server" ID="lblEmpresa" Text=<%# DataBinder.Eval(Container.DataItem, "nombre")%>></asp:Label>
                                                    </ItemTemplate>
                                                    </asp:TemplateField>     
                                                      <asp:TemplateField HeaderText="Empresa SICAM">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblEmpresaSicam" Text=<%# DataBinder.Eval(Container.DataItem, "emsRazon_social")%>></asp:Label>
                                                    </ItemTemplate>
                                                    </asp:TemplateField>                                                       
                                                    <asp:TemplateField HeaderText="Póliza">
                                                    <ItemTemplate>                                                    
                                                        <asp:Label runat="server" ID="lblPóliza" Text=<%# DataBinder.Eval(Container.DataItem, "polNombre")%>></asp:Label>
                                                    </ItemTemplate>
                                                    </asp:TemplateField>  
                                                    <asp:TemplateField HeaderText="Consultas">
                                                    <ItemTemplate>                                                    
                                                        <asp:Label runat="server" ID="lblConsultas" Text=<%# DataBinder.Eval(Container.DataItem, "TieneConsultas")%>></asp:Label>
                                                    </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fecha última modificación">
                                                    <ItemTemplate>                                                    
                                                        <asp:Label runat="server" ID="lblFechaModificacion" Text=<%# DataBinder.Eval(Container.DataItem, "FechaUltimaModificacion")%>></asp:Label>
                                                    </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Fecha última asociación">
                                                    <ItemTemplate>                                                    
                                                        <asp:Label runat="server" ID="lblFechaAsociación" Text=<%# DataBinder.Eval(Container.DataItem, "fechaUltimaAsociacion")%>></asp:Label>
                                                    </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Estado">
                                                    <ItemTemplate>                                                    
                                                        <asp:Label runat="server" ID="lblFechaAsociación" Text=<%# DataBinder.Eval(Container.DataItem, "estado")%>></asp:Label>
                                                    </ItemTemplate>
                                                    </asp:TemplateField>                                                         
                                                    </Columns>
                                                </asp:GridView>
                                                
                                            </TD>
										</TR>
										<TR>
											<TD class="textSmallBlack" colSpan="2" height="20">
												&nbsp;</TD>
										</TR>
										<TR>
											<TD class="style3" colSpan="2">
                                                &nbsp;</TD>
										</TR>
										</TABLE>
								</td>
							</tr>
						</TABLE>
					</td>
				</tr>
			</TABLE>
			&nbsp;
		</form>
	</body>
</HTML>
