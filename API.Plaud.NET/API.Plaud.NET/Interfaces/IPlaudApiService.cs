using System.Collections.Generic;
using System.Threading.Tasks;
using API.Plaud.NET.Models.Requests;
using API.Plaud.NET.Models.Responses;

namespace API.Plaud.NET.Interfaces
{
    public interface IPlaudApiService
    {
        /// <summary>
        /// Gets or sets the access token used for authenticating requests
        /// to the API. This property holds the bearer token required to
        /// perform authorized operations with the API service.
        /// </summary>
        string AccessToken { get; set; }

        /// <summary>
        /// Authenticates the user asynchronously using the provided credentials.
        /// </summary>
        /// <param name="username">The username of the user to authenticate.</param>
        /// <param name="password">The password of the user to authenticate.</param>
        /// <returns>A task representing the asynchronous operation, with a response containing authentication details.  <see cref="AccessToken"/> will be set if the request is successful.</returns>
        Task<ResponseAuth> AuthenticateAsync(string username, string password);

        /// <summary>
        /// Retrieves the current user's profile information asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, with a response containing the user's profile details.</returns>
        Task<ResponseUser> GetMyUserAsync();

        /// <summary>
        /// Retrieves the current status information asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, with a response containing the status details.</returns>
        Task<ResponseStatus> GetStatusAsync();

        /// <summary>
        /// Retrieves all recordings asynchronously without any filters or pagination.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, with a response containing the list of all recordings and related metadata.</returns>
        Task<ResponseListRecordings> GetAllRecordingsAsync();

        /// <summary>
        /// Retrieves a filtered list of recordings asynchronously based on the specified criteria.
        /// </summary>
        /// <param name="skip">The number of recordings to skip from the beginning of the list.</param>
        /// <param name="limit">The maximum number of recordings to retrieve.</param>
        /// <param name="isTrash">Indicates whether to include only trashed recordings. Use 1 for true and 0 for false.</param>
        /// <param name="sortBy">The field by which to sort the recordings (e.g., "date", "name").</param>
        /// <param name="isDesc">Indicates whether the sorting should be in descending order. Use true for descending, false for ascending.</param>
        /// <returns>A task representing the asynchronous operation, with a response containing the filtered list of recordings.</returns>
        Task<ResponseListRecordings> GetRecordingsApplyFilterAsync(int skip, int limit, int isTrash, string sortBy, bool isDesc);

        /// <summary>
        /// Retrieves specific recordings asynchronously based on the provided list of recording IDs.
        /// </summary>
        /// <param name="recordingIds">The list of recording IDs to retrieve details for.</param>
        /// <returns>A task representing the asynchronous operation, with a response containing the details of the specified recordings.</returns>
        Task<ResponseListRecordings> GetSpecificRecordingsAsync(List<string> recordingIds);

        /// <summary>
        /// Retrieves the file tags associated with files asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, with a response containing file tags information.</returns>
        Task<ResponseFileTags> GetFileTagsAsync();

        /// <summary>
        /// Creates a shareable link for a specified recording with the provided permissions.
        /// </summary>
        /// <param name="recordingId">The unique identifier of the recording for which to create the shareable link.</param>
        /// <param name="permissions">The set of permissions to apply for the shareable link.</param>
        /// <returns>A task representing the asynchronous operation, with a response containing the shareable link details.</returns>
        Task<ResponseShareableLink> CreateShareableLinkAsync(string recordingId, RequestShareableLinkPermissions permissions);

        /// <summary>
        /// Downloads an audio file for the specified recording asynchronously.
        /// </summary>
        /// <param name="recordingId">The unique identifier of the recording to download.</param>
        /// <returns>A task representing the asynchronous operation, with a string containing the content of the downloaded audio file.</returns>
        /// <remarks>
        /// Base64 string will be returned and audio files are .mp3 format.
        /// </remarks>
        Task<string> DownloadAudioFileAsync(string recordingId);

        /// <summary>
        /// Downloads the transcript file for a specific recording asynchronously.
        /// </summary>
        /// <param name="recordingId">The unique identifier of the recording for which the transcript file needs to be downloaded.</param>
        /// <param name="fileType">The type of file format in which the transcript is to be downloaded (e.g., PDF, DOCX).</param>
        /// <returns>A task representing the asynchronous operation, with a response containing the file content as a string.</returns>
        Task<string> DownloadTranscriptFileAsync(string recordingId, string fileType);

        /// <summary>
        /// Downloads the summary file for a specified recording asynchronously.
        /// </summary>
        /// <param name="recordingId">The identifier of the recording for which the summary file is to be downloaded.</param>
        /// <param name="fileType">The type of the file to download, such as "pdf" or "txt".</param>
        /// <returns>A task representing the asynchronous operation, with a result containing the file path or file content as a string.</returns>
        Task<string> DownloadSummaryFileAsync(string recordingId, string fileType);

        /// <summary>
        /// Moves the specified recordings to the trash asynchronously.
        /// </summary>
        /// <param name="recordingIds">A list of recording IDs to be moved to the trash.</param>
        /// <returns>A task representing the asynchronous operation. With a boolean representing if the process was successful or not.</returns>
        Task<bool> TrashRecordingsAsync(List<string> recordingIds);

        /// <summary>
        /// Restores recordings from the trash asynchronously.
        /// </summary>
        /// <param name="recordingIds">A list of recording IDs to be restored from the trash.</param>
        /// <returns>A task representing the asynchronous operation. With a boolean representing if the process was successful or not.</returns>
        Task<bool> UnTrashRecordingsAsync(List<string> recordingIds);

        /// <summary>
        /// Permanently deletes the specified recordings from the trash asynchronously.
        /// </summary>
        /// <param name="recordingIds">A list of recording IDs to be permanently deleted.</param>
        /// <returns>A task representing the asynchronous operation. With a boolean representing if the process was successful or not.</returns>
        Task<bool> PermanentlyDeleteRecordingsAsync(List<string> recordingIds);
    }
}