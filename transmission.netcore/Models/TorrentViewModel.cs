using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Transmission.API.RPC.Entity;
using transmission.netcore.Utils;
using System.ComponentModel;
using System.Globalization;
using System.Drawing;

namespace transmission.netcore.Models {
    public class TorrentViewModel {
        public int ID { get; set; }
        public string Name { get; set; }
        public TorrentSize TotalSize { get; set; }
        [Display(Name = "Done")]
        public double PercentDone { get; set; }
        public TorrentSize Downloaded { get; set; }
        public TorrentSize Uploaded { get; set; }
        public TorrentEta ETA { get; set; }
        public TorrentSpeed RateDownload { get; set; }
        public TorrentSpeed RateUpload { get; set; }
        public int Status { get; set; }
        public string Hash { get; set; }
        public string DownloadDir { get; set; }

        public TorrentViewModel() { }
        public TorrentViewModel(TorrentInfo t) {
            this.ID = t.ID;
            this.Name = t.Name;
            this.TotalSize = new TorrentSize(t.TotalSize);
            this.PercentDone = t.PercentDone * 100;
            this.Downloaded = new TorrentSize((long)(t.TotalSize * t.PercentDone));
            this.Uploaded = new TorrentSize(t.UploadedEver);
            this.ETA = new TorrentEta(t.ETA);
            this.RateDownload = new TorrentSpeed(t.RateDownload);
            this.RateUpload = new TorrentSpeed(t.RateUpload);
            this.Status = t.Status;
            this.Hash = t.HashString;
            this.DownloadDir = t.DownloadDir;
        }

        public bool IsActive() {
            return this.Status != 0;
        }

        public bool IsCompleted() {
            return this.PercentDone == 100;
        }
    }

    public class TorrentSize : IComparable {
        public long Bytes { get; set; }
        public string String { get; set; }
        public TorrentSize(long bytes) {
            Bytes = bytes;
            String = bytes.ToSizeString();
        }
        public int CompareTo(object obj) {
            return Bytes.CompareTo(((TorrentSize)obj).Bytes);
        }
        public override string ToString() {
            return String;
        }
    }

    public class TorrentSpeed : TorrentSize {
        public TorrentSpeed(long bytes) : base(bytes) {
            String += "/s";
        }
    }

    public class TorrentEta : IComparable {
        public int Seconds { get; set; }
        public string String { get; set; }
        public TorrentEta(int etaSeconds) {
            Seconds = etaSeconds;
            if (etaSeconds < 0)
                String = "N/A";
            else if (etaSeconds / (60 * 60 * 24) > 0)
                String = TimeSpan.FromSeconds(etaSeconds).ToString(@"d\d\ hh\h\ mm\m\ ss\s");
            else
                String = TimeSpan.FromSeconds(etaSeconds).ToString(@"hh\h\ mm\m\ ss\s");
        }
        public int CompareTo(object obj) {
            return Seconds.CompareTo(((TorrentEta)obj).Seconds);
        }
        public override string ToString() {
            return String;
        }
    }
}
