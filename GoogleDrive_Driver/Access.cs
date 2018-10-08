using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Windows.Forms;
using Google.Apis.Drive.v3;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.Threading;
using Google.Apis.Drive.v3.Data;
using System.Collections.Generic;
using Google.Apis.Upload;
using Google.Apis.Download;

namespace gd_Driver
{
    public partial class Access : Form
    {
        public string email,nome, folderID = "";

        public Access()
        {
            InitializeComponent();

            AccessToken.Text = "";
            refreshToken.Text = "";
            Expire.Text = "";
        }

        private void btnAuth_Click(object sender, EventArgs e)
        {
            Auth m = new Auth();
            var result = m.ShowDialog();

            if (result == DialogResult.OK)
            {
                AccessToken.Text = m.access.Access_token;
                refreshToken.Text = m.access.refresh_token;

                if (DateTime.Now < m.access.created.AddHours(1))
                {
                    Expire.Text = m.access.created.AddHours(1).Subtract(DateTime.Now).Minutes.ToString();
                    getgoogleplususerdataSer(AccessToken.Text);
                }
            }


        }

        private void Access_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] scopes = new string[] { DriveService.Scope.Drive,
                             DriveService.Scope.DriveFile};
            var clientId = "781187646141-cj76ogmifqklfirjae62afm8blo2v4a0.apps.googleusercontent.com";
            var clientSecret = "5PGwIurTyd_7lA4tOGLp_oO_";

            var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(new ClientSecrets
            {
                ClientId = clientId,
                ClientSecret = clientSecret
            },
                                                                    scopes,
                                                                    Environment.UserName,
                                                                    CancellationToken.None,
                                                                    new FileDataStore("Epocum_Remote")).Result;

            DriveService service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Epocum",
            });

            service.HttpClient.Timeout = TimeSpan.FromMinutes(100);

            // Define parameters of request.
            FilesResource.ListRequest listRequest = service.Files.List();
            listRequest.PageSize = 10;
            listRequest.Fields = "nextPageToken, files(id, name)";

            // List files.
            IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute()
                .Files;

            int a = 0;
            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    listBox1.Items.Insert(a, "Filename: " + file.Name + "  ID: " + file.Id);
                    a++;
                }
            }
            else
            {
                listBox1.Items.Insert(a, "No files found.");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

            string[] scopes = new string[] { DriveService.Scope.Drive,
                             DriveService.Scope.DriveFile};
            var clientId = "781187646141-cj76ogmifqklfirjae62afm8blo2v4a0.apps.googleusercontent.com";
            var clientSecret = "5PGwIurTyd_7lA4tOGLp_oO_";

            var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(new ClientSecrets
            {
                ClientId = clientId,
                ClientSecret = clientSecret
            },
                                                                    scopes,
                                                                    Environment.UserName,
                                                                    CancellationToken.None,
                                                                    new FileDataStore("Epocum_Remote")).Result;

            DriveService service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Epocum",
            });

            service.HttpClient.Timeout = TimeSpan.FromMinutes(100);
            var fileMetadata = new File()
            {
                Name = "TEST2",
                MimeType = "application/vnd.google-apps.folder"
            };
            var request = service.Files.Create(fileMetadata);
            request.Fields = "id";
            var file = request.Execute();
            //Console.WriteLine("Folder ID: " + file.Id);
        }

        private void button3_Click(object sender, EventArgs e)
        {

            string[] scopes = new string[] { DriveService.Scope.Drive,
                             DriveService.Scope.DriveFile};
            var clientId = "781187646141-cj76ogmifqklfirjae62afm8blo2v4a0.apps.googleusercontent.com";
            var clientSecret = "5PGwIurTyd_7lA4tOGLp_oO_";

            var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(new ClientSecrets
            {
                ClientId = clientId,
                ClientSecret = clientSecret
            },
                                                                    scopes,
                                                                    Environment.UserName,
                                                                    CancellationToken.None,
                                                                    new FileDataStore("Epocum_Remote")).Result;

            DriveService service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Epocum",
            });

            service.HttpClient.Timeout = TimeSpan.FromMinutes(100);
            var fileMetadata = new File()
            {
                Name = "home.jpg"
            };
            FilesResource.CreateMediaUpload request;
            using (var stream = new System.IO.FileStream("files/home.jpg",
                                    System.IO.FileMode.Open))
            {
                request = service.Files.Create(
                    fileMetadata, stream, "image/jpeg");
                request.Fields = "id";
                request.Upload();
            }
            var file = request.ResponseBody;
            Console.WriteLine("File ID: " + file.Id);
        }

        private void button4_Click(object sender, EventArgs e)
        {

            string[] scopes = new string[] { DriveService.Scope.Drive,
                             DriveService.Scope.DriveFile};
            var clientId = "781187646141-cj76ogmifqklfirjae62afm8blo2v4a0.apps.googleusercontent.com";
            var clientSecret = "5PGwIurTyd_7lA4tOGLp_oO_";

            var credential = GoogleWebAuthorizationBroker.AuthorizeAsync(new ClientSecrets
            {
                ClientId = clientId,
                ClientSecret = clientSecret
            },
                                                                    scopes,
                                                                    Environment.UserName,
                                                                    CancellationToken.None,
                                                                    new FileDataStore("Epocum_Remote")).Result;

            DriveService service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = "Epocum",
            });

            service.HttpClient.Timeout = TimeSpan.FromMinutes(100);
            var fileId = "0BwwA4oUTeiV1UVNwOHItT0xfa2M";
            var request = service.Files.Get(fileId);
            var stream = new System.IO.MemoryStream();

            // Add a handler which will be notified on progress changes.
            // It will notify on each chunk download and when the
            // download is completed or failed.
            request.MediaDownloader.ProgressChanged +=
                (IDownloadProgress progress) =>
                {
                    switch (progress.Status)
                    {
                        case DownloadStatus.Downloading:
                            {
                                Console.WriteLine(progress.BytesDownloaded);
                                break;
                            }
                        case DownloadStatus.Completed:
                            {
                                Console.WriteLine("Download complete.");
                                break;
                            }
                        case DownloadStatus.Failed:
                            {
                                Console.WriteLine("Download failed.");
                                break;
                            }
                    }
                };
            request.Download(stream);
        }

        private async void getgoogleplususerdataSer(string access_token)
        {
            try
            {
                HttpClient client = new HttpClient();
                var urlProfile = "https://www.googleapis.com/oauth2/v1/userinfo?access_token=" + access_token;

                client.CancelPendingRequests();
                HttpResponseMessage output = await client.GetAsync(urlProfile);

                if (output.IsSuccessStatusCode)
                {
                    string outputData = await output.Content.ReadAsStringAsync();
                    GoogleUserOutputData serStatus = JsonConvert.DeserializeObject<GoogleUserOutputData>(outputData);

                    if (serStatus != null)
                    {
                        nome = serStatus.name;
                        email = serStatus.email;
                        this.label5.Text = email;
                        this.label7.Text = nome;
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
                //catching the exception
            }
        }


        public class GoogleUserOutputData
        {
            public string id { get; set; }
            public string name { get; set; }
            public string given_name { get; set; }
            public string email { get; set; }
            public string picture { get; set; }
        }

        

    }


}
