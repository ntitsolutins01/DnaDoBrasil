﻿var vm = new Vue({
    el: "#formDados",
    data: {
        params: {
            cpf: ""
        },
        loading: false
    },
    mounted: function () {
        var self = this;
        (function ($) {

            'use strict';

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

            $("#ddlEstado").change(function () {

                var url = "GetEmpresaByUnidade";

                var ddlSource = "#ddlUnidadeInfraestrutura";

                $.getJSON(url,
                    { id: $(ddlSource).val() },
                    function (data) {
                        var items = '';
                        $("#ddlProfissional").empty;
                        $.each(data,
                            function (i, row) {
                                items += "<option value='" + row.value + "'>" + row.text + "</option>";
                            });
                        $("#ddlProfissional").html(items);
                    });
            });

            $("#ddlMunicipio").change(function () {

                var url = "GetMunicipiosByUf";

                var ddlSource = "#ddlMunicipio";

                $.getJSON(url,
                    { id: $(ddlSource).val() },
                    function (data) {
                        var items = '';
                        $("#ddlMunicipio").empty;
                        $.each(data,
                            function (i, row) {
                                items += "<option value='" + row.value + "'>" + row.text + "</option>";
                            });
                        $("#ddlMunicipio").html(items);
                    });
            });

            var $numCpf = $("#cpf");
            $numCpf.mask('000.000.000-00', { reverse: false });

            var $numCpf1 = $("#cpf1");
            $numCpf1.mask('000.000.000-00', { reverse: false });

            var $numCpf2 = $("#cpf2");
            $numCpf2.mask('000.000.000-00', { reverse: false });

            var $numCpf3 = $("#cpf3");
            $numCpf3.mask('000.000.000-00', { reverse: false });

            var $numTel = $("#numTelefone");
            $numTel.mask('(00) 0000-0000');

            var $numTel = $("#numCelular");
            $numTel.mask('(00) 00000-0000');

            var $numCep = $("#cep");
            $numCep.mask('00000-000');

            $("#formDados").validate({
                rules: {
                    //cpf: { cpf: true, required: true }
                },
                messages: {
                    //cpf: { cpf: 'Formato de CPF inválido', required: "Por favor informe o número do CPF do responsável técnico." }
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

            jQuery.validator.addMethod("cpf", function (cpf, element) {
                var regex = /^\d{3}\.\d{3}\.\d{3}\-\d{2}$/;
                var add, rev, i;
                if (!regex.test(cpf))
                    return false;

                cpf = cpf.replace(/[^\d]+/g, '');
                if (cpf == '') return false;
                // Elimina CPFs invalidos conhecidos	
                if (cpf.length != 11 ||
                    cpf == "00000000000" ||
                    cpf == "11111111111" ||
                    cpf == "22222222222" ||
                    cpf == "33333333333" ||
                    cpf == "44444444444" ||
                    cpf == "55555555555" ||
                    cpf == "66666666666" ||
                    cpf == "77777777777" ||
                    cpf == "88888888888" ||
                    cpf == "99999999999")
                    return false;
                // Valida 1o digito	
                add = 0;
                for (i = 0; i < 9; i++)
                    add += parseInt(cpf.charAt(i)) * (10 - i);
                rev = 11 - (add % 11);
                if (rev == 10 || rev == 11)
                    rev = 0;
                if (rev != parseInt(cpf.charAt(9)))
                    return false;
                // Valida 2o digito	
                add = 0;
                for (i = 0; i < 10; i++)
                    add += parseInt(cpf.charAt(i)) * (11 - i);
                rev = 11 - (add % 11);
                if (rev == 10 || rev == 11)
                    rev = 0;
                if (rev != parseInt(cpf.charAt(10)))
                    return false;
                return true;


            }, "Informe um CPF válido");
        }).apply(this, [jQuery]);

        $('input[type=radio][name=rdbDeficiencia]').change(function () {
            if (this.value == 'sim') {
                $("#divDeficiencia").show();
            }
            else if (this.value == 'nao') {
                $("#divDeficiencia").hide();
            }
        });
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
        //ExisteCpf: function () {
        //    var self = this;
        //    self.ShowLoad(true, "vResponsavel");

        //    axios.get("GetResponsavelTecnicoByCpf/?cpf=" + self.params.cpf).then(result => {

        //        if (result.data !== false) {
        //            new PNotify({
        //                title: 'Responsavel',
        //                text: result.data,
        //                type: 'error'
        //            });
        //        }

        //        self.ShowLoad(false, "vResponsavel");

        //    }).catch(error => {
        //        Site.Notification("Erro ao buscar e analisar dados", error.response.data, "error", 1);
        //        self.ShowLoad(false, "vResponsavel");
        //    });
        //}
    }
});


//var vm = new Vue({
//    el: "#form",
//    data: {
//        params: {
//            cnpj: ""
//        },
//        loading: false
//    },
//    mounted: function () {
//        var self = this;
//        (function ($) {

//            'use strict';

//            jQuery.validator.addMethod("noSpace", function (value, element) {
//                return value == '' || value.trim().length != 0;
//            }, "Sem espaço por favor e não o deixe vazio");

//            var $numCnpj = $("#cnpj");
//            $numCnpj.mask('00.000.000/0000-00', { reverse: false });

//            var $numTel = $("#Telefone");
//            $numTel.mask('(00) 0000-0000');

//            $("#form").validate({
//                rules: {
//                    cnpj: { cnpj: true, required: true },
//                    Empresa: {
//                        noSpace: true
//                    }
//                },
//                messages: {
//                    cnpj: { cnpj: 'CNPJ inválido', required: "Por favor informe o CNPJ da empresa / órgão público" }
//                },
//                //submitHandler: function (form) {
//                //    alert('ok');
//                //},
//                highlight: function (label) {
//                    $(label).closest('.form-group').removeClass('has-success').addClass('has-error');
//                },
//                success: function (label) {
//                    $(label).closest('.form-group').removeClass('has-error');
//                    label.remove();
//                },
//                errorPlacement: function (error, element) {
//                    var placement = element.closest('.input-group');
//                    if (!placement.get(0)) {
//                        placement = element;
//                    }
//                    if (error.text() !== '') {
//                        placement.after(error);
//                    }
//                }
//            });

//            jQuery.validator.addMethod("cnpj",
//                function (value, element) {

//                    var numeros, digitos, soma, i, resultado, pos, tamanho, digitos_iguais;
//                    if (value.length == 0) {
//                        return false;
//                    }

//                    value = value.replace(/\D+/g, '');
//                    digitos_iguais = 1;

//                    for (i = 0; i < value.length - 1; i++)
//                        if (value.charAt(i) != value.charAt(i + 1)) {
//                            digitos_iguais = 0;
//                            break;
//                        }
//                    if (digitos_iguais)
//                        return false;

//                    tamanho = value.length - 2;
//                    numeros = value.substring(0, tamanho);
//                    digitos = value.substring(tamanho);
//                    soma = 0;
//                    pos = tamanho - 7;
//                    for (i = tamanho; i >= 1; i--) {
//                        soma += numeros.charAt(tamanho - i) * pos--;
//                        if (pos < 2)
//                            pos = 9;
//                    }
//                    resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
//                    if (resultado != digitos.charAt(0)) {
//                        return false;
//                    }
//                    tamanho = tamanho + 1;
//                    numeros = value.substring(0, tamanho);
//                    soma = 0;
//                    pos = tamanho - 7;
//                    for (i = tamanho; i >= 1; i--) {
//                        soma += numeros.charAt(tamanho - i) * pos--;
//                        if (pos < 2)
//                            pos = 9;
//                    }

//                    resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;

//                    return (resultado == digitos.charAt(1));
//                },
//                "Informe um CNPJ válido");

//        }).apply(this, [jQuery]);
//    },
//    methods: {
//        ShowLoad: function (flag, el) {
//            var self = this;

//            self.isLoading = flag;
//            $("#" + el).loadingOverlay({
//                "startShowing": flag
//            });
//            self.loading = flag;

//            if (!flag) {
//                self.isLoading = flag;
//                $("#" + el).removeClass("loading-overlay-showing");
//                self.loading = flag;
//            } else {
//                self.isLoading = flag;
//                $("#" + el).addClass("loading-overlay-showing");
//                self.loading = flag;
//            }
//        },
//        CancelarEdit: function (event) {
//            var self = this;

//            $("#ddlUnidadeInfraestrutura").select2("val", "0");

//        },
//        ExisteCnpj: function () {
//            var self = this;
//            self.ShowLoad(true, "vEmpresa");

//            axios.get("GetEmpresaByCnpj/?cnpj=" + self.params.cnpj).then(result => {

//                if (result.data !== false) {
//                    new PNotify({
//                        title: 'Empresa',
//                        text: result.data,
//                        type: 'error'
//                    });
//                }

//                self.ShowLoad(false, "vEmpresa");

//            }).catch(error => {
//                Site.Notification("Erro ao buscar e analisar dados", error.response.data, "error", 1);
//                self.ShowLoad(false, "vEmpresa");
//            });
//        }
//    }
//});