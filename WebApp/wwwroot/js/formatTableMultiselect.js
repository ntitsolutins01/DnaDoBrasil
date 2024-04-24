(function ($) {

    'use strict';

    var datatableInit = function () {

        $('#datatable-default').dataTable({
            layout: {
                topStart: {
                    buttons: ['selectAll', 'selectNone']
                }
            },
            dom: '<"row"<"col-lg-6"l><"col-lg-6"f>><"table-responsive"t>p',
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
                selector: 'td:first-child',
                style: 'multi'
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

    };

    $(function () {
        datatableInit();
    });

}).apply(this, [jQuery]);