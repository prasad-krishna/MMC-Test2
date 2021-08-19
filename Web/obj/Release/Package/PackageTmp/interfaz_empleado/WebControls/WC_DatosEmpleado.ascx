<%@ Control Language="c#" AutoEventWireup="false" Codebehind="WC_DatosEmpleado.ascx.cs" Inherits="TPA.interfaz_empleado.WebControls.WC_DatosEmpleado" %>
<TABLE id="Table1" cellSpacing="0" cellPadding="1" width="99%" align="right">
	<TR>
		<td>
			<FIELDSET class="FieldSet" style="WIDTH: 99%">
				<LEGEND onclick="FieldClick(this);"><IMG alt="" src="../../images/icoExpand.gif"><IMG src="../../images/imgPersona.gif" border="0"> Datos del 
      Empleado&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:label id="lblEstado" CssClass="titleBlue" runat="server"></asp:label>&nbsp;&nbsp;</LEGEND>
				<DIV id="dvEmpleado" style="WIDTH: 98%" align="left" runat="server">
					<TABLE class="tableSmall" id="Table1" cellSpacing="0" cellPadding="1" width="95%">
						<TR>
							<TD width="10%">Identificación</TD>
							<TD align="left" width="20%"><asp:label id="lblIdentificacion" runat="server"></asp:label></TD>
							<TD align="left" width="15%"><asp:label id="lblTituloCodigo" runat="server">Código</asp:label></TD>
							<TD width="20%"><asp:label id="lblCodigo" runat="server"></asp:label></TD>
							<TD align="left" width="15%">Centro de Costo</TD>
							<TD width="20%"><asp:label id="lblCentroCosto" runat="server"></asp:label></TD>
						</TR>
						<TR>
							<TD>Nombre</TD>
							<TD align="left" colSpan="3"><asp:label id="lblNombre" runat="server"></asp:label></TD>
							<TD><asp:label id="lblLabelBase" runat="server" Visible="False">Base</asp:label></TD>
							<TD><asp:label id="lblBase" runat="server"></asp:label></TD>
						</TR>
						<TR>
							<TD>Edad</TD>
							<TD><asp:label id="lblEdad" runat="server"></asp:label></TD>
							<TD align="left">Fecha de Nacimiento</TD>
							<TD><asp:label id="lblFechaNac" runat="server"></asp:label></TD>
							<TD align="left"><asp:label id="lblTituloTelefonos" runat="server">Teléfonos</asp:label></TD>
							<TD><asp:label id="lblTelefono" runat="server"></asp:label><BR>
								<asp:label id="lblCelular" runat="server"></asp:label></TD>
						</TR>
						<TR>
							<TD>Género </TD>
							<TD>
                                <asp:Label ID="lblGenero" runat="server"></asp:Label>
                            </TD>
							<TD align="left">Fecha Ingreso Plan</TD>
							<TD><asp:label id="lblFechaIngreso" runat="server"></asp:label></TD>
							<TD align="left"><asp:label id="lblTituloPlan" runat="server">Plan Adicional&nbsp;Salud</asp:label></TD>
							<TD><asp:label id="lblPlan" runat="server"></asp:label></TD>
						</TR>
						<TR>
							<TD><asp:label id="lblTituloEPS" runat="server">EPS</asp:label> </TD>
							<TD><asp:label id="lblEPS" runat="server"></asp:label></TD>
							<TD align="left"><asp:label id="lblTituloCargo" runat="server">Cargo</asp:label></TD>
							<TD><asp:label id="lblCargo" runat="server"></asp:label></TD>
							<TD align="left"><asp:label id="lblTituloCiudad" runat="server">Ciudad</asp:label></TD>
							<TD><asp:label id="lblCiudad" runat="server"></asp:label></TD>
						</TR>
						<TR>
							<TD><asp:label id="lblTituloEmail" runat="server">Email</asp:label></TD>
							<TD colSpan="3"><asp:label id="lblEmail" runat="server"></asp:label></TD>
							<TD>&nbsp;</TD>
							<TD></TD>
						</TR>
						<TR>
							<TD><asp:label id="Label1" runat="server">Firmó aviso de privacidad</asp:label></TD>
							<TD><asp:label id="lblFirma" runat="server"></asp:label></TD>
							<TD>
                                <asp:Label ID="Label3" runat="server" Text="Fecha de firma"></asp:Label>
                            </TD>
							<TD>
                                <asp:Label ID="lblFechaFirma" runat="server"></asp:Label>
                            </TD>
                            <TD>
                                &nbsp;</TD>
                            <TD>
                                &nbsp;</TD>
						</TR>
						<TR>
							<TD colSpan="6"><asp:datagrid id="dtgPreexistenciasTitual" CssClass="grid" runat="server" PageSize="30" Width="100%"
									CellPadding="1" AutoGenerateColumns="False">
									<AlternatingItemStyle CssClass="norItemsColor"></AlternatingItemStyle>
									<ItemStyle CssClass="norItemsColor"></ItemStyle>
									<HeaderStyle CssClass="headerGrid"></HeaderStyle>
									<Columns>
										<asp:BoundColumn Visible="False" DataField="beneficiario_id"></asp:BoundColumn>
										<asp:BoundColumn DataField="Preexistencias" HeaderText="">
											<ItemStyle Width="100%"></ItemStyle>
										</asp:BoundColumn>
									</Columns>
									<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages"></PagerStyle>
								</asp:datagrid></TD>
						</TR>
						<TR>
							<TD class="textRed" colSpan="6">
								<fieldset id="fldCupo" runat="server"><legend onclick="FieldClick(this);"><IMG alt="" src="../../images/icoExpand.gif">&nbsp;Cupos</legend>&nbsp; 

            <DIV style="DISPLAY: none; WIDTH: 90%" align="left">
										<TABLE id="Table2" cellSpacing="0" cellPadding="1" width="70%">
											<TR>
												<TD class="textRed"><STRONG>Cupo Utilizado</STRONG></TD>
												<TD class="textRed"><STRONG><asp:label id="lblUtilizado" runat="server"></asp:label></STRONG></TD>
												<TD class="textRed"><STRONG></STRONG></TD>
												<TD class="textRed"><STRONG></STRONG></TD>
											</TR>
											<TR>
												<TD class="textRed"><STRONG><asp:label id="Label2" runat="server" Visible="False">Cupo Disponible</asp:label></STRONG></TD>
												<TD class="textRed"><STRONG><asp:label id="lblDisponible" runat="server"></asp:label></STRONG></TD>
												<TD class="textRed"></TD>
												<TD class="textRed"></TD>
											</TR>
											<TR>
												<TD class="textRed"><STRONG>Total de Solicitudes</STRONG></TD>
												<TD class="textRed"><asp:label id="lblCantidadSolicitudes" runat="server" Font-Bold="True"></asp:label></TD>
												<TD class="textRed"></TD>
												<TD class="textRed"></TD>
											</TR>
										</TABLE>
									</DIV></fieldset>
							</TD>
						</TR>
						<TR>
							<TD class="textRed" colSpan="6">
								<FIELDSET id="Fieldset1" runat="server"><LEGEND onclick="FieldClick(this);"><IMG alt="" src="../../images/icoExpand.gif">&nbsp;Preexistencias 
            Grupo Familar</LEGEND>&nbsp; 
            <DIV style="display:none; WIDTH: 95%" align="left"><asp:datagrid id="dtgBeneficiarios" CssClass="grid" runat="server" PageSize="30" Width="100%"
											CellPadding="1" AutoGenerateColumns="False">
											<AlternatingItemStyle CssClass="norItemsColor"></AlternatingItemStyle>
											<ItemStyle CssClass="norItemsColor"></ItemStyle>
											<HeaderStyle CssClass="headerGrid"></HeaderStyle>
											<Columns>
												<asp:BoundColumn DataField="beneficiario_id" Visible="false"></asp:BoundColumn>
												<asp:BoundColumn DataField="identificacion" HeaderText="Identificaci&#243;n">
													<ItemStyle HorizontalAlign="Right" Width="10%"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="nombre" HeaderText="Nombre">
													<ItemStyle Width="30%"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="edad" HeaderText="Edad">
													<ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Parentesco" HeaderText="Parentesco">
													<ItemStyle Width="10%"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="FechaIngresoPlan" HeaderText="Ingreso Plan" DataFormatString="{0:dd/MM/yyyy} ">
													<ItemStyle HorizontalAlign="Center" Width="10%"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Preexistencias" HeaderText="Preexistencias">
													<ItemStyle Width="40%"></ItemStyle>
												</asp:BoundColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages"></PagerStyle>
										</asp:datagrid></DIV></FIELDSET>
							</TD>
						</TR>
                        <tr>
                            <td class="textRed" colSpan="6">
                                <fieldset runat="server">
                                    <LEGEND onclick="FieldClick(this);"><IMG alt="" src="../../images/icoExpand.gif">&nbsp;Información Histórica</LEGEND>
                                    <div style="display:none; width: 95%" align="left">
                                        <asp:GridView ID="gridInformacionHistorica" runat="server" OnRowDataBound="gridInformacionHistorica_RowDataBound" CellPadding="4" CellSpacing="2" EmptyDataText="Sin resultados" CssClass="TableInfHis">
                                            <HeaderStyle CssClass="headerGrid"></HeaderStyle>
                                        </asp:GridView>
                                        <asp:Label ID="noDatos" runat="server" Visible="false"></asp:Label>
                                        </div>
                                </fieldset>
                            </td>
                        </tr>
					</TABLE>
				</DIV>
			</FIELDSET>
		</td>
	</TR>
	<tr>
		<TD>
			<FIELDSET class="FieldSet" id="fldUsuario" style="DISPLAY: none; WIDTH: 99%" runat="server">
				<LEGEND><IMG src="../../images/imgBeneficiario.gif" border="0"> Datos del 
      Beneficiario</LEGEND>
				<TABLE class="tableSmall" id="Table3" cellSpacing="0" cellPadding="1" width="95%">
					<TR>
						<TD width="15%">Identificación</TD>
						<TD align="left" width="20%"><asp:label id="lblIdentificacionUsu" runat="server"></asp:label></TD>
						<TD width="15%">Parentesco</TD>
						<TD width="16%"><asp:label id="lblParentescoUsu" runat="server"></asp:label></TD>
						<TD width="17%"><asp:label id="lblTituloTelefonoUsuario" runat="server">Teléfono</asp:label>
						</TD>
						<TD width="18%"><asp:label id="lblTelefonoUsu" runat="server"></asp:label></TD>
					</TR>
					<TR>
						<TD>Nombre</TD>
						<TD align="left" colSpan="5"><asp:label id="lblNombreUsu" runat="server"></asp:label></TD>
					</TR>
					<TR>
						<TD>Fecha de Nacimiento</TD>
						<TD><asp:label id="lblFechaNacUsu" runat="server"></asp:label></TD>
						<TD>Edad</TD>
						<TD><asp:label id="lblEdadUsu" runat="server"></asp:label></TD>
						<TD>Genero</TD>
						<TD><asp:label id="lblGeneroUsu" runat="server"></asp:label></TD>
					</TR>
					<TR>
						<TD><asp:label id="lblTituloEPSUsuario" runat="server">EPS</asp:label></TD>
						<TD><asp:label id="lblEPSUsu" runat="server"></asp:label></TD>
						<TD><asp:label id="lblTituloFechaIngresoUsuario" runat="server">Fecha Ingreso Plan</asp:label></TD>
						<TD><asp:label id="lblFechaIngresoUsu" runat="server"></asp:label></TD>
						<TD><asp:label id="lblTituloPlanUsuario" runat="server">Plan Adicional de Salud</asp:label></TD>
						<TD><asp:label id="lblPlanUsu" runat="server"></asp:label></TD>
					</TR>
					<tr>
					<td><asp:Label ID="lblTextoFirmoUsuario" runat="server" Text="Aviso de privacidad"></asp:Label></td>
					<td><asp:Label ID="lblFirmoAvisoUsuario" runat="server"></asp:Label></td>
					<td><asp:Label ID="lblTextofechaUsuario" runat="server" Text="Fecha de firma"></asp:Label></td>
					<td><asp:Label ID="lblFechaAvisoUsuario" runat="server"></asp:Label></td>					
					</tr>
					<TR>
						<TD class="textRed" colSpan="6"><asp:datagrid id="dtgPreexistenciasUsuario" CssClass="grid" runat="server" PageSize="30" Width="100%"
								CellPadding="1" AutoGenerateColumns="False">
								<AlternatingItemStyle CssClass="norItemsColor"></AlternatingItemStyle>
								<ItemStyle CssClass="norItemsColor"></ItemStyle>
								<HeaderStyle CssClass="headerGrid"></HeaderStyle>
								<Columns>
									<asp:BoundColumn Visible="False" DataField="beneficiario_id"></asp:BoundColumn>
									<asp:BoundColumn DataField="Preexistencias" HeaderText="">
										<ItemStyle Width="100%"></ItemStyle>
									</asp:BoundColumn>
								</Columns>
								<PagerStyle HorizontalAlign="Left" ForeColor="#000066" BackColor="White" Mode="NumericPages"></PagerStyle>
							</asp:datagrid></TD>
					</TR>
				</TABLE>
				<DIV></DIV>
			</FIELDSET>
		</TD>
	</tr>
</TABLE>
