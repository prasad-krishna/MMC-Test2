<%@ Register TagPrefix="uc1" TagName="WC_AdicionarDiagnosticoConsulta" Src="../WebControls/WC_AdicionarDiagnosticoConsulta.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_DatosEmpleado" Src="../WebControls/WC_DatosEmpleado.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_BuscarPrestador" Src="../WebControls/WC_BuscarPrestador.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_BuscarDiagnosticoTipoServicio" Src="../WebControls/WC_BuscarDiagnosticoTipoServicio.ascx" %>
<%@ Page language="c#" Codebehind="AE_registronutricion.aspx.cs" AutoEventWireup="false" Inherits="TPA.interfaz_empleado.forma.AE_registronutricion" %>
<%@ Register TagPrefix="uc1" TagName="WC_BuscarProveedor" Src="../WebControls/WC_BuscarProveedor.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_BuscarPrestadorTipoServicio" Src="../WebControls/WC_BuscarPrestadorTipoServicio.ascx" %>
<%@ Register TagPrefix="uc1" TagName="WC_BuscarDiagnostico" Src="../WebControls/WC_BuscarDiagnostico.ascx" %>


<%@ Register src="../WebControls/WC_AdicionarDiagnosticoConsultaCIE10.ascx" tagname="WC_AdicionarDiagnosticoConsultaCIE10" tagprefix="uc2" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
	<title>HC-Historias Clínicas</title>
	<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
	<meta content="C#" name="CODE_LANGUAGE">
	<meta content="JavaScript" name="vs_defaultClientScript">
	<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	<LINK href="../../css/admon.css" type="text/css" rel="stylesheet">
	<script language="javascript" src="../../scripts/Base.js" type="text/javascript"></script>
	<LINK href="../../css/Calendar.css" type="text/css" rel="stylesheet">
	<script language="javascript" src="../../scripts/Calendar.js" type="text/javascript"></script>
    <!-- Inicio - Emilio Bueno 20/11/2012 -->
    <!-- Se agregan Scripts para control de sesión -->
    <script src="../../scripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../../scripts/jquery.countdown.js" type="text/javascript"></script>
    <script src="../../scripts/ControlSesion.js" type="text/javascript"></script>
    <!-- Fin - Emilio Bueno 20/11/2012 -->
