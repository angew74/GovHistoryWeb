var app = angular.module('GovApp', ['ngMessages', 'ngMaterial', 'ngAnimate', 'ngAria',])
.config(function ($mdThemingProvider) {

    var bluegreyMap = $mdThemingProvider.extendPalette('blue-grey', {
        '700': '#455A64',
        'contrastDefaultColor': 'dark'
    });

    // Register the new color palette map with the name <code>neonRed</code>
    $mdThemingProvider.definePalette('blue-grey', bluegreyMap);

    // Use that theme for the primary intentions
    $mdThemingProvider.theme('default')
      .primaryPalette('blue-grey');

});
app.config(function($mdIconProvider) {
    $mdIconProvider
      .defaultIconSet('img/icons/sets/core-icons.svg', 24);
})
app.filter('keyboardShortcut', function ($window) {
     return function (str) {
         if (!str) return;
         var keys = str.split('-');
         var isOSX = /Mac OS X/.test($window.navigator.userAgent);

         var seperator = (!isOSX || keys.length > 2) ? '+' : '';

         var abbreviations = {
             M: isOSX ? '⌘' : 'Ctrl',
             A: isOSX ? 'Option' : 'Alt',
             S: 'Shift'
         };

         return keys.map(function (key, index) {
             var last = index == keys.length - 1;
             return last ? key : abbreviations[key];
         }).join(seperator);
     };
 })

 