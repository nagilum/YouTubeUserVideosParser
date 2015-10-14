# YouTube User Videos Parser

A simple C# class to fetch a list of latest YouTube videos for a user.

	var videos = YouTubeUserVideos.GetFromUser("FailArmy");

This will give you a list of the latest videos (50 I believe) from the given user.
The metadata will include the thumbnail image URL, code/id for the video, and title.

To include the description, you have to add the second parameter in the GetFromUser function.

	var videos = YouTubeUserVideos.GetFromUser("FailArmy", true);

Warning!
Including the descriptions will invoke an extra request pr. video, since the description isn't included in the general videos page.
