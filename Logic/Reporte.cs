using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using Mercer.Medicines.Logic;
using Microsoft.Security.Application;
using System.Data;
/// <summary>
/// Genera un reporte con las restricciones a partir de un XML
/// </summary>
public class Reporte
{
    /// <summary>Atributo, </summary>
    private int tipo; // Tipo de generacion 

    // Methods

    /// <summary>
    /// Constructor, define el tipo del reporte, 1=Normal,2=Excel,3=Word
    /// </summary>
    /// <returns>void</returns>
    public Reporte()
    {
        this.tipo = 1;
    }
    /// <summary>
    /// Cambia el tipo del reporte, 1=Normal,2=Excel,3=Word
    /// </summary>
    /// <returns>void</returns>
    public void cambiarTipo(int tipo_real)
    {
        this.tipo = tipo_real;
    }

    /// <summary>
    /// Devuelve las columnas y celdas de un reporte determinado en una cadena, a partir del xml, las restricciones del usuario y la pagina actual
    /// </summary>
    /// <returns>string cadena con la tabla de los resultados del reporte</returns>
    /* Se encarga de leer la informacion de la consulta y mandarla a pantalla */
    public string darReporte(string nombreReporte,
                            System.Xml.XmlNodeList campos,
                            string[] nombresRequest,
                            string[] valoresRequest,
                            string link,
                            string consultaReporte,
                            string whereNuevo,
                            int pagina,
                            int numeroRegistrosPagina,
                            string plantillaTabla,
                            string plantillaTitulo,
                            string plantillaFilaTitulo,
                            string plantillaCelda,
                            string plantillaFilaCelda,
                            string plantillaNavegacion,
                            string conexion,
                            int cantidad_parametros,
                            string nombreDivAnterior,
                            string parametrosAnteriores,
                            System.Web.HttpRequest request,
                            string titulo
        )
    {
        /* Se consulta la cantidad de reguistros de la tabla*/
        int conteoRegistros;
        int offset;
        string consultaSinSelect = "";
        SqlCommand comandoConsulta = null;
        SqlConnection laConexion = null;
        SqlDataReader reader = null;
        SqlDataReader lector = null;
        string valorCelda = "";
        string textoCelda = "";
        string filaTabla = "";
        string filaTitulos = "";
        string textoFilaTitulos = "";
        string textoFilaCelda = "";
        string filas = "";
        string valorTabla = "";
        string elementosForm = "";
        string laNavegacion = "";
        int cantidadElementos;
        int cantidadPaginas;
        bool hayAnterior;
        bool haySiguiente;
        int i = 0;
        int j = 0;
        int l = 0;
        int registrosTraidos;
        int nivel;
        string nombre_div = "";
        string plantillaReemplazada = "";
        string groupBy = "";
        string alias_campos = "";
        string alias_campos_base = "";
        int k = 0;
        string orderBy = "";
        string orderByNegativo = "";
        string valorPlantillaTitulo = "";
        int z = 0;
        string consultaConteoReporte = "";
        bool hayExpansion = false;
        string filaExpansion = "";
        string filaExpansionAnterior;
        string filaSimple = "";
        string filaNormal = "";
        string celdaExpansion;
        string celdaSimple, celdaNormal;
        string[][] acumulados;
        int t = 0;
        int u = 0;
        bool hayGrupo = false;
        string groupByNuevo = "";
        string cadenaExpandir = "";
        string[] elementosExpandir;
        string expandirAntes = "";
        string expandirActual = "";
        string filaEncabezadoExpansion = "";
        string filaDetalleExpansion = "";
        string str_acumulados = "";
        string[] str_elementosAcumulados;
        int y = 0;
        int ultimasuma = 0;
        int p = 0;
        double sumaActual = 0;
        string filaTotalFinal = "";
        int[] posiciones;
        int indice = 0;
        int el_indice = 0;
        int minimo = 0;
        int ultima_posicion = 0;
        int ultima_posicion_aumentada = 0;
        object valorObjeto;
        string tipoDato = "";
        string primeraColumnaVisible = "";

        posiciones = new int[campos.Count];
        while (indice < campos.Count)
        {
            try
            {
                if (request["_SELECTORDER" + indice] != "")
                {
                    posiciones[indice] = Convert.ToInt32(request["_SELECTORDER" + indice]);
                }
            }
            catch
            {
            }
            indice = indice + 1;
        }


        celdaExpansion = "";
        celdaSimple = "";
        celdaNormal = "";

        nivel = cantidad_parametros + 1;
        valorTabla = "";
        filaTitulos = "";
        conteoRegistros = 0;
        laConexion = new SqlConnection(conexion);
        laConexion.Open();

        /* Se genera la consulta paginada de registros */
        if (this.tipo != 1)
        {
        }
        if (numeroRegistrosPagina > 0 && pagina > 0)
        {
            consultaSinSelect = consultaReporte;
            consultaSinSelect = consultaSinSelect.ToUpper();
            k = 0;
            foreach (System.Xml.XmlNode campo in campos)
            {
                try
                {
                    l = k + 1;
                    if (request["_VISIBLE" + k] == "visible")
                    {

                        if (primeraColumnaVisible == "")
                        {
                            primeraColumnaVisible = "COL" + l;
                        }
                        if (alias_campos != "")
                        {
                            alias_campos = alias_campos + ",";
                            alias_campos_base = alias_campos_base + ",";
                        }
                        try
                        {
                            if (campo.Attributes["acumulado"].InnerXml.ToString() == "si" && request["_VISIBLE" + k] == "visible")
                            {
                                if (hayGrupo)
                                {
                                    alias_campos = alias_campos + "SUM(COL" + l.ToString() + ") AS COL" + l.ToString();
                                    alias_campos_base = alias_campos_base + "COL" + l.ToString();
                                }
                                else
                                {
                                    alias_campos = alias_campos + "COL" + l.ToString() + " AS COL" + l.ToString();
                                    alias_campos_base = alias_campos_base + "COL" + l.ToString();
                                }
                                if (str_acumulados != "")
                                {
                                    str_acumulados = str_acumulados + ",";
                                }
                                str_acumulados = str_acumulados + "1";

                            }
                            else
                            {
                                if (groupByNuevo == "")
                                {
                                    groupByNuevo = " GROUP BY ";
                                }
                                else
                                {
                                    groupByNuevo = groupByNuevo + ",";
                                }
                                groupByNuevo = groupByNuevo + "COL" + l.ToString();

                                if (str_acumulados != "")
                                {
                                    str_acumulados = str_acumulados + ",";
                                }
                                str_acumulados = str_acumulados + "0";

                            }
                        }
                        catch
                        {
                            alias_campos = alias_campos + "COL" + l.ToString() + " AS COL" + l.ToString();
                            alias_campos_base = alias_campos_base + "COL" + l.ToString();

                            if (groupByNuevo == "")
                            {
                                groupByNuevo = " GROUP BY ";
                            }
                            else
                            {
                                groupByNuevo = groupByNuevo + ",";
                            }
                            groupByNuevo = groupByNuevo + "COL" + l.ToString();

                            if (str_acumulados != "")
                            {
                                str_acumulados = str_acumulados + ",";
                            }
                            str_acumulados = str_acumulados + "0";


                        }
                        try
                        {
                            if (request["_GROUP" + k] == "grupo")
                            {
                                if (groupBy == "")
                                {
                                    groupBy = " GROUP BY ";

                                }
                                else
                                {
                                    groupBy = groupBy + ",";
                                    cadenaExpandir = cadenaExpandir + ",";
                                }
                                groupBy = groupBy + "COL" + l.ToString();
                                cadenaExpandir = cadenaExpandir + "1";
                                hayGrupo = true;
                            }
                            else
                            {
                                if (cadenaExpandir != "")
                                {
                                    cadenaExpandir = cadenaExpandir + ",";
                                }
                                cadenaExpandir = cadenaExpandir + "0";
                            }
                        }
                        catch
                        {
                            if (cadenaExpandir != "")
                            {
                                cadenaExpandir = cadenaExpandir + ",";
                            }
                            cadenaExpandir = cadenaExpandir + "0";
                        }
                    }
                    else
                    {
                        if (str_acumulados != "")
                        {
                            str_acumulados = str_acumulados + ",";
                        }
                        str_acumulados = str_acumulados + "0";
                    }
                }
                catch
                {
                    if (str_acumulados != "")
                    {
                        str_acumulados = str_acumulados + ",";
                    }
                    str_acumulados = str_acumulados + "0";
                }
                k = k + 1;
            }

            elementosExpandir = cadenaExpandir.Split(new char[] { ',' });

            str_elementosAcumulados = str_acumulados.Split(new char[] { ',' });

            // Se procesan los order By
            indice = 0;
            while (indice < posiciones.Length)
            {
                minimo = posiciones.Length + 2;
                ultima_posicion = 0;
                el_indice = 0;
                while (el_indice < posiciones.Length)
                {
                    if (posiciones[el_indice] < minimo && posiciones[el_indice] > 0)
                    {
                        ultima_posicion = el_indice;
                        minimo = posiciones[el_indice];
                    }
                    el_indice = el_indice + 1;
                }
                if (minimo < posiciones.Length + 2)
                {
                    // Se actualiza descontando el mayor
                    if (orderBy != "")
                    {
                        orderBy = orderBy + ",";
                        orderByNegativo = orderByNegativo + ",";
                    }
                    else
                    {
                        orderBy = orderBy + " ORDER BY ";
                        orderByNegativo = orderByNegativo + " ORDER BY ";
                    }
                    ultima_posicion_aumentada = ultima_posicion + 1;
                    orderBy = orderBy + "COL" + ultima_posicion_aumentada + " ASC";
                    orderByNegativo = orderByNegativo + "COL" + ultima_posicion_aumentada + " DESC";
                    posiciones[ultima_posicion] = 0;
                }

                indice = indice + 1;
            }


            if (orderBy == "")
            {
                orderBy = "ORDER BY " + primeraColumnaVisible + " ASC";
                orderByNegativo = "ORDER BY " + primeraColumnaVisible + " DESC";
            }
            if (hayGrupo)
            {
                groupBy = groupByNuevo;
            }
            //RAM* reporte para mostrar bajas y mayores de 5 años
            if (nombreReporte == "ConsultasBajas")
            {
                consultaSinSelect = consultaSinSelect + " AND SIC_BENEFICIARIO.estado = 2" +
                                    " AND SIC_BENEFICIARIO.fecha_egreso < DATEADD(year, -5, getdate()) ";
            }
            //RAM* reporte para mostrar bajas y menores de 5 años
                    //else
                    //{
                    //    consultaSinSelect = consultaSinSelect + " AND SIC_BENEFICIARIO.estado = 1 ";
                    //}

            consultaConteoReporte =
                                "       SELECT COUNT(*) " +
                                "       FROM ( " +
                                "               SELECT DISTINCT " + alias_campos +
                                "               FROM ( " +
                                "                       SELECT " + alias_campos_base +
                                "                       FROM (  " +
                                "                               " + consultaSinSelect +
                                "                       ) AS CONSULTA0 " +
                                "                       WHERE 1=1 " + whereNuevo +
                                "               ) AS CONSULTA1 " +
                                "           " + groupBy +
                                "       ) AS CONSULTA2 ";




            comandoConsulta = new SqlCommand("Exec ExecReport @consulta", laConexion);
            comandoConsulta.CommandTimeout = 10000;
            comandoConsulta.Parameters.Add("@consulta", SqlDbType.Text).Value = consultaConteoReporte;

            try
            {
                lector = comandoConsulta.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw new Exception(consultaConteoReporte + "<br><br>" + ex.Message + "<br>");
            }

            while (lector.Read())
            {
                try
                {
                    conteoRegistros = lector.GetInt32(0);
                }
                catch
                {
                    throw new Exception("(Consulta de conteo no valida) Numero de parametro: " + consultaConteoReporte);
                }
            }
            lector.Close();
            lector = null;
            comandoConsulta = null;

            // Se arman todos los registros posibles con valores en blanco
            if (hayGrupo || this.tipo == 2)
            {
                // Si hay grupo no se pagina ya que se deben devolver todos los registros
                numeroRegistrosPagina = conteoRegistros;
            }


            // Se crea uan tabla con los elementos para acumular
            acumulados = new string[numeroRegistrosPagina][];

            t = 0;
            while (t < numeroRegistrosPagina)
            {
                acumulados[t] = new string[campos.Count + 1];
                u = 0;
                // Se llenan los valores con blancos para despues hacer cuentas
                while (u < campos.Count + 1)
                {
                    acumulados[t][u] = "";
                    u = u + 1;
                }
                t = t + 1;
            }


            /* Se calcula la cantidad de paginas de la consulta */
            if (numeroRegistrosPagina > 0)
            {
                cantidadPaginas = Convert.ToInt16(conteoRegistros / numeroRegistrosPagina);
            }
            else
            {
                cantidadPaginas = 0;
            }
            if (cantidadPaginas * numeroRegistrosPagina < conteoRegistros)
            {
                cantidadPaginas = cantidadPaginas + 1;
            }

            valorTabla = "";
            offset = pagina * numeroRegistrosPagina;
            if (offset > conteoRegistros)
            {
                registrosTraidos = offset - conteoRegistros - 1;
                offset = conteoRegistros;
                if (registrosTraidos < numeroRegistrosPagina)
                {
                    registrosTraidos = conteoRegistros - ((pagina - 1) * numeroRegistrosPagina);
                }
            }
            else
            {
                registrosTraidos = numeroRegistrosPagina;
            }

            if (this.tipo == 2)
            {
                registrosTraidos = conteoRegistros;
            }

            /* Se arma la consulta definitiva */
            if (this.tipo == 1 || this.tipo == 3)
            {
                consultaSinSelect = "SELECT TOP " + registrosTraidos + " " + alias_campos_base + " " +
                                    "FROM ( " +
                                    "       SELECT TOP " + registrosTraidos + " " + alias_campos_base + " " +
                                    "       FROM ( " +
                                    "               SELECT DISTINCT TOP " + offset + " " + alias_campos +
                                    "               FROM ( " +
                                    "                       SELECT " + alias_campos_base +
                                    "                       FROM (  " +
                                    "                               " + consultaSinSelect +
                                    "                       ) AS CONSULTA0 " +
                                    "                       WHERE 1=1 " + whereNuevo +
                                    "               ) AS CONSULTA1 " +
                                    "           " + groupBy +
                                    "           " + orderBy +
                                    "       ) AS CONSULTA2 " +
                                    "   " + orderByNegativo +
                                    ") AS CONSULTA3 " +
                                    orderBy;
            }
            else if (this.tipo == 2)
            {
                consultaSinSelect = "               SELECT DISTINCT " + alias_campos +
                                    "               FROM ( " +
                                    "                       SELECT " + alias_campos_base +
                                    "                       FROM (  " +
                                    "                               " + consultaSinSelect +
                                    "                       ) AS CONSULTA0 " +
                                    "                       WHERE 1=1 " + whereNuevo +
                                    "               ) AS CONSULTA1 " +
                                    "           " + groupBy +
                                    "           " + orderBy;
            }
            else
            {
            }

            try
            {
                consultaSinSelect = consultaSinSelect.Replace("  ", " ");
                consultaSinSelect = consultaSinSelect.Replace("  ", " ");
                comandoConsulta = new SqlCommand("Exec ExecReport @consulta", laConexion);
                comandoConsulta.CommandTimeout = 10000;
                comandoConsulta.Parameters.Add("@consulta", SqlDbType.Text).Value = consultaSinSelect;
                reader = comandoConsulta.ExecuteReader();
                filas = "";
                j = 0;
            }
            catch (Exception ex)
            {
                throw new Exception("(Definicion de registros de reportes no valida <br> " + consultaSinSelect + "<br>, revise la cantidad de elementos y numeros de pagina)" + ex.ToString() + "<br><br>" + consultaSinSelect + "<br><br>, la longitud de la cadena es " + consultaSinSelect.Length);
            }
            j = 0;
            filaExpansionAnterior = "";
            filaEncabezadoExpansion = "";
            filaDetalleExpansion = "";
            while (reader.Read())
            {
                /* Se asumen resultados solo como chars */
                i = 0;
                filaTabla = "";
                z = 0;
                hayExpansion = false;
                filaExpansion = "";
                filaSimple = "";
                filaNormal = "";
                filaTabla = "";
                expandirActual = "";
                foreach (System.Xml.XmlNode campo in campos)
                {
                    try
                    {
                        if (request["_VISIBLE" + i].ToString() == "visible")
                        {
                            try
                            {
                                acumulados[j][i] = reader.IsDBNull(z) ? "" : reader.GetValue(z).ToString();
                            }
                            catch
                            {
                                throw new Exception("j nuevamente es " + j.ToString() + " y i es " + i.ToString());
                            }
                            valorObjeto = reader.IsDBNull(z) ? null : reader.GetValue(z);
                            if (valorObjeto == null)
                            {
                                valorCelda = "";
                            }
                            else
                            {
                                // Se intenta primero leer como cadena
                                tipoDato = valorObjeto.GetType().FullName;
                                if (tipoDato == "System.String")
                                {
                                    valorCelda = valorObjeto.ToString();
                                }
                                else if (tipoDato == "System.Double")
                                {
                                    // Se lee como double
                                    valorCelda = quitarCentavos(Convert.ToDouble(valorObjeto).ToString("N3"));
                                }
                                else if (tipoDato == "System.Int32")
                                {
                                    // Se lee como entero
                                    valorCelda = valorObjeto.ToString();
                                }
                                else if (tipoDato == "System.Int16")
                                {
                                    // Se lee como entero
                                    valorCelda = valorObjeto.ToString();
                                }
                                else if (tipoDato == "System.Int64")
                                {
                                    // Se lee como entero
                                    valorCelda = valorObjeto.ToString();
                                }
                                else if (tipoDato == "System.DateTime")
                                {
                                    // Se lee como fecha
                                    valorCelda = valorObjeto.ToString();
                                }
                                else if (tipoDato == "System.Decimal")
                                {
                                    // Se lee como double
                                    valorCelda = quitarCentavos(Convert.ToDouble(valorObjeto).ToString("N3"));
                                }
                                else if (tipoDato == "System.Single")
                                {
                                    // Se lee como double
                                    valorCelda = quitarCentavos(Convert.ToDouble(valorObjeto).ToString());
                                }
                                else
                                {
                                    // En otro caso se convierte a string
                                    valorCelda = valorObjeto.ToString();
                                }
                            }


                            try
                            {
                                if (request["_GROUP" + i] == "grupo")
                                {
                                    expandirActual = expandirActual + reader.GetValue(z).ToString() + ",";
                                }
                            }
                            catch
                            {
                            }

                            textoCelda = plantillaCelda.Replace("{VALORCELDA}", valorCelda);
                            plantillaReemplazada = textoCelda;

                            celdaExpansion = textoCelda.Replace("{LINKEXPANDIR}", "");
                            celdaSimple = plantillaCelda.Replace("{VALORCELDA}", "").Replace("{LINKEXPANDIR}", "");
                            celdaNormal = textoCelda.Replace("{LINKEXPANDIR}", "");


                            z = z + 1;

                            if (hayGrupo)
                            {
                                try
                                {
                                    if (request["_GROUP" + i] == "grupo")
                                    {
                                        filaEncabezadoExpansion = filaEncabezadoExpansion + plantillaCelda.Replace("{VALORCELDA}", valorCelda).Replace("{LINKEXPANDIR}", "");
                                        filaDetalleExpansion = filaDetalleExpansion + plantillaCelda.Replace("{VALORCELDA}", "").Replace("{LINKEXPANDIR}", "");
                                    }
                                    else
                                    {
                                        if (str_elementosAcumulados[i] == "1")
                                        {
                                            filaEncabezadoExpansion = filaEncabezadoExpansion + plantillaCelda.Replace("{VALORCELDA}", "{ACUMULADO" + j.ToString() + "-" + i.ToString() + "}").Replace("{LINKEXPANDIR}", "");
                                            filaDetalleExpansion = filaDetalleExpansion + plantillaCelda.Replace("{VALORCELDA}", valorCelda).Replace("{LINKEXPANDIR}", "");
                                        }
                                        else
                                        {
                                            filaEncabezadoExpansion = filaEncabezadoExpansion + plantillaCelda.Replace("{VALORCELDA}", "").Replace("{LINKEXPANDIR}", "");
                                            filaDetalleExpansion = filaDetalleExpansion + plantillaCelda.Replace("{VALORCELDA}", valorCelda).Replace("{LINKEXPANDIR}", "");
                                        }
                                    }
                                }
                                catch
                                {
                                    if (str_elementosAcumulados[i - 1] == "1")
                                    {
                                        filaEncabezadoExpansion = filaEncabezadoExpansion + plantillaCelda.Replace("{VALORCELDA}", "{ACUMULADO" + i.ToString() + "-" + j.ToString() + "}").Replace("{LINKEXPANDIR}", "");
                                        filaDetalleExpansion = filaDetalleExpansion + plantillaCelda.Replace("{VALORCELDA}", valorCelda).Replace("{LINKEXPANDIR}", "");
                                    }
                                    else
                                    {
                                        filaEncabezadoExpansion = filaEncabezadoExpansion + plantillaCelda.Replace("{VALORCELDA}", "").Replace("{LINKEXPANDIR}", "");
                                        filaDetalleExpansion = filaDetalleExpansion + plantillaCelda.Replace("{VALORCELDA}", valorCelda).Replace("{LINKEXPANDIR}", "");
                                    }
                                }
                            }
                            else
                            {
                                filaEncabezadoExpansion = filaEncabezadoExpansion + plantillaCelda.Replace("{VALORCELDA}", "").Replace("{LINKEXPANDIR}", "");
                                filaDetalleExpansion = filaDetalleExpansion + plantillaCelda.Replace("{VALORCELDA}", valorCelda).Replace("{LINKEXPANDIR}", "");
                            }

                            if (j == 0)
                            {
                                valorPlantillaTitulo = plantillaTitulo;
                                valorPlantillaTitulo = valorPlantillaTitulo.Replace("{VALORTITULO}", campo.Attributes["titulo"].InnerXml.ToString());
                                valorPlantillaTitulo = valorPlantillaTitulo.Replace("{ORDEN}", "all");
                                valorPlantillaTitulo = valorPlantillaTitulo.Replace("{POS}", i.ToString());
                                filaTitulos = filaTitulos + valorPlantillaTitulo;
                            }
                        }
                    }
                    catch
                    {
                    }
                    i = i + 1;
                }

                if (hayGrupo)
                {
                    if (expandirAntes == "" || (expandirAntes != expandirActual))
                    {
                        textoFilaTitulos = plantillaFilaTitulo.Replace("{FILATITULO}", filaTitulos);
                        textoFilaCelda = plantillaFilaCelda.Replace("{FILACELDA}", filaEncabezadoExpansion);
                        textoFilaCelda = textoFilaCelda.Replace("{NOMBREDIVEXPANDIR}", nombre_div);
                        filas = filas + textoFilaCelda;
                        textoFilaCelda = plantillaFilaCelda.Replace("{FILACELDA}", filaDetalleExpansion);
                        textoFilaCelda = textoFilaCelda.Replace("{NOMBREDIVEXPANDIR}", nombre_div);
                        filas = filas + textoFilaCelda;

                    }
                    else
                    {
                        textoFilaTitulos = plantillaFilaTitulo.Replace("{FILATITULO}", filaTitulos);
                        textoFilaCelda = plantillaFilaCelda.Replace("{FILACELDA}", filaDetalleExpansion);
                        textoFilaCelda = textoFilaCelda.Replace("{NOMBREDIVEXPANDIR}", nombre_div);
                        filas = filas + textoFilaCelda;
                    }
                }
                else
                {
                    textoFilaTitulos = plantillaFilaTitulo.Replace("{FILATITULO}", filaTitulos);
                    textoFilaCelda = plantillaFilaCelda.Replace("{FILACELDA}", filaDetalleExpansion);
                    textoFilaCelda = textoFilaCelda.Replace("{NOMBREDIVEXPANDIR}", nombre_div);
                    filas = filas + textoFilaCelda;
                }

                // Se revisa si hubo cambios de expansion
                if (expandirAntes != expandirActual && expandirAntes != "")
                {
                    y = 0;
                    while (y < str_elementosAcumulados.Length)
                    {


                        if (str_elementosAcumulados[y] == "1")
                        {
                            // Se suman todos los elementos de la columna
                            p = ultimasuma;
                            sumaActual = 0;
                            while (p < j)
                            {
                                try
                                {
                                    sumaActual = sumaActual + Convert.ToDouble(acumulados[p][y]);
                                }
                                catch
                                {
                                }
                                p = p + 1;
                            }
                            // Se hace el reemplazo en las ya generados
                            filas = filas.Replace("{ACUMULADO" + ultimasuma.ToString() + "-" + y.ToString() + "}", "<b>" + sumaActual.ToString("N3") + "</b>");
                        }
                        y = y + 1;
                    }
                    ultimasuma = j;
                }

                expandirAntes = expandirActual;
                filaEncabezadoExpansion = "";
                filaDetalleExpansion = "";
                j = j + 1;
            }
            reader.Close();
            lector = null;
            comandoConsulta = null;


            // Se arma el ultimo grupo
            if (hayGrupo)
            {
                y = 0;
                while (y < str_elementosAcumulados.Length)
                {
                    if (str_elementosAcumulados[y] == "1")
                    {
                        // Se suman todos los elementos de la columna
                        p = ultimasuma;
                        sumaActual = 0;
                        while (p < j)
                        {
                            try
                            {
                                sumaActual = sumaActual + Convert.ToDouble(acumulados[p][y]);
                            }
                            catch
                            {
                            }
                            p = p + 1;
                        }
                        // Se hace el reemplazo en las ya generados
                        filas = filas.Replace("{ACUMULADO" + ultimasuma.ToString() + "-" + y.ToString() + "}", "<b>" + sumaActual.ToString("N3") + "</b>");
                    }
                    y = y + 1;
                }
            }
            if (hayGrupo)
            {
                y = 0;
                ultimasuma = 0;
                filaTotalFinal = "";
                while (y < str_elementosAcumulados.Length)
                {
                    if (request["_VISIBLE" + y] == "visible")
                    {
                        if (str_elementosAcumulados[y] == "1")
                        {
                            // Se suman todos los elementos de la columna
                            p = ultimasuma;
                            sumaActual = 0;
                            while (p < j)
                            {
                                try
                                {
                                    sumaActual = sumaActual + Convert.ToDouble(acumulados[p][y]);
                                }
                                catch
                                {
                                }
                                p = p + 1;
                            }
                            // Se hace el reemplazo en las ya generados
                            filaTotalFinal = filaTotalFinal + plantillaCelda.Replace("{VALORCELDA}", "<b>" + sumaActual.ToString("N3") + "</b>").Replace("{LINKEXPANDIR}", "");
                        }
                        else
                        {
                            if (filaTotalFinal == "")
                            {
                                filaTotalFinal = filaTotalFinal + plantillaCelda.Replace("{VALORCELDA}", "<b>TOTAL</b>").Replace("{LINKEXPANDIR}", "");
                            }
                            else
                            {
                                filaTotalFinal = filaTotalFinal + plantillaCelda.Replace("{VALORCELDA}", "").Replace("{LINKEXPANDIR}", "");
                            }
                        }
                    }
                    y = y + 1;
                }
                filas = filas + filaTotalFinal;
            }

            valorTabla = plantillaTabla.Replace("{REGISTROS}", filas);
            valorTabla = valorTabla.Replace("{TITULOS}", textoFilaTitulos);
            /* Se genera la paginacion correspondiente */
            hayAnterior = false;
            haySiguiente = false;

            if (this.tipo == 1)
            {
                elementosForm = "<form method=post name=formularioReporte action='" + link + "' style='margin: 0 0 0 0'>";
                /* Se agregan los elementos del request */
                i = 0;
                cantidadElementos = nombresRequest.Length;
                while (i < cantidadElementos)
                {
                    if (nombresRequest[i] != "paginaActual" && !nombresRequest[i].ToString().StartsWith("inputOrden"))
                    {
                        elementosForm = elementosForm + "<input type=hidden name='" + nombresRequest[i] + "' value='" + valoresRequest[i] + "'>";
                    }
                    i = i + 1;
                }
                elementosForm = elementosForm + "<input type=hidden name='paginaActual' value='" + request["paginaActual"].ToString() + "'>";
                elementosForm = elementosForm + "<script>";
                elementosForm = elementosForm + "function paginaReporte(pagina){";
                elementosForm = elementosForm + "   document.formularioReporte.paginaActual.value = pagina;";
                elementosForm = elementosForm + "   document.formularioReporte.action = 'reporte.aspx';";
                elementosForm = elementosForm + "   document.formularioReporte.target = '_self';";
                elementosForm = elementosForm + "   document.formularioReporte.submit();";
                elementosForm = elementosForm + "}";
                elementosForm = elementosForm + "</script>";
            }
            else if (this.tipo == 2)
            {
                elementosForm = "";
            }
            else
            {
                elementosForm = "";
            }
            valorTabla = valorTabla.Replace("{FORMULARIO}", elementosForm);
            valorTabla = valorTabla.Replace("{TITULO}", titulo);
            valorTabla = valorTabla.Replace("{FECHA}", DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString() + " " + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second);
            valorTabla = valorTabla.Replace("{NUMREGISTROS}", conteoRegistros.ToString());
            laNavegacion = "";
            hayAnterior = false;
            haySiguiente = false;
            if (this.tipo == 1)
            {
                if (pagina > 1)
                {
                    hayAnterior = true;
                }
                if (pagina < cantidadPaginas)
                {
                    haySiguiente = true;
                }
                if (!hayAnterior)
                {
                    laNavegacion = plantillaNavegacion.Replace("{LINKANTERIOR}", "#");
                    laNavegacion = laNavegacion.Replace("{INICIOCOMENTARIOANTERIOR}", "<!--");
                    laNavegacion = laNavegacion.Replace("{FINCOMENTARIOANTERIOR}", "-->");
                }
                else
                {
                    laNavegacion = plantillaNavegacion.Replace("{LINKANTERIOR}", "javascript:paginaReporte(" + Convert.ToString(pagina - 1) + ")");
                    laNavegacion = laNavegacion.Replace("{INICIOCOMENTARIOANTERIOR}", "");
                    laNavegacion = laNavegacion.Replace("{FINCOMENTARIOANTERIOR}", "");
                }
                if (!haySiguiente)
                {
                    laNavegacion = laNavegacion.Replace("{LINKSIGUIENTE}", "#");
                    laNavegacion = laNavegacion.Replace("{INICIOCOMENTARIOSIGUIENTE}", "<!--");
                    laNavegacion = laNavegacion.Replace("{FINCOMENTARIOSIGUIENTE}", "-->");

                }
                else
                {
                    laNavegacion = laNavegacion.Replace("{LINKSIGUIENTE}", "javascript:paginaReporte(" + Convert.ToString(pagina + 1) + ")");
                    laNavegacion = laNavegacion.Replace("{INICIOCOMENTARIOSIGUIENTE}", "");
                    laNavegacion = laNavegacion.Replace("{FINCOMENTARIOSIGUIENTE}", "");
                }
            }
            else if (this.tipo == 2)
            {
                laNavegacion = "";
            }
            else
            {
                laNavegacion = "";
            }
            // Se arma el listado de paginas que pueden ser visualizadas en la navegación
            string cadena_numeros_pagina = "<table cellspacing=1 cellpadding=1 border=0><tr>";
            int indice_pagina = 1;
            while (indice_pagina <= cantidadPaginas)
            {
                cadena_numeros_pagina = cadena_numeros_pagina + "<td align=right>";
                if (indice_pagina == pagina)
                {
                    cadena_numeros_pagina = cadena_numeros_pagina + "<b>" + pagina.ToString() + "</b>";
                }
                else
                {
                    cadena_numeros_pagina = cadena_numeros_pagina + "<a href='javascript:paginaReporte(" + indice_pagina.ToString() + ")'>" + indice_pagina.ToString() + "</a>";
                }
                cadena_numeros_pagina = cadena_numeros_pagina + "</td>";
                if (Convert.ToInt32(indice_pagina) % 50 == 0)
                {
                    cadena_numeros_pagina = cadena_numeros_pagina + "</tr><tr>";
                }
                indice_pagina = indice_pagina + 1;
            }
            cadena_numeros_pagina = cadena_numeros_pagina + "</tr></table>";
            laNavegacion = laNavegacion.Replace("{NUMEROSPAGINAS}", cadena_numeros_pagina);
            if (conteoRegistros > 0)
            {
                laNavegacion = laNavegacion.Replace("{NOREGISTROS}", "");
                // Se reemplaza la cantidad de paginas devueltas
                laNavegacion = laNavegacion.Replace("{PAGINAACTUAL}", pagina.ToString());
                laNavegacion = laNavegacion.Replace("{NUMEROPAGINAS}", cantidadPaginas.ToString());
            }
            else
            {
                laNavegacion = laNavegacion.Replace("{NOREGISTROS}", "No se encontraron registros para su consulta");
                // Se reemplaza la cantidad de paginas devueltas
                laNavegacion = laNavegacion.Replace("{PAGINAACTUAL}", "0");
                laNavegacion = laNavegacion.Replace("{NUMEROPAGINAS}", "0");
            }

            valorTabla = valorTabla.Replace("{NAVEGACION}", laNavegacion);
            valorTabla = valorTabla.Replace("{TITULO}", nombreReporte);
        }

        laConexion.Close();
        laConexion = null;
        return valorTabla + "</form>";
    }

