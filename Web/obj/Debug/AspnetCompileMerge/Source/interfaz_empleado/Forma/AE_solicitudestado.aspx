<%@ Page language="c#" Codebehind="AE_solicitudestado.aspx.cs" AutoEventWireup="false" Inherits="TPA.interfaz_empleado.forma.AE_solicitudestado" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>HC-Historias Clínicas</title>
		<meta content="False" name="vs_showGrid">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../css/admon.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../../scripts/Base.js" type="text/javascript"></script>
		<LINK href="../../css/Calendar.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../../scripts/Calendar.js" type="text/javascript"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="Z-INDEX: 101; LEFT: 8px; POSITION: absolute; TOP: 8px" cellSpacing="0"
				cellPadding="10" width="100%" align="center">
				<TR>
					<TD align="center">
						<FIELDSET class="FieldSet" style="WIDTH: 450px"><LEGEND><IMG src="../../images/icoFactura.gif" border="0">&nbsp;Modificar 
								Estado</LEGEND><BR>
							<TABLE class="tableBorder" id="Table2" cellSpacing="0" cellPadding="6" width="90%">
								<TR>
									<TD width="20%">Cambiar a Estado</TD>
									<TD width="30%">
										<asp:Label id="lblEstado" runat="server"></asp:Label></TD>
								</TR>
								<TR>
									<TD width="20%">
										<asp:label id="lblMotivo" runat="server" Visible="False">Motivo</asp:label></TD>
									<TD width="30%">
										<asp:dropdownlist id="ddlMotivos" runat="server" Visible="False" Width="176px" CssClass="textBox"
											AutoPostBack="True"></asp:dropdownlist>
										<asp:comparevalidator id="cmvMotivo" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
											ControlToValidate="ddlMotivos" ErrorMessage="Requerido" Operator="NotEqual" ValueToCompare="0"></asp:comparevalidator></TD>
								</TR>
								<TR>
									<TD width="20%">
										<P>
											<asp:Label id="lblAnulacion" runat="server" Visible="False">Observaciones Anulación</asp:Label></P>
									</TD>
									<TD width="50%">&nbsp;
										<asp:DropDownList ID="ddlMotivosAnulacion" runat="server" CssClass="textBox" 
                                            Visible="False" Width="300px">
                                        </asp:DropDownList>
                                        <br />
										<asp:textbox id="txtAnulacion" runat="server" CssClass="textBox" MaxLength="500" Width="300px"
											Height="40px" TextMode="MultiLine" Visible="False"></asp:textbox></TD>
								</TR>
							</TABLE>
							<BR>
						</FIELDSET>
					</TD>
				</TR>
				<TR>
					<TD align="center">
						<asp:button id="btnEstado" runat="server" CssClass="button"></asp:button></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
