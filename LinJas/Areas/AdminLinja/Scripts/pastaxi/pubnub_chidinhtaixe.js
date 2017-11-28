var pubnub;
pubnub = PUBNUB.init({
    publish_key: PUBLISH_KEY,
    subscribe_key: SUBSCRIBE_KEY
});

function PublishAlertChiDinhTaiXe(datXeId) {
    
    pubnub.publish({
        channel: PASGO_ALERT_SHOWNOTE,
        message: { "type": "chidinhtaixe", "DatXeId": datXeId }

    });
    
}



