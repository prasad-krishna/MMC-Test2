<%@ Page language="c#" Codebehind="LIS_historicohistoriaclinica.aspx.cs" AutoEventWireup="false" Inherits="TPA.interfaz_empleado.forma.LIS_historicohistoriaclinica" %>
<HTML>
	<HEAD>
		<title>HC-Historias Clínicas</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../../css/admon.css" type="text/css" rel="stylesheet">
		<LINK href="../../css/Calendar.css" type="text/css" rel="stylesheet">
		<script language="javascript" src="../../scripts/Calendar.js" type="text/javascript"></script>
		<script language="javascript" src="../../scripts/Base.js" type="text/javascript"></script>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" cellSpacing="0" cellPadding="2" width="100%" align="center" border="0">
				<TR>
					<TD align="center"><asp:datalist id="dtlConsultas" runat="server" CellSpacing="5">
							<ItemTemplate>
								<TABLE class="tableBorderBlack" id="tblBorder" cellSpacing="0" cellPadding="0" width="100%"
									border="0" runat="server">
									<TR>
										<TD>
											<P>
												<TABLE class="tableBorderSmall" id="tblDatosPrincipales" cellSpacing="0" cellPadding="1"
													width="100%" align="center" runat="server">
												</TABLE>
												<TABLE class="tableBorderSmall" id="tblDatosConsulta" cellSpacing="0" cellPadding="1" width="100%"
													align="center" runat="server">												
													<TR>
														<TD width="20%">Motivo</TD>
														<TD width="80%">
															<asp:Label id="txtMotivo" runat="server" Width="520" CssClass="LabelNoModifySmall">
																<%# DataBinder.Eval(Container, "DataItem.Motivo") %>
															</asp:Label></TD>
													</TR>
													<TR>
														<TD>Enfermedad Actual</TD>
														<TD>
															<asp:Label id="txtEnfermedad" runat="server" Width="520" CssClass="LabelNoModifySmall">
																<%# DataBinder.Eval(Container, "DataItem.EnfermedadActual") %>
															</asp:Label></TD>
													</TR>
													<TR>
														<TD>Plan de Tratamiento</TD>
														<TD>
															<asp:Label id="txtPlan" runat="server" Width="520" CssClass="LabelNoModifySmall">
																<%# DataBinder.Eval(Container, "DataItem.PlanTratamiento") %>
															</asp:Label></TD>
													</TR>
													<TR>
														<TD>Observaciones</TD>
														<TD>
															<asp:Label id="txtObservaciones" runat="server" Width="520" CssClass="LabelNoModifySmall">
																<%# DataBinder.Eval(Container, "DataItem.ObservacionesGenerales") %>
															</asp:Label></TD>
													</TR>
													<TR>
														<TD>Contrarreferencia</TD>
														<TD>
															<asp:Label id="Label2" runat="server" Width="520" CssClass="LabelNoModifySmall">
																<%# DataBinder.Eval(Container, "DataItem.Contrarreferencia")%>
															</asp:Label></TD>
													</TR>
													
												</TABLE>
												<TABLE class="tableBorderSmall" id="tblAntecendentes" cellSpacing="0" cellPadding="1" width="100%"
													align="center" runat="server">
													<TR>
														<TD class="headerTableGrey" colSpan="2">ANTECEDENTES
														</TD>
													</TR>
													<TR>
														<TD width="20%">Médicos</TD>
														<TD width="80%">
															<asp:Label id="txtMedicos" runat="server" Width="520" CssClass="LabelNoModifySmall">
																<%# DataBinder.Eval(Container, "DataItem.Medicos") %>
															</asp:Label></TD>
													</TR>
													<TR>
														<TD>
															<P>Quirúrgicos</P>
														</TD>
														<TD>
															<asp:Label id="txtQuirurgicos" runat="server" Width="520" CssClass="LabelNoModifySmall">
																<%# DataBinder.Eval(Container, "DataItem.Quirurgicos") %>
															</asp:Label></TD>
													</TR>
													<TR>
														<TD>Ginecoobstétricos</TD>
														<TD>
															<asp:Label id="txtGineco" runat="server" Width="520" CssClass="LabelNoModifySmall">
																<%# DataBinder.Eval(Container, "DataItem.Ginecobstetricos") %>
															</asp:Label></TD>
													</TR>
													<TR>
														<TD>Transfusionales</TD>
														<TD>
															<asp:Label id="txtTransfusionales" runat="server" Width="520" CssClass="LabelNoModifySmall">
																<%# DataBinder.Eval(Container, "DataItem.Transfusionales") %>
															</asp:Label></TD>
													</TR>
													<TR>
														<TD>Tóxico-Alérgicos</TD>
														<TD>
															<asp:Label id="txtToxicoAlergicos" runat="server" Width="520" CssClass="LabelNoModifySmall">
																<%# DataBinder.Eval(Container, "DataItem.ToxicoAlergicos") %>
															</asp:Label></TD>
													</TR>
													<TR>
														<TD>Farmacologicos</TD>
														<TD>
															<asp:Label id="txtFarmacologicos" runat="server" Width="520" CssClass="LabelNoModifySmall">
																<%# DataBinder.Eval(Container, "DataItem.Farmacologicos") %>
															</asp:Label></TD>
													</TR>
													<TR>
														<TD>Otros Antecedentes</TD>
														<TD>
															<asp:Label id="txtOtrosAntecedentes" runat="server" Width="520" CssClass="LabelNoModifySmall">
																<%# DataBinder.Eval(Container, "DataItem.OtrosAntecedentes") %>
															</asp:Label></TD>
													</TR>
													<TR>
														<TD>Familiares</TD>
														<TD>
															<asp:Label id="txtFamiliares" runat="server" Width="520" CssClass="LabelNoModifySmall">
																<%# DataBinder.Eval(Container, "DataItem.Familiares") %>
															</asp:Label></TD>
													</TR>
												</TABLE>
												<TABLE class="tableBorderSmall" id="tblRevisionSistemas" cellSpacing="0" cellPadding="1"
													width="100%" align="center" runat="server">
													<TR>
														<TD class="headerTableGrey" colSpan="2">REVISIÓN POR SISTEMAS
														</TD>
													</TR>
													<TR>
														<TD width="20%">Aspecto General</TD>
														<TD width="80%">
															<asp:Label id="txtAspectoGeneral" runat="server" Width="520" CssClass="LabelNoModifySmall">
																<%# DataBinder.Eval(Container, "DataItem.AspectoGeneral") %>
															</asp:Label></TD>
													</TR>
													<TR>
														<TD>
															<P>Cabeza</P>
														</TD>
														<TD>
															<asp:Label id="txtCabeza" runat="server" Width="520" CssClass="LabelNoModifySmall">
																<%# DataBinder.Eval(Container, "DataItem.Cabeza") %>
															</asp:Label></TD>
													</TR>
													<TR>
														<TD>Cuello</TD>
														<TD>
															<asp:Label id="txtCuello" runat="server" Width="520" CssClass="LabelNoModifySmall">
																<%# DataBinder.Eval(Container, "DataItem.Cuello") %>
															</asp:Label></TD>
													</TR>
													<TR>
														<TD>Tórax</TD>
														<TD>
															<asp:Label id="txtTorax" runat="server" Width="520" CssClass="LabelNoModifySmall">
																<%# DataBinder.Eval(Container, "DataItem.Torax") %>
															</asp:Label></TD>
													</TR>
													<TR>
														<TD>Abdomen</TD>
														<TD>
															<asp:Label id="txtAbdomen" runat="server" Width="520" CssClass="LabelNoModifySmall">
																<%# DataBinder.Eval(Container, "DataItem.Abdomen") %>
															</asp:Label></TD>
													</TR>
													<TR>
														<TD>Otros</TD>
														<TD>
															<asp:Label id="Label1" runat="server" Width="520" CssClass="LabelNoModifySmall">
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
														<TD width="20%">
															<asp:Label id=txtPeso runat="server" CssClass="LabelNoModifySmall" width="120px" text='<%# DataBinder.Eval(Container, "DataItem.Peso") %>' >
															</asp:Label>&nbsp;Kgs</TD>
														<TD width="11%">Talla</TD>
														<TD width="22%">
															<asp:Label id=txtTalla runat="server" CssClass="LabelNoModifySmall" width="120px" text='<%# DataBinder.Eval(Container, "DataItem.Talla") %>'>
															</asp:Label>&nbsp;Mts</TD>
														<TD width="10%">IMC
															<BR>
															<SPAN class="textSmallBlack">(Índice de masa corporal)</SPAN>
														</TD>
														<TD width="22%">
															<asp:Label id=txtIMC runat="server" CssClass="LabelNoModifySmall" width="120px" text='<%# DataBinder.Eval(Container, "DataItem.IndiceMasaCorporal") %>'>
															</asp:Label>&nbsp;Kgs/Mts<SUP>2</SUP></TD>
													</TR>
													<TR>
														<TD>
															<P>Tensión Arterial</P>
														</TD>
														<TD>
															<asp:Label id="txtTension" runat="server" CssClass="LabelNoModifySmall" Width="150px" text='<%# "--" + DataBinder.Eval(Container, "DataItem.TensionArterial") %>'>
															</asp:Label><br />
															<asp:Label id="txtDiastolica" runat="server" CssClass="LabelNoModifySmall" Width="150px" text='<%# DataBinder.Eval(Container, "DataItem.TensionArterialDiastolica") != "" ? " Diastólica:" + DataBinder.Eval(Container, "DataItem.TensionArterialDiastolica") : "" %>'>
															</asp:Label> <br />
															<asp:Label id="txtSistolica" runat="server" CssClass="LabelNoModifySmall" Width="150px" text='<%# DataBinder.Eval(Container, "DataItem.TensionArterialSistolica") != "" ? " Sistólica:" + DataBinder.Eval(Container, "DataItem.TensionArterialSistolica") : "" %>'>
															</asp:Label>
															</TD>
														<TD>Frecuencia Cardiaca</TD>
														<TD>
															<asp:Label id=txtFrecuenciaCardiaca runat="server" CssClass="LabelNoModifySmall" width="120px" text='<%# DataBinder.Eval(Container, "DataItem.FrecuenciaCardiaca") %>'>
															</asp:Label>&nbsp;x minuto</TD>
														<TD>Frecuencia Respiratoria</TD>
														<TD>
															<asp:Label id=txtFrecuenciaRespiratoria runat="server" CssClass="LabelNoModifySmall" width="120px" text='<%# DataBinder.Eval(Container, "DataItem.FrecuenciaRespiratoria") %>'>
															</asp:Label>&nbsp;x minuto</TD>
													</TR>
													<TR>
														<TD>Temperatura</TD>
														<TD>
															<asp:Label id="txtTemperatura" runat="server" CssClass="LabelNoModifySmall" width="120px" text='<%# DataBinder.Eval(Container, "DataItem.Temperatura") %>' >
															</asp:Label>&nbsp;°C
														</TD>
														<TD style="DISPLAY: none" id="tdlblPerimetroAbdominal" runat="server"><asp:Label CssClass="LabelNoModifySmall" id="lblPerimetroAbdominal" runat="server" width="120px" text='<%# DataBinder.Eval(Container, "DataItem.PerimetroAbdominal") != "" ? "Perímetro Abdominal" : "" %>' >
															</asp:Label></TD>
														<TD style="DISPLAY: none" id="tdPerimetroAbdominal" runat="server">
															<asp:Label CssClass="LabelNoModifySmall" id="txtPerimetroAbdominal" runat="server" width="120px" text='<%# DataBinder.Eval(Container, "DataItem.PerimetroAbdominal") %>' >
															</asp:Label>&nbsp;cms
														</TD>
														<TD></TD>
														<TD></TD>
													</TR>
													<TR>
														<TD>Aspecto General</TD>
														<TD colSpan="6">
															<asp:Label id="txtFisGeneral" runat="server" Width="550" CssClass="LabelNoModifySmall">
																<%# DataBinder.Eval(Container, "DataItem.ExamenAspectoGeneral") %>
															</asp:Label>
														</TD>
													</TR>
													<TR>
														<TD>Piel y Faneras</TD>
														<TD colSpan="6">
															<asp:Label id="txtFisPielFanelas" runat="server" Width="550" CssClass="LabelNoModifySmall">
																<%# DataBinder.Eval(Container, "DataItem.ExamenPielFanelas") %>
															</asp:Label>
														</TD>
													</TR>
													<TR>
														<TD>
															<P>Cabeza</P>
														</TD>
														<TD colSpan="6">
															<asp:Label id="txtFisCabeza" runat="server" Width="550" CssClass="LabelNoModifySmall">
																<%# DataBinder.Eval(Container, "DataItem.ExamenCabeza") %>
															</asp:Label>
														</TD>
													</TR>
													<TR>
														<TD>Conjuntiva Ocular</TD>
														<TD colSpan="6">
															<asp:Label id="txtFisConjuntiva" runat="server" Width="550" CssClass="LabelNoModifySmall">
																<%# DataBinder.Eval(Container, "DataItem.ExamenConjuntivaOcular") %>
															</asp:Label>
														</TD>
													</TR>
													<TR>
														<TD>Reflejo Corneal</TD>
														<TD colSpan="6">
															<asp:Label id="txtFisReflejo" runat="server" Width="550" CssClass="LabelNoModifySmall">
																<%# DataBinder.Eval(Container, "DataItem.ExamenReflejoCorneal") %>
															</asp:Label>
														</TD>
													</TR>
													<TR>
														<TD>Pupilas</TD>
														<TD colSpan="6">
															<asp:Label id="txtFisPupilas" runat="server" Width="550" CssClass="LabelNoModifySmall">
																<%# DataBinder.Eval(Container, "DataItem.ExamenPupilas") %>
															</asp:Label>
														</TD>
													</TR>
													<TR>
														<TD>Oídos</TD>
														<TD colSpan="6">
															<asp:Label id="txtFisOidos" runat="server" Width="550" CssClass="LabelNoModifySmall">
																<%# DataBinder.Eval(Container, "DataItem.ExamenOidos") %>
															</asp:Label>
														</TD>
													</TR>
													<TR>
														<TD>Otoscopia</TD>
														<TD colSpan="6">
															<asp:Label id="txtFisOtoscopia" runat="server" Width="550" CssClass="LabelNoModifySmall">
																<%# DataBinder.Eval(Container, "DataItem.ExamenOtoscopia") %>
															</asp:Label>
														</TD>
													</TR>
													<TR>
														<TD>Rinoscopia</TD>
														<TD colSpan="6">
															<asp:Label id="txtFisRinoscopia" runat="server" Width="550" CssClass="LabelNoModifySmall">
																<%# DataBinder.Eval(Container, "DataItem.ExamenRinoscopia") %>
															</asp:Label>
														</TD>
													</TR>
													<TR>
														<TD>Boca y Faringe</TD>
														<TD colSpan="6">
															<asp:Label id="txtFisBocaFaringe" runat="server" Width="550" CssClass="LabelNoModifySmall">
																<%# DataBinder.Eval(Container, "DataItem.ExamenBocaFaringe") %>
															</asp:Label>
														</TD>
													</TR>
													<TR>
														<TD>Amígdalas</TD>
														<TD colSpan="6">
															<asp:Label id="txtFisAmigdalas" runat="server" Width="550" CssClass="LabelNoModifySmall">
																<%# DataBinder.Eval(Container, "DataItem.ExamenAmigdalas") %>
															</asp:Label>
														</TD>
													</TR>
													<TR>
														<TD>Cuello</TD>
														<TD colSpan="6">
															<asp:Label id="txtFisCuello" runat="server" Width="550" CssClass="LabelNoModifySmall">
																<%# DataBinder.Eval(Container, "DataItem.ExamenCuello") %>
															</asp:Label>
														</TD>
													</TR>
													<TR>
														<TD>Tiroides</TD>
														<TD colSpan="6">
															<asp:Label id="txtFisTiroides" runat="server" Width="550" CssClass="LabelNoModifySmall">
																<%# DataBinder.Eval(Container, "DataItem.ExamenTiroides") %>
															</asp:Label>
														</TD>
													</TR>
													<TR>
														<TD>Adenopatias</TD>
														<TD colSpan="6">
															<asp:Label id="txtFisAdenopatias" runat="server" Width="550" CssClass="LabelNoModifySmall">
																<%# DataBinder.Eval(Container, "DataItem.ExamenAdenopatias") %>
															</asp:Label>
														</TD>
													</TR>
													<TR>
														<TD>Tórax</TD>
														<TD colSpan="6">
															<asp:Label id="txtFisTorax" runat="server" Width="550" CssClass="LabelNoModifySmall">
																<%# DataBinder.Eval(Container, "DataItem.ExamenTorax") %>
															</asp:Label>
														</TD>
													</TR>
													<TR>
														<TD>Ruidos Cardiacos</TD>
														<TD colSpan="6">
															<asp:Label id="txtExamenRuidosCardiacos" runat="server" Width="550" CssClass="LabelNoModifySmall">
																<%# DataBinder.Eval(Container, "DataItem.ExamenRuidosCardiacos") %>
															</asp:Label>
														</TD>
													</TR>
													<TR>
														<TD>Ruidos Respiratorios</TD>
														<TD colSpan="6">
															<asp:Label id="txtExamenRuidosRespiratorios" runat="server" Width="550" CssClass="LabelNoModifySmall">
																<%# DataBinder.Eval(Container, "DataItem.ExamenRuidosRespiratorios") %>
															</asp:Label>
														</TD>
													</TR>
													<TR>
														<TD>Abdomen</TD>
														<TD colSpan="6">
															<asp:Label id="txtExamenAbdomen" runat="server" Width="550" CssClass="LabelNoModifySmall">
																<%# DataBinder.Eval(Container, "DataItem.ExamenAbdomen") %>
															</asp:Label>
														</TD>
													</TR>
													<TR>
														<TD>Palpación Abdomen</TD>
														<TD colSpan="6">
															<asp:Label id="txtExamenPalpacionAbdomen" runat="server" Width="550" CssClass="LabelNoModifySmall">
																<%# DataBinder.Eval(Container, "DataItem.ExamenPalpacionAbdomen") %>
															</asp:Label>
														</TD>
													</TR>
													<TR>
														<TD>Genitales Externos</TD>
														<TD colSpan="6">
															<asp:Label id="txtExamenGenitalesExternos" runat="server" Width="550" CssClass="LabelNoModifySmall">
																<%# DataBinder.Eval(Container, "DataItem.ExamenGenitalesExternos") %>
															</asp:Label>
														</TD>
													</TR>
													<TR>
														<TD>Hernias</TD>
														<TD colSpan="6">
															<asp:Label id="txtExamenHernias" runat="server" Width="550" CssClass="LabelNoModifySmall">
																<%# DataBinder.Eval(Container, "DataItem.ExamenHernias") %>
															</asp:Label>
														</TD>
													</TR>
													<TR>
														<TD>Columna Vertebral</TD>
														<TD colSpan="6">
															<asp:Label id="txtExamenColumnaVertebral" runat="server" Width="550" CssClass="LabelNoModifySmall">
																<%# DataBinder.Eval(Container, "DataItem.ExamenColumnaVertebral") %>
															</asp:Label>
														</TD>
													</TR>
													<TR>
														<TD>Extremidades Superiores</TD>
														<TD colSpan="6">
															<asp:Label id="txtExamenExtremidadesSuperiores" runat="server" Width="550" CssClass="LabelNoModifySmall">
																<%# DataBinder.Eval(Container, "DataItem.ExamenExtremidadesSuperiores") %>
															</asp:Label>
														</TD>
													</TR>
													<TR>
														<TD>Extremidades Inferiores</TD>
														<TD colSpan="6">
															<asp:Label id="txtExamenExtremidadesInferiores" runat="server" Width="550" CssClass="LabelNoModifySmall">
																<%# DataBinder.Eval(Container, "DataItem.ExamenExtremidadesInferiores") %>
															</asp:Label>
														</TD>
													</TR>
													<TR>
														<TD>Várices</TD>
														<TD colSpan="6">
															<asp:Label id="txtyExamenVarices" runat="server" Width="550" CssClass="LabelNoModifySmall">
																<%# DataBinder.Eval(Container, "DataItem.ExamenVarices") %>
															</asp:Label>
														</TD>
													</TR>
													<TR>
														<TD>Neurológico</TD>
														<TD colSpan="6">
															<asp:Label id="txtExamenNeurologico" runat="server" Width="550" CssClass="LabelNoModifySmall">
																<%# DataBinder.Eval(Container, "DataItem.ExamenNeurologico") %>
															</asp:Label>
														</TD>
													</TR>
													<TR>
														<TD>Otros</TD>
														<TD colSpan="6">
															<asp:Label id="txtExamenOtros" runat="server" Width="550" CssClass="LabelNoModifySmall">
																<%# DataBinder.Eval(Container, "DataItem.ExamenOtros") %>
															</asp:Label>
														</TD>
													</TR>
													<tr>
														<td>Comentarios Examen Físico</td>
														<td colSpan="6">
															<asp:Label id="lblExamenFisico" runat="server" Width="400" CssClass="LabelNoModifySmall">
																<%# DataBinder.Eval(Container, "DataItem.ComentariosExamenFisico") %>
															</asp:Label></td>
													</tr>
													
												</TABLE>
												<TABLE class="tableBorderSmall" id="tblPruebasBiometricas" cellSpacing="0" cellPadding="1"
													width="100%" align="center" runat="server"  style="DISPLAY: none" >
									               
													<tr>
									                     <td class="headerTableGrey" colSpan="6">PRUEBAS BIOMÉTRICAS
														</td>
													</TR>
													
													<tr>
								    
								                        <td width="20%">Colesterol total</td>
								                        <td width="19%"><asp:Label id="lblColesterolTotal"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.ColesterolTotal") %>' CssClass="LabelNoModifySmall" Width="70px" MaxLength="3"></asp:Label>&nbsp;mg/dl</td>
								                        <td width="15%">Colesterol HDL</td>
								                        <td width="18%"><asp:Label id="lblColesterolHDL"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.ColesterolHDL") %>' CssClass="LabelNoModifySmall" Width="70px" MaxLength="2"></asp:Label>&nbsp;mg/dl
								                        <br />
												                            <asp:Label id="lblColesterolHDLmmol"  text='<%# DataBinder.Eval(Container, "DataItem.ColesterolHDLmmol") %>' CssClass="LabelNoModifySmall" runat="server" Width="70px" MaxLength="2"></asp:Label>&nbsp;mmol/L
												        </td>
								                        <td width="13%">Colesterol LDL</td>
								                        <td width="16%"><asp:Label id="lblColesterolLDL" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.ColesterolLDL") %>' CssClass="LabelNoModifySmall" Width="70px" MaxLength="3"></asp:Label>&nbsp;mg/dl</td>
							                        </tr>
							                        <tr>
									                    <td>Triglicéridos</td>
									                    <td><asp:Label id="lblTrigliceridos" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Trigliceridos") %>'  CssClass="LabelNoModifySmall" Width="70px" MaxLength="3"></asp:Label>&nbsp;mg/dl</td>
									                    <td>Índice Aterogénico</td>
									                    <td width="19%"><asp:Label id="lblIndiceAterogenico" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.IndiceAterogenico") %>' CssClass="LabelNoModifySmall" Width="70px" MaxLength="4"></asp:Label></td>
									                    <td>Antígeno Específico de Próstata</td>
									                    <td><asp:Label id="lblAntigenoProstata" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.AntigenoProstata") %>'  CssClass="LabelNoModifySmall" Width="70px"></asp:Label>&nbsp;mg/dl</td>
								                    </tr>
								                    <tr>
									                    <td>Glucemia en Ayunas</td>
									                    <td><asp:Label id="lblGlucemiaAyunas" runat="server"  text='<%# DataBinder.Eval(Container, "DataItem.GlucemiaAyunas") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:Label>&nbsp;mg/dl</td>
									                    <td>Hemoglobina Glucosilada</td>
									                    <td colspan="1"><asp:Label  id="lblHemoglobinaGlucosilada" runat="server"  text='<%# DataBinder.Eval(Container, "DataItem.HemoglobinaGlucosilada") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:Label>&nbsp;%</td>
									                    <td>Homocisteína</td>
									                    <td><asp:Label id="lblHomocisteina" runat="server"  text='<%# DataBinder.Eval(Container, "DataItem.Homocisteina") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:Label>&nbsp;micromol/L</td>
								                    </tr>
													<TR>
														<TD>Exámenes Laboratorio</TD>
														<TD colspan ="5">
															<asp:Label id="txtExamenesLaboratorio" runat="server" Width="600" CssClass="LabelNoModifySmall">
																<%# DataBinder.Eval(Container, "DataItem.ExamenesLaboratorio") %>
															</asp:Label></TD>
													</TR>
													<tr>
								    
									                    <td width="20%" valign="top">Papanicolau Microbiológico</td>
									                    <td colspan="3" style="width: 38%"><asp:Label id="lblPresenciaMicroorganismos" runat="server"  text='<%# DataBinder.Eval(Container, "DataItem.PresenciaMicroorganismos") %>' CssClass="LabelNoModifySmall" Width="300px"></asp:Label></td>
									                    <td width="12%" >Fecha</td>
									                    <td width="19%" ><asp:Label id="lblFechaPapanicolauMicro" runat="server"  text='<%# DataBinder.Eval(Container, "DataItem.FechaPapanicolauMicro") %>' CssClass="LabelNoModifySmall" Width="80px"></asp:Label></td>
								                    </tr>
								                    <tr>
								    
									                    <td width="20%" valign="top">&nbsp;</td>
									                    <td width="19%" colspan="5">Observaciones&nbsp;<asp:Label id="lblObservacionesPresenciaMicro" runat="server"  text='<%# DataBinder.Eval(Container, "DataItem.ObservacionesPresenciaMicro") %>' CssClass="LabelNoModifySmall" Width="520px" MaxLength="300" ></asp:Label></td>
								                    </tr>
								                    <tr>
								                        <td width="20%" valign="top">Papanicolau Morfológico</td>
									                    <td width="19%" colspan="5" style="width: 30%"><asp:Label id="lblResultadoMorfologico" runat="server"  text='<%# DataBinder.Eval(Container, "DataItem.ResultadoMorfologico") %>' CssClass="LabelNoModifySmall" Width="300px"></asp:Label></td>
    												</tr>
    												
								                    <tr>
    												    <td width="20%">&nbsp;</td>
									                    <td width="80%" colspan ="5">Anormalidades en células epiteliales<br /><asp:Label id="lblAnormalCelulasEpi" runat="server"  text='<%# DataBinder.Eval(Container, "DataItem.AnormalidadCelulasEpiteliales") %>' CssClass="LabelNoModifySmall" Width="300px"></asp:Label></td>
    													
								                    </tr>
    												
								                    <tr>
    												    
									                    <td width="20%">&nbsp;</td>
									                    <td width="80%" colspan ="5">ASC (Células escamosas atípicas)<br /><asp:Label id="lblCelulasEscamosas" runat="server"  text='<%# DataBinder.Eval(Container, "DataItem.CelulasEscamosasAtipicas") %>' CssClass="LabelNoModifySmall" Width="600px"></asp:Label></td>
    													
								                    </tr>
								                    <tr><td width="20%">Mamografía</td>
									                    <td width="39%" colspan ="2"><asp:Label id="lblMamografia" runat="server"  text='<%# DataBinder.Eval(Container, "DataItem.Mamografia") %>' CssClass="LabelNoModifySmall" Width="300px"></asp:Label></td>
    													<td width="20%">Observaciones</td>
                    									<td width="11%" colspan="2"><asp:Label ID="lblMamografiaObservaciones" runat="server"  text='<%# DataBinder.Eval(Container, "DataItem.MamografiaObservaciones") %>'  CssClass="LabelNoModifySmall" MaxLength="300" Width="205px"></asp:Label></td>
												                    
								                    </tr>
								                    <tr>
								                        <td width="20%" valign="top">Audiometría</td>
									                    <td width="19%"><asp:Label id="lblAudiometria" runat="server"  text='<%# DataBinder.Eval(Container, "DataItem.Audiometria") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:Label></td>
                                                        <td width="20%">Observaciones</td>
                                                        <td width="11%" colspan="3"><asp:Label ID="lblAudiometriaObservaciones" runat="server"  text='<%# DataBinder.Eval(Container, "DataItem.AudiometriaObservaciones") %>'  CssClass="LabelNoModifySmall" MaxLength="300" Width="340px"></asp:Label></td>
								                    </tr>
    												
								                    <tr>
    												    
									                    <td width="20%" valign="top">Rayos X (de tórax)</td>
									                    <td width="19%"><asp:Label id="lblRayosX" runat="server"  text='<%# DataBinder.Eval(Container, "DataItem.RayosX") %>'  CssClass="LabelNoModifySmall" Width="70px"></asp:Label></td>
                                                        <td width="20%">Observaciones</td>
                                                        <td width="11%" colspan="3"><asp:Label ID="lblRayosXObservaciones" runat="server"  text='<%# DataBinder.Eval(Container, "DataItem.RayosXObservaciones") %>'  CssClass="LabelNoModifySmall" MaxLength="300" Width="340px"></asp:Label></td>
								                    </tr>
								                    <tr>
								    
									                    <td width="20%">Examen Visual</td>
									                    <td width="19%">Miopía <br /> <asp:label id="lblMiopia" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Miopia") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:label> </td>
									                    <td width="8%">Valor O.D. <br /><asp:label id="lblMiopiaValor" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.MiopiaValor") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:label></td>
									                    <td width="19%">Valor O.I. <br /><asp:label id="lblMiopiaValorOI" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.MiopiaValorOI") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:label></td>
									                    <td width="12%">Observaciones</td>
									                    <td width="19%"><asp:label ID="lblMiopiaObservaciones" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.MiopiaObservaciones") %>' CssClass="LabelNoModifySmall" MaxLength="300" Width="120px"></asp:label></td>
								                    </tr>
								                    <tr>
    												    
									                    <td width="20%">&nbsp;</td>
									                    <td width="19%">Astigmatismo <br /><asp:label id="lblAstigmatismo"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Astigmatismo") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:label> </td>
									                    <td>Valor O.D. <br /><asp:label id="lblAstigmatismoValor"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.AstigmatismoValor") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:label></td>
									                    <td width="19%">Valor O.I. <br /><asp:label id="lblAstigmatismoValorOI"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.AstigmatismoValorOI") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:label></td>
									                    <td width="12%">Observaciones</td>
									                    <td width="19%"><asp:label ID="lblAstigmatismoObservaciones" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.AstigmatismoObservaciones") %>' CssClass="LabelNoModifySmall" MaxLength="300" Width="120px"></asp:label></td>
								                    </tr>
								                    <tr>
                    												    
									                    <td width="20%">&nbsp;</td>
									                    <td width="19%">Hipermetropía<br /><asp:label id="lblHipermetropia" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Hipermetropia") %>' CssClass="LabelNoModifySmall" Width="10px"></asp:label> </td>
									                    <td width="11%">Valor O.D. <br /><asp:label id="lblHipermetropiaValor" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.HipermetropiaValor") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:label></td>
									                    <td  width="19%">Valor O.I. <br /><asp:label id="lblHipermetropiaValorOI" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.HipermetropiaValorOI") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:label></td>
									                    <td width="12%">Observaciones</td>
									                    <td width="19%"> <asp:label ID="lblHipermetropiaObservaciones" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.HipermetropiaObservaciones") %>'  CssClass="LabelNoModifySmall" MaxLength="300" Width="120px"></asp:label></td>
								                    </tr>
								                    <tr>
    												    
									                    <td width="20%">&nbsp;</td>
									                    <td width="19%">Presbicia<br /><asp:label id="lblPresbicia" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Presbicia") %>' CssClass="LabelNoModifySmall" Width="10px"></asp:label> </td>
									                    <td>Valor O.D. <br /><asp:label id="lblPresbiciaValor" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.PresbiciaValor") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:label></td>
									                    <td  width="19%">Valor O.I. <br /><asp:label id="lblPresbiciaValorOI" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.PresbiciaValorOI") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:label></td>
									                    <td width="12%">Observaciones</td>
									                    <td width="19%"><asp:label ID="lblPresbiciaObservaciones" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.PresbiciaObservaciones") %>' CssClass="LabelNoModifySmall" MaxLength="300" Width="120px"></asp:label></td>
								                    </tr>
								                    <tr>
    												    
									                    <td width="20%">&nbsp;</td>
									                    <td width="19%">Otros<br /><asp:label id="lblOtrosExamenVisual" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.OtrosExamenVisual") %>' CssClass="LabelNoModifySmall" Width="10px"></asp:label> </td>
									                    <td width="12%">Diagnóstico</td>
									                    <td colspan="3"><asp:label ID="lblOtrosDiagnostico" runat="server" text='<%# DataBinder.Eval(Container, "DataItem.IdDiagnosticoExamenVisual") %>' CssClass="LabelNoModifySmall" MaxLength="300" Width="340px"></asp:label> </td>
    													
								                    </tr>
												</TABLE>
												<TABLE class="tableBorderSmall" id="tblWellness" style="DISPLAY: none" cellSpacing="0" cellPadding="1"
													                        width="100%" align="center" runat="server">
							                        <tr>
								                        <TD class="headerTableGrey" colSpan="4">WELLNESS</td>
							                        </tr>
							                        <tr>
								                        <td colspan="4">¿Está actualmente afiliado al programa de wellness?<br />
								                        <asp:Label id="lblWellness"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.ProgramaWellness") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:Label>
										                </td>
							                        </tr>
							                        <tr>
								                        <td colspan="4">¿Tiene firmado el acuerdo para participar en el programa?<br />
								                            <asp:Label id="lblFirmaWellness"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.FirmaWellness") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:Label>
										                </td>	
							                        </tr>
							                    
						                        </TABLE>
											<TABLE class="tableBorderSmall" id="tblHabitoFumar" style="DISPLAY: none" cellSpacing="0" cellPadding="1"
								                        width="100%" align="center" runat="server">
								                    <tr>
								                        <TD class="headerTableGrey"  colspan="6"> HÁBITO DE FUMAR</td>
							                        </tr>
    												
							                        <tr>
								                        <td colspan="6">¿Cuál de las siguientes respuestas describe mejor su conducta frente a el cigarrillo?<br />
								                        <asp:Label id="lblConductaCigarrillo"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.ConductaCigarrillo") %>' CssClass="LabelNoModifySmall" Width="300px"></asp:Label>
								                        </td>
							                        </tr>
							                       <tr>
								                        <td colspan="6">¿Cuánto tiempo transcurre desde que se levanta hasta encender el primer cigarrillo?<br />
								                        <asp:Label id="lblTiempoPrimerCigarrillo"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.TiempoPrimerCigarrillo") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:Label>
								                        </td>
							                        </tr>
    												
							                        <tr>
								                        <td colspan="6">¿Tiene dificultades para no fumar en lugares donde está prohibido (Ej. en la iglesia, en la biblioteca, en el cine)?<br />
								                        <asp:Label id="lblDificultadFumar"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.DificultadFumar") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:Label>
								                        </td>
							                        </tr>
							                        <tr>
								                        <td colspan="6">¿Qué cigarrillo le costaría más suprimir?<br />
								                             <asp:Label id="lblCigarrilloSuprimir"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.CigarrilloSuprimir") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:Label>
								                        </td>
							                        </tr>
							                        <tr>
								                        <td colspan="6">¿Cuántos cigarrillos fuma al día?<br />
    													    <asp:Label id="lblCigarrillosalDia"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.CigarrillosalDia") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:Label>
								                        </td>
							                        </tr>
							                        <tr>
								                        <td colspan="6">¿Fuma más frecuentemente durante las primeras horas del día que durante el resto del día?<br />	
								                            <asp:Label id="lblFrecuenciaPrimerasHorasDia"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.FrecuenciaPrimerasHorasDia") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:Label>
								                        </td>
							                        </tr>
							                        <tr>
								                        <td colspan="6">¿Fuma cuándo debe guardar cama por una enfermedad la mayor parte del día?<br />
								                            <asp:Label id="lblFumaEnfermedad"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.FumaEnfermedad") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:Label>
								                        </td>
							                        </tr>
							                        <tr>
								                        <td colspan="6">¿Dentro de qué categoría entran la mayoría de cigarrillos que usted fuma?<br />
								                            <asp:Label id="lblCategoriaCigarrillos"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.CategoriaCigarrillos") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:Label>
								                        </td>
						                            </tr>
							                        <tr>
								                        <td colspan="6">¿Aspira el humo cuando fuma?<br />	
								                            <asp:Label id="lblAspiraHumo"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.AspiraHumo") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:Label>
								                        </td>		
							                        </tr>
							                        <tr>
								                        <td colspan="6">¿Cuántos años hace que dejó de fumar?<br />
								                            <asp:Label id="lblAnosDejoFumar"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.AnosDejoFumar") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:Label>
								                        </td>
							                        </tr>
							                        <tr>
								                        <td colspan="6">¿Cual era el promedio diario de cigarrillos que fumaba durante los dos años previos de dejar el hábito?<br />
    													    <asp:Label id="lblPromedioDiarioX2Anos"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.PromedioDiarioX2Anos") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:Label>
								                        </td>
							                        </tr>
								                    
						                        </table>
						                        
						                        <TABLE class="tableBorderSmall" id="tblConsumoAlcohol" style="DISPLAY: none" cellSpacing="0" cellPadding="1"
								                        width="100%" align="center" runat="server">
								                        <tr>
								                            <TD class="headerTableGrey" colSpan="6">CONSUMO DE ALCOHOL</td>
							                            </tr>
							                            <tr>
								                            <td colspan="6">Alcohol, Copas a la semana (rangos de 1-5)<br />
								                                <asp:Label id="lblCopasSemana"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.CopasSemana") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:Label>
								                            </td>
							                            </tr>
							                            <tr>
								                            <td colspan="6">¿Alguna vez han criticado su consumo de alcohol?<br />
								                                 <asp:Label id="lblCriticaAlcohol"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.CriticaAlcohol") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:Label>
								                            </td>
							                            </tr>
							                            <tr>
								                            <td colspan="6">¿Alguna vez se ha arrepentido de la cantidad de alcohol que consumió?<br />
								                                <asp:Label id="lblArrepentidoAlcohol"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.ArrepentidoAlcohol") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:Label>
								                             </td>
							                            </tr>
							                            <tr>
								                            <td colspan="6">¿Ha tenido lagunas por el consumo de alcohol?<br />
								                                <asp:Label id="lblLagunaAlcohol"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.LagunaAlcohol") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:Label>
								                            </td>
							                            </tr>
							                            <tr>
								                            <td colspan="6">¿Alguna vez lo primero que ha consumido en la mañana ha sido una copa de alcohol?<br />
								                                <asp:Label id="lblMananaAlcohol"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.MananaAlcohol") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:Label>
								                            </td>
        												</tr>
						                            </table>
						                            <TABLE class="tableBorderSmall" id="tblVacunacion" style="DISPLAY: none" cellSpacing="0" cellPadding="1"
								                        width="100%" align="center" runat="server">
						                               <tr>
								                            <TD class="headerTableGrey" colSpan="6">VACUNACIÓN&nbsp;</td>
							                            </tr>
							                            <tr>
								                            <td colspan="6">¿Se ha aplicado la vacuna contra Influenza Estacional en el último año? <br />
        													    <asp:Label id="lblInfluenciaEstacional"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.InfluenciaEstacional") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:Label>
								                            &nbsp;&nbsp;&nbsp;Fecha <asp:Label id="lblFechaInfluenciaEstacional"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.FechaInfluenzaEstacional") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:Label>
								                            </td>
							                            </tr>
							                            <tr>
							                                <td colspan="6">¿Se ha aplicado la vacuna contra Influenza H1N1 en el último año?<br />
							                                    <asp:Label id="lblInfluenciaH1N1"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.InfluenciaH1N1") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:Label>
								                            &nbsp;&nbsp;&nbsp;Fecha <asp:Label id="lblFechaInfluenciaH1N1"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.FechaInfluenciaH1N1") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:Label>
								                            </td>
					                                     </tr>
							                            <tr>
								                            <td colspan="6">¿Se ha aplicado la vacuna contra Fiebre Amarilla? <br />
								                                <asp:Label id="lblFiebreAmarilla"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.FiebreAmarilla") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:Label>
								                            &nbsp;&nbsp;&nbsp;Fecha <asp:Label id="lblFechaFiebreAmarilla"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.FechaFiebreAmarilla") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:Label>
								                            </td>
								                        </tr>   
								                        <tr>
								                            <td colspan="6">¿Se ha aplicado la vacuna contra Hepatitis Viral? <br />
								                                <asp:Label id="lblHepatitisViral"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.HepatitisViral") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:Label>
								                            &nbsp;&nbsp;&nbsp;Fecha <asp:Label id="lblFechaHepatitisViral"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.FechaHepatitisViral") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:Label>
								                            </td>
							                            </tr>
							                            <tr>
								                            <td colspan="6">¿Se ha aplicado la vacuna contra el Tétanos? <br />
								                                 <asp:Label id="lblToxoideTetanico"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.ToxoideTetanico") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:Label>
								                                &nbsp;&nbsp;&nbsp;Fecha <asp:Label id="lblFechaToxoideTetanico"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.FechaToxoideTetanico") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:Label>
								                            </td>
							                            </tr>
							                        </table>
							                        
							                       
											</P>
										</TD>
									</TR>
								</TABLE>
							</ItemTemplate>
						</asp:datalist>
					</TD>
					
				</TR>
				<TR>
					<TD align="center"><asp:datalist id="dtlConsultas1" runat="server" CellSpacing="5">
							<ItemTemplate>
								<TABLE class="tableBorderBlack" id="tblBorder" cellSpacing="0" cellPadding="0" width="100%"
									border="0" runat="server">
									<TR>
										<TD>
											<P>
												<TABLE class="tableBorderSmall" id="tblDatosPrincipales" cellSpacing="0" cellPadding="1"
													width="100%" align="center" runat="server">
												</TABLE>
												<table class="tableBorderSmall" id="tblSedentarismo" style="DISPLAY: none" cellSpacing="0" cellPadding="1"
								                        width="100%" align="center" runat="server">
						                                <tr>
								                            <TD class="headerTableGrey" colSpan="6">SEDENTARISMO&nbsp;</TD>
									                    </tr>
							                            <tr>
								                            <td colspan="6">¿Practicas de manera constante deporte en los últimos 6 meses?<br />
								                                <asp:Label id="lblPracticaDeporte"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.PracticaDeporte") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:Label>
								                              </td>
							                            </tr>
							                            <tr>
								                            <td colspan="6">¿Cuántas veces practicas deporte o actividad física en la semana?<br />
								                                <asp:Label id="lblPracticaDeporteSemana"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.PracticaDeporteSemana") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:Label>
								                             </td>
							                            </tr>
							                            <tr>
								                            <td colspan="6">Promedio de tiempo en minutos en cada sesión:<br />	
        													    <asp:Label id="lblPromedioTiempoMinutos"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.PromedioTiempoMinutos") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:Label>
								                             </td>
							                            </tr>
							                             <tr>
								                            <td colspan="6">¿Qué tipo de actividad física realizas?<br />
								                                <asp:Label id="lblTipoActividadFisica"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.TipoActividadFisica") %>' CssClass="LabelNoModifySmall" Width="700px"></asp:Label>
								                              </td>
							                            </tr>
							                            <tr>
								                            <td colspan="6">¿Cuántas horas ves diarias en promedio de televisión?<br />
								                                <asp:Label id="lblHorasDiariasTV"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.HorasDiariasTV") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:Label>
								                             </td>
							                            </tr>
						                            </table>
						                            <table class="tableBorderSmall" id="tblSaludOral" style="DISPLAY: none" cellSpacing="0" cellPadding="1"
								                        width="100%" align="center" runat="server">
						                                <tr>
								                            <TD class="headerTableGrey" colSpan="6">SALUD ORAL </TD>
									                    </tr>
							                            <tr>
								                            <td colspan="6">¿Ha asistido a consulta odontológica en el último año?<br />
								                                 <asp:Label id="lblConsultaOdontologica"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.ConsultaOdontologica") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:Label>
								                            </td>
							                            </tr>
							                            <tr>
								                            <td colspan="6">¿Cuántas veces se lava los dientes al día?<br />
								                                <asp:Label id="lblLavaDientes"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.LavaDientes") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:Label>
								                            </td>
							                            </tr>
							                            <tr>
								                            <td colspan="6">¿Además de lavarse los dientes, usa hilo dental todos los días?<br />
								                                <asp:Label id="lblSedaDental"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.SedaDental") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:Label>
								                            </td>
        														
							                            </tr>
							                       </table>
												<table class="tableBorderSmall" id="tblEstres" style="DISPLAY: none" cellSpacing="0" cellPadding="1"
													                        width="100%" align="center" runat="server">
								                        <tr>
								                            <TD class="headerTableGrey" colSpan="4">ESTRÉS&nbsp;</td>
							                            </tr>
							                            <tr>
								                            <td colspan="4">En los últimos meses, ¿te has sentido decaído (a), deprimido (a) o estresado (a) de manera persistente?<br />
								                                 <asp:label id="lblSentidoDecaido"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.SentidoDecaido") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:label>
								                            </td>
							                            </tr>
							                            <tr>
								                            <td colspan="4">En los dos últimos meses, ¿has tenido poco interés o placer al hacer las cosas?<br />
									                             <asp:label id="lblInteresPlacer"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.InteresPlacer") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:label>
								                            </td>
							                            </tr>
							                            <tr>
								                            <td colspan="4">¿Cómo clasificarías tu nivel de estrés?<br />
									                            <asp:label id="lblNivelEstres"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.NivelEstres") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:label>
								                            </td>
							                            </tr>
							                            <tr>
								                            <td colspan="4">A continuación se muestra una lista de las fuentes típicas de estrés. Marque todas las que usted sienta sean una sobrecarga para usted: <br />
								                                <asp:Label id="lblFuentesEstres" runat="server" Width="750px" CssClass="LabelNoModifySmall">
																        <%# DataBinder.Eval(Container, "DataItem.FuentesEstres")%>
															     </asp:Label>
                                                            </td>
                                                        </tr>
							                            <tr>
								                            <td colspan="4">
                                                                <br />
                                                                Selecciona el enunciado que mejor describa tus planes para controlar tu estrés<br />
        													    <asp:label id="lblControlarEstres"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.ControlarEstres") %>' CssClass="LabelNoModifySmall" Width="700px"></asp:label>
								                            </td>
        													
							                            </tr>
						                            </table>
						                            <table class="tableBorderSmall" id="tblEmocional" style="DISPLAY: none" cellSpacing="0" cellPadding="1"
								                        width="100%" align="center" runat="server">
								                        <tr>
								                            <TD class="headerTableGrey" colSpan="6">EMOCIONAL&nbsp;</td>
							                            </tr>
        												
        												
							                            <tr>
								                            <td colspan="6">¿En el último mes cómo calificarías la calidad general de tu sueño?<br />
        													    <asp:label id="lblCalificacionSueno"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.CalificacionSueno") %>' CssClass="LabelNoModifySmall" Width="700px"></asp:label>
								                            </td>
							                            </tr>
        												
							                            <tr>
								                            <td colspan="6">Después de una noche habitual de sueño ¿te sientes cansado(a) o fatigado(a)  al levantarte?<br />
								                                <asp:label id="lblEstadoLevantarse"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.EstadoLevantarse") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:label>
								                            </td>
							                            </tr>
							                            <tr>
								                            <td colspan="6">¿Cuantas horas duerme regularmente?<br />
								                                    <asp:label id="lblHorasDuermeRegular"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.HorasDuermeRegular") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:label>
								                            </td>
							                            </tr>
							                            <tr>
								                            <td colspan="6">¿Cómo califica su estado de ánimo emocional?<br />
								                                <asp:label id="lblEstadoAnimoEmocional"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.EstadoAnimoEmocional") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:label>
								                            </td>
        												
							                            </tr>
						                            </table>
						                            <table class="tableBorderSmall" id="tblAccidentalidad" style="DISPLAY: none" cellSpacing="0" cellPadding="1"
								                        width="100%" align="center" runat="server">
								                        <tr>
						                                    <TD class="headerTableGrey" colSpan="6">COMPORTAMIENTOS DE RIESGO Y ACCIDENTALIDAD&nbsp;</td>
					                                    </tr>
        													
					                                    <tr>
						                                    <td colspan="6">¿Utiliza el Cinturón de seguridad? <br />
						                                        <asp:label id="lblCinturonSeguridad"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.CinturonSeguridad") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:label>
								                            </td>
					                                    </tr>
					                                    <tr>
						                                    <td colspan="6">¿Cuándo conduce el coche utiliza el celular con manos libres? <br />
						                                            <asp:label id="lblCocheCelular"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.CocheCelular") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:label>
								                            </td>
					                                    </tr>
					                                    <tr>
						                                    <td colspan="6">¿Qué tan cerca del límite de velocidad conduces generalmente?<br />
						                                            <asp:label id="lblLimiteVelocidad"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.LimiteVelocidad") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:label>
								                            </td>
					                                    </tr>
					                                    <tr>
						                                    <td colspan="6">¿Con qué frecuencia en el último mes has manejado o viajado en un vehículo en el que posiblemente el conductor había bebido demasiado?<br />
						                                          <asp:label id="lblConductorBebido"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.ConductorBebido") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:label>
								                            </td>
				                                        </tr>
					                                    <tr>
						                                    <td colspan="6">¿Con qué frecuencia usas un casco cuando paseas en bicicleta o motocicleta?<br />
						                                          <asp:label id="lblCasco"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Casco") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:label>
								                            </td>
					                                    </tr>
					                                    <tr>
						                                    <td colspan="6">¿Con qué frecuencia usas filtro solar con factor de protección 15 o mayor cuando pasas tiempo al sol?<br />
						                                        <asp:label id="lblFiltroSolar"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.FiltroSolar") %>' CssClass="LabelNoModifySmall" Width="300px"></asp:label>
								                            </td>
				                                        </tr>
					                                    <tr>
						                                    <td colspan="6">¿Has realizado alguna revisión de seguridad doméstica en los seis meses anteriores?<br />
						                                        <asp:label id="lblSeguridadDomestica"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.SeguridadDomestica") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:label>
								                            </td>
					                                    </tr>
					                                    <tr>
								                            <td colspan="6">¿Toma las medidas de protección adecuadas frente al riesgo de contraer enfermedades de transmisión sexual?<br />
								                                 <asp:label id="lblTrasmisionSexual"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.TrasmisionSexual") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:label>
								                            </td>
						                                </tr>
				                                    </table>
				                                    <table class="tableBorderSmall" id="tblEstadoSalud" style="DISPLAY: none" cellSpacing="0" cellPadding="1"
								                        width="100%" align="center" runat="server">
								                        <tr>
								                            <TD class="headerTableGrey" colSpan="6">PERCEPCIÓN DEL ESTADO DE SALUD&nbsp;</td>
							                            </tr>
							                            <tr>
								                            <td colspan="6">¿Cómo califica su estado de salud en términos generales?<br />
								                                <asp:label id="lblEstadoSalud"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.EstadoSalud") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:label>
								                            </td>
							                            </tr>
							                            <tr>
								                            <td colspan="6">En general que tan dispuesto está a modificar sus hábitos de vida como son actividad física, dejar de fumar y un programa de educación en salud<br />
								                                <asp:label id="lblHabitosVida"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.HabitosVida") %>' CssClass="LabelNoModifySmall" Width="200px"></asp:label>
								                             </td>
							                            </tr>
        												
						                            </table>
						                            
	                                                 <table class="tableBorderSmall" id="tblAntecedentesAusentismo" style="DISPLAY: none" cellSpacing="0" cellPadding="1"
								                        width="100%" align="center" runat="server">
		                                                <tr>
								                            <TD class="headerTableGrey" colSpan="5">ANTECEDENTES AUSENTISMO</td>
							                            </tr>
							                            <tr>
								                            <td colspan="5">¿Ha estado incapacitado en el último año?<br />
								                                <asp:label id="lblIncapacitado"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Incapacitado") %>' CssClass="LabelNoModifySmall" Width="300px"></asp:label>
								                            </td>
							                            </tr>
							                            <tr>
								                            <td colspan="5">¿Cuál fue el diagnóstico que originó la incapacidad?<br />
        													    <asp:label id="lblIdDiagnosticoIncapacidad"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.IdDiagnosticoIncapacidad") %>' CssClass="LabelNoModifySmall" Width="500px"></asp:label>
								                            </td>
        												</tr>
							                            <tr>
								                            <td colspan="5">¿Por cuántos días fue incapacitado?<br />
								                                <asp:label id="lblDiasIncapacidad"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.DiasIncapacidad") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:label>
								                            </td>
							                            </tr>
							                            <tr>
								                            <td colspan="5">Antecedentes hospitalarios, médicos o quirúrgicos en el último año que hayan generado incapacidad:
								                            </td>
							                            </tr>
							                            <tr>
								                            <td>
								                                <asp:label id="lblIdDiagnosticoHospitalizacion1"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.IdDiagnosticoHospitalizacion1") %>' CssClass="LabelNoModifySmall" Width="500px"></asp:label>
								                            </td>
	                                                        <td>
	                                                             <asp:label id="lblFechaHospitalizacion1"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.FechaHospitalizacion1") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:label>
								                            </td>
									                        <td colspan="3">
									                            <asp:label id="lblDiasHospitalizacion1"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.DiasHospitalizacion1") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:label>
								                            </td>
        														
							                            </tr>
							                            <tr>
								                            <td>
								                                <asp:label id="lblIdDiagnosticoHospitalizacion2"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.IdDiagnosticoHospitalizacion2") %>' CssClass="LabelNoModifySmall" Width="500px"></asp:label>
								                            </td>
	                                                        <td>
	                                                             <asp:label id="lblFechaHospitalizacion2"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.FechaHospitalizacion2") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:label>
								                            </td>
									                        <td colspan="3">
									                            <asp:label id="lblDiasHospitalizacion2"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.DiasHospitalizacion2") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:label>
								                            </td>
        														
							                            </tr>
							                            <tr>
								                            <td>
								                                <asp:label id="lblIdDiagnosticoHospitalizacion3"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.IdDiagnosticoHospitalizacion3") %>' CssClass="LabelNoModifySmall" Width="500px"></asp:label>
								                            </td>
	                                                        <td>
	                                                             <asp:label id="lblFechaHospitalizacion3"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.FechaHospitalizacion3") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:label>
								                            </td>
									                        <td colspan="3">
									                            <asp:label id="lblDiasHospitalizacion3"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.DiasHospitalizacion3") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:label>
								                            </td>
        														
							                            </tr>
							                            <tr>
								                            <td>
								                                <asp:label id="lblIdDiagnosticoHospitalizacion4"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.IdDiagnosticoHospitalizacion4") %>' CssClass="LabelNoModifySmall" Width="500px"></asp:label>
								                            </td>
	                                                        <td>
	                                                             <asp:label id="lblFechaHospitalizacion4"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.FechaHospitalizacion4") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:label>
								                            </td>
									                        <td colspan="3">
									                            <asp:label id="lblDiasHospitalizacion4"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.DiasHospitalizacion4") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:label>
								                            </td>
        														
							                            </tr>
        												
							                            </table>
							                            <table class="tableBorderSmall" id="tblRecomendaciones" style="DISPLAY: none" cellSpacing="0" cellPadding="1"
								                        width="100%" align="center" runat="server">
		                                                    <tr>
								                                <TD class="headerTableGrey"  colSpan="4">RECOMENDACIONES&nbsp;</td>
							                                </tr>
            												
							                                <tr>
								                                <td colspan="4">Se recomienda ingresar a alguno de los siguientes programas<br />
                                                                    <asp:Label id="lblRecomendaciones" runat="server" Width="520" CssClass="LabelNoModifySmall">
																        <%# DataBinder.Eval(Container, "DataItem.Recomendaciones")%>
															        </asp:Label>
                                                                </td>
            												</tr>
						                                </table>
						                                <table class="tableBorderSmall" id="tblNutricion" style="DISPLAY: none" cellSpacing="0" cellPadding="1"
													                        width="100%" align="center" runat="server">
	                                                        <tr>
		                                                        <TD class="headerTableGrey" colSpan="6">NUTRICIÓN&nbsp;</td>
	                                                        </tr>
	                                                        <tr>
		                                                        <td width="18%">Peso de hace seis meses</td>
		                                                        <td width="15%">
		                                                            <asp:label id="lblPesoHace6Meses"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.PesoHace6Meses") + " Kgs" %>' CssClass="LabelNoModifySmall" Width="70px"></asp:label>
							                                    </td>
			                                                        <td width="18%"></td>
		                                                        <td width="19%"></td>
		                                                        <td width="15%">
                                                                    </td>
		                                                        <td width="15%"></td>
            													
	                                                        </tr>
	                                                        
	                                                        <tr>
		                                                        <td colspan="6">¿Consideras que en tu peso, ha habido una fluctuación mayor al 10% en los últimos dos años?.<br />
		                                                            <asp:label id="lblPesoFluctuacion"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.PesoFluctuacion") %>' CssClass="LabelNoModifySmall" Width="700px"></asp:label>
							                                  </td>
    												        </tr>
    												        
    												        <tr>
													            <td class="headerTable" colSpan="6">HÁBITOS ALIMENTICIOS&nbsp;
														            </td>
												            </tr>
												            <tr>
		                                                        <td colspan="6">¿Cómo consideras que es tu apetito?.<br />
		                                                            <asp:label id="lblConsideracionApetito"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.ConsideracionApetito") %>' CssClass="LabelNoModifySmall" Width="700px"></asp:label>
							                                  </td>
    												        </tr>
    												        <tr>
		                                                        <td colspan="6">¿Con que frecuencia existe eliminación intestinal?.<br />
		                                                            <asp:label id="lblEliminacionIntestinal"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.EliminacionIntestinal") %>' CssClass="LabelNoModifySmall" Width="700px"></asp:label>
							                                  </td>
    												        </tr>
												            <tr>
		                                                        <td width="18%">¿Eres intolerante a algún alimento?</td>
		                                                        <td width="15%">
		                                                            <asp:label id="lblIntoranciaAlimento"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.IntoranciaAlimento")  %>' CssClass="LabelNoModifySmall" Width="70px"></asp:label>
							                                    </td>
			                                                    <td colspan="4">Especificar<br />
			                                                        <asp:label id="lblIntoranciaAlimentoEspecificacion"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.IntoranciaAlimentoEspecificacion") %>' CssClass="LabelNoModifySmall" Width="300px"></asp:label>
			                                                        
			                                                    </td>
		                                                    </tr>
	                                                        <tr>
		                                                        <td width="18%">¿Padeces alergia (s) con algún alimento?</td>
		                                                        <td width="15%">
		                                                            <asp:label id="lblAlergiaAlimento"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.AlergiaAlimento")  %>' CssClass="LabelNoModifySmall" Width="70px"></asp:label>
							                                    </td>
			                                                    <td colspan="4">Especificar<br />
			                                                        <asp:label id="lblAlergiaAlimentoEspecificacion"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.AlergiaAlimentoEspecificacion") %>' CssClass="LabelNoModifySmall" Width="300px"></asp:label>
			                                                        
			                                                    </td>
		                                                    </tr>
		                                                    <tr>
		                                                        <td colspan="6">Generalmente, ¿quién compra los alimentos?<br />
		                                                            <asp:label id="lblCompraAlimentos"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.CompraAlimentos") %>' CssClass="LabelNoModifySmall" Width="700px"></asp:label>
							                                  </td>
    												        </tr>
    												        <tr>
		                                                        <td colspan="6">Generalmente, ¿quién prepara los alimentos?<br />
		                                                            <asp:label id="lblPreparaAlimentos"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.PreparaAlimentos") %>' CssClass="LabelNoModifySmall" Width="700px"></asp:label>
							                                  </td>
    												        </tr>
	                                                        
    												        <tr>
		                                                        <td colSpan="6">¿Comidas que regularmente consume al día?</td>
    												        </tr>
    												        
    												        
	                                                        <tr>
		                                                        <td width="15%">Desayuno</td>
		                                                        <td width="10%">
		                                                            <asp:label id="lblDesayuno"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Desayuno") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:label>
							                                    </td>
		                                                        <td colspan="2">
		                                                		        Hora<asp:label id="lblDesayunoHora"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.DesayunoHora") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:label>
							                                    </td>
							                                     <td colspan="2">
		                                                		        Lugar<asp:label id="lblDesayunoLugar"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.DesayunoLugar") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:label>
							                                    </td>
	                                                        </tr>
	                                                        <tr>	
		                                                        <td width="15%">Almuerzo</td>
		                                                        <td width="10%">
		                                                            <asp:label id="lblAlmuerzo"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Almuerzo") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:label>
							                                    </td>
		                                                        <td colspan="2">
		                                                                Hora<asp:label id="lblAlmuerzoHora"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.AlmuerzoHora") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:label>
							                                    </td>
							                                     <td colspan="2">
		                                                		        Lugar<asp:label id="lblAlmuerzoLugar"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.AlmuerzoLugar") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:label>
							                                    </td>
	                                                        </tr>
	                                                        <tr>
		                                                        <td width="15%">Comida</td>
		                                                        <td width="10%">
		                                                            <asp:label id="lblComida"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Comida") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:label>
							                                   </td>
		                                                        <td colspan="2">
		                                                                Hora<asp:label id="lblComidaHora"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.ComidaHora") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:label>
							                                   </td>
							                                    <td colspan="2">
		                                                		        Lugar<asp:label id="lblComidaLugar"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.ComidaLugar") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:label>
							                                    </td>
	                                                        </tr>
	                                                        <tr>
		                                                        <td width="15%">Entremés</td>
		                                                        <td width="10%">
		                                                            <asp:label id="lblEntremes"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Entremes") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:label>
							                                    </td>
		                                                        <td colspan="2">
                                                                                Hora<asp:label id="lblEntremesHora"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.EntremesHora") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:label>
							                                    </td>
							                                    <td colspan="2">
		                                                		        Lugar<asp:label id="lblEntremesLugar"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.EntremesLugar") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:label>
							                                    </td>
	                                                        </tr>
	                                                        <tr>
		                                                        <td width="19%">Cena</td>
		                                                        <td width="10%">
		                                                            <asp:label id="lblCena"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.Cena") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:label>
							                                    </td>
		                                                        <td colspan="2">
                                                                                Hora<asp:label id="lblCenaHora"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.CenaHora") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:label>
							                                    </td>
							                                    <td colspan="2">
		                                                		        Lugar<asp:label id="lblCenaLugar"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.CenaLugar") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:label>
							                                    </td>
	                                                        </tr>
	                                                        <tr>
		                                                        <td colspan="6">Enlista alimentos que son de tu agrado<br />
		                                                            <asp:label id="lblAlimentosAgrado"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.AlimentosAgrado") %>' CssClass="LabelNoModifySmall" Width="700px"></asp:label>
							                                  </td>
    												        </tr>
    												        <tr>
		                                                        <td colspan="6">Enlista alimentos que le disgustan<br />
		                                                            <asp:label id="lblAlimentosDisguntan"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.AlimentosDisguntan") %>' CssClass="LabelNoModifySmall" Width="700px"></asp:label>
							                                  </td>
    												        </tr>
	                                                         <tr>
		                                                        <td colspan="6">¿Reconoces cuando estás satisfecho?<br />
		                                                            <asp:label id="lblEstarSatisfecho"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.EstarSatisfecho") %>' CssClass="LabelNoModifySmall" Width="700px"></asp:label>
							                                  </td>
    												        </tr>
    												         <tr>
		                                                        <td colspan="6">¿Crees que te satisfaces con facilidad?<br />
		                                                            <asp:label id="lblSatisfaccionFacilidad"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.SatisfaccionFacilidad") %>' CssClass="LabelNoModifySmall" Width="700px"></asp:label>
							                                  </td>
    												        </tr>
    												         <tr>
		                                                        <td colspan="6">¿Reconoces cuando tienes hambre?<br />
		                                                            <asp:label id="lblReconocerHambre"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.ReconocerHambre") %>' CssClass="LabelNoModifySmall" Width="700px"></asp:label>
							                                  </td>
    												        </tr>
    												         <tr>
		                                                        <td colspan="6">¿Acostumbras comer despacio?<br />
		                                                            <asp:label id="lblComerDespacio"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.ComerDespacio") %>' CssClass="LabelNoModifySmall" Width="700px"></asp:label>
							                                  </td>
    												        </tr>
    												         <tr>
		                                                        <td colspan="6">¿A qué hora del día sientes mayor apetito?<br />
		                                                            <asp:label id="lblMayorApetitoHora"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.MayorApetitoHora") %>' CssClass="LabelNoModifySmall" Width="700px"></asp:label>
							                                  </td>
    												        </tr>
    												        <tr>
		                                                        <td colspan="6">¿A qué hora del día sientes antojos?<br />
		                                                            <asp:label id="lblAntojosHora"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.AntojosHora") %>' CssClass="LabelNoModifySmall" Width="700px"></asp:label>
							                                  </td>
    												        </tr>
    												        <tr>
		                                                        <td width="18%">¿En el último año te has sometido a alguna dieta?</td>
		                                                        <td width="15%">
		                                                            <asp:label id="lblSometidoDieta"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.SometidoDieta") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:label>
							                                    </td>
			                                                        <td width="18%"></td>
		                                                        <td width="19%"></td>
		                                                        <td width="15%">
                                                                    </td>
		                                                        <td width="15%"></td>
            												</tr>
	                                                        <tr>
		                                                        <td width="18%">¿Actualmente llevas a cabo alguna dieta?</td>
		                                                        <td width="15%">
		                                                            <asp:label id="lblLlevasDieta"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.LlevasDieta") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:label>
							                                    </td>
			                                                        <td width="18%"></td>
		                                                        <td width="19%"></td>
		                                                        <td width="15%">
                                                                    </td>
		                                                        <td width="15%"></td>
            												</tr>
            												<tr>
		                                                        <td colspan="6">¿Por quién fue prescrita?<br />
		                                                            <asp:label id="lblQuienPrescribe"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.QuienPrescribe") %>' CssClass="LabelNoModifySmall" Width="700px"></asp:label>
							                                  </td>
    												        </tr>
    												        <tr>
		                                                        <td colspan="6">¿Cuál fue la principal razón que te motivó a iniciar una dieta?<br />
		                                                            <asp:label id="lblMotivoIniciarDieta"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.MotivoIniciarDieta") %>' CssClass="LabelNoModifySmall" Width="700px"></asp:label>
							                                  </td>
    												        </tr>
    												        <tr>
		                                                        <td colspan="6">Comparando con la dieta que llevabas , ¿cómo consideras que es la ingestión actual de tus alimentos?<br />
		                                                            <asp:label id="lblIngestionAlimentos"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.IngestionAlimentos") %>' CssClass="LabelNoModifySmall" Width="700px"></asp:label>
							                                  </td>
    												        </tr>
    												        <tr>
		                                                        <td colspan="6">¿En caso de consumir algún complemento para bajar de peso, por quién fue prescrito?<br />
		                                                            <asp:label id="lblBajarPesoPrescrito"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.BajarPesoPrescrito") %>' CssClass="LabelNoModifySmall" Width="700px"></asp:label>
							                                  </td>
    												        </tr>
    												        <tr>
		                                                        <td colspan="6">Especificar<br />
		                                                            <asp:label id="lblBajarPesoPrescritoEspecificacion"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.BajarPesoPrescritoEspecificacion") %>' CssClass="LabelNoModifySmall" Width="700px"></asp:label>
							                                  </td>
    												        </tr>
	                                                        <tr>
													            <td class="headerTableGrey" colSpan="6">ANTECEDENTES DEL TRANSTORNO DE ALIMENTACIÓN</td>
												            </tr>
												            <tr>
		                                                        <td colspan = "6">¿Has padecido de algún trastorno de alimentación?<br />
		                                                            <asp:label id="lblTrastornoAlimentacion"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.TrastornoAlimentacion") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:label>
							                                    </td>
            												</tr>
            												<tr>
		                                                        <td colspan="6">¿Qué trastorno padeciste?<br />
		                                                             <asp:label id="lblIdDiagnosticoTrastorno"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.IdDiagnosticoTrastorno") %>' CssClass="LabelNoModifySmall" Width="700px"></asp:label>
							                                  </td>
    												        </tr>
												           <tr>
		                                                        <td colspan="6">¿Hace cuánto tiempo lo padeciste?<br />
		                                                             <asp:label id="lblPadecerTrastorno"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.PadecerTrastorno") %>' CssClass="LabelNoModifySmall" Width="700px"></asp:label>
							                                  </td>
    												        </tr>
    												        <tr>
													            <td class="headerTableGrey" colSpan="6">MEDIDAS ANTROPOMÉTRICAS</td>
												            </tr>
	                                                        <tr>
		                                                        <td width="18%">Diámetro de la cintura</td>
		                                                        <td width="15%">
		                                                            <asp:label id="lblDiametroCintura"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.DiametroCintura") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:label>
							                                    </td>
		                                                        <td width="19%">Diámetro de la cadera</td>
		                                                        <td width="15%">
		                                                            <asp:label id="lblDiametroCadera"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.DiametroCadera") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:label>
							                                    </td>
			                                                        <td width="18%">Relación Cintura Cadera</td>
		                                                        <td width="15%">
		                                                            <asp:label id="lblRelacionCinturaCadera"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.RelacionCinturaCadera") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:label>
							                                        <br />
                                                                    <asp:label id="lblDescripcionRelacion"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.DescripcionRelacion") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:label>
    								                            
                                                               </td>
            													
	                                                        </tr>
	                                                        <tr>
		                                                        <td width="18%">Masa grasa</td>
		                                                        <td width="15%">
		                                                            <asp:label id="lblMasaGrasa"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.MasaGrasa") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:label>
							                                    </td>
		                                                        <td width="19%">Masa magra</td>
		                                                        <td width="15%">
		                                                            <asp:label id="lblMasaGrama"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.MasaGrama")  %>' CssClass="LabelNoModifySmall" Width="70px"></asp:label>
							                                    </td>
			                                                    <td width="18%">Peso recomendable</td>
		                                                        <td width="15%">
		                                                            <asp:label id="lblPesoRecomendable"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.PesoRecomendable")  %>' CssClass="LabelNoModifySmall" Width="70px"></asp:label>
							                                    </td>
            													
	                                                        </tr>
	                                                        <tr>
		                                                        <td width="18%">Excedente de grasa</td>
		                                                        <td width="15%">
		                                                            <asp:label id="lblExcedenteGrasa"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.ExcedenteGrasa") + " Kg" %>' CssClass="LabelNoModifySmall" Width="70px"></asp:label>
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
		                                                            <asp:label id="lblDiagnosticoNutricional"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.DiagnosticoNutricional") %>' CssClass="LabelNoModifySmall" Width="300px"></asp:label>
							                                        <br />
							                                        <asp:label id="lblIdDiagnosticoNutricional"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.IdDiagnosticoNutricional") %>' CssClass="LabelNoModifySmall" Width="300px"></asp:label>
							                                    </td>
    												        </tr>
	                                                        <tr>
		                                                        <td width="18%">Recomendaciones nutricionales</td>
		                                                        <td colspan="5" style="width: 63%">
		                                                            <asp:label id="lblRecomendacionesNutricionales"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.RecomendacionesNutricionales") %>' CssClass="LabelNoModifySmall" Width="600px"></asp:label>
							                                    </td>
            														
            													
	                                                        </tr>
	                                                        <tr>
		                                                        <td colspan="6">Selecciona la afirmación que mejor describa tus planes para tener una alimentación saludable.<br />
		                                                            <asp:label id="lblAlimentacionSaludable"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.AlimentacionSaludable") %>' CssClass="LabelNoModifySmall" Width="700px"></asp:label>
							                                  </td>
    												        </tr>
    												        
	                                                </table>
	                                                
												    
											</P>
										</TD>
									</TR>
								</TABLE>
							</ItemTemplate>
						</asp:datalist>
					</TD>
					
				</TR>
				<TR>
					<TD align="center"><asp:datalist id="dtlConsultas2" runat="server" CellSpacing="5">
							<ItemTemplate>
								<TABLE class="tableBorderBlack" id="tblBorder" cellSpacing="0" cellPadding="0" width="100%"
									border="0" runat="server">
									<TR>
										<TD>
											<P>
												<TABLE class="tableBorderSmall" id="tblDatosPrincipales" cellSpacing="0" cellPadding="1"
													width="100%" align="center" runat="server">
												</TABLE>
												<table class="tableBorderSmall" id="tblRutinaDiaria" style="DISPLAY: none" cellSpacing="0" cellPadding="1"
								                        width="100%" align="center" runat="server">
								                        <tr>
													            <td class="headerTableGrey" colSpan="6">RUTINA DIARIA</td>
												            </tr>
												             <tr>
		                                                        <td colspan="2">¿A qué hora acostumbra levantarse?</td>
		                                                        <td width="18%">Entre semana </td>
		                                                        <td width="19%"> <asp:label id="lblLevantarseEntreSemana"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.LevantarseEntreSemana") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:label></td>
		                                                        <td width="15%">Fin de semana </td>
		                                                        <td width="15%"> <asp:label id="lblLevantarseFinDeSemana"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.LevantarseFinDeSemana")%>' CssClass="LabelNoModifySmall" Width="70px"></asp:label></td>
            													
	                                                        </tr>
	                                                        <tr>
		                                                        <td colspan="2">¿A qué hora acostumbra salir de casa?</td>
		                                                        <td width="18%">Entre semana </td>
		                                                        <td width="19%"> <asp:label id="lblSalirCasaEntreSemana"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.SalirCasaEntreSemana") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:label></td>
		                                                        <td width="15%">Fin de semana </td>
		                                                        <td width="15%"> <asp:label id="lblSalirCasaFinDeSemana"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.SalirCasaFinDeSemana")%>' CssClass="LabelNoModifySmall" Width="70px"></asp:label></td>
            													
	                                                        </tr>
	                                                        <tr>
		                                                        <td colspan="2">¿A qué hora acostumbra acostarse?</td>
		                                                        <td width="18%">Entre semana </td>
		                                                        <td width="19%"> <asp:label id="lblAcostarseEntreSemana"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.AcostarseEntreSemana") %>' CssClass="LabelNoModifySmall" Width="70px"></asp:label></td>
		                                                        <td width="15%">Fin de semana </td>
		                                                        <td width="15%"> <asp:label id="lblAcostarseFinDeSemana"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.AcostarseFinDeSemana")%>' CssClass="LabelNoModifySmall" Width="70px"></asp:label></td>
            													
	                                                        </tr>
	                                                        <tr>
		                                                        <td colspan="6">En promedio, ¿Qué tan frecuentemente compras comida rápida?<br />
		                                                             <asp:label id="lblComidaRapida"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.ComidaRapida") %>' CssClass="LabelNoModifySmall" Width="700px"></asp:label>
							                                    </td>
    												        </tr>
    												        <tr>
		                                                        <td colspan="6" Width="800px">Aproximadamente ¿Cuántos vasos de agua consumes al día?<br />
		                                                             <asp:label id="lblVasosAgua"  runat="server" text='<%# DataBinder.Eval(Container, "DataItem.VasosAgua") %>' CssClass="LabelNoModifySmall" Width="700px"></asp:label>
							                                  </td>
    												        </tr>
                                                </table>
                                                <TABLE class="tableBorderSmall" id="tblImpresionDiagnostica" cellSpacing="0" cellPadding="1"
													    width="100%" align="center" runat="server">
													    <TR>
														    <TD class="headerTableGrey" colSpan="2">IMPRESIÓN DIAGNÓSTICA
														    </TD>
													    </TR>
													    <TR>
														    <TD width="20%">Diagnosticos</TD>
														    <TD width="80%">
															    <asp:Label id="lblDiagnosticos" runat="server" Width="520" CssClass="LabelNoModifySmall">
																    <%# DataBinder.Eval(Container, "DataItem.Diagnosticos") %>
															    </asp:Label></TD>
													    </TR>
												    </TABLE>
                                            </P>
										</TD>
									</TR>
								</TABLE>
							</ItemTemplate>
						</asp:datalist>
					</TD>
					
				</TR>
	                                                
				<TR>
					<TD align="center"><asp:button id="btnCerrar" runat="server" CssClass="button" Text="Cerrar"></asp:button></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
