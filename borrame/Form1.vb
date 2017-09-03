Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim Direccion, Url As String
        Direccion = "Belisario+Dominguez,Casa+Blanca,Queretaro"
        Url = "https://www.google.com/maps/embed/v1/place?key=AIzaSyB2ZrSznX1exheGYf5Wq9NNAgs4JuUeqII&q=" & Direccion
        'Direccion = TextBox1.Text & "," & TextBox2.Text
        'vvvvvvvvvvvv https://maps.googleapis.com/maps/api/geocode/outputFormat?parameters
        'retorna un json con datos entre ellos latitud y longitud 
        WebBrowser1.Url = New Uri(Url)
        WebBrowser1.AllowNavigation = True
        WebBrowser1.ScriptErrorsSuppressed = True 'fuerza al browser a no mostrar errores de script(necesario)
        ' WebBrowser1.DocumentText = "<iframe width=100% height=100% src=" + Url + "></iframe>"
        WebBrowser1.DocumentText =
            "<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Strict//EN' ' http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd'>" &
            "<html xmlns='http://www.w3.org/1999/xhtml' xmlns:v='urn:schemas-microsoft-com:vml'>" &
            "<head>" &
            "<meta http-equiv='content-type' content='text/html; charset=UTF-8'/>" &
            "<title>MAPA</title>" &
            "<style> " &
            "body {color: #000; font-family: Arial; font-size: 14px;} label{font-weight: 900;}.div-snippets{display: none;position: fixed;background-color: efefef;width: 900px;height: 500px;top: 50%;left: 50%;margin-top: -250px;margin-left: -450px;outline: 3px solid #afafaf;padding-left: 20px;overflow:auto;} " &
            "</style>" &
            "<script src='https://maps.googleapis.com/maps/api/js?key=AIzaSyB2ZrSznX1exheGYf5Wq9NNAgs4JuUeqII&region=MX' type='text/javascript'></script>" &
            "<script type='text/javascript' charset='utf-8'> var map = null; var marker = null; var geocoder = null; var infowindow = null; var ini_lat = 20.5825527; var ini_lng = -100.4104596; var a_locations_type = new Array('APPROXIMATE', 'GEOMETRIC_CENTER', 'RANGE_INTERPOLATED', 'ROOFTOP');" &
            "a_locations_type [a_locations_type[0]] = ['Aproximado']; a_locations_type[a_locations_type[1]] = ['Centro geom&#233;trico(por ejemplo, una calle)']; a_locations_type[a_locations_type[2]] = ['Aproximaci&#243;n, interpolada entre dos puntos.']; a_locations_type[a_locations_type[3]] = ['C&#243;digo geogr&#225;fico preciso.']; var a_geocode_status = new Array('ERROR', 'INVALID_REQUEST', 'OK', 'OVER_QUERY_LIMIT', 'REQUEST_DENIED', 'UNKNOWN_ERROR', 'ZERO_RESULTS');" &
            "a_geocode_status[a_geocode_status[0]] = ['Error al establecer la comunicaci&#243;n con Google']; a_geocode_status[a_geocode_status[1]] = ['Solicitud GeocoderRequest no es v&#225;lida.']; a_geocode_status[a_geocode_status[2]] = ['Indica que la respuesta contiene un valor GeocoderResponse v&#225;lido']; a_geocode_status[a_geocode_status[3]] = ['La p&#225;gina web ha superado el l&#237;mite de solicitudes en un per&#237;odo de tiempo demasiado breve.']; " &
            "a_geocode_status[a_geocode_status[4]] = ['No se permite que la p&#225;gina web utilice el geocoder.']; a_geocode_status[a_geocode_status[5]] = ['No se pudo procesar una solicitud de codificaci&#243;n geogr. Error del servidor. Puede que se realice correctamente si lo intentas de nuevo.']; a_geocode_status[a_geocode_status[6]] = ['No se ha encontrado ning&#250;n resultado para esta solicitud GeocoderRequest.']; " &
            "function initGMaps() {map = new google.maps.Map(document.getElementById('map_canvas'), {'zoom': 9 , 'center': new google.maps.LatLng(ini_lat, ini_lng), 'mapTypeId': google.maps.MapTypeId.ROADMAP , 'scaleControl': true , 'scrollwheel': false });marker = new google.maps.Marker({map: map, position: new google.maps.LatLng(ini_lat, ini_lng), draggable: true, visible: false}); infowindow = new google.maps.InfoWindow();geocoder = new google.maps.Geocoder();" &
            "google.maps.event.addListener(marker, 'dragend', function() {showLatLongPos();});google.maps.event.addListener(marker, 'click', function() {showLatLongPos();});} function showAddress(address) {if (geocoder) {geocoder.geocode({'address': address, 'region': 'MX'}, function(results, status) {if (status == google.maps.GeocoderStatus.OK) {if (results[0]) {var input = results[0].geometry.location.toUrlValue();var latlngStr = input.split(',', 2);" &
            "var lat_mx = parseFloat(latlngStr[0]);var lng_mx = parseFloat(latlngStr[1]);var latLong_mx = new google.maps.LatLng(lat_mx, lng_mx); map.setZoom(16);map.setCenter(latLong_mx);marker.setPosition(latLong_mx);marker.setVisible(true); var location_type_mx = results[0].geometry.location_type infowindow.setContent('<b>' + results[0].formatted_address + '</b>' + '<br/><br/><i style='color: #777;'>' + a_locations_type[location_type_mx] + '</i>');" &
            "infowindow.open(map, marker);} else {alert(a_geocode_status[status]);}} else {alert(a_geocode_status[status]); }});}} function showLatLongPos() {var location = marker.getPosition().toUrlValue(7);var latlngStr = location.split(',', 2);var lat_mx = parseFloat(latlngStr[0]);var lng_mx = parseFloat(latlngStr[1]);var latLong_mx = new google.maps.LatLng(lat_mx, lng_mx); geocoder.geocode({'latLng': latLong_mx, 'region': 'MX'} , function(results) " &
            "{var location_type_mx = results[0].geometry.location_type infowindow.setContent('<b>' + results[0].formatted_address + '</b>' + '<br/><br/><i style='color: #777;'>' + a_locations_type[location_type_mx] + '</i>'); infowindow.open(map, marker);}); document.getElementById('latitud').value = lat_mx; document.getElementById('longitud').value = lng_mx;} google.maps.event.addDomListener(window, 'load', initGMaps);" &
            "</script>" &
            "<script type='text/javascript' src='js/jquery-1.5.2.min.js'></script>" &
        "</head>" &
        "<body>" &
        "<form id='form1' action='#' onsubmit='showAddress(this.address.value);return false'>" &
        "<p>" &
        "<input type='text' id='address' name='address' value='Corregidora sur, Queretaro' style='width: 800px;' />" &
        "&nbsp;&nbsp;&nbsp;" &
        "<input type='submit' value='&#161;Buscar!' />" &
        "</p>" &
        "<p>Lat.:<input type='text' style='width:180px' id='latitud' name='latitud' value=''/>" &
        "&nbsp;&nbsp;&nbsp;&nbsp;" &
        "Long.:<input type='text' style='width:180px' id='longitud' name='longitud' value='' />" &
        "</p>" &
        "<div id='map_canvas' style='width: 900px; height: 500px;'></div>" &
        "</form>" &
        "<script type='text/javascript' charset='utf-8'> jQuery(document).ready(function() {jQuery('#address').focus();});" &
        "</script>" &
        "</body>" &
        "</html>"

    End Sub
    Private Sub InvokeTestMethod(ByVal Name As String, ByVal Address As String)
        If (Not (WebBrowser1.Document Is Nothing)) Then
            Dim ObjArr(2) As Object
            ObjArr(0) = CObj(New String(Name))
            ObjArr(1) = CObj(New String(Address))
            WebBrowser1.Document.InvokeScript("test", ObjArr)
        End If
    End Sub
    Private Sub goo()
        'Creamos variable para almacenar la url

        Dim urlMaps, Direccion, VarColonia As String

        VarColonia = 2
        'ok Direccion = TbxCalle.Text & "," & TxtboxLocalida.Text & "," & TboxEntidad.Text & "&output=embed"
        'Direccion = "https://www.google.com/maps/embed/v1/place?key=AIzaSyB2ZrSznX1exheGYf5Wq9NNAgs4JuUeqII&q=belisario+dominguez,casa+blanca,queretaro"

        MsgBox(Direccion)
        'Concatenamos http con direcciones de textbox
        'ok urlMaps = "https://www.google.com/maps/embed/v1/place?key=AIzaSyB2ZrSznX1exheGYf5Wq9NNAgs4JuUeqII&q=" & Direccion
        'urlMaps = Direccion
        'Creamos una variable direccion para que el WebBrowser lo pueda abrir puesto que no puede abrir directamente un string
        'mmm no lo use.
        'ok Dim Url_direccion As New Uri(urlMaps)
        'ASignamos como URL la direccion

        WebBrowser1.AllowNavigation = True ' navegar
        WebBrowser1.ScriptErrorsSuppressed = True 'fuerza al browser a no mostrar errores de script(necesario)
        WebBrowser1.Navigate("http://maps.google.es/maps?q=") 'crea el espacio de navegacion
        REM HtmlDocument doc = this.webBrowser1.Document
        'encontre este ejemplo poner la direccion en .documenttext
        WebBrowser1.DocumentText = "<iframe width=100% height=100% src=" + urlMaps + "></iframe>"
        'WebBrowser1.Url = Url_direccion
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class


