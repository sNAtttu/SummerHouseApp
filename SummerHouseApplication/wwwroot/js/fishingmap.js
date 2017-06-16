// Function for placing marker on map.
function PlaceMarkerOnMap(map, lat, lng, imagePath, contentString) {

    var iconImage = {
        url: imagePath,
        scaledSize: new google.maps.Size(32, 32),
        origin: new google.maps.Point(0, 0),
        anchor: new google.maps.Point(16, 16),
    };

    var myLatLng =
        {
            lat: parseFloat(lat),
            lng: parseFloat(lng)
        };

    var marker = new google.maps.Marker({
        position: myLatLng,
        map: map,
        icon: iconImage
    });

    if (contentString) {
        var infowindow = new google.maps.InfoWindow({
            content: contentString
        });

        marker.addListener('click', function () {
            infowindow.open(gmap, marker);
        });
    }
}
// function for initializing google map
function initMap(centerLocation) {
    var mapProp = {
        zoom: 15,
        center: centerLocation
    }
    var map = new google.maps.Map(document.getElementById("googleMap"), mapProp);
    // we need to make a global variable of map. This could be do otherwise but map
    // should be accessible in click functions also.
    gmap = map;

    map.addListener('click', function (event) {
        //placeMarker(event.latLng);
        latestCoords = event.latLng;
        if (!isDrawing) {
            $('#action-select-modal').modal('open');
        }
        else {
            placeMarker(event.latLng);
        }
    });

    return map;
}
// Initialize click functions for fishing map
function initializeButtons() {
    $('.modal').modal();

    $('#fish-select').material_select();
    $('#rod-select').material_select();

    $("#fish-select-container").click(function (event) {
        event.stopPropagation();
        // Do something
    });
    $("#rod-select-container").click(function (event) {
        event.stopPropagation();
        // Do something
    });
    $('#fish-marker').click(function () {
        activeFunctionality = 1;
        $('.modal').modal('close');
        placeMarker(latestCoords);
    });

    $('#net-marker').click(function () {

        activeFunctionality = 2;
        // Enable extra functionality when we are making net marking.
        $('#restore-marker').addClass("button-visible");
        $('#done-drawing').addClass("button-visible");

        $('.modal').modal('close');

        isDrawing = true;
        placeMarker(latestCoords);
    });

    $('#cottage-marker').click(function () {
        activeFunctionality = 3;
        $('.modal').modal('close');
        placeMarker(latestCoords);
    })
    // This button is for removing latest drawn polyline. If user is making fishing net and accidentally
    // clicks wrong place on map, this button removes that line and draws latest again.
    $('#restore-marker').click(function () {
        if (markerCoords.length > 0) {
            markerCoords.pop();
            markers.pop().setMap(null);
            latestPolyline = drawPolylinesOnMap(markerCoords, gmap);
        }
    });
    $('#done-drawing').click(function () {
        isDrawing = false;
        resetActiveButton();
        activeFunctionality = 0;
        placeMarker(latestCoords);
    });

}
    // Deactivate all buttons and hide extra functionality button.
    function resetActiveButton() {

        $('#done-drawing').removeClass("button-visible");
        $('#done-drawing').addClass("button-hidden");
        $('#restore-marker').removeClass("button-visible");
        $('#restore-marker').addClass("button-hidden");

    }
    // Function for drawing polyline on map.
    function drawPolylinesOnMap(coordinatesArray, map) {

        var fishingNet = new google.maps.Polyline({
            path: coordinatesArray,
            strokeColor: "#0000FF",
            strokeOpacity: 0.8,
            strokeWeight: 2
        });
        // remove latest polyline so we are not stacking multiple polylines on top of each other.
        if (typeof (latestPolyline) !== "undefined") {
            latestPolyline.setMap(null);
        }

        fishingNet.setMap(map);
        return fishingNet;
    }


    function setLatestClickCoordinatesOnMap(coords) {
        latestCoords = coords;
    }
