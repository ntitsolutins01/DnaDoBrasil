var vm = new Vue({
    el: "#vAmbienteAluno",
    data: {
        params: {
            cpf: "",
            AmbienteAlunos: []
        },
        loading: false
    },
    mounted: function ()     {
        var self = this;
        (function ($) {

            'use strict';
            //skin select
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
        AddAmbienteAluno: function () {
            var self = this;
            self.ShowLoad(true, "vAmbienteAluno");

            var mapped = $("#ddlAmbienteAluno").select2('data');

            $('#AmbienteAlunoDataTable').DataTable().destroy();

            var table = $('#AmbienteAlunoDataTable').DataTable({
                columnDefs: [
                    { "className": "text-center", "targets": "_all" }
                ]
            });

            table.row.add([mapped[0].id, mapped[0].text,
            "<a style='color:#F44336' href='javascript:(crud.DeleteAmbienteAluno(\"" + mapped[0].id + "\"))'><i class='fa fa-trash'></i></a>"])
                .draw();

            self.params.AmbienteAlunos.push(mapped[0].id);

            $('input[name="arrAmbienteAlunos"]').attr('value', self.params.AmbienteAlunos);

            $("#ddlAmbienteAluno").select2("val", "0");

            self.ShowLoad(false, "vAmbienteAluno");
        },
        DeleteAmbienteAluno: function (index) {
            var self = this;
            self.ShowLoad(true, "vAmbienteAluno");

            var table = $('#AmbienteAlunoDataTable').DataTable();

            table.row(index).remove().draw();

            $('#AmbienteAlunoDataTable tbody').on('click', 'tr', function () {
                //alert('Row index: ' + table.row(this).index());
                var index = table.row(this).index();
                table.row(index).remove().draw();
            });
        }
    }
});
var crud = {
    DeleteModal: function (id) {
        $('input[name="deleteProfissionalId"]').attr('value', id);
        $('#mdDeleteProfissional').modal('show');
        vm.DeleteProfissional(id)
    },
    HabilitarModal: function (id) {
        $('input[name="habilitarProfissionalId"]').attr('value', id);
        $('#mdHabilitarProfissional').modal('show');
    },
    AddAmbienteAluno: function () {
        vm.AddAmbienteAluno()
    },
    DeleteAmbienteAluno: function (index) {
        vm.DeleteAmbienteAluno(index)
    }
};
