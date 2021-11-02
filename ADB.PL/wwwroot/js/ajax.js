////$(function () {
////    $("#CountryList").change(function () {
////        var CountryId = $("#CountryList option:selected").val();
////        $("#CityList").empty();
////        $("#CityList").append("<option>Choose City</option>");
////        $.ajax({
////            type: "POST",
////            url: "/Employee/GetCityDataByCountryId",
////            data: { countryId: CountryId },
////            success: function (result) {
////                $.each(result, function (i, e) {
////                    $("#CityList").append("<option value='" + e.Id + "'>" + e.Name + "</option>");
////                });
////            }
////        });
////    });

////    $("#CityList").change(function () {
////        var CityId = $("#CityList option:selected").val();
////        $("#DistrictId").empty();
////        $("#DistrictId").append("<option>Choose District</option>");
////        $.ajax({
////            type: "POST",
////            url: "/Employee/GetDistrictDataByCityId",
////            data: { cityId: CityId },
////            success: function (result) {
////                $.each(result, function (i, e) {
////                    $("#DistrictId").append("<option value='" + e.Id + "'>" + e.Name + "</option>");
////                });
////            }
////        });
////    });
////});