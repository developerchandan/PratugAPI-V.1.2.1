using System;
using System.Collections.Generic;
using System.Text;

namespace Prayug.Infrastructure
{
    public class GlobalSettings
    {
        public static string WebRootPath { get; set; }
        public static string ContentRootPath { get; set; }
        public static string FolerName { get; set; }
        public static string SqliteFolerPath { get; set; }
        public static string JsonSyncFile { get; set; }
        public static string SqliteFileName { get; set; }
        public static string DeliverySqliteFileName { get; set; }
        public static string AbsolutePath { get; set; }
        public static string FileExtensions { get; set; }
        public static string ApiKey { get; set; }
        public static string Audience { get; set; }
        public static string Issuer { get; set; }
        public static int MaxFileSizeLimit { get; set; } = 20;
        public static bool IsDevelopment { get; set; }
        public static bool isEmailEnable { get; set; }
        public static bool isWhatsappEnable { get; set; }
        public static string NotificationServerUrl { get; set; }
        public static string MediaServerUrl { get; set; }
        public static string push_Payment_path { get; set; }
        public static string push_trip_order_path { get; set; }
        public static string LogConnection { get; set; }
        public static string ImgURL { get; set; }
        public static string calling_icon { get; set; }
        public static string PrintImgURL { get; set; }
        public static string SmtpServer { get; set; }
        public static string SmtpPort { get; set; }
        public static string SmtpUsername { get; set; }
        public static string SmtpPassword { get; set; }
    }
}
