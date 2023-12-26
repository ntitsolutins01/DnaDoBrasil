var vm = new Vue({
    el: "#vParceiro",
    data: {
        params: {
            cpf: "",
            visible: false
        },
        loading: false,
        editDto: { Id: "", Nome: "", DtNascimento: "", Email: "", AspNetUserId: "", Sexo: "", Cpf: "", Telefone: "", Celular: "", Endereco: "", Numero: "", Cep: "", Bairro: "", Municipio: "",Habilitado: true, Status: true }
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
                var sigla = $("#ddlEstado").val();

                var url = "../DivisaoAdministrativa/GetMunicipioByUf?uf=" + sigla;

                var ddlSource = "#ddlMunicipio";

                $.getJSON(url, { id: $(ddlSource).val() }, function (data) {
                    if (data.length > 0) {
                        var items = '<option value="">Selecionar Municipio</option>';
                        $("#ddlMunicipio").empty;
                        $.each(data,
                            function (i, row) {
                                items += "<option value='" + row.value + "'>" + row.text + "</option>";
                            });
                        $("#ddlMunicipio").html(items);
                    }
                    else {
                        new PNotify({
                            title: 'Parceiro',
                            text: data,
                            type: 'warning'
                        });
                    }
                });
            });

            var $numCpf = $("#cpf");
            $numCpf.mask('000.000.000-00', { reverse: false });

            var $numCnpj = $("#cnpj");
            $numCnpj.mask('00.000.000/0000-00', { reverse: false });

            var $numTel = $("#numTelefone");
            $numTel.mask('(00) 0000-0000');

            var $numTel = $("#numCelular");
            $numTel.mask('(00) 00000-0000');

            var $numCep = $("#cep");
            $numCep.mask('00000-000');

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

            var formid = $('form').attr('id');

            //Inclusao
            if (formid === "formParceiro") {
                
                $("#formParceiro").validate({
                    rules: {
                        "email": {
                            required: true,
                            email: true
                        },
                        cpf: { cpf: true, required: true }
                    },
                    messages: {
                        "email": {
                            required: "Por favor informe o endereço eletrônico válido do usuário.",
                            email: "Formato de e-mail inválido."
                        },
                        cpf: { cpf: 'Formato de CPF inválido', required: "Por favor informe o número do CPF do parceiro." }
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
            //Ediçao
            if (formid === "formEditParceiro") {

                FormatInputs();

                $("#formEditParceiro").validate({
                    rules: {
                        "EndEmail": {
                            required: true,
                            email: true
                        },
                        cpf: { cpf: true, required: true }
                    },
                    messages: {
                        "EndEmail": {
                            required: "Por favor informe o endereço eletrônico válido do usuário.",
                            email: "Formato de e-mail inválido."
                        },
                        cpf: { cpf: 'Formato de CPF inválido', required: "Por favor informe o número do CPF do parceiro." }
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
            //Habilitar
            if (formid === "formHabilitarParceiro") {

                $("#formHabilitarParceiro").validate({
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
        EditPendentes: function (id) {
            var self = this;

            axios.get("../SistemaSocioeconomico/GetParceiroById/?id=" + id).then(result => {

                self.editDto.Id = result.data.id;

                var alunos = result.data.alunos;

                $('#alunosSolicitados').DataTable().destroy();

                $('#alunosSolicitados').DataTable({
                    data: alunos,
                    "columns": [
                        {
                            "data": null,
                            "sortable": false,
                            "render": function (c) {
                                return "";
                            }
                        },
                        { "data": "id" },
                        { "data": "nome" },
                        { "data": "idade" },
                        {
                            "data": null,
                            "sortable": false,
                            "render": function (c) {
                                return "<a class='text-center' style='color:#1E88E5' href='javascript:(crud.visualizarLaudo(" +
                                    c.Id +
                                    "))' data-toggle='tooltip' data-placement='top' title='Visualizar Laudo'><i class='fa fa-eye'></i></a>";
                            }
                        }
                    ],
                    columnDefs: [
                        {
                            orderable: false,
                            className: 'select-checkbox',
                            targets: 0
                        },
                        { "className": "text-center", "targets": "_all" }
                    ],
                    select: {
                        style: 'os',
                        selector: 'td:first-child'
                    },
                    order: [[1, 'asc']],
                    "paging": true,
                    "searching": true,
                    "language": {
                        "sEmptyTable": "Nenhum registro encontrado",
                        "sInfo": "Mostrando de _START_ até _END_ de _TOTAL_ registros",
                        "sInfoEmpty": "Mostrando 0 até 0 de 0 registros",
                        "sInfoFiltered": "(Filtrados de _MAX_ registros)",
                        "sInfoPostFix": "",
                        "sInfoThousands": ".",
                        "sLengthMenu": "_MENU_ resultados por página",
                        "sLoadingRecords": "Carregando...",
                        "sProcessing": "Processando...",
                        "sZeroRecords": "Nenhum registro encontrado",
                        "sSearch": "Pesquisar: ",
                        "select": {
                            "rows": {
                                "_": "Você selecionou %d linhas",
                                "0": "Clique em uma linha para selecioná-la",
                                "1": "Apenas 1 linha selecionada"
                            }
                        },
                        "oPaginate": {
                            "sNext": "Próximo →" +
                                "" +
                                "",
                            "sPrevious": "← Anterior",
                            "sFirst": "Primeiro",
                            "sLast": "Último"
                        },
                        "oAria": {
                            "sSortAscending": ": Ordenar colunas de forma ascendente",
                            "sSortDescending": ": Ordenar colunas de forma descendente"
                        }
                    }
                });

                $('#alunosSolicitados').DataTable().draw();

            }).catch(error => {
                Site.Notification("Erro ao buscar e analisar dados", error.message, "error", 1);
            });

            
        },
        ExisteCpf: function () {
            var self = this;
            self.ShowLoad(true, "vParceiro");

            axios.get("GetParceiroByCpf/?cpf=" + self.params.cpf).then(result => {

                if (result.data === false) {
                    new PNotify({
                        title: 'Parceiro',
                        text: "Já existe um usuário cadastrado com esse cpf.",
                        type: 'warning'
                    });
                }

                self.ShowLoad(false, "vParceiro");

            }).catch(error => {
                Site.Notification("Erro ao buscar e analisar dados", error.response.data, "error", 1);
                self.ShowLoad(false, "vParceiro");
            });
        },
        ExisteEmail: function () {
            var self = this;
            self.ShowLoad(true, "vParceiro");

            axios.get("GetParceiroByEmail/?email=" + self.params.email).then(result => {

                if (result.data === false) {
                    new PNotify({
                        title: 'Parceiro',
                        text: "Já existe um usuário cadastrado com esse email.",
                        type: 'warning'
                    });
                }

                self.ShowLoad(false, "vParceiro");

            }).catch(error => {
                Site.Notification("Erro ao buscar e analisar dados", error.response.data, "error", 1);
                self.ShowLoad(false, "vParceiro");
            });
        },
        EditParceiro: function (id) {
            var self = this;

            axios.get("Parceiro/GetParceiroById/?id=" + id).then(result => {

                self.editDto.Id = result.data.id;
                self.editDto.Nome = result.data.nome;
                self.editDto.Status = result.data.status;
                self.editDto.DtNascimento = result.data.dtnascimento;
                self.editDto.Email = result.data.email;
                self.editDto.AspNetUserId = result.data.aspnetuserid;
                self.editDto.Sexo = result.data.sexo;
                self.editDto.Cpf = result.data.cpf;
                self.editDto.Telefone = result.data.telefone;
                self.editDto.Celular = result.data.celular;
                self.editDto.Endereco = result.data.endereco;
                self.editDto.Numero = result.data.numero;
                self.editDto.Cep = result.data.cep;
                self.editDto.Bairro = result.data.bairro;
                self.editDto.Municipio = result.data.municipio;
                self.editDto.Ambientes = result.data.ambientes;
                self.editDto.Contratos = result.data.contratos;

            }).catch(error => {
                Site.Notification("Erro ao buscar e analisar dados", error.message, "error", 1);
            });
        }
    }
});

var crud = {
    DeleteModal: function (id) {
        $('input[name="ParceiroId"]').attr('value', id);
        $('#mdDeleteParceiro').modal('show');
        vm.DeleteParceiro(id)
    },
    HabilitarModal: function (id) {
        $('input[name="habilitarParceiroId"]').attr('value', id);
        $('#mdHabilitar').modal('show');
    },
    PendentesModal: function (id) {
        $('#mdPendentes').modal('show');
        vm.EditPendentes(id)
    }
};