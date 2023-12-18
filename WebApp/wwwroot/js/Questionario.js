var vm = new Vue({
    el: "#formQuestionario",
    data: {
        loading: false,
        editDto: { Id: "",TipoLaudo: "", Pergunta: "" }
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

            if (formid === "formEditQuestionario") {

                $("#formEditQuestionario").validate({
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

            if (formid === "formQuestionario") {

                $("#formQuestionario").validate({
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
        DeleteQuestionario: function (id) {
            var url = "Questionario/Delete/" + id;
            $("#deleteQuestionarioHref").prop("href", url);
        },
        EditQuestionario: function (id) {
            var self = this;

            axios.get("Questionario/GetQuestionarioById/?id=" + id).then(result => {

                self.editDto.Id = result.data.id;
                self.editDto.TipoLaudo = result.data.tipoLaudo.nome;
                self.editDto.Pergunta = result.data.pergunta;
               
            }).catch(error => {
                Site.Notification("Erro ao buscar e analisar dados", error.message, "error", 1);
            });
        }
    }
});

var crud = {
    DeleteModal: function (id) {
        $('input[name="QuestionarioId"]').attr('value', id);
        $('#mdDeleteQuestionario').modal('show');
        vm.DeleteQuestionario(id)
    },
    EditModal: function (id) {
        $('input[name="QuestionarioId"]').attr('value', id);
        $('#mdEditQuestionario').modal('show');
        vm.EditQuestionario(id)
    }
};