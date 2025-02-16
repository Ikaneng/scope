﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using DemoInfo;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Threading;


namespace ECO
{
    class DownloadStreamClass
    {

        private Boolean isDownloading;
        private MemoryStream downloadedFile;
        private Boolean isReady;
        private String gameLink;

        public MemoryStream DownloadedFile { get => downloadedFile; set => downloadedFile = value; }
        public bool IsDownloading { get => isDownloading; set => isDownloading = value; }
        public bool IsReady { get => isReady; set => isReady = value; }
        public string GameLink { get => gameLink; set => gameLink = value; }

        public DownloadStreamClass()
        {
            IsReady = false;
            IsDownloading = false;
            DownloadedFile = new MemoryStream();
        }

        public Boolean DownloadFile(String filePath)
        {
            gameLink = filePath;
            try {
            /*if(downloadedFile != null) {
                DownloadedFile.Close();
            }*/
            WebClient wc = new WebClient();
            IsDownloading = true;
            using (MemoryStream stream = new MemoryStream(wc.DownloadData(filePath)))
            {
                Console.WriteLine();
                DownloadedFile = Decompress(stream);
                //Decompress(stream).CopyTo(DownloadedFile);
            }
            } catch
            {
                return false;
            }
            IsDownloading = false;
            IsReady = true;
            return true;
        }

        public static MemoryStream Decompress(MemoryStream fileToDecompress)
        {

            MemoryStream tempMemoryStream = new MemoryStream();
            using (MemoryStream originalFileStream = fileToDecompress)
            {
                using (GZipStream decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                {
                    decompressionStream.CopyTo(tempMemoryStream);
                    decompressionStream.Close();
                }
                originalFileStream.Close();
            }
            return tempMemoryStream;
        }



    }

}
