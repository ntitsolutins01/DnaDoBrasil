var vm = new Vue({
    el: "#formLaudo",
    data: {
        loading: false,
        editDto: { Id: "", Nome: "", Status: true, IdadeInicial:"", IdadeFinal:"", ScoreTotal:"", Descricao:"" }
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
        },
        DeleteLaudo: function (id) {
            var url = "Laudo/Delete/" + id;
            $("#deleteLaudoHref").prop("href", url);
        },
        EditLaudo: function (id) {
            var self = this;

            axios.get("Laudo/GetLaudoById/?id=" + id).then(result => {

                self.editDto.Id = result.data.id;
                self.editDto.Nome = result.data.nome;
                self.editDto.Descricao = result.data.descricao;
                self.editDto.IdadeInicial = result.data.idadeInicial;
                self.editDto.IdadeFinal = result.data.idadeFinal;
                self.editDto.ScoreTotal = result.data.scoreTotal;
                self.editDto.Status = result.data.status;

            }).catch(error => {
                Site.Notification("Erro ao buscar e analisar dados", error.message, "error", 1);
            });
        }
    }
});

var crud = {
    DeleteModal: function (id) {
        $('input[name="LaudoId"]').attr('value', id);
        $('#mdDeleteLaudo').modal('show');
        vm.DeleteLaudo(id)
    },
    EditModal: function (id) {
        $('input[name="LaudoId"]').attr('value', id);
        $('#mdEditLaudo').modal('show');
        vm.EditLaudo(id)
    }
};