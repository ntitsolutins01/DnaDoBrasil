var vm = new Vue({
    el: "#vLaudoDetails",
    data: {},
    mounted: function () {
        var self = this;
        (function ($) {

            'use strict';

            /*
            Liquid Meter Saude
            */
            if ($('#meterSaude').get(0)) {
                $('#meterSaude').liquidMeter({
                    shape: 'circle',
                    color: '#d2322d',
                    background: '#F9F9F9',
                    fontSize: '24px',
                    fontWeight: '600',
                    stroke: '#F2F2F2',
                    textColor: '#333',
                    liquidOpacity: 0.9,
                    liquidPalette: ['#333'],
                    speed: 3000,
                    animate: !$.browser.mobile
                });
            }

            /*
            Liquid Meter TalentoEsportivo
            */
            if ($('#meterTalentoEsportivo').get(0)) {
                $('#meterTalentoEsportivo').liquidMeter({
                    shape: 'circle',
                    color: '#ed9c28',
                    background: '#F9F9F9',
                    fontSize: '24px',
                    fontWeight: '600',
                    stroke: '#F2F2F2',
                    textColor: '#333',
                    liquidOpacity: 0.9,
                    liquidPalette: ['#333'],
                    speed: 3000,
                    animate: !$.browser.mobile
                });
            }

            /*
            Liquid Meter ConsumoAlimentar
            */
            if ($('#meterConsumoAlimentar').get(0)) {
                $('#meterConsumoAlimentar').liquidMeter({
                    shape: 'circle',
                    color: '#47a447',
                    background: '#F9F9F9',
                    fontSize: '24px',
                    fontWeight: '600',
                    stroke: '#F2F2F2',
                    textColor: '#333',
                    liquidOpacity: 0.9,
                    liquidPalette: ['#333'],
                    speed: 3000,
                    animate: !$.browser.mobile
                });
            }

            /*
            Liquid Meter SaudeBucal
            */
            if ($('#meterSaudeBucal').get(0)) {
                $('#meterSaudeBucal').liquidMeter({
                    shape: 'circle',
                    color: '#5bc0de',
                    background: '#F9F9F9',
                    fontSize: '24px',
                    fontWeight: '600',
                    stroke: '#F2F2F2',
                    textColor: '#333',
                    liquidOpacity: 0.9,
                    liquidPalette: ['#333'],
                    speed: 3000,
                    animate: !$.browser.mobile
                });
            }

            /*
            Liquid Meter QualidadeVida
            */
            if ($('#meterQualidadeVida').get(0)) {
                $('#meterQualidadeVida').liquidMeter({
                    shape: 'circle',
                    color: '#00a4a9',
                    background: '#F9F9F9',
                    fontSize: '24px',
                    fontWeight: '600',
                    stroke: '#F2F2F2',
                    textColor: '#333',
                    liquidOpacity: 0.9,
                    liquidPalette: ['#333'],
                    speed: 3000,
                    animate: !$.browser.mobile
                });
            }

            /*
            Liquid Meter Vocacional
            */
            if ($('#meterVocacional').get(0)) {
                $('#meterVocacional').liquidMeter({
                    shape: 'circle',
                    color: '#cdbadb',
                    background: '#F9F9F9',
                    fontSize: '24px',
                    fontWeight: '600',
                    stroke: '#F2F2F2',
                    textColor: '#333',
                    liquidOpacity: 0.9,
                    liquidPalette: ['#333'],
                    speed: 3000,
                    animate: !$.browser.mobile
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
        }
    }
});