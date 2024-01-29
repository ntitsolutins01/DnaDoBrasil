var vm = new Vue({
    el: "#vAluno",
    data: {
        loading: false
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

            //clique de escolha do select
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
                                title: 'Usuario',
                                text: data,
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
                                text: 'Localidades não encontradas.',
                                type: 'warning'
                            });
                        }
                    });
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
        }
    }
});
var crud = {
    CarterinhaModal: function (id) {
        $('input[name="carteirinhaAlunoId"]').attr('value', id);
        $('#mdCarteirinhaAluno').modal('show');
    }
};
