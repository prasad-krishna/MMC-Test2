<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConvencionesEstados.ascx.cs"
    Inherits="Mercer.Tpa.Agenda.Web.UI.BusquedaCitas.ConvencionesEstados" %>
<div class="convencionesEstados">
  
    <table id="tablaConvenciones">
        <tbody>
            <tr>
                <td>
                    <div class="estadoCitaPendiente">
                    </div>
                </td>
                <td>
                    Cita pendiente
                </td>

                <td>
                    <div class="estadoCitaFinalizada">
                    </div>
                </td>
                <td>
                    Cita finalizada
                </td>

                <td>
                    <div class="estadoCitaInasistida">
                    </div>
                </td>
                <td>
                    El paciente no asitió
                </td>

                <td>
                    <div class="estadoCitaCancelada">
                    </div>
                </td>
                <td>
                    Cita cancelada
                </td>

                <td>
                    <div class="estadoCitaEspera">
                    </div>
                </td>
                <td>
                    El paciente esta esperando
                </td>
            </tr>
        </tbody>
    </table>
</div>
