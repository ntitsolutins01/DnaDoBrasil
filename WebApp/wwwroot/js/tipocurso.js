var vm = new Vue({
    el: "#vTipoCurso ",
    data: {
        loading: false,
        editDto: { Id: "", Nome: "", Status: true }
    },
    mounted: function () {
        var self = this;
        (function ($) {
            'use strict';

            //skin checkbox
            if (typeof Switch !== 'undefined' && $.isFunction(Switch)) {

                $(function () {
                    $('[data-plugin-ios-switch]').each(function () {
                        var $this = $(this);

                        $this.themePluginIOS7Switch();
                    });
                });
            }

            var formid = $('form')[1].id;

            if (formid === "formEditTipoCurso") {

                $("#formEditTipoCurso ").validate({
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

            if (formid === "formTipoCurso") {


                $("#formTipoCurso").validate({
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
        DeleteTipoCurso: function (id) {
            var url = "TipoCurso/Delete/" + id;
            $("#deleteTipoCursoHref").prop("href", url);
        },
        EditTipoCurso: function (id) {
            var self = this;

            axios.get("TipoCurso/GetTipoCursoById/?id=" + id).then(result => {

                self.editDto.Id = result.data.id;
                self.editDto.Nome = result.data.nome;
                self.editDto.Status = result.data.status;

            }).catch(error => {
                Site.Notification("Erro ao buscar e analisar dados", error.message, "error", 1);
            });
        }
    }
});

var crud = {
    DeleteModal: function (id) {
        $('input[name="deleteTipoCursoId"]').attr('value', id);
        $('#mdDeleteTipoCurso').modal('show');
        vm.DeleteTipoCurso(id)
    },
    EditModal: function (id) {
        $('input[name="editTipoCursoId"]').attr('value', id);
        $('#mdEditTipoCurso').modal('show');
        vm.EditTipoCurso(id)
    }
};