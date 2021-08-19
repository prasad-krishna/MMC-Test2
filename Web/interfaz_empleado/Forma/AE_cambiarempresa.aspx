<%@ Page language="c#" Codebehind="AE_cambiarempresa.aspx.cs" AutoEventWireup="false" Inherits="TPA.interfaz_admon.forma.AE_cambiarempresa" %>
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
	    </HEAD>
	<body onload="CargarConfiguracion()">
		<form id="Form1" method="post" runat="server" >
			<TABLE id="tblPrincipal" align=center cellSpacing="0" cellPadding="0" width="50%" border="0">
				<tr>
					<td>
						<TABLE class="tableBorder" id="table6" width="50%" align="center">
							<tr>
								<td>
									<TABLE id="Table1" cellSpacing="0" cellPadding="3" border="0">
										<TR>
											<TD class="titleBackBlue">
                                                <asp:Label ID="lblTitulo" runat="server" 
                                                    Text="CAMBIAR DE EMPRESA"></asp:Label></TD>
										</TR>
										<TR>
											<TD>&nbsp;&nbsp;</TD>
										</TR>
										<TR>
											<TD>
												<TABLE id="table3" cellSpacing="0" cellPadding="5" align="center" border="0">
													<TR>
														<TD>Empresa</TD>
														<TD>
															<asp:DropDownList id="ddlEmpresa" runat="server" Width="300px" CssClass="textBox"></asp:DropDownList>
															<asp:comparevalidator id="CompareValidator7" runat="server" CssClass="textRed" Operator="NotEqual" ValueToCompare="0"
																ErrorMessage="Requerido" Display="Dynamic" ControlToValidate="ddlEmpresa" ForeColor=" "></asp:comparevalidator></TD>
													</TR>
													<TR>
														<TD></TD>
														<TD></TD>
													</TR>
													<TR>
														<TD height="15"></TD>
														<TD height="15"></TD>
													</TR>
													<TR>
														<TD style="HEIGHT: 13px" align="center" colSpan="2"><asp:button id="btnAceptar" 
                                                                runat="server" CssClass="button" Text="Aceptar" onclick="btnIngresar_Click"></asp:button></TD>
													</TR>
													<TR>
														<TD height="15"></TD>
														<TD height="15"></TD>
													</TR>
													</TABLE>
											</TD>
										</TR>
										</TABLE>
								</td>
							</tr>
						</TABLE>
					</td>
				</tr>
			</TABLE>
			</form>
	</body>
</HTML>
