<%@ Page language="c#" Codebehind="FO_ConstanciaPlanMedico.aspx.cs" AutoEventWireup="false" Inherits="TPA.interfaz_empleado.forma.FO_ConstanciaPlanMedico" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
		<title>HC-Historias Clínicas</title>
<meta content="Microsoft Visual Studio .NET 7.1" name=GENERATOR>
<meta content=C# name=CODE_LANGUAGE>
<meta content=JavaScript name=vs_defaultClientScript>
<meta content=http://schemas.microsoft.com/intellisense/ie5 name=vs_targetSchema>
<STYLE type=text/css>.gridFormatoSmall { BORDER-RIGHT: #000000 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; PADDING-BOTTOM: 2px; BORDER-LEFT: #000000 1px solid; PADDING-TOP: 2px; BORDER-BOTTOM: #000000 1px solid; cellpadding: 1px }
	.norItemsFormatoSmall { BORDER-RIGHT: #000000 1px solid; PADDING-RIGHT: 2px; BORDER-TOP: #000000 1px solid; PADDING-LEFT: 2px; FONT-SIZE: 8pt; PADDING-BOTTOM: 2px; BORDER-LEFT: #000000 1px solid; COLOR: #000000; PADDING-TOP: 2px; BORDER-BOTTOM: #000000 1px solid; FONT-FAMILY: Times New Roman , Arial, Verdana, Helvetica, sans-serif; BORDER-COLLAPSE: collapse }
	.headerGridFormatoSmall { BORDER-RIGHT: #000000 1px solid; PADDING-RIGHT: 3px; BORDER-TOP: #000000 1px solid; PADDING-LEFT: 3px; FONT-WEIGHT: bold; FONT-SIZE: 8pt; PADDING-BOTTOM: 3px; BORDER-LEFT: #000000 1px solid; COLOR: #000000; PADDING-TOP: 3px; BORDER-BOTTOM: #000000 1px solid; FONT-FAMILY: Times New Roman , Arial, Verdana, Helvetica, sans-serif; BORDER-COLLAPSE: collapse; TEXT-ALIGN: center }
	.tableCarta { BORDER-RIGHT: medium none; PADDING-RIGHT: 0px; BORDER-TOP: medium none; FONT-SIZE: 12pt; BORDER-LEFT: medium none; COLOR: #000000; BORDER-BOTTOM: medium none; FONT-FAMILY: Times New Roman , Arial, Verdana, Helvetica, sans-serif }
	.textJustify { FONT-SIZE: 12pt; BORDER-LEFT: medium none; COLOR: #000000; FONT-FAMILY: Times New Roman , Arial, Verdana, Helvetica, sans-serif; TEXT-ALIGN: justify }
	</STYLE>
</HEAD>
<body 
style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none" 
bottomMargin=0 MS_POSITIONING="GridLayout">
<form id=Form1 
style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none" 
method=post runat="server">
<TABLE class=tableFormato id=Table1 
style="BORDER-TOP-STYLE: none; BORDER-RIGHT-STYLE: none; BORDER-LEFT-STYLE: none; BORDER-BOTTOM-STYLE: none" 
cellSpacing=0 cellPadding=0 width=600 border=0>
  <TR>
    <TD>
      <TABLE class=tableCarta id=Table2 cellSpacing=0 cellPadding=10 
      width="100%" border=0>
        <TR>
          <TD>
            <P><BR>Bogotá,&nbsp; <asp:label id=lblFecha runat="server"></asp:label><BR 
            ><BR><BR 
            >SEÑOR(A): <BR><asp:label id=lblNombre runat="server"></asp:label><BR 
            >Empleado BP Exploration Company <BR 
            >Ciudad </P>
            <P align=center><STRONG 
            >REF: Constancia de Ingreso al Beneficio de Salud de la 
            Empresa</STRONG> <BR></P>
            <P>Fecha de Inclusión: <asp:label id=lblFechaIngreso runat="server"></asp:label><BR 
            ><BR>A partir de la fecha 
            de inclusión, usted y/o los siguientes beneficiarios ingresan al 
            Beneficio de Salud de BP Colombia según información relacionada a 
            continuación:<BR><BR 
          ></P></TD></TR></TABLE></TD></TR>
  <TR>
    <TD><asp:datagrid id=dtgDetalle runat="server" GridLines="Horizontal" AutoGenerateColumns="False" AllowPaging="False" CellPadding="0" Width="100%" CssClass="gridFormatoSmall">
							<AlternatingItemStyle CssClass="norItemsFormatoSmall"></AlternatingItemStyle>
							<ItemStyle CssClass="norItemsFormatoSmall"></ItemStyle>
							<HeaderStyle CssClass="headerGridFormatoSmall"></HeaderStyle>
							<Columns>
								<asp:BoundColumn Visible="False" DataField="beneficiario_id"></asp:BoundColumn>
								<asp:BoundColumn DataField="nombre" HeaderText="Nombre" ItemStyle-Width="40%"></asp:BoundColumn>
								<asp:BoundColumn DataField="Parentesco" HeaderText="Parentesco" ItemStyle-Width="20%"></asp:BoundColumn>
								<asp:BoundColumn DataField="Preexistencias" HeaderText="Preexistencias" ItemStyle-Width="40%"></asp:BoundColumn>
							</Columns>
						</asp:datagrid></TD></TR>
  <TR>
    <TD>
     
            <P class="textJustify"><BR>De 
            conformidad con la información consignada en la solicitud de 
            afiliación, la declaración del estado de salud, los exámenes médicos 
            de ingreso (si se realizaron), las historias clínicas y demás 
            documentos incluidos con ocasión de la utilización de los servicios 
            médicos y paramédicos, le confirmamos que las anteriores 
            preexistencias así como sus secuelas, estudios, y/o complicaciones, 
            quedarán excluidas de la prestación de servicios dentro del Beneficio de Salud. </P>
            <P class="textJustify">Se considera preexistencia 
            toda enfermedad, malformación o afección que se pueda demostrar 
            existía a la fecha de iniciación de la vinculación al Beneficio, 
            declarada o no. En virtud de lo anterior, si se identifican 
            patologías preexistentes con posterioridad al ingreso, serán 
            igualmente excluidas de la prestación de servicios. </P>
            <P class="textJustify">El valor de la cuota contributiva es de 
            $&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
            mensuales, la cual será ajustada por la División Médica en el 
            momento que se considere necesario y para lo cual se le avisará con 
            anticipación. </P>
            <P><BR>Cordialmente,</P>
            <P>&nbsp;</P>
            <P>________________________________ <BR 
            ><STRONG>División Médica 
            BP<BR><BR><BR 
            ><BR></STRONG></P>
            <P>
            <BR>&nbsp;</P></TD></TR></TR></TABLE></form>
	</body>
</HTML>
