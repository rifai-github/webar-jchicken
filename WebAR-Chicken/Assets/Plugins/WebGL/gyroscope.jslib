mergeInto(LibraryManager.library, {
    requestGyroPermission: function () {
        if (typeof DeviceMotionEvent.requestPermission === 'function') {
            DeviceMotionEvent.requestPermission().then(response => {
                if (response === 'granted') {
                    console.log("Gyroscope Access Granted!");
                } else {
                    console.log("Gyroscope Access Denied!");
                }
            }).catch(console.error);
        }
    }
});
