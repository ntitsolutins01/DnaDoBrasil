var vm = new Vue({
    el: "#formContrato",
    data: {
        loading: false,
        editDto: { Id: "", Nome: "", Status: true }
},
    mounted: function () {
        var self = this;
        (function ($) {

            'use strict';

            $("#formContrato").validate({
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

            $("#formEditContrato").validate({
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
         DeleteContrato: function (id) {
            var url = "Contrato/Delete/" + id;
            $("#deleteContratoHref").prop("href", url);
        },
        EditContrato: function (id) {
            var self = this;

            axios.get("Contrato/GetContratoById/?id=" + id).then(result => {

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
        $('input[name="ContratoId"]').attr('value', id);
        $('#mdDeleteContrato').modal('show');
        vm.DeleteContrato(id)
    },
    EditModal: function (id) {
        $('input[name="ContratoId"]').attr('value', id);
        $('#mdEditContrato').modal('show');
        vm.EditContrato(id)
    }
};