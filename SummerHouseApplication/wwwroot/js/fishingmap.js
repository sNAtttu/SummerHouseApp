// Array of coordinates for polyline. This is reseted after polyline is "done".
var markerCoords = [];
// Array of markers for polyline. This is reseted after polyline is "done".
var markers = [];
// Latest polyline which is drawn.
var latestPolyline;
// Generated map.
var gmap;
// Geocoder so we can get lat and lng from address.
var geocoder;
// 0 is that we don't mark anything to map
// 1 is when we want to place just a marker
// 2 is when we want to draw polyline
var activeFunctionality = 0;
// Variable to check the time for latest tap.
// Used to make double tap effect.
var mylatesttap;
// Latest click on map saves coordinates to this global variable.
// we also need global variable to check if user is still drawing
// or not
var latestCoords;
var isDrawing = false;

// this is initialized in fishing map view.
var summerhouseId = -1;
// Function for calling api. Api returns an array of markers
// related to this particular summerhouse.
function loadMarkersOnMap(summerhouseId) {
    $.ajax({
        method: "GET",
        contentType: "application/json",
        url: "/api/mapmarker/" + summerhouseId,
    })
        .done(function (markers) {
            _.each(markers, function (marker) {

                var imagePath = "../images/fishmarker.png";

                PlaceMarkerOnMap(gmap,
                    marker.coordinates.latitude,
                    marker.coordinates.longitude,
                    imagePath,
                    marker.info.contentString);
            });
        });
    $.ajax({
        method: "GET",
        contentType: "application/json",
        url: "/api/fishingnet/" + summerhouseId
    }).done(function (nets) {

        var imagePath = "../images/fishing-net.png";

        _.each(nets, function (net) {
            var coordsPath = [];
            _.each(net.markers, function (marker) {

                coordsPath.push({
                    lat: parseFloat(marker.coordinates.latitude),
                    lng: parseFloat(marker.coordinates.longitude)
                });

                PlaceMarkerOnMap(gmap,
                    marker.coordinates.latitude,
                    marker.coordinates.longitude,
                    imagePath,
                    marker.info.contentString);

            });
            drawPolylinesOnMap(coordsPath, gmap);
        });
    });
}

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

        marker.addListener('click', function (point) {
            var self = this;
            var now = new Date().getTime();
            var timesince = now - mylatesttap;
            if ((timesince < 600) && (timesince > 0)) {
                // double tap   
                console.log(point.latLng.lat());
                console.log(point.latLng.lng());
                var coords = {
                    Latitude: point.latLng.lat(),
                    Longitude: point.latLng.lng()
                };
                $.ajax({
                    method: "DELETE",
                    contentType: "application/json",
                    data: JSON.stringify(coords),
                    url: "/map/marker/delete/" + summerhouseId
                }).
                    done(function (result) {
                        console.log(result);
                        self.setMap(null);
                    });
            } else {
                // too much time to be a doubletap
            }
            mylatesttap = new Date().getTime();
            infowindow.open(gmap, marker);
        });
    }
}
// function for initializing google map
function initMap(centerLocation) {
    var mapProp = {
        zoom: 17,
        center: centerLocation,
        styles: [
            {
                "featureType": "water",
                "elementType": "geometry",
                "stylers": [
                    {
                        "visibility": "on"
                    },
                    {
                        "color": "#aee2e0"
                    }
                ]
            },
            {
                "featureType": "landscape",
                "elementType": "geometry.fill",
                "stylers": [
                    {
                        "color": "#abce83"
                    }
                ]
            },
            {
                "featureType": "poi",
                "elementType": "geometry.fill",
                "stylers": [
                    {
                        "color": "#769E72"
                    }
                ]
            },
            {
                "featureType": "poi",
                "elementType": "labels.text.fill",
                "stylers": [
                    {
                        "color": "#7B8758"
                    }
                ]
            },
            {
                "featureType": "poi",
                "elementType": "labels.text.stroke",
                "stylers": [
                    {
                        "color": "#EBF4A4"
                    }
                ]
            },
            {
                "featureType": "poi.park",
                "elementType": "geometry",
                "stylers": [
                    {
                        "visibility": "simplified"
                    },
                    {
                        "color": "#8dab68"
                    }
                ]
            },
            {
                "featureType": "road",
                "elementType": "geometry.fill",
                "stylers": [
                    {
                        "visibility": "simplified"
                    }
                ]
            },
            {
                "featureType": "road",
                "elementType": "labels.text.fill",
                "stylers": [
                    {
                        "color": "#5B5B3F"
                    }
                ]
            },
            {
                "featureType": "road",
                "elementType": "labels.text.stroke",
                "stylers": [
                    {
                        "color": "#ABCE83"
                    }
                ]
            },
            {
                "featureType": "road",
                "elementType": "labels.icon",
                "stylers": [
                    {
                        "visibility": "off"
                    }
                ]
            },
            {
                "featureType": "road.local",
                "elementType": "geometry",
                "stylers": [
                    {
                        "color": "#A4C67D"
                    }
                ]
            },
            {
                "featureType": "road.arterial",
                "elementType": "geometry",
                "stylers": [
                    {
                        "color": "#9BBF72"
                    }
                ]
            },
            {
                "featureType": "road.highway",
                "elementType": "geometry",
                "stylers": [
                    {
                        "color": "#EBF4A4"
                    }
                ]
            },
            {
                "featureType": "transit",
                "stylers": [
                    {
                        "visibility": "off"
                    }
                ]
            },
            {
                "featureType": "administrative",
                "elementType": "geometry.stroke",
                "stylers": [
                    {
                        "visibility": "on"
                    },
                    {
                        "color": "#87ae79"
                    }
                ]
            },
            {
                "featureType": "administrative",
                "elementType": "geometry.fill",
                "stylers": [
                    {
                        "color": "#7f2200"
                    },
                    {
                        "visibility": "off"
                    }
                ]
            },
            {
                "featureType": "administrative",
                "elementType": "labels.text.stroke",
                "stylers": [
                    {
                        "color": "#ffffff"
                    },
                    {
                        "visibility": "on"
                    },
                    {
                        "weight": 4.1
                    }
                ]
            },
            {
                "featureType": "administrative",
                "elementType": "labels.text.fill",
                "stylers": [
                    {
                        "color": "#495421"
                    }
                ]
            },
            {
                "featureType": "administrative.neighborhood",
                "elementType": "labels",
                "stylers": [
                    {
                        "visibility": "off"
                    }
                ]
            }
        ]
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
            placeMarker(event.latLng, summerhouseId);
        }
    });

    return map;
}
// Initialize click functions for fishing map
function initializeButtons(summerhouseId) {

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
        placeMarker(latestCoords, summerhouseId);
    });

    $('#net-marker').click(function () {

        activeFunctionality = 2;
        // Enable extra functionality when we are making net marking.
        $('#restore-marker').addClass("button-visible");
        $('#done-drawing').addClass("button-visible");

        $('.modal').modal('close');

        isDrawing = true;
        placeMarker(latestCoords, summerhouseId);
    });

    $('#cottage-marker').click(function () {
        activeFunctionality = 3;
        $('.modal').modal('close');
        placeMarker(latestCoords, summerhouseId);
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
        postFishingNet(summerhouseId, markers);
        activeFunctionality = 0;
        placeMarker(latestCoords, summerhouseId);
    });

}

