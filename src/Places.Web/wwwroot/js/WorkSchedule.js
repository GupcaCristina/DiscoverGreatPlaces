
$(function () {

        $('.hasLunch').change(function () {
            if (this.checked)
                $('#lunchbreak').fadeIn('slow');
            else
                $('#lunchbreak').fadeOut('slow');

        });

        $(".hasLunch").checkboxradio();

});

