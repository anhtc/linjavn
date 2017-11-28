
var channel1 = "pastaxi_channel1";
var pubnub1;

(function () {

    pubnub1 = PUBNUB.init({
        publish_key: 'pub-c-aedde538-6a7a-4e15-98e9-3785df1d355c',
        subscribe_key: 'sub-c-13b753b4-cc4e-11e3-9782-02ee2ddab7fe'
    });

    pubnub1.subscribe({
        channel: channel1,
        message: function (msg) {
            alert(msg);
        },

        presence: function (m) { log("Presenced :" + JSON.stringify(m)); },
        connect: function () { log("Connected"); },
        disconnect: function () { log("Disconnected"); },
        reconnect: function () { log("Reconnected"); },
    });
})();

function testPubNub() {
    pubnub1.publish({
        channel: channel1,
        message: 'nguyen duc chien'
    });
}


