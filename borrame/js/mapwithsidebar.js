//<![CDATA[
var cm_map;
var cm_openInfowindow;
var cm_mapMarkers = [];
var cm_mapHTMLS = [];

/**
 * Loads map and calls function to load in worksheet data.
 */
function cm_load() {  
   var myLatlng = new google.maps.LatLng(globalLat, globalLon);
   var myOptions = {
      zoom: 16
      , center: myLatlng
      , mapTypeId: google.maps.MapTypeId.ROADMAP
      , mapTypeControl: false
      , scrollwheel: false
      , zoomControlOptions: google.maps.ZoomControlStyle.SMALL
   }
   cm_map = new google.maps.Map(document.getElementById("cm_map"), myOptions);

   cm_loadMapJSON();
}

/**
 * Function called when marker on the map is clicked.
 * Opens an info window (bubble) above the marker.
 * @param {Number} markerNum Number of marker in global array
 */
function cm_markerClicked(markerNum) {
   var infowindowOptions = {
      content: cm_mapHTMLS[markerNum]
   }
   
//   var result = "";
//   for (var i in cm_mapMarkers[markerNum]) {
//      result += "cm_mapMarkers." + i + " = " + cm_mapMarkers[markerNum][i] + "\n";
//   }
//   alert(result);
   
   var infowindow = new google.maps.InfoWindow(infowindowOptions);
   infowindow.open(cm_map, cm_mapMarkers[markerNum]);
   cm_setInfowindow(infowindow);
   cm_map.setCenter(cm_mapMarkers[markerNum].getPosition());
   cm_map.setZoom(16);
}


/** 
 * Called when JSON is loaded. Creates sidebar if param_sideBar is true.
 * Sorts rows if param_rankColumn is valid column. Iterates through worksheet rows, 
 * creating marker and sidebar entries for each row.
 * @param {JSON} json Worksheet feed
 */       


function cm_setInfowindow(newInfowindow) {
   if (cm_openInfowindow != undefined) {
      cm_openInfowindow.close();
   }

   cm_openInfowindow = newInfowindow;
}

/**
 * Creates marker with ranked Icon or blank icon,
 * depending if rank is defined. Assigns onclick function.
 * @param {GLatLng} point Point to create marker at
 * @param {String} title Tooltip title to display for marker
 * @param {String} html HTML to display in InfoWindow
 * @param {Number} rank Number rank of marker, used in creating icon
 * @return {GMarker} Marker created
 */
function cm_createMarker(map, latlng, title, html) {
   /*
   var iconSize = new google.maps.Size(32, 37);
   var iconShadowSize = new google.maps.Size(37, 34);
   var iconHotSpotOffset = new google.maps.Point(9, 0); // Should this be (9, 34)?
   var iconPosition = new google.maps.Point(0, 0);
   var infoWindowAnchor = new google.maps.Point(9, 2);
   var infoShadowAnchor = new google.maps.Point(18, 25);

   var iconShadowUrl = "http://www.google.com/mapfiles/shadow50.png";
   var iconImageUrl = 'http://intranet.oficinas-publicas.com/imgs/gmaps/markers/camping-2.png';
   var iconImageOverUrl = iconImageUrl;
   var iconImageOutUrl = iconImageUrl;

   var markerShadow =
   new google.maps.MarkerImage(iconShadowUrl, iconShadowSize,
      iconPosition, iconHotSpotOffset);
   
   var markerImage =
   new google.maps.MarkerImage(iconImageUrl, iconSize,
      iconPosition, iconHotSpotOffset);

   var markerImageOver =
   new google.maps.MarkerImage(iconImageOverUrl, iconSize,
      iconPosition, iconHotSpotOffset);

   var markerImageOut =
   new google.maps.MarkerImage(iconImageOutUrl, iconSize,
      iconPosition, iconHotSpotOffset);
   */
   var markerOptions = {
      title: title,
      position: latlng,
      map: map
   }

   var marker = new google.maps.Marker(markerOptions);

   google.maps.event.addListener(marker, "click", function() {
      var infowindowOptions = {
         content: html
      }
      var infowindow = new google.maps.InfoWindow(infowindowOptions);
      cm_setInfowindow(infowindow);
      infowindow.open(map, marker);
      marker.setIcon(markerImageOut);
   });
   /*
   google.maps.event.addListener(marker, "mouseover", function() {
      marker.setIcon(markerImageOver);
   });
   google.maps.event.addListener(marker, "mouseout", function() {
      marker.setIcon(markerImageOut);
   });
   */

   return marker;
}

setTimeout('cm_load()', 300); 

//]]>