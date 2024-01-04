var vm = new Vue({
    el: "#formDadosEstudante",
    data: {
        loading: false,
        editDto: { Id: "", Nome: "", Status: true, IdadeInicial: "", IdadeFinal: "", ScoreTotal: "", Descricao: "" },
        params:{}
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

            if (formid === "formDadosEstudante") {

                $("#formDadosEstudante").validate({
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
        DeleteDadosEstudante: function (id) {
            var url = "DadosEstudante/Delete/" + id;
            $("#deleteDadosEstudanteHref").prop("href", url);
        }
    }
});

var crud = {
    DeleteModal: function (id) {
        $('input[name="DadosEstudanteId"]').attr('value', id);
        $('#mdDeleteDadosEstudante').modal('show');
        vm.DeleteDadosEstudante(id)
    }
};