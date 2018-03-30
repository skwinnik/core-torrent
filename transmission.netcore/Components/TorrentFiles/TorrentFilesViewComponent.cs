using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using transmission.netcore.Models;
using Transmission.API.RPC.Entity;

namespace transmission.netcore.Views.TorrentFiles {
    public class TorrentFilesViewComponent : ViewComponent {
        public TorrentFilesViewComponent(ITorrentRepository repository) {
            TorrentRepository = repository;
        }
        protected ITorrentRepository TorrentRepository { get; }

        public async Task<IViewComponentResult> InvokeAsync(int torrentId) {
            TorrentInfo info = TorrentRepository.GetTorrentById(torrentId);
            IEnumerable<FileInfo> files = info.Files.Join(info.FileStats,
                fileInfo => Array.IndexOf(info.Files, fileInfo),
                fileStats => Array.IndexOf(info.FileStats, fileStats),
                (fileInfo, fileStats) => new FileInfo(fileInfo, fileStats));
            ViewData["torrentId"] = torrentId;
            return View("TorrentFiles", files);
        }
    }
}
