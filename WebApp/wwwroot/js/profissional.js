var vm = new Vue({
    el: "#formProfissional",
    data: {
        loading: false,
        editDto: { Id: "", Nome: "", DtNascimento: "", Email: "", AspNetUserId: "", Sexo: "", Cpf: "", Telefone: "", Celular: "", Endereco: "", Numero: "", Cep: "", Bairro: "",  Municipio: "", Ambientes: "", Contratos: "", Status: true  }
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

            if (formid === "formEditProfissional") {

                $("#formEditProfissional").validate({
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
        DeleteProfissional: function (id) {
            var url = "Profissional/Delete/" + id;
            $("#deleteProfissionalHref").prop("href", url);
        },
        EditProfissional: function (id) {
            var self = this;

            axios.get("Profissional/GetProfissionalById/?id=" + id).then(result => {

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
        $('input[name="ProfissionalId"]').attr('value', id);
        $('#mdDeleteProfissional').modal('show');
        vm.DeleteProfissional(id)
    },
    EditModal: function (id) {
        $('input[name="ProfissionalId"]').attr('value', id);
        $('#mdEditProfissional').modal('show');
        vm.EditProfissional(id)
    }
};