    /// <summary>
    /// Devuelve los resultados de una consulta de valores multiples como las empresas o los usuarios
    /// </summary>
    /// <returns>string cadena con la tabla de los resultados de la consulta</returns>
    /* Se encarga de leer la informacion de la consulta y mandarla a pantalla */
    //RAM* se agrega corporativo
    public string darReporteSelector(string nombreReporte, string tabla,
        string campo_valor,
        string campo_nombre,
        string nombreCampo,
        string tituloCampo,
        System.Web.HttpRequest request,
        int pagina, int numeroRegistrosPagina, string plantillaTabla, string plantillaTitulo, string plantillaFilaTitulo, string plantillaCelda, string plantillaFilaCelda, string plantillaNavegacion, string conexion, string corporativo, string condicion)
    {
        /* Se consulta la cantidad de reguistros de la tabla*/
        int conteoRegistros;
        int offset;
        string consultaSinSelect;
        SqlDataReader reader = null;
        string valorCelda = "";
        string textoCelda = "";
        string filaTabla = "";
        string filaTitulos = "";
        string textoFilaTitulos = "";
        string textoFilaCelda = "";
        string filas = "";
        string valorTabla = "";
        string elementosForm = "";
        string laNavegacion = "";
        int cantidadElementos;
        int cantidadPaginas;
        bool hayAnterior;
        bool haySiguiente;
        int i;
        int j;
        int registrosTraidos;
        string plantillaReemplazada = "";
        string consultaReporte;
        string consultaConteoReportes;
        string nombreCelda = "";
        SqlConnection laConexion = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings[conexion].ToString());
        SqlCommand comando;
        string[] nombresRequest;
        string[] valoresRequest;
        string alias_campos = "";
        string alias_campos_base = "";
        string orderBy = "";
        string orderByNegativo = "";

