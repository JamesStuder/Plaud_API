using API.Plaud.NET.Constants;
using API.Plaud.NET.Interfaces;
using API.Plaud.NET.Models.Requests;
using API.Plaud.NET.Models.Responses;
using API.Plaud.NET.Services;

namespace API.Plaud.NET.Tests
{
    public class Tests
    {
        private IPlaudApiService _plaudApiService;
        private string? _PlaudUserName;
        private string? _PlaudPassword;
        private ResponseAuth? _AuthResponse;
        
        [OneTimeSetUp]
        public void Setup()
        {
            _plaudApiService = new PlaudApiService();
            _PlaudUserName = Environment.GetEnvironmentVariable("PlaudUserName", EnvironmentVariableTarget.User);
            _PlaudPassword = Environment.GetEnvironmentVariable("PlaudPassword", EnvironmentVariableTarget.User);
            _AuthResponse = _plaudApiService.AuthenticateAsync(_PlaudUserName,_PlaudPassword).Result;
        }

        [Test]
        public void AuthenticationTest()
        {
            Assert.That(_AuthResponse, Is.Not.Null);
            Assert.That(_AuthResponse.AccessToken, Is.Not.Null);
            Assert.That(_plaudApiService.AccessToken, Is.Not.Null);
            Assert.That(_AuthResponse.AccessToken, Is.EqualTo(_plaudApiService.AccessToken));
        }
        
        [Test]
        public void GetMyUserTest()
        {
            Assert.That(_AuthResponse, Is.Not.Null);
            Assert.That(_AuthResponse.AccessToken, Is.Not.Null);
            Assert.That(_plaudApiService.AccessToken, Is.Not.Null);
            
            ResponseUser myUser = _plaudApiService.GetMyUserAsync().Result;
            Assert.That(myUser, Is.Not.Null);
            Assert.That(myUser.DataUser.Email, Is.EqualTo(_PlaudUserName));
        }
        
        [Test]
        public void  GetStatusTest()
        {
            Assert.That(_AuthResponse, Is.Not.Null);
            Assert.That(_AuthResponse.AccessToken, Is.Not.Null);
            Assert.That(_plaudApiService.AccessToken, Is.Not.Null);
            
            ResponseStatus status = _plaudApiService.GetStatusAsync().Result;
            Assert.That(status, Is.Not.Null);
            Assert.That(status.Status, Is.EqualTo(0));
        }
        
        [Test]
        public void GetAllRecordingsTest()
        {
            Assert.That(_AuthResponse, Is.Not.Null);
            Assert.That(_AuthResponse.AccessToken, Is.Not.Null);
            Assert.That(_plaudApiService.AccessToken, Is.Not.Null);
            
            ResponseListRecordings recordings = _plaudApiService.GetAllRecordingsAsync().Result;
            Assert.That(recordings, Is.Not.Null);
            Assert.That(recordings.DataFileList, Is.Not.Empty);
        }
        
        [Test]
        public void GetRecordingsApplyFilterTest()
        {
            Assert.That(_AuthResponse, Is.Not.Null);
            Assert.That(_AuthResponse.AccessToken, Is.Not.Null);
            Assert.That(_plaudApiService.AccessToken, Is.Not.Null);
            
            ResponseListRecordings recordings = _plaudApiService.GetRecordingsApplyFilterAsync(0, 1, 2, "start_time", true).Result;
            Assert.That(recordings, Is.Not.Null);
            Assert.That(recordings.DataFileList, Is.Not.Empty);
            Assert.That(recordings.DataFileList, Has.Count.EqualTo(1));
        }
        
