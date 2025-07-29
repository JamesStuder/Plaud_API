namespace API.Plaud.NET.Constants
{
    public static class Endpoints
    {
        /// <summary>
        /// Defines the base URL used by the Plaud API for all HTTP requests.
        /// Serves as the foundational address to which specific endpoint paths are appended,
        /// ensuring consistent communication with the API.
        /// Changes to this value generally occur only if the API's host or domain is updated.
        /// </summary>
        internal const string BaseUrl = "https://api.plaud.ai";

        /// <summary>
        /// 
        /// </summary>
        internal const string Authentication = "/auth/access-token";

        /// <summary>
        /// Represents the default query parameters applied when listing records
        /// through the Plaud API. These include pagination settings, trash
        /// status, sorting attributes, and order direction.
        /// Ensures a predefined behavior for retrieving lists of records.
        /// </summary>
        internal const string ListRecordsDefaultQuery = "skip=0&limit=99999&is_trash=2&sort_by=start_time&is_desc=true";

        /// <summary>
        /// Represents the endpoint for generating shareable links to access specific files.
        /// This allows users to get URLs that can be shared externally, providing
        /// controlled access to file resources through the Plaud API.
        /// </summary>
        /// <remarks>
        /// Need to append the id of the file to create the link for to the end of the URL
        /// </remarks>
        internal const string ShareableLink = "/file/share-url/";

        /// <summary>
        /// Represents the endpoint path used to interact with file tag
        /// functionalities in the Plaud API. This endpoint facilitates
        /// operations such as retrieving, creating, or managing tags
        /// associated with files.
        /// </summary>
        internal const string FileTags = "/filetag/";

        /// <summary>
        /// Represents the API endpoint used to retrieve a list of recordings in a simplified web format.
        /// Provides access to stored audio or file recordings managed by the system.
        /// Typically aligned with query parameters to filter and sort the retrieved data.
        /// </summary>
        /// <remarks>
        /// Query string parameters get appended to the end of the url to apply filtering
        /// </remarks>
        internal const string ListRecordings = "/file/simple/web";

        /// <summary>
        /// Represents the endpoint used to retrieve specific recordings
        /// by their unique identifiers in the Plaud API.
        /// Allows for targeted fetching of recordings through ID-based queries,
        /// supporting precise and efficient data retrieval operations.
        /// </summary>
        internal const string GetRecordingsById = "/file/list";

        /// <summary>
        /// Represents the endpoint used to retrieve information pertaining to uploads,
        /// including configuration details and metadata, within the Plaud API. This endpoint
        /// supports the management and handling of file upload processes.
        /// </summary>
        internal const string UploadInfo = "/others/upload-info";

        /// <summary>
        /// Represents the endpoint for retrieving a temporary URL for accessing an audio file.
        /// This URL is generally used for short-term access and may expire after a defined period.
        /// </summary>
        /// <remarks>
        /// Need to append the id of the file to create the link for to the end of the URL
        /// </remarks>
        internal const string GetAudioFileTempUrl = "/file/temp-url/";

        /// <summary>
        /// Represents the API endpoint for exporting a document.
        /// This endpoint is specifically used to facilitate the process of
        /// exporting document-related data to an external format or location.
        /// </summary>
        internal const string ExportDocument = "/file/document/export";

        /// <summary>
        /// Represents the API endpoint path for retrieving authenticated user information.
        /// This constant is used to request the details of the currently authenticated user
        /// from the Plaud API. It provides access to user-specific data such as profile information
        /// and account settings.
        /// </summary>
        internal const string GetMyUser = "/user/me";

        /// <summary>
        /// Represents the endpoint for retrieving the AI system's operational status.
        /// Used to query the current state or availability of AI-related processes
        /// within the Plaud API framework.
        /// </summary>
        internal const string GetStatus = "/ai/status";

        /// <summary>
        /// Represents the endpoint for file operations within the Plaud API.
        /// Facilitates actions such as retrieval, management, and handling of file-related metadata.
        /// Used as the foundational route for API requests targeting file-specific resources.
        /// </summary>
        internal const string File = "/file/";

        /// <summary>
        /// Specifies the API endpoint used to restore files or recordings that were previously marked as trash.
        /// This endpoint facilitates the untrashing functionality, allowing files to be returned to their original state
        /// or location within the system.
        /// </summary>
        internal const string UnTrash = "/file/untrash/";

        /// <summary>
        /// Represents the API endpoint used for managing and retrieving files marked as trash.
        /// This endpoint facilitates operations related to soft-deleted files, such as listing
        /// or restoring them from the trash section within the system.
        /// </summary>
        internal const string Trash = "/file/trash/";
    }
}