        // Se quitan los reemplazos del nombre
        campo_nombre = campo_nombre.Replace("~", "'").Replace("¡", "+");

        if (Convert.ToInt32(request["paginaActual"].ToString()) > 1)
        {
            pagina = Convert.ToInt32(request["paginaActual"].ToString());
        }

        //RAM* llena la empresas del seleccionado
        if (tabla == "EmpresaDatos")
        {
            if (corporativo == "" || corporativo == "{CORPORATIVO}")
            {
                string empresas = "";
                EmpresaUsers objEmpresaUsers = new EmpresaUsers();
                objEmpresaUsers.IdUser = Convert.ToInt32(System.Web.HttpContext.Current.Session["IdUser"]);
                DataSet ds = objEmpresaUsers.GetEmpresasUser();
                for (int indice = 0; indice < ds.Tables[0].Rows.Count; indice++)
                {
                    empresas += ds.Tables[0].Rows[indice].ItemArray[0].ToString() + ',';

                }
                if (empresas.Length > 0)
                    empresas = empresas.Substring(0, empresas.Length - 1);
                consultaReporte = "SELECT " + campo_valor + " AS COL0," + campo_nombre + " AS COL1 FROM " + tabla + " WHERE empresa_id IN ("+ empresas +")";
                consultaConteoReportes = "SELECT COUNT(*) FROM ( SELECT " + campo_valor + " AS COL0," + campo_nombre + " AS COL1 FROM " + tabla + " WHERE empresa_id IN ("+ empresas +")) AS CONSULTA0 ";
            }
            else
            {
                string condicionCorp = "";
                string empresas = "";
                //si es igual
                if (condicion == "1")
                {
                    consultaReporte = "SELECT a." + campo_valor + " AS COL0, a." + campo_nombre + " AS COL1 FROM " + tabla + " a" +
                  " INNER JOIN SIC_EMPRESA b ON a.empresa_id = b.empresa_id INNER JOIN SIC_CORPORATIVO c ON c.corporativo_id = b.corporativo_id " +
                  " where c.corporativo_id =" + corporativo;
                    consultaConteoReportes = "SELECT COUNT(*) FROM ( SELECT a." + campo_valor + " AS COL0, a." + campo_nombre + " AS COL1 FROM " + tabla + " a" +
                                      " INNER JOIN SIC_EMPRESA b ON a.empresa_id = b.empresa_id INNER JOIN SIC_CORPORATIVO c ON c.corporativo_id = b.corporativo_id " +
                                      " where c.corporativo_id =" + corporativo + ") AS CONSULTA0 ";
                }
                //si es diferente
                else if (condicion == "2")
                {
                    EmpresaUsers objEmpresaUsers = new EmpresaUsers();
                    objEmpresaUsers.IdUser = Convert.ToInt32(System.Web.HttpContext.Current.Session["IdUser"]);
                    DataSet ds = objEmpresaUsers.GetEmpresasUser();
                    for (int indice = 0; indice < ds.Tables[0].Rows.Count; indice++)
                    {
                        empresas += ds.Tables[0].Rows[indice].ItemArray[0].ToString() + ',';

                    }
                    empresas = empresas.Substring(0, empresas.Length - 1);
                    consultaReporte = "SELECT a." + campo_valor + " AS COL0, a." + campo_nombre + " AS COL1 FROM " + tabla + " a" +
                                      " INNER JOIN SIC_EMPRESA b ON a.empresa_id = b.empresa_id INNER JOIN SIC_CORPORATIVO c ON c.corporativo_id = b.corporativo_id " +
                                      " where c.corporativo_id !=" + corporativo + " AND a.empresa_id IN("+ empresas +" )";
                    consultaConteoReportes = "SELECT COUNT(*) FROM ( SELECT a." + campo_valor + " AS COL0, a." + campo_nombre + " AS COL1 FROM " + tabla + " a" +
                                          " INNER JOIN SIC_EMPRESA b ON a.empresa_id = b.empresa_id INNER JOIN SIC_CORPORATIVO c ON c.corporativo_id = b.corporativo_id " +
                                          " where c.corporativo_id !=" + corporativo + " AND a.empresa_id IN(" + empresas + " )) AS CONSULTA0 ";
                }
                else
                {
                    consultaReporte = "SELECT a." + campo_valor + " AS COL0, a." + campo_nombre + " AS COL1 FROM " + tabla + " a" +
                                      " INNER JOIN SIC_EMPRESA b ON a.empresa_id = b.empresa_id INNER JOIN SIC_CORPORATIVO c ON c.corporativo_id = b.corporativo_id " +
                                      " where c.corporativo_id =" + corporativo;
                    consultaConteoReportes = "SELECT COUNT(*) FROM ( SELECT a." + campo_valor + " AS COL0, a." + campo_nombre + " AS COL1 FROM " + tabla + " a" +
                                      " INNER JOIN SIC_EMPRESA b ON a.empresa_id = b.empresa_id INNER JOIN SIC_CORPORATIVO c ON c.corporativo_id = b.corporativo_id " +
                                      " where c.corporativo_id =" + corporativo + ") AS CONSULTA0 ";
                }
            }
        }
        else
        {
            consultaReporte = "SELECT " + campo_valor + " AS COL0," + campo_nombre + " AS COL1 FROM " + tabla;
            consultaConteoReportes = "SELECT COUNT(*) FROM ( SELECT " + campo_valor + " AS COL0," + campo_nombre + " AS COL1 FROM " + tabla + ") AS CONSULTA0 ";
        }

