using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

/// <summary>
/// Parserclass to fetch a list of YouTube videos from a username or URL.
/// </summary>
public class YouTubeUserVideos {
	/// <summary>
	/// Get a list of YouTube videos from username.
	/// </summary>
	public static List<YouTubeVideo> GetFromUser(string username) {
		return GetFromURL(string.Format("https://www.youtube.com/user/{0}/videos", username));
	}

	/// <summary>
	/// Get a list of YouTube videos from URL.
	/// </summary>
	public static List<YouTubeVideo> GetFromURL(string url) {
		var list = new List<YouTubeVideo>();
		var webClient = new WebClient();
		var source = string.Empty;

		try {
			var bytes = webClient.DownloadData(url);
			source = Encoding.UTF8.GetString(bytes);
		}
		catch {}

		if (string.IsNullOrWhiteSpace(source))
			return list;

		var sp = source.IndexOf("/watch?v=", StringComparison.InvariantCultureIgnoreCase);

		while (sp > -1) {
			var video = parseSource(ref list, ref source, sp);

			if (video != null)
				list.Add(video);

			source = source.Substring(sp + 3000);
			sp = source.IndexOf("/watch?v=", StringComparison.InvariantCultureIgnoreCase);
		}

		return list;
	}

	/// <summary>
	/// Parse a section of the source for video entry.
	/// </summary>
	private static YouTubeVideo parseSource(ref List<YouTubeVideo> videos, ref string source, int sp) {
		var temp = source.Substring(sp, 3500);

		// code
		var code = temp.Substring(temp.IndexOf("=", StringComparison.InvariantCultureIgnoreCase) + 1);
		code = code.Substring(0, code.IndexOf("\"", StringComparison.InvariantCultureIgnoreCase));

		if (videos.Any(v => v.Code == code))
			return null;

		// title
		var title = temp.Substring(temp.IndexOf("href=\"/watch?v=" + code + "\">", StringComparison.InvariantCultureIgnoreCase));
		title = title.Substring(title.IndexOf(">", StringComparison.InvariantCultureIgnoreCase) + 1);
		title = title.Substring(0, title.IndexOf("<", StringComparison.InvariantCultureIgnoreCase));

		return new YouTubeVideo {
			Code = code,
			Title = title,
		};
	}
}

/// <summary>
/// YouTube video entry.
/// </summary>
public class YouTubeVideo {
	/// <summary>
	/// Video watch code.
	/// </summary>
	public string Code { get; set; }

	/// <summary>
	/// Video title.
	/// </summary>
	public string Title { get; set; }

	/// <summary>
	/// Thumbnail image URL.
	/// </summary>
	public string ImageURL {
		get { return string.Format("//i.ytimg.com/vi/{0}/mqdefault.jpg", Code); }
	}

	/// <summary>
	/// Full YouTube URL.
	/// </summary>
	public string URL {
		get { return string.Format("//www.youtube.com/watch?v={0}", Code); }
	}
}