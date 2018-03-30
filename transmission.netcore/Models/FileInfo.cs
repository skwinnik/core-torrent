using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transmission.API.RPC.Entity;

namespace transmission.netcore.Models {
    public class FileInfo {
        public string Name { get; set; }
        public double Size { get; set; }
        public double SizeCompleted { get; set; }
        public bool Wanted { get; set; }
        public int Priority { get; set; }

        public FileInfo(TransmissionTorrentFiles file, TransmissionTorrentFileStats stats) {
            this.Name = file.Name;
            this.Size = file.Length;
            this.SizeCompleted = file.BytesCompleted;
            this.Wanted = stats.Wanted;
            this.Priority = stats.Priority;
        }
    }
}
