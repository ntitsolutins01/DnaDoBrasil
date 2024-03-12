var vm = new Vue({
    el: "#vMetricaImc ",
    data: {
        loading: false,
        editDto: { Id: "", Classificacao: "", ValorInicial: "", ValorFinal: "", Sexo: "", Idade: "", Status: true }
    },
    mounted: function () {
        var self = this;
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

            var formid = $('form').attr('id');

            if (formid === "formEditMetricaImc ") {

                //mascara dos inputs
                var valorInicial = $("#valorInicial");
                valorInicial.mask('0.00', { reverse: false });
                var valorFinal = $("#valorFinal");
                valorFinal.mask('0.00', { reverse: false });

                $("#formEditMetricaImc ").validate({
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

            if (formid === "formMetricaImc ") {

                //mascara dos inputs
                var valorInicial = $("#valorInicial");
                valorInicial.mask('0.00', { reverse: false });
                var valorFinal = $("#valorFinal");
                valorFinal.mask('0.00', { reverse: false });

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

                $("#formMetricaImc ").validate({
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
        },
        DeleteMetricaImc : function (id) {
            var url = "MetricaImc /Delete/" + id;
            $("#deleteMetricaImc Href").prop("href", url);
        },
        EditMetricaImc : function (id) {
            var self = this;

            axios.get("MetricaImc /GetMetricaImc ById/?id=" + id).then(result => {

                self.editDto.Id = result.data.id;
                self.editDto.Classificacao = result.data.classificacao;
                self.editDto.valorInicial = result.data.valorInicial;
                self.editDto.ValorFinal = result.data.valorfinal;
                self.editDto.Sexo = result.data.sexo;
                self.editDto.Idade = result.data.idade;
                self.editDto.Status = result.data.status;

            }).catch(error => {
                Site.Notification("Erro ao buscar e analisar dados", error.message, "error", 1);
            });
        }
    }
});

var crud = {
    DeleteModal: function (id) {
        $('input[name="deleteMetricaImc Id"]').attr('value', id);
        $('#mdDeleteMetricaImc ').modal('show');
        vm.DeleteMetricaImc (id)
    },
    EditModal: function (id) {
        $('input[name="editMetricaImc Id"]').attr('value', id);
        $('#mdEditMetricaImc ').modal('show');
        vm.EditMetricaImc (id)
    }
};