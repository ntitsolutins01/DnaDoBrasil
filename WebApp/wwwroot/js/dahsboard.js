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

                var percentSaude = result.data.dashboard.statusLaudos.progressoSaude +'%'
                $('#progressoSaude').css('width', percentSaude);
                $('#vlProgressoSaude').text(result.data.dashboard.statusLaudos.progressoSaude + ' %');


                $("#totTalentoEsportivoFinalizado").text(result.data.dashboard.statusLaudos.totTalentoEsportivoFinalizado);
                $("#totTalentoEsportivoAndamento").text(result.data.dashboard.statusLaudos.totTalentoEsportivoAndamento);

                var percentTalentoEsportivo = result.data.dashboard.statusLaudos.progressoTalentoEsportivo +'%'
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


                self.SetGraficoControlePresenca(result)
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
                            data: result.data.listPresencasAnual
                        },
                        {
                            name: 'Falta',
                            data: result.data.listFaltasAnual
                        }
                    ]
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
