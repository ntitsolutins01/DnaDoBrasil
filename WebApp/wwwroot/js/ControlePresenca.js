var vm = new Vue({
    el: "#vControlePresenca",
    data: {
        loading: false,
        editDto: { Id: "", Controle: "", Justificativa: "", Status: true }
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

            if (formid === "formEditControlePresenca") {

                $("#formEditControlePresenca").validate({
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

            if (formid === "formControlePresenca") {

                $("#formControlePresenca").validate({
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
        DeleteControlePresenca: function (id) {
            var url = "ControlePresenca/Delete/" + id;
            $("#deleteControlePresencaHref").prop("href", url);
        },
        EditControlePresenca: function (id) {
            var self = this;

            axios.get("ControlePresenca/GetControlePresencaById/?id=" + id).then(result => {

                self.editDto.Id = result.data.id;
                self.editDto.Controle = result.data.controle;
                self.editDto.Status = result.data.status;
                self.editDto.Justificativa = result.data.justificativa;
   
            }).catch(error => {
                Site.Notification("Erro ao buscar e analisar dados", error.message, "error", 1);
            });
        }
    }
});

var crud = {
    DeleteModal: function (id) {
        $('input[name="ControlePresencaId"]').attr('value', id);
        $('#mdDeleteControlePresenca').modal('show');
        vm.DeleteControlePresenca(id)
    },
    EditModal: function (id) {
        $('input[name="ControlePresencaId"]').attr('value', id);
        $('#mdEditControlePresenca').modal('show');
        vm.EditControlePresenca(id)
    }
};