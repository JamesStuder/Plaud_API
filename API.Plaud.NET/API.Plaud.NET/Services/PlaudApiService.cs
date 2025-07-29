using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Plaud.NET.Constants;
using API.Plaud.NET.DataAccess;
using API.Plaud.NET.Interfaces;
using API.Plaud.NET.Models;
using API.Plaud.NET.Models.Requests;
using API.Plaud.NET.Models.Responses;

namespace API.Plaud.NET.Services
{
    /// <inheritdoc />
    public class PlaudApiService : IPlaudApiService
    {
        /// <inheritdoc />
        public string AccessToken { get; set; }

        /// <summary>
        /// Instance of the PlaudApi class used for handling direct API interactions.
        /// Responsible for communicating with the Plaud backend, including authentication and data retrieval processes.
        /// </summary>
        private readonly PlaudApi _plaudApi;

        /// <summary>
        /// Provides an implementation of the IPlaudApiService interface for interacting with the Plaud API.
        /// Handles authentication, data retrieval, and management of recordings using the PlaudApi class.
        /// </summary>
        public PlaudApiService()
        {
            _plaudApi = new PlaudApi();
        }
        
        /// <inheritdoc />
        public async Task<ResponseAuth> AuthenticateAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                throw new Exception("Username or password cannot be empty.");
            }
            
