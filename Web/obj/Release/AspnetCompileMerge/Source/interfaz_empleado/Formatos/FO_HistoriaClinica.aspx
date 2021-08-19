<%@ Register TagPrefix="uc1" TagName="WC_EncabezadoFormato" Src="../WebControls/WC_EncabezadoFormato.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_DiagnosticosFormato" Src="../WebControls/WC_DiagnosticosFormato.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_AnotacionesFijasFormato" Src="../WebControls/WC_AnotacionesFijasFormato.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_PieFormato" Src="../WebControls/WC_PieFormato.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_EncabezadoFormatoOrden" Src="../WebControls/WC_EncabezadoFormatoOrden.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_EncabezadoFormatoIncapacidad" Src="../WebControls/WC_EncabezadoFormatoIncapacidad.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_PieFormatoOrden" Src="../WebControls/WC_PieFormatoOrden.ascx" %>
<%@ Page language="c#" Codebehind="FO_HistoriaClinica.aspx.cs" AutoEventWireup="false" Inherits="TPA.interfaz_empleado.formatos.FO_HistoriaClinica" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>HC-Historias Clínicas</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../css/admon.css" type="text/css" rel="stylesheet">
  </HEAD>
	<BODY bottomMargin="5" leftMargin="1" topMargin="1" rightMargin="5">
		<form id="Form1" method="post" runat="server">
			<TABLE class="tableFormato" id="tblPrincipal" cellSpacing="0" cellPadding="0" width="620"
				border="0" runat="server">
				<TR>
					<TD align="center">
						<TABLE class="tableFormato" id="tblInterior" cellSpacing="0" cellPadding="0" width="620"
							border="0" runat="server">
							<TBODY>
								<TR>
									<TD><uc1:wc_encabezadoformatoincapacidad id="WC_EncabezadoFormatoIncapacidad1" runat="server"></uc1:wc_encabezadoformatoincapacidad></TD>
								</TR>
								<TR>
									<TD align="left"></TD>
								</TR>
								<TR>
									<TD><asp:datalist id="dtlConsultas" runat="server" CellSpacing="0" width="620">
											<ItemTemplate>
												<TABLE class="tableBorderBlack" id="tblBorder" cellSpacing="0" cellPadding="0" width="100%"
													border="0" runat="server">
													<TR>
														<TD>
															<P>
																<TABLE class="tableBorderSmall" id="tblDatosPrincipales" cellSpacing="0" cellPadding="1"
																	width="100%" align="center" runat="server">
																	<TR>
																		<TD>Típo de Consulta</TD>
																		<TD>
																			<asp:TextBox id=txtTipoConsulta runat="server" CssClass="LabelNoModifySmall" Width="200px" text='<%# DataBinder.Eval(Container, "DataItem.NombreTipoConsulta") %>' >
																			</asp:TextBox></TD>
																		<TD>Tipo Enfermedad</TD>
																		<TD>
																			<asp:TextBox id=txtTipoEnfermedad runat="server" CssClass="LabelNoModifySmall" Width="200px" text='<%# DataBinder.Eval(Container, "DataItem.NombreTipoEnfermedad") %>'>
																			</asp:TextBox></TD>
																	</TR>
																	<TR>
																		<TD>Sede</TD>
																		<TD>
																			<asp:TextBox id=txtSede runat="server" CssClass="LabelNoModifySmall" Width="200px" text='<%# DataBinder.Eval(Container, "DataItem.nombreSede") %>' >
																			</asp:TextBox></TD>
																		<TD></TD>
																		<TD>
																			</TD>
																	</TR>
																</TABLE>
																<TABLE class="tableBorderSmall" id="tblDatosConsulta" cellSpacing="0" cellPadding="1" width="100%"
																	align="center" runat="server">
																	<TR>
																		<TD width="20%">Motivo</TD>
																		<TD width="80%">
																			<asp:Label id="txtMotivo" runat="server" Width="460" CssClass="LabelNoModifySmall">
																				<%# DataBinder.Eval(Container, "DataItem.Motivo") %>
																			</asp:Label></TD>
																	</TR>
																	<TR>
																		<TD>Enfermedad Actual</TD>
																		<TD>
																			<asp:Label id="txtEnfermedad" runat="server" Width="460" CssClass="LabelNoModifySmall">
																				<%# DataBinder.Eval(Container, "DataItem.EnfermedadActual") %>
																			</asp:Label></TD>
																	</TR>
																	<TR>
																		<TD>Plan de Tratamiento</TD>
																		<TD>
																			<asp:Label id="txtPlan" runat="server" Width="460" CssClass="LabelNoModifySmall">
																				<%# DataBinder.Eval(Container, "DataItem.PlanTratamiento") %>
																			</asp:Label></TD>
																	</TR>
																	<TR>
																		<TD>Cita control</TD>
																		<TD>
																			<asp:TextBox id=txtCitaControl runat="server" CssClass="LabelNoModifySmall" Width="460" text='<%# string.Format("{0:dd/MM/yyyy}",DataBinder.Eval(Container, "DataItem.CitaControl")) %>' >
																			</asp:TextBox></TD>
																	</TR>
																	<TR>
																		<TD>Observaciones</TD>
																		<TD>
																			<asp:Label id="txtObservaciones" runat="server" Width="460" CssClass="LabelNoModifySmall">
																				<%# DataBinder.Eval(Container, "DataItem.ObservacionesGenerales") %>
																			</asp:Label></TD>
																	</TR>
																	<TR>
																		<TD>Exámenes Laboratorio</TD>
																		<TD>
																			<asp:Label id="txtExamenesLaboratorio" runat="server" Width="460" CssClass="LabelNoModifySmall">
																				<%# DataBinder.Eval(Container, "DataItem.ExamenesLaboratorio") %>
																			</asp:Label></TD>
																	</TR>
																</TABLE>
																<TABLE class="tableBorderSmall" id="tblAntecendentes" cellSpacing="0" cellPadding="1" width="100%"
																	align="center" runat="server">
																	<TR>
																		<TD class="headerTableGrey" colSpan="3">ANTECEDENTES
																		</TD>
																	</TR>
																	<TR>
																		<TD width="20%">Médicos</TD>
																		<TD width="15%">
																			<asp:TextBox id=txtNormalMedicos runat="server" CssClass="LabelNoModifySmall" Width="80px" text='<%# DataBinder.Eval(Container, "DataItem.NormalMedicos") %>' >
																			</asp:TextBox></TD>
																		<TD width="70%">
																			<asp:Label id="txtMedicos" runat="server" Width="380" CssClass="LabelNoModifySmall">
																				<%# DataBinder.Eval(Container, "DataItem.Medicos") %>
																			</asp:Label></TD>
																	</TR>
																	<TR>
																		<TD>
																			<P>Quirúrgicos</P>
																		</TD>
																		<TD>
																			<asp:TextBox id=txtNormalQuirurgicos runat="server" CssClass="LabelNoModifySmall" Width="80px" text='<%# DataBinder.Eval(Container, "DataItem.NormalQuirurgicos") %>' >
																			</asp:TextBox></TD>
																		<TD>
																			<asp:Label id="txtQuirurgicos" runat="server" Width="380" CssClass="LabelNoModifySmall">
																				<%# DataBinder.Eval(Container, "DataItem.Quirurgicos") %>
																			</asp:Label></TD>
																	</TR>
																	<TR>
																		<TD>Gineco-obstétricos</TD>
																		<TD>
																			<asp:TextBox id=txtNormalGineco runat="server" CssClass="LabelNoModifySmall" Width="80px" text='<%# DataBinder.Eval(Container, "DataItem.NormalGinecobstetricos") %>'>
																			</asp:TextBox></TD>
																		<TD>
																			<asp:Label id="txtGineco" runat="server" Width="380" CssClass="LabelNoModifySmall">
																				<%# DataBinder.Eval(Container, "DataItem.Ginecobstetricos") %>
																			</asp:Label></TD>
																	</TR>
																	<TR align="right">
																		<TD colSpan="3" align="right">
																			<TABLE class="tableNoBorderSmall" id="Table4" cellSpacing="0" cellPadding="1" width="620"
																				align="center">
																				<TR>
																					<TD>Menarquia</TD>
																					<TD>&nbsp;
																						<asp:TextBox id=txtMenarquia runat="server" CssClass="LabelNoModifySmall" Width="70px" text='<%# DataBinder.Eval(Container, "DataItem.Menarquia") %>'>
																						</asp:TextBox></TD>
																					<TD align="right">&nbsp;&nbsp;FUM</TD>
																					<TD>&nbsp;
																						<asp:TextBox id=txtFechaUltimaMestruacion runat="server" CssClass="LabelNoModifySmall" Width="70px" text='<%# DataBinder.Eval(Container, "DataItem.FechaUltimaMestruacion") %>'>
																						</asp:TextBox></TD>
																					<TD align="right">&nbsp;&nbsp;G</TD>
																					<TD>&nbsp;
																						<asp:TextBox id=txtGestaciones runat="server" CssClass="LabelNoModifySmall" Width="20px" text='<%# DataBinder.Eval(Container, "DataItem.Gestaciones") %>' >
																						</asp:TextBox></TD>
																					<TD align="right">&nbsp;&nbsp;P</TD>
																					<TD>&nbsp;
																						<asp:TextBox id=txtPartos runat="server" CssClass="LabelNoModifySmall" Width="20px" text='<%# DataBinder.Eval(Container, "DataItem.Partos") %>' >
																						</asp:TextBox></TD>
																					<TD align="right">&nbsp; &nbsp;C</TD>
																					<TD>&nbsp;
																						<asp:TextBox id=txtCesareas runat="server" CssClass="LabelNoModifySmall" Width="20px" text='<%# DataBinder.Eval(Container, "DataItem.Cesareas") %>' >
																						</asp:TextBox></TD>
																					<TD align="right">&nbsp;&nbsp; A</TD>
																					<TD>&nbsp;
																						<asp:TextBox id=txtAbortos runat="server" CssClass="LabelNoModifySmall" Width="20px" text='<%# DataBinder.Eval(Container, "DataItem.Abortos") %>' >
																						</asp:TextBox></TD>
																					<TD align="right">&nbsp; &nbsp; V</TD>
																					<TD>&nbsp;
																						<asp:TextBox id=txtVivos runat="server" CssClass="LabelNoModifySmall" Width="20px" text='<%# DataBinder.Eval(Container, "DataItem.Vivos") %>'>
																						</asp:TextBox></TD>
																				</TR>
																			</TABLE>
																		</TD>
																	</TR>
																	<TR>
																		<TD>Transfusionales</TD>
																		<TD>
																			<asp:TextBox id=txtNormalTransfusionales runat="server" CssClass="LabelNoModifySmall" Width="80px" text='<%# DataBinder.Eval(Container, "DataItem.NormalTransfusionales") %>'>
																			</asp:TextBox></TD>
																		<TD>
																			<asp:Label id="txtTransfusionales" runat="server" Width="380" CssClass="LabelNoModifySmall">
																				<%# DataBinder.Eval(Container, "DataItem.Transfusionales") %>
																			</asp:Label></TD>
																	</TR>
																	<TR>
																		<TD>Tóxico-Alérgicos</TD>
																		<TD>
																			<asp:TextBox id=txtNormalToxicoAlergicos runat="server" CssClass="LabelNoModifySmall" Width="80px" text='<%# DataBinder.Eval(Container, "DataItem.NormalToxicoAlergicos") %>'>
																			</asp:TextBox></TD>
																		<TD>
																			<asp:Label id="txtToxicoAlergicos" runat="server" Width="380" CssClass="LabelNoModifySmall">
																				<%# DataBinder.Eval(Container, "DataItem.ToxicoAlergicos") %>
																			</asp:Label></TD>
																	</TR>
																	<TR>
																		<TD>Farmacologicos</TD>
																		<TD>
																			<asp:TextBox id="txtNormalFarmacologicos" runat="server" CssClass="LabelNoModifySmall" Width="80px" text='<%# DataBinder.Eval(Container, "DataItem.NormalFarmacologicos") %>' >
																			</asp:TextBox><br/>
																			<asp:TextBox id="txtRiesgoCardiovascular" runat="server" CssClass="LabelNoModifySmall" Width="80px" text='<%# DataBinder.Eval(Container, "DataItem.RiesgoCardiovascular") %>'>
																			</asp:TextBox></TD>
																		<TD>
																			<asp:Label id="txtFarmacologicos" runat="server" Width="380" CssClass="LabelNoModifySmall">
																				<%# DataBinder.Eval(Container, "DataItem.Farmacologicos") %>
																			</asp:Label></TD>
																	</TR>
																	<TR>
																		<TD>Otros Antecedentes</TD>
																		<TD>
																			<asp:TextBox id=txtNormalOtrosAntecedentes runat="server" CssClass="LabelNoModifySmall" Width="80px" text='<%# DataBinder.Eval(Container, "DataItem.NormalOtrosAntecedentes") %>' >
																			</asp:TextBox></TD>
																		<TD>
																			<asp:Label id="txtOtrosAntecedentes" runat="server" Width="380" CssClass="LabelNoModifySmall">
																				<%# DataBinder.Eval(Container, "DataItem.OtrosAntecedentes") %>
																			</asp:Label></TD>
																	</TR>
																	<TR>
																		<TD>Familiares</TD>
																		<TD>
																			<asp:TextBox id=txtNormalFamiliares runat="server" CssClass="LabelNoModifySmall" Width="80px" text='<%# DataBinder.Eval(Container, "DataItem.NormalFamiliares") %>' >
																			</asp:TextBox></TD>
																		<TD>
																			<asp:Label id="txtFamiliares" runat="server" Width="380" CssClass="LabelNoModifySmall">
																				<%# DataBinder.Eval(Container, "DataItem.Familiares") %>
																			</asp:Label></TD>
																	</TR>
																</TABLE>
																<TABLE class="tableBorderSmall" id="tblHabitos" style="DISPLAY: none" cellSpacing="0" cellPadding="1"
													                width="100%" align="center" runat="server">
																	<TR>
																		<TD class="headerTableGrey" colSpan="4">HÁBITOS
																		</TD>
																	</TR>
																	<TR>
																		<TD width="20%">Tabaquismo</TD>
																		<TD width="30%">
																			<asp:TextBox id="txtTabaquismo" runat="server" CssClass="LabelNoModifySmall" Width="80px" text='<%# DataBinder.Eval(Container, "DataItem.Tabaquismo") %>' >
																			</asp:TextBox>Frecuencia&nbsp;
																			<asp:textbox id="txtFrecuenciaTabaquismo" runat="server" CssClass="LabelNoModifySmall" text='<%# DataBinder.Eval(Container, "DataItem.FrecuenciaTabaquismo") %>' Width="100px">
																			</asp:textbox></TD>
																		<TD width="20%">Actividad Deportiva</TD>
																		<TD width="30%">
																			<asp:TextBox id="txtActividadDeportiva" runat="server" CssClass="LabelNoModifySmall" Width="80px" text='<%# DataBinder.Eval(Container, "DataItem.ActividadDeportiva") %>' >
																			</asp:TextBox></TD>
																	</TR>
																	<TR>
																		<TD width="20%">Consumo de Alcohol</TD>
																		<TD width="30%">
																			<asp:TextBox id="txtAlcohol" runat="server" CssClass="LabelNoModifySmall" Width="80px" text='<%# DataBinder.Eval(Container, "DataItem.ConsumoAlcohol") %>' >
																			</asp:TextBox>Frecuencia&nbsp;
																			<asp:textbox id="txtFrecuenciaAlcohol" runat="server" CssClass="LabelNoModifySmall" text='<%# DataBinder.Eval(Container, "DataItem.FrecuenciaConsumo") %>' Width="100px">
																			</asp:textbox>
																		</TD>
																		<TD width="20%">Vacunación</TD>
																		<TD width="30%">
																			<asp:Label id="Label2" runat="server" Width="150" CssClass="LabelNoModifySmall">
																				<%# DataBinder.Eval(Container, "DataItem.Vacunacion") %>
																			</asp:Label>
																		</TD>
																	</TR>
																</TABLE>
																<TABLE class="tableBorderSmall" id="tblRevisionSistemas" cellSpacing="0" cellPadding="1"
																	width="100%" align="center" runat="server">
																	<TR>
																		<TD class="headerTableGrey" colSpan="3">REVISIÓN POR SISTEMAS
																		</TD>
																	</TR>
																	<TR>
																		<TD width="20%">Aspecto General</TD>
																		<TD width="15%">
																			<asp:TextBox id=txtNormalAspectoGeneral runat="server" CssClass="LabelNoModifySmall" Width="80px" text='<%# DataBinder.Eval(Container, "DataItem.NormalAspectoGeneral") %>'>
																			</asp:TextBox></TD>
																		<TD width="70%">
																			<asp:Label id="txtAspectoGeneral" runat="server" Width="380" CssClass="LabelNoModifySmall">
																				<%# DataBinder.Eval(Container, "DataItem.AspectoGeneral") %>
																			</asp:Label></TD>
																	</TR>
																	<TR>
																		<TD>
																			<P>Cabeza</P>
																		</TD>
																		<TD>
																			<asp:TextBox id=txtNormalCabeza runat="server" CssClass="LabelNoModifySmall" Width="80px" text='<%# DataBinder.Eval(Container, "DataItem.NormalCabeza") %>' >
																			</asp:TextBox></TD>
																		<TD>
																			<asp:Label id="txtCabeza" runat="server" Width="380" CssClass="LabelNoModifySmall">
																				<%# DataBinder.Eval(Container, "DataItem.Cabeza") %>
																			</asp:Label></TD>
																	</TR>
																	<TR>
																		<TD>Cuello</TD>
																		<TD>
																			<asp:TextBox id=txtNormalCuello runat="server" CssClass="LabelNoModifySmall" Width="80px" text='<%# DataBinder.Eval(Container, "DataItem.NormalCuello") %>' >
																			</asp:TextBox></TD>
																		<TD>
																			<asp:Label id="txtCuello" runat="server" Width="380" CssClass="LabelNoModifySmall">
																				<%# DataBinder.Eval(Container, "DataItem.Cuello") %>
																			</asp:Label></TD>
																	</TR>
																	<TR>
																		<TD>Tórax</TD>
																		<TD>
																			<asp:TextBox id=txtNormalTorax runat="server" CssClass="LabelNoModifySmall" Width="80px" text='<%# DataBinder.Eval(Container, "DataItem.NormalTorax") %>'>
																			</asp:TextBox></TD>
																		<TD>
																			<asp:Label id="txtTorax" runat="server" Width="380" CssClass="LabelNoModifySmall">
																				<%# DataBinder.Eval(Container, "DataItem.Torax") %>
																			</asp:Label></TD>
																	</TR>
																	<TR>
																		<TD>Abdomen</TD>
																		<TD>
																			<asp:TextBox id=txtNormalAbdomen runat="server" CssClass="LabelNoModifySmall" Width="80px" text='<%# DataBinder.Eval(Container, "DataItem.NormalAbdomen") %>'>
																			</asp:TextBox></TD>
																		<TD>
																			<asp:Label id="txtAbdomen" runat="server" Width="380" CssClass="LabelNoModifySmall">
																				<%# DataBinder.Eval(Container, "DataItem.Abdomen") %>
																			</asp:Label></TD>
																	</TR>
																	<TR>
																		<TD>Otros</TD>
																		<TD>
																			<asp:TextBox id=txtNormalOtros runat="server" CssClass="LabelNoModifySmall" Width="80px" text='<%# DataBinder.Eval(Container, "DataItem.NormalOtros") %>'>
																			</asp:TextBox></TD>
																		<TD>
																			<asp:Label id="Label1" runat="server" Width="380" CssClass="LabelNoModifySmall">
																				<%# DataBinder.Eval(Container, "DataItem.Otros") %>
																			</asp:Label></TD>
																	</TR>
																</TABLE>
																<TABLE class="tableBorderSmall" id="tblExamenFisico" cellSpacing="0" cellPadding="1" width="100%"
																	align="center" runat="server">
																	<TR>
																		<TD class="headerTableGrey" colSpan="6">EXAMEN FÍSICO
																		</TD>
																	</TR>
																	<TR>
																		<TD width="15%">Peso</TD>
																		<TD width="15%">
																			<asp:TextBox id=txtPeso runat="server" CssClass="LabelNoModifySmall" Width="50px" text='<%# DataBinder.Eval(Container, "DataItem.Peso") %>' >
																			</asp:TextBox>&nbsp;Kgs</TD>
																		<TD width="11%">Talla</TD>
																		<TD width="15%">
																			<asp:TextBox id=txtTalla runat="server" CssClass="LabelNoModifySmall" Width="50px" text='<%# DataBinder.Eval(Container, "DataItem.Talla") %>'>
																			</asp:TextBox>&nbsp;Mts</TD>
																		<TD width="10%">IMC
																			<BR>
																			<SPAN class="textSmallBlack">(Índice de masa corporal)</SPAN>
																		</TD>
																		<TD width="15%">
																			<asp:TextBox id=txtIMC runat="server" CssClass="LabelNoModifySmall" Width="50px" text='<%# DataBinder.Eval(Container, "DataItem.IndiceMasaCorporal") %>'>
																			</asp:TextBox>&nbsp;Kgs/Mts<SUP>2</SUP></TD>
																	</TR>
																	<TR>
																		<TD>
																			<P>Tensión Arterial</P>
																		</TD>
																		<TD>
																			<asp:TextBox id=txtTension runat="server" CssClass="LabelNoModifySmall" TextMode="MultiLine" Rows="3" Width="150px" text='<%# "Media: " + DataBinder.Eval(Container, "DataItem.TensionArterial") +  (DataBinder.Eval(Container, "DataItem.TensionArterialSistolica") != "" ? " Sistólica:" + DataBinder.Eval(Container, "DataItem.TensionArterialSistolica") : "") +  (DataBinder.Eval(Container, "DataItem.TensionArterialSistolica") != "" ? " Diastólica:" + DataBinder.Eval(Container, "DataItem.TensionArterialDiastolica") : "") %>'>
																			</asp:TextBox></TD>
																		<TD>Frecuencia Cardiaca</TD>
																		<TD>
																			<asp:TextBox id=txtFrecuenciaCardiaca runat="server" CssClass="LabelNoModifySmall" Width="50px" text='<%# DataBinder.Eval(Container, "DataItem.FrecuenciaCardiaca") %>'>
																			</asp:TextBox>&nbsp;x minuto</TD>
																		<TD>Frecuencia Respiratoria</TD>
																		<TD>
																			<asp:TextBox id=txtFrecuenciaRespiratoria runat="server" CssClass="LabelNoModifySmall" Width="50px" text='<%# DataBinder.Eval(Container, "DataItem.FrecuenciaRespiratoria") %>'>
																			</asp:TextBox>&nbsp;x minuto</TD>
																	</TR>
																	<TR>
																		<TD>Temperatura</TD>
																		<TD>
																			<asp:textbox id="txtTemperatura" runat="server" CssClass="LabelNoModifySmall" Width="50px" text='<%# DataBinder.Eval(Container, "DataItem.Temperatura") %>'>
																			</asp:textbox>&nbsp;°C
																		</TD>
																		<TD>Perímetro Abdominal</TD>
																		<TD>
																			<asp:textbox CssClass="LabelNoModifySmall" id="txtPerimetroAbdominal" runat="server" Width="50px" text='<%# DataBinder.Eval(Container, "DataItem.PerimetroAbdominal") %>'>
																			</asp:textbox>&nbsp;cms
																		</TD>
																		<TD></TD>
																		<TD></TD>
																	</TR>
																	<TR>
																		<TD>Aspecto General</TD>
																		<TD>
																			<asp:TextBox id="txtNormalFisGeneral" runat="server" CssClass="LabelNoModifySmall" Width="80px" text='<%# DataBinder.Eval(Container, "DataItem.ExamenNormalAspectoGeneral") %>' >
																			</asp:TextBox></TD>
																		<TD colSpan="4">
																			<asp:Label id="txtFisGeneral" runat="server" Width="380" CssClass="LabelNoModifySmall">
																				<%# DataBinder.Eval(Container, "DataItem.ExamenAspectoGeneral") %>
																			</asp:Label>
																		</TD>
																	</TR>
																	<TR>
																		<TD>Piel y Faneras</TD>
																		<TD>
																			<asp:TextBox id="txtNormalFisPielFanelas" runat="server" CssClass="LabelNoModifySmall" Width="80px" text='<%# DataBinder.Eval(Container, "DataItem.ExamenNormalPielFanelas") %>'>
																			</asp:TextBox></TD>
																		<TD colSpan="4">
																			<asp:Label id="txtFisPielFanelas" runat="server" Width="380" CssClass="LabelNoModifySmall">
																				<%# DataBinder.Eval(Container, "DataItem.ExamenPielFanelas") %>
																			</asp:Label>
																		</TD>
																	</TR>
																	<TR>
																		<TD>
																			<P>Cabeza</P>
																		</TD>
																		<TD>
																			<asp:TextBox id="txtNormalFisCabeza" runat="server" CssClass="LabelNoModifySmall" Width="80px" text='<%# DataBinder.Eval(Container, "DataItem.ExamenNormalCabeza") %>'>
																			</asp:TextBox></TD>
																		<TD colSpan="4">
																			<asp:Label id="txtFisCabeza" runat="server" Width="380" CssClass="LabelNoModifySmall">
																				<%# DataBinder.Eval(Container, "DataItem.ExamenCabeza") %>
																			</asp:Label>
																		</TD>
																	</TR>
																	<TR>
																		<TD>Conjuntiva Ocular</TD>
																		<TD>
																			<asp:TextBox id="txtNormalFisConjuntiva" runat="server" CssClass="LabelNoModifySmall" Width="80px" text='<%# DataBinder.Eval(Container, "DataItem.ExamenNormalConjuntivaOcular") %>'>
																			</asp:TextBox></TD>
																		<TD colSpan="4">
																			<asp:Label id="txtFisConjuntiva" runat="server" Width="380" CssClass="LabelNoModifySmall">
																				<%# DataBinder.Eval(Container, "DataItem.ExamenConjuntivaOcular") %>
																			</asp:Label>
																		</TD>
																	</TR>
																	<TR>
																		<TD>Reflejo Corneal</TD>
																		<TD>
																			<asp:TextBox id="txtNormalFisReflejo" runat="server" CssClass="LabelNoModifySmall" Width="80px" text='<%# DataBinder.Eval(Container, "DataItem.ExamenNormalReflejoCorneal") %>'>
																			</asp:TextBox></TD>
																		<TD colSpan="4">
																			<asp:Label id="txtFisReflejo" runat="server" Width="380" CssClass="LabelNoModifySmall">
																				<%# DataBinder.Eval(Container, "DataItem.ExamenReflejoCorneal") %>
																			</asp:Label>
																		</TD>
																	</TR>
																	<TR>
																		<TD>Pupilas</TD>
																		<TD>
																			<asp:TextBox id="txtNormalFisPupilas" runat="server" CssClass="LabelNoModifySmall" Width="80px" text='<%# DataBinder.Eval(Container, "DataItem.ExamenNormalPupilas") %>' >
																			</asp:TextBox></TD>
																		<TD colSpan="4">
																			<asp:Label id="txtFisPupilas" runat="server" Width="380" CssClass="LabelNoModifySmall">
																				<%# DataBinder.Eval(Container, "DataItem.ExamenPupilas") %>
																			</asp:Label>
																		</TD>
																	</TR>
																	<TR>
																		<TD>Oídos</TD>
																		<TD>
																			<asp:TextBox id="txtNormalFisOidos" runat="server" CssClass="LabelNoModifySmall" Width="80px" text='<%# DataBinder.Eval(Container, "DataItem.ExamenNormalOidos") %>'>
																			</asp:TextBox></TD>
																		<TD colSpan="4">
																			<asp:Label id="txtFisOidos" runat="server" Width="380" CssClass="LabelNoModifySmall">
																				<%# DataBinder.Eval(Container, "DataItem.ExamenOidos") %>
																			</asp:Label>
																		</TD>
																	</TR>
																	<TR>
																		<TD>Otoscopia</TD>
																		<TD>
																			<asp:TextBox id="txtNormalFisOtoscopia" runat="server" CssClass="LabelNoModifySmall" Width="80px" text='<%# DataBinder.Eval(Container, "DataItem.ExamenNormalOtoscopia") %>'>
																			</asp:TextBox></TD>
																		<TD colSpan="4">
																			<asp:Label id="txtFisOtoscopia" runat="server" Width="380" CssClass="LabelNoModifySmall">
																				<%# DataBinder.Eval(Container, "DataItem.ExamenOtoscopia") %>
																			</asp:Label>
																		</TD>
																	</TR>
																	<TR>
																		<TD>Rinoscopia</TD>
																		<TD>
																			<asp:TextBox id="txtNormalFisRinoscopia" runat="server" CssClass="LabelNoModifySmall" Width="80px" text='<%# DataBinder.Eval(Container, "DataItem.ExamenNormalRinoscopia") %>'>
																			</asp:TextBox></TD>
																		<TD colSpan="4">
																			<asp:Label id="txtFisRinoscopia" runat="server" Width="380" CssClass="LabelNoModifySmall">
																				<%# DataBinder.Eval(Container, "DataItem.ExamenRinoscopia") %>
																			</asp:Label>
																		</TD>
																	</TR>
																	<TR>
																		<TD>Boca y Faringe</TD>
																		<TD>
																			<asp:TextBox id="txtNormalFisBocaFaringe" runat="server" CssClass="LabelNoModifySmall" Width="80px" text='<%# DataBinder.Eval(Container, "DataItem.ExamenNormalBocaFaringe") %>'>
																			</asp:TextBox></TD>
																		<TD colSpan="4">
																			<asp:Label id="txtFisBocaFaringe" runat="server" Width="380" CssClass="LabelNoModifySmall">
																				<%# DataBinder.Eval(Container, "DataItem.ExamenBocaFaringe") %>
																			</asp:Label>
																		</TD>
																	</TR>
																	<TR>
																		<TD>Amígdalas</TD>
																		<TD>
																			<asp:TextBox id="txtNormalFisAmigdalas" runat="server" CssClass="LabelNoModifySmall" Width="80px" text='<%# DataBinder.Eval(Container, "DataItem.ExamenNormalAmigdalas") %>'>
																			</asp:TextBox></TD>
																		<TD colSpan="4">
																			<asp:Label id="txtFisAmigdalas" runat="server" Width="380" CssClass="LabelNoModifySmall">
																				<%# DataBinder.Eval(Container, "DataItem.ExamenAmigdalas") %>
																			</asp:Label>
																		</TD>
																	</TR>
																	<TR>
																		<TD>Cuello</TD>
																		<TD>
																			<asp:TextBox id="txtNormalFisCuello" runat="server" CssClass="LabelNoModifySmall" Width="80px" text='<%# DataBinder.Eval(Container, "DataItem.ExamenNormalCuello") %>'>
																			</asp:TextBox></TD>
																		<TD colSpan="4">
																			<asp:Label id="txtFisCuello" runat="server" Width="380" CssClass="LabelNoModifySmall">
																				<%# DataBinder.Eval(Container, "DataItem.ExamenCuello") %>
																			</asp:Label>
																		</TD>
																	</TR>
																	<TR>
																		<TD>Tiroides</TD>
																		<TD>
																			<asp:TextBox id="txtNormalFisTiroides" runat="server" CssClass="LabelNoModifySmall" Width="80px" text='<%# DataBinder.Eval(Container, "DataItem.ExamenNormalTiroides") %>'>
																			</asp:TextBox></TD>
																		<TD colSpan="4">
																			<asp:Label id="txtFisTiroides" runat="server" Width="380" CssClass="LabelNoModifySmall">
																				<%# DataBinder.Eval(Container, "DataItem.ExamenTiroides") %>
																			</asp:Label>
																		</TD>
																	</TR>
																	<TR>
																		<TD>Adenopatías</TD>
																		<TD>
																			<asp:TextBox id="txtNormalFisAdenopatias" runat="server" CssClass="LabelNoModifySmall" Width="80px" text='<%# DataBinder.Eval(Container, "DataItem.ExamenNormalAdenopatias") %>'>
																			</asp:TextBox></TD>
																		<TD colSpan="4">
																			<asp:Label id="txtFisAdenopatias" runat="server" Width="380" CssClass="LabelNoModifySmall">
																				<%# DataBinder.Eval(Container, "DataItem.ExamenAdenopatias") %>
																			</asp:Label>
																		</TD>
																	</TR>
																	<TR>
																		<TD>Tórax</TD>
																		<TD>
																			<asp:TextBox id="txtNormalFisTorax" runat="server" CssClass="LabelNoModifySmall" Width="80px" text='<%# DataBinder.Eval(Container, "DataItem.ExamenNormalTorax") %>'>
																			</asp:TextBox></TD>
																		<TD colSpan="4">
																			<asp:Label id="txtFisTorax" runat="server" Width="380" CssClass="LabelNoModifySmall">
																				<%# DataBinder.Eval(Container, "DataItem.ExamenTorax") %>
																			</asp:Label>
																		</TD>
																	</TR>
																	<TR>
																		<TD>Ruidos Cardiacos</TD>
																		<TD>
																			<asp:TextBox id="txtNormalFisRuidosCardiacos" runat="server" CssClass="LabelNoModifySmall" Width="80px" text='<%# DataBinder.Eval(Container, "DataItem.ExamenNormalRuidosCardiacos") %>'>
																			</asp:TextBox></TD>
																		<TD colSpan="4">
																			<asp:Label id="txtExamenRuidosCardiacos" runat="server" Width="380" CssClass="LabelNoModifySmall">
																				<%# DataBinder.Eval(Container, "DataItem.ExamenRuidosCardiacos") %>
																			</asp:Label>
																		</TD>
																	</TR>
																	<TR>
																		<TD>Ruidos Respiratorios</TD>
																		<TD>
																			<asp:TextBox id="txtNormalFisRuidosRespiratorios" runat="server" CssClass="LabelNoModifySmall" Width="80px" text='<%# DataBinder.Eval(Container, "DataItem.ExamenNormalRuidosRespiratorios") %>'>
																			</asp:TextBox></TD>
																		<TD colSpan="4">
																			<asp:Label id="txtExamenRuidosRespiratorios" runat="server" Width="380" CssClass="LabelNoModifySmall">
																				<%# DataBinder.Eval(Container, "DataItem.ExamenRuidosRespiratorios") %>
																			</asp:Label>
																		</TD>
																	</TR>
																	<TR>
																		<TD>Abdomen</TD>
																		<TD>
																			<asp:TextBox id="txtNormalFisAbdomen" runat="server" CssClass="LabelNoModifySmall" Width="80px" text='<%# DataBinder.Eval(Container, "DataItem.ExamenNormalAbdomen") %>'>
																			</asp:TextBox></TD>
																		<TD colSpan="4">
																			<asp:Label id="txtExamenAbdomen" runat="server" Width="380" CssClass="LabelNoModifySmall">
																				<%# DataBinder.Eval(Container, "DataItem.ExamenAbdomen") %>
																			</asp:Label>
																		</TD>
																	</TR>
																	<TR>
																		<TD>Palpación Abdomen</TD>
																		<TD>
																			<asp:TextBox id="txtNormalFisPalpacionAbdomen" runat="server" CssClass="LabelNoModifySmall" Width="80px" text='<%# DataBinder.Eval(Container, "DataItem.ExamenNormalPalpacionAbdomen") %>'>
																			</asp:TextBox></TD>
																		<TD colSpan="4">
																			<asp:Label id="txtExamenPalpacionAbdomen" runat="server" Width="380" CssClass="LabelNoModifySmall">
																				<%# DataBinder.Eval(Container, "DataItem.ExamenPalpacionAbdomen") %>
																			</asp:Label>
																		</TD>
																	</TR>
																	<TR>
																		<TD>Genitales Externos</TD>
																		<TD>
																			<asp:TextBox id="txtNormalFisGenitales" runat="server" CssClass="LabelNoModifySmall" Width="80px" text='<%# DataBinder.Eval(Container, "DataItem.ExamenNormalGenitalesExternos") %>'>
																			</asp:TextBox></TD>
																		<TD colSpan="4">
																			<asp:Label id="txtExamenGenitalesExternos" runat="server" Width="380" CssClass="LabelNoModifySmall">
																				<%# DataBinder.Eval(Container, "DataItem.ExamenGenitalesExternos") %>
																			</asp:Label>
																		</TD>
																	</TR>
																	<TR>
																		<TD>Hernias</TD>
																		<TD>
																			<asp:TextBox id="txtNormalFisHernias" runat="server" CssClass="LabelNoModifySmall" Width="80px" text='<%# DataBinder.Eval(Container, "DataItem.ExamenNormalHernias") %>'>
																			</asp:TextBox></TD>
																		<TD colSpan="4">
																			<asp:Label id="txtExamenHernias" runat="server" Width="380" CssClass="LabelNoModifySmall">
																				<%# DataBinder.Eval(Container, "DataItem.ExamenHernias") %>
																			</asp:Label>
																		</TD>
																	</TR>
																	<TR>
																		<TD>Columna Vertebral</TD>
																		<TD>
																			<asp:TextBox id="txtNormalFisColumna" runat="server" CssClass="LabelNoModifySmall" Width="80px" text='<%# DataBinder.Eval(Container, "DataItem.ExamenNormalColumnaVertebral") %>'>
																			</asp:TextBox></TD>
																		<TD colSpan="4">
																			<asp:Label id="txtExamenColumnaVertebral" runat="server" Width="380" CssClass="LabelNoModifySmall">
																				<%# DataBinder.Eval(Container, "DataItem.ExamenColumnaVertebral") %>
																			</asp:Label>
																		</TD>
																	</TR>
																	<TR>
																		<TD>Extremidades Superiores</TD>
																		<TD>
																			<asp:TextBox id="txtNormalFisExtremidadesSuperiores" runat="server" CssClass="LabelNoModifySmall" Width="80px" text='<%# DataBinder.Eval(Container, "DataItem.ExamenNormalExtremidadesSuperiores") %>'>
																			</asp:TextBox></TD>
																		<TD colSpan="4">
																			<asp:Label id="txtExamenExtremidadesSuperiores" runat="server" Width="380" CssClass="LabelNoModifySmall">
																				<%# DataBinder.Eval(Container, "DataItem.ExamenExtremidadesSuperiores") %>
																			</asp:Label>
																		</TD>
																	</TR>
																	<TR>
																		<TD>Extremidades Inferiores</TD>
																		<TD>
																			<asp:TextBox id="txtNormalFisExtremidadesInferiores" runat="server" CssClass="LabelNoModifySmall" Width="80px" text='<%# DataBinder.Eval(Container, "DataItem.ExamenNormalExtremidadesInferiores") %>'>
																			</asp:TextBox></TD>
																		<TD colSpan="4">
																			<asp:Label id="txtExamenExtremidadesInferiores" runat="server" Width="380" CssClass="LabelNoModifySmall">
																				<%# DataBinder.Eval(Container, "DataItem.ExamenExtremidadesInferiores") %>
																			</asp:Label>
																		</TD>
																	</TR>
																	<TR>
																		<TD>Várices</TD>
																		<TD>
																			<asp:TextBox id="txtNormalFisVarices" runat="server" CssClass="LabelNoModifySmall" Width="80px" text='<%# DataBinder.Eval(Container, "DataItem.ExamenNormalVarices") %>'>
																			</asp:TextBox></TD>
																		<TD colSpan="4">
																			<asp:Label id="txtyExamenVarices" runat="server" Width="380" CssClass="LabelNoModifySmall">
																				<%# DataBinder.Eval(Container, "DataItem.ExamenVarices") %>
																			</asp:Label>
																		</TD>
																	</TR>
																	<TR>
																		<TD>Neurológico</TD>
																		<TD>
																			<asp:TextBox id="txtNormalFisNeurologico" runat="server" CssClass="LabelNoModifySmall" Width="80px" text='<%# DataBinder.Eval(Container, "DataItem.ExamenNormalNeurologico") %>'>
																			</asp:TextBox></TD>
																		<TD colSpan="4">
																			<asp:Label id="txtExamenNeurologico" runat="server" Width="380" CssClass="LabelNoModifySmall">
																				<%# DataBinder.Eval(Container, "DataItem.ExamenNeurologico") %>
																			</asp:Label>
																		</TD>
																	</TR>
																	<TR>
																		<TD>Otros</TD>
																		<TD>
																			<asp:TextBox id="txtNormalFisOtros" runat="server" CssClass="LabelNoModifySmall" Width="80px" text='<%# DataBinder.Eval(Container, "DataItem.ExamenNormalOtros") %>'>
																			</asp:TextBox></TD>
																		<TD colSpan="4">
																			<asp:Label id="txtExamenOtros" runat="server" Width="380" CssClass="LabelNoModifySmall">
																				<%# DataBinder.Eval(Container, "DataItem.ExamenOtros") %>
																			</asp:Label>
																		</TD>
																	</TR>
																	<tr>
																		<td>Comentarios Examen Físico</td>
																		<td colSpan="5">
																			<asp:Label id="txtComentarioFisico" runat="server" Width="500" CssClass="LabelNoModifySmall">
																				<%# DataBinder.Eval(Container, "DataItem.ComentariosExamenFisico") %>
																			</asp:Label></td>
																	</tr>
																	
																</TABLE>
																<TABLE class="tableBorderSmall" id="tblPruebasBiometricas" style="DISPLAY: none" cellSpacing="0" cellPadding="1"
													width="100%" align="center" runat="server">
													               
																	<tr>
													                     <td class="headerTable" colSpan="6">PRUEBAS BIOMÉTRICAS
																		</td>
																	</TR>
																	
																	<tr>
												    
												                        <td width="20%">Colesterol total</td>
												                        <td width="19%"><asp:textbox id="txtColesterolTotal"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.ColesterolTotal") %>' CssClass="LabelNoModifySmall" Width="60px" MaxLength="3"></asp:textbox>&nbsp;mg/dl</td>
												                        <td width="15%">Colesterol HDL</td>
												                        <td width="18%"><asp:textbox id="txtColesterolHDL"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.ColesterolHDL") %>' CssClass="LabelNoModifySmall" Width="60px" MaxLength="2"></asp:textbox>&nbsp;mg/dl
												                            <br />
												                            <asp:textbox id="txtColesterolHDLmmol"  text='<%# DataBinder.Eval(Container, "DataItem.ColesterolHDLmmol") %>' CssClass="LabelNoModifySmall" runat="server" Width="60px" MaxLength="2"></asp:textbox>&nbsp;mmol/L
												                        </td>
												                        <td width="13%">Colesterol LDL</td>
												                        <td width="16%"><asp:textbox id="txtColesterolLDL" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.ColesterolLDL") %>' CssClass="LabelNoModifySmall" Width="60px" MaxLength="3"></asp:textbox>&nbsp;mg/dl</td>
											                        </tr>
											                        <tr>
													                    <td>Triglicéridos</td>
													                    <td><asp:textbox id="txtTrigliceridos" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Trigliceridos") %>'  CssClass="LabelNoModifySmall" Width="60px" MaxLength="3"></asp:textbox>&nbsp;mg/dl</td>
													                    <td>Índice Aterogénico</td>
													                    <td width="19%"><asp:textbox id="txtIndiceAterogenico" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.IndiceAterogenico") %>' CssClass="LabelNoModifySmall" Width="60px" MaxLength="4"></asp:textbox></td>
													                    <td>Antígeno Específico de Próstata</td>
													                    <td><asp:textbox id="txtAntigenoProstata" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.AntigenoProstata") %>'  CssClass="LabelNoModifySmall" Width="60px"></asp:textbox>&nbsp;mg/dl</td>
												                    </tr>
												                    <tr>
													                    <td>Glucemia en Ayunas</td>
													                    <td><asp:textbox id="txtGlucemiaAyunas" runat="server"  text='<%# DataBinder.Eval(Container, "DataItem.GlucemiaAyunas") %>' CssClass="LabelNoModifySmall" Width="60px"></asp:textbox>&nbsp;mg/dl</td>
													                    <td>Hemoglobina Glucosilada</td>
													                    <td colspan="1"><asp:textbox  id="txtHemoglobinaGlucosilada" runat="server"  text='<%# DataBinder.Eval(Container, "DataItem.HemoglobinaGlucosilada") %>' CssClass="LabelNoModifySmall" Width="60px"></asp:textbox>&nbsp;%</td>
													                    <td>Homocisteína</td>
													                    <td><asp:textbox id="txtHomocisteina" runat="server"  text='<%# DataBinder.Eval(Container, "DataItem.Homocisteina") %>' CssClass="LabelNoModifySmall" Width="50px"></asp:textbox>&nbsp;micromol/L</td>
												                    </tr>
																	<TR>
																		 <td width="20%">Exámenes Laboratorio</TD>
																		<td colspan="5">
																			<asp:Label id="lblExamenesLaboratorio" runat="server" Width="460" CssClass="LabelNoModifySmall">
																				<%# DataBinder.Eval(Container, "DataItem.ExamenesLaboratorio") %>
																			</asp:Label></TD>
																	</TR>
																	<tr>
												    
													                    <td width="20%" valign="top">Papanicolau Microbiológico</td>
													                    <td colspan="3" style="width: 35%"><asp:textbox id="txtPresenciaMicroorganismos" runat="server"  text='<%# DataBinder.Eval(Container, "DataItem.PresenciaMicroorganismos") %>' CssClass="LabelNoModifySmall" Width="250px"></asp:textbox></td>
													                    <td width="12%" >Fecha</td>
													                    <td width="19%" ><asp:textbox id="txtFechaPapanicolauMicro" runat="server"  text='<%# string.Format("{0:dd/MM/yyyy}",DataBinder.Eval(Container, "DataItem.FechaPapanicolauMicro")) %>' CssClass="LabelNoModifySmall" Width="75px"></asp:textbox></td>
												                    </tr>
												                    <tr>
												    
													                    <td width="20%" valign="top">&nbsp;</td>
													                    <td width="19%" colspan="5">Observaciones&nbsp;<asp:textbox id="txtObservacionesPresenciaMicro" runat="server"  text='<%# DataBinder.Eval(Container, "DataItem.ObservacionesPresenciaMicro") %>' CssClass="LabelNoModifySmall" Width="420px" MaxLength="300" ></asp:textbox></td>
												                    </tr>
												                    <tr>
												                        <td width="20%" valign="top">Papanicolau Morfológico</td>
													                    <td width="19%" colspan="5" style="width: 30%"><asp:textbox id="txtResultadoMorfologico" runat="server"  text='<%# DataBinder.Eval(Container, "DataItem.ResultadoMorfologico") %>' CssClass="LabelNoModifySmall" Width="400px"></asp:textbox></td>
                    												</tr>
                    												
												                    <tr>
                    												    
													                    <td width="80%" colspan ="6">
                                                                            <asp:Label ID="lblAnormalCelulasEpi" runat="server" CssClass="LabelNoModifySmall">Anormalidades en células epiteliales</asp:Label>
                                                                            <br /><asp:textbox id="txtAnormalCelulasEpi" runat="server"  text='<%# DataBinder.Eval(Container, "DataItem.AnormalidadCelulasEpiteliales") %>' CssClass="LabelNoModifySmall" Width="400px"></asp:textbox></td>
                    													
												                    </tr>
                    												
												                    <tr>
                    												    
													               
													                    <td width="80%" colspan ="6"> <asp:Label ID="lblCelulasEscamosas" runat="server" CssClass="LabelNoModifySmall">ASC (Células escamosas atípicas)</asp:Label><br />
													                    <asp:textbox id="txtCelulasEscamosas" runat="server"  text='<%# DataBinder.Eval(Container, "DataItem.CelulasEscamosasAtipicas") %>' CssClass="LabelNoModifySmall" Width="600px"></asp:textbox></td>
                    													
												                    </tr>
												                    <tr>
												                        <td width="20%">Mamografía</td>
													                    <td width="39%" colspan ="6"><asp:textbox id="txtMamografia" runat="server"  text='<%# DataBinder.Eval(Container, "DataItem.Mamografia") %>' CssClass="LabelNoModifySmall" Width="450px"></asp:textbox></td>
                    													
												                    
												                    </tr>
												                    <tr>
												                        <td width="20%">Observaciones Mamografía</td>
                    													<td width="11%" colspan="6"><asp:TextBox ID="txtMamografiaObservaciones" runat="server"  text='<%# DataBinder.Eval(Container, "DataItem.MamografiaObservaciones") %>'  CssClass="LabelNoModifySmall" MaxLength="300" Width="530px"></asp:TextBox></td>
												                    
												                    </tr>
												                    <tr>
												                        <td width="20%" valign="top">Audiometría</td>
													                    <td width="19%"><asp:textbox id="txtAudiometria" runat="server"  text='<%# DataBinder.Eval(Container, "DataItem.Audiometria") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:textbox></td>
                                                                        <td width="20%">Observaciones</td>
                                                                        <td width="11%" colspan="3"><asp:TextBox ID="txtAudiometriaObservaciones" runat="server"  text='<%# DataBinder.Eval(Container, "DataItem.AudiometriaObservaciones") %>'  CssClass="LabelNoModifySmall" MaxLength="300" Width="300px"></asp:TextBox></td>
												                    </tr>
                    												
												                    <tr>
                    												    
													                    <td width="20%" valign="top">Rayos X (de tórax)</td>
													                    <td width="19%"><asp:textbox id="txtRayosX" runat="server"  text='<%# DataBinder.Eval(Container, "DataItem.RayosX") %>'  CssClass="LabelNoModifySmall" Width="70px"></asp:textbox></td>
                                                                        <td width="20%">Observaciones</td>
                                                                        <td width="11%" colspan="3"><asp:TextBox ID="txtRayosXObservaciones" runat="server"  text='<%# DataBinder.Eval(Container, "DataItem.RayosXObservaciones") %>'  CssClass="LabelNoModifySmall" MaxLength="300" Width="300px"></asp:TextBox></td>
												                    </tr>
												                    <tr>
												    
													                    <td width="20%">Examen Visual</td>
													                    <td width="15%"><asp:textbox id="txtMiopia" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Miopia") %>' CssClass="LabelNoModifySmall" Width="10px"></asp:textbox> Miopía</td>
													                    <td width="8%">Valor O.D. <br /><asp:textbox id="txtMiopiaValor" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.MiopiaValor") %>' CssClass="LabelNoModifySmall" Width="60px"></asp:textbox></td>
													                    <td width="15%">Valor O.I. <br /><asp:textbox id="txtMiopiaValorOI" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.MiopiaValorOI") %>' CssClass="LabelNoModifySmall" Width="60px"></asp:textbox></td>
													                    <td width="10%">Observaciones</td>
													                    <td width="19%"><asp:TextBox ID="txtMiopiaObservaciones" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.MiopiaObservaciones") %>' CssClass="LabelNoModifySmall" MaxLength="300" Width="100px"></asp:TextBox></td>
												                    </tr>
												                    <tr>
                    												    
													                    <td>&nbsp;</td>
													                    <td><asp:textbox id="txtAstigmatismo"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Astigmatismo") %>' CssClass="LabelNoModifySmall" Width="10px"></asp:textbox> Astigmatismo</td>
													                    <td>Valor O.D. <br /><asp:textbox id="txtAstigmatismoValor"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.AstigmatismoValor") %>' CssClass="LabelNoModifySmall" Width="60px"></asp:textbox></td>
													                    <td >Valor O.I. <br /><asp:textbox id="txtAstigmatismoValorOI"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.AstigmatismoValorOI") %>' CssClass="LabelNoModifySmall" Width="60px"></asp:textbox></td>
													                    <td>Observaciones</td>
													                    <td><asp:TextBox ID="txtAstigmatismoObservaciones" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.AstigmatismoObservaciones") %>' CssClass="LabelNoModifySmall" MaxLength="300" Width="100px"></asp:TextBox></td>
												                    </tr>
												                    <tr>
                    												    
													                    <td >&nbsp;</td>
													                    <td><asp:textbox id="txtHipermetropia" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Hipermetropia") %>' CssClass="LabelNoModifySmall" Width="10px"></asp:textbox> Hipermetropia</td>
													                    <td>Valor O.D. <br /><asp:textbox id="txtHipermetropiaValor" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.HipermetropiaValor") %>' CssClass="LabelNoModifySmall" Width="60px"></asp:textbox></td>
													                    <td>Valor O.I. <br /><asp:textbox id="txtHipermetropiaValorOI" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.HipermetropiaValorOI") %>' CssClass="LabelNoModifySmall" Width="60px"></asp:textbox></td>
													                    <td>Observaciones</td>
													                    <td> <asp:TextBox ID="txtHipermetropiaObservaciones" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.HipermetropiaObservaciones") %>'  CssClass="LabelNoModifySmall" MaxLength="300" Width="100px"></asp:TextBox></td>
												                    </tr>
												                    <tr>
                    												    
													                    <td>&nbsp;</td>
													                    <td><asp:textbox id="txtPresbicia" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Presbicia") %>' CssClass="LabelNoModifySmall" Width="10px"></asp:textbox> Presbicia</td>
													                    <td>Valor O.D. <br /><asp:textbox id="txtPresbiciaValor" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.PresbiciaValor") %>' CssClass="LabelNoModifySmall" Width="60px"></asp:textbox></td>
													                    <td>Valor O.I. <br /><asp:textbox id="txtPresbiciaValorOI" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.PresbiciaValorOI") %>' CssClass="LabelNoModifySmall" Width="60px"></asp:textbox></td>
													                    <td>Observaciones</td>
													                    <td><asp:TextBox ID="txtPresbiciaObservaciones" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.PresbiciaObservaciones") %>' CssClass="LabelNoModifySmall" MaxLength="300" Width="100px"></asp:TextBox></td>
												                    </tr>
												                    <tr>
                    												    
													                    <td>&nbsp;</td>
													                    <td><asp:textbox id="txtOtrosExamenVisual" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.OtrosExamenVisual") %>' CssClass="LabelNoModifySmall" Width="10px"></asp:textbox> Otros</td>
													                    <td>Diagnóstico</td>
													                    <td colspan="3"><asp:TextBox ID="txtOtrosDiagnostico" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.IdDiagnosticoExamenVisual") %>' CssClass="LabelNoModifySmall" MaxLength="300" Width="320px"></asp:TextBox> </td>
                    													
												                    </tr>
																</TABLE>
																<TABLE class="tableBorderSmall" id="tblWellness" style="DISPLAY: none" cellSpacing="0" cellPadding="1"
													                        width="100%" align="center" runat="server">
												                        <tr>
													                        <TD class="headerTable" colSpan="4">WELLNESS</td>
												                        </tr>
												                        <tr>
													                        <td colspan="4">¿Está actualmente afiliado al programa de wellness?<br />
													                        <asp:textbox id="txtWellness"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.ProgramaWellness") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:textbox>
															                </td>
												                        </tr>
												                        <tr>
													                        <td colspan="4">¿Tiene firmado el acuerdo para participar en el programa?<br />
													                            <asp:textbox id="txtFirmaWellness"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.FirmaWellness") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:textbox>
															                </td>	
												                        </tr>
												                    
											                        </TABLE>
																<TABLE class="tableBorderSmall" id="tblHabitoFumar" style="DISPLAY: none" cellSpacing="0" cellPadding="1"
													                        width="100%" align="center" runat="server">
													                    <tr>
													                        <TD class="headerTable"  colspan="6"> HÁBITO DE FUMAR</td>
												                        </tr>
                        												
												                        <tr>
													                        <td colspan="6">¿Cuál de las siguientes respuestas describe mejor su conducta frente a el cigarrillo?<br />
													                        <asp:textbox id="txtConductaCigarrillo"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.ConductaCigarrillo") %>' CssClass="LabelNoModifySmall" Width="300px"></asp:textbox>
													                        <asp:textbox id="txtIdConductaCigarrillo"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.IdConductaCigarrillo") %>' CssClass="LabelNoModifySmall" Width="300px" Visible="false"></asp:textbox>
													                        
													                        </td>
												                        </tr>
												                       <tr id="trTiempoPrimerCigarrillo" runat="server" visible="false">
													                        <td colspan="6">¿Cuánto tiempo transcurre desde que se levanta hasta encender el primer cigarrillo?<br />
													                        <asp:textbox id="txtTiempoPrimerCigarrillo"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.TiempoPrimerCigarrillo") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:textbox>
													                        </td>
												                        </tr>
                        												
												                        <tr id="trDificultadFumar" runat="server" visible="false">
													                        <td colspan="6">¿Tiene dificultades para no fumar en lugares donde está prohibido (Ej. en la iglesia, en la biblioteca, en el cine)?<br />
													                        <asp:textbox id="txtDificultadFumar"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.DificultadFumar") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:textbox>
													                        </td>
												                        </tr>
												                        <tr id="trCigarrilloSuprimir" runat="server" visible="false">
													                        <td colspan="6">¿Qué cigarrillo le costaría más suprimir?<br />
													                             <asp:textbox id="txtCigarrilloSuprimir"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.CigarrilloSuprimir") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:textbox>
													                        </td>
												                        </tr>
												                        <tr id="trCigarrillosalDia" runat="server" visible="false">
													                        <td colspan="6">¿Cuántos cigarrillos fuma al día?<br />
                        													    <asp:textbox id="txtCigarrillosalDia"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.CigarrillosalDia") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:textbox>
													                        </td>
												                        </tr>
												                        <tr id="trFrecuenciaPrimerasHorasDia" runat="server" visible="false">
													                        <td colspan="6">¿Fuma más frecuentemente durante las primeras horas del día que durante el resto del día?<br />	
													                            <asp:textbox id="txtFrecuenciaPrimerasHorasDia"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.FrecuenciaPrimerasHorasDia") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:textbox>
													                        </td>
												                        </tr>
												                        <tr id="trFumaEnfermedad" runat="server" visible="false">
													                        <td colspan="6">¿Fuma cuándo debe guardar cama por una enfermedad la mayor parte del día?<br />
													                            <asp:textbox id="txtFumaEnfermedad"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.FumaEnfermedad") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:textbox>
													                        </td>
												                        </tr>
												                        <tr id="trCategoriaCigarrillos" runat="server" visible="false">
													                        <td colspan="6">¿Dentro de qué categoría entran la mayoría de cigarrillos que usted fuma?<br />
													                            <asp:textbox id="txtCategoriaCigarrillos"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.CategoriaCigarrillos") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:textbox>
													                        </td>
											                            </tr>
												                        <tr id="trAspiraHumo" runat="server" visible="false">
													                        <td colspan="6">¿Aspira el humo cuando fuma?<br />	
													                            <asp:textbox id="txtAspiraHumo"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.AspiraHumo") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:textbox>
													                        </td>		
												                        </tr>
												                        <tr id="trAnosDejoFumar" runat="server" visible="false">
													                        <td colspan="6">¿Cuántos años hace que dejó de fumar?<br />
													                            <asp:textbox id="txtAnosDejoFumar"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.AnosDejoFumar") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:textbox>
													                        </td>
												                        </tr>
												                        <tr id="trPromedioDiarioX2Anos" runat="server" visible="false">
													                        <td colspan="6">¿Cual era el promedio diario de cigarrillos que fumaba durante los dos años previos de dejar el hábito?<br />
                        													    <asp:textbox id="txtPromedioDiarioX2Anos"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.PromedioDiarioX2Anos") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:textbox>
													                        </td>
												                        </tr>
    												                    
											                        </table>
											                        
											                        <TABLE class="tableBorderSmall" id="tblConsumoAlcohol" style="DISPLAY: none" cellSpacing="0" cellPadding="1"
													                        width="100%" align="center" runat="server">
													                        <tr>
													                            <TD class="headerTable" colSpan="6">CONSUMO DE ALCOHOL</td>
												                            </tr>
												                            <tr>
													                            <td colspan="6">Alcohol, Copas a la semana (rangos de 1-5)<br />
													                                <asp:textbox id="txtCopasSemana"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.CopasSemana") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:textbox>
													                            </td>
												                            </tr>
												                            <tr>
													                            <td colspan="6">¿Alguna vez han criticado su consumo de alcohol?<br />
													                                 <asp:textbox id="txtCriticaAlcohol"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.CriticaAlcohol") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:textbox>
													                            </td>
												                            </tr>
												                            <tr>
													                            <td colspan="6">¿Alguna vez se ha arrepentido de la cantidad de alcohol que consumió?<br />
													                                <asp:textbox id="txtArrepentidoAlcohol"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.ArrepentidoAlcohol") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:textbox>
													                             </td>
												                            </tr>
												                            <tr>
													                            <td colspan="6">¿Ha tenido lagunas por el consumo de alcohol?<br />
													                                <asp:textbox id="txtLagunaAlcohol"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.LagunaAlcohol") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:textbox>
													                            </td>
												                            </tr>
												                            <tr>
													                            <td colspan="6">¿Alguna vez lo primero que ha consumido en la mañana ha sido una copa de alcohol?<br />
													                                <asp:textbox id="txtMananaAlcohol"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.MananaAlcohol") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:textbox>
													                            </td>
                            												</tr>
											                            </table>
											                            <TABLE class="tableBorderSmall" id="tblVacunacion" style="DISPLAY: none" cellSpacing="0" cellPadding="1"
													                        width="100%" align="center" runat="server">
											                               <tr>
													                            <TD class="headerTable" colSpan="6">VACUNACIÓN&nbsp;</td>
												                            </tr>
												                            <tr>
													                            <td colspan="6">¿Se ha aplicado la vacuna contra Influenza Estacional en el último año? <br />
                            													    <asp:textbox id="txtInfluenciaEstacional"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.InfluenciaEstacional") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:textbox>
													                            &nbsp;&nbsp;&nbsp;Fecha <asp:textbox id="txtFechaInfluenciaEstacional"  runat="server" text='<%# string.Format("{0:dd/MM/yyyy}",DataBinder.Eval(Container, "DataItem.FechaInfluenzaEstacional")) %>' CssClass="LabelNoModifySmall" Width="200px"></asp:textbox>
													                            </td>
												                            </tr>
												                            <tr>
												                                <td colspan="6">¿Se ha aplicado la vacuna contra Influenza H1N1 en el último año?<br />
												                                    <asp:textbox id="txtInfluenciaH1N1"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.InfluenciaH1N1") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:textbox>
													                            &nbsp;&nbsp;&nbsp;Fecha <asp:textbox id="txtFechaInfluenciaH1N1"  runat="server" text='<%# string.Format("{0:dd/MM/yyyy}",DataBinder.Eval(Container, "DataItem.FechaInfluenciaH1N1")) %>' CssClass="LabelNoModifySmall" Width="200px"></asp:textbox>
													                            </td>
										                                     </tr>
												                            <tr>
													                            <td colspan="6">¿Se ha aplicado la vacuna contra Fiebre Amarilla? <br />
													                                <asp:textbox id="txtFiebreAmarilla"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.FiebreAmarilla") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:textbox>
													                            &nbsp;&nbsp;&nbsp;Fecha <asp:textbox id="txtFechaFiebreAmarilla"  runat="server" text='<%# string.Format("{0:dd/MM/yyyy}",DataBinder.Eval(Container, "DataItem.FechaFiebreAmarilla")) %>' CssClass="LabelNoModifySmall" Width="200px"></asp:textbox>
													                            </td>
													                        </tr>   
													                        <tr>
													                            <td colspan="6">¿Se ha aplicado la vacuna contra Hepatitis Viral? <br />
													                                <asp:textbox id="txtHepatitisViral"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.HepatitisViral") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:textbox>
													                            &nbsp;&nbsp;&nbsp;Fecha <asp:textbox id="txtFechaHepatitisViral"  runat="server" text='<%# string.Format("{0:dd/MM/yyyy}",DataBinder.Eval(Container, "DataItem.FechaHepatitisViral")) %>' CssClass="LabelNoModifySmall" Width="200px"></asp:textbox>
													                            </td>
												                            </tr>
												                            <tr>
													                            <td colspan="6">¿Se ha aplicado la vacuna contra el Tétanos? <br />
													                                 <asp:textbox id="txtToxoideTetanico"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.ToxoideTetanico") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:textbox>
													                                &nbsp;&nbsp;&nbsp;Fecha <asp:textbox id="txtFechaToxoideTetanico"  runat="server" text='<%# string.Format("{0:dd/MM/yyyy}",DataBinder.Eval(Container, "DataItem.FechaToxoideTetanico")) %>' CssClass="LabelNoModifySmall" Width="200px"></asp:textbox>
													                            </td>
												                            </tr>
												                        </table>
												                        <table class="tableBorderSmall" id="tblSedentarismo" style="DISPLAY: none" cellSpacing="0" cellPadding="1"
													                        width="100%" align="center" runat="server">
											                                <tr>
													                            <TD class="headerTable" colSpan="6">SEDENTARISMO&nbsp;</TD>
														                    </tr>
												                            <tr>
													                            <td colspan="6">¿Practicas de manera constante deporte en los últimos 6 meses?<br />
													                                <asp:textbox id="txtPracticaDeporte"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.PracticaDeporte") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:textbox>
													                              </td>
												                            </tr>
												                            <tr>
													                            <td colspan="6">¿Cuántas veces practicas deporte o actividad física en la semana?<br />
													                                <asp:textbox id="txtPracticaDeporteSemana"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.PracticaDeporteSemana") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:textbox>
													                             </td>
												                            </tr>
												                            <tr>
													                            <td colspan="6">Promedio de tiempo en minutos en cada sesión:<br />	
                            													    <asp:textbox id="txtPromedioTiempoMinutos"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.PromedioTiempoMinutos") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:textbox>
													                             </td>
												                            </tr>
												                             <tr>
													                            <td colspan="6">¿Qué tipo de actividad física realizas?<br />
													                                <asp:textbox id="txtTipoActividadFisica"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.TipoActividadFisica") %>' CssClass="LabelNoModifySmall" Width="600px"></asp:textbox>
													                              </td>
												                            </tr>
												                            <tr>
													                            <td colspan="6">¿Cuántas horas ves diarias en promedio de televisión?<br />
													                                <asp:textbox id="txtHorasDiariasTV"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.HorasDiariasTV") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:textbox>
													                             </td>
												                            </tr>
											                            </table>
											                            <table class="tableBorderSmall" id="tblSaludOral" style="DISPLAY: none" cellSpacing="0" cellPadding="1"
													                        width="100%" align="center" runat="server">
											                                <tr>
													                            <TD class="headerTable" colSpan="6">SALUD ORAL </TD>
														                    </tr>
												                            <tr>
													                            <td colspan="6">¿Ha asistido a consulta odontológica en el último año?<br />
													                                 <asp:textbox id="txtConsultaOdontologica"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.ConsultaOdontologica") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:textbox>
													                            </td>
												                            </tr>
												                            <tr>
													                            <td colspan="6">¿Cuántas veces se lava los dientes al día?<br />
													                                <asp:textbox id="txtLavaDientes"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.LavaDientes") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:textbox>
													                            </td>
												                            </tr>
												                            <tr>
													                            <td colspan="6">¿Además de lavarse los dientes, usa hilo dental todos los días?<br />
													                                <asp:textbox id="txtSedaDental"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.SedaDental") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:textbox>
													                            </td>
                            														
												                            </tr>
												                       </table>
												                       <table class="tableBorderSmall" id="tblEstres" style="DISPLAY: none" cellSpacing="0" cellPadding="1"
													                        width="100%" align="center" runat="server">
													                        <tr>
													                            <TD class="headerTable" colSpan="4">ESTRÉS&nbsp;</td>
												                            </tr>
												                            <tr>
													                            <td colspan="4">En los últimos meses, ¿te has sentido decaído (a), deprimido (a) o estresado (a) de manera persistente?<br />
													                                 <asp:textbox id="txtSentidoDecaido"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.SentidoDecaido") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:textbox>
													                            </td>
												                            </tr>
												                            <tr>
													                            <td colspan="4">En los dos últimos meses, ¿has tenido poco interés o placer al hacer las cosas?<br />
														                             <asp:textbox id="txtInteresPlacer"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.InteresPlacer") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:textbox>
													                            </td>
												                            </tr>
												                            <tr>
													                            <td colspan="4">¿Cómo clasificarías tu nivel de estrés?<br />
														                            <asp:textbox id="txtNivelEstres"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.NivelEstres") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:textbox>
													                            </td>
												                            </tr>
												                            <tr>
													                            <td colspan="4">A continuación se muestra una lista de las fuentes típicas de estrés. Marque todas las que usted sienta sean una sobrecarga para usted: <br />
													                                <asp:BulletedList ID="btlFuentesEstres" DataTextField="Descripcion" runat="server">
                                                                                            
                                                                                    </asp:BulletedList>
                                                                                </td>
                                                                            </tr>
												                            <tr>
													                            <td colspan="4">
                                                                                    <br />
                                                                                    Selecciona el enunciado que mejor describa tus planes para controlar tu estrés<br />
                            													    <asp:textbox id="txtControlarEstres"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.ControlarEstres") %>' CssClass="LabelNoModifySmall" Width="600px"></asp:textbox>
													                            </td>
                            													
												                            </tr>
											                            </table>
											                            <table class="tableBorderSmall" id="tblEmocional" style="DISPLAY: none" cellSpacing="0" cellPadding="1"
													                        width="100%" align="center" runat="server">
													                        <tr>
													                            <TD class="headerTable" colSpan="6">EMOCIONAL&nbsp;</td>
												                            </tr>
                            												
                            												
												                            <tr>
													                            <td colspan="6">¿En el último mes cómo calificarías la calidad general de tu sueño?<br />
                            													    <asp:textbox id="txtCalificacionSueno"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.CalificacionSueno") %>' CssClass="LabelNoModifySmall" Width="600px"></asp:textbox>
													                            </td>
												                            </tr>
                            												
												                            <tr>
													                            <td colspan="6">Después de una noche habitual de sueño ¿te sientes cansado(a) o fatigado(a)  al levantarte?<br />
													                                <asp:textbox id="txtEstadoLevantarse"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.EstadoLevantarse") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:textbox>
													                            </td>
												                            </tr>
												                            <tr>
													                            <td colspan="6">¿Cuantas horas duerme regularmente?<br />
													                                    <asp:textbox id="txtHorasDuermeRegular"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.HorasDuermeRegular") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:textbox>
													                            </td>
												                            </tr>
												                            <tr>
													                            <td colspan="6">¿Cómo califica su estado de ánimo emocional?<br />
													                                <asp:textbox id="txtEstadoAnimoEmocional"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.EstadoAnimoEmocional") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:textbox>
													                            </td>
                            												
												                            </tr>
											                            </table>
											                            <table class="tableBorderSmall" id="tblAccidentalidad" style="DISPLAY: none" cellSpacing="0" cellPadding="1"
													                        width="100%" align="center" runat="server">
													                        <tr>
											                                    <TD class="headerTable" colSpan="6">COMPORTAMIENTOS DE RIESGO Y ACCIDENTALIDAD&nbsp;</td>
										                                    </tr>
                            													
										                                    <tr>
											                                    <td colspan="6">¿Utiliza el Cinturón de seguridad? <br />
											                                        <asp:textbox id="txtCinturonSeguridad"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.CinturonSeguridad") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:textbox>
													                            </td>
										                                    </tr>
										                                    <tr>
											                                    <td colspan="6">¿Cuándo conduce el coche utiliza el celular con manos libres? <br />
											                                            <asp:textbox id="txtCocheCelular"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.CocheCelular") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:textbox>
													                            </td>
										                                    </tr>
										                                    <tr>
											                                    <td colspan="6">¿Qué tan cerca del límite de velocidad conduces generalmente?<br />
											                                            <asp:textbox id="txtLimiteVelocidad"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.LimiteVelocidad") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:textbox>
													                            </td>
										                                    </tr>
										                                    <tr>
											                                    <td colspan="6">¿Con qué frecuencia en el último mes has manejado o viajado en un vehículo en el que posiblemente el conductor había bebido demasiado?<br />
											                                          <asp:textbox id="txtConductorBebido"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.ConductorBebido") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:textbox>
													                            </td>
									                                        </tr>
										                                    <tr>
											                                    <td colspan="6">¿Con qué frecuencia usas un casco cuando paseas en bicicleta o motocicleta?<br />
											                                          <asp:textbox id="txtCasco"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Casco") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:textbox>
													                            </td>
										                                    </tr>
										                                    <tr>
											                                    <td colspan="6">¿Con qué frecuencia usas filtro solar con factor de protección 15 o mayor cuando pasas tiempo al sol?<br />
											                                        <asp:textbox id="txtFiltroSolar"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.FiltroSolar") %>' CssClass="LabelNoModifySmall" Width="300px"></asp:textbox>
													                            </td>
									                                        </tr>
										                                    <tr>
											                                    <td colspan="6">¿Has realizado alguna revisión de seguridad doméstica en los seis meses anteriores?<br />
											                                        <asp:textbox id="txtSeguridadDomestica"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.SeguridadDomestica") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:textbox>
													                            </td>
										                                    </tr>
										                                     <tr>
													                            <td colspan="6">¿Toma las medidas de protección adecuadas frente al riesgo de contraer enfermedades de transmisión sexual?<br />
													                                 <asp:textbox id="txtTrasmisionSexual"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.TrasmisionSexual") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:textbox>
													                            </td>
											                                </tr>
									                                    </table>
									                                    <table class="tableBorderSmall" id="tblEstadoSalud" style="DISPLAY: none" cellSpacing="0" cellPadding="1"
													                        width="100%" align="center" runat="server">
													                        <tr>
													                            <TD class="headerTable" colSpan="6">PERCEPCIÓN DEL ESTADO DE SALUD&nbsp;</td>
												                            </tr>
												                            <tr>
													                            <td colspan="6">¿Cómo califica su estado de salud en términos generales?<br />
													                                <asp:textbox id="txtEstadoSalud"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.EstadoSalud") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:textbox>
													                            </td>
												                            </tr>
												                            <tr>
													                            <td colspan="6">En general que tan dispuesto está a modificar sus hábitos de vida como son actividad física, dejar de fumar y un <br />programa de educación en salud<br />
													                                <asp:textbox id="txtHabitosVida"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.HabitosVida") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:textbox>
													                             </td>
												                            </tr>
                            												
											                            </table>
											                            <table class="tableBorderSmall" id="tblAntecedentesAusentismo" style="DISPLAY: none" cellSpacing="0" cellPadding="1"
													                        width="100%" align="center" runat="server">
							                                                <tr>
													                            <TD class="headerTable" colSpan="5">ANTECEDENTES AUSENTISMO</td>
												                            </tr>
												                            <tr>
													                            <td colspan="5">¿Ha estado incapacitado en el último año?<br />
													                                <asp:textbox id="txtIncapacitado"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Incapacitado") %>' CssClass="LabelNoModifySmall" Width="300px"></asp:textbox>
													                            </td>
												                            </tr>
												                            <tr>
													                            <td colspan="5">¿Cuál fue el diagnóstico que originó la incapacidad?<br />
                            													    <asp:textbox id="txtIdDiagnosticoIncapacidad"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.IdDiagnosticoIncapacidad") %>' CssClass="LabelNoModifySmall" Width="500px"></asp:textbox>
													                            </td>
                            												</tr>
												                            <tr>
													                            <td colspan="5">¿Por cuántos días fue incapacitado?<br />
													                                <asp:textbox id="txtDiasIncapacidad"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.DiasIncapacidad") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:textbox>
													                            </td>
												                            </tr>
												                            <tr>
													                            <td colspan="5">Antecedentes hospitalarios, médicos o quirúrgicos en el último año que hayan generado incapacidad:
													                            </td>
												                            </tr>
												                            <tr>
													                            <td>
													                                <asp:textbox id="txtIdDiagnosticoHospitalizacion1"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.IdDiagnosticoHospitalizacion1") %>' CssClass="LabelNoModifySmall" Width="450px"></asp:textbox>
													                            </td>
						                                                        <td>
						                                                             <asp:textbox id="txtFechaHospitalizacion1"  runat="server" text='<%# string.Format("{0:dd/MM/yyyy}",DataBinder.Eval(Container, "DataItem.FechaHospitalizacion1")) %>' CssClass="LabelNoModifySmall" Width="70px"></asp:textbox>
													                            </td>
														                        <td colspan="3">
														                            <asp:textbox id="txtDiasHospitalizacion1"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.DiasHospitalizacion1") %>' CssClass="LabelNoModifySmall" Width="40px"></asp:textbox>
													                            </td>
                            														
												                            </tr>
												                            <tr>
													                            <td>
													                                <asp:textbox id="txtIdDiagnosticoHospitalizacion2"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.IdDiagnosticoHospitalizacion2") %>' CssClass="LabelNoModifySmall" Width="450px"></asp:textbox>
													                            </td>
						                                                        <td>
						                                                             <asp:textbox id="txtFechaHospitalizacion2"  runat="server" text='<%# string.Format("{0:dd/MM/yyyy}",DataBinder.Eval(Container, "DataItem.FechaHospitalizacion2")) %>' CssClass="LabelNoModifySmall" Width="70px"></asp:textbox>
													                            </td>
														                        <td colspan="3">
														                            <asp:textbox id="txtDiasHospitalizacion2"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.DiasHospitalizacion2") %>' CssClass="LabelNoModifySmall" Width="40px"></asp:textbox>
													                            </td>
                            														
												                            </tr>
												                            <tr>
													                            <td>
													                                <asp:textbox id="txtIdDiagnosticoHospitalizacion3"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.IdDiagnosticoHospitalizacion3") %>' CssClass="LabelNoModifySmall" Width="450px"></asp:textbox>
													                            </td>
						                                                        <td>
						                                                             <asp:textbox id="txtFechaHospitalizacion3"  runat="server" text='<%# string.Format("{0:dd/MM/yyyy}",DataBinder.Eval(Container, "DataItem.FechaHospitalizacion3")) %>' CssClass="LabelNoModifySmall" Width="70px"></asp:textbox>
													                            </td>
														                        <td colspan="3">
														                            <asp:textbox id="txtDiasHospitalizacion3"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.DiasHospitalizacion3") %>' CssClass="LabelNoModifySmall" Width="40px"></asp:textbox>
													                            </td>
                            														
												                            </tr>
												                            <tr>
													                            <td>
													                                <asp:textbox id="txtIdDiagnosticoHospitalizacion4"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.IdDiagnosticoHospitalizacion4") %>' CssClass="LabelNoModifySmall" Width="450px"></asp:textbox>
													                            </td>
						                                                        <td>
						                                                             <asp:textbox id="txtFechaHospitalizacion4"  runat="server" text='<%# string.Format("{0:dd/MM/yyyy}",DataBinder.Eval(Container, "DataItem.FechaHospitalizacion4")) %>' CssClass="LabelNoModifySmall" Width="70px"></asp:textbox>
													                            </td>
														                        <td colspan="3">
														                            <asp:textbox id="txtDiasHospitalizacion4"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.DiasHospitalizacion4") %>' CssClass="LabelNoModifySmall" Width="40px"></asp:textbox>
													                            </td>
                            														
												                            </tr>
                            												
												                            </table>
												                            <table class="tableBorderSmall" id="tblRecomendaciones" style="DISPLAY: none" cellSpacing="0" cellPadding="1"
													                        width="100%" align="center" runat="server">
							                                                    <tr>
													                                <TD class="headerTable" colSpan="4">RECOMENDACIONES&nbsp;</td>
												                                </tr>
                                												
												                                <tr>
													                                <td colspan="4">Se recomienda ingresar a alguno de los siguientes programas<br />
                                                                                        <asp:BulletedList ID="btlRecomendaciones" DataTextField="Descripcion" runat="server">
                                                                                            
                                                                                        </asp:BulletedList>
                                                                                    </td>
                                												</tr>
											                                </table>
											                                <table class="tableBorderSmall" id="tblNutricion" style="DISPLAY: none" cellSpacing="0" cellPadding="1"
													                            width="100%" align="center" runat="server">
							                                                   <tr>
		                                                                            <TD class="headerTable" colSpan="6">NUTRICIÓN&nbsp;</td>
	                                                                            </tr>
	                                                                            <tr>
		                                                                            <td colspan="2">Peso de hace seis meses</td>
		                                                                            <td width="15%">
		                                                                                <asp:textbox id="txtPesoHace6Meses"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.PesoHace6Meses") + " Kgs" %>' CssClass="LabelNoModifySmall" Width="70px"></asp:textbox>
							                                                        </td>
			                                                                            <td width="18%"></td>
		                                                                            <td width="19%"></td>
		                                                                          
		                                                                            <td width="15%"></td>
                                													
	                                                                            </tr>
                    	                                                        
	                                                                            <tr>
		                                                                            <td colspan="6">¿Consideras que en tu peso, ha habido una fluctuación mayor al 10% en los últimos dos años?.<br />
		                                                                                <asp:textbox id="txtPesoFluctuacion"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.PesoFluctuacion") %>' CssClass="LabelNoModifySmall" Width="700px"></asp:textbox>
							                                                      </td>
    												                            </tr>
                        												        
    												                            <tr>
													                                <td class="headerTable" colSpan="6">HÁBITOS ALIMENTICIOS&nbsp;
														                                </td>
												                                </tr>
												                                <tr>
		                                                                            <td colspan="6">¿Cómo consideras que es tu apetito?.<br />
		                                                                                <asp:textbox id="txtConsideracionApetito"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.ConsideracionApetito") %>' CssClass="LabelNoModifySmall" Width="700px"></asp:textbox>
							                                                      </td>
    												                            </tr>
    												                            <tr>
		                                                                            <td colspan="6">¿Con que frecuencia existe eliminación intestinal?.<br />
		                                                                                <asp:textbox id="txtEliminacionIntestinal"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.EliminacionIntestinal") %>' CssClass="LabelNoModifySmall" Width="700px"></asp:textbox>
							                                                      </td>
    												                            </tr>
												                                <tr>
		                                                                            <td width="18%">¿Eres intolerante a algún alimento?</td>
		                                                                            <td width="15%">
		                                                                                <asp:textbox id="txtIntoranciaAlimento"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.IntoranciaAlimento")  %>' CssClass="LabelNoModifySmall" Width="70px"></asp:textbox>
							                                                        </td>
			                                                                        <td colspan="4">Especificar<br />
			                                                                            <asp:textbox id="txtIntoranciaAlimentoEspecificacion"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.IntoranciaAlimentoEspecificacion") %>' CssClass="LabelNoModifySmall" Width="300px"></asp:textbox>
                    			                                                        
			                                                                        </td>
		                                                                        </tr>
	                                                                            <tr>
		                                                                            <td width="18%">¿Padeces alergia (s) con algún alimento?</td>
		                                                                            <td width="15%">
		                                                                                <asp:textbox id="txtAlergiaAlimento"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.AlergiaAlimento")  %>' CssClass="LabelNoModifySmall" Width="70px"></asp:textbox>
							                                                        </td>
			                                                                        <td colspan="4">Especificar<br />
			                                                                            <asp:textbox id="txtAlergiaAlimentoEspecificacion"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.AlergiaAlimentoEspecificacion") %>' CssClass="LabelNoModifySmall" Width="300px"></asp:textbox>
                    			                                                        
			                                                                        </td>
		                                                                        </tr>
		                                                                        <tr>
		                                                                            <td colspan="6">Generalmente, ¿quién compra los alimentos?<br />
		                                                                                <asp:BulletedList ID="btlCompraAlimentos" DataTextField="Descripcion" runat="server">
                                                                                            
                                                                                        </asp:BulletedList>
							                                                      </td>
    												                            </tr>
    												                            <tr>
		                                                                            <td colspan="6">Generalmente, ¿quién prepara los alimentos?<br />
		                                                                                <asp:BulletedList ID="btlPreparaAlimentos" DataTextField="Descripcion" runat="server">
                                                                                            
                                                                                        </asp:BulletedList>
							                                                      </td>
    												                            </tr>
							                                                    
                            												    <tr>
		                                                                            <td colSpan="6">¿Comidas que regularmente consume al día?</td>
    												                            </tr>
                        												        
                        												        
	                                                                            <tr>
		                                                                            <td width="15%">Desayuno</td>
		                                                                            <td width="10%">
		                                                                                <asp:textbox id="txtDesayuno"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Desayuno") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:textbox>
							                                                        </td>
		                                                                            <td >
		                                                		                            Hora<asp:textbox id="txtDesayunoHora"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.DesayunoHora") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:textbox>
							                                                        </td>
							                                                         <td colspan="3">
		                                                		                            Lugar<asp:textbox id="txtDesayunoLugar"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.DesayunoLugar") %>' CssClass="LabelNoModifySmall" Width="170px"></asp:textbox>
							                                                        </td>
	                                                                            </tr>
	                                                                            <tr>	
		                                                                            <td width="15%">Almuerzo</td>
		                                                                            <td width="10%">
		                                                                                <asp:textbox id="txtAlmuerzo"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Almuerzo") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:textbox>
							                                                        </td>
		                                                                            <td >
		                                                                                    Hora<asp:textbox id="txtAlmuerzoHora"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.AlmuerzoHora") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:textbox>
							                                                        </td>
							                                                         <td colspan="3">
		                                                		                            Lugar<asp:textbox id="txtAlmuerzoLugar"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.AlmuerzoLugar") %>' CssClass="LabelNoModifySmall" Width="170px"></asp:textbox>
							                                                        </td>
	                                                                            </tr>
	                                                                            <tr>
		                                                                            <td width="15%">Comida</td>
		                                                                            <td width="10%">
		                                                                                <asp:textbox id="txtComida"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Comida") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:textbox>
							                                                       </td>
		                                                                            <td >
		                                                                                    Hora<asp:textbox id="txtComidaHora"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.ComidaHora") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:textbox>
							                                                       </td>
							                                                        <td colspan="3">
		                                                		                            Lugar<asp:textbox id="txtComidaLugar"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.ComidaLugar") %>' CssClass="LabelNoModifySmall" Width="170px"></asp:textbox>
							                                                        </td>
	                                                                            </tr>
	                                                                            <tr>
		                                                                            <td width="15%">Entremés</td>
		                                                                            <td width="10%">
		                                                                                <asp:textbox id="txtEntremes"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Entremes") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:textbox>
							                                                        </td>
		                                                                            <td >
                                                                                                    Hora<asp:textbox id="txtEntremesHora"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.EntremesHora") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:textbox>
							                                                        </td>
							                                                        <td colspan="3">
		                                                		                            Lugar<asp:textbox id="txtEntremesLugar"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.EntremesLugar") %>' CssClass="LabelNoModifySmall" Width="170px"></asp:textbox>
							                                                        </td>
	                                                                            </tr>
	                                                                            <tr>
		                                                                            <td width="19%">Cena</td>
		                                                                            <td width="10%">
		                                                                                <asp:textbox id="txtCena"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Cena") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:textbox>
							                                                        </td>
		                                                                            <td >
                                                                                                    Hora<asp:textbox id="txtCenaHora"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.CenaHora") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:textbox>
							                                                        </td>
							                                                        <td colspan="3">
		                                                		                            Lugar<asp:textbox id="txtCenaLugar"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.CenaLugar") %>' CssClass="LabelNoModifySmall" Width="170px"></asp:textbox>
							                                                        </td>
	                                                                            </tr>
	                                                                            <tr>
		                                                                            <td colspan="6">Enlista alimentos que son de tu agrado<br />
		                                                                            <asp:BulletedList ID="btlAlimentosAgrado" DataTextField="Descripcion" runat="server">
                                                                                    </asp:BulletedList>
		                                                                           </td>
    												                            </tr>
    												                            <tr>
		                                                                            <td colspan="6">Enlista alimentos que le disgustan<br />
		                                                                                <asp:BulletedList ID="btlAlimentosDisguntan" DataTextField="Descripcion" runat="server">
                                                                                        </asp:BulletedList>
		                                                                          </td>
    												                            </tr>
	                                                                             <tr>
		                                                                            <td colspan="6">¿Reconoces cuando estás satisfecho?<br />
		                                                                                <asp:textbox id="txtEstarSatisfecho"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.EstarSatisfecho") %>' CssClass="LabelNoModifySmall" Width="700px"></asp:textbox>
							                                                      </td>
    												                            </tr>
    												                             <tr>
		                                                                            <td colspan="6">¿Crees que te satisfaces con facilidad?<br />
		                                                                                <asp:textbox id="txtSatisfaccionFacilidad"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.SatisfaccionFacilidad") %>' CssClass="LabelNoModifySmall" Width="700px"></asp:textbox>
							                                                      </td>
    												                            </tr>
    												                             <tr>
		                                                                            <td colspan="6">¿Reconoces cuando tienes hambre?<br />
		                                                                                <asp:textbox id="txtReconocerHambre"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.ReconocerHambre") %>' CssClass="LabelNoModifySmall" Width="700px"></asp:textbox>
							                                                      </td>
    												                            </tr>
    												                             <tr>
		                                                                            <td colspan="6">¿Acostumbras comer despacio?<br />
		                                                                                <asp:textbox id="txtComerDespacio"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.ComerDespacio") %>' CssClass="LabelNoModifySmall" Width="700px"></asp:textbox>
							                                                      </td>
    												                            </tr>
    												                             <tr>
		                                                                            <td colspan="6">¿A qué hora del día sientes mayor apetito?<br />
		                                                                                <asp:textbox id="txtMayorApetitoHora"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.MayorApetitoHora") %>' CssClass="LabelNoModifySmall" Width="700px"></asp:textbox>
							                                                      </td>
    												                            </tr>
    												                            <tr>
		                                                                            <td colspan="6">¿A qué hora del día sientes antojos?<br />
		                                                                                <asp:textbox id="txtAntojosHora"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.AntojosHora") %>' CssClass="LabelNoModifySmall" Width="700px"></asp:textbox>
							                                                      </td>
    												                            </tr>
    												                            <tr>
		                                                                            <td colspan="6">¿En el último año te has sometido a alguna dieta?<br />
		                                                                                <asp:textbox id="txtSometidoDieta"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.SometidoDieta") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:textbox>
							                                                        </td>
            												                    </tr>
	                                                                            <tr>
		                                                                            <td colspan="6">¿Actualmente llevas a cabo alguna dieta?<br />
		                                                                                <asp:textbox id="txtLlevasDieta"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.LlevasDieta") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:textbox>
							                                                        </td>
            												                    </tr>
            												                    <tr>
		                                                                            <td colspan="6">¿Por quién fue prescrita?<br />
		                                                                                <asp:textbox id="txtQuienPrescribe"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.QuienPrescribe") %>' CssClass="LabelNoModifySmall" Width="700px"></asp:textbox>
							                                                      </td>
    												                            </tr>
    												                            <tr>
		                                                                            <td colspan="6">¿Cuál fue la principal razón que te motivó a iniciar una dieta?<br />
		                                                                                <asp:textbox id="txtMotivoIniciarDieta"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.MotivoIniciarDieta") %>' CssClass="LabelNoModifySmall" Width="700px"></asp:textbox>
							                                                      </td>
    												                            </tr>
    												                            <tr>
		                                                                            <td colspan="6">Comparando con la dieta que llevabas , ¿cómo consideras que es la ingestión actual de tus alimentos?<br />
		                                                                                <asp:textbox id="txtIngestionAlimentos"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.IngestionAlimentos") %>' CssClass="LabelNoModifySmall" Width="700px"></asp:textbox>
							                                                      </td>
    												                            </tr>
    												                            <tr>
		                                                                            <td colspan="6">¿En caso de consumir algún complemento para bajar de peso, por quién fue prescrito?<br />
		                                                                                <asp:textbox id="txtBajarPesoPrescrito"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.BajarPesoPrescrito") %>' CssClass="LabelNoModifySmall" Width="700px"></asp:textbox>
							                                                      </td>
    												                            </tr>
    												                            <tr>
		                                                                            <td colspan="6">Especificar<br />
		                                                                                <asp:textbox id="txtBajarPesoPrescritoEspecificacion"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.BajarPesoPrescritoEspecificacion") %>' CssClass="LabelNoModifySmall" Width="700px"></asp:textbox>
							                                                      </td>
    												                            </tr>
	                                                                            <tr>
													                                <td class="headerTable" colSpan="6">ANTECEDENTES DEL TRANSTORNO DE ALIMENTACIÓN</td>
												                                </tr>
												                                <tr>
		                                                                            <td colspan="6">¿Has padecido de algún trastorno de alimentación?<br />
		                                                                            
		                                                                                <asp:textbox id="txtTrastornoAlimentacion"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.TrastornoAlimentacion") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:textbox>
							                                                        </td>
			                                                                        
            												                    </tr>
            												                    <tr>
		                                                                            <td colspan="6">¿Qué trastorno padeciste?<br />
		                                                                                 <asp:textbox id="txtIdDiagnosticoTrastorno"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.IdDiagnosticoTrastorno") %>' CssClass="LabelNoModifySmall" Width="700px"></asp:textbox>
							                                                      </td>
    												                            </tr>
												                               <tr>
		                                                                            <td colspan="6">¿Hace cuánto tiempo lo padeciste?<br />
		                                                                                 <asp:textbox id="txtPadecerTrastorno"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.PadecerTrastorno") %>' CssClass="LabelNoModifySmall" Width="700px"></asp:textbox>
							                                                      </td>
    												                            </tr>
    												                             <tr>
												                                    <td class="headerTable" colSpan="6">MEDIDAS ANTROPOMÉTRICAS</td>
											                                    </tr>
                                                                                <tr>
	                                                                                <td width="18%">Diámetro de la cintura</td>
	                                                                                <td width="15%">
	                                                                                    <asp:textbox id="txtDiametroCintura"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.DiametroCintura") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:textbox>
						                                                            </td>
	                                                                                <td width="19%">Diámetro de la cadera</td>
	                                                                                <td width="15%">
	                                                                                    <asp:textbox id="txtDiametroCadera"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.DiametroCadera") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:textbox>
						                                                            </td>
		                                                                                <td width="18%">Relación Cintura Cadera</td>
	                                                                                <td width="15%">
	                                                                                    <asp:textbox id="txtRelacionCinturaCadera"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.RelacionCinturaCadera") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:textbox>
						                                                                <br />
                                                                                        <asp:textbox id="txtDescripcionRelacion"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.DescripcionRelacion") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:textbox>
                        								                            
                                                                                   </td>
                                													
                                                                                </tr>
                                                                                <tr>
	                                                                                <td width="18%">Masa grasa</td>
	                                                                                <td width="15%">
	                                                                                    <asp:textbox id="txtMasaGrasa"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.MasaGrasa") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:textbox>
						                                                            </td>
	                                                                                <td width="19%">Masa magra</td>
	                                                                                <td width="15%">
	                                                                                    <asp:textbox id="txtMasaGrama"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.MasaGrama")  %>' CssClass="LabelNoModifySmall" Width="70px"></asp:textbox>
						                                                            </td>
		                                                                            <td width="18%">Peso recomendable</td>
	                                                                                <td width="15%">
	                                                                                    <asp:textbox id="txtPesoRecomendable"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.PesoRecomendable")  %>' CssClass="LabelNoModifySmall" Width="70px"></asp:textbox>
						                                                            </td>
                                													
                                                                                </tr>
                                                                                <tr>
	                                                                                <td width="18%">Excedente de grasa</td>
	                                                                                <td width="15%">
	                                                                                    <asp:textbox id="txtExcedenteGrasa"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.ExcedenteGrasa") + " Kg" %>' CssClass="LabelNoModifySmall" Width="70px"></asp:textbox>
						                                                            </td>
		                                                                                <td width="18%"></td>
	                                                                                <td width="19%"></td>
	                                                                                <td width="15%">
                                                                                        </td>
	                                                                                <td width="15%"></td>
                                													
                                                                                </tr>
                                                                                <tr>
	                                                                                <td width="18%">Diagnóstico nutricional</td>
	                                                                                <td colspan="5" style="width: 63%">
	                                                                                    <asp:textbox id="txtDiagnosticoNutricional"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.DiagnosticoNutricional") %>' CssClass="LabelNoModifySmall" Width="300px"></asp:textbox>
						                                                                <br />
						                                                                <asp:textbox id="txtIdDiagnosticoNutricional"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.IdDiagnosticoNutricional") %>' CssClass="LabelNoModifySmall" Width="300px"></asp:textbox>
						                                                            </td>
												                                </tr>
                                                                                <tr>
	                                                                                <td width="18%">Recomendaciones nutricionales</td>
	                                                                                <td colspan="5" style="width: 63%">
	                                                                                    <asp:textbox id="txtRecomendacionesNutricionales"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.RecomendacionesNutricionales") %>' CssClass="LabelNoModifySmall" Width="600px"></asp:textbox>
						                                                            </td>
                                														
                                													
                                                                                </tr>
                                                                                <tr>
	                                                                                <td colspan="6">Selecciona la afirmación que mejor describa tus planes para tener una alimentación saludable.<br />
	                                                                                    <asp:textbox id="txtAlimentacionSaludable"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.AlimentacionSaludable") %>' CssClass="LabelNoModifySmall" Width="700px"></asp:textbox>
						                                                          </td>
												                                </tr>
												                                  <tr>
												                                        <td class="headerTable" colSpan="6">RUTINA DIARIA</td>
												                                </tr>
												                                 <tr>
		                                                                            <td colspan="2">¿A qué hora acostumbra levantarse?</td>
		                                                                            <td width="18%">Entre semana </td>
		                                                                            <td width="19%"> <asp:textbox id="txtLevantarseEntreSemana"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.LevantarseEntreSemana") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:textbox></td>
		                                                                            <td width="15%">Fin de semana </td>
		                                                                            <td width="15%"> <asp:textbox id="txtLevantarseFinDeSemana"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.LevantarseFinDeSemana")%>' CssClass="LabelNoModifySmall" Width="70px"></asp:textbox></td>
                                													
	                                                                            </tr>
	                                                                            <tr>
		                                                                            <td colspan="2">¿A qué hora acostumbra salir de casa?</td>
		                                                                            <td width="18%">Entre semana </td>
		                                                                            <td width="19%"> <asp:textbox id="txtSalirCasaEntreSemana"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.SalirCasaEntreSemana") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:textbox></td>
		                                                                            <td width="15%">Fin de semana </td>
		                                                                            <td width="15%"> <asp:textbox id="txtSalirCasaFinDeSemana"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.SalirCasaFinDeSemana")%>' CssClass="LabelNoModifySmall" Width="70px"></asp:textbox></td>
                                													
	                                                                            </tr>
	                                                                            <tr>
		                                                                            <td colspan="2">¿A qué hora acostumbra acostarse?</td>
		                                                                            <td width="18%">Entre semana </td>
		                                                                            <td width="19%"> <asp:textbox id="txtAcostarseEntreSemana"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.AcostarseEntreSemana") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:textbox></td>
		                                                                            <td width="15%">Fin de semana </td>
		                                                                            <td width="15%"> <asp:textbox id="txtAcostarseFinDeSemana"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.AcostarseFinDeSemana")%>' CssClass="LabelNoModifySmall" Width="70px"></asp:textbox></td>
                                													
	                                                                            </tr>
	                                                                            <tr>
		                                                                            <td colspan="6">En promedio, ¿Qué tan frecuentemente compras comida rápida?<br />
		                                                                                 <asp:textbox id="txtComidaRapida"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.ComidaRapida") %>' CssClass="LabelNoModifySmall" Width="700px"></asp:textbox>
							                                                        </td>
    												                            </tr>
    												                            <tr>
		                                                                            <td colspan="6" Width="800px">Aproximadamente ¿Cuántos vasos de agua consumes al día?<br />
		                                                                                 <asp:textbox id="txtVasosAgua"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.VasosAgua") %>' CssClass="LabelNoModifySmall" Width="700px"></asp:textbox>
							                                                      </td>
    												                            </tr>
						                                                    </table>
						                                                
																<TABLE class="tableBorderSmall" id="tblImpresionDiagnostica" cellSpacing="0" cellPadding="1"
																	width="100%" align="center" runat="server">
																	<TR>
																		<TD class="headerTableGrey">IMPRESIÓN DIAGNÓSTICA</TD>
																	</TR>
																	<TR>
																		<TD>
																			<asp:DataGrid id="dtgDiagnosticos" runat="server" CssClass="gridNoBorder" Width="100%" BorderStyle="None" ShowHeader="False" AutoGenerateColumns="False" GridLines="Horizontal">
																				<AlternatingItemStyle CssClass="norItemsSmall"></AlternatingItemStyle>
																				<ItemStyle CssClass="norItemsSmall"></ItemStyle>
																				<Columns>
																					<asp:BoundColumn DataField="NombreDiagnostico" HeaderText="Diagnóstico">
																						<ItemStyle Width="70%"></ItemStyle>
																					</asp:BoundColumn>
																					<asp:BoundColumn DataField="TiempoEvolucion" HeaderText="TiempoEvolucion">
																						<ItemStyle Width="10%"></ItemStyle>
																					</asp:BoundColumn>
																					<asp:BoundColumn DataField="PeriodoEvolucion" HeaderText="PeriodoEvolucion">
																						<ItemStyle Width="20%"></ItemStyle>
																					</asp:BoundColumn>
																				</Columns>
																			</asp:DataGrid></TD>
																	</TR>
																</TABLE>
															</P>
														</TD>
													</TR>
												</TABLE>
											</ItemTemplate>
										</asp:datalist></TD>
								</TR>
								<TR>
									<TD>
											<TABLE class="tableBorderFormato" id="Table6" cellPadding="5" width="620" border="0">
											<TR>
												<TD vAlign="top" height="10"></TD>
											</TR>
											<TR>
												<TD align="left" width="620px">________________________________<BR>
													<asp:Label id="lblMedico" runat="server"></asp:Label><BR>
													<asp:Label id="lblIdentificacion" runat="server"></asp:Label>
                                                    <br />
													<asp:Label id="lblInstitucion" runat="server" width="620px"></asp:Label>
                                                    <br />
													<asp:Label id="lblCedula" runat="server" width="620px">Cód. Prof.: </asp:Label>
                                                    <br />
													</TD>
											</TR>
											<TR>
												<TD height="25">
													<asp:Label id="lblRegistroMedico" runat="server">Registro Médico No. ________________________________</asp:Label></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD></TD>
								</TR>
								<TR>
									<TD><uc1:wc_pieformatoorden id="WC_PieFormatoOrden1" runat="server"></uc1:wc_pieformatoorden></TD>
								</TR>
				</TR>
			</TABLE></TD></TR></TBODY></TABLE></form>
	</BODY>
</HTML>