        string baseCondicion = "";
        laConexion.Open();
        if (request["_C0"].ToString() != "")
        {
            if (request["_C0"].ToString() == "=")
            {
                baseCondicion = " COL1 = '" + request["_V0"].ToString() + "'";
            }
            else if (request["_C0"].ToString() == "LIKE")
            {
                baseCondicion = " COL1 LIKE '%" + request["_V0"].ToString() + "%'";
            }
            else if (request["_C0"].ToString() == "GT")
            {
                baseCondicion = " COL1 > '" + request["_V0"].ToString() + "'";
            }
            else if (request["_C0"].ToString() == "GTE")
            {
                baseCondicion = " COL1 >= '" + request["_V0"].ToString() + "'";
            }
            else if (request["_C0"].ToString() == "LT")
            {
                baseCondicion = " COL1 < '" + request["_V0"].ToString() + "'";
            }
            else if (request["_C0"].ToString() == "LTE")
            {
                baseCondicion = " COL1 <= '" + request["_V0"].ToString() + "'";
            }
            else if (request["_C0"].ToString() == "BETWEEN")
            {
                baseCondicion = " COL1 BETWEEN '" + request["_V0"].ToString() + "' AND '" + request["_2V0"].ToString() + "'";
            }
            else
            {
                baseCondicion = "1=1";
            }
        }
        else
        {
            baseCondicion = "1=1";
        }

        consultaConteoReportes = consultaConteoReportes + " WHERE " + baseCondicion;

        nombresRequest = new string[request.Form.Keys.Count + request.QueryString.Count];
        valoresRequest = new string[request.Form.Keys.Count + request.QueryString.Count];
        i = 0;
        while (i < request.Form.Keys.Count)
        {
            nombresRequest[i] = request.Form.Keys.Get(i);
            //Inicio
            //Auto:Diego Montejano Avila
            //Proyecto: Auditoria 2014
            //Fecha: 2014/09/17
            //Observaciones: Se utiliza la dll de Microsoft para eliminar el XSS
            valoresRequest[i] = Encoder.HtmlEncode(request.Form.GetValues(i).GetValue(0).ToString());
            //FIN
            i = i + 1;
        }

        while (i < (request.Form.Keys.Count + request.QueryString.Count))
        {
            nombresRequest[i] = request.QueryString.GetKey(i - request.Form.Keys.Count).ToString();
            //Inicio
            //Auto:Diego Montejano Avila
            //Proyecto: Auditoria 2014
            //Fecha: 2014/09/17
            //Observaciones: Se utiliza la dll de Microsoft para eliminar el XSS
            valoresRequest[i] = Encoder.HtmlEncode(request.QueryString.GetValues(i - request.Form.Keys.Count).GetValue(0).ToString());
            //FIN
            i = i + 1;
        }

        valorTabla = "";
        filaTitulos = "";
        conteoRegistros = 0;
        comando = new SqlCommand("Exec ExecReport @consulta", laConexion);
        comando.Parameters.Add("@consulta", SqlDbType.VarChar).Value = consultaConteoReportes;

        try
        {
            reader = comando.ExecuteReader();
        }
        catch (Exception ex)
        {
            throw new Exception(consultaConteoReportes + "<br><br>" + ex.Message + "<br>");
        }
        while (reader.Read())
        {
            try
            {
                conteoRegistros = reader.IsDBNull(0) ? 0 : reader.GetInt32(0);
            }
            catch
            {
                throw new Exception("(Consulta de conteo no valida) Numero de parametro: ");
            }
        }
        reader.Close();
        reader = null;
        comando = null;

