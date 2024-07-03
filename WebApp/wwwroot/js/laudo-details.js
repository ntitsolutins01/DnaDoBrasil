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
            if ($('#meterSales').get(0)) {
                $('#meterSales').liquidMeter({
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