var vm = new Vue({
    el: "#vQuestaoEad",
    data: {
        loading: false,
    },
    mounted: function () {
        var self = this;
        (function ($) {
            'use strict';

            var formid = $('form')[1].id;

            if (formid === "formQuestaoEad") {

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
                                    text: 'Curso não encontrados.',
                                    type: 'warning'
                                });
                            }
                        });
                });

                $("#ddlCurso").change(function () {
                    var cursoId = $("#ddlCurso").val();

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
                                    text: 'Modulo não encontrados.',
                                    type: 'warning'
                                });
                            }
                        });
                });

                $("#ddlModuloEad").change(function () {
                    var moduloEadId = $("#ddlModuloEad").val();

                    var url = "../Aula/GetAulasAllByModuloEadId";

                    $.getJSON(url,
                        { id: moduloEadId },
                        function (data) {
                            if (data.length > 0) {
                                var items = '<option value="">Selecionar Aula</option>';
                                $("#ddlAula").empty;
                                $.each(data,
                                    function (i, row) {
                                        items += "<option value='" + row.value + "'>" + row.text + "</option>";
                                    });
                                $("#ddlAula").html(items);
                            }
                            else {
                                new PNotify({
                                    title: 'Aula',
                                    text: 'Modulo não encontrados.',
                                    type: 'warning'
                                });
                            }
                        });
                });

                $("#formQuestaoEad").validate({
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
        addTextoImagem: function () {
            var self = this;

            $('.wrapper').append(`
                <div class="col-sm-4">
	                <select class="form-control populate select2" name="ddlTipoTextoImagem" id="ddlTipoTextoImagem"
			                title="Por favor selecione um sexo." required style="width:100%">
		                <option value="">Selecionar tipo texto ou imagem</option>
		                <option value="T">TEXTO</option>
		                <option value="I">IMAGEM</option>
	                </select>
                </div>
            `);

            self.setSelected2("ddlTipoTextoImagem");

            $("#ddlTipoTextoImagem").change(function () {
                var value = $("#ddlTipoTextoImagem").val();

                var innerHTML = '';

                switch (value) {

                    case 'T':

                        $('.wrapper').append(`
                                            <div class="col-sm-8">
                                                <textarea class="form-control" rows="3" maxlength="500" name="texto" id="texto"></textarea>
                                            </div>
                                        `);
                        break;

                    case 'I':
                        $('.wrapper').append(`
                                            <div class="col-sm-5">
                                                <div class="fileupload fileupload-new" data-provides="fileupload">
                                                    <div class="input-append">
                                                        <div class="uneditable-input">
                                                            <i class="fa fa-file fileupload-exists"></i>
                                                            <span class="fileupload-preview"></span>
                                                        </div>
                                                        <span class="btn btn-default btn-file">
                                                            <span class="fileupload-exists">Alterar</span>
                                                            <span class="fileupload-new">Selecionar Arquivo</span>
                                                            <input type="file" id="arquivo" name="arquivo" />
                                                        </span>
                                                        <a href="#" class="btn btn-default fileupload-exists" data-dismiss="fileupload">Remover</a>
                                                    </div>
                                                </div>
                                            </div>
                                        `);
                        break;

                    default:
                        break;
                }
            });
        },
        createElement: function (str) {

            const container = document.getElementById("textimg-container");
            var div = document.createElement('div');
            div.className = "form-group";
            div.innerHTML = str;

            for (var i = 0; i < div.childNodes.length; i++) {
                var node = div.childNodes[i].cloneNode(true);
                container.appendChild(node);
            }
            return container.childNodes;
        },
        setSelected2: function (ddl) {
            var $select = $("#"+ddl).select2({
                allowClear: true
            });

            $("#" + ddl).each(function () {
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
        },
        DeleteQuestaoEad: function (id) {
            var url = "QuestaoEad/Delete/" + id;
            $("#deleteQuestaoEadHref").prop("href", url);
        }
    }
});

var crud = {
    DeleteModal: function (id) {
        $('input[name="deleteQuestaoEadId"]').attr('value', id);
        $('#mdDeleteQuestaoEad').modal('show');
        vm.DeleteQuestaoEad(id)
    }
};