mergeInto(LibraryManager.library, {
    StartGyroscope: function () {
        function handleOrientation(event) {
            const data = {
                alpha: event.alpha,
                beta: event.beta,
                gamma: event.gamma
            };
            const json = JSON.stringify(data);

            // Kirim ke Unity
            SendMessage("GyroReceiver", "OnGyroData", json);
        }

        // iOS Safari permission
        if (typeof DeviceOrientationEvent !== "undefined" &&
            typeof DeviceOrientationEvent.requestPermission === "function") {
            DeviceOrientationEvent.requestPermission()
                .then(function (response) {
                    if (response === "granted") {
                        window.addEventListener("deviceorientation", handleOrientation, true);
                    }
                })
                .catch(console.error);
        } else {
            window.addEventListener("deviceorientation", handleOrientation, true);
        }
    }
});
