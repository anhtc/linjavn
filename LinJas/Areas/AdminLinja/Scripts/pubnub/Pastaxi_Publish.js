
//Cac bien da duoc dinh nghia trong Pastaxi_Subsribe.js

var person = {
    firstName: "John",
    lastName: "Doe",
    age: 50,
    eyeColor: "blue"
};

function testPubNub() {
    pubnub.publish({
        channel: channel,
        message: person
    });
}