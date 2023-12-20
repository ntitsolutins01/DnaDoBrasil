var vm = new Vue({
    el: "#formParceiro",
    data: {},
    mounted: function () {
        var self = this;
        (function ($) {

            'use strict';
            var formid = $('form').attr('id');

            $("#formParceiro").validate({
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
                });
            }

            $("#formEditParceiro").validate({
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

            axios.get("Parceiro/GetParceiroById/?id=" + id).then(result => {

                self.editDto.Id = result.data.id;

                var alunos = result.data.alunos;

                $('#alunosSolicitados').DataTable().destroy();

                $('#alunosSolicitados').DataTable({
                    data: alunos,
                    "columns": [
                        { "data": "nome" },
                        { "data": "idade" },
                        { "data": "talento" }
                    ],
                    "paging": false,
                    "searching": false,
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
        }
    }
});

var crud = {
    DeleteModal: function (id) {
        $('input[name="ParceiroId"]').attr('value', id);
        $('#mdDeleteParceiro').modal('show');
        vm.DeleteParceiro(id)
    },
    EditModal: function (id) {
        $('input[name="ParceiroId"]').attr('value', id);
        $('#mdEditParceiro').modal('show');
        vm.EditParceiro(id)
    },
    PendentesModal: function (id) {
        $('input[name="ParceiroId"]').attr('value', id);
        $('#mdEditParceiro').modal('show');
        vm.EditPendentes(id)
    }
};