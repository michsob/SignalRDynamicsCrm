function OnSave() {

    var connection = $.connection('http://serverurl/signalr');
    $.connection.hub.url = 'http://serverurl/signalr/hubs';

    var loghub = $.connection.logHub;

    $.connection.hub.start({
        jsonp: true
    }).done(function () {
        var fullname = Xrm.Page.getAttribute("fullname").getValue();
        var date = new Date().toLocaleString();

        loghub.server.send(fullname, date);
    });
}