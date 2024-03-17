var vm = new Vue({
    el: "#formDashboard",
    data: {
        loading: false
    },
    mounted: function () {
        var self = this;
        (function ($) {
            'use strict';
            var formid = $('form').attr('id');

            if (formid === "formDashboard") {

                self.GetPesquisaDashboard();


                var $select = $(".select2").select2({
                    allowClear: true
                });

                $(".select2").each(function () {
                    var $this = $(this),
                        opts = {};

                    var pluginOptions = $this.data('plugin-options');
                    if (pluginOptions)
                        opts = pluginOptions;

                    $this.themePluginSelect2(opts);
                });

                /*
                 * When you change the value the select via select2, it triggers
                 * a 'change' event, but the jquery validation plugin
                 * only re-validates on 'blur'*/

                $select.on('change', function () {
                    $(this).trigger('blur');
                });


                $("#formDashboard").validate({
                    highlight: function (label) {
                        $(label).closest('.form-group').removeClass('has-success').addClass('has-error');
                    },
                    success: function (label) {
                        $(label).closest('.form-group').removeClass('has-error');
                        label.remove();
                    },
                    errorPlacement: function (error, element) {
                        var placement = element.closest('.input-group');
                        if (!placement.get(0)) {
                            placement = element;
                        }
                        if (error.text() !== '') {
                            placement.after(error);
                        }
                    }
                });

                $("#ddlEstado").change(function () {
                    var sigla = $("#ddlEstado").val();

                    var url = "DivisaoAdministrativa/GetMunicipioByUf?uf=" + sigla;

                    var ddlSource = "#ddlMunicipio";

                    $.getJSON(url,
                        { id: $(ddlSource).val() },
                        function (data) {
                            if (data.length > 0) {
                                var items = '<option value="">Selecionar Municipio</option>';
                                $("#ddlMunicipio").empty;
                                $.each(data,
                                    function (i, row) {
                                        items += "<option value='" + row.value + "'>" + row.text + "</option>";
                                    });
                                $("#ddlMunicipio").html(items);
                            }
                            else {
                                new PNotify({
                                    title: 'Usuario',
                                    text: data,
                                    type: 'warning'
                                });
                            }
                        });
                });



                //clique de escolha do select
                $("#ddlMunicipio").change(function () {
                    var id = $("#ddlMunicipio").val();

                    var url = "../Localidade/GetLocalidadeByMunicipio?id=" + id;

                    var ddlSource = "#ddlLocalidade";

                    $.getJSON(url,
                        { id: $(ddlSource).val() },
                        function (data) {
                            if (data.length > 0) {
                                var items = '<option value="">Selecionar Localidade</option>';
                                $("#ddlLocalidade").empty;
                                $.each(data,
                                    function (i, row) {
                                        items += "<option value='" + row.value + "'>" + row.text + "</option>";
                                    });
                                $("#ddlLocalidade").html(items);
                            }
                            else {
                                new PNotify({
                                    title: 'Localidades',
                                    text: 'Localidades não encontradas.',
                                    type: 'warning'
                                });
                            }
                        });
                });



            }





        }).apply(this, [jQuery]);
    },
    methods: {
        ShowLoad: function (flag, el) {
            var self = this;

            self.isLoading = flag;
            $("#" + el).loadingOverlay({
                "startShowing": flag
            });
            self.loading = flag;

            if (!flag) {
                self.isLoading = flag;
                $("#" + el).removeClass("loading-overlay-showing");
                self.loading = flag;
            } else {
                self.isLoading = flag;
                $("#" + el).addClass("loading-overlay-showing");
                self.loading = flag;
            }
        },
        DeleteDashboard: function (id) {
            var url = "Dashboard/Delete/" + id;
            $("#deleteDashboardHref").prop("href", url);
        },
        GetPesquisaDashboard: function () {

            var self = this;

            var obj = {
                FomentoId: $("#ddlFomento").val(),
                Estado: $("#ddlEstado").val(),
                MunicipioId: $("#ddlMunicipio").val(),
                LocalidadeId: $("#ddlLocalidade").val(),
                DeficienciaId: $("#ddlDeficiencia").val(),
                Etnia: $("#ddlEtnia").val()
            }
            axios.post("Dashboard/GetPesquisaDashboardByFilter", obj).then(result => {
                $("#cadastrosMasculinos").text(result.data.dashboard.cadastrosMasculinos);
                $("#avaliacoesDna").text(result.data.dashboard.avaliacoesDna);
                $("#laudosAndamentos").text(result.data.dashboard.laudosAndamentos);
                $("#laudosFinalizados").text(result.data.dashboard.laudosFinalizados);
                $("#cadastrosFemininos").text(result.data.dashboard.cadastrosFemininos);
                $("#alunosCadastrados").text(result.data.dashboard.alunosCadastrados);
                $("#laudosMasculinos").text(result.data.dashboard.laudosMasculinos);
                $("#laudosMasculinos").text(result.data.dashboard.laudosMasculinos);

                $("#totSaudeFinalizado").text(result.data.dashboard.statusLaudos.totSaudeFinalizado);
                $("#totSaudeAndamento").text(result.data.dashboard.statusLaudos.totSaudeAndamento);

                var percentSaude = result.data.dashboard.statusLaudos.progressoSaude + '%'
                $('#progressoSaude').css('width', percentSaude);
                $('#vlProgressoSaude').text(result.data.dashboard.statusLaudos.progressoSaude + ' %');


                $("#totTalentoEsportivoFinalizado").text(result.data.dashboard.statusLaudos.totTalentoEsportivoFinalizado);
                $("#totTalentoEsportivoAndamento").text(result.data.dashboard.statusLaudos.totTalentoEsportivoAndamento);

                var percentTalentoEsportivo = result.data.dashboard.statusLaudos.progressoTalentoEsportivo + '%'
                $('#progressoTalentoEsportivo').css('width', percentTalentoEsportivo);
                $('#vlProgressoTalentoEsportivo').text(result.data.dashboard.statusLaudos.progressoTalentoEsportivo + ' %');

                $("#totConsumoAlimentarFinalizado").text(result.data.dashboard.statusLaudos.totConsumoAlimentarFinalizado);
                $("#totConsumoAlimentarAndamento").text(result.data.dashboard.statusLaudos.totConsumoAlimentarAndamento);

                var percentConsumoAlimentar = result.data.dashboard.statusLaudos.progressoConsumoAlimentar + '%'
                $('#progressoConsumoAlimentar').css('width', percentConsumoAlimentar);
                $('#vlProgressoConsumoAlimentar').text(result.data.dashboard.statusLaudos.progressoConsumoAlimentar + ' %');

                $("#totSaudeBucalFinalizado").text(result.data.dashboard.statusLaudos.totSaudeBucalFinalizado);
                $("#totSaudeBucalAndamento").text(result.data.dashboard.statusLaudos.totSaudeBucalAndamento);

                var percentSaudeBucal = result.data.dashboard.statusLaudos.progressoSaudeBucal + '%'
                $('#progressoSaudeBucal').css('width', percentSaudeBucal);
                $('#vlProgressoSaudeBucal').text(result.data.dashboard.statusLaudos.progressoSaudeBucal + ' %');

                $("#totQualidadeDeVidaFinalizado").text(result.data.dashboard.statusLaudos.totQualidadeDeVidaFinalizado);
                $("#totQualidadeDeVidaAndamento").text(result.data.dashboard.statusLaudos.totQualidadeDeVidaAndamento);

                var percentQualidadeDeVida = result.data.dashboard.statusLaudos.progressoQualidadeDeVida + '%'
                $('#progressoQualidadeDeVida').css('width', percentQualidadeDeVida);
                $('#vlProgressoQualidadeDeVida').text(result.data.dashboard.statusLaudos.progressoQualidadeDeVida + ' %');

                $("#totVocacionalFinalizado").text(result.data.dashboard.statusLaudos.totVocacionalFinalizado);
                $("#totVocacionalAndamento").text(result.data.dashboard.statusLaudos.totVocacionalAndamento);

                var percentVocacional = result.data.dashboard.statusLaudos.progressoVocacional + '%'
                $('#progressoVocacional').css('width', percentVocacional);
                $('#vlProgressoVocacional').text(result.data.dashboard.statusLaudos.progressoVocacional + ' %');


                self.SetGraficoControlePresenca(result);
                self.SetGraficoLaudosPeriodo(result);
                self.SetGraficoSaudePercentual(result);
                self.SetGraficoTotalizadorSaudeSexo(result);
                self.SetGraficoTalentoPercentual(result);
                self.SetGraficoTotalizadorTalento(result);

            }).catch(error => {
                Site.Notification("Erro ao buscar e analisar dados", error.message, "error", 1);
            });
        },
        SetGraficoControlePresenca: function (result) {

            var self = this;

            $(function () {

                Highcharts.chart('containerPresenca', {
                    chart: {
                        type: 'column'
                    },
                    title: {
                        text: undefined
                    },
                    xAxis: {
                        categories: ['Jav', 'Fev', 'Mar',
                            'Abr', 'Mai', 'Jun',
                            'Jul', 'Ago', 'Set',
                            'Out', 'Nov', 'Dez'],
                        labels: {
                            style: {
                                fontSize: '15px'
                            }
                        }
                    },
                    yAxis: {
                        min: 0,
                        title: {
                            text: 'Quantidade de Presenças e Faltas',
                            style: {
                                fontSize: '10px'
                            }
                        },
                        labels: {
                            style: {
                                fontSize: '15px'
                            }
                        }
                    },
                    plotOptions: {
                        column: {
                            pointPadding: 0.2,
                            borderWidth: 0
                        }
                    },
                    tooltip: {
                        style: {
                            fontSize: '15px'
                        }
                    },
                    legend: {
                        itemStyle: {
                            fontSize: '15px'
                        }
                    },
                    colors: ['#4CAF50', '#F44336'],
                    series: [
                        {
                            name: 'Presença',
                            data: result.data.dashboard.listPresencasAnual
                        },
                        {
                            name: 'Falta',
                            data: result.data.dashboard.listFaltasAnual
                        }
                    ]
                });
            });
        },
        SetGraficoLaudosPeriodo: function (result) {
            $(function () {

                Highcharts.chart('containerLaudos', {

                    chart: {
                        type: 'column',
                        inverted: false
                    },

                    title: {
                        text: undefined
                    },

                    xAxis: {
                        categories: ['Últimos 3 meses', 'Últimos 6 meses', 'Em 1 ano'],
                        labels: {
                            style: {
                                fontSize: '15px'
                            }
                        }
                    },

                    yAxis: {
                        title: {
                            text: 'Laudos',
                            style: {
                                fontSize: '10px'
                            },
                            labels: {
                                style: {
                                    fontSize: '15px'
                                }
                            }
                        }
                    },

                    plotOptions: {
                        columnrange: {
                            dataLabels: {
                                enabled: true,
                                formatter: function () {
                                    return this.y;
                                }
                            }
                        }
                    },
                    tooltip: {
                        style: {
                            fontSize: '15px'
                        }
                    },

                    legend: {
                        itemStyle: {
                            fontSize: '15px'
                        },
                        enabled: false
                    },

                    series: [{
                        name: 'Total',
                        data: [
                            { y: result.data.dashboard.ultimos3Meses, color: '#3949AB' },
                            { y: result.data.dashboard.ultimos6Meses, color: '#8E24AA' },
                            { y: result.data.dashboard.em1Ano, color: '#1E88E5' }
                        ]
                    }]

                });

            });
        },
        SetGraficoSaudePercentual: function (result) {
            $(function () {

                Highcharts.chart('containerSaudePercentual', {
                    chart: {
                        type: 'variablepie'
                    },
                    title: {
                        text: undefined
                    },
                    tooltip: {
                        headerFormat: '',
                        pointFormat: '<span style="color:{point.color}">\u25CF</span> <b> {point.name}</b><br/>' +
                            '<b>{point.y} %</b> dos Alunos',
                        style: {
                            fontSize: '12px'
                        }
                    },
                    legend: {
                        itemStyle: {
                            fontSize: '2px'
                        }
                    },
                    plotOptions: {
                        variablepie: {
                            dataLabels: {
                                enabled: true,
                                style: {
                                    fontSize: '12px',
                                    fontWeight: '400'
                                }
                            }
                        }
                    },
                    series: [{
                        minPointSize: 10,
                        innerSize: '20%',
                        zMin: 0,
                        name: 'Percentual de Saúde dos Alunos',
                        borderRadius: 5,
                        data: [{
                            name: 'ABAIXO DO NORMAL',
                            y: result.data.dashboard.percentualSaude.ABAIXODONORMAL,
                            z: 50
                        }, {
                            name: 'NORMAL',
                            y: result.data.dashboard.percentualSaude.NORMAL,
                            z: 50
                        }, {
                            name: 'SOBREPESO',
                            y: result.data.dashboard.percentualSaude.SOBREPESO,
                            z: 50
                        }, {
                            name: 'OBESIDADE',
                            y: result.data.dashboard.percentualSaude.OBESIDADE,
                            z: 50
                        }],
                        colors: [
                            '#EF5350',
                            '#EC407A',
                            '#AB47BC',
                            '#7E57C2'
                        ]
                    }]
                });

            });
        },
        SetGraficoTotalizadorSaudeSexo: function (result) {
            var teste = result.data.dashboard.listTotalizadorSaudeSexo;

            $(function () {

                Highcharts.chart('containerSaudeSexo', {
                    chart: {
                        type: 'bar'
                    },
                    title: {
                        text: undefined
                    },
                    xAxis: {
                        categories: ['Baixo Peso', 'Acima do Peso', 'Risco de Colesterol Alto e Diabetes',
                            'Risco de Hipertensão Arterial e Transtornos Cardíacos', 'Pré – disposição a resistência a insulina',
                            'Pré – disposição a desequilíbrios musculares', 'Índice positivo de saúde'],

                        labels: {
                            style: {
                                fontSize: '12px'
                            }
                        }
                    },
                    yAxis: {
                        min: 0,
                        title: {
                            text: 'Total',
                            style: {
                                fontSize: '12px'
                            }
                        },

                        labels: {
                            style: {
                                fontSize: '12px'
                            }
                        }
                    },
                    legend: {
                        reversed: true,
                        itemStyle: {
                            fontSize: '12px'
                        }
                    },
                    tooltip: {
                        style: {
                            fontSize: '12px'
                        }
                    },
                    plotOptions: {
                        series: {
                            stacking: 'normal',
                            dataLabels: {
                                enabled: true,
                                style: {
                                    fontSize: '12px',
                                    fontWeight: '400'
                                }
                            }
                        }
                    },
                    series: [{
                        name: 'Feminino',
                        color: '#EC407A',
                        data: [
                            result.data.dashboard.listTotalizadorSaudeSexo.valorTotalizadorSaudeFeminino.baixoPeso,
                            result.data.dashboard.listTotalizadorSaudeSexo.valorTotalizadorSaudeFeminino.acimaPeso,
                            result.data.dashboard.listTotalizadorSaudeSexo.valorTotalizadorSaudeFeminino.riscoColesterolAlto,
                            result.data.dashboard.listTotalizadorSaudeSexo.valorTotalizadorSaudeFeminino.riscoHipertensao,
                            result.data.dashboard.listTotalizadorSaudeSexo.valorTotalizadorSaudeFeminino.resistenciaInsulina,
                            result.data.dashboard.listTotalizadorSaudeSexo.valorTotalizadorSaudeFeminino.desequilibrioMuscular,
                            result.data.dashboard.listTotalizadorSaudeSexo.valorTotalizadorSaudeFeminino.indicePositivoSaude
                        ]
                    }, {
                        name: 'Masculino',
                        data: [

                            result.data.dashboard.listTotalizadorSaudeSexo.valorTotalizadorSaudeMasculino.baixoPeso,
                            result.data.dashboard.listTotalizadorSaudeSexo.valorTotalizadorSaudeMasculino.acimaPeso,
                            result.data.dashboard.listTotalizadorSaudeSexo.valorTotalizadorSaudeMasculino.riscoColesterolAlto,
                            result.data.dashboard.listTotalizadorSaudeSexo.valorTotalizadorSaudeMasculino.riscoHipertensao,
                            result.data.dashboard.listTotalizadorSaudeSexo.valorTotalizadorSaudeMasculino.resistenciaInsulina,
                            result.data.dashboard.listTotalizadorSaudeSexo.valorTotalizadorSaudeMasculino.desequilibrioMuscular,
                            result.data.dashboard.listTotalizadorSaudeSexo.valorTotalizadorSaudeMasculino.indicePositivoSaude
                        ]
                    }]
                });

            });
        },
        SetGraficoTalentoPercentual: function (result) {
            $(function () {

                Highcharts.chart('containerTalentoPercentual', {
                    chart: {
                        type: 'variablepie'
                    },
                    title: {
                        text: undefined
                    },
                    tooltip: {
                        headerFormat: '',
                        pointFormat: '<span style="color:{point.color}">\u25CF</span> <b> {point.name}</b><br/>' +
                            '<b>{point.y} %</b> dos Alunos',
                        style: {
                            fontSize: '12px'
                        }
                    },
                    legend: {
                        itemStyle: {
                            fontSize: '2px'
                        }
                    },
                    plotOptions: {
                        variablepie: {
                            dataLabels: {
                                enabled: true,
                                style: {
                                    fontSize: '12px',
                                    fontWeight: '400'
                                }
                            }
                        }
                    },
                    series: [{
                        minPointSize: 10,
                        innerSize: '20%',
                        zMin: 0,
                        name: 'Percentual de Saúde dos Alunos',
                        borderRadius: 5,
                        data: result.data.dashboard.listPercTalento,
                        colors: [
                            '#EF5350',
                            '#EC407A',
                            '#AB47BC',
                            '#7E57C2'
                        ]
                    }]
                });

            });
        },
        SetGraficoTotalizadorTalento: function (result) {
            $(function () {

                Highcharts.chart('containerTalento', {
                    chart: {
                        type: 'bar'
                    },
                    title: {
                        text: undefined
                    },
                    xAxis: {
                        categories: result.data.dashboard.listPercTalentoCategorias,

                        labels: {
                            style: {
                                fontSize: '12px'
                            }
                        }
                    },
                    yAxis: {
                        min: 0,
                        title: {
                            text: 'Total',
                            style: {
                                fontSize: '12px'
                            }
                        },

                        labels: {
                            style: {
                                fontSize: '12px'
                            }
                        }
                    },
                    legend: {
                        reversed: true,
                        itemStyle: {
                            fontSize: '12px'
                        }
                    },
                    tooltip: {
                        style: {
                            fontSize: '12px'
                        }
                    },
                    plotOptions: {
                        series: {
                            stacking: 'normal',
                            dataLabels: {
                                enabled: true,
                                style: {
                                    fontSize: '12px',
                                    fontWeight: '400'
                                }
                            }
                        }
                    },
                    series: [{
                        name: 'Feminino',
                        color: '#EC407A',
                        data: result.data.dashboard.listValorTalentoFem
                    }, {
                        name: 'Masculino',
                        data: result.data.dashboard.listValorTalentoMasc
                    }]
                });

            });
        }
    }
});

var crud = {
    DeleteModal: function (id) {
        $('input[name="DashboardId"]').attr('value', id);
        $('#mdDeleteDashboard').modal('show');
        vm.DeleteDashboard(id)
    }
};
