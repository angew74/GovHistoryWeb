var app = angular.module('GovApp', ['ngMessages', 'ngMaterial', 'ngAnimate', 'ngAria', 'ngSanitize', 'md.data.table'])
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
app.config(function ($mdDateLocaleProvider) {

    // Example of a French localization.
    $mdDateLocaleProvider.months = ['gennaio', 'febbraio', 'marzo', 'aprile', 'maggio', 'giugno', 'luglio', 'agosto', 'settembre', 'ottobre', 'novembre', 'dicembre'];
    $mdDateLocaleProvider.shortMonths = ['gen', 'feb', 'mar', 'apr', 'mag', 'giu', 'lug', 'ago', 'set', 'ott', 'nov', 'dic'];
    $mdDateLocaleProvider.days = ['domenica', 'lunedì', 'martedì', 'mercoledì', 'giovedì', 'venerdì', 'sabato'];
    $mdDateLocaleProvider.shortDays = ['Do', 'Lu', 'Ma', 'Me', 'Gi', 'Ve', 'Sa'];

    // Can change week display to start on Monday.
    $mdDateLocaleProvider.firstDayOfWeek = 1;

    // Example uses moment.js to parse and format dates.
    $mdDateLocaleProvider.parseDate = function (dateString) {
        var m = moment(dateString, 'DD/MM/YYYY', true);
        return m.isValid() ? m.toDate() : new Date(NaN);
    };

    $mdDateLocaleProvider.formatDate = function (date) {
        return date ? moment(date).format('DD/MM/YYYY') : null;
    };

    //$mdDateLocaleProvider.monthHeaderFormatter = function(date) {
    //    return myShortMonths[date.getMonth()] + ' ' + date.getFullYear();
    //};

    // In addition to date display, date components also need localized messages
    // for aria-labels for screen-reader users.

    $mdDateLocaleProvider.weekNumberFormatter = function (weekNumber) {
        return 'Settimana ' + weekNumber;
    };

    $mdDateLocaleProvider.msgCalendar = 'Calendario';
    $mdDateLocaleProvider.msgOpenCalendar = 'Aprire il calendario';

    // You can also set when your calendar begins and ends.
    $mdDateLocaleProvider.firstRenderableDate = new Date(1870, 9, 20);
    $mdDateLocaleProvider.lastRenderableDate = new Date(2999, 12, 31);
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

 