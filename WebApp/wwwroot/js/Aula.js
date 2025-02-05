var vm = new Vue({
    el: "#vAula ",
    data: {
        loading: false,
        editDto: { Id: "", Titulo: "", Descricao: "", CargaHoraria: "", Status: true }
    },
    mounted: function () {
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

            //mascara dos inputs
            var cargaHoraria = $("#cargaHoraria");
            cargaHoraria.mask('000', { reverse: false });


            //skin checkbox
            if (typeof Switch !== 'undefined' && $.isFunction(Switch)) {

                $(function () {
                    $('[data-plugin-ios-switch]').each(function () {
                        var $this = $(this);

                        $this.themePluginIOS7Switch();
                    });
                });
            }

            var formid = $('form')[1].id;

            if (formid === "formEditAula") {

                $("#formEditAula ").validate({
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

            if (formid === "formAula") {

                $("#ddlTipoCurso").change(function () {
                    var tipoCursoId = $("#ddlTipoCurso").val();

                    var sigla = $("#ddlTipoCurso").val();

                    var url = "../Curso/GetCursosAllByTipoCursoId";

                    $.getJSON(url,
                        { id: tipoCursoId },
                        function (data) {
                            if (data.length > 0) {
                                var items = '<option value="">Selecionar Curso</option>';
                                $("#ddlCurso").empty;
                                $.each(data,
                                    function (i, row) {
                                        items += "<option value='" + row.value + "'>" + row.text + "</option>";
                                    });
                                $("#ddlCurso").html(items);
                            }
                            else {
                                new PNotify({
                                    title: 'Curso',
                                    text: 'Curso n�o encontrados.',
                                    type: 'warning'
                                });
                            }
                        });
                });

                $("#ddlCurso").change(function () {
                    var cursoId = $("#ddlCurso").val();

                    var sigla = $("#ddlCurso").val();

                    var url = "../ModuloEad/GetModulosEadAllByCursoId";

                    $.getJSON(url,
                        { id: cursoId },
                        function (data) {
                            if (data.length > 0) {
                                var items = '<option value="">Selecionar Modulo</option>';
                                $("#ddlModuloEad").empty;
                                $.each(data,
                                    function (i, row) {
                                        items += "<option value='" + row.value + "'>" + row.text + "</option>";
                                    });
                                $("#ddlModuloEad").html(items);
                            }
                            else {
                                new PNotify({
                                    title: 'Modulo',
                                    text: 'Modulo n�o encontrados.',
                                    type: 'warning'
                                });
                            }
                        });
                });

                $("#formAula").validate({

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
        DeleteAula: function (id) {
            var url = "Aula/Delete/" + id;
            $("#deleteAulaHref").prop("href", url);
        },
        EditAula: function (id) {
            var self = this;

            axios.get("Aula/GetAulaById/?id=" + id).then(result => {

                self.editDto.Id = result.data.id;
                self.editDto.Titulo = result.data.titulo;
                self.editDto.Descricao = result.data.descricao;
                self.editDto.CargaHoraria = result.data.cargaHoraria;
                self.editDto.Status = result.data.status;

                if (result.data.listProfessores.length > 0) {
                    var items = '<option value="">Selecionar o Professor</option>';
                    $("#ddlProfessor").empty;
                    $.each(result.data.listProfessores,
                        function (i, row) {
                            if (row.selected) {
                                items += "<option selected value='" + row.value + "'>" + row.text + "</option>";
                            } else {
                                items += "<option value='" + row.value + "'>" + row.text + "</option>";
                            }
                        });
                    $("#ddlProfessor").html(items);
                }
                else {
                    new PNotify({
                        title: 'Professor',
                        text: 'Professores não encontrados.',
                        type: 'warning'
                    });
                }


            }).catch(error => {
                Site.Notification("Erro ao buscar e analisar dados", error.message, "error", 1);
            });
        }
    }
});

var crud = {
    DeleteModal: function (id) {
        $('input[name="deleteAulaId"]').attr('value', id);
        $('#mdDeleteAula').modal('show');
        vm.DeleteAula(id)
    },
    EditModal: function (id) {
        $('input[name="editAulaId"]').attr('value', id);
        $('#mdEditAula').modal('show');
        vm.EditAula(id)
    }
};