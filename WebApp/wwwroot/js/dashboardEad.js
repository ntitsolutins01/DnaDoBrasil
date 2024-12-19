var vm = new Vue({
    el: "#vDashboardEad", 
    data: {
        loading: false
    },
    mounted: function () {
        var self = this;
        (function ($) {
            'use strict';
            var formid = $('form')[1].id;

            if (formid === "formDashboardEad") {

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

                $select.on('change', function () {
                    $(this).trigger('blur');
                });

                $("#formDashboardEad").validate({
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

                $("#ddlFomento").change(function () {

                    self.ShowLoad(true, "pFiltro");

                    var id = $("#ddlFomento").val().split("-")[0];

                    var url = "../DivisaoAdministrativa/GetMunicipioByFomento?id=" + id;


                    $.getJSON(url,
                        function (data) {
                            if (data.length > 0) {
                                $("#ddlEstado").val(data[1]).trigger("change");
                                //var items = '<option value="">Selecionar Município</option>';
                                //$("#ddlMunicipio").empty;
                                //$.each(data,
                                //    function (i, row) {
                                //        if (row.selected === true) {
                                //            items += "<option selected value='" + row.value + "'>" + row.text + "</option>";
                                //            $("#ddlMunicipio").val(row.value).trigger("change")
                                //        } else {
                                //            items += "<option value='" + row.value + "'>" + row.text + "</option>";

                                //        }
                                //    });
                                //$("#ddlMunicipio").html(items);
                            }
                            else {
                                new PNotify({
                                    title: 'Municipios',
                                    text: 'Municipios não encontradas.',
                                    type: 'warning'
                                });
                            }
                        });

                    self.ShowLoad(false, "pFiltro");
                });

                $("#ddlEstado").change(function () {

                    self.ShowLoad(true, "pFiltro");

                    var sigla = $("#ddlEstado").val();
                    var municipioId = $("#ddlFomento").val().split("-")[1];

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
                                $("#ddlMunicipio").val(municipioId).trigger("change");
                            }
                            else {
                                new PNotify({
                                    title: 'Usuario',
                                    text: data,
                                    type: 'warning'
                                });
                            }
                        });

                    self.ShowLoad(false, "pFiltro");
                });

                $("#ddlMunicipio").change(function () {

                    self.ShowLoad(true, "pFiltro");

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

                    self.ShowLoad(false, "pFiltro");
                });

                $("#ddlTipoCurso").change(function () {
                    var tipoCursoId = $("#ddlTipoCurso").val();
                    var url = "../Curso/GetCursosAllByTipoCursoId";

                    $.getJSON(url,
                        { id: tipoCursoId },
                        function (data) {
                            if (data.length > 0) {
                                var items = '<option value="">Selecionar Curso</option>';
                                $("#ddlCurso").empty();
                                $.each(data,
                                    function (i, row) {
                                        items += "<option value='" + row.value + "'>" + row.text + "</option>";
                                    });
                                $("#ddlCurso").html(items);
                            }
                            else {
                                new PNotify({
                                    title: 'Curso',
                                    text: 'Cursos não encontrados.',
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
        GetPesquisaDashboard: function () {
        }
    }
});

