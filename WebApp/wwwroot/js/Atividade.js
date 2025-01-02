var vm = new Vue({
    el: "#vAtividade",
    data: {
        loading: false,
        editDto: {
            Id: "", Atividade: "", Nome: "", Decricao: "",
            IdadeInicial: "", IdadeFinal: "", Status: true
        },
        params: {
            visible: true
        }
    },
    mounted: function () {
        var self = this;
        (function ($) {
            'use strict';

            var formid = $('form')[1].id;

            //skin checkbox
            if (typeof Switch !== 'undefined' && $.isFunction(Switch)) {

                $(function () {
                    $('[data-plugin-ios-switch]').each(function () {
                        var $this = $(this);

                        $this.themePluginIOS7Switch();
                    });
                });
            }

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

            if (formid === "formEditAtividade") {

                $("#formEditAtividade").validate({
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

            if (formid === "formAtividade") {

                $("#ddlEstado").change(function () {
                    var sigla = $("#ddlEstado").val();

                    var url = "../DivisaoAdministrativa/GetMunicipioByUf?uf=" + sigla;

                    var ddlSource = "#ddlMunicipio";

                    $.getJSON(url,
                        { id: $(ddlSource).val() },
                        function (data) {
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
                                    title: 'Fomento',
                                    text: 'Munic�pios n�o encontrados.',
                                    type: 'warning'
                                });
                            }
                        });
                });

                //clique de escolha do select
                $("#ddlMunicipio").change(function () {
                    var id = $("#ddlMunicipio").val();

                    var url = "../../Localidade/GetLocalidadeByMunicipio?id=" + id;

                    var ddlSource = "#ddlLocalidade";

                    $.getJSON(url,
                        { id: $(ddlSource).val() },
                        function (data) {
                            if (data.length > 0) {
                                var items = '<option value="">Selecionar Localidade</option>';
                                $("#ddlLocalidade").empty;
                                $.each(data,
                                    function (i, row) {
                                        items += "<option value='" + row.value + "'>" + row.text + "</option>";
                                    });
                                $("#ddlLocalidade").html(items);
                            }
                            else {
                                new PNotify({
                                    title: 'Localidades',
                                    text: 'Localidades n�o encontradas.',
                                    type: 'warning'
                                });
                            }
                        });
                });

                //clique de escolha do select
                $("#ddlLocalidade").change(function () {
                    var id = $("#ddlLocalidade").val();

                    var url = "../../Estrutura/GetEstruturasByLocalidade";

                    $.getJSON(url,
                        { id: id },
                        function (data) {
                            if (data.length > 0) {
                                var items = '<option value="">Selecionar Estrutura</option>';
                                $("#ddlEstrutura").empty;
                                $.each(data,
                                    function (i, row) {
                                        items += "<option value='" + row.value + "'>" + row.text + "</option>";
                                    });
                                $("#ddlEstrutura").html(items);
                            }
                            else {
                                new PNotify({
                                    title: 'Estruturas',
                                    text: 'Estruturas n�o encontradas.',
                                    type: 'warning'
                                });
                            }
                        });
                });

                $("#formAtividade").validate({
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
        DeleteAtividade: function (id) {
            var url = "Atividade/Delete/" + id;
            $("#deleteAtividadeHref").prop("href", url);
        },
        EditAtividade: function (id) {
            var self = this;

            self.editDto = {
                Id: "",
                Codigo: "",
                Nome: "",
                Descricao: "",
                idadeInicial: "",
                IdadeFim: "",
                Status: true
            };

            axios.get("Atividade/GetAtividadeById/?id=" + id).then(result => {
                //console.log('Dados retornados:', result.data);

                self.$nextTick(() => {
                    self.editDto = {
                        Id: result.data.id,
                        Nome: result.data.nome,
                        Codigo: result.data.codigo,
                        IdadeInicial: result.data.idadeInicial,
                        IdadeFinal: result.data.idadeFinal,
                        Descricao: result.data.descricao,
                        Status: result.data.status
                    };
                });

            }).catch(error => {
                console.error('Erro ao carregar dados:', error);
                Site.Notification("Erro ao buscar e analisar dados", error.message, "error", 1);
            });
        }
    }
});

var crud = {
    DeleteModal: function (id) {
        $('input[name="deleteAtividadeId"]').attr('value', id);
        $('#mdDeleteAtividade').modal('show');
        vm.DeleteAtividade(id)
    },
    EditModal: function (id) {
        $('input[name="editAtividadeId"]').attr('value', id);
        $('#mdEditAtividade').modal('show');
        vm.EditAtividade(id)
    }
};