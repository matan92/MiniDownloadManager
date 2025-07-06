using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniDownloadManager
{
    public class FileOption
    {
        public string? Title { get; set; }
        public string? ImageURL { get; set; }
        public string? FileURL { get; set; }
        public int Score { get; set; }
        public Validator? Validators { get; set; }
    }
}
