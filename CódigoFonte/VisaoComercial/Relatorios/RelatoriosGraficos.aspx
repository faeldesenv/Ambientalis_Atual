<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RelatoriosGraficos.aspx.cs"
    Inherits="Relatorios_RelatoriosGraficos" %>

<%@ Register Src="../MBOX/MBOX.ascx" TagName="MBOX" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Styles/Style.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.6.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/jquery-ui.min.js" type="text/javascript"></script>
    <script src="../Scripts/Funcoes.js" type="text/javascript"></script>
    <!--[if IE]><script type="text/javascript" src="../Scripts/excanvas.compiled.js"></script><![endif]-->
    <script src="../Scripts/jqxcore.js" type="text/javascript"></script>
    <script src="../Scripts/jqxdata.js" type="text/javascript"></script>
    <script src="../Scripts/jqxchart.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(document).ready(function () {

            $("#grafico_pizza").hide();
            $("#grafico_barras").hide();

            // setup the chart
            if ($("#<%= hfBarrasValores.ClientID %>").val() != "0") {
                $("#grafico_barras").show();

                // Parametros de configurção
                var valorMaximo = eval($("#<%= hfBarrasValorMaximo.ClientID %>").val());
                var valoresGrafico = $("#<%= hfBarrasValores.ClientID %>").val().replace('[', '').replace(']', '').split(',');
                var ano = $("#<%= hfAno.ClientID %>").val();

                // prepara chart data para VendasProspectos
                var sampleDataColumn = [
                { Mês: 'Janeiro', Vendas: 02, Prospectos: 10, Percent: 2 },
                { Mês: 'Fevereiro', Vendas: 02, Prospectos: 10, Percent: 3 },
                { Mês: 'Março', Vendas: 02, Prospectos: 10, Percent: 4 },
                { Mês: 'Abril', Vendas: 02, Prospectos: 10, Percent: 5 },
                { Mês: 'Maio', Vendas: 02, Prospectos: 10, Percent: 6 },
                { Mês: 'Junho', Vendas: 02, Prospectos: 10, Percent: 7 },
                { Mês: 'Julho', Vendas: 02, Prospectos: 10, Percent: 8 },
                { Mês: 'Agosto', Vendas: 02, Prospectos: 10, Percent: 9 },
                { Mês: 'Setembro', Vendas: 02, Prospectos: 10, Percent: 10 },
                { Mês: 'Outubro', Vendas: 02, Prospectos: 10, Percent: 11 },
                { Mês: 'Novembro', Vendas: 02, Prospectos: 10, Percent: 12 },
                { Mês: 'Dezembro', Vendas: 02, Prospectos: 10, Percent: 13 }

                ];

                var listaValores = $("#<%= hfBarrasValores.ClientID %>").val().replace('[[', '').replace(']]', '').split('],[');
                for (var i = 0; i < sampleDataColumn.length; i++) {
                    sampleDataColumn[i]["Vendas"] = listaValores[i].split(',')[0];
                    sampleDataColumn[i]["Prospectos"] = listaValores[i].split(',')[1];
                }

                // prepare jqxChart settings para VendasProspectos
                var settingsVendasProspectos = {
                    title: "Vendas X Indicação de Clientes",
                    description: "Ano " + ano,
                    showLegend: true,
                    backgroundColor: "#fff",
                    enableAnimations: true,
                    padding: { left: 20, top: 5, right: 20, bottom: 5 },
                    titlePadding: { left: 30, top: 0, right: 0, bottom: 10 },
                    source: sampleDataColumn,
                    categoryAxis:
                    {
                        dataField: 'Mês',
                        showGridLines: true,
                        flip: false,
//                        toolTipFormatFunction: function (value) {
//                            return value + '-' + index;
//                        },
                    },
                    colorScheme: 'scheme01',
                    seriesGroups:
                    [
                        {
                            type: 'column',
                            orientation: 'vertical',
                            columnsGapPercent: 100,
                            toolTipFormatSettings: { thousandsSeparator: ',' },
                            valueAxis:
                            {
                                flip: false,
                                // ver em parametros
                                unitInterval: 2,
                                maxValue: valorMaximo,
                                displayValueAxis: true,
                                description: '',
                                formatFunction: function (value) {
                                    return parseInt(value / 1);
                                }
                            },
                            series: [
                                    { dataField: 'Vendas', displayText: 'Venda', showsLabels: true },
                                    { dataField: 'Prospectos', displayText: 'Indicação de Clientes', showsLabels: true }
                                ]
                        }
                    ]
                };
                // Fim chart settingsVendasProspectos

                $('#jqxChartVendarProspectos').jqxChart(settingsVendasProspectos);
            }

            if ($("#<%= hfPizzaValores.ClientID %>").val() != "0") {
                $("#grafico_pizza").show();

                //                        var sampleDataPie = [
                //                            { Estado: 'Acre', Vendas: 0 },
                //                            { Estado: 'Alagoas', Vendas: 0 },
                //                            { Estado: 'Amapá', Vendas: 0 },
                //                            { Estado: 'Amazonas', Vendas: 0 },
                //                            { Estado: 'Bahia', Vendas: 0 },
                //                            { Estado: 'Ceará', Vendas: 0 },
                //                            { Estado: 'Distrito Federal', Vendas: 0 },
                //                            { Estado: 'Espírito Santo', Vendas: 0 },
                //                            { Estado: 'Goiás', Vendas: 0 },
                //                            { Estado: 'Maranhão', Vendas: 0 },
                //                            { Estado: 'Mato Grosso', Vendas: 0 },
                //                            { Estado: 'Mato Grosso do Sul', Vendas: 0 },
                //                            { Estado: 'Minas Gerais', Vendas: 0 },
                //                            { Estado: 'Pará', Vendas: 0 },
                //                            { Estado: 'Paraíba', Vendas: 0 },
                //                            { Estado: 'Paraná', Vendas: 0 },
                //                            { Estado: 'Pernambuco', Vendas: 0 },
                //                            { Estado: 'Piauí', Vendas: 0 },
                //                            { Estado: 'Rio de Janeiro', Vendas: 0 },
                //                            { Estado: 'Rio Grande do Norte', Vendas: 0 },
                //                            { Estado: 'Rio Grande do Sul', Vendas: 0 },
                //                            { Estado: 'Rondônia', Vendas: 0 },
                //                            { Estado: 'Roraima', Vendas: 0 },
                //                            { Estado: 'Santa Catarina', Vendas: 0 },
                //                            { Estado: 'São Paulo', Vendas: 0 },
                //                            { Estado: 'Sergipe', Vendas: 0 },
                //                            { Estado: 'Tocantins', Vendas: 0 }
                //                            ];

                var sampleDataPie = new Array();
                var listaValores = $("#<%= hfPizzaValores.ClientID %>").val().replace('{', '').replace('}', '').split(','); ;
                for (var i = 0; i < listaValores.length; i++) {
                    var aux = new Array();
                    aux["Estado"] = listaValores[i].split(':')[0].replace('"', '').replace('"', '');
                    aux["Vendas"] = listaValores[i].split(':')[1];
                    sampleDataPie.push(aux);
                }

                // prepare jqxChart settings para VendasEstados
                var settingsVendasEstados = {
                    title: "Vendas Por Estado",
                    description: "",
                    enableAnimations: true,
                    showLegend: true,
                    backgroundColor: "#fff",
                    legendLayout: { left: 60, top: 60, width: 0, height: 400, flow: 'vertical' },
                    padding: { left: 0, top: 5, right: 0, bottom: 5 },
                    titlePadding: { left: 5, top: 5, right: 0, bottom: 10 },
                    source: sampleDataPie,
                    colorScheme: 'scheme01',
                    seriesGroups:
                    [
                        {
                            type: 'pie',
                            showLabels: true,
                            series:
                                [
                                    {
                                        dataField: 'Vendas',
                                        displayText: 'Estado',
                                        labelRadius: 120,
                                        initialAngle: 15,
                                        radius: 140,
                                        centerOffset: 0,
                                        formatSettings: { sufix: '%', decimalPlaces: 1 }
                                    }
                                ]
                        }
                    ]
                };
                // Fim chart settingsVendasEstados

                $('#jqxChartVendasEstados').jqxChart(settingsVendasEstados);
            }







        });
    </script>
    <style type="text/css">
        .jqx-chart-axis-text, .jqx-chart-label-text, .jqx-chart-tooltip-text, .jqx-chart-legend-text, .jqx-chart-axis-description, .jqx-chart-title-text, .jqx-chart-title-description
        {
            fill: #000;
            color: #000;
        }
        .jqx-chart-tooltip-text
        {
            fill: #000 !important;
        }
        .jqx-chart-axis-text, .jqx-chart-legend-text
        {
            font-size:13px;
        }
    </style>