function postFishingNet(summerhouseId, netMarkers) {

    if (summerhouseId < 0) {
        alert("Summerhouse has an invalid Id");
    }

    var fishingNetMarkersDto = [];
    _.each(netMarkers, function (marker) {
        var markerDto = {
            Title: marker.title,
            Info: {
                ContentString: "Täältä on tullut kalaa verkoilla"
            },
            Coordinates: {
                Latitude: marker.position.lat(),
                Longitude: marker.position.lng()
            },
            MarkerType: 0, // 0 is net marker,
            FishType: 7 // 7 is multiple different
        };
        fishingNetMarkersDto.push(markerDto);
    });
    $.ajax({
        method: "POST",
        contentType: "application/json",
        url: "/map/fishingnet/" + summerhouseId,
        data: JSON.stringify(fishingNetMarkersDto)
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
function placeMarker(location, summerhouseId) {

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
            contentString = "Verkoilla";
        }
        else if (activeFunctionality === 3) {
            iconImage = {
                url: '../images/Icon_Cabin.png',
                scaledSize: new google.maps.Size(24, 24),
                origin: new google.maps.Point(0, 0),
                anchor: new google.maps.Point(12, 12),
            };
            markerTitle = "Mökki";
            contentString = "Mökin sijainti, kartan keskipiste.";
        }
        var infowindow = new google.maps.InfoWindow({
            content: contentString
        });

        var marker = new google.maps.Marker({
            position: location,
            map: gmap,
            icon: iconImage,
            title: markerTitle
        });

        marker.addListener('click', function () {
            infowindow.open(gmap, marker);
        });

        // 2 means polyline
        if (activeFunctionality === 1) {

            var rodType = $('#rod-select').val();
            var fishType = $('#fish-select').val();

            var markerDto = {
                Title: markerTitle,
                Info: {
                    ContentString: contentString
                },
                Coordinates: {
                    Latitude: location.lat(),
                    Longitude: location.lng()
                },
                MarkerType: rodType,
                FishType: fishType,
            };

            $.ajax({
                method: "POST",
                contentType: "application/json",
                url: "/map/marker/" + summerhouseId,
                data: JSON.stringify(markerDto)
            });
        }
        if (activeFunctionality === 2) {

            markers.push(marker);
            markerCoords.push({
                lat: marker.position.lat(),
                lng: marker.position.lng()
            });

            for (var i = 0; i < markerCoords.length; i++) {

                if (markerCoords.length > 1) {
                    latestPolyline = drawPolylinesOnMap(markerCoords, gmap);
                }
            }
        }
        // 3 means that user saves cottage location so this is loaded when
        // map is loaded.
        else if (activeFunctionality === 3) {
            var locationObj =
                {
                    Latitude: location.lat(),
                    Longitude: location.lng()
                };
            $.ajax({
                method: "POST",
                contentType: "application/json",
                url: "/map/location/" + summerhouseId,
                data: JSON.stringify(locationObj)
            });
        }
    }
}