</head>
<body leftMargin="5" topMargin="1" onload="CargarConfiguracion();HabilitarValidadores(null)"
	rightMargin="5">
	<form id="Form1" method="post" runat="server">
    <!-- Inicio - Emilio Bueno 20/11/2012 -->
    <!-- Se agregan Controles para mensaje de control de sesión -->
    <div style="display: none;">
        <asp:HiddenField ID="hdnTimeout" runat="server" />
        <asp:HiddenField ID="hdnSesion" runat="server" />
        <span id="shortly" style="display: none;"></span>
        <asp:HiddenField ID="hdnBotonClick" Value="lnkGuardar" runat="server" />
        <asp:HiddenField ID="hdnTiempoMostrarAlerta" runat="server" />
        <asp:HiddenField ID="hdnTiempoGuardarTemporal" runat="server" />
    </div>
    <div id="divCodigoCS">
    </div>
    <!-- Fin - Emilio Bueno 20/11/2012 -->

		<asp:ScriptManager ID="scrMng" runat="server"></asp:ScriptManager>
			<script type="text/javascript">
			function ValidarCaracteres(textareaControl,maxlength)
			{                     
			    if (textareaControl.value.length > maxlength)
		        {            
		            textareaControl.value = textareaControl.value.substring(0,maxlength);            
		            alert("Debe ingresar hasta un maximo de "+maxlength+" caracteres");        
		        }    
			}		    
			function CalcularCinturaCadera()
			{	
			    var strSeparadorMiles = '<%=ConfigurationManager.AppSettings["SeparadorMiles"].ToString() %>';
			    var strSeparadorDecimales = '<%=ConfigurationManager.AppSettings["SeparadorDecimales"].ToString() %>';
			    var txtCintura = document.getElementById('txtDiametroCintura').value.replace(strSeparadorMiles,"");
				var txtCadera = document.getElementById('txtDiametroCadera').value.replace(strSeparadorMiles,"");
				var txtRelacionCinturaCadera = document.getElementById('txtRelacionCinturaCadera');					
				var ResultadoRelacion = (parseFloat(txtCintura) / (parseFloat(txtCadera))).toFixed(2);
				txtRelacionCinturaCadera.value = ResultadoRelacion.toString().replace(".",strSeparadorDecimales).replace(",",strSeparadorDecimales);				
			}
			
			function HabilitarValidadores(checkBox)
			{					
			}

			if (typeof window.event != 'undefined')
			    document.onkeydown = function() {
			        var test_var = event.srcElement.tagName.toUpperCase();
			        var test_id = event.srcElement.id;
			        if (test_var != 'INPUT' && test_var != 'TEXTAREA' && test_id.indexOf('WC_') == -1)
			            return (event.keyCode != 8);
			    }
			else
			    document.onkeypress = function(e) {
			        var test_var = e.target.nodeName.toUpperCase();
			        var test_id = e.target.id;
			        if (test_var != 'INPUT' && test_var != 'TEXTAREA' && test_id.indexOf('WC_') == -1)
			            return (e.keyCode != 8);
			    }
				
		    function MostrarAlimentacionInadecuada()
			{
			    div = document.getElementById("divAlimentacionInadecuada");
                div.style.display = "";
                document.getElementById("lnkMostrar").style.display = "none";;
                document.getElementById("lnkNoMostrar").style.display = "";
			}
			
			function CerrarAlimentacionInadecuada()
			{
			    div = document.getElementById("divAlimentacionInadecuada");
                div.style.display = "none";
                document.getElementById("lnkNoMostrar").style.display = "none";                
                document.getElementById("lnkMostrar").style.display = "";                
			}
			
			</script>
			<table cellSpacing="0" cellPadding="5" width="100%" align="center" border="0">
				<TBODY>
					<tr>
						<TD align="center" colSpan="2">&nbsp;
							<uc1:wc_datosempleado id="WC_DatosEmpleado1" runat="server"></uc1:wc_datosempleado></td>
					</tr>
					<tr>
						<TD align="left" colSpan="2"><asp:imagebutton id="imbHistorial" runat="server" CausesValidation="false" ImageUrl="../../images/icoHistorial.gif"></asp:imagebutton>&nbsp;<asp:linkbutton id="lnkVerHistorico" runat="server" CausesValidation="false">Ver Histórico de Consultas</asp:linkbutton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							<asp:imagebutton id="imbVerHistorialOrdenes" runat="server" ImageUrl="../../images/icoHistorial.gif"
								CausesValidation="false"></asp:imagebutton>&nbsp;
							<asp:linkbutton id="lnkVerHistorialOrdenes" runat="server" CausesValidation="false">Ver Histórico de Órdenes</asp:linkbutton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
							<asp:imagebutton id="imblnkVerHistoria" runat="server" ImageUrl="../../images/icoHistorial.gif" CausesValidation="false"></asp:imagebutton>&nbsp;
							<asp:linkbutton id="lnkVerHistoria" runat="server" CausesValidation="false">Ver Historia Clínica Consolidada</asp:linkbutton>&nbsp;&nbsp;&nbsp;
							<asp:imagebutton id="imbGuardar" runat="server" 
                                ImageUrl="../../iconos/icoGuardar.gif" CausesValidation="false" onclick="imbGuardar_Click"></asp:imagebutton>
                            <asp:linkbutton id="lnkGuardar" runat="server" CausesValidation="false" onclick="lnkGuardar_Click">Guardar</asp:linkbutton>&nbsp;&nbsp;&nbsp;</td>
					</tr>
					<tr>
						<TD align="left" colSpan="2">Los campos marcados con (<SPAN class="textRed">*</SPAN>) 
							son obligatorios</td>
					</tr>
					<tr>
						<TD align="center" colSpan="2">&nbsp;
							<FIELDSET class="FieldSet" style="WIDTH: 97%"><legend><IMG src="../../images/icoHistoria.gif" border="0">
									&nbsp;Nutrición</legend><br>
								<table id="Table3" cellSpacing="0" cellPadding="3" width="95%" align="center">
									<tr>
									<td colspan ="2" runat="server">
                                        <a class="accToggler" id="lnkMostrar" runat="server" href="javascript:MostrarAlimentacionInadecuada();"> <b>Presione aquí</b> para visualizar el antecedente de hábito alimenticio durante la última valoración Wellness</a> 
                                        
									</td>
									</tr>
									<tr>
									<td colspan ="2">                                       
                                        <a class="accToggler" style="display: none" id="lnkNoMostrar"  href="javascript:CerrarAlimentacionInadecuada();"> Presione aquí para <b>cerrar</b> Alimentación Inadecuada</a> 
									</td>
									</tr>
									<tr>
										<td colSpan="2">
										<div id="divAlimentacionInadecuada"  class="accContent" runat="server" style="display:none">
											<table class="tableBorder" id="Table5" cellSpacing="0" cellPadding="3" width="100%" align="center">
												<tr>
													<TD class="headerTable" colSpan="6">ALIMENTACIÓN INADECUADA&nbsp;
														</td>
												</tr>
												<tr>
													<td colspan="6">¿Cuantas porciones de fruta come en el día en promedio?<br />
													 <b>Ejemplo:</b> 120ml (1/2 tasa) de fruta fresca, congelada o enlatada. 60 ml (1/4 tasa) de fruta seca, 120 ml (1/2 tasa) de jugo 100% de fruta, 1 manzana pequeña, 1 plátano pequeño o 4 fresas grandes.<br />
                                                        <br />
													      <asp:updatePanel id="upPorcionesFrutas" runat="server">
                                                            <ContentTemplate>
                                                                <b>Respuesta:</b> <asp:Label ID="lblPorcionesFrutas" runat="server" Text="________________"></asp:Label>                                
                                                            </ContentTemplate>
                                                        </asp:updatePanel>
													</td>
												</tr>
												<tr>
													<td colspan="6">¿Cuantas porciones de vegetales come cada día, en promedio?<br />	
													    <b>Ejemplo:</b> 120ml (1/2 tasa) de vegetales crudos o cocidos, 240 ml (1 tasa) de lechuga frondosa, 120 ml (1/2 tasa) de jugo 100% de vegetales, 120 (1/2 tasa) de frijoles, 1 zanahoria mediana, 1 elote pequeño.<br />
                                                        <br />	
                                                        											   
													    <asp:updatePanel id="UpPorcionesVegetales" runat="server">
                                                                <ContentTemplate>
                                                               <b>Respuesta:</b>  <asp:Label ID="lblPorcionesVegetales" runat="server" Text="________________"></asp:Label>                                                                                                                                          
                                                                </ContentTemplate>
                                                        </asp:updatePanel>
													</td>
												</tr>
												<tr>
													<td colspan="6">¿Qué tan frecuente su elección de comida y/o opciones al cocinar incluyen uno o más de los siguientes ejemplos?<br />	
													    <b>Ejemplo:</b> Carne roja grasa (Tocino o carne frita), salami, Bolonia, carne molida, hamburguesas, hot dogs y salsas, pollo frito, pescado frito y papas fritas, donas, galletas, pastas, barras de dulce, leche entera, crema o queso (o comida hecho con éstos), mantequilla, margarina, manteca, helado.<br />
                                                        <br />	
                                                         											   
													    <asp:updatePanel id="UpFrecuenciaCarne" runat="server">
                                                                <ContentTemplate>
                                                                   <b>Respuesta:</b>  <asp:Label ID="lblFrecuenciaCarne" runat="server" Text="________________"></asp:Label>                                                                   
                                                                </ContentTemplate>
                                                        </asp:updatePanel>
													</td>
												</tr>
												<tr>
													<td colspan="6">¿Qué tan frecuente su elección de comida y/o opciones al cocinar incluyen uno o más de los siguientes ejemplos?<br />	
													    <b>Ejemplo:</b> Nueces, mantequilla de nuez, mantequilla de cacahuate, aguacate, pescado rico en omega 3 (salmón, atún), cápsulas de aceite de pescado, aceites como: oliva, canola, girasol, sésamo, maíz, cacahuate, soya, (NO aceite de palma o coco), margarina líquida.<br />
                                                        <br />	
                                                         											   
													    <asp:updatePanel id="upFrecuenciaSano" runat="server">
                                                                <ContentTemplate>
                                                                    <b>Respuesta:</b>  <asp:Label ID="lblFrecuenciaSano" runat="server" Text="________________"></asp:Label>                                                                  
                                                                </ContentTemplate>
                                                        </asp:updatePanel>
													</td>
												</tr>
												<tr>
													<td colspan="6">¿Cuántas porciones de alimentos de grano come al día, en promedio?<br />	
													    <b>Ejemplo:</b> Avena, palomas de maíz, arroz. 1 rebanada de pan 100% de trigo, 28 gr o 240 ml (1 tasa) de cereal de grano, 120 ml (1/2 tasa) de cereal de avena (cocido), 120 ml (1/2 tasa) de arroz cocido o pasta de trigo.<br />
                                                        <br />
                                                         												   
													    <asp:updatePanel id="upFrecuenciaGranos" runat="server">
                                                                <ContentTemplate>
                                                                     <b>Respuesta:</b> <asp:Label ID="lblFrecuenciaGranos" runat="server" Text="________________"></asp:Label>                                                                  
                                                                </ContentTemplate>
                                                        </asp:updatePanel>
													</td>
												</tr>
												<tr>
													<td colspan="6">¿Cuántas bebidas con azúcar (no endulzadas artificialmente) toma por día en promedio?<br />	
													    <b>Ejemplo:</b> una bebida es equivalente a 360ml.<br />
                                                        <br />	                                                          											   
													    <asp:updatePanel id="upFrecuenciaAzucar" runat="server">
                                                                <ContentTemplate>
                                                                     <b>Respuesta:</b> <asp:Label ID="lblFrecuenciaAzucar" runat="server" Text="________________"></asp:Label>                                                                  
                                                                </ContentTemplate>
                                                        </asp:updatePanel>
													</td>
												</tr>
												<tr>
													<td colspan="6">¿Cuántas veces es cuidadoso con el límite y cantidad de sal (sodio) en sus comidas y bebidas?<br />
                                                        <br />	
                                                        										   
													    <asp:updatePanel id="upFrecuenciaSodio" runat="server">
                                                                <ContentTemplate>
                                                                     <b>Respuesta:</b>  <asp:Label ID="lblFrecuenciaSodio" runat="server" Text="________________"></asp:Label>                                                                  
                                                                </ContentTemplate>
                                                        </asp:updatePanel>
													</td>
												</tr>
												
											</table>
											</div>
										</td>
									</tr>	  
									<tr>
										<td colSpan="2" id="nutricion">
										<div id="divNutricion" runat="server" visible="False">
											<table class="tableBorder" id="Table12" cellSpacing="0" cellPadding="3" width="100%" align="center">
												<tr>
													<TD class="headerTable" colSpan="6">NUTRICIÓN&nbsp;
														</td>
												</tr>
												<tr>
													<td colspan="2">Peso de hace seis meses</td>
													<td width="18%"><asp:textbox onkeypress="return currencyFormat(this,event,true,true)" onkeydown="return keyDown(this,event)"
															id="txtPesoHace6Meses"  runat="server" CssClass="textBox" Width="70px"></asp:textbox>
                                                        Kgs<br /><asp:requiredfieldvalidator id="rfvPesoHace6Meses" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="txtPesoHace6Meses" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator></td>
														<td width="18%"></td>
													<td width="19%"></td>
													
													<td width="15%"></td>
													
												</tr>
												<tr>
													<td colspan="6">¿Consideras que en tu peso, ha habido una fluctuación mayor al 10% en los últimos dos años?<br />
													<asp:radiobuttonlist id="rblPesoFluctuacion" runat="server" Width="112px" RepeatDirection="Horizontal">
															<asp:ListItem Value="1">Si</asp:ListItem>
															<asp:ListItem Value="0">No</asp:ListItem>
														</asp:radiobuttonlist><asp:requiredfieldvalidator id="rfvPesoFluctuacion" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="rblPesoFluctuacion" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator>
													</td>
												</tr>
												</table>			
									<table class="tableBorder" id="Table6" cellSpacing="0" cellPadding="3" width="100%" align="center">
												<tr>
													<td class="headerTable" colSpan="6">HÁBITOS ALIMENTICIOS&nbsp;
														</td>
												</tr>
												
												<tr>
													<td colspan="6">¿Cómo consideras que es tu apetito?<br />	
													
														<asp:updatePanel id="UpdatePanel1" runat="server">
                                                               <ContentTemplate>
                                                                   <asp:DropDownList ID="ddlConsideracionApetito" runat="server" CssClass="textBox" 
                                                                       Visible="True" >
                                                                   </asp:DropDownList>
                                                               </ContentTemplate>
                                                        </asp:updatePanel>
														<asp:requiredfieldvalidator id="rfvConsideracionApetito" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="ddlConsideracionApetito" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator>
													</td>
												</tr>
												<tr>
													<td colspan="6">¿Con que frecuencia existe eliminación intestinal?<br />	
													
														<asp:updatePanel id="UpdatePanel2" runat="server">
                                                               <ContentTemplate>
                                                                   <asp:DropDownList ID="ddlEliminacionIntestinal" runat="server" CssClass="textBox" 
                                                                       Visible="True" >
                                                                   </asp:DropDownList>
                                                               </ContentTemplate>
                                                        </asp:updatePanel>
														<asp:requiredfieldvalidator id="rfvEliminacionIntestinal" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="ddlEliminacionIntestinal" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator>
													</td>
												</tr>
												<tr>
													<td >¿Eres intolerante a algún alimento?</td>
													<td ><asp:radiobuttonlist id="rblIntoranciaAlimento" runat="server" Width="112px" RepeatDirection="Horizontal">
															<asp:ListItem Value="1">Si</asp:ListItem>
															<asp:ListItem Value="0">No</asp:ListItem>
														</asp:radiobuttonlist><asp:requiredfieldvalidator id="rfvIntoranciaAlimento" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="rblIntoranciaAlimento" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator>
													</td>
													<td >Especificar</td>
													<td colspan="3"><asp:textbox id="txtIntoranciaAlimentoEspecificacion" CssClass="textBox" runat="server" onkeypress='return (this.value.length < 300);' 
                                                            MaxLength="300"
						                                    TextMode="MultiLine" Height="40px" Width="370px"></asp:textbox><br /><asp:requiredfieldvalidator id="rfvIntoranciaAlimentoEspecificacion" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="txtIntoranciaAlimentoEspecificacion" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator>
                                                        </td>
												</tr>
												<tr>
													<td>¿Padeces alergia (s) con algún alimento?</td>
													<td ><asp:radiobuttonlist id="rblAlergiaAlimento" runat="server" Width="112px" RepeatDirection="Horizontal">
															<asp:ListItem Value="1">Si</asp:ListItem>
															<asp:ListItem Value="0">No</asp:ListItem>
														</asp:radiobuttonlist><asp:requiredfieldvalidator id="rfvAlergiaAlimento" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="rblAlergiaAlimento" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator>
													</td>
													<td >Especificar</td>
													<td colspan="3"><asp:textbox id="txtAlergiaAlimentoEspecificacion" CssClass="textBox" runat="server" onkeypress='return (this.value.length < 300);' 
                                                            MaxLength="300"
						                                    TextMode="MultiLine" Height="40px" Width="370px"></asp:textbox><br /><asp:requiredfieldvalidator id="rfvAlergiaAlimentoEspecificacion" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="txtAlergiaAlimentoEspecificacion" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator>
                                                        </td>
												</tr>
												<tr>
													<td colspan="6">Generalmente, ¿quién compra los alimentos?<br />
													    <asp:CheckBoxList ID="cblCompraAlimentos" runat="server" 
                                                            RepeatDirection="Horizontal" CssClass="tableNoBorderSmall" 
                                                            RepeatColumns="2">
                                                        </asp:CheckBoxList>
                                                    </td>
												</tr>
												<tr>
													<td colspan="6">Generalmente, ¿quién prepara los alimentos?<br />
													    <asp:CheckBoxList ID="cblPreparaAlimentos" runat="server" 
                                                            RepeatDirection="Horizontal" CssClass="tableNoBorderSmall" 
                                                            RepeatColumns="2">
                                                        </asp:CheckBoxList>
                                                    </td>
												</tr>
												<tr>
													<td colSpan="6">¿Comidas que regularmente consume al día?&nbsp;
														</td>
												</tr>
												
												<tr>
													<td>Desayuno</td>
													<td><asp:radiobuttonlist id="rblDesayuno" runat="server" Width="112px" RepeatDirection="Horizontal">
															<asp:ListItem Value="1">Si</asp:ListItem>
															<asp:ListItem Value="0">No</asp:ListItem>
														</asp:radiobuttonlist><asp:requiredfieldvalidator id="rfvDesayuno" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="rblDesayuno" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator>
													</td>
													<td style="text-align: right" >Hora</td>
													<td >		
															<asp:updatePanel id="UpdatePanel24" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:DropDownList ID="ddlDesayunoHora" runat="server" CssClass="textBox" 
                                                                        Visible="True" >
                                                                        <asp:ListItem Value="0">--Seleccione--</asp:ListItem>
                                                                        <asp:ListItem>1</asp:ListItem>
                                                                        <asp:ListItem>2</asp:ListItem>
                                                                        <asp:ListItem>3</asp:ListItem>
                                                                        <asp:ListItem>4</asp:ListItem>
                                                                        <asp:ListItem>5</asp:ListItem>
                                                                        <asp:ListItem>6</asp:ListItem>
                                                                        <asp:ListItem>7</asp:ListItem>
                                                                        <asp:ListItem>8</asp:ListItem>
                                                                        <asp:ListItem>9</asp:ListItem>
                                                                        <asp:ListItem>10</asp:ListItem>
                                                                        <asp:ListItem>11</asp:ListItem>
                                                                        <asp:ListItem>12</asp:ListItem>
                                                                        <asp:ListItem>13</asp:ListItem>
                                                                        <asp:ListItem>14</asp:ListItem>
                                                                        <asp:ListItem>15</asp:ListItem>
                                                                        <asp:ListItem>16</asp:ListItem>
                                                                        <asp:ListItem>17</asp:ListItem>
                                                                        <asp:ListItem>18</asp:ListItem>
                                                                        <asp:ListItem>19</asp:ListItem>
                                                                        <asp:ListItem>20</asp:ListItem>
                                                                        <asp:ListItem>21</asp:ListItem>
                                                                        <asp:ListItem>22</asp:ListItem>
                                                                        <asp:ListItem>23</asp:ListItem>
                                                                        <asp:ListItem>24</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    
                                                                </ContentTemplate>
                                                        </asp:updatePanel><asp:requiredfieldvalidator id="rfvDesayunoHora" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="ddlDesayunoHora" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator></td>
													<td colspan="2">
													    <asp:updatePanel id="UpdatePanel3" runat="server">
                                                              <ContentTemplate>
                                                                    Lugar<asp:DropDownList ID="ddlDesayunoLugar" runat="server" CssClass="textBox" 
                                                                       Visible="True" >
                                                                   </asp:DropDownList>
                                                               </ContentTemplate>
                                                        </asp:updatePanel>
														<asp:requiredfieldvalidator id="rfvDesayunoLugar" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="ddlDesayunoLugar" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator></td>
												</tr>
												<tr>	
													<td width="15%" >Almuerzo</td>
													<td width="10%" ><asp:radiobuttonlist id="rblAlmuerzo" runat="server" Width="112px" RepeatDirection="Horizontal">
															<asp:ListItem Value="1">Si</asp:ListItem>
															<asp:ListItem Value="0">No</asp:ListItem>
														</asp:radiobuttonlist><asp:requiredfieldvalidator id="rfvAlmuerzo" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="rblAlmuerzo" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator>
													</td>
													<td style="text-align: right" >Hora</td>
													<td ><asp:updatePanel id="UpdatePanel25" runat="server">
                                                                <ContentTemplate>
                                                                   <asp:DropDownList ID="ddlAlmuerzoHora" runat="server" CssClass="textBox" 
                                                                        Visible="True" >
                                                                        <asp:ListItem Value="0">--Seleccione--</asp:ListItem>
                                                                        <asp:ListItem>1</asp:ListItem>
                                                                        <asp:ListItem>2</asp:ListItem>
                                                                        <asp:ListItem>3</asp:ListItem>
                                                                        <asp:ListItem>4</asp:ListItem>
                                                                        <asp:ListItem>5</asp:ListItem>
                                                                        <asp:ListItem>6</asp:ListItem>
                                                                        <asp:ListItem>7</asp:ListItem>
                                                                        <asp:ListItem>8</asp:ListItem>
                                                                        <asp:ListItem>9</asp:ListItem>
                                                                        <asp:ListItem>10</asp:ListItem>
                                                                        <asp:ListItem>11</asp:ListItem>
                                                                        <asp:ListItem>12</asp:ListItem>
                                                                        <asp:ListItem>13</asp:ListItem>
                                                                        <asp:ListItem>14</asp:ListItem>
                                                                        <asp:ListItem>15</asp:ListItem>
                                                                        <asp:ListItem>16</asp:ListItem>
                                                                        <asp:ListItem>17</asp:ListItem>
                                                                        <asp:ListItem>18</asp:ListItem>
                                                                        <asp:ListItem>19</asp:ListItem>
                                                                        <asp:ListItem>20</asp:ListItem>
                                                                        <asp:ListItem>21</asp:ListItem>
                                                                        <asp:ListItem>22</asp:ListItem>
                                                                        <asp:ListItem>23</asp:ListItem>
                                                                        <asp:ListItem>24</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    
                                                                </ContentTemplate>
                                                        </asp:updatePanel><asp:requiredfieldvalidator id="rfvAlmuerzoHora" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="ddlAlmuerzoHora" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator></td>
													<td colspan="2"><asp:updatePanel id="UpdatePanel4" runat="server">
                                                              <ContentTemplate>
                                                                    Lugar<asp:DropDownList ID="ddlAlmuerzoLugar" runat="server" CssClass="textBox" 
                                                                       Visible="True" >
                                                                   </asp:DropDownList>
                                                               </ContentTemplate>
                                                        </asp:updatePanel>
														<asp:requiredfieldvalidator id="rfvAlmuerzoLugar" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="ddlAlmuerzoLugar" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator></td>
												</tr>
												<tr>
													<td width="15%">Comida</td>
													<td width="10%"><asp:radiobuttonlist id="rblComida" runat="server" Width="112px" RepeatDirection="Horizontal">
															<asp:ListItem Value="1">Si</asp:ListItem>
															<asp:ListItem Value="0">No</asp:ListItem>
														</asp:radiobuttonlist><asp:requiredfieldvalidator id="rfvComida" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="rblComida" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator>
															
													</td>
													<td style="text-align: right" >Hora</td>
													<td ><asp:updatePanel id="UpdatePanel26" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:DropDownList ID="ddlComidaHora" runat="server" CssClass="textBox" 
                                                                        Visible="True" >
                                                                        <asp:ListItem Value="0">--Seleccione--</asp:ListItem>
                                                                        <asp:ListItem>1</asp:ListItem>
                                                                        <asp:ListItem>2</asp:ListItem>
                                                                        <asp:ListItem>3</asp:ListItem>
                                                                        <asp:ListItem>4</asp:ListItem>
                                                                        <asp:ListItem>5</asp:ListItem>
                                                                        <asp:ListItem>6</asp:ListItem>
                                                                        <asp:ListItem>7</asp:ListItem>
                                                                        <asp:ListItem>8</asp:ListItem>
                                                                        <asp:ListItem>9</asp:ListItem>
                                                                        <asp:ListItem>10</asp:ListItem>
                                                                        <asp:ListItem>11</asp:ListItem>
                                                                        <asp:ListItem>12</asp:ListItem>
                                                                        <asp:ListItem>13</asp:ListItem>
                                                                        <asp:ListItem>14</asp:ListItem>
                                                                        <asp:ListItem>15</asp:ListItem>
                                                                        <asp:ListItem>16</asp:ListItem>
                                                                        <asp:ListItem>17</asp:ListItem>
                                                                        <asp:ListItem>18</asp:ListItem>
                                                                        <asp:ListItem>19</asp:ListItem>
                                                                        <asp:ListItem>20</asp:ListItem>
                                                                        <asp:ListItem>21</asp:ListItem>
                                                                        <asp:ListItem>22</asp:ListItem>
                                                                        <asp:ListItem>23</asp:ListItem>
                                                                        <asp:ListItem>24</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    
                                                                </ContentTemplate>
                                                        </asp:updatePanel><asp:requiredfieldvalidator id="rfvComidaHora" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="ddlComidaHora" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator></td>
													<td colspan="2"><asp:updatePanel id="UpdatePanel7" runat="server">
                                                              <ContentTemplate>
                                                                    Lugar<asp:DropDownList ID="ddlComidaLugar" runat="server" CssClass="textBox" 
                                                                       Visible="True" >
                                                                   </asp:DropDownList>
                                                               </ContentTemplate>
                                                        </asp:updatePanel>
														<asp:requiredfieldvalidator id="rfvComidaLugar" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="ddlComidaLugar" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator></td>
												</tr>
												<tr>
													<td width="15%">Entremés</td>
													<td width="10%"><asp:radiobuttonlist id="rblEntremes" runat="server" Width="112px" RepeatDirection="Horizontal">
															<asp:ListItem Value="1">Si</asp:ListItem>
															<asp:ListItem Value="0">No</asp:ListItem>
														</asp:radiobuttonlist><asp:requiredfieldvalidator id="rfvEntremes" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="rblEntremes" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator>
													</td>
													<td style="text-align: right" >Hora</td>
													<td ><asp:updatePanel id="UpdatePanel27" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:DropDownList ID="ddlEntremesHora" runat="server" CssClass="textBox" 
                                                                        Visible="True" >
                                                                        <asp:ListItem Value="0">--Seleccione--</asp:ListItem>
                                                                        <asp:ListItem>1</asp:ListItem>
                                                                        <asp:ListItem>2</asp:ListItem>
                                                                        <asp:ListItem>3</asp:ListItem>
                                                                        <asp:ListItem>4</asp:ListItem>
                                                                        <asp:ListItem>5</asp:ListItem>
                                                                        <asp:ListItem>6</asp:ListItem>
                                                                        <asp:ListItem>7</asp:ListItem>
                                                                        <asp:ListItem>8</asp:ListItem>
                                                                        <asp:ListItem>9</asp:ListItem>
                                                                        <asp:ListItem>10</asp:ListItem>
                                                                        <asp:ListItem>11</asp:ListItem>
                                                                        <asp:ListItem>12</asp:ListItem>
                                                                        <asp:ListItem>13</asp:ListItem>
                                                                        <asp:ListItem>14</asp:ListItem>
                                                                        <asp:ListItem>15</asp:ListItem>
                                                                        <asp:ListItem>16</asp:ListItem>
                                                                        <asp:ListItem>17</asp:ListItem>
                                                                        <asp:ListItem>18</asp:ListItem>
                                                                        <asp:ListItem>19</asp:ListItem>
                                                                        <asp:ListItem>20</asp:ListItem>
                                                                        <asp:ListItem>21</asp:ListItem>
                                                                        <asp:ListItem>22</asp:ListItem>
                                                                        <asp:ListItem>23</asp:ListItem>
                                                                        <asp:ListItem>24</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    
                                                                </ContentTemplate>
                                                        </asp:updatePanel><asp:requiredfieldvalidator id="rfvEntremesHora" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="ddlEntremesHora" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator></td>
												    <td colspan="2"><asp:updatePanel id="UpdatePanel5" runat="server">
                                                              <ContentTemplate>
                                                                    Lugar<asp:DropDownList ID="ddlEntremesLugar" runat="server" CssClass="textBox" 
                                                                       Visible="True" >
                                                                   </asp:DropDownList>
                                                               </ContentTemplate>
                                                        </asp:updatePanel>
														<asp:requiredfieldvalidator id="rfvEntremesLugar" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="ddlEntremesLugar" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator></td>
												</tr>
												<tr>
													<td width="19%">Cena</td>
													<td width="10%"><asp:radiobuttonlist id="rblCena" runat="server" Width="112px" RepeatDirection="Horizontal">
															<asp:ListItem Value="1">Si</asp:ListItem>
															<asp:ListItem Value="0">No</asp:ListItem>
														</asp:radiobuttonlist><asp:requiredfieldvalidator id="rfvCena" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="rblCena" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator>
													</td>
													<td style="text-align: right" >Hora</td>
													<td ><asp:updatePanel id="UpdatePanel28" runat="server" >
                                                                <ContentTemplate>
                                                                    <asp:DropDownList ID="ddlCenaHora" runat="server" CssClass="textBox" 
                                                                        Visible="True" >
                                                                        <asp:ListItem Value="0">--Seleccione--</asp:ListItem>
                                                                        <asp:ListItem>1</asp:ListItem>
                                                                        <asp:ListItem>2</asp:ListItem>
                                                                        <asp:ListItem>3</asp:ListItem>
                                                                        <asp:ListItem>4</asp:ListItem>
                                                                        <asp:ListItem>5</asp:ListItem>
                                                                        <asp:ListItem>6</asp:ListItem>
                                                                        <asp:ListItem>7</asp:ListItem>
                                                                        <asp:ListItem>8</asp:ListItem>
                                                                        <asp:ListItem>9</asp:ListItem>
                                                                        <asp:ListItem>10</asp:ListItem>
                                                                        <asp:ListItem>11</asp:ListItem>
                                                                        <asp:ListItem>12</asp:ListItem>
                                                                        <asp:ListItem>13</asp:ListItem>
                                                                        <asp:ListItem>14</asp:ListItem>
                                                                        <asp:ListItem>15</asp:ListItem>
                                                                        <asp:ListItem>16</asp:ListItem>
                                                                        <asp:ListItem>17</asp:ListItem>
                                                                        <asp:ListItem>18</asp:ListItem>
                                                                        <asp:ListItem>19</asp:ListItem>
                                                                        <asp:ListItem>20</asp:ListItem>
                                                                        <asp:ListItem>21</asp:ListItem>
                                                                        <asp:ListItem>22</asp:ListItem>
                                                                        <asp:ListItem>23</asp:ListItem>
                                                                        <asp:ListItem>24</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    
                                                                </ContentTemplate>
                                                        </asp:updatePanel><asp:requiredfieldvalidator id="rfvCenaHora" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="ddlCenaHora" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator></td>
												    <td colspan="2"><asp:updatePanel id="UpdatePanel6" runat="server">
                                                              <ContentTemplate>
                                                                    Lugar<asp:DropDownList ID="ddlCenaLugar" runat="server" CssClass="textBox" 
                                                                       Visible="True" >
                                                                   </asp:DropDownList>
                                                               </ContentTemplate>
                                                        </asp:updatePanel>
														<asp:requiredfieldvalidator id="rfvCenaLugar" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="ddlCenaLugar" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator></td>
												</tr>
												<tr>
													<td colspan="6">Enlista alimentos que son de tu agrado<br />
													    <asp:CheckBoxList ID="cblAlimentosAgrado" runat="server" 
                                                            RepeatDirection="Horizontal" CssClass="tableNoBorderSmall" 
                                                            RepeatColumns="3">
                                                        </asp:CheckBoxList>
                                                    </td>
												</tr>
												<tr>
													<td colspan="6">Enlista alimentos que le disgustan<br />
													    <asp:CheckBoxList ID="cblAlimentosDisguntan" runat="server" 
                                                            RepeatDirection="Horizontal" CssClass="tableNoBorderSmall" 
                                                            RepeatColumns="3">
                                                        </asp:CheckBoxList>
                                                    </td>
												</tr>
												<tr>
													<td colspan="6">¿Reconoces cuando estás satisfecho?<br />	
													
														<asp:updatePanel id="UpdatePanel8" runat="server">
                                                               <ContentTemplate>
                                                                   <asp:DropDownList ID="ddlEstarSatisfecho" runat="server" CssClass="textBox" 
                                                                       Visible="True" >
                                                                       
                                                                   </asp:DropDownList>
                                                               </ContentTemplate>
                                                        </asp:updatePanel>
														<asp:requiredfieldvalidator id="rfvEstarSatisfecho" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="ddlEstarSatisfecho" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator>
													</td>
												</tr>
												<tr>
													<td colspan="6">¿Crees que te satisfaces con facilidad?<br />	
													
														<asp:updatePanel id="UpdatePanel9" runat="server">
                                                               <ContentTemplate>
                                                                   <asp:DropDownList ID="ddlSatisfaccionFacilidad" runat="server" CssClass="textBox" 
                                                                       Visible="True" >
                                                                   </asp:DropDownList>
                                                               </ContentTemplate>
                                                        </asp:updatePanel>
														<asp:requiredfieldvalidator id="rfvSatisfaccionFacilidad" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="ddlSatisfaccionFacilidad" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator>
													</td>
												</tr>
												<tr>
													<td colspan="6">¿Reconoces cuando tienes hambre? <br />	
													
														<asp:updatePanel id="UpdatePanel10" runat="server">
                                                               <ContentTemplate>
                                                                   <asp:DropDownList ID="ddlReconocerHambre" runat="server" CssClass="textBox" 
                                                                       Visible="True" >
                                                                   </asp:DropDownList>
                                                               </ContentTemplate>
                                                        </asp:updatePanel>
														<asp:requiredfieldvalidator id="rfvReconocerHambre" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="ddlReconocerHambre" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator>
													</td>
												</tr>
												<tr>
													<td colspan="6">¿Acostumbras comer despacio?<br />	
													
														<asp:updatePanel id="UpdatePanel11" runat="server">
                                                               <ContentTemplate>
                                                                   <asp:DropDownList ID="ddlComerDespacio" runat="server" CssClass="textBox" 
                                                                       Visible="True" >
                                                                   </asp:DropDownList>
                                                               </ContentTemplate>
                                                        </asp:updatePanel>
														<asp:requiredfieldvalidator id="rfvComerDespacio" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="ddlComerDespacio" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator>
													</td>
												</tr>
												<tr>
													<td colspan="6">¿A qué hora del día sientes mayor apetito?<br />	
													
														<asp:updatePanel id="UpdatePanel12" runat="server">
                                                               <ContentTemplate>
                                                                   <asp:DropDownList ID="ddlMayorApetitoHora" runat="server" CssClass="textBox" 
                                                                       Visible="True" >
                                                                       <asp:ListItem Value="0">--Seleccione--</asp:ListItem>
                                                                        <asp:ListItem>1</asp:ListItem>
                                                                        <asp:ListItem>2</asp:ListItem>
                                                                        <asp:ListItem>3</asp:ListItem>
                                                                        <asp:ListItem>4</asp:ListItem>
                                                                        <asp:ListItem>5</asp:ListItem>
                                                                        <asp:ListItem>6</asp:ListItem>
                                                                        <asp:ListItem>7</asp:ListItem>
                                                                        <asp:ListItem>8</asp:ListItem>
                                                                        <asp:ListItem>9</asp:ListItem>
                                                                        <asp:ListItem>10</asp:ListItem>
                                                                        <asp:ListItem>11</asp:ListItem>
                                                                        <asp:ListItem>12</asp:ListItem>
                                                                        <asp:ListItem>13</asp:ListItem>
                                                                        <asp:ListItem>14</asp:ListItem>
                                                                        <asp:ListItem>15</asp:ListItem>
                                                                        <asp:ListItem>16</asp:ListItem>
                                                                        <asp:ListItem>17</asp:ListItem>
                                                                        <asp:ListItem>18</asp:ListItem>
                                                                        <asp:ListItem>19</asp:ListItem>
                                                                        <asp:ListItem>20</asp:ListItem>
                                                                        <asp:ListItem>21</asp:ListItem>
                                                                        <asp:ListItem>22</asp:ListItem>
                                                                        <asp:ListItem>23</asp:ListItem>
                                                                        <asp:ListItem>24</asp:ListItem>
                                                                   </asp:DropDownList>
                                                               </ContentTemplate>
                                                        </asp:updatePanel>
														<asp:requiredfieldvalidator id="rfvMayorApetitoHora" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="ddlMayorApetitoHora" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator>
													</td>
												</tr>
												<tr>
													<td colspan="6">¿A qué hora del día sientes antojos?<br />	
													
														<asp:updatePanel id="UpdatePanel13" runat="server">
                                                               <ContentTemplate>
                                                                   <asp:DropDownList ID="ddlAntojosHora" runat="server" CssClass="textBox" 
                                                                       Visible="True" >
                                                                       <asp:ListItem Value="0">--Seleccione--</asp:ListItem>
                                                                        <asp:ListItem>1</asp:ListItem>
                                                                        <asp:ListItem>2</asp:ListItem>
                                                                        <asp:ListItem>3</asp:ListItem>
                                                                        <asp:ListItem>4</asp:ListItem>
                                                                        <asp:ListItem>5</asp:ListItem>
                                                                        <asp:ListItem>6</asp:ListItem>
                                                                        <asp:ListItem>7</asp:ListItem>
                                                                        <asp:ListItem>8</asp:ListItem>
                                                                        <asp:ListItem>9</asp:ListItem>
                                                                        <asp:ListItem>10</asp:ListItem>
                                                                        <asp:ListItem>11</asp:ListItem>
                                                                        <asp:ListItem>12</asp:ListItem>
                                                                        <asp:ListItem>13</asp:ListItem>
                                                                        <asp:ListItem>14</asp:ListItem>
                                                                        <asp:ListItem>15</asp:ListItem>
                                                                        <asp:ListItem>16</asp:ListItem>
                                                                        <asp:ListItem>17</asp:ListItem>
                                                                        <asp:ListItem>18</asp:ListItem>
                                                                        <asp:ListItem>19</asp:ListItem>
                                                                        <asp:ListItem>20</asp:ListItem>
                                                                        <asp:ListItem>21</asp:ListItem>
                                                                        <asp:ListItem>22</asp:ListItem>
                                                                        <asp:ListItem>23</asp:ListItem>
                                                                        <asp:ListItem>24</asp:ListItem>
                                                                   </asp:DropDownList>
                                                               </ContentTemplate>
                                                        </asp:updatePanel>
														<asp:requiredfieldvalidator id="rfvAntojosHora" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="ddlAntojosHora" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator>
													</td>
												</tr>
												<tr>
													<td colspan="6">¿En el último año te has sometido a alguna dieta?<br />
													<asp:radiobuttonlist id="rblSometidoDieta" runat="server" Width="112px" RepeatDirection="Horizontal">
															<asp:ListItem Value="1">Si</asp:ListItem>
															<asp:ListItem Value="0">No</asp:ListItem>
														</asp:radiobuttonlist><asp:requiredfieldvalidator id="rfvSometidoDieta" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="rblSometidoDieta" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator>
													</td>
												</tr>
												<tr>
													<td colspan="6">¿Actualmente llevas a cabo alguna dieta?<br />
													<asp:radiobuttonlist id="rblLlevasDieta" runat="server" Width="112px" RepeatDirection="Horizontal">
															<asp:ListItem Value="1">Si</asp:ListItem>
															<asp:ListItem Value="0">No</asp:ListItem>
														</asp:radiobuttonlist><asp:requiredfieldvalidator id="rfvLlevasDieta" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="rblLlevasDieta" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator>
													</td>
												</tr>
												<tr>
													<td colspan="6">¿Por quién fue prescrita?<br />	
													
														<asp:updatePanel id="UpdatePanel14" runat="server">
                                                               <ContentTemplate>
                                                                   <asp:DropDownList ID="ddlQuienPrescribe" runat="server" CssClass="textBox" 
                                                                       Visible="True" >
                                                                   </asp:DropDownList>
                                                               </ContentTemplate>
                                                        </asp:updatePanel>
														<asp:requiredfieldvalidator id="rfvQuienPrescribe" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="ddlQuienPrescribe" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator>
													</td>
												</tr>
												<tr>
													<td colspan="6">¿Cuál fue la principal razón que te motivó a iniciar una dieta?<br />	
													
														<asp:updatePanel id="UpdatePanel15" runat="server">
                                                               <ContentTemplate>
                                                                   <asp:DropDownList ID="ddlMotivoIniciarDieta" runat="server" CssClass="textBox" 
                                                                       Visible="True" >
                                                                   </asp:DropDownList>
                                                               </ContentTemplate>
                                                        </asp:updatePanel>
														<asp:requiredfieldvalidator id="rfvMotivoIniciarDieta" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="ddlMotivoIniciarDieta" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator>
													</td>
												</tr>
												<tr>
													<td colspan="6">Comparando con la dieta que llevabas , ¿cómo consideras que es la ingestión actual de tus alimentos?<br />	
													
														<asp:updatePanel id="UpdatePanel16" runat="server">
                                                               <ContentTemplate>
                                                                   <asp:DropDownList ID="ddlIngestionAlimentos" runat="server" CssClass="textBox" 
                                                                       Visible="True" >
                                                                   </asp:DropDownList>
                                                               </ContentTemplate>
                                                        </asp:updatePanel>
														<asp:requiredfieldvalidator id="rfvIngestionAlimentos" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="ddlIngestionAlimentos" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator>
													</td>
												</tr>
												<tr>
													<td colspan="6">¿En caso de consumir algún complemento para bajar de peso, por quién fue prescrito?
													
													    <asp:updatePanel id="UpdatePanel17" runat="server">
                                                               <ContentTemplate>
                                                                   <asp:DropDownList ID="ddlBajarPesoPrescrito" runat="server" CssClass="textBox" 
                                                                       Visible="True" >
                                                                   </asp:DropDownList>
                                                               </ContentTemplate>
                                                        </asp:updatePanel>
														<asp:requiredfieldvalidator id="rfvBajarPesoPrescrito" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="ddlBajarPesoPrescrito" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator>
													</td>
												</tr>
												<tr>
													<td >Especificar</td>
													<td colspan="5"><asp:textbox id="txtBajarPesoPrescritoEspecificacion" CssClass="textBox" runat="server" onkeypress='return (this.value.length < 300);' 
                                                            MaxLength="300"
						                                    TextMode="MultiLine" Height="40px" Width="620px"></asp:textbox><br /><asp:requiredfieldvalidator id="rfvBajarPesoPrescritoEspecificacion" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="txtBajarPesoPrescritoEspecificacion" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator>
                                                        </td>
												</tr>
									</table>			
									<table class="tableBorder" id="Table1" cellSpacing="0" cellPadding="3" width="100%" align="center">
												<tr>
													<td class="headerTable" colSpan="6">ANTECEDENTES DEL TRANSTORNO DE ALIMENTACIÓN&nbsp;
														</td>
												</tr>
												
												<tr>
													<td colspan="6">¿Has padecido de algún trastorno de alimentación?<br />
													<asp:radiobuttonlist id="rblTrastornoAlimentacion" runat="server" Width="112px" RepeatDirection="Horizontal">
															<asp:ListItem Value="1">Si</asp:ListItem>
															<asp:ListItem Value="0">No</asp:ListItem>
														</asp:radiobuttonlist><asp:requiredfieldvalidator id="rfvTrastornoAlimentacion" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="rblTrastornoAlimentacion" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator>
													</td>
												</tr>
												<tr>
													<td colspan="6">¿Qué trastorno padeciste?<br />
													
                                                        <asp:updatePanel id="UpdatePanel19" runat="server">
                                                             <ContentTemplate>
                                                              <table id="Table16" cellSpacing="0" cellPadding="3" width="100%" align=center>
                                                               <tr>
                                                                    <td>
                                                                      <uc2:WC_AdicionarDiagnosticoConsultaCIE10 ID="WC_AdicionarDiagnosticoConsultaCIE1" 
                                                                          runat="server" />
                                                                    </td>
                                                               </tr>
                                                              </table>
						                                     </ContentTemplate>
						                                 </asp:updatePanel>
                                                    </td>
														
												</tr>
												<tr>
													<td colspan="6">¿Hace cuánto tiempo lo padeciste?
													
													    <asp:updatePanel id="UpdatePanel20" runat="server">
                                                               <ContentTemplate>
                                                                   <asp:DropDownList ID="ddlPadecerTrastorno" runat="server" CssClass="textBox" 
                                                                       Visible="True" >
                                                                   </asp:DropDownList>
                                                               </ContentTemplate>
                                                        </asp:updatePanel>
														<asp:requiredfieldvalidator id="rfvPadecerTrastorno" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="ddlPadecerTrastorno" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator>
													</td>
												</tr>
										</table>			
									    <table class="tableBorder" id="Table2" cellSpacing="0" cellPadding="3" width="100%" align="center">
												<tr>
													<td class="headerTable" colSpan="6">MEDIDAS ANTROPOMÉTRICAS&nbsp;
														</td>
												</tr>
												
												<tr>
													<td width="18%">Diámetro de la cintura</td>
													<td width="15%">
                                                        <asp:textbox onkeypress="return currencyFormat(this,event,true,true)" onkeydown="return keyDown(this,event)"
															id="txtDiametroCintura"  runat="server" CssClass="textBox" Width="70px"></asp:textbox>
                                                        cms<br /><asp:requiredfieldvalidator id="rfvDiametroCintura" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="txtDiametroCintura" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator></td>
													<td width="19%">Diámetro de la cadera</td>
													<td width="15%">
                                                        <asp:textbox onkeypress="return currencyFormat(this,event,true,true)" onkeydown="return keyDown(this,event)"
															id="txtDiametroCadera"  runat="server" CssClass="textBox" Width="70px"  onmouseout="javascript:CalcularCinturaCadera()"></asp:textbox>
                                                        cms<br /><asp:requiredfieldvalidator id="rfvDiametroCadera" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="txtDiametroCadera" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator></td>
														<td width="18%">Relación Cintura Cadera</td>
													<td width="15%">
                                                        <asp:textbox onkeypress="return currencyFormat(this,event,true,true)" onkeydown="return keyDown(this,event)"
															id="txtRelacionCinturaCadera"  runat="server" CssClass="LabelNoModify" Width="70px" Enabled="false"></asp:textbox><br />
															<asp:requiredfieldvalidator id="rfvRelacionCinturaCadera" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="txtRelacionCinturaCadera" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator>
                                                        <br />
                                                        <asp:radiobuttonlist id="rblDescripcionRelacion" runat="server" Width="112px" RepeatDirection="Horizontal">
															<asp:ListItem Value="1">Con Riesgo</asp:ListItem>
															<asp:ListItem Value="0">Sin Riesgo</asp:ListItem>
														</asp:radiobuttonlist><asp:requiredfieldvalidator id="rfvDescripcionRelacion" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="rblDescripcionRelacion" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator></td>
													
												</tr>
												<tr>
													<td width="18%">Masa grasa</td>
													<td width="15%"> <asp:textbox onkeypress="return currencyFormat(this,event,true,true)" onkeydown="return keyDown(this,event)"
															id="txtMasaGrasa"  runat="server" CssClass="textBox" Width="70px"></asp:textbox>
                                                        Kg<br /><asp:requiredfieldvalidator id="rfvMasaGrasa" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="txtMasaGrasa" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator></td>
													<td width="19%">Masa magra</td>
													<td width="15%"><asp:textbox onkeypress="return currencyFormat(this,event,true,true)" onkeydown="return keyDown(this,event)"
															id="txtMasaGrama"  runat="server" CssClass="textBox" Width="70px"></asp:textbox>
                                                        Kg<br /><asp:requiredfieldvalidator id="rfvMasaGrama" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="txtMasaGrama" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator>
                                                       </td>
														<td width="18%">Peso recomendable</td>
													<td width="15%"><asp:textbox onkeypress="return currencyFormat(this,event,true,true)" onkeydown="return keyDown(this,event)"
															id="txtPesoRecomendable"  runat="server" CssClass="textBox" Width="70px"></asp:textbox>
                                                        Kg<br /><asp:requiredfieldvalidator id="rfvPesoRecomendable" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="txtPesoRecomendable" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator>
                                                        </td>
													
												</tr>
												<tr>
													<td width="18%">Excedente de grasa</td>
													<td width="15%"><asp:textbox onkeypress="return currencyFormat(this,event,true,true)" onkeydown="return keyDown(this,event)"
															id="txtExcedenteGrasa"  runat="server" CssClass="textBox" Width="70px"></asp:textbox>
                                                        Kg<br /><asp:requiredfieldvalidator id="rfvExcedenteGrasa" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="txtExcedenteGrasa" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator></td>
														<td width="18%"></td>
													<td width="19%"></td>
													<td width="15%">
                                                        </td>
													<td width="15%"></td>
													
												</tr>
												<tr>
													<td width="18%">Diagnóstico nutricional</td>
													<td colspan="5" style="width: 63%">
                                                        <asp:updatePanel id="UpdatePanel18" runat="server">
                                                             <ContentTemplate>
                                                              <table id=Table13 cellSpacing=0 cellPadding=3 width="100%" align=center>
                                                               <tr>
                                                                    <TD>
                                                                    <asp:DropDownList ID="ddlDiagnosticoNutricional" runat="server" CssClass="textBox" 
                                                                        Visible="True" >
                                                                       
                                                                    </asp:DropDownList><br />
                                                                      <uc2:WC_AdicionarDiagnosticoConsultaCIE10 ID="WC_AdicionarDiagnosticoConsultaCIE101" 
                                                                          runat="server" />
                                                                    </td>
                                                               </tr>
                                                              </table>
						                                     </ContentTemplate>
						                                 </asp:updatePanel>
                                                        </td>
														
													
												</tr>
												<tr>
													<td width="18%">Recomendaciones nutricionales</td>
													<td colspan="5" style="width: 63%">
                                                        <asp:textbox id="txtRecomendacionesNutricionales" CssClass="textBox" runat="server" onkeypress='return (this.value.length < 500);' 
                                                            MaxLength="500"
						                                    TextMode="MultiLine" Height="40px" Width="620px"></asp:textbox><br /><asp:requiredfieldvalidator id="trfvRecomendacionesNutricionales" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="txtRecomendacionesNutricionales" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator>
                                                        </td>
														
													
												</tr>
												<tr>
													<td colspan="6">Selecciona la afirmación que mejor describa tus planes para tener una alimentación saludable.<br />
													    <asp:RadioButtonList ID="rblAlimentacionSaludable" runat="server"
                                                            RepeatDirection="Vertical" CssClass="tableNoBorderSmall">
                                                        </asp:RadioButtonList>
                                                       <asp:requiredfieldvalidator id="rfvAlimentacionSaludable" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="rblAlimentacionSaludable" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator>
											    </td>
													
												</tr>
										</table>			
									    <table class="tableBorder" id="Table4" cellSpacing="0" cellPadding="3" width="100%" align="center">
												<tr>
													<td class="headerTable" colSpan="6">RUTINA DIARIA&nbsp;
														</td>
												</tr>
												
												<tr>
													<td colspan="2" >¿A qué hora acostumbra levantarse?</td>
													
													<td style="text-align: right" >Entre semana</td>
													<td >		
															<asp:updatePanel id="UpdatePanel21" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:DropDownList ID="ddlLevantarseEntreSemana" runat="server" CssClass="textBox" 
                                                                        Visible="True" >
                                                                        <asp:ListItem Value="0">--Seleccione--</asp:ListItem>
                                                                        <asp:ListItem>1</asp:ListItem>
                                                                        <asp:ListItem>2</asp:ListItem>
                                                                        <asp:ListItem>3</asp:ListItem>
                                                                        <asp:ListItem>4</asp:ListItem>
                                                                        <asp:ListItem>5</asp:ListItem>
                                                                        <asp:ListItem>6</asp:ListItem>
                                                                        <asp:ListItem>7</asp:ListItem>
                                                                        <asp:ListItem>8</asp:ListItem>
                                                                        <asp:ListItem>9</asp:ListItem>
                                                                        <asp:ListItem>10</asp:ListItem>
                                                                        <asp:ListItem>11</asp:ListItem>
                                                                        <asp:ListItem>12</asp:ListItem>
                                                                        <asp:ListItem>13</asp:ListItem>
                                                                        <asp:ListItem>14</asp:ListItem>
                                                                        <asp:ListItem>15</asp:ListItem>
                                                                        <asp:ListItem>16</asp:ListItem>
                                                                        <asp:ListItem>17</asp:ListItem>
                                                                        <asp:ListItem>18</asp:ListItem>
                                                                        <asp:ListItem>19</asp:ListItem>
                                                                        <asp:ListItem>20</asp:ListItem>
                                                                        <asp:ListItem>21</asp:ListItem>
                                                                        <asp:ListItem>22</asp:ListItem>
                                                                        <asp:ListItem>23</asp:ListItem>
                                                                        <asp:ListItem>24</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    
                                                                </ContentTemplate>
                                                        </asp:updatePanel><asp:requiredfieldvalidator id="rfvLevantarseEntreSemana" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="ddlLevantarseEntreSemana" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator></td>
													<td colspan="2">
													    <asp:updatePanel id="UpdatePanel22" runat="server">
                                                              <ContentTemplate>
                                                                    Fin de semana<asp:DropDownList ID="ddlLevantarseFinDeSemana" runat="server" CssClass="textBox" 
                                                                       Visible="True" >
                                                                       <asp:ListItem Value="0">--Seleccione--</asp:ListItem>
                                                                        <asp:ListItem>1</asp:ListItem>
                                                                        <asp:ListItem>2</asp:ListItem>
                                                                        <asp:ListItem>3</asp:ListItem>
                                                                        <asp:ListItem>4</asp:ListItem>
                                                                        <asp:ListItem>5</asp:ListItem>
                                                                        <asp:ListItem>6</asp:ListItem>
                                                                        <asp:ListItem>7</asp:ListItem>
                                                                        <asp:ListItem>8</asp:ListItem>
                                                                        <asp:ListItem>9</asp:ListItem>
                                                                        <asp:ListItem>10</asp:ListItem>
                                                                        <asp:ListItem>11</asp:ListItem>
                                                                        <asp:ListItem>12</asp:ListItem>
                                                                        <asp:ListItem>13</asp:ListItem>
                                                                        <asp:ListItem>14</asp:ListItem>
                                                                        <asp:ListItem>15</asp:ListItem>
                                                                        <asp:ListItem>16</asp:ListItem>
                                                                        <asp:ListItem>17</asp:ListItem>
                                                                        <asp:ListItem>18</asp:ListItem>
                                                                        <asp:ListItem>19</asp:ListItem>
                                                                        <asp:ListItem>20</asp:ListItem>
                                                                        <asp:ListItem>21</asp:ListItem>
                                                                        <asp:ListItem>22</asp:ListItem>
                                                                        <asp:ListItem>23</asp:ListItem>
                                                                        <asp:ListItem>24</asp:ListItem>
                                                                   </asp:DropDownList>
                                                               </ContentTemplate>
                                                        </asp:updatePanel>
														<asp:requiredfieldvalidator id="rfvLevantarseFinDeSemana" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="ddlLevantarseFinDeSemana" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator></td>
												</tr>
												<tr>	
													<td colspan="2" >¿A qué hora acostumbra salir de casa?</td>
													
													<td style="text-align: right" >Entre semana</td>
													<td ><asp:updatePanel id="UpdatePanel23" runat="server">
                                                                <ContentTemplate>
                                                                   <asp:DropDownList ID="ddlSalirCasaEntreSemana" runat="server" CssClass="textBox" 
                                                                        Visible="True" >
                                                                        <asp:ListItem Value="0">--Seleccione--</asp:ListItem>
                                                                        <asp:ListItem>1</asp:ListItem>
                                                                        <asp:ListItem>2</asp:ListItem>
                                                                        <asp:ListItem>3</asp:ListItem>
                                                                        <asp:ListItem>4</asp:ListItem>
                                                                        <asp:ListItem>5</asp:ListItem>
                                                                        <asp:ListItem>6</asp:ListItem>
                                                                        <asp:ListItem>7</asp:ListItem>
                                                                        <asp:ListItem>8</asp:ListItem>
                                                                        <asp:ListItem>9</asp:ListItem>
                                                                        <asp:ListItem>10</asp:ListItem>
                                                                        <asp:ListItem>11</asp:ListItem>
                                                                        <asp:ListItem>12</asp:ListItem>
                                                                        <asp:ListItem>13</asp:ListItem>
                                                                        <asp:ListItem>14</asp:ListItem>
                                                                        <asp:ListItem>15</asp:ListItem>
                                                                        <asp:ListItem>16</asp:ListItem>
                                                                        <asp:ListItem>17</asp:ListItem>
                                                                        <asp:ListItem>18</asp:ListItem>
                                                                        <asp:ListItem>19</asp:ListItem>
                                                                        <asp:ListItem>20</asp:ListItem>
                                                                        <asp:ListItem>21</asp:ListItem>
                                                                        <asp:ListItem>22</asp:ListItem>
                                                                        <asp:ListItem>23</asp:ListItem>
                                                                        <asp:ListItem>24</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    
                                                                </ContentTemplate>
                                                        </asp:updatePanel><asp:requiredfieldvalidator id="rfvSalirCasaEntreSemana" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="ddlSalirCasaEntreSemana" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator></td>
													<td colspan="2"><asp:updatePanel id="UpdatePanel29" runat="server">
                                                              <ContentTemplate>
                                                                    Fin de semana<asp:DropDownList ID="ddlSalirCasaFinDeSemana" runat="server" CssClass="textBox" 
                                                                       Visible="True" >
                                                                       <asp:ListItem Value="0">--Seleccione--</asp:ListItem>
                                                                        <asp:ListItem>1</asp:ListItem>
                                                                        <asp:ListItem>2</asp:ListItem>
                                                                        <asp:ListItem>3</asp:ListItem>
                                                                        <asp:ListItem>4</asp:ListItem>
                                                                        <asp:ListItem>5</asp:ListItem>
                                                                        <asp:ListItem>6</asp:ListItem>
                                                                        <asp:ListItem>7</asp:ListItem>
                                                                        <asp:ListItem>8</asp:ListItem>
                                                                        <asp:ListItem>9</asp:ListItem>
                                                                        <asp:ListItem>10</asp:ListItem>
                                                                        <asp:ListItem>11</asp:ListItem>
                                                                        <asp:ListItem>12</asp:ListItem>
                                                                        <asp:ListItem>13</asp:ListItem>
                                                                        <asp:ListItem>14</asp:ListItem>
                                                                        <asp:ListItem>15</asp:ListItem>
                                                                        <asp:ListItem>16</asp:ListItem>
                                                                        <asp:ListItem>17</asp:ListItem>
                                                                        <asp:ListItem>18</asp:ListItem>
                                                                        <asp:ListItem>19</asp:ListItem>
                                                                        <asp:ListItem>20</asp:ListItem>
                                                                        <asp:ListItem>21</asp:ListItem>
                                                                        <asp:ListItem>22</asp:ListItem>
                                                                        <asp:ListItem>23</asp:ListItem>
                                                                        <asp:ListItem>24</asp:ListItem>
                                                                   </asp:DropDownList>
                                                               </ContentTemplate>
                                                        </asp:updatePanel>
														<asp:requiredfieldvalidator id="rfvSalirCasaFinDeSemana" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="ddlSalirCasaFinDeSemana" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator></td>
												</tr>
												<tr>
													<td colspan="2">¿A qué hora acostumbra acostarse?</td>
													
													<td style="text-align: right" >Entre semana</td>
													<td ><asp:updatePanel id="UpdatePanel30" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:DropDownList ID="ddlAcostarseEntreSemana" runat="server" CssClass="textBox" 
                                                                        Visible="True" >
                                                                        <asp:ListItem Value="0">--Seleccione--</asp:ListItem>
                                                                        <asp:ListItem>1</asp:ListItem>
                                                                        <asp:ListItem>2</asp:ListItem>
                                                                        <asp:ListItem>3</asp:ListItem>
                                                                        <asp:ListItem>4</asp:ListItem>
                                                                        <asp:ListItem>5</asp:ListItem>
                                                                        <asp:ListItem>6</asp:ListItem>
                                                                        <asp:ListItem>7</asp:ListItem>
                                                                        <asp:ListItem>8</asp:ListItem>
                                                                        <asp:ListItem>9</asp:ListItem>
                                                                        <asp:ListItem>10</asp:ListItem>
                                                                        <asp:ListItem>11</asp:ListItem>
                                                                        <asp:ListItem>12</asp:ListItem>
                                                                        <asp:ListItem>13</asp:ListItem>
                                                                        <asp:ListItem>14</asp:ListItem>
                                                                        <asp:ListItem>15</asp:ListItem>
                                                                        <asp:ListItem>16</asp:ListItem>
                                                                        <asp:ListItem>17</asp:ListItem>
                                                                        <asp:ListItem>18</asp:ListItem>
                                                                        <asp:ListItem>19</asp:ListItem>
                                                                        <asp:ListItem>20</asp:ListItem>
                                                                        <asp:ListItem>21</asp:ListItem>
                                                                        <asp:ListItem>22</asp:ListItem>
                                                                        <asp:ListItem>23</asp:ListItem>
                                                                        <asp:ListItem>24</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    
                                                                </ContentTemplate>
                                                        </asp:updatePanel><asp:requiredfieldvalidator id="rfvAcostarseEntreSemana" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="ddlAcostarseEntreSemana" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator></td>
													<td colspan="2"><asp:updatePanel id="UpdatePanel31" runat="server">
                                                              <ContentTemplate>
                                                                    Fin de semana<asp:DropDownList ID="ddlAcostarseFinDeSemana" runat="server" CssClass="textBox" 
                                                                       Visible="True" >
                                                                       <asp:ListItem Value="0">--Seleccione--</asp:ListItem>
                                                                        <asp:ListItem>1</asp:ListItem>
                                                                        <asp:ListItem>2</asp:ListItem>
                                                                        <asp:ListItem>3</asp:ListItem>
                                                                        <asp:ListItem>4</asp:ListItem>
                                                                        <asp:ListItem>5</asp:ListItem>
                                                                        <asp:ListItem>6</asp:ListItem>
                                                                        <asp:ListItem>7</asp:ListItem>
                                                                        <asp:ListItem>8</asp:ListItem>
                                                                        <asp:ListItem>9</asp:ListItem>
                                                                        <asp:ListItem>10</asp:ListItem>
                                                                        <asp:ListItem>11</asp:ListItem>
                                                                        <asp:ListItem>12</asp:ListItem>
                                                                        <asp:ListItem>13</asp:ListItem>
                                                                        <asp:ListItem>14</asp:ListItem>
                                                                        <asp:ListItem>15</asp:ListItem>
                                                                        <asp:ListItem>16</asp:ListItem>
                                                                        <asp:ListItem>17</asp:ListItem>
                                                                        <asp:ListItem>18</asp:ListItem>
                                                                        <asp:ListItem>19</asp:ListItem>
                                                                        <asp:ListItem>20</asp:ListItem>
                                                                        <asp:ListItem>21</asp:ListItem>
                                                                        <asp:ListItem>22</asp:ListItem>
                                                                        <asp:ListItem>23</asp:ListItem>
                                                                        <asp:ListItem>24</asp:ListItem>  
                                                                   </asp:DropDownList>
                                                               </ContentTemplate>
                                                        </asp:updatePanel>
														<asp:requiredfieldvalidator id="rfvAcostarseFinDeSemana" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="ddlAcostarseFinDeSemana" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator></td>
												</tr>
												<tr>
													<td colspan="6">En promedio, ¿Qué tan frecuentemente compras comida rápida?
													
													    <asp:updatePanel id="UpdatePanel32" runat="server">
                                                               <ContentTemplate>
                                                                   <asp:DropDownList ID="ddlComidaRapida" runat="server" CssClass="textBox" 
                                                                       Visible="True" >
                                                                   </asp:DropDownList>
                                                               </ContentTemplate>
                                                        </asp:updatePanel>
														<asp:requiredfieldvalidator id="rfvComidaRapida" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="ddlComidaRapida" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator>
													</td>
												</tr>
												<tr>
													<td colspan="6">Aproximadamente ¿Cuántos vasos de agua consumes al día?
													
													    <asp:updatePanel id="UpdatePanel33" runat="server">
                                                               <ContentTemplate>
                                                                   <asp:DropDownList ID="ddlVasosAgua" runat="server" CssClass="textBox" 
                                                                       Visible="True" >
                                                                   </asp:DropDownList>
                                                               </ContentTemplate>
                                                        </asp:updatePanel>
														<asp:requiredfieldvalidator id="rfvVasosAgua" runat="server" CssClass="textRed" ForeColor=" " Display="Dynamic"
															ControlToValidate="ddlVasosAgua" ErrorMessage="Requerido" Enabled="False"></asp:requiredfieldvalidator>
													</td>
												</tr>
											</table>
											</div>
										</td>
									</tr>
									
			
			<tr>
				<TD align="center" colSpan="2"><asp:button id="btnAnterior" runat="server" 
                        CssClass="button" Text="« Anterior" CausesValidation="False" 
                        onclick="btnAnterior_Click1"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:button id="btnGuardar" runat="server" 
                        CssClass="button" Text="Siguiente »" ></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
					<asp:button id="btnCancelar" runat="server" CausesValidation="false" CssClass="button" Text="Cancelar"></asp:button>&nbsp;</td>
			</tr>
			</TBODY></table>
			<P><BR>
				<uc1:wc_buscarprestador id="WC_BuscarPrestador1" runat="server"></uc1:wc_buscarprestador><BR>
				<uc1:wc_buscardiagnostico id="WC_BuscarDiagnostico1" runat="server"></uc1:wc_buscardiagnostico><BR>
				<uc1:wc_buscarproveedor id="WC_BuscarProveedor1" runat="server"></uc1:wc_buscarproveedor></P>
			<P><BR>
				&nbsp;</P>
		</form>
	</body>
</html>
