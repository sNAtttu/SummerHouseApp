// Array of coordinates for polyline. This is reseted after polyline is "done".
var markerCoords = [];
// Array of markers for polyline. This is reseted after polyline is "done".
var markers = [];
// Latest polyline which is drawn.
var latestPolyline;
// Generated map.
var gmap;

// 0 is that we don't mark anything to map
// 1 is when we want to place just a marker
// 2 is when we want to draw polyline
var activeFunctionality = 0;

// Click functions for each button.
$(document).ready(function () {

    $('.modal').modal();

    $('#nothing-selected').click(function () {
        activeFunctionality = 0;
        resetActiveButton();
        $(this).addClass("button-active");
    });

    $('#fish-marker').click(function () {
        activeFunctionality = 1;
        resetActiveButton();
        $(this).addClass("button-active");
    });

    $('#net-marker').click(function () {
        activeFunctionality = 2;
        resetActiveButton();
        $(this).addClass("button-active");
        // Enable extra functionality when we are making net marking.
        $('#restore-marker').addClass("button-visible");
    });
    // This button is for removing latest drawn polyline. If user is making fishing net and accidentally
    // clicks wrong place on map, this button removes that line and draws latest again.
    $('#restore-marker').click(function () {
        if (markerCoords.length > 0) {
            markerCoords.pop();
            markers.pop().setMap(null);
            latestPolyline = drawPolylinesOnMap(markerCoords, gmap);
        }
    });
});
// Deactivate all buttons and hide extra functionality button.
function resetActiveButton() {

    $('#net-marker').removeClass("button-active");
    $('#fish-marker').removeClass("button-active");
    $('#nothing-selected').removeClass("button-active");

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
// Initialize map
function myMap() {
    
    var mapProp = {
        center: new google.maps.LatLng(61.867973, 28.886384),
        zoom: 13,
    };

    var map = new google.maps.Map(document.getElementById("googleMap"), mapProp);
    // we need to make a global variable of map. This could be do otherwise but map
    // should be accessible in click functions also.
    gmap = map;

    map.addListener('click', function (event) {
        placeMarker(event.latLng);
    });

    function placeMarker(location) {

        // If we are doing something else than fishing net.
        // Reset fishing net coords and markers, so next time we make a net
        // last one is not modified.

        if (activeFunctionality !== 2) {
            markerCoords = [];
            markers = [];
            if (latestPolyline) {
                latestPolyline = new google.maps.Polyline({
                    path: [],
                    strokeColor: "#0000FF",
                    strokeOpacity: 0.8,
                    strokeWeight: 2
                });
            }
        }
        // If user is doing something else than just scrolling on map.
        // If user is just marking single spots, just add marker. Otherwise add markers and coordinates
        // to array so polyline can be drawn.
        if (activeFunctionality !== 0) {

            var iconImage;
            var markerTitle = "Kalapaikka";
            var contentString = "Placeholder string for infowindow content.";

            if (activeFunctionality === 1) {
                iconImage = {
                    url: '../images/fishmarker.png',
                    scaledSize: new google.maps.Size(32, 32),
                    origin: new google.maps.Point(0, 0),
                    anchor: new google.maps.Point(16, 16),
                };
                contentString = "Täältä on tullut kalaa virvelillä";
            }
            else if (activeFunctionality === 2) {
                iconImage = {
                    url: '../images/fishing-net.png',
                    scaledSize: new google.maps.Size(24, 24),
                    origin: new google.maps.Point(0, 0),
                    anchor: new google.maps.Point(12, 12),
                };
                markerTitle = "Verkko";
                contentString = "Verkoilla saa täältä";
            }

            var infowindow = new google.maps.InfoWindow({
                content: contentString
            });

            var marker = new google.maps.Marker({
                position: location,
                map: map,
                icon: iconImage,
                title: markerTitle
            });

            marker.addListener('click', function () {
                infowindow.open(map, marker);
            });

            // 2 means polyline
            if (activeFunctionality === 2) {

                markers.push(marker);
                markerCoords.push({
                    lat: marker.position.lat(),
                    lng: marker.position.lng()
                });

                for (var i = 0; i < markerCoords.length; i++) {

                    if (markerCoords.length > 1) {
                        latestPolyline = drawPolylinesOnMap(markerCoords, map);
                    }
                }
            }
        }
    }
}
