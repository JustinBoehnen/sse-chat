export const eventSourceHandler = {
    eventSource: null,

    startEventSource: function (url, dotNetObject) {
        if (this.eventSource) {
            this.eventSource.close();
        }

        this.eventSource = new EventSource(url);

        this.eventSource.onmessage = function (event) {
            const item = JSON.parse(event.data);
            console.log("New item received:", item);

            // Invoke .NET method with the received item
            dotNetObject.invokeMethodAsync('OnMessageReceived', item);
        };

        this.eventSource.onerror = function (error) {
            console.error("EventSource error:", error);
        };

        console.log("EventSource started");
    },

    stopEventSource: function () {
        if (this.eventSource) {
            this.eventSource.close();
            console.log("EventSource closed.");
        }
    }
};
