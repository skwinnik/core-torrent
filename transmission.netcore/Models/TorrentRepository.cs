using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transmission.API.RPC;
using Transmission.API.RPC.Entity;

namespace transmission.netcore.Models {
    public interface ITorrentRepository {
        TorrentInfo[] Torrents { get; }
        void StartTorrents(params int[] ids);
        void StopTorrents(params int[] ids);
        TorrentInfo GetTorrentById(int id);
        void AddTorrent(string magnet, string path);
    }
    public class TorrentRepository : ITorrentRepository {
        Client _client;
        SessionInfo _sessionInfo;
        public TorrentRepository(string url) {
            _client = new Client(url);
            _sessionInfo = _client.GetSessionInformation();
        }
        public TorrentInfo[] Torrents { get => _client.TorrentGet(TorrentFields.ALL_FIELDS.Except(new string[] { TorrentFields.FILE_STATS, TorrentFields.FILES }).ToArray()).Torrents; }
        public TorrentInfo GetTorrentById(int id) {
            return _client.TorrentGet(TorrentFields.ALL_FIELDS, id).Torrents.First();
        }
        public void AddTorrent(string magnet, string path) {
            _client.TorrentAdd(new Transmission.API.RPC.Entity.NewTorrent { Filename = magnet, DownloadDirectory = path });
        }
        public void StartTorrents(params int[] ids) {
            _client.TorrentStartAsync(ids);
        }
        public void StopTorrents(params int[] ids) {
            _client.TorrentStopAsync(ids);
        }
    }
}
