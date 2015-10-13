using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouTubeUserVideosParser {
	class Program {
		static void Main(string[] args) {
			Console.WriteLine("Downloading Source");

			var list = YouTubeUserVideos.GetFromUser("RamirentGroup");

			Console.WriteLine("Found " + list.Count + " videos.");

			foreach (var video in list) {
				Console.WriteLine("Code: " + video.Code);
				Console.WriteLine("Title: " + video.Title);
				Console.WriteLine("ImageURL: " + video.ImageURL);
				Console.WriteLine("----------------");
			}

			Console.ReadKey();
		}
	}
}
