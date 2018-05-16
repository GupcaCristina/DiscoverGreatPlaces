
$(function () {
    $.ajax({
        url: " http://localhost:52217/Address/GetCities?id=1",
        type: "GET",
        datatype: 'json',
        contentType: 'json',
        success: function (data) {
            var results = JSON.parse(data);
            $("#CityId").empty();

            $.each(results, function (index, region) {
                $("#CityId").append($('<option/>', {
                    value: region.Id,
                    text: region.Name
                }));
            });
            //
            var Countryid = jQuery("#CountryId option:selected").val();
            $.ajax({
                url: " http://localhost:52217/Address/GetStreets",
                type: "GET",
                data: { countryId: Countryid, cityId: 1 },
                datatype: 'json',
                contentType: 'json',

                success: function (data) {
                    var results = JSON.parse(data);
                    $("#Street").empty();

                    $.each(results, function (index, region) {
                        $("#Street").append($('<option/>', {
                            value: region.Id,
                            text: region.Name
                        }));
                    });
                },
                error: function (response) { alert("error"); }
            });

        },
        error: function (response) { alert("error"); }
    });

    $("#CityId").change(function () {
        var Cityid = $(this).val();
        var Countryid = $("#CountryId option:selected").val();
        $.ajax({
            url: " http://localhost:52217/Address/GetStreets",
            type: "GET",
            data: { countryId: Countryid, cityId: Cityid },
            datatype: 'json',
            contentType: 'json',

            success: function (data) {
                var results = JSON.parse(data);
                $("#Street").empty();

                $.each(results, function (index, region) {
                    $("#Street").append($('<option/>', {
                        value: region.Id,
                        text: region.Name
                    }));
                });
            },
            error: function (response) { alert("error"); }
        });

    });

    $("#CountryId").change(function () {

        var cities = $('.city');
        var id = $(this).val();

        $.ajax({
            url: " http://localhost:52217/Address/GetCities?id=" + id,
            type: "GET",

            datatype: 'json',
            contentType: 'json',
            success: function (data) {
                var results = JSON.parse(data);
                $("#CityId").empty();

                $.each(results, function (index, region) {
                    $("#CityId").append($('<option/>', {
                        value: region.Id,
                        text: region.Name
                    }));
                });

                var Cityid = $("#CityId option:selected").val();

                $.ajax({
                    url: " http://localhost:52217/Address/GetStreets",
                    type: "GET",
                    data: { countryId: id, cityId: Cityid },
                    datatype: 'json',
                    contentType: 'json',
                    success: function (data) {
                        var results = JSON.parse(data);
                        $("#Street").empty();
                        $.each(results, function (index, region) {
                            $("#Street").append($('<option/>', {
                                value: region.Id,
                                text: region.Name
                            }));
                        });
                    },
                    error: function (response) { alert("error"); }
                });
            },
            error: function (response) { alert("error"); }
        });
    });
});