        /* Se genera la consulta paginada de registros */
        if (numeroRegistrosPagina > 0 && pagina > 0)
        {
            /* Se calcula la cantidad de paginas de la consulta */
            cantidadPaginas = Convert.ToInt16(conteoRegistros / numeroRegistrosPagina);
            if (cantidadPaginas * numeroRegistrosPagina < conteoRegistros)
            {
                cantidadPaginas = cantidadPaginas + 1;
            }

            valorTabla = "";
            offset = pagina * numeroRegistrosPagina;
            if (offset > conteoRegistros)
            {
                registrosTraidos = offset - conteoRegistros - 1;
                offset = conteoRegistros;
                if (registrosTraidos < numeroRegistrosPagina)
                {
                    registrosTraidos = conteoRegistros - ((pagina - 1) * numeroRegistrosPagina);
                }
            }
            else
            {
                registrosTraidos = numeroRegistrosPagina;
            }
            consultaSinSelect = consultaReporte;
            consultaSinSelect = consultaSinSelect.ToUpper();
            consultaSinSelect = consultaSinSelect.Replace("SELECT ", " ");

            if (orderBy == "")
            {
                orderBy = "ORDER BY COL0 ASC";
                orderByNegativo = "ORDER BY COL0 DESC";
            }
            alias_campos = "COL0,COL1";
            alias_campos_base = "COL0,COL1";

            /* Se arma la consulta definitiva */
            if (this.tipo == 1)
            {
                consultaSinSelect = "SELECT TOP " + registrosTraidos + " " + alias_campos_base + " " +
                                                        "FROM ( " +
                                                        "       SELECT TOP " + registrosTraidos + " " + alias_campos_base + " " +
                                                        "       FROM ( " +
                                                        "               SELECT DISTINCT TOP " + offset + " " + alias_campos +
                                                        "               FROM ( " +
                                                        "                       SELECT " + alias_campos_base +
                                                        "                       FROM (  " +
                                                        "                               SELECT " + consultaSinSelect +
                                                        "                       ) AS CONSULTA0 " +
                                                        "                       WHERE 1=1 AND " + baseCondicion +
                                                        "               ) AS CONSULTA1 " +
                                                        "           " + orderBy +
                                                        "       ) AS CONSULTA2 " +
                                                        "   " + orderByNegativo +
                                                        ") AS CONSULTA3 " +
                                                        orderBy;

            }
            else if (this.tipo == 2)
            {
                consultaSinSelect = "   SELECT  " + consultaSinSelect
                                    + " ORDER BY CAST(ID AS INTEGER) ASC ";
            }
            else if (this.tipo == 3)
            {
                consultaSinSelect = "SELECT TOP " + registrosTraidos + " " + alias_campos_base + " " +
                                                        "FROM ( " +
                                                        "       SELECT TOP " + registrosTraidos + " " + alias_campos_base + " " +
                                                        "       FROM ( " +
                                                        "               SELECT DISTINCT TOP " + offset + " " + alias_campos +
                                                        "               FROM ( " +
                                                        "                       SELECT " + alias_campos_base +
                                                        "                       FROM (  " +
                                                        "                               SELECT " + consultaSinSelect +
                                                        "                       ) AS CONSULTA0 " +
                                                        "                       WHERE 1=1 AND " + baseCondicion +
                                                        "               ) AS CONSULTA1 " +
                                                        "           " + orderBy +
                                                        "       ) AS CONSULTA2 " +
                                                        "   " + orderByNegativo +
                                                        ") AS CONSULTA3 " +
                                                        orderBy;
            }
            try
            {
                consultaSinSelect = consultaSinSelect.Replace("  ", " ");
                consultaSinSelect = consultaSinSelect.Replace("  ", " ");
                comando = new SqlCommand("Exec ExecReport @consulta", laConexion);
                comando.Parameters.Add("@consulta", SqlDbType.VarChar).Value = consultaSinSelect;
                reader = comando.ExecuteReader();
                filas = "";
                j = 0;
            }
            catch (Exception ex)
            {
                throw new Exception("(Definicion de registros de reportes no valida, revise la cantidad de elementos y numeros de pagina)" + ex.ToString() + "<br><br>" + consultaSinSelect + "<br><br>, la longitud de la cadena es " + consultaSinSelect.Length);
            }
            j = 0;
            while (reader.Read())
            {
                // Se asumen resultados solo como chars 
                i = 0;
                filaTabla = "";
                // Se intenta primero leer como cadena
                try
                {
                    valorCelda = reader.IsDBNull(0) ? "" : reader.GetString(0);
                }
                catch
                {
                    // Se lee como double
                    try
                    {
                        valorCelda = reader.IsDBNull(0) ? "" : quitarCentavos(reader.GetDouble(0).ToString("N3"));
                    }
                    catch
                    {
                        // Se lee como entero    
                        try
                        {
                            valorCelda = reader.IsDBNull(0) ? "" : reader.GetInt32(0).ToString();
                        }
                        catch
                        {
                            // Se lee como smallint
                            try
                            {
                                valorCelda = reader.IsDBNull(0) ? "" : reader.GetInt16(0).ToString();
                            }
                            catch
                            {
                                // Se lee como long
                                try
                                {
                                    valorCelda = reader.IsDBNull(0) ? "" : reader.GetInt64(0).ToString();
                                }
                                catch
                                {
                                    // Se lee como fecha
                                    try
                                    {
                                        valorCelda = reader.IsDBNull(0) ? "" : reader.GetDateTime(0).ToString();
                                    }
                                    catch
                                    {
                                        // Se lee como decimal
                                        valorCelda = reader.IsDBNull(0) ? "" : quitarCentavos(reader.GetDecimal(0).ToString());
                                    }
                                }
                            }
                        }

                    }
                }

                try
                {
                    nombreCelda = reader.IsDBNull(1) ? "" : reader.GetString(1);
                }
                catch
                {
                    // Se lee como double
                    try
                    {
                        nombreCelda = reader.IsDBNull(1) ? "" : quitarCentavos(reader.GetDouble(1).ToString("N3"));
                    }
                    catch
                    {
                        // Se lee como entero    
                        try
                        {
                            nombreCelda = reader.IsDBNull(1) ? "" : reader.GetInt32(1).ToString();
                        }
                        catch
                        {
                            // Se lee como smallint
                            try
                            {
                                nombreCelda = reader.IsDBNull(1) ? "" : reader.GetInt16(1).ToString();
                            }
                            catch
                            {
                                // Se lee como long
                                try
                                {
                                    nombreCelda = reader.IsDBNull(1) ? "" : reader.GetInt64(1).ToString();
                                }
                                catch
                                {
                                    // Se lee como fecha
                                    try
                                    {
                                        nombreCelda = reader.IsDBNull(1) ? "" : reader.GetDateTime(1).ToString();
                                    }
                                    catch
                                    {
                                        // Se lee como decimal
                                        nombreCelda = reader.IsDBNull(1) ? "" : quitarCentavos(reader.GetDecimal(1).ToString());
                                    }
                                }
                            }
                        }

                    }
                }

                textoCelda = plantillaCelda.Replace("{VALORCELDA}", valorCelda);
                textoCelda = textoCelda.Replace("{NOMBRECELDA}", nombreCelda);
                plantillaReemplazada = textoCelda;

                filaTabla = filaTabla + plantillaReemplazada;

                if (j == 0)
                {
                    filaTitulos = filaTitulos + plantillaTitulo.Replace("{VALORTITULO}", tituloCampo);
                }
                i = i + 1;
                textoFilaTitulos = plantillaFilaTitulo.Replace("{FILATITULO}", filaTitulos);
                textoFilaCelda = plantillaFilaCelda.Replace("{FILACELDA}", filaTabla);
                filas = filas + textoFilaCelda;
                j = j + 1;
            }
            valorTabla = plantillaTabla.Replace("{REGISTROS}", filas);
            valorTabla = valorTabla.Replace("{TITULOS}", textoFilaTitulos);
            /* Se genera la paginacion correspondiente */
            hayAnterior = false;
            haySiguiente = false;

            if (this.tipo == 1)
            {
                elementosForm = "<form method=post name=formularioReporte action='seleccionarEntidad.aspx' style='margin: 0 0 0 0'>";
                /* Se agregan los elementos del request */
                elementosForm = elementosForm + "<input type=hidden name='paginaActual' value='" + request["paginaActual"].ToString() + "'>";

                /* Se agregan los elementos del request */
                i = 0;
                cantidadElementos = nombresRequest.Length;
                while (i < cantidadElementos)
                {
                    if (nombresRequest[i] != "paginaActual" && !nombresRequest[i].ToString().StartsWith("inputOrden"))
                    {
                        elementosForm = elementosForm + "<input type=hidden name='" + nombresRequest[i] + "' value='" + valoresRequest[i] + "'>";
                    }
                    i = i + 1;
                }

                //RAM
                elementosForm = elementosForm + "</form>";
                elementosForm = elementosForm + "<script>";
                elementosForm = elementosForm + "function paginaReporte(pagina){";
                elementosForm = elementosForm + "   document.formularioReporte.paginaActual.value = pagina;";
                elementosForm = elementosForm + "   document.formularioReporte.target = '_self';";
                elementosForm = elementosForm + "   document.formularioReporte.submit();";
                elementosForm = elementosForm + "}";
                elementosForm = elementosForm + "</script>";
            }
            else if (this.tipo == 2)
            {
                elementosForm = "";
            }
            else
            {
                elementosForm = "";
            }
            valorTabla = valorTabla.Replace("{FORMULARIO}", elementosForm);
            laNavegacion = "";
            hayAnterior = false;
            haySiguiente = false;
            if (this.tipo == 1)
            {
                if (pagina > 1)
                {
                    hayAnterior = true;
                }
                if (pagina < cantidadPaginas)
                {
                    haySiguiente = true;
                }
                if (!hayAnterior)
                {
                    laNavegacion = plantillaNavegacion.Replace("{LINKANTERIOR}", "#");
                    laNavegacion = laNavegacion.Replace("{INICIOCOMENTARIOANTERIOR}", "<!--");
                    laNavegacion = laNavegacion.Replace("{FINCOMENTARIOANTERIOR}", "-->");
                }
                else
                {
                    laNavegacion = plantillaNavegacion.Replace("{LINKANTERIOR}", "javascript:paginaReporte(" + Convert.ToString(pagina - 1) + ")");
                    laNavegacion = laNavegacion.Replace("{INICIOCOMENTARIOANTERIOR}", "");
                    laNavegacion = laNavegacion.Replace("{FINCOMENTARIOANTERIOR}", "");
                }
                if (!haySiguiente)
                {
                    laNavegacion = laNavegacion.Replace("{LINKSIGUIENTE}", "#");
                    laNavegacion = laNavegacion.Replace("{INICIOCOMENTARIOSIGUIENTE}", "<!--");
                    laNavegacion = laNavegacion.Replace("{FINCOMENTARIOSIGUIENTE}", "-->");

                }
                else
                {
                    laNavegacion = laNavegacion.Replace("{LINKSIGUIENTE}", "javascript:paginaReporte(" + Convert.ToString(pagina + 1) + ")");
                    laNavegacion = laNavegacion.Replace("{INICIOCOMENTARIOSIGUIENTE}", "");
                    laNavegacion = laNavegacion.Replace("{FINCOMENTARIOSIGUIENTE}", "");
                }
            }
            else if (this.tipo == 2)
            {
                laNavegacion = "";
            }
            else
            {
                laNavegacion = "";
            }
            if (conteoRegistros > 0)
            {
                laNavegacion = laNavegacion.Replace("{NOREGISTROS}", "");
            }
            else
            {
                laNavegacion = laNavegacion.Replace("{NOREGISTROS}", "No se encontraron registros para su consulta");
            }

            valorTabla = valorTabla.Replace("{NAVEGACION}", laNavegacion);
            valorTabla = valorTabla.Replace("{TITULO}", nombreReporte);
            reader.Close();
            reader = null;
        }
        else
        {
            throw new Exception("(Definicion de registros de reportes no valida, revise la cantidad de elementos y numeros de pagina)");
        }
        laConexion.Close();
        laConexion = null;
        return valorTabla;
    }

    /// <summary>
    /// Devuelve una tabla con todas las posibles restricciones que se pueden hacer sobre los campos del reporte
    /// </summary>
    /// <returns>string cadena con el formulario de restricciones de un reporte</returns>
    /* Se encarga de leer la informacion de la consulta y mandarla a pantalla */
    public string darFormularioReporte(string nombreReporte, System.Xml.XmlNodeList campos, string[] nombresRequest, string[] valoresRequest, string plantillaTablaConsulta, string plantillaFilaConsulta, string plantillaFilaConsultaSelect, string plantillaFilaConsultaSelectMultiple, string plantillaFilaConsultaFecha, string conexion, string nombreConexion, System.Web.SessionState.HttpSessionState sesion)
    {
        /* Se consulta la cantidad de reguistros de la tabla*/
        string filas = "";
        string fila = "";
        string valorTabla = "";
        string elementosForm = "";
        int cantidadElementos;
        string reemplazo = "";
        int i = 0;
        SqlDataReader reader = null;
        string consultaCombo = "";
        string opciones = "";
        SqlConnection laConexion;
        SqlCommand comando;
        laConexion = new SqlConnection(conexion);
        laConexion.Open();
        int campo_empresa = -2;
        int campo_actual = 0;
        int escogido = 0;
        string colsUsuarios = "";
        string EsEmpresa = "";

        valorTabla = "";
        filas = "";

        /* Se pintan los elementos de las restricciones */
        foreach (System.Xml.XmlNode campo in campos)
        {
            EsEmpresa = "";
            try
            {
                if (campo.Attributes["filtro"].InnerXml.ToString() == "si")
                {
                    try
                    {
                        if (campo.Attributes["select"].InnerXml.ToString() == "si")
                        {
                            reemplazo = plantillaFilaConsultaSelect;
                            reader = null;
                            string query = "";
                            comando = new SqlCommand("Exec ExecReport @consulta", laConexion);
                            //RAM*  mostrara solo los corporativos de las empresas que tiene permiso
                            if (campo.Attributes["campo_valor"].InnerXml == "corporativo_id")
                            {
                                string empresas = "";
                                EmpresaUsers objEmpresaUsers = new EmpresaUsers();
                                objEmpresaUsers.IdUser = Convert.ToInt32(sesion["IdUser"]);
                                DataSet ds = objEmpresaUsers.GetEmpresasUser();
                                for (int indice = 0; indice < ds.Tables[0].Rows.Count; indice++)
                                {
                                    empresas += ds.Tables[0].Rows[indice].ItemArray[0].ToString() + ',';

                                }
                                empresas = empresas.Substring(0, empresas.Length-1);
                                consultaCombo = "SELECT DISTINCT(CAST(a." + campo.Attributes["campo_valor"].InnerXml.ToString() + " AS VARCHAR(20))), a." + campo.Attributes["campo_nombre"].InnerXml.ToString() + " FROM " + campo.Attributes["tabla"].InnerXml.ToString() + " a INNER JOIN SIC_EMPRESA b ON a.corporativo_id = b.corporativo_id WHERE  b.empresa_id IN ("+ empresas +") ORDER BY " + campo.Attributes["campo_nombre"].InnerXml.ToString() + " ASC";
                            }
                            else
                            {
                                consultaCombo = "SELECT CAST(" + campo.Attributes["campo_valor"].InnerXml.ToString() + " AS VARCHAR(20))," + campo.Attributes["campo_nombre"].InnerXml.ToString() + " FROM " + campo.Attributes["tabla"].InnerXml.ToString() + query + " ORDER BY " + campo.Attributes["campo_nombre"].InnerXml.ToString() + " ASC";
                            }
                            comando.Parameters.Add("@consulta", SqlDbType.VarChar).Value = consultaCombo;

                            opciones = "";
                            reader = comando.ExecuteReader();
                            while (reader.Read())
                            {
                                opciones = opciones + "<option value='" + reader.GetString(0) + "'>" + reader.GetString(1) + "</option>";
                            }
                            reader.Close();
                            reader = null;

                            reemplazo = reemplazo.Replace("{OPCIONES}", opciones);
                            if (campo.Attributes["campo_valor"].InnerXml == "empresa_id")
                                EsEmpresa = "1";
                        }
                    }
                    catch
                    {
                        try
                        {
                            if (campo.Attributes["multiple"].InnerXml.ToString() == "si")
                            {
                                // Se revisa si el campo es la escogencia de la empresa 
                                if (campo.Attributes["tabla"].Value.ToString() == "EmpresaDatos")
                                {
                                    try
                                    {
                                        if (sesion["Administrador"].ToString() == "")
                                        {
                                            // Se deja como definido la empresa actual del usuario
                                            reemplazo = "<tr><td colspan=6 style='visibility:hidden;height:0px'><input type=checkbox id=_VISIBLE" + Convert.ToString(i) + " name=_VISIBLE" + Convert.ToString(i) + " value='' style='visibility:hidden'>";
                                            reemplazo = reemplazo + "<input type=checkbox id=_GROUP" + Convert.ToString(i) + " name=_GROUP" + Convert.ToString(i) + " value='' style='visibility:hidden'>";
                                            reemplazo = reemplazo + "<input type=checkbox id=_ORDER" + Convert.ToString(i) + " name=_ORDER" + Convert.ToString(i) + " value='-1' style='visibility:hidden'>";
                                            reemplazo = reemplazo + "<input type=hidden id='_C" + Convert.ToString(i) + "' name='_C" + Convert.ToString(i) + "' value='='>";
                                            reemplazo = reemplazo + "<input type=hidden name=_V" + Convert.ToString(i) + " value='" + sesion["Company"].ToString() + "'>";
                                            reemplazo = reemplazo + "</td></tr>";
                                            campo_empresa = campo_actual;
                                        }
                                        else
                                        {
                                            reemplazo = plantillaFilaConsultaSelectMultiple;
                                            reemplazo = reemplazo.Replace("{TABLABASE}", campo.Attributes["tabla"].InnerXml.ToString());
                                            reemplazo = reemplazo.Replace("{IDTABLABASE}", campo.Attributes["campo_valor"].InnerXml.ToString());
                                            reemplazo = reemplazo.Replace("{NOMBRETABLABASE}", campo.Attributes["campo_nombre"].InnerXml.ToString().Replace("'", "~").Replace("+", "¡"));
                                            reemplazo = reemplazo.Replace("{NOMBREENTIDAD}", campo.Attributes["titulo"].InnerXml.ToString());
                                            reemplazo = reemplazo.Replace("{CADENACONEXION}", nombreConexion);
                                        }

                                    }
                                    catch
                                    {
                                        reemplazo = plantillaFilaConsultaSelectMultiple;
                                        reemplazo = reemplazo.Replace("{TABLABASE}", campo.Attributes["tabla"].InnerXml.ToString());
                                        reemplazo = reemplazo.Replace("{IDTABLABASE}", campo.Attributes["campo_valor"].InnerXml.ToString());
                                        reemplazo = reemplazo.Replace("{NOMBRETABLABASE}", campo.Attributes["campo_nombre"].InnerXml.ToString().Replace("'", "~").Replace("+", "¡"));
                                        reemplazo = reemplazo.Replace("{NOMBREENTIDAD}", campo.Attributes["titulo"].InnerXml.ToString());
                                        reemplazo = reemplazo.Replace("{CADENACONEXION}", nombreConexion);
                                    }
                                }
                                else if (campo.Attributes["tabla"].Value.ToString() == "Users")
                                {
                                    try
                                    {
                                        if (sesion["Administrador"].ToString() == "")
                                        {
                                            // Se deja como definido el identificador del usuario actual
                                            reemplazo = "<tr><td colspan=6 style='visibility:hidden;height:0px'><input type=checkbox id=_VISIBLE" + Convert.ToString(i) + " name=_VISIBLE" + Convert.ToString(i) + " value='' style='visibility:hidden'>";
                                            reemplazo = reemplazo + "<input type=checkbox id=_GROUP" + Convert.ToString(i) + " name=_GROUP" + Convert.ToString(i) + " value='' style='visibility:hidden'>";
                                            reemplazo = reemplazo + "<input type=checkbox id=_ORDER" + Convert.ToString(i) + " name=_ORDER" + Convert.ToString(i) + " value='-1' style='visibility:hidden'>";
                                            reemplazo = reemplazo + "<input type=hidden id='_C" + Convert.ToString(i) + "' name='_C" + Convert.ToString(i) + "' value='=NULL'>";
                                            reemplazo = reemplazo + "<input type=hidden name=_V" + Convert.ToString(i) + " value='" + sesion["IdUser"].ToString() + "'>";
                                            reemplazo = reemplazo + "</td></tr>";
                                        }
                                        else
                                        {
                                            if (escogido == 0)
                                            {
                                                reemplazo = plantillaFilaConsultaSelectMultiple;
                                                reemplazo = reemplazo.Replace("{TABLABASE}", campo.Attributes["tabla"].InnerXml.ToString());
                                                reemplazo = reemplazo.Replace("{IDTABLABASE}", campo.Attributes["campo_valor"].InnerXml.ToString());
                                                reemplazo = reemplazo.Replace("{NOMBRETABLABASE}", campo.Attributes["campo_nombre"].InnerXml.ToString().Replace("'", "~").Replace("+", "¡"));
                                                reemplazo = reemplazo.Replace("{NOMBREENTIDAD}", campo.Attributes["titulo"].InnerXml.ToString());
                                                reemplazo = reemplazo.Replace("{CADENACONEXION}", nombreConexion);
                                                colsUsuarios = i.ToString();
                                                escogido = 1;
                                            }
                                            else
                                            {
                                                // Se deja marcado el basico
                                                reemplazo = "<tr><td colspan=6 style='visibility:hidden;height:0px'><input type=checkbox id=_VISIBLE" + Convert.ToString(i) + " name=_VISIBLE" + Convert.ToString(i) + " checked value='visible' style='visibility:hidden'>";
                                                reemplazo = reemplazo + "<input type=checkbox id=_GROUP" + Convert.ToString(i) + " name=_GROUP" + Convert.ToString(i) + " value='' style='visibility:hidden'>";
                                                reemplazo = reemplazo + "<input type=checkbox id=_ORDER" + Convert.ToString(i) + " name=_ORDER" + Convert.ToString(i) + " value='-1' style='visibility:hidden'>";
                                                reemplazo = reemplazo + "<input type=hidden id='_C" + Convert.ToString(i) + "' name='_C" + Convert.ToString(i) + "' value=''>";
                                                reemplazo = reemplazo + "<input type=hidden name=_V" + Convert.ToString(i) + " value=''>";
                                                reemplazo = reemplazo + "</td></tr>";
                                                colsUsuarios = colsUsuarios + "," + i.ToString();
                                            }
                                        }

                                    }
                                    catch
                                    {
                                        reemplazo = plantillaFilaConsultaSelectMultiple;
                                        reemplazo = reemplazo.Replace("{TABLABASE}", campo.Attributes["tabla"].InnerXml.ToString());
                                        reemplazo = reemplazo.Replace("{IDTABLABASE}", campo.Attributes["campo_valor"].InnerXml.ToString());
                                        reemplazo = reemplazo.Replace("{NOMBRETABLABASE}", campo.Attributes["campo_nombre"].InnerXml.ToString().Replace("'", "~").Replace("+", "¡"));
                                        reemplazo = reemplazo.Replace("{NOMBREENTIDAD}", campo.Attributes["titulo"].InnerXml.ToString());
                                        reemplazo = reemplazo.Replace("{CADENACONEXION}", nombreConexion);
                                    }

                                }

                                else
                                {
                                    reemplazo = plantillaFilaConsultaSelectMultiple;
                                    reemplazo = reemplazo.Replace("{TABLABASE}", campo.Attributes["tabla"].InnerXml.ToString());
                                    reemplazo = reemplazo.Replace("{IDTABLABASE}", campo.Attributes["campo_valor"].InnerXml.ToString());
                                    reemplazo = reemplazo.Replace("{NOMBRETABLABASE}", campo.Attributes["campo_nombre"].InnerXml.ToString().Replace("'", "~").Replace("+", "¡"));
                                    reemplazo = reemplazo.Replace("{NOMBREENTIDAD}", campo.Attributes["titulo"].InnerXml.ToString());
                                    reemplazo = reemplazo.Replace("{CADENACONEXION}", nombreConexion);
                                }
                            }
                        }
                        catch
                        {
                            try
                            {
                                if (campo.Attributes["fecha"].InnerXml.ToString() == "si")
                                {
                                    reemplazo = plantillaFilaConsultaFecha;
                                }
                            }
                            catch
                            {
                                if (campo.Attributes["titulo"].InnerXml.ToString() == "NOMBRE EMPRESA" && (campo_actual - 1) == campo_empresa)
                                {
                                    // Se deja como definido la empresa actual del usuario
                                    reemplazo = "<tr><td colspan=6 style='visibility:hidden;height:0px'><input type=checkbox id=_VISIBLE" + Convert.ToString(i) + " name=_VISIBLE" + Convert.ToString(i) + " value='' style='visibility:hidden'>";
                                    reemplazo = reemplazo + "<input type=checkbox id=_GROUP" + Convert.ToString(i) + " name=_GROUP" + Convert.ToString(i) + " value='' value='' style='visibility:hidden'>";
                                    reemplazo = reemplazo + "<input type=checkbox id=_ORDER" + Convert.ToString(i) + " name=_ORDER" + Convert.ToString(i) + " value='-1' style='visibility:hidden'>";
                                    reemplazo = reemplazo + "<input type=hidden id='_C" + Convert.ToString(i) + "' name='_C" + Convert.ToString(i) + "' value=''>";
                                    reemplazo = reemplazo + "<input type=hidden name=_V" + Convert.ToString(i) + " value=''>";
                                    reemplazo = reemplazo + "</td></tr>";
                                    fila = reemplazo;
                                }
                                else
                                {
                                    reemplazo = plantillaFilaConsulta;
                                }
                            }
                        }
                    }

                    fila = reemplazo.Replace("{NOMBRECAMPO}", campo.Attributes["titulo"].InnerXml.ToString());
                    fila = fila.Replace("{POSICIONCAMPO}", Convert.ToString(i));
                    fila = fila.Replace("{CAMPOFILTRO1}", "");
                    fila = fila.Replace("{FINCAMPOFILTRO1}", "");
                    fila = fila.Replace("{CAMPOFILTRO2}", "");
                    fila = fila.Replace("{FINCAMPOFILTRO2}", "");
                    fila = fila.Replace("{CAMPOFILTRO3}", "");
                    fila = fila.Replace("{FINCAMPOFILTRO3}", "");

                }
                else
                {
                    if (campo.Attributes["titulo"].InnerXml.ToString() == "NOMBRE EMPRESA" && (campo_actual - 1) == campo_empresa)
                    {
                        // Se deja como definido la empresa actual del usuario
                        reemplazo = "<tr><td colspan=6 style='visibility:hidden;height:0px'><input type=checkbox id=_VISIBLE" + Convert.ToString(i) + " name=_VISIBLE" + Convert.ToString(i) + " checked value=visible style='visibility:hidden'>";
                        reemplazo = reemplazo + "<input type=checkbox id=_GROUP" + Convert.ToString(i) + " name=_GROUP" + Convert.ToString(i) + " value='grupo' value=visible style='visibility:hidden'>";
                        reemplazo = reemplazo + "<input type=checkbox id=_ORDER" + Convert.ToString(i) + " name=_ORDER" + Convert.ToString(i) + " value='" + Convert.ToString(i) + "' style='visibility:hidden'>";
                        reemplazo = reemplazo + "<input type=hidden id='_C" + Convert.ToString(i) + "' name='_C" + Convert.ToString(i) + "' value=''>";
                        reemplazo = reemplazo + "<input type=hidden name=_V" + Convert.ToString(i) + " value=''>";
                        reemplazo = reemplazo + "</td></tr>";
                        fila = reemplazo;
                    }
                    else
                    {
                        reemplazo = plantillaFilaConsulta;
                        fila = reemplazo.Replace("{NOMBRECAMPO}", campo.Attributes["titulo"].InnerXml.ToString() + "<input type=hidden name=_C" + i + "><input type=hidden name=_V" + i + "><input type=hidden name=_2V" + i + ">");
                        fila = fila.Replace("{POSICIONCAMPO}", Convert.ToString(i));
                        fila = fila.Replace("{CAMPOFILTRO1}", "<!--");
                        fila = fila.Replace("{FINCAMPOFILTRO1}", "-->");
                        fila = fila.Replace("{CAMPOFILTRO2}", "<!--");
                        fila = fila.Replace("{FINCAMPOFILTRO2}", "-->");
                        fila = fila.Replace("{CAMPOFILTRO3}", "<!--");
                        fila = fila.Replace("{FINCAMPOFILTRO3}", "-->");
                    }
                }
            }
            catch
            {
                reemplazo = plantillaFilaConsulta;
                fila = reemplazo.Replace("{NOMBRECAMPO}", campo.Attributes["titulo"].InnerXml.ToString() + "<input type=hidden name=_C" + i + "><input type=hidden name=_V" + i + "><input type=hidden name=_2V" + i + ">");
                fila = fila.Replace("{POSICIONCAMPO}", Convert.ToString(i));
                fila = fila.Replace("{CAMPOFILTRO1}", "<!--");
                fila = fila.Replace("{FINCAMPOFILTRO1}", "-->");
                fila = fila.Replace("{CAMPOFILTRO2}", "<!--");
                fila = fila.Replace("{FINCAMPOFILTRO2}", "-->");
                fila = fila.Replace("{CAMPOFILTRO3}", "<!--");
                fila = fila.Replace("{FINCAMPOFILTRO3}", "-->");
            }
            try
            {
                if (campo.Attributes["acumulado"].InnerXml.ToString() == "si")
                {
                    fila = fila.Replace("{AGRUPABLE}", "<!--");
                    fila = fila.Replace("{FINAGRUPABLE}", "-->");
                    fila = fila.Replace("{ORDENABLE}", "<!--");
                    fila = fila.Replace("{FINORDENABLE}", "-->");
                }
                else
                {
                    fila = fila.Replace("{AGRUPABLE}", "");
                    fila = fila.Replace("{FINAGRUPABLE}", "");
                    fila = fila.Replace("{ORDENABLE}", "");
                    fila = fila.Replace("{FINORDENABLE}", "");
                }
            }
            catch
            {
                fila = fila.Replace("{AGRUPABLE}", "");
                fila = fila.Replace("{FINAGRUPABLE}", "");

                fila = fila.Replace("{ORDENABLE}", "");
                fila = fila.Replace("{FINORDENABLE}", "");
            }

            filas = filas + fila;

            if (campo.Attributes["titulo"].InnerXml.ToString() == "EMPRESA ID")
            {
                filas = filas.Replace("<a href=\"#\" onclick=\"javascript:seleccionarEntidad('EMPRESA ID'", "<a href=\"#\" id=\"controlEmpresa\" onclick=\"javascript:seleccionarEntidad('EMPRESA ID'");
                filas = filas.Replace("if(this.options.selectedIndex != 0 && a.options.selectedIndex == 0){a.options.selectedIndex = 2;}"," $(\"#_V0\").removeClass(\"textBoxError\"); $(\"#_V0\").addClass(\"textBox\");");
            }
            i = i + 1;
            campo_actual = campo_actual + 1;
        }
        valorTabla = plantillaTablaConsulta.Replace("{FILASCONDICIONES}", filas);
        valorTabla = valorTabla.Replace("{_NUMFILAS_}", i.ToString());
        elementosForm = "<form method=post name=reporte action='reporte.aspx' style='margin: 0 0 0 0'>";
        elementosForm = elementosForm + "<input type=hidden name=colsUsuarios value='" + colsUsuarios + "'>";
        /* Se agregan los elementos del request */
        i = 0;
        cantidadElementos = nombresRequest.Length;
        while (i < cantidadElementos)
        {
            elementosForm = elementosForm + "<input type=hidden name='" + nombresRequest[i] + "' value='" + valoresRequest[i] + "'>";
            i = i + 1;
        }
        elementosForm = elementosForm + "<input type=hidden name='paginaActual' value='1'>";
        valorTabla = valorTabla.Replace("{FORMULARIO}", elementosForm);
        valorTabla = valorTabla.Replace("{TITULO}", nombreReporte);


        laConexion.Close();
        laConexion = null;
        return valorTabla;
    }
    /// <summary>
    /// Devuelve una tabla con todas las posibles restricciones de un formulario de seleccion multiple como empresas o usuarios
    /// </summary>
    /// <returns>string cadena con el formulario de restricciones de una selección de este tipo</returns>
    /* Se encarga de leer la informacion de la consulta y mandarla a pantalla */
    public string darFormularioReporteSelector(string nombreReporte,
        string tabla,
        string campo_valor,
        string campo_nombre,
        string nombreCampo,
        string tituloCampo,
        string plantillaTablaConsulta,
        string plantillaFilaConsulta,
        string conexion,
        string corporativo,
        string condicion
        )
    {
        /* Se consulta la cantidad de reguistros de la tabla*/
        string filas = "";
        string fila = "";
        string valorTabla = "";
        string elementosForm = "";
        string reemplazo = "";

        valorTabla = "";
        filas = "";
        reemplazo = plantillaFilaConsulta;
        tituloCampo = tituloCampo.Replace(" ID", "");
        tituloCampo = tituloCampo.Replace("ID ", "");
        fila = reemplazo.Replace("{NOMBRECAMPO}", tituloCampo);
        fila = fila.Replace("{POSICIONCAMPO}", "0");
        filas = filas + fila;

        valorTabla = plantillaTablaConsulta.Replace("{FILASCONDICIONES}", filas);
        elementosForm = "<form method=post name=reporte action='seleccionarEntidad.aspx' style='margin: 0 0 0 0'>";
        /* Se agregan los elementos del request */
        elementosForm = elementosForm + "<input type=hidden name='paginaActual' value='1'>";
        elementosForm = elementosForm + "<input type=hidden name='conexion' value='" + conexion + "'>";
        elementosForm = elementosForm + "<input type=hidden name='tabla' value='" + tabla + "'>";
        elementosForm = elementosForm + "<input type=hidden name='campo_valor' value='" + campo_valor + "'>";
        elementosForm = elementosForm + "<input type=hidden name='campo_nombre' value='" + campo_nombre + "'>";
        elementosForm = elementosForm + "<input type=hidden name='nombreCampo' value='" + nombreCampo + "'>";
        elementosForm = elementosForm + "<input type=hidden name='tituloCampo' value='" + tituloCampo + "'>";
        elementosForm = elementosForm + "<input type=hidden id='idCorporativo' name='corporativo' value='" + corporativo + "'>";
        elementosForm = elementosForm + "<input type=hidden name='condicion' value='" + condicion + "'>";
        valorTabla = valorTabla.Replace("{FORMULARIO}", elementosForm);
        valorTabla = valorTabla.Replace("{TITULO}", nombreReporte);
        return valorTabla;
    }

    /// <summary>
    /// Informa si un valor se encuentra dentro de un arreglo o no
    /// </summary>
    /// <returns>bool Información de si el valor existe o no</returns>
    private bool enArreglo(string[] arreglo, string valor)
    {
        bool resultado = false;
        int i = 0;
        while (i < arreglo.Length && resultado == false)
        {
            if (arreglo[i] == valor)
            {
                resultado = true;
            }
            i = i + 1;
        }
        return resultado;
    }
    /// <summary>
    /// Formatea los centavos que devuelven los manejadores de base de datos SQL Server en campos float, tipicamente ,0000
    /// </summary>
    /// <returns>string cadena formateada con la informacion del float</returns>
    private string quitarCentavos(string cadena)
    {
        //Elimina los 3 ultimos caracteres de centavos de una cadena
        string resultado = "";
        resultado = cadena.Substring(0, cadena.Length);
        return resultado;
    }
    /// <summary>
    /// Descompone un XML que representa un reporte para mostrar los resultados correspondientes, obtiene el directorio en el cual se encuentran las plantillas asociadas
    /// </summary>
    /* Se lee el XML correspondiente para crear los resultados del reporte */
    public string XMLReporte(string archivoXML, string directorioXMLs, string dirBasePlantillas, string loginUsuario, string loginRPC, System.Web.HttpRequest request, int pagina, int numeroRegistrosPagina, System.Web.SessionState.HttpSessionState sesion)
    {
        System.Xml.XmlNode reporte;
        System.Xml.XmlDocument objXMLDocument;
        System.Xml.XmlTextReader objXMLTextReader;
        System.Xml.XmlValidatingReader objXMLValidatingReader;
        string consultaBase;
        string dirPlantillas;
        string nombreReporte;
        string[] nombresRequest;
        string[] valoresRequest;
        int i;
        int j;
        string plantillaTabla;
        string plantillaFilaTitulo;
        string plantillaTitulo;
        string plantillaFilaCelda;
        string plantillaCelda;
        string plantillaNavegacion;
        StreamReader archivoLectura;
        char[] splits;
        string resultado;
        string condicionesAdicionales;
        string baseCondicion = "";
        string cadenaConexion = "";
        int cantidad_parametros = 0;
        int indice_parametro = 0;
        string parametrosAnteriores = "";
        string nombreDivAnterior = "";
        string[] elementosOR;
        string elOR = "";
        int indice_elOR = 0;
        string restriccionSesionEmpresa = "";
        string elTitulo = "";
        string[] arregloColUsuarios;
        int z = 1;

        splits = new char[1];
        splits[0] = Convert.ToChar(",");


        objXMLTextReader = new System.Xml.XmlTextReader(directorioXMLs + archivoXML);
        objXMLDocument = new System.Xml.XmlDocument();
        objXMLValidatingReader = new System.Xml.XmlValidatingReader(objXMLTextReader);
        resultado = "";
        condicionesAdicionales = "";
        dirPlantillas = "";
        arregloColUsuarios = request["colsUsuarios"].ToString().Split(splits);
        try
        {

            objXMLDocument.Load(objXMLValidatingReader);

            reporte = objXMLDocument.SelectSingleNode("/Reporte");
            // Se leen cada uno de los elementos de reemplazo principales 
            consultaBase = reporte.Attributes["Consulta"].InnerXml;
            if (this.tipo == 1)
            {
                dirPlantillas = dirBasePlantillas + reporte.Attributes["DirPlantillas"].InnerXml;
            }
            else if (this.tipo == 2)
            {
                dirPlantillas = dirBasePlantillas + "Excel/";
            }
            else if (this.tipo == 3)
            {
                dirPlantillas = dirBasePlantillas + "Word/";
            }
            else
            {
            }
            nombreReporte = reporte.Attributes["NombreReporte"].InnerXml;
            cadenaConexion = reporte.Attributes["cadenaConexion"].InnerXml;

            try
            {
                elTitulo = reporte.Attributes["Titulo"].InnerXml;
            }
            catch
            {
                elTitulo = "";
            }

            /* Se leen cada uno de los campos para armar los filtros */
            System.Xml.XmlNodeList campos = reporte.ChildNodes[0].ChildNodes;

            try
            {
                cantidad_parametros = Convert.ToInt16(reporte.Attributes["cantidad_parametros"].InnerXml);
            }
            catch
            {
                cantidad_parametros = 0;
            }

            nombresRequest = new string[request.Form.Keys.Count + request.QueryString.Count];
            valoresRequest = new string[request.Form.Keys.Count + request.QueryString.Count];
            i = 0;
            while (i < request.Form.Keys.Count)
            {
                nombresRequest[i] = request.Form.Keys.Get(i);
                valoresRequest[i] = Encoder.HtmlEncode(request.Form.GetValues(i).GetValue(0).ToString());
                i = i + 1;
            }

            while (i < (request.Form.Keys.Count + request.QueryString.Count))
            {
                nombresRequest[i] = request.QueryString.GetKey(i - request.Form.Keys.Count).ToString();
                valoresRequest[i] = Encoder.HtmlEncode(request.QueryString.GetValues(i - request.Form.Keys.Count).GetValue(0).ToString());
                i = i + 1;
            }

            /* Se hace una lectura de lod valores de los campos */
            i = 0;
            foreach (System.Xml.XmlNode campo in campos)
            {
                j = i + 1;
                /* Si el campo tiene restriccion se agrega la consulta */
                try
                {
                    if (request["_C" + i].ToString() != "")
                    {
                        if (request["_C" + i].ToString() == "=")
                        {
                            try
                            {
                                if (campo.Attributes["fecha"].InnerXml.ToString() == "si")
                                {
                                    baseCondicion = "CONVERT(datetime,CONVERT(char,COL" + j.ToString() + ",103),103)=CONVERT(datetime,'" + request["_V" + i].ToString() + "',103)";
                                }
                                else
                                {
                                    elementosOR = request["_V" + i].ToString().Split(splits);
                                    elOR = "(";
                                    indice_elOR = 0;
                                    while (indice_elOR < elementosOR.Length)
                                    {
                                        if (indice_elOR > 0)
                                        {
                                            elOR = elOR + " OR ";
                                        }
                                        elOR = elOR + "COL" + j.ToString() + "='" + elementosOR[indice_elOR].ToString() + "'";
                                        indice_elOR = indice_elOR + 1;
                                    }
                                    // Se revisa que este dentro del arreglo de Usuarios
                                    if (enArreglo(arregloColUsuarios, i.ToString()))
                                    {
                                        z = 1;
                                        while (z < arregloColUsuarios.Length)
                                        {
                                            elementosOR = request["_V" + i].ToString().Split(splits);
                                            indice_elOR = 0;
                                            while (indice_elOR < elementosOR.Length)
                                            {
                                                elOR = elOR + " OR ";
                                                elOR = elOR + "COL" + (Convert.ToInt32(arregloColUsuarios[z].ToString()) + 1).ToString() + "='" + elementosOR[indice_elOR].ToString() + "'";
                                                indice_elOR = indice_elOR + 1;
                                            }
                                            z = z + 1;
                                        }
                                    }

                                    elOR = elOR + ")";
                                    baseCondicion = elOR;
                                }
                            }
                            catch
                            {
                                elementosOR = request["_V" + i].ToString().Split(splits);
                                elOR = "(";
                                indice_elOR = 0;
                                while (indice_elOR < elementosOR.Length)
                                {
                                    if (indice_elOR > 0)
                                    {
                                        elOR = elOR + " OR ";
                                    }
                                    elOR = elOR + "COL" + j.ToString() + "='" + elementosOR[indice_elOR].ToString() + "'";
                                    indice_elOR = indice_elOR + 1;
                                }
                                // Se revisa que este dentro del arreglo de Usuarios
                                if (enArreglo(arregloColUsuarios, i.ToString()))
                                {
                                    z = 1;
                                    while (z < arregloColUsuarios.Length)
                                    {
                                        elementosOR = request["_V" + i].ToString().Split(splits);
                                        indice_elOR = 0;
                                        while (indice_elOR < elementosOR.Length)
                                        {
                                            elOR = elOR + " OR ";
                                            elOR = elOR + "COL" + (Convert.ToInt32(arregloColUsuarios[z].ToString()) + 1).ToString() + "='" + elementosOR[indice_elOR].ToString() + "'";
                                            indice_elOR = indice_elOR + 1;
                                        }
                                        z = z + 1;
                                    }
                                }
                                elOR = elOR + ")";
                                baseCondicion = elOR;

                            }
                        }
                        else if (request["_C" + i].ToString() == "=NULL")
                        {
                            try
                            {
                                if (campo.Attributes["fecha"].InnerXml.ToString() == "si")
                                {
                                    baseCondicion = "(CONVERT(datetime,CONVERT(char,COL" + j.ToString() + ",103),103)=CONVERT(datetime,'" + request["_V" + i].ToString() + "',103) OR COL" + j.ToString() + " IS NULL)";
                                }
                                else
                                {
                                    elementosOR = request["_V" + i].ToString().Split(splits);
                                    elOR = "(";
                                    indice_elOR = 0;
                                    while (indice_elOR < elementosOR.Length)
                                    {
                                        if (indice_elOR > 0)
                                        {
                                            elOR = elOR + " OR ";
                                        }
                                        elOR = elOR + "(COL" + j.ToString() + "='" + elementosOR[indice_elOR].ToString() + "' OR COL" + j.ToString() + " IS NULL)";
                                        indice_elOR = indice_elOR + 1;
                                    }
                                    elOR = elOR + ")";
                                    baseCondicion = elOR;
                                }
                            }
                            catch
                            {
                                elementosOR = request["_V" + i].ToString().Split(splits);
                                elOR = "(";
                                indice_elOR = 0;
                                while (indice_elOR < elementosOR.Length)
                                {
                                    if (indice_elOR > 0)
                                    {
                                        elOR = elOR + " OR ";
                                    }
                                    elOR = elOR + "(COL" + j.ToString() + "='" + elementosOR[indice_elOR].ToString() + "' OR COL" + j.ToString() + " IS NULL)";
                                    indice_elOR = indice_elOR + 1;
                                }
                                elOR = elOR + ")";
                                baseCondicion = elOR;

                            }
                        }

                        else if (request["_C" + i].ToString() == "LIKE")
                        {
                            try
                            {
                                if (campo.Attributes["fecha"].InnerXml.ToString() == "si")
                                {
                                }
                                else
                                {
                                    baseCondicion = "COL" + j.ToString() + " LIKE '%" + request["_V" + i].ToString() + "%'";
                                }
                            }
                            catch
                            {
                                baseCondicion = "COL" + j.ToString() + " LIKE '%" + request["_V" + i].ToString() + "%'";
                            }
                        }
                        else if (request["_C" + i].ToString() == "GT")
                        {
                            try
                            {
                                if (campo.Attributes["fecha"].InnerXml.ToString() == "si")
                                {
                                    baseCondicion = "CONVERT(datetime,CONVERT(char,COL" + j.ToString() + ",103),103) > CONVERT(datetime,'" + request["_V" + i].ToString() + "',103)";
                                }
                                else
                                {
                                    baseCondicion = "COL" + j.ToString() + " > '" + request["_V" + i].ToString() + "'";
                                }
                            }
                            catch
                            {
                                baseCondicion = "COL" + j.ToString() + " > '" + request["_V" + i].ToString() + "'";
                            }
                        }
                        else if (request["_C" + i].ToString() == "GTE")
                        {
                            try
                            {
                                if (campo.Attributes["fecha"].InnerXml.ToString() == "si")
                                {
                                    baseCondicion = "CONVERT(datetime,CONVERT(char,COL" + j.ToString() + ",103),103) >= CONVERT(datetime,'" + request["_V" + i].ToString() + "',103)";
                                }
                                else
                                {
                                    baseCondicion = "COL" + j.ToString() + " >= '" + request["_V" + i].ToString() + "'";
                                }
                            }
                            catch
                            {
                                baseCondicion = "COL" + j.ToString() + " >= '" + request["_V" + i].ToString() + "'";
                            }
                        }
                        else if (request["_C" + i].ToString() == "LT")
                        {
                            try
                            {
                                if (campo.Attributes["fecha"].InnerXml.ToString() == "si")
                                {
                                    baseCondicion = "CONVERT(datetime,CONVERT(char,COL" + j.ToString() + ",103),103) < CONVERT(datetime,'" + request["_V" + i].ToString() + "',103)";
                                }
                                else
                                {
                                    baseCondicion = "COL" + j.ToString() + " < '" + request["_V" + i].ToString() + "'";
                                }
                            }
                            catch
                            {
                                baseCondicion = "COL" + j.ToString() + " < '" + request["_V" + i].ToString() + "'";
                            }
                        }
                        else if (request["_C" + i].ToString() == "LTE")
                        {
                            try
                            {
                                if (campo.Attributes["fecha"].InnerXml.ToString() == "si")
                                {
                                    baseCondicion = "CONVERT(datetime,CONVERT(char,COL" + j.ToString() + ",103),103) <= CONVERT(datetime,'" + request["_V" + i].ToString() + "',103)";
                                }
                                else
                                {
                                    baseCondicion = "COL" + j.ToString() + " <= '" + request["_V" + i].ToString() + "'";
                                }
                            }
                            catch
                            {
                                baseCondicion = "COL" + j.ToString() + " <= '" + request["_V" + i].ToString() + "'";
                            }
                        }
                        else if (request["_C" + i].ToString() == "BETWEEN")
                        {
                            try
                            {
                                if (campo.Attributes["fecha"].InnerXml.ToString() == "si")
                                {
                                    baseCondicion = "CONVERT(datetime,CONVERT(char,COL" + j.ToString() + ",103),103)  BETWEEN CONVERT(datetime,'" + request["_V" + i].ToString() + "',103) AND CONVERT(datetime,'" + request["_2V" + i].ToString() + "',103)";
                                }
                                else
                                {
                                    baseCondicion = "COL" + j.ToString() + " BETWEEN '" + request["_V" + i].ToString() + "' AND '" + request["_2V" + i].ToString() + "'";
                                }
                            }
                            catch
                            {
                                baseCondicion = "COL" + j.ToString() + " BETWEEN '" + request["_V" + i].ToString() + "' AND '" + request["_2V" + i].ToString() + "'";
                            }
                        }
                        else
                        {
                            baseCondicion = "";
                        }
                        if (baseCondicion != "")
                        {
                            condicionesAdicionales = condicionesAdicionales + " AND " + baseCondicion;
                        }
                    }
                }
                catch
                {
                }
                i = i + 1;
            }

            try
            {
                restriccionSesionEmpresa = reporte.Attributes["restriccionSesionEmpresa"].InnerXml;
            }
            catch
            {
                restriccionSesionEmpresa = "";
            }

            #region Restricción Sesión

            if (restriccionSesionEmpresa != "")
            {
                try
                {
                    //if (sesion["Company"].ToString() != "")
                    //{
                    //    restriccionSesionEmpresa = restriccionSesionEmpresa.Replace("{SESSIONEMPRESAID}", sesion["Company"].ToString());
                    //}
                    //else
                    //{
                    //    restriccionSesionEmpresa = "";
                    //}

                    //Inicio 05/08/2010 MAHG Se agrega la restricción de la empresa

                    //Fin 05/08/2010 MAHG
                }
                catch
                {
                    restriccionSesionEmpresa = "";
                }
            }
            if (restriccionSesionEmpresa != "")
            {
                consultaBase = consultaBase + " AND " + restriccionSesionEmpresa;
            }

            #endregion

            //#region Se restringen las empresas a las que tiene permisos el usuario

            //DataSet dsEmpresas;
            //EmpresaUsers objEmpresaUsers = new EmpresaUsers();
            //objEmpresaUsers.IdUser = Convert.ToInt32(Session["IdUser"]);

            //dsEmpresas = objEmpresaUsers.GetEmpresasUser();

            //int i = 0;
            //foreach(DataRow row in dsEmpresas.Tables[0].Rows)
            //{

            //    if (dsEmpresas.Tables[0].Rows.Count <= i)
            //    {
            //        string strEmpresas = row["empresa_id"].ToString();
            //    }

            //    i++;
            //}



            //#endregion


            consultaBase = consultaBase.Replace("&gt;", ">");
            consultaBase = consultaBase.Replace("&lt;", "<");

            // Se procesan los parametros de traspaso
            int pos_param = 0;
            while (indice_parametro < cantidad_parametros)
            {
                pos_param = indice_parametro + 1;
                consultaBase = consultaBase.Replace("{VALPARAMETRO" + pos_param + "}", request["PARAM" + pos_param].ToString());
                if (parametrosAnteriores != "")
                {
                    parametrosAnteriores = parametrosAnteriores + "&";
                }
                parametrosAnteriores = parametrosAnteriores + "PARAM" + pos_param + "=" + request["PARAM" + pos_param];
                indice_parametro = indice_parametro + 1;
            }
            try
            {
                nombreDivAnterior = request["nombreDivAnterior"];
            }
            catch
            {
                nombreDivAnterior = "";
            }

            /* Lee la plantillas de las tablas */
            archivoLectura = new StreamReader(dirPlantillas + "Tabla.html");
            plantillaTabla = archivoLectura.ReadToEnd();
            archivoLectura.Close();

            /* Lee la plantillas de fila de titulo */
            archivoLectura = new StreamReader(dirPlantillas + "FilaTitulo.html");
            plantillaFilaTitulo = archivoLectura.ReadToEnd();
            archivoLectura.Close();

            /* Lee la plantillas de titulo */
            archivoLectura = new StreamReader(dirPlantillas + "Titulo.html");
            plantillaTitulo = archivoLectura.ReadToEnd();
            archivoLectura.Close();

            /* Lee la plantillas de fila de celda */
            archivoLectura = new StreamReader(dirPlantillas + "FilaCelda.html");
            plantillaFilaCelda = archivoLectura.ReadToEnd();
            archivoLectura.Close();

            /* Lee la plantillas de celda */
            archivoLectura = new StreamReader(dirPlantillas + "Celda.html");
            plantillaCelda = archivoLectura.ReadToEnd();
            archivoLectura.Close();

            /* Lee la plantillas de navegacion */
            archivoLectura = new StreamReader(dirPlantillas + "Navegacion.html");
            plantillaNavegacion = archivoLectura.ReadToEnd();
            archivoLectura.Close();

            resultado = this.darReporte(nombreReporte, campos, nombresRequest, valoresRequest, request.Path, consultaBase, condicionesAdicionales, pagina, numeroRegistrosPagina, plantillaTabla, plantillaTitulo, plantillaFilaTitulo, plantillaCelda, plantillaFilaCelda, plantillaNavegacion, System.Configuration.ConfigurationManager.ConnectionStrings[cadenaConexion].ToString(), cantidad_parametros, nombreDivAnterior, parametrosAnteriores, request, elTitulo);
        }
        catch (Exception ex)
        {
            //GAMM. Information Leakage. No se debe mostrar el mensje de error.

            /* Se imprime el valor de la excepcion */
            //resultado = ex.ToString();
            throw new Exception("Error");
        }

        /* Se libera el archivo xml */
        objXMLValidatingReader.Close();
        objXMLValidatingReader = null;
        objXMLTextReader = null;
        objXMLDocument = null;

        return resultado;
    }
    /// <summary>
    /// A partir de un xml que define un reporte calcula como se debe visualizar obteniendo las plantillas necesarias
    /// </summary>
    /* Se lee el XML correspondiente para generar un formulario de busqueda */
    public string formularioXMLReporte(string archivoXML, string directorioXMLs, string dirBasePlantillas, System.Web.HttpRequest request, System.Web.SessionState.HttpSessionState sesion)
    {
        System.Xml.XmlNode reporte;
        System.Xml.XmlDocument objXMLDocument;
        System.Xml.XmlTextReader objXMLTextReader;
        System.Xml.XmlValidatingReader objXMLValidatingReader;
        string consultaBase;
        string dirPlantillas;
        string nombreReporte;
        string titulos;
        string[] nombresRequest;
        string[] valoresRequest;
        int i;
        string plantillaTablaConsulta;
        string plantillaFilaCondicion;
        string plantillaFilaCondicionSelect;
        string plantillaFilaCondicionSelectMultiple;
        string plantillaFilaCondicionFecha;
        StreamReader archivoLectura;
        char[] splits;
        string resultado;
        string cadenaConexion = "";

        splits = new char[1];
        splits[0] = Convert.ToChar(",");

        objXMLTextReader = new System.Xml.XmlTextReader(directorioXMLs + archivoXML);
        objXMLDocument = new System.Xml.XmlDocument();
        objXMLValidatingReader = new System.Xml.XmlValidatingReader(objXMLTextReader);
        resultado = "";
        try
        {

            objXMLDocument.Load(objXMLValidatingReader);

            reporte = objXMLDocument.SelectSingleNode("/Reporte");
            // Se leen cada uno de los elementos de reemplazo 
            consultaBase = reporte.Attributes["Consulta"].InnerXml;
            dirPlantillas = dirBasePlantillas + reporte.Attributes["DirPlantillas"].InnerXml;
            nombreReporte = reporte.Attributes["NombreReporte"].InnerXml;
            cadenaConexion = reporte.Attributes["cadenaConexion"].InnerXml;

            // Se leen cada uno de los campos para armar los filtros
            System.Xml.XmlNodeList campos = reporte.ChildNodes[0].ChildNodes;

            nombresRequest = new string[request.Form.Keys.Count + request.QueryString.Count];
            valoresRequest = new string[request.Form.Keys.Count + request.QueryString.Count];
            i = 0;
            while (i < request.Form.Keys.Count)
            {
                nombresRequest[i] = request.Form.Keys.Get(i);
                valoresRequest[i] = request.Form.GetValues(i).GetValue(0).ToString();
                i = i + 1;
            }

            while (i < (request.Form.Keys.Count + request.QueryString.Count))
            {
                nombresRequest[i] = request.QueryString.GetKey(i - request.Form.Keys.Count).ToString();
                valoresRequest[i] = request.QueryString.GetValues(i - request.Form.Keys.Count).GetValue(0).ToString();
                i = i + 1;
            }


            // Lee la plantillas de las tablas
            archivoLectura = new StreamReader(dirPlantillas + "TablaConsulta.html");
            plantillaTablaConsulta = archivoLectura.ReadToEnd();
            archivoLectura.Close();

            // Lee la plantillas de fila de titulo 
            archivoLectura = new StreamReader(dirPlantillas + "FilaCondicion.html");
            plantillaFilaCondicion = archivoLectura.ReadToEnd();
            archivoLectura.Close();

            // Lee la plantillas de fila de titulo 
            archivoLectura = new StreamReader(dirPlantillas + "FilaCondicionSelect.html");
            plantillaFilaCondicionSelect = archivoLectura.ReadToEnd();
            archivoLectura.Close();

            /* Lee la plantillas de fila de titulo */
            archivoLectura = new StreamReader(dirPlantillas + "FilaCondicionSelectMultiple.html");
            plantillaFilaCondicionSelectMultiple = archivoLectura.ReadToEnd();
            archivoLectura.Close();

            /* Lee la plantillas de fila de titulo */
            archivoLectura = new StreamReader(dirPlantillas + "FilaCondicionFecha.html");
            plantillaFilaCondicionFecha = archivoLectura.ReadToEnd();
            archivoLectura.Close();

            resultado = this.darFormularioReporte(nombreReporte, campos, nombresRequest, valoresRequest, plantillaTablaConsulta, plantillaFilaCondicion, plantillaFilaCondicionSelect, plantillaFilaCondicionSelectMultiple, plantillaFilaCondicionFecha, System.Configuration.ConfigurationManager.ConnectionStrings[cadenaConexion].ToString(), cadenaConexion, sesion);
        }
        catch (Exception ex)
        {
            //GAMM. Information Leakage. No se debe mostrar el mensje de error.

            /* Se imprime el valor de la excepcion */
            //resultado = ex.ToString();

            throw new Exception("Error");
            
        }

        /* Se libera el archivo xml */
        objXMLValidatingReader.Close();
        objXMLValidatingReader = null;
        objXMLTextReader = null;
        objXMLDocument = null;

        return resultado;
    }

}