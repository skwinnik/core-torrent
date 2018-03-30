using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace transmission.netcore.Models
{
    public class LiveCellViewModel
    {
        public LiveCellViewModel(int id, string fieldName, string value) {
            this.ID = id;
            this.FieldName = fieldName;
            this.Value = value;
        }
        public LiveCellViewModel(DevExpress.AspNetCore.Bootstrap.BootstrapGridViewDataItemTemplateContainerSettings<TorrentViewModel> t) {
            this.ID = (int)t.KeyValue;
            this.FieldName = t.Column.FieldName;
            this.Value = t.Eval(t.Column.FieldName).ToString();
        }
        public int ID { get; set; }
        public string FieldName { get; set; }
        public string Value { get; set; }
    }
}