        [Test]
        public void GetSpecificRecordingsTest()
        {
            Assert.That(_AuthResponse, Is.Not.Null);
            Assert.That(_AuthResponse.AccessToken, Is.Not.Null);
            Assert.That(_plaudApiService.AccessToken, Is.Not.Null);
            
            ResponseListRecordings recordings = _plaudApiService.GetAllRecordingsAsync().Result;
            Assert.That(recordings, Is.Not.Null);
            Assert.That(recordings.DataFileList, Is.Not.Empty);
            
            ResponseListRecordings specificRecording = _plaudApiService.GetSpecificRecordingsAsync( new List<string>() { recordings.DataFileList[0].Id }).Result;
            Assert.That(specificRecording, Is.Not.Null);
            Assert.That(specificRecording.DataFileList, Is.Not.Empty);
            Assert.That(specificRecording.DataFileList, Has.Count.EqualTo(1));
            Assert.That(specificRecording.DataFileList[0].Id, Is.EqualTo(recordings.DataFileList[0].Id));
        }
        [Test]
        public void GetFileTagsTest()
        {
            Assert.That(_AuthResponse, Is.Not.Null);
            Assert.That(_AuthResponse.AccessToken, Is.Not.Null);
            Assert.That(_plaudApiService.AccessToken, Is.Not.Null);
            
            ResponseFileTags fileTags = _plaudApiService.GetFileTagsAsync().Result;
            Assert.That(fileTags, Is.Not.Null);
            Assert.That(fileTags.DataFiletagList, Is.Not.Empty);
        }
        [Test]
        public void CreateShareableLinkTest()
        {
            Assert.That(_AuthResponse, Is.Not.Null);
            Assert.That(_AuthResponse.AccessToken, Is.Not.Null);
            Assert.That(_plaudApiService.AccessToken, Is.Not.Null);

            ResponseListRecordings recordings = _plaudApiService.GetAllRecordingsAsync().Result;
            Assert.That(recordings, Is.Not.Null);
            Assert.That(recordings.DataFileList, Is.Not.Empty);
            
            RequestShareableLinkPermissions permissions = new ()
            {
                IsMindmap = 1,
                IsAudio = 1,
                IsTrans = 1,
                IsAiContent = 1
            };
            ResponseShareableLink shareableLink = _plaudApiService.CreateShareableLinkAsync(recordings.DataFileList[0].Id, permissions).Result;
            Assert.That(shareableLink, Is.Not.Null);
            Assert.That(shareableLink.Url, Is.Not.Null);
        }
        [Test]
        public void DownloadAudioFileTest()
        {
            Assert.That(_AuthResponse, Is.Not.Null);
            Assert.That(_AuthResponse.AccessToken, Is.Not.Null);
            Assert.That(_plaudApiService.AccessToken, Is.Not.Null);
            
            ResponseListRecordings recordings = _plaudApiService.GetAllRecordingsAsync().Result;
            Assert.That(recordings, Is.Not.Null);
            Assert.That(recordings.DataFileList, Is.Not.Empty);
            
            string audioFile = _plaudApiService.DownloadAudioFileAsync(recordings.DataFileList[0].Id).Result;
            Assert.That(audioFile, Is.Not.Null);
            Assert.That(audioFile, Is.Not.Empty);
        }
        [Test]
        public void DownloadTranscriptFileTest()
        {
            Assert.That(_AuthResponse, Is.Not.Null);
            Assert.That(_AuthResponse.AccessToken, Is.Not.Null);
            Assert.That(_plaudApiService.AccessToken, Is.Not.Null);
            ResponseListRecordings recordings = _plaudApiService.GetAllRecordingsAsync().Result;
            Assert.That(recordings, Is.Not.Null);
            Assert.That(recordings.DataFileList, Is.Not.Empty);
            
            string transcriptFile = _plaudApiService.DownloadTranscriptFileAsync(recordings.DataFileList[0].Id, FileTypes.TXT).Result;
            Assert.That(transcriptFile, Is.Not.Null);
            Assert.That(transcriptFile, Is.Not.Empty);
        }
        [Test]
        public void DownloadSummaryFileTest()
        {            
            Assert.That(_AuthResponse, Is.Not.Null);
            Assert.That(_AuthResponse.AccessToken, Is.Not.Null);
            Assert.That(_plaudApiService.AccessToken, Is.Not.Null);
            
            ResponseListRecordings recordings = _plaudApiService.GetAllRecordingsAsync().Result;
            Assert.That(recordings, Is.Not.Null);
            Assert.That(recordings.DataFileList, Is.Not.Empty);
            
            string summaryFile = _plaudApiService.DownloadSummaryFileAsync(recordings.DataFileList[0].Id, FileTypes.TXT).Result;
            Assert.That(summaryFile, Is.Not.Null);
            Assert.That(summaryFile, Is.Not.Empty);
        }
        
        /// <summary>
        /// This test can be used to grab a specific transcript.  You will need the id.
        /// </summary>
        [Test]
        public void DownloadSpecificTranscriptFileTest()
        {            
            Assert.That(_AuthResponse, Is.Not.Null);
            Assert.That(_AuthResponse.AccessToken, Is.Not.Null);
            Assert.That(_plaudApiService.AccessToken, Is.Not.Null);
            
            string summaryFile = _plaudApiService.DownloadTranscriptFileAsync("", FileTypes.PDF).Result;
            Assert.That(summaryFile, Is.Not.Null);
            Assert.That(summaryFile, Is.Not.Empty);
        }
    }
}