var vm = new Vue({
    el: "#vTextoLaudo",
    data: {
        loading: false,
        editDto: { Id: "", Classificacao: "", PontoInicial: "", PontoFinal: "", Aviso: "", Txto: "", NomeTipoLaudo:"", Idade:"", Sexo: "", TipoLaudoId: "" }
    },
    mounted: function () {
        var self = this;
        (function ($) {
            'use strict';


            var formid = $('form')[1].id;

            if (formid === "formEditTextoLaudo") {


                //mascara dos inputs
                var $pontoInicial = $("#pontoInicial");
                $pontoInicial.mask('00.00', { reverse: true });

                var $pontoFinal = $("#pontoFinal");
                $pontoFinal.mask('00.00', { reverse: true });

                var quadrante = $("#quadrante");
                quadrante.mask('00', { reverse: true });

                var idade = $("#idade");
                idade.mask('00', { reverse: true });

                $("#formEditTextoLaudo").validate({
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

            if (formid === "formTextoLaudo") {
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

                //mascara dos inputs
                var $pontoInicial = $("#pontoInicial");
                $pontoInicial.mask('00.00', { reverse: true });

                var $pontoFinal = $("#pontoFinal");
                $pontoFinal.mask('00.00', { reverse: true });

                $("#formTextoLaudo").validate({
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
        DeleteTextoLaudo: function (id) {
            var url = "TextoLaudo/Delete/" + id;
            $("#deleteTextoLaudoHref").prop("href", url);
        },
        EditTextoLaudo: function (id) {
            var self = this;

            axios.get("TextoLaudo/GetTextoLaudoById/?id=" + id).then(result => {

                self.editDto.Id = result.data.id;
                self.editDto.TipoLaudoId = result.data.tipoLaudoId;
                self.editDto.Classificacao = result.data.classificacao;
                self.editDto.PontoInicial = result.data.pontoInicial;
                self.editDto.PontoFinal = result.data.pontoFinal;
                self.editDto.Aviso = result.data.aviso;
                self.editDto.Texto = result.data.texto;
                self.editDto.NomeTipoLaudo = result.data.nomeTipoLaudo;
                self.editDto.Idade = result.data.idade;
                self.editDto.Sexo = result.data.sexo;
                self.editDto.Quadrante = result.data.quadrante;

            }).catch(error => {
                Site.Notification("Erro ao buscar e analisar dados", error.message, "error", 1);
            });
        }
    }
});

var crud = {
    DeleteModal: function (id) {
        $('input[name="deleteTextoLaudoId"]').attr('value', id);
        $('#mdDeleteTextoLaudo').modal('show');
        vm.DeleteTextoLaudo(id)
    },
    EditModal: function (id) {
        $('input[name="editTextoLaudoId"]').attr('value', id);
        $('#mdEditTextoLaudo').modal('show');
        vm.EditTextoLaudo(id)
    }
};