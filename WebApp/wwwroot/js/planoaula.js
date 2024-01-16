var vm = new Vue({
    el: "#formPlanoAula",
    data: {
        loading: false,
        editDto: { Id: "", PlanoAula: "", TipoEscolaridade: "" }
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

            $("#formPlanoAula").validate({
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

            $("#formEditPlanoAula").validate({
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
        DeletePlanoAula: function (id) {
            var url = "PlanoAula/Delete/" + id;
            $("#deletePlanoAulaHref").prop("href", url);
        },
        EditPlanoAula: function (id) {
            var self = this;

            axios.get("PlanoAula/GetPlanoAulaById/?id=" + id).then(result => {

                self.editDto.Id = result.data.id;
                self.editDto.PlanoAula = result.data.planoaula;
                self.editDto.TipoEscolaridade = result.data.tipoescolaridade;

            }).catch(error => {
                Site.Notification("Erro ao buscar e analisar dados", error.message, "error", 1);
            });
        }
    }
});
var crud = {
    DeleteModal: function (id) {
        $('input[name="PlanoAulaId"]').attr('value', id);
        $('#mdDeletePlanoAula').modal('show');
        vm.DeletePlanoAula(id)
    },
    EditModal: function (id) {
        $('input[name="PlanoAulaId"]').attr('value', id);
        $('#mdEditPlanoAula').modal('show');
        vm.EditPlanoAula(id)
    }
};