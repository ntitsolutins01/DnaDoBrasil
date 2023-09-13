(function () {

    'use strict';

    // basic
    $("#form").validate({
        rules: {
            "Login.Email": {
                required: true,
                email: true
            },
            "Login.Password": {
                required: true,
                minlength: 8
            },
            "Input.Password": {
                required: true,
                minlength: 8
            },
            "Input.ConfirmPassword": {
                required: true,
                minlength: 8,
                //equalTo: "#Input.Password"
            },
            "Input.Email": {
                required: true,
                email: true
            }
        },
        messages: {
            "Login.Email": {
                required: "Por favor informe seu usuário.",
                email: "Formato de e-mail inválido."
            },
            "Login.Password": {
                required: "Por favor informe sua senha.",
                minlength: jQuery.validator.format("Formato de senha inválido, a senha deve conter no mínimo 8 digitos.")
            },
            "Input.Password": {
                required: "Por favor informe sua senha.",
                minlength: jQuery.validator.format("Formato de senha inválido, a senha deve conter no mínimo 8 digitos.")
            },
            "Input.ConfirmPassword": {
                required: "Por favor informe sua senha.",
                minlength: jQuery.validator.format("Formato de senha inválido, a senha deve conter no mínimo 8 digitos."),
                //equalTo: "As senhas digitadas são diferentes. Por favor, repita a operação."
            },
            "Input.Email": {
                required: "Por favor informe seu e-mail.",
                email: "Formato de e-mail inválido."
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

}).apply(this, [jQuery]);