            ResponseAuth authResponse = await _plaudApi.AuthWithApiAsync(username, password);
            if (string.IsNullOrEmpty(authResponse.AccessToken))
            {
                throw new Exception("Authentication failed.");
            }
            AccessToken = authResponse.AccessToken;
            return authResponse;
        }
        
        /// <inheritdoc />
        public async Task<ResponseUser> GetMyUserAsync()
        {
            return await _plaudApi.GetDataAsync<ResponseUser>(Endpoints.GetMyUser, AccessToken);
        }

        /// <inheritdoc />
        public async Task<ResponseStatus> GetStatusAsync()
        {
            return await _plaudApi.GetDataAsync<ResponseStatus>(Endpoints.GetStatus, AccessToken);
        }

        /// <inheritdoc />
        public async Task<ResponseListRecordings> GetAllRecordingsAsync()
        {
            return await _plaudApi.GetDataAsync<ResponseListRecordings>($"{Endpoints.ListRecordings}?{Endpoints.ListRecordsDefaultQuery}", AccessToken);
        }

        /// <inheritdoc />
        public async Task<ResponseListRecordings> GetRecordingsApplyFilterAsync(int skip, int limit, int isTrash, string sortBy, bool isDesc)
        {
            return await _plaudApi.GetDataAsync<ResponseListRecordings>($"{Endpoints.ListRecordings}?skip={skip}&limit={limit}&is_trash={isTrash}&sort_by={sortBy}&is_desc={isDesc}", AccessToken);
        }

        /// <inheritdoc />
        public async Task<ResponseListRecordings> GetSpecificRecordingsAsync(List<string> recordingIds)
        {
            if (recordingIds == null || recordingIds.Count == 0)
            {
                throw new Exception("Recording IDs cannot be empty.");
            }
            return await _plaudApi.PostDataAsync<ResponseListRecordings>(Endpoints.GetRecordingsById, recordingIds, AccessToken);
        }

        /// <inheritdoc />
        public async Task<ResponseFileTags> GetFileTagsAsync()
        {
            return await _plaudApi.GetDataAsync<ResponseFileTags>(Endpoints.FileTags, AccessToken);
        }

        /// <inheritdoc />
        public async Task<ResponseShareableLink> CreateShareableLinkAsync(string recordingId, RequestShareableLinkPermissions permissions)
        {
            if (string.IsNullOrEmpty(recordingId))
            {
                throw new Exception("Recording ID cannot be empty.");
            }

            if (permissions == null)
            {
                throw new Exception("Permissions cannot be empty.");
            }
            
            return await _plaudApi.PostDataAsync<ResponseShareableLink>($"{Endpoints.ShareableLink}{recordingId}", permissions, AccessToken);
        }

        /// <inheritdoc />
        public async Task<string> DownloadAudioFileAsync(string recordingId)
        {
            if (string.IsNullOrEmpty(recordingId))
            {
                throw new Exception("Recording ID cannot be empty.");
            }
            
            RequestUploadInfo uploadInfo = new RequestUploadInfo
            {
                Info = new Info
                {
                    EventCat = "share",
                    EventParam = new EventParam
                    {
                        Action = "export_audio",
                        FileKey = recordingId,
                        FileID = recordingId,
                        From = "web"
                    }
                }
            };
            ResponseUploadInfo responseUploadInfo = await _plaudApi.PostDataAsync<ResponseUploadInfo>(Endpoints.UploadInfo, uploadInfo, AccessToken);
            if (responseUploadInfo.Msg != "success")
            {
                throw new Exception("Upload Info failed.");
            }
            ResponseAudioTempUrl responseAudioTempUrl = await _plaudApi.GetDataAsync<ResponseAudioTempUrl>($"{Endpoints.GetAudioFileTempUrl}{recordingId}", AccessToken);
            if (string.IsNullOrEmpty(responseAudioTempUrl.TempUrl))
            {
                throw new Exception("No download url found.");
            }
            return await DownloadFile.DownloadFileAsBase64Async(responseAudioTempUrl.TempUrl);
        }

        /// <inheritdoc />
        public async Task<string> DownloadTranscriptFileAsync(string recordingId, string fileType)
        {
            if (string.IsNullOrEmpty(recordingId) && string.IsNullOrEmpty(fileType))
            {
                throw new Exception("Recording ID and File Type cannot be empty.");
            }
            
            RequestUploadInfo uploadInfo = new RequestUploadInfo
            {
                Info = new Info
                {
                    EventCat = "share",
                    EventParam = new EventParam
                    {
                        Action = "export_transcription",
                        FileKey = recordingId,
                        FileID = recordingId,
                        From = "web"
                    }
                }
            };
            ResponseUploadInfo responseUploadInfo = await _plaudApi.PostDataAsync<ResponseUploadInfo>(Endpoints.UploadInfo, uploadInfo, AccessToken);
            if (responseUploadInfo.Msg != "success")
            {
                throw new Exception("Upload Info failed.");
            }
            ResponseListRecordings recording = await _plaudApi.PostDataAsync<ResponseListRecordings>(Endpoints.GetRecordingsById, new List<string> { recordingId }, AccessToken);
            if (recording.DataFileTotal != 1)
            {
                throw new Exception($"Unable to locate recording with provided ID {recordingId}.");
            }
            bool hasSpeaker = recording.DataFileList[0].TransResult.Any(s => string.IsNullOrEmpty(s.Speaker));
            bool hasTimestamp = recording.DataFileList[0].TransResult.Any(s => s.StartTime >= 0);
            RequestExportFile exportFile = new RequestExportFile
            {
                FileId = recordingId,
                PromptType = "trans",
                ToFormat = fileType,
                Title = recording.DataFileList[0].Filename,
                CreateTime = DateService.ConvertTimeStampToFormattedDateTime(recording.DataFileList[0].StartTime),
                WithSpeaker = hasSpeaker ? 1 : 0,
                WithTimestamp = hasTimestamp ? 1 : 0,
                TransContent = recording.DataFileList[0].TransResult
            };
            ResponseExportFile responseExportFile = await _plaudApi.PostDataAsync<ResponseExportFile>(Endpoints.ExportDocument, exportFile, AccessToken);
            if (string.IsNullOrEmpty(responseExportFile.Data))
            {
                throw new Exception("No download url found.");
            }
            return await DownloadFile.DownloadFileAsBase64Async(responseExportFile.Data);
        }

        /// <inheritdoc />
        public async Task<string> DownloadSummaryFileAsync(string recordingId, string fileType)
        {
            if (string.IsNullOrEmpty(recordingId) && string.IsNullOrEmpty(fileType))
            {
                throw new Exception("Recording ID and File Type cannot be empty.");
            }
            ResponseListRecordings recording = await _plaudApi.PostDataAsync<ResponseListRecordings>(Endpoints.GetRecordingsById, new List<string> { recordingId }, AccessToken);
            if (recording.DataFileTotal != 1)
            {
                throw new Exception($"Unable to locate recording with provided ID {recordingId}.");
            }
            bool hasSpeaker = recording.DataFileList[0].TransResult.Any(s => string.IsNullOrEmpty(s.Speaker));
            bool hasTimestamp = recording.DataFileList[0].TransResult.Any(s => s.StartTime >= 0);
            RequestExportSummary exportFile = new RequestExportSummary
            {
                FileId = recordingId,
                PromptType = "summary",
                ToFormat = fileType,
                Title = recording.DataFileList[0].Filename,
                CreateTime = DateService.ConvertTimeStampToFormattedDateTime(recording.DataFileList[0].StartTime),
                WithSpeaker = hasSpeaker ? 1 : 0,
                WithTimestamp = hasTimestamp ? 1 : 0,
                SummaryContent = recording.DataFileList[0].AiContent
            };
            ResponseExportFile responseExportFile = await _plaudApi.PostDataAsync<ResponseExportFile>(Endpoints.ExportDocument, exportFile, AccessToken);
            if (string.IsNullOrEmpty(responseExportFile.Data))
            {
                throw new Exception("No download url found.");
            }
            return await DownloadFile.DownloadFileAsBase64Async(responseExportFile.Data);
        }

        /// <inheritdoc />
        public async Task<bool> TrashRecordingsAsync(List<string> recordingIds)
        {
            if (recordingIds == null || recordingIds.Count == 0)
            {
                throw new Exception("Recording IDs cannot be empty.");
            }

            return await _plaudApi.PostDataAsyncNoResponseBody(Endpoints.Trash, recordingIds.ToArray(), AccessToken);
        }

        /// <inheritdoc />
        public async Task<bool> UnTrashRecordingsAsync(List<string> recordingIds)
        {
            if (recordingIds == null || recordingIds.Count == 0)
            {
                throw new Exception("Recording IDs cannot be empty.");
            }
            
            return await _plaudApi.PostDataAsyncNoResponseBody(Endpoints.UnTrash, recordingIds.ToArray(), AccessToken);
        }

        /// <inheritdoc />
        public async Task<bool> PermanentlyDeleteRecordingsAsync(List<string> recordingIds)
        {
            if (recordingIds == null || recordingIds.Count == 0)
            {
                throw new Exception("Recording IDs cannot be empty.");
            }
            
            return await _plaudApi.DeleteRequestWithBodyAsyncNoResponseBody(Endpoints.File, recordingIds.ToArray(), AccessToken);
        }
    }
}