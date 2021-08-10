google.maps.event.addDomListener(window, "load", function () {
    const ubicacion = new Localization(() => {
        const myLatLgn = { lat: ubicacion.latitude, lng: ubicacion.longitude };
        const options = {
            center: myLatLgn,
            zoom: 14
        }
        var map = document.getElementById('map');
        const mapa = new google.maps.Map(map, options);
        const marcador = new google.maps.Marker({
            position: myLatLgn,
            map: mapa
        });
        var informacion = new google.maps.InfoWindow();
        marcador.addListener('click', function () {
            informacion.open(mapa, marcador);
        });
        var autocomplete = document.getElementById('autocomplete');
        const search = new google.maps.places.Autocomplete(autocomplete);
        search.bindTo("bounds", mapa);
        search.addListener('place_changed', function () {
            informacion.close();
            marcador.setVisible(false);
            var place = search.getPlace();
            console.log(place);
           /* if (!place.geometry.viewport) {
                window.alert('Error al mostrar el lugar');
                return;
            }
            if (place.geometry.viewport) {
                mapa.fitBounds(place.geometry.viewport);
            } else {
                map.setCenter(place.geometry.location);
                mapa.setZoom(18);
            }
            marcador.setPosition(place.geometry.location);
            marcador.setVisible(true);
            var addres = "";
            if (place.address_components) {
                addres = [
                    (place.address_components[0] && place.address_components[0].short_name
                        || ''),
                    (place.address_components[1] && place.address_components[1].short_name
                        || ''),
                    (place.address_components[2] && place.address_components[2].short_name
                        || ''),
                ];
            }
            informacion.setContent('<div><strong>' + place.name + '</strong><br>' + addres);
            informacion.open(map, marcador);*/
        });
    });
});