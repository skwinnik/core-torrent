window.charts = (function () {
    function ChartItem(downloadSpeed, uploadSpeed, time) {
        return { DownloadSpeed: downloadSpeed, UploadSpeed: uploadSpeed, Time: time };
    }
    function FillEmptyItems(array) {
        var baseDate = new Date();
        baseDate = baseDate.setMinutes(baseDate.getMinutes() - 1);
        for (var i = 0; i < 30; i++)
            array.push(ChartItem(0, 0, baseDate + i * 2000));
        return array;
    }
    this.onInit = function (s, e) {
        s.cpDataSource = FillEmptyItems([]);
        s.AddNewItem = function (item) {
            s.cpDataSource = s.cpDataSource.slice(1);
            s.cpDataSource.push(ChartItem(item.rateDownload.bytes, item.rateUpload.bytes, new Date()));
            s.SetDataSource(s.cpDataSource);
        }
    }
    return this;
})();