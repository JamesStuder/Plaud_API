# Plaud API .NET Standard 2.0 Library
This is an UNOFFICIAL .NET Standard 2.0 Library for the PLAUD API.  This is NOT supported by the PLAUD team and was created by me to use in other projects I have to fill gaps that the platform doesn't support yet.  Feel free to use this Library in your projects to work with the API to perform tasks that you need with the recordings that you have created.

# Implementation

1) To implement this project you can either download the project and include it in yours or use the nuget package (https://www.nuget.org/packages/API.Plaud.NET/).
2) Instantiate the Interface:
   ```csharp
   IPlaudApiService plaudApi = new PlaudApiService();
    ```
3) Authenticate with the API:
   ```csharp
   ResponseAuth authRequest = await plaudApi.AuthenticateAsync("username", "password");
   ```
4) Make requests

# Supported Endpoints:
- **AuthenticateAsync**
  - Authentic with the Plaud API.  Only supported username and password authentication at this time.
- **GetMyUserAsync**
  - Get your users information
- **GetStatusAsync**
  - Get status of API and system
- **GetAllRecordingsAsync**
  - Get a list of all your recordings
- **GetRecordingsApplyFilterAsync**
  - Same as get all recordings but allow you to apply filtering
- **GetSpecificRecordingsAsync**
  - Pass 1 or more recording ids in a list to get just those recordings
- **GetFileTagsAsync**
  - Get all your file tags (folders)
- **CreateShareableLinkAsync**
  - Create a shareable link for a recording
- **DownloadAudioFileAsync**
  - Download an audio (MP3) file.  This will return the BASE64 string for you to convert.
- **DownloadTranscriptFileAsync**
  - Download the transcript file.  This will return the BASE64 string for you to convert.
- **DownloadSummaryFileAsync**
  - Download the summary file.  This will return the BASE64 string for you to convert.
- **TrashRecordingsAsync**
  - Moves recordings(s) to the trash can.
- **UnTrashRecordingsAsync**
  - Moves the recording(s) out of the trash can.
- **PermanentlyDeleteRecordingsAsync**
  - Permanently deletes the recoding(s) from the trash can.

# Notes:
- Supported File Types:
  - Use ```FileTypes``` static class which has all the supported file types you can pass.  Not all types are supported by all endpoints.  Check the Plaud UI to see what you can export for a transcript vs summary vs audio.  You also don't have to use these, you can pass just a string when a file type is needed.  These are provided to assist.