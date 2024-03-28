var vm = new Vue({
    el: "#vControlePresenca",
    data: {
        loading: false
    },
    mounted: function () {
        var self = this;
        (function ($) {
            'use strict';

            var formid = $('form').attr('id');


            //triggered when modal is about to be shown
            $('#mdTermo').on('show.bs.modal', function (e) {

                //get data-id attribute of the clicked element
                var id = $(e.relatedTarget).data('id');

                if (id == "P") {
                    $("#divJustificativa").css("display", "none");
                } else {
                    $("#divJustificativa").css("display", "inline");
                }
            });

            if (formid === "formControlePresenca") {
                $("#formControlePresenca").validate({
                    rules: {
                        "email": {
                            required: true,
                            email: true
                        },
                        "senha": {
                            required: true,
                            minlength: 8
                        }
                    },
                    messages: {
                        "email": {
                            required: "Por favor informe o endereço eletrônico válido do profissional.",
                            email: "Formato de e-mail inválido."
                        },
                        "senha": {
                            required: "Por favor informe sua senha.",
                            minlength: jQuery.validator.format("Formato de senha inválido, a senha deve conter no mínimo 8 digitos.")
                        }
                    },
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