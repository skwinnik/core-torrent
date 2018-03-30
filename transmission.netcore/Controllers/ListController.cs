using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using transmission.netcore.Models;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Transmission.API.RPC.Entity;

namespace transmission.netcore.Controllers {
    public class ListController : Controller {
        public ListController(ITorrentRepository repository) {
            TorrentRepository = repository;
        }
        protected ITorrentRepository TorrentRepository { get; }
        private IEnumerable<TorrentViewModel> GetTorrentsByStatus(string status = "") {
            IEnumerable<TorrentViewModel> data;
            switch (status) {
                case "active":
                    data = TorrentRepository.Torrents.Select(t => new TorrentViewModel(t)).Where(t => t.IsActive());
                    break;
                case "inactive":
                    data = TorrentRepository.Torrents.Select(t => new TorrentViewModel(t)).Where(t => !t.IsActive());
                    break;
                case "completed":
                    data = TorrentRepository.Torrents.Select(t => new TorrentViewModel(t)).Where(t => t.IsCompleted());
                    break;
                case "inprocess":
                    data = TorrentRepository.Torrents.Select(t => new TorrentViewModel(t)).Where(t => !t.IsCompleted());
                    break;
                default:
                    data = TorrentRepository.Torrents.Select(t => new TorrentViewModel(t));
                    break;
            }
            return data;
        }

        public IActionResult Index(string status = "") {
            return View(GetTorrentsByStatus(status));
        }
        public IActionResult TorrentList(string status = "") {
            return PartialView(GetTorrentsByStatus(status));
        }
        public IActionResult TorrentFiles(int torrentId) {
            TorrentInfo info = TorrentRepository.GetTorrentById(torrentId);
            IEnumerable<FileInfo> files = info.Files.Join(info.FileStats,
                fileInfo => Array.IndexOf(info.Files, fileInfo),
                fileStats => Array.IndexOf(info.FileStats, fileStats),
                (fileInfo, fileStats) => new FileInfo(fileInfo, fileStats));
            ViewData["torrentId"] = torrentId;
            return PartialView("Components/TorrentFiles/TorrentFiles", files);
        }
        public IActionResult AddTorrent(AddTorrentViewModel torrent, string status = "") {
            if (ModelState.IsValid)
                TorrentRepository.AddTorrent(torrent.Magnet, torrent.Path);
            ViewBag.NewTorrent = torrent;
            return PartialView("TorrentList", GetTorrentsByStatus(status));
        }

        //AJAX API
        public JsonResult GetTorrentsJson(string status = "") {
            return Json(GetTorrentsByStatus(status));
        }
        public JsonResult StartTorrents(int[] ids) {
            TorrentRepository.StartTorrents(ids);
            return Json("OK");
        }
        public JsonResult StopTorrents(int[] ids) {
            TorrentRepository.StopTorrents(ids);
            return Json("OK");
        }
    }
}