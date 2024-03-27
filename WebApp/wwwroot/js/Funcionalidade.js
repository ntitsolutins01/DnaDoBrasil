var vm = new Vue({
    el: "#vFuncionalidade",
    data: {
        loading: false,
        editDto: { Id: "", Nome: "", NomeModulo: "" }
    },
    mounted: function () {
        var self = this;
        (function ($) {
            'use strict';


            var formid = $('form').attr('id');

            if (formid === "formEditFuncionalidade") {


                //mascara dos inputs
                var $pontoInicial = $("#pontoInicial");
                $pontoInicial.mask('00.00', { reverse: true });

                var $pontoFinal = $("#pontoFinal");
                $pontoFinal.mask('00.00', { reverse: true });

                $("#formEditFuncionalidade").validate({
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

            if (formid === "formFuncionalidade") {
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

                $("#formFuncionalidade").validate({
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
        DeleteFuncionalidade: function (id) {
            var url = "Funcionalidade/Delete/" + id;
            $("#deleteFuncionalidadeHref").prop("href", url);
        },
        EditFuncionalidade: function (id) {
            var self = this;

            axios.get("Funcionalidade/GetFuncionalidadeById/?id=" + id).then(result => {

                self.editDto.Id = result.data.id;
                self.editDto.Nome = result.data.nome;
                self.editDto.NomeModulo = result.data.nomeModulo;

            }).catch(error => {
                Site.Notification("Erro ao buscar e analisar dados", error.message, "error", 1);
            });
        }
    }
});

var crud = {
    DeleteModal: function (id) {
        $('input[name="deleteFuncionalidadeId"]').attr('value', id);
        $('#mdDeleteFuncionalidade').modal('show');
        vm.DeleteFuncionalidade(id)
    },
    EditModal: function (id) {
        $('input[name="editFuncionalidadeId"]').attr('value', id);
        $('#mdEditFuncionalidade').modal('show');
        vm.EditFuncionalidade(id)
    }
};