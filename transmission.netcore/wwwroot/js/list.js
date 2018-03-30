window.list = (function () {
    //live data
    var liveCells = [{ name: "RateDownload", value: function (item) { return item.rateDownload.string } },
    { name: "RateUpload", value: function (item) { return item.rateUpload.string } },
    { name: "ETA", value: function (item) { return item.eta.string } },
    { name: "Downloaded", value: function (item) { return item.downloaded.string } },
    { name: "Uploaded", value: function (item) { return item.uploaded.string } }];
    var liveCharts = ["SpeedStats"];

    function scheduleGridUpdate() {
        window.setTimeout(updateGrid, 2000);
    }
    function updateGrid() {
        TorrentList.FetchJsonData(function (data) {
            if (data && data.length)
                data.forEach(function (item) {
                    updateCells(item);
                    updateCharts(item);
                });
            scheduleGridUpdate();
        }
        );
    }
    function updateData(data) {
        data.forEach(function (item) {
            updateCells(item);
            updateCharts(item);
        });
    }
    function updateCells(item) {
        liveCells.forEach(function (cell) {
            $('.' + cell.name + item.id).text(cell.value(item));
        });
    }
    function updateCharts(item) {
        liveCharts.forEach(function (chart) {
            if (window[chart + item.id] && window[chart + item.id].AddNewItem)
                window[chart + item.id].AddNewItem(item);
        })
    }
    //event handlers
    this.onGridInit = function (s, e) {
        s.FetchJsonData = function (callback) {
            $.ajax({ method: 'post', url: 'List/GetTorrentsJson', data: { 'status': document.getElementById("Status").value }, success: callback });
        }
        scheduleGridUpdate();
    };
    this.onGridBeginCallback = function (s, e) {
        e.customArgs["status"] = document.getElementById("Status").value;
    };
    this.onToolbarItemClick = function (s, e) {
        switch (e.item.name) {
            case "Start":
                $.ajax({ method: 'POST', url: "/List/StartTorrents", data: { ids: s.GetSelectedKeysOnPage() } });
                break;
            case "Stop":
                $.ajax({ method: 'POST', url: "/List/StopTorrents", data: { ids: s.GetSelectedKeysOnPage() } });
                break;
        }
    };
    this.onAddTorrentClick = function (s, e) {
        TorrentList.UpdateEdit();
    };
    this.onCancelAddTorrentClick = function (s, e) {
        TorrentList.CancelEdit();
    };
    return this;
})();