</head>
<body>
<form id="form1" runat="server">
    <asp:Button ID="btnVoltar" runat="server" Text="VOLTAR AO SISTEMA"  
        CssClass="Button" onclick="btnVoltar_Click"/>
            <div>
        <asp:HiddenField ID="hfBarrasValorMaximo" Value="20" runat="server" />
        <asp:HiddenField ID="hfBarrasValores" Value="0" runat="server" />
        <asp:HiddenField ID="hfAno" Value="0" runat="server" />
        <asp:HiddenField ID="hfPizzaValores" Value="0" runat="server" />
    </div>
        </form>
    <div id="relatorio" style="width: 800px">
    <div id="imprimir_grafico">
        <div id="grafico">
            <div id="grafico_barras" style="text-align: center;">
                <div style="font-size: 18px; padding: 8px;">
                    Relatório Vendas X Indicação de Clientes
                </div>
                <div id='jqxChartVendarProspectos' style="width: 220mm; height: 300px; position: relative;
                    margin: 0 auto; top: 0px;">
                </div>
            </div>
            <div id="grafico_pizza" style="text-align: center; display: none;">
                <div style="font-size: 18px; padding: 8px;">
                    Relatório Vendas X Estados
                </div>
                <div id='jqxChartVendasEstados' style="width: 210mm; height: 520px; position: relative;
                    margin: 0 auto; top: 0px;">
                </div>
            </div>
        </div>
    </div>
    <br />
    <div style="text-align: center">
        <a href="#" onclick="printDiv('imprimir_grafico','janela')">
            <img style="border: 0px; margin-top: 5px;" alt="imprimir" src="../imagens/ico_imprimir.gif" /><br />
            Imprimir Gráfico</a>
    </div>

</body>
</html>
