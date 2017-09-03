<?php
/* * *******************************************************************************
 * @AUTOR:           ULTIMINIO RAMOS GALAN.
 * SISTEMA:          php-aplicado.blogspot.com
 * FECHA:            2012-03-24.
 * DESCRIPCION:      Obtener codificaci&#243;n inversa con Google Maps API V3
 * ****************************************************************************** */
?>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<!--
 Copyright 2008 Google Inc. 
 Licensed under the Apache License, Version 2.0: 
 http://www.apache.org/licenses/LICENSE-2.0 
-->
<html xmlns="http://www.w3.org/1999/xhtml" xmlns:v="urn:schemas-microsoft-com:vml">
    <head>
        <meta http-equiv="content-type" content="text/html; charset=UTF-8"/>
        <title>Google Maps API Example: Simple Geocoding</title>

        <style >
            body{
                color: #000;
                font-family: Arial;
                font-size: 14px;
            }

            label{
                font-weight: 900;
            }

            .div-snippets{
                display: none;
                position: fixed;
                background-color: #efefef;
                width: 900px;
                height: 500px;
                top: 50%;
                left: 50%;
                margin-top: -250px;
                margin-left: -450px;
                outline: 3px solid #afafaf;
                padding-left: 20px;
                overflow: auto;
            }
        </style>

        <script src="http://maps.google.com/maps/api/js?sensor=false" type="text/javascript"></script>
        <script type="text/javascript" charset="utf-8">
            var map = null;
            var marker = null;
            var geocoder = null;
            var infowindow = null;
            // posicion predeterminada
            var ini_lat = 19.360927;
            var ini_lng = -99.183325;

            // traducciones del tipo de localizaci&#243;n
            var a_locations_type = new Array('APPROXIMATE', 'GEOMETRIC_CENTER', 'RANGE_INTERPOLATED', 'ROOFTOP');
            a_locations_type[a_locations_type[0]] = ['El resultado devuelto es aproximado.'];
            a_locations_type[a_locations_type[1]] = ['El resultado devuelto es el centro geom&#233;trico de un resultado como una l&#237;nea (por ejemplo, una calle) o un pol&#237;gono (una regi&#243;n).'];
            a_locations_type[a_locations_type[2]] = ['El resultado devuelto refleja una aproximaci&#243;n (normalmente en una carretera) interpolada entre dos puntos precisos (por ejemplo, intersecciones). Normalmente, los resultados interpolados se devuelven cuando los c&#243;digos geogr&#225;ficos de la parte superior no est&#225;n disponibles para una direcci&#243;n postal.'];
            a_locations_type[a_locations_type[3]] = ['El resultado devuelto refleja un c&#243;digo geogr&#225;fico preciso.'];

            // traducciones del estatus de la geocodificaci&#243;n
            var a_geocode_status = new Array('ERROR', 'INVALID_REQUEST', 'OK', 'OVER_QUERY_LIMIT', 'REQUEST_DENIED', 'UNKNOWN_ERROR', 'ZERO_RESULTS');
            a_geocode_status[a_geocode_status[0]] = ['Se ha producido un error al establecer la comunicaci&#243;n con los servidores de Google.'];
            a_geocode_status[a_geocode_status[1]] = ['La solicitud GeocoderRequest no es v&#225;lida.'];
            a_geocode_status[a_geocode_status[2]] = ['Indica que la respuesta contiene un valor GeocoderResponse v&#225;lido.'];
            a_geocode_status[a_geocode_status[3]] = ['La p&#225;gina web ha superado el l&#237;mite de solicitudes en un per&#237;odo de tiempo demasiado breve.'];
            a_geocode_status[a_geocode_status[4]] = ['No se permite que la p&#225;gina web utilice el geocoder.'];
            a_geocode_status[a_geocode_status[5]] = ['No se pudo procesar una solicitud de codificaci&#243;n geogr&#225;fica debido a un error del servidor. Puede que la solicitud se realice correctamente si lo intentas de nuevo.'];
            a_geocode_status[a_geocode_status[6]] = ['No se ha encontrado ning&#250;n resultado para esta solicitud GeocoderRequest.'];

            // funciones para nuestro mapa
            function initGMaps() {
                // crear los objetos necesarios, primero el mapa
                map = new google.maps.Map(document.getElementById("map_canvas"), {
                    'zoom': 4
                            , 'center': new google.maps.LatLng(ini_lat, ini_lng)
                            , 'mapTypeId': google.maps.MapTypeId.ROADMAP
                            , 'scaleControl': true
                            , 'scrollwheel': false
                });
                // el marcador (pin)
                marker = new google.maps.Marker({
                    map: map
                            , position: new google.maps.LatLng(ini_lat, ini_lng)
                            , draggable: true
                            , visible: false
                });
                // la ventana de info (globo)
                infowindow = new google.maps.InfoWindow();
                // el geocodificador
                geocoder = new google.maps.Geocoder();
                // crear los eventos para acciones del mouse sobre el marcador (pin)
                google.maps.event.addListener(marker, "dragend", function() {
                    showLatLongPos();
                });
                google.maps.event.addListener(marker, "click", function() {
                    showLatLongPos();
                });
            }

            function showAddress(address) {
                if (geocoder) {
                    // obtener la Geo-Codificaci&#243;n Forward,
                    // introduciendo un dato string (address)
                    geocoder.geocode({'address': address, 'region': 'MX'}
                    , function(results, status) {
                        if (status == google.maps.GeocoderStatus.OK) {
                            if (results[0]) {
                                // preparar la info de la posici&#243;n latitud y longitud
                                var input = results[0].geometry.location.toUrlValue();
                                var latlngStr = input.split(",", 2);
                                var lat_mx = parseFloat(latlngStr[0]);
                                var lng_mx = parseFloat(latlngStr[1]);
                                var latLong_mx = new google.maps.LatLng(lat_mx, lng_mx);
                                // centrar el mapa en la posici&#243;n encontrada
                                map.setZoom(16);
                                map.setCenter(latLong_mx);
                                marker.setPosition(latLong_mx);
                                marker.setVisible(true);
                                //
                                google.maps.event.trigger(marker, 'click');
                                // llenar con la info de la codificaci&#243;n inversa, o sea, la direcci&#243;n humanamente legible
                                var location_type_mx = results[0].geometry.location_type
                                infowindow.setContent('<b>' + results[0].formatted_address + '</b>' + '<br/><br/><i style="color: #777;">' + a_locations_type[location_type_mx] + '</i>');
                                infowindow.open(map, marker);
                            } else {
                                alert(a_geocode_status[status]);
                            }
                        } else {
                            alert(a_geocode_status[status]);
                        }
                    });
                } // endif
            }

            function showLatLongPos() {
                // preparar la info de la posici&#243;n latitud y longitud
                var location = marker.getPosition().toUrlValue(7);
                var latlngStr = location.split(",", 2);
                var lat_mx = parseFloat(latlngStr[0]);
                var lng_mx = parseFloat(latlngStr[1]);
                var latLong_mx = new google.maps.LatLng(lat_mx, lng_mx);

                // obtener la Geo-Codificaci&#243;n Inversa, o sea, la direcci&#243;n humanamente legible
                // introduciendo un dato latLong
                geocoder.geocode({'latLng': latLong_mx, 'region': 'MX'}
                , function(results) {
                    var location_type_mx = results[0].geometry.location_type
                    infowindow.setContent('<b>' + results[0].formatted_address + '</b>' + '<br/><br/><i style="color: #777;">' + a_locations_type[location_type_mx] + '</i>');
                    infowindow.open(map, marker);
                });
                // llenar los campos de texto con los valores latitud y longitud respectivamente
                document.getElementById("latitud").value = lat_mx;
                document.getElementById("longitud").value = lng_mx;
            }

            // cargar el mapa autom&#225;ticamente cuando se carga la p&#225;gina
            // es el equivalente a poner body onload="initGMaps();">
            google.maps.event.addDomListener(window, 'load', initGMaps);
        </script>
        <script type="text/javascript" src="js/jquery-1.5.2.min.js"></script>
    </head>

    <body>
        <form id="form1" action="#" onsubmit="showAddress(this.address.value);
                return false">
            <p>
                <label for="address">Ingresa una direccipon en este formato:(calle y n&#250;mero, colonia, delegaci&#243;n o municipio, entidad)</label>
                <br/>
                Ejemplo:&nbsp;&nbsp;&nbsp;&nbsp;<i><b style="color: blue;">insurgentes sur 1677, gualupe inn, &#193;varo obreg&#243;n, distrito federal</b></i>
                <br/>
                <input type="text" id="address" name="address" value="insurgentes sur 1677, gualupe inn, &#193;lvaro obreg&#243;n, distrito federal" style="width: 800px;" />
                &nbsp;&nbsp;&nbsp;
                <input type="submit" value="&#161;Buscar!" />
            </p>
            <!-- Coordenadas y datos a recibir para convertirlos en c&#243;digo -->
            <p>
                La latitud/longitud aparecer&#225;n en los cuadros de texto despu&#233;s que <b style="color: blue;">muevas</b> el marcador dentro del mapa.
            </p>
            <p>
                Lat.:<input type="text" style="width:180px" id="latitud" name="latitud" value="" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                Long.:<input type="text" style="width:180px" id="longitud" name="longitud" value="" />
            </p>
            <div id="map_canvas" style="width: 900px; height: 500px;"></div>
        </form>

        <script type="text/javascript" charset="utf-8">
            jQuery(document).ready(function() {
                jQuery('#address').focus();
            });
        </script>
    </body>
</html>

<?php
/* EOF */
