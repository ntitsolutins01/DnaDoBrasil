var vm = new Vue({
    el: "#vPesquisarAluno",
    data: {
        loading: false,
        editDto: { Id: "", Nome: "", Status: true, Email: "", Sexo: "", DtNascimento: "", MunicipioEstado: "", NomeLocalidade: "" }
    },
    mounted: function () {
        var self = this;
        (function ($) {
            'use strict';

            var formid = $('form').attr('id');

            if (formid === "formPesquisarAluno") {

                //clique de escolha do select
                $("#ddlEstado").change(function () {
                    var sigla = $("#ddlEstado").val();

                    var url = "../../DivisaoAdministrativa/GetMunicipioByUf?uf=" + sigla;

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
        DeleteAluno: function (id) {
            var url = "Aluno/Delete/" + id;
            $("#deleteAlunoHref").prop("href", url);
        },
        CarteirinhaAluno: function (id) {
            var self = this;

            


            axios.get("Aluno/GetAlunoById/?id=" + id).then(result => {

                self.editDto.Id = result.data.id;
                self.editDto.Nome = result.data.nome;
                self.editDto.Status = result.data.status;
                self.editDto.Email = result.data.email;
                self.editDto.Sexo = result.data.sexo;
                self.editDto.DtNascimento = result.data.dtNascimento;
                self.editDto.MunicipioEstado = result.data.municipioEstado;
                self.editDto.NomeLocalidade = result.data.nomeLocalidade;
                self.editDto.Controle = result.data.controle;

                $('#qr').ClassyQR({
                    create: true,// signals the library to create the image tag inside the container div.
                    type: 'text',// text/url/sms/email/call/locatithe text to encode in the QR. on/wifi/contact, default is TEXT
                    text: self.editDto.Id // the text to encode in the QR.
                });

            }).catch(error => {
                Site.Notification("Erro ao buscar e analisar dados", error.message, "error", 1);
            });
        }
    }
});
var crud = {
    CarterinhaModal: function (id) {
        $('input[name="carteirinhaAlunoId"]').attr('value', id);
        $('#mdCarteirinhaAluno').modal('show');
        vm.CarteirinhaAluno(id);
    }
};
