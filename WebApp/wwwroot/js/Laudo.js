var vm = new Vue({
    el: "#formLaudo",
    data: {
        loading: false,
    },
    mounted: function () {
        var self = this;
        (function ($) {
            'use strict';

            // iosSwitcher
            (function ($) {

                'use strict';

                if (typeof Switch !== 'undefined' && $.isFunction(Switch)) {

                    $(function () {
                        $('[data-plugin-ios-switch]').each(function () {
                            var $this = $(this);

                            $this.themePluginIOS7Switch();
                        });
                    });

                }

            }).apply(this, [jQuery]);

            // TimePicker
            (function (theme, $) {

                theme = theme || {};

                var instanceName = '__timepicker';

                var PluginTimePicker = function ($el, opts) {
                    return this.initialize($el, opts);
                };

                PluginTimePicker.defaults = {
                    disableMousewheel: true
                };

                PluginTimePicker.prototype = {
                    initialize: function ($el, opts) {
                        if ($el.data(instanceName)) {
                            return this;
                        }

                        this.$el = $el;

                        this
                            .setData()
                            .setOptions(opts)
                            .build();

                        return this;
                    },

                    setData: function () {
                        this.$el.data(instanceName, this);

                        return this;
                    },

                    setOptions: function (opts) {
                        this.options = $.extend(true, {}, PluginTimePicker.defaults, opts);

                        return this;
                    },

                    build: function () {
                        this.$el.timepicker(this.options);

                        return this;
                    }
                };

                // expose to scope
                $.extend(theme, {
                    PluginTimePicker: PluginTimePicker
                });

                // jquery plugin
                $.fn.themePluginTimePicker = function (opts) {
                    return this.each(function () {
                        var $this = $(this);

                        if ($this.data(instanceName)) {
                            return $this.data(instanceName);
                        } else {
                            return new PluginTimePicker($this, opts);
                        }

                    });
                }

            }).apply(this, [window.theme, jQuery]);

            // TimePicker
            (function ($) {

                'use strict';

                if ($.isFunction($.fn['timepicker'])) {

                    $(function () {
                        $('[data-plugin-timepicker]').each(function () {
                            var $this = $(this),
                                opts = {};

                            var pluginOptions = $this.data('plugin-options');
                            if (pluginOptions)
                                opts = pluginOptions;

                            $this.themePluginTimePicker(opts);
                        });
                    });

                }

            }).apply(this, [jQuery]);

            var formid = $('form').attr('id');

            if (formid === "formEditLaudo") {

                $("#formEditLaudo").validate({
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
            }

            if (formid === "formLaudo") {

                //skin select
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

                $("#ddlEstado").change(function () {
                    var sigla = $("#ddlEstado").val();

                    var url = "../DivisaoAdministrativa/GetMunicipioByUf?uf=" + sigla;

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
                                    title: 'Fomento',
                                    text: 'Municípios não encontrados.',
                                    type: 'warning'
                                });
                            }
                        });
                });

                //clique de escolha do select
                $("#ddlMunicipio").change(function () {
                    var id = $("#ddlMunicipio").val();

                    var url = "../../Localidade/GetLocalidadeByMunicipio?id=" + id;

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

                //clique de escolha do select
                $("#ddlLocalidade").change(function () {
                    var id = $("#ddlLocalidade").val();

                    var url = "../../Aluno/GetAlunosByLocalidade?id=" + id;

                    var ddlSource = "#ddlAluno";

                    $.getJSON(url,
                        { id: $(ddlSource).val() },
                        function (data) {
                            if (data.length > 0) {
                                var items = '<option value="">Selecionar Aluno</option>';
                                $("#ddlAluno").empty;
                                $.each(data,
                                    function (i, row) {
                                        items += "<option value='" + row.value + "'>" + row.text + "</option>";
                                    });
                                $("#ddlAluno").html(items);
                            }
                            else {
                                new PNotify({
                                    title: 'Alunos',
                                    text: 'Alunos não encontrados.',
                                    type: 'warning'
                                });
                            }
                        });

                    var urlProfissional = "../../Profissional/GetProfissionaisByLocalidade/?id=" + id;

                    var ddlSource = "#ddlProfissional";

                    $.getJSON(urlProfissional,
                        { id: $(ddlSource).val() },
                        function (data) {
                            if (data.length > 0) {
                                var items = '<option value="">Selecionar Profissional</option>';
                                $("#ddlProfissional").empty;
                                $.each(data,
                                    function (i, row) {
                                        items += "<option value='" + row.value + "'>" + row.text + "</option>";
                                    });
                                $("#ddlProfissional").html(items);
                            }
                            else {
                                new PNotify({
                                    title: 'Profissional',
                                    text: 'Profissional não encontrados.',
                                    type: 'warning'
                                });
                            }
                        });
                });

                //mascara dos inputs
                var $numAltura = $("#altura");
                $numAltura.mask('000', { reverse: false });
                var $numMassaCorporal = $("#massaCorporal");
                $numMassaCorporal.mask('000', { reverse: false });
                var $numPreensaoManual = $("#preensaoManual");
                $numPreensaoManual.mask('000', { reverse: false });
                var $numFlexibilidade = $("#flexibilidade");
                $numFlexibilidade.mask('000', { reverse: false });
                var $numImpulsaoHorizontal = $("#impulsaoHorizontal");
                $numImpulsaoHorizontal.mask('000', { reverse: false });
                var $numAptidaoFisica = $("#aptidaoFisica");
                $numAptidaoFisica.mask('000', { reverse: false });
                var $numAlturaSaude = $("#alturaSaude");
                $numAlturaSaude.mask('000', { reverse: false });
                var $numMassaCorporalSaude = $("#massaCorporalSaude");
                $numMassaCorporalSaude.mask('000', { reverse: false });
                var $numEnvergaduraSaude = $("#envergaduraSaude");
                $numEnvergaduraSaude.mask('000', { reverse: false });
                var $numTesteVelocidade = $("#testeVelocidade");
                $numTesteVelocidade.mask('00:00', { reverse: false });
                var $numAptidaoFisica = $("#aptidaoFisica");
                $numAptidaoFisica.mask('00:00', { reverse: false });

                $("#formLaudo").validate({
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
        }
    }
});

var crud = {
    //DeleteModal: function (id) {
    //    $('input[name="LaudoId"]').attr('value', id);
    //    $('#mdDeleteLaudo').modal('show');
    //    vm.DeleteLaudo(id)
    //},
    //EditModal: function (id) {
    //    $('input[name="LaudoId"]').attr('value', id);
    //    $('#mdEditLaudo').modal('show');
    //    vm.EditLaudo(id)
    